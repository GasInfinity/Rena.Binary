using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rena.Binary.Annotations;

namespace Rena.Binary.SourceGenerator
{
    [Generator]
    public class SourceGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValuesProvider<TypeDeclarationSyntax> typesWithBinaryStructurableAttribute = context.SyntaxProvider
            .CreateSyntaxProvider(
                static (n, _) => IsTypeAndHasAttributes(n),
                static (ctx, _) => GetPossibleBinaryStructurable(ctx)
            ).Where(static m => m is not null)!;

            IncrementalValueProvider<(Compilation, ImmutableArray<TypeDeclarationSyntax>)> compilationAndBinaryStructurables = context.CompilationProvider.Combine(typesWithBinaryStructurableAttribute.Collect());

            context.RegisterSourceOutput(compilationAndBinaryStructurables,
                static (spc, cbs) => Execute(cbs.Item1, cbs.Item2, spc));
        }

        private static void Execute(Compilation compilation, ImmutableArray<TypeDeclarationSyntax> binaryStructurables, SourceProductionContext context)
        {
            // Nothing to do
            if (binaryStructurables.IsDefaultOrEmpty)
                return;

            IEnumerable<TypeDeclarationSyntax> distinctTypes = binaryStructurables.Distinct();

            if (TryProcessTypes(compilation, distinctTypes, context, out List<StructurableType> processedStructurables))
            {
                foreach (var structurable in processedStructurables)
                    SourceGeneration.GenerateStructurable(structurable, context);
            }
        }

        private static bool TryProcessTypes(Compilation compilation, IEnumerable<TypeDeclarationSyntax> distinctTypes, SourceProductionContext context, out List<StructurableType> processedTypes)
        {
            processedTypes = new();
            INamedTypeSymbol? binaryStructurableSymbol = compilation.GetTypeByMetadataName(typeof(BinaryStructurable).FullName);

            // Wtf, something is wrong
            if (binaryStructurableSymbol is null)
                return false;

            bool noError = true; // If we do this, we can report more errors!
            foreach (var typeSyntax in distinctTypes)
            {
                context.CancellationToken.ThrowIfCancellationRequested();

                string typeName = typeSyntax.Identifier.ToString();
                bool isPartial = false;
                bool isAbstract = false;

                foreach (SyntaxToken token in typeSyntax.Modifiers) // Check if type is partial
                {
                    switch (token.Kind())
                    {
                        case SyntaxKind.PartialKeyword:
                            isPartial = true;
                            break;
                        case SyntaxKind.AbstractKeyword:
                            isAbstract = true;
                            break;
                    }
                }

                if (!isPartial) // We can't extend the IBinaryStructurable<T> interface
                {
                    context.ReportDiagnostic(Diagnostic.Create(Diagnostics.TypeIsNotPartial, typeSyntax.GetLocation(), typeName));
                    noError = false;
                    continue;
                }

                if (isAbstract) // We can't instantiate abstract types
                {
                    context.ReportDiagnostic(Diagnostic.Create(Diagnostics.TypeIsAbstract, typeSyntax.GetLocation(), typeName));
                    noError = false;
                    continue;
                }

                if (TryProcessMembers(compilation, typeSyntax, context, out var processedMembers) && Util.TryGetParentClasses(typeSyntax, context, out var parentTypes))
                {
                    TypeData data = new(typeSyntax, parentTypes);
                    processedTypes.Add(new(data, processedMembers));
                }
                else
                {
                    noError = false;
                }
            }

            return noError;
        }

        private static bool TryProcessMembers(Compilation compilation, TypeDeclarationSyntax typeSyntax, SourceProductionContext context, out List<IncludedMember> processedMembers)
        {
            processedMembers = new();
            SemanticModel sema = compilation.GetSemanticModel(typeSyntax.SyntaxTree);
            INamedTypeSymbol type = sema.GetDeclaredSymbol(typeSyntax)!;

            bool noError = true; // If we do this, we can report more errors!
            foreach (var member in type.GetMembers())
            {
                if (member.Kind is not SymbolKind.Field and not SymbolKind.Property)
                    continue;

                ImmutableArray<AttributeData> attributes = member.GetAttributes();
                AttributeData? binaryIncludeAttribute = null;

                foreach (var attribute in attributes)
                {
                    if (attribute.AttributeClass is not null && Util.IsBinaryIncludeAttribute(attribute.AttributeClass))
                        binaryIncludeAttribute = attribute;
                }

                if (binaryIncludeAttribute is null) // Not included :p
                    continue;

                ITypeSymbol memberType = default!; // This won't be null

                if (member is IPropertySymbol property)
                {
                    if (property.IsIndexer) // We can't include an indexer
                    {
                        context.ReportDiagnostic(Diagnostic.Create(Diagnostics.PropertyIsIndexer, member.Locations.FirstOrDefault(), type.Name));
                        noError = false;
                        continue;
                    }

                    if (property.IsReadOnly || property.IsWriteOnly) // It must be readable and writable
                    {
                        context.ReportDiagnostic(Diagnostic.Create(Diagnostics.PropertyMustBeReadableAndWritable, member.Locations.FirstOrDefault(), type.Name));
                        noError = false;
                        continue;
                    }

                    memberType = property.Type;
                }
                else if (member is IFieldSymbol field)
                {
                    memberType = field.Type;
                }

                MemberTypeData typeData = new(memberType);

                if (!typeData.IsCompatibleMember) // We don't know how to read or write this
                {
                    context.ReportDiagnostic(Diagnostic.Create(Diagnostics.MemberTypeIsNotPrimitiveOrBinaryStructurable, member.Locations.FirstOrDefault(), type.Name));
                    noError = false;
                    continue;
                }

                if (typeData.IsFixed)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Diagnostics.MemberIsNotFixed, member.Locations.FirstOrDefault(), memberType.Name, type.Name));
                    noError = false;
                    continue;
                }

                string memberName = member.Name;
                IncludedData data = Util.GetBinaryIncludedData(binaryIncludeAttribute);

                processedMembers.Add(new(new(memberType), memberName, data));
            }

            return noError;
        }

        private static bool IsTypeAndHasAttributes(SyntaxNode node)
            => node is TypeDeclarationSyntax { AttributeLists.Count: > 0 };

        private static TypeDeclarationSyntax? GetPossibleBinaryStructurable(GeneratorSyntaxContext context)
        {
            var node = (context.Node as TypeDeclarationSyntax)!;

            foreach (AttributeListSyntax attributeList in node.AttributeLists)
            {
                foreach (AttributeSyntax attribute in attributeList.Attributes)
                {
                    // Weird, we didn't get the attribute symbol
                    if (context.SemanticModel.GetSymbolInfo(attribute).Symbol is not IMethodSymbol attributeSymbol)
                        continue;

                    INamedTypeSymbol attributeType = attributeSymbol.ContainingType;
                    string attributeTypeFullName = attributeType.ToDisplayString();

                    if (attributeTypeFullName == typeof(BinaryStructurable).FullName)
                        return node;
                }
            }

            return null;
        }
    }
}
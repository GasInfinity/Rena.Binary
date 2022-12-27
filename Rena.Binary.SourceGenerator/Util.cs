using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rena.Binary.Annotations;
using Rena.Binary.Common;

namespace Rena.Binary.SourceGenerator;

public static class Util
{
    public static string GetNamespace(BaseTypeDeclarationSyntax baseTypeSyntax)
    {
        StringBuilder fullNamespace = new();

        SyntaxNode? parentSyntax = baseTypeSyntax.Parent;

        while (parentSyntax is not null and not NamespaceDeclarationSyntax and not FileScopedNamespaceDeclarationSyntax)
            parentSyntax = parentSyntax.Parent;

        if (parentSyntax is BaseNamespaceDeclarationSyntax namespaceSyntax)
        {
            fullNamespace.Append(namespaceSyntax.Name.ToString());

            while (namespaceSyntax.Parent is NamespaceDeclarationSyntax parentNamespace)
            {
                fullNamespace.Insert(0, '.');
                fullNamespace.Insert(0, parentNamespace.Name.ToString());
                namespaceSyntax = parentNamespace;
            }
        }

        return fullNamespace.ToString();
    }

    public static bool TryGetParentClasses(TypeDeclarationSyntax typeSyntax, SourceProductionContext context, out StructurableParentType? parentClasses)
    {
        static bool IsTypeKind(SyntaxKind kind)
            => kind is SyntaxKind.RecordDeclaration
            or SyntaxKind.RecordStructDeclaration
            or SyntaxKind.ClassDeclaration
            or SyntaxKind.StructDeclaration;

        parentClasses = null;
        TypeDeclarationSyntax? parentSyntax = typeSyntax;

        while ((parentSyntax = parentSyntax!.Parent as TypeDeclarationSyntax) is not null && IsTypeKind(parentSyntax.Kind()))
        {
            if (!IsPartial(parentSyntax))
            {
                context.ReportDiagnostic(Diagnostic.Create(Diagnostics.ParentTypeIsNotPartial, parentSyntax.GetLocation(),
                                                           parentSyntax.Identifier.ToString(), typeSyntax.Identifier.ToString()));
                return false;
            }

            parentClasses = new(new(parentSyntax, parentClasses));
        }

        return true;
    }

    public static bool IsBinaryStructurable(ITypeSymbol type)
    {
        // First check if it has the attribute
        foreach (var attribute in type.GetAttributes())
        {
            if (attribute.AttributeClass is not INamedTypeSymbol namedType)
                continue;

            if (namedType.ToDisplayString() == typeof(BinaryStructurable).FullName)
                return true;
        }

        // Then check if it inherits IBinaryStructurable<T>
        foreach (var iFace in type.AllInterfaces)
        {
            string iFaceString = $"{ConstantStrings.GlobalNamespacePrefix}{iFace.ToDisplayString()}";

            if (iFaceString.Equals($"{ConstantStrings.IBinaryStructurableFullName}<{type.ToDisplayString()}>"))
                return true;
        }

        return false;
    }

    public static bool IsBinaryIncludeAttribute(INamedTypeSymbol attributeType)
        => attributeType.ToString() == typeof(BinaryInclude).FullName;

    public static bool IsPartial(TypeDeclarationSyntax typeSyntax)
    {
        bool isPartialType = false;

        foreach (SyntaxToken token in typeSyntax.Modifiers) // Check if type is partial
        {
            if (token.IsKind(SyntaxKind.PartialKeyword))
                isPartialType = true;
        }

        return isPartialType;
    }

    public static IncludedData GetBinaryIncludedData(AttributeData binaryInclude)
    {
        PrimitiveEndianness endianness = PrimitiveEndianness.Little;

        foreach (var argument in binaryInclude.NamedArguments)
        {
            switch (argument.Key)
            {
                case "Endianness":
                    endianness = (PrimitiveEndianness)(argument.Value.Value ?? PrimitiveEndianness.Little);
                    break;
            }
        }

        return new(endianness);
    }
}
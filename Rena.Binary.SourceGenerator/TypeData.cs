
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rena.Binary.SourceGenerator;

namespace Rena.Binary.SourceGenerator;

public readonly record struct TypeData(string NamespacePath, StructurableParentType? ParentType, TypeKind Kind, string ShortName, string LongName, string Constraits)
{
    public TypeData(TypeDeclarationSyntax typeSyntax, StructurableParentType? parentType) : this(Util.GetNamespace(typeSyntax),
                                                                                                 parentType,
                                                                                                 TypeKindExtensions.FromSyntaxKind(typeSyntax.Kind()),
                                                                                                 typeSyntax.Identifier.ToString(),
                                                                                                 typeSyntax.Identifier.ToString() + typeSyntax.TypeParameterList?.ToString(),
                                                                                                 typeSyntax.ConstraintClauses.ToString())
    {
    }
}
using Microsoft.CodeAnalysis.CSharp;

namespace Rena.Binary.SourceGenerator;

public enum TypeKind : byte
{
    Struct,
    RecordStruct,
    Class,
    Record
}

public static class TypeKindExtensions
{
    public static string ToDisplayString(this TypeKind kind)
        => kind switch
        {
            TypeKind.Struct => "struct",
            TypeKind.RecordStruct => "record struct",
            TypeKind.Class => "class",
            TypeKind.Record => "record",
            _ => string.Empty
        };

    public static bool IsValueType(this TypeKind kind)
        => kind < TypeKind.Class;

    public static TypeKind FromSyntaxKind(SyntaxKind kind)
        => kind switch
        {
            SyntaxKind.RecordDeclaration => TypeKind.Record,
            SyntaxKind.RecordStructDeclaration => TypeKind.RecordStruct,
            SyntaxKind.ClassDeclaration => TypeKind.Class,
            SyntaxKind.StructDeclaration => TypeKind.Struct,
            _ => TypeKind.Class
        };
}
using Microsoft.CodeAnalysis;

namespace Rena.Binary.SourceGenerator;

public static class Diagnostics
{
    public static readonly DiagnosticDescriptor TypeIsNotPartial = new("RB0001", "Non partial [BinaryStructurable]", "Type '{0}' is not partial, add the partial modifier or remove the [BinaryStructurable] attribute", "Type", DiagnosticSeverity.Error, true);
    public static readonly DiagnosticDescriptor ParentTypeIsNotPartial = new("RB0002", "Non partial [BinaryStructurable] parent type", "Parent type '{0}' of type '{1}' is not partial, add the partial modifier or remove the [BinaryStructurable] attribute", "Type", DiagnosticSeverity.Error, true);
    public static readonly DiagnosticDescriptor TypeIsAbstract = new("RB0003", "Abstract [BinaryStructurable]", "Type '{0}' is abstract, remove the abstract modifier or remove the [BinaryStructurable] attribute", "Type", DiagnosticSeverity.Error, true);
    public static readonly DiagnosticDescriptor PropertyIsIndexer = new("RB0004", "Property must not be an indexer", "An indexer property can't be included in a [BinaryStructurable]", "Property", DiagnosticSeverity.Error, true);
    public static readonly DiagnosticDescriptor PropertyMustBeReadableAndWritable = new("RB0005", "Property must be Readable and Writable", "A property that is ReadOnly or WriteOnly can not be included in a [BinaryStructurable]", "Property", DiagnosticSeverity.Error, true);
    public static readonly DiagnosticDescriptor MemberTypeIsNotPrimitiveOrBinaryStructurable = new("RB0006", "Member must be Primitive or [BinaryStructurable]", "Member inside type '{0}' must be Primitive or [BinaryStructurable]", "Member", DiagnosticSeverity.Error, true);
    public static readonly DiagnosticDescriptor RecursiveMemberType = new("RB0007", "Member type must not be recursive", "Member inside type '{0}' must not be recursive", "Member", DiagnosticSeverity.Error, true);
    public static readonly DiagnosticDescriptor MemberIsNotFixed = new("RB0008", "Member type must have a fixed size", "Member with dynamic size type '{0}' inside type '{1}' must be a fixed size type", "Member", DiagnosticSeverity.Error, true);
}
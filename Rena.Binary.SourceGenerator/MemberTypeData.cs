using System.Text;
using Microsoft.CodeAnalysis;
using Rena.Binary.Common;

namespace Rena.Binary.SourceGenerator;

public record struct MemberTypeData(DatatypeKind Kind, string DisplayString, bool IsBinaryStructurable, bool IsValueType)
{
    public bool IsPrimitive
        => Kind.IsPrimitive();

    public bool IsSingleByte
        => Kind.IsSingleByte();

    public bool IsFixed
        => Kind.IsFixed();

    public bool IsCompatibleMember
        => IsPrimitive || IsBinaryStructurable;

    public MemberTypeData(ITypeSymbol type) : this(type.SpecialType switch
    {
        SpecialType.System_Boolean => DatatypeKind.Boolean,
        SpecialType.System_Byte => DatatypeKind.Byte,
        SpecialType.System_SByte => DatatypeKind.SByte,
        SpecialType.System_Int16 => DatatypeKind.Short,
        SpecialType.System_UInt16 => DatatypeKind.UShort,
        SpecialType.System_Int32 => DatatypeKind.Int,
        SpecialType.System_UInt32 => DatatypeKind.UInt,
        SpecialType.System_Int64 => DatatypeKind.Long,
        SpecialType.System_UInt64 => DatatypeKind.ULong,
        SpecialType.System_Single => DatatypeKind.Float,
        SpecialType.System_Double => DatatypeKind.Double,
        SpecialType.System_IntPtr => DatatypeKind.NativeInt,
        SpecialType.System_UIntPtr => DatatypeKind.NativeUInt,
        SpecialType.System_Char => DatatypeKind.Char,
        _ => DatatypeKind.UserDefined
    },
        type.ToDisplayString(),
        Util.IsBinaryStructurable(type),
        type.IsValueType)
    {
    }

    public string ToNullableString()
        => IsValueType ? ToDisplayString() : $"{ToDisplayString()}?";

    public string ToDisplayString()
        => IsPrimitive ? DisplayString : $"{ConstantStrings.GlobalNamespacePrefix}{DisplayString}";

    public override string ToString()
        => DisplayString;
}
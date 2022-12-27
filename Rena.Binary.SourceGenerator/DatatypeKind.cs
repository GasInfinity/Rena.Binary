namespace Rena.Binary.SourceGenerator;

public enum DatatypeKind
{
    Boolean,
    Byte,
    SByte,
    Short,
    UShort,
    Int,
    UInt,
    Long,
    ULong,
    Float,
    Double,
    NativeInt,
    NativeUInt,
    Char,

    UserDefined
}

public static class DatatypeKindExtensions
{
    public static bool IsPrimitive(this DatatypeKind kind)
        => kind < DatatypeKind.UserDefined;

    public static bool IsSingleByte(this DatatypeKind kind)
        => kind < DatatypeKind.Short;

    public static bool IsFixed(this DatatypeKind kind)
        => kind is >= DatatypeKind.NativeInt and <= DatatypeKind.NativeUInt;
}
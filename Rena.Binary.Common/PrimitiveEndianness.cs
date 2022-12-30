namespace Rena.Binary.Common;

/// <summary>
/// An enumeration that defines the endianness of a primitive type.
/// </summary>
public enum PrimitiveEndianness : byte
{
    /// <summary>
    /// Represents a little-endian value, where the least significant byte is stored first.
    /// </summary>
    Little,

    /// <summary>
    /// Represents a big-endian value, where the most significant byte is stored first.
    /// </summary>
    Big
}
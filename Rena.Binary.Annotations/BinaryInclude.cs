using Rena.Binary.Common;

namespace Rena.Binary.Annotations;

/// <summary>
/// Attribute that specifies that a field or property should be included in a [BinaryStructurable]
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class BinaryInclude : Attribute
{
    /// <summary>
    /// The endianness of the field or property when serialized or deserialized.
    /// </summary>
    public PrimitiveEndianness Endianness { get; set; }
}
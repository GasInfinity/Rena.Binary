using Rena.Binary.Common;

namespace Rena.Binary.Annotations;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class BinaryInclude : Attribute
{
    public PrimitiveEndianness Endianness { get; set; }
}
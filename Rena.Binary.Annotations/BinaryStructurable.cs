namespace Rena.Binary.Annotations;

/// <summary>
/// Attribute that specifies that the source generator should implement IBinaryStructurable<T> and it's methods on a type.
/// </summary>
[AttributeUsage(AttributeTargets.Struct)]
public class BinaryStructurable : Attribute
{
}
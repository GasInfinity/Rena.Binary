using System.Text;

namespace Rena.Binary.SourceGenerator;

public record StructurableParentType(TypeData Data)
{
    public void GeneratePrefix(StringBuilder builder)
    {
        builder.Append("partial ").Append(Data.Kind.ToDisplayString()).Append(' ').Append(Data.ShortName).Append(' ').Append(Data.Constraits)
        .Append('{');
        Data.ParentType?.GeneratePrefix(builder); // TODO: Here it is the Child Type but... shhhh!
    }

    public void GenerateSuffix(StringBuilder builder)
    {
        Data.ParentType?.GenerateSuffix(builder);
        builder.Append('}').Append('\n');
    }

    public string ToFileString()
        => $"{Data.ShortName}{(Data.ParentType is not null ? $".{Data.ParentType.ToFileString()}" : string.Empty)}";
}
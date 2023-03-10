using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Rena.Binary.SourceGenerator;

public static class SourceGeneration
{
    public static void GenerateStructurable(StructurableType structurable, SourceProductionContext context)
    {
        StringBuilder output = new();
        output.Append("#nullable enable").Append('\n');
        output.Append("/// <auto-generated/>").Append('\n');
        output.Append($"using {ConstantStrings.RenaBinaryNamespace};")
        .Append($"using {ConstantStrings.SystemBuffersFullName};");

        structurable.Generate(output);

        // Maybe we could generate it without indentation
        var source = CSharpSyntaxTree.ParseText(output.ToString());
        var root = source.GetRoot().NormalizeWhitespace();
        context.AddSource(structurable.ToFileString(), SourceText.From(root.ToFullString(), Encoding.UTF8));
    }
}
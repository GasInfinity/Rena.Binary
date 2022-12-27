using System.Text;

namespace Rena.Binary.SourceGenerator;

public readonly record struct StructurableType(TypeData Data, List<IncludedMember> Members)
{
    public void Generate(StringBuilder output)
    {
        if (Data.NamespacePath != string.Empty)
            output.Append("namespace ").Append(Data.NamespacePath).Append(';');

        Data.ParentType?.GeneratePrefix(output);

        output.Append("partial ").Append(Data.Kind.ToDisplayString()).Append(' ').Append(Data.LongName).Append(':').Append(ConstantStrings.IBinaryStructurableFullName).Append('<').Append(Data.LongName).Append('>').Append(Data.Constraits)
        .Append('{');

        GenerateBinarySize(output);
        GenerateConstructor(output);

        GenerateWriteMethod(output);
        GenerateSpanWriteMethod(output);

        GenerateReadMethod(output);
        GenerateSpanReadMethod(output);

        output.Append('}');
        Data.ParentType?.GenerateSuffix(output);
    }

    private void GenerateBinarySize(StringBuilder output)
    {
        output.Append(ConstantStrings.BinarySizePropertyOverride);

        if (Members.Count == 0)
        {
            output.Append("0;");
            return;
        }

        int lastMember = Members.Count - 1;
        for (int i = 0; i < Members.Count; ++i)
        {
            IncludedMember member = Members[i];
            member.AppendSizeExpression(output);
            output.Append(i >= lastMember ? ';' : '+');
        }
    }

    private void GenerateConstructor(StringBuilder output)
    {
        output.Append("private ").Append(Data.ShortName).Append('(');

        int lastMember = Members.Count - 1;
        for (int i = 0; i < Members.Count; ++i)
        {
            IncludedMember member = Members[i];
            string typeName = member.TypeData.ToDisplayString();
            string memberName = member.MemberName;
            output.Append(typeName).Append(' ').Append(memberName)
            .Append(i >= lastMember ? ')' : ',');
        }

        output.Append('{');

        for (int i = 0; i < Members.Count; ++i)
        {
            IncludedMember member = Members[i];
            string memberName = member.MemberName;
            output.Append("this.").Append(memberName).Append('=').Append(memberName).Append(';');
        }

        output.Append('}');
    }

    private void GenerateWriteMethod(StringBuilder output)
    {
        output.Append(ConstantStrings.IBufferWriterTryWriteMethodSignature).Append('{')
        .Append("try{var span=writer.GetSpan(").Append(ConstantStrings.BinarySizePropertyName).Append(");")
        .Append("bool success = ").Append(ConstantStrings.SpanTryWriteMethodName).Append("(span);")
        .Append("if(!success){return false;}else{writer.Advance(").Append(ConstantStrings.BinarySizePropertyName).Append(");return true;} }catch(OutOfMemoryException){return false;}}");
    }

    private void GenerateSpanWriteMethod(StringBuilder output)
    {
        output.Append(ConstantStrings.SpanTryWriteMethodSignature)
        .Append("{if(").Append(ConstantStrings.SpanTryWriteBufferName).Append(".Length<").Append(ConstantStrings.BinarySizePropertyName).Append(')')
        .Append("return false;")
        .Append("ref byte ").Append(ConstantStrings.SpanTryWriteInitialBufferName).Append("=ref ").Append(ConstantStrings.MemoryMarshalGetReferenceFullName).Append('(').Append(ConstantStrings.SpanTryWriteBufferName).Append(");")
        .Append("ref byte ").Append(ConstantStrings.SpanTryWriteCurrentBufferName).Append("=ref ").Append(ConstantStrings.SpanTryWriteInitialBufferName).Append(';');

        for (int i = 0; i < Members.Count; ++i)
            Members[i].AppendWriteStatements(output, ConstantStrings.SpanTryWriteBufferName, ConstantStrings.SpanTryWriteInitialBufferName, ConstantStrings.SpanTryWriteCurrentBufferName);

        output.Append("return true;}");
    }

    private void GenerateReadMethod(StringBuilder output)
    {
        output.Append(ConstantStrings.SequenceTryReadPrefixMethodSignature).Append(Data.ShortName).Append(Data.Kind.IsValueType() ? ' ' : '?').Append(ConstantStrings.SequenceTryReadSuffixMethodSignature)
        .Append('{')
        .Append("if(").Append(ConstantStrings.SequenceTryReadBufferName).Append(".IsSingleSegment").Append("||").Append(ConstantStrings.SequenceTryReadBufferName).Append(".FirstSpan.Length>=").Append(ConstantStrings.BinarySizePropertyName).Append(')')
        .Append("return ").Append(ConstantStrings.SpanTryReadMethodName).Append('(').Append(ConstantStrings.SequenceTryReadBufferName).Append(".FirstSpan").Append(", out ").Append(ConstantStrings.SequenceTryReadValueName).Append(");")
        .Append("if(buffer.Length<").Append(ConstantStrings.BinarySizePropertyName).Append(')')
        .Append("{value = default;return false;}");

        output.Append("// Slow path...").Append('\n')
        .Append("var reader = new ").Append(ConstantStrings.SequenceReaderByteFullName).Append('(').Append(ConstantStrings.SequenceTryReadBufferName).Append(");");

        for (int i = 0; i < Members.Count; ++i)
            Members[i].AppendSequenceReaderReadStatements(output, ConstantStrings.SequenceTryReadBufferName, "reader");

        output.Append(ConstantStrings.SequenceTryReadValueName).Append("=new(");

        int lastMember = Members.Count - 1;
        for (int i = 0; i < Members.Count; ++i)
            output.Append(Members[i].MemberName).Append(i >= lastMember ? ')' : ',');

        output.Append(';')
        .Append("return true;")
        .Append('}');
    }

    private void GenerateSpanReadMethod(StringBuilder output)
    {
        output.Append(ConstantStrings.SpanTryReadPrefixMethodSignature).Append(Data.ShortName).Append(Data.Kind.IsValueType() ? ' ' : '?').Append(ConstantStrings.SpanTryReadSuffixMethodSignature)
        .Append('{')
        .Append("if(buffer.Length<").Append(ConstantStrings.BinarySizePropertyName).Append(')')
        .Append("{value = default;return false;}")
        .Append("ref byte ").Append(ConstantStrings.SpanTryReadInitialBufferName).Append("=ref ").Append(ConstantStrings.MemoryMarshalGetReferenceFullName).Append('(').Append(ConstantStrings.SpanTryWriteBufferName).Append(");")
        .Append("ref byte ").Append(ConstantStrings.SpanTryReadCurrentBufferName).Append("=ref ").Append(ConstantStrings.SpanTryReadInitialBufferName).Append(';');

        for (int i = 0; i < Members.Count; ++i)
            Members[i].AppendReferenceReadStatements(output, ConstantStrings.SpanTryReadBufferName, ConstantStrings.SpanTryReadInitialBufferName, ConstantStrings.SpanTryReadCurrentBufferName);

        output.Append(ConstantStrings.SpanTryReadValueName).Append("=new(");

        int lastMember = Members.Count - 1;
        for (int i = 0; i < Members.Count; ++i)
            output.Append(Members[i].MemberName).Append(i >= lastMember ? ')' : ',');

        output.Append(';')
        .Append("return true;")
        .Append('}');
    }

    public string ToFileString()
        => $"{(Data.NamespacePath != string.Empty ? $"{Data.NamespacePath}." : string.Empty)}{(Data.ParentType is not null ? $"{Data.ParentType.ToFileString()}." : string.Empty)}{Data.LongName}.g.cs".Replace('<', '{').Replace('>', '}');
}
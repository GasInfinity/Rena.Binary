using System.Text;
using Rena.Binary.Common;

namespace Rena.Binary.SourceGenerator;

public readonly record struct IncludedMember(MemberTypeData TypeData, string MemberName, IncludedData Data)
{
    public void AppendSequenceReaderReadStatements(StringBuilder output, string sequenceBufferName, string sequenceReaderName)
    {
        if (TypeData.IsPrimitive)
        {
            output.Append("if(!").Append(sequenceReaderName).Append('.').Append("TryRead").Append(TypeData.IsSingleByte ? string.Empty : $"{Data.Endianness}Endian").Append("(out ").Append(TypeData.ToDisplayString()).Append(' ').Append(MemberName).Append("))")
            .Append("{value=default;return false;}");
        }
        else
        {
            output.Append("if(!").Append(TypeData.ToDisplayString()).Append('.').Append(ConstantStrings.SequenceTryReadMethodName)
            .Append('(').Append(sequenceBufferName).Append(".Slice(").Append(sequenceReaderName).Append(".Position),out var ").Append(MemberName).Append("))")
            .Append("{value=default;return false;}");
            output.Append(sequenceReaderName).Append(".Advance(").Append(TypeData.ToDisplayString()).Append('.').Append(ConstantStrings.BinarySizePropertyName).Append(");");
        }
    }

    public void AppendReferenceReadStatements(StringBuilder output, string spanBufferName, string initialBufferName, string currentBufferName)
    {
        if (TypeData.IsPrimitive)
        {
            output.Append(currentBufferName).Append("=ref ")
            .Append(ConstantStrings.ByteRefReadingReadFullName).Append(TypeData.IsSingleByte ? string.Empty : Data.Endianness.ToString())
            .Append("(ref ").Append(currentBufferName).Append(",out ").Append(TypeData.ToNullableString()).Append(' ').Append(MemberName).Append(");");
        }
        else
        {
            output.Append("if(!").Append(TypeData.ToDisplayString()).Append('.').Append(ConstantStrings.SpanTryReadMethodName)
            .Append('(').Append(ConstantStrings.MemoryMarshalCreateSpanFullName)
            .Append("(ref ").Append(currentBufferName).Append(',').Append(spanBufferName).Append(".Length").Append('-').Append("(int)").Append(ConstantStrings.UnsafeByteOffsetFullName).Append("(ref ").Append(initialBufferName).Append(",ref ").Append(currentBufferName)
            .Append("))").Append(",out var ").Append(MemberName).Append(")){value = default;return false;}")
            .Append(currentBufferName).Append("=ref ").Append(ConstantStrings.UnsafeAddByteOffset).Append("(ref ").Append(currentBufferName).Append(',').Append(TypeData.ToDisplayString()).Append('.').Append(ConstantStrings.BinarySizePropertyName).Append(");");
        }
    }

    public void AppendWriteStatements(StringBuilder output, string spanBufferName, string initialBufferName, string currentBufferName)
    {
        if (TypeData.IsPrimitive)
        {
            output.Append(currentBufferName).Append("=ref ")
            .Append(ConstantStrings.ByteRefWritingWriteFullName).Append(TypeData.IsSingleByte ? string.Empty : Data.Endianness.ToString())
            .Append("(ref ").Append(currentBufferName).Append(',').Append(MemberName).Append(");");
        }
        else
        {
            output.Append("if(!").Append(MemberName).Append('.').Append(ConstantStrings.SpanTryWriteMethodName)
            .Append('(').Append(ConstantStrings.MemoryMarshalCreateSpanFullName)
            .Append("(ref ").Append(currentBufferName).Append(',').Append(spanBufferName).Append(".Length").Append('-').Append("(int)").Append(ConstantStrings.UnsafeByteOffsetFullName).Append("(ref ").Append(initialBufferName).Append(",ref ").Append(currentBufferName)
            .Append("))))return false;")
            .Append(currentBufferName).Append("=ref ").Append(ConstantStrings.UnsafeAddByteOffset).Append("(ref ").Append(currentBufferName).Append(',').Append(TypeData.ToDisplayString()).Append('.').Append(ConstantStrings.BinarySizePropertyName).Append(");");
        }
    }

    public void AppendSizeExpression(StringBuilder output)
    {
        if (TypeData.IsPrimitive)
            output.Append(ConstantStrings.UnsafeSizeOf).Append('<').Append(TypeData.ToDisplayString()).Append('>').Append("()");
        else
            output.Append(TypeData.ToDisplayString()).Append('.').Append(ConstantStrings.BinarySizePropertyName);
    }
}
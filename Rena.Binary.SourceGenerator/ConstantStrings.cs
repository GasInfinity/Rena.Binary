namespace Rena.Binary.SourceGenerator;

public static class ConstantStrings
{
    public const string GlobalNamespacePrefix = "global::";

    public const string SystemDiagnosticsCodeAnalysis = $"{GlobalNamespacePrefix}System.Diagnostics.CodeAnalysis";
    public const string NotNullWhenAttributeFullName = $"{SystemDiagnosticsCodeAnalysis}.NotNullWhen";

    public const string SystemBuffersFullName = $"{GlobalNamespacePrefix}System.Buffers";
    public const string IBufferWriterByteFullName = $"{SystemBuffersFullName}.IBufferWriter<byte>";
    public const string ReadOnlySequenceByteFullName = $"{SystemBuffersFullName}.ReadOnlySequence<byte>";
    public const string SequenceReaderByteFullName = $"{SystemBuffersFullName}.SequenceReader<byte>";

    public const string RenaBinaryNamespace = $"{GlobalNamespacePrefix}Rena.Binary";
    public const string ByteRefWritingFullName = $"{RenaBinaryNamespace}.ByteRefWriting";
    public const string ByteRefWritingWriteFullName = $"{ByteRefWritingFullName}.Write";

    public const string ByteRefReadingFullName = $"{RenaBinaryNamespace}.ByteRefReading";
    public const string ByteRefReadingReadFullName = $"{ByteRefReadingFullName}.Read";

    public const string IBinaryStructurableFullName = $"{RenaBinaryNamespace}.IBinaryStructurable";

    public const string IBufferWriterTryWriteMethodName = "TryWrite";
    public const string SpanTryWriteMethodName = "TryWrite";
    public const string SpanTryReadMethodName = "TryRead";
    public const string SequenceTryReadMethodName = "TryRead";
    public const string BinarySizePropertyName = "BinarySize";
    public const string BinarySizePropertyOverride = $"public static int {BinarySizePropertyName} =>";

    public const string SpanTryWriteBufferName = "buffer";
    public const string SpanTryWriteInitialBufferName = "initialBuffer";
    public const string SpanTryWriteCurrentBufferName = "currentBuffer";

    public const string SpanTryWriteMethodSignature = $"public bool {SpanTryWriteMethodName}(Span<byte> {SpanTryWriteBufferName})";
    public const string IBufferWriterTryWriteMethodSignature = $"public bool {IBufferWriterTryWriteMethodName}<TWriter>(TWriter writer) where TWriter : {IBufferWriterByteFullName}";

    public const string SpanTryReadBufferName = "buffer";
    public const string SpanTryReadValueName = "value";
    public const string SpanTryReadInitialBufferName = "initialBuffer";
    public const string SpanTryReadCurrentBufferName = "currentBuffer";

    public const string SpanTryReadPrefixMethodSignature = $"public static bool {SpanTryReadMethodName}(ReadOnlySpan<byte> {SpanTryReadBufferName}, [{NotNullWhenAttributeFullName}(true)] out ";
    public const string SpanTryReadSuffixMethodSignature = $"{SpanTryReadValueName})";

    public const string SequenceTryReadBufferName = "buffer";
    public const string SequenceTryReadValueName = "value";

    public const string SequenceTryReadPrefixMethodSignature = $"public static bool {SequenceTryReadMethodName}({ReadOnlySequenceByteFullName} {SequenceTryReadBufferName}, [{NotNullWhenAttributeFullName}(true)] out ";
    public const string SequenceTryReadSuffixMethodSignature = $"{SequenceTryReadValueName})";

    public const string SystemRuntimeInteropServicesFullName = $"{GlobalNamespacePrefix}System.Runtime.InteropServices";
    public const string MemoryMarshalFullName = $"{SystemRuntimeInteropServicesFullName}.MemoryMarshal";
    public const string MemoryMarshalCreateSpanFullName = $"{MemoryMarshalFullName}.CreateSpan";
    public const string MemoryMarshalGetReferenceFullName = $"{MemoryMarshalFullName}.GetReference";

    public const string SystemRuntimeCompilerServicesFullName = $"{GlobalNamespacePrefix}System.Runtime.CompilerServices";
    public const string UnsafeFullName = $"{SystemRuntimeCompilerServicesFullName}.Unsafe";
    public const string UnsafeByteOffsetFullName = $"{UnsafeFullName}.ByteOffset";
    public const string UnsafeAddByteOffset = $"{UnsafeFullName}.AddByteOffset";
    public const string UnsafeSizeOf = $"{UnsafeFullName}.SizeOf";
}
using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace Rena.Binary;

public interface IBinaryStructurable<T>
    where T : unmanaged, IBinaryStructurable<T>
{
    static abstract int BinarySize { get; }

    bool TryWrite<TWriter>(TWriter writer)
        where TWriter : IBufferWriter<byte>;

    bool TryWrite(Span<byte> buffer);

    static abstract bool TryRead(ReadOnlySpan<byte> buffer, [NotNullWhen(true)] out T value);
    static abstract bool TryRead(ReadOnlySequence<byte> buffer, [NotNullWhen(true)] out T value);
}
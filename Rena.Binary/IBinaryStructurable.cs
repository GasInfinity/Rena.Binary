using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace Rena.Binary;

/// <summary>
/// Interface for types that have a binary structure.
/// </summary>
/// <typeparam name="T">The type</typeparam>
public interface IBinaryStructurable<T>
    where T : unmanaged, IBinaryStructurable<T>
{
    /// <summary>
    /// Gets the size of the struct in bytes when written or read.
    /// </summary>
    static abstract int BinarySize { get; }

    /// <summary>
    /// Tries to write the struct to a <see cref="IBufferWriter{T}"/>.
    /// </summary>
    /// <typeparam name="TWriter">The type of the buffer writer.</typeparam>
    /// <param name="writer">The buffer writer to write the struct to.</param>
    /// <returns>true if the struct was written successfully; otherwise, false.</returns>
    bool TryWrite<TWriter>(TWriter writer)
        where TWriter : IBufferWriter<byte>;

    /// <summary>
    /// Tries to write the struct to a span of bytes.
    /// </summary>
    /// <param name="buffer">The span of bytes to write the struct to.</param>
    /// <returns>true if the struct was written successfully; otherwise, false.</returns>
    bool TryWrite(Span<byte> buffer);

    /// <summary>
    /// Tries to read a struct from a read-only span of bytes.
    /// </summary>
    /// <param name="buffer">The read-only span of bytes to read the struct from.</param>
    /// <param name="value">The read struct, if successful.</param>
    /// <returns>true if the struct was read successfully; otherwise, false.</returns>
    static abstract bool TryRead(ReadOnlySpan<byte> buffer, [NotNullWhen(true)] out T value);

    /// <summary>
    /// Tries to read a struct from a read-only sequence of bytes.
    /// </summary>
    /// <param name="buffer">The read-only sequence of bytes to read the struct from.</param>
    /// <param name="value">The read struct, if successful.</param>
    /// <returns>true if the struct was read successfully; otherwise, false.</returns>
    static abstract bool TryRead(ReadOnlySequence<byte> buffer, [NotNullWhen(true)] out T value);
}
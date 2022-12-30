using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rena.Binary;

public static class ByteRefReading
{
    /// <summary>
    /// Reads a byte from a buffer and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the byte from.</param>
    /// <param name="value">The read byte.</param>
    /// <returns>The buffer advanced by the size of a byte.</returns>
    public static ref byte Read(ref byte buffer, out byte value)
    {
        value = buffer;
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<byte>());
    }

    /// <summary>
    /// Reads a signed byte from a buffer and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the signed byte from.</param>
    /// <param name="value">The read signed byte.</param>
    /// <returns>The buffer advanced by the size of a signed byte.</returns>
    public static ref byte Read(ref byte buffer, out sbyte value)
    {
        value = (sbyte)buffer;
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<sbyte>());
    }

    /// <summary>
    /// Reads a boolean value from a buffer and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the boolean value from.</param>
    /// <param name="value">The read boolean value.</param>
    /// <returns>The buffer advanced by the size of a boolean value.</returns>
    public static ref byte Read(ref byte buffer, out bool value)
    {
        value = Unsafe.As<byte, bool>(ref buffer);
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<bool>());
    }

    /// <summary>
    /// Reads a unsigned short from a buffer in little-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the unsigned short from.</param>
    /// <param name="value">The read unsigned short.</param>
    /// <returns>The buffer advanced by the size of an unsigned short.</returns>
    public static ref byte ReadLittle(ref byte buffer, out ushort value)
    {
        value = BinaryPrimitives.ReadUInt16LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<ushort>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<ushort>());
    }

    /// <summary>
    /// Reads a short from a buffer in little-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the short from.</param>
    /// <param name="value">The read short.</param>
    /// <returns>The buffer advanced by the size of a short.</returns>
    public static ref byte ReadLittle(ref byte buffer, out short value)
    {
        value = BinaryPrimitives.ReadInt16LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<short>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<short>());
    }

    /// <summary>
    /// Reads a unsigned short from a buffer in big-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the unsigned short from.</param>
    /// <param name="value">The read unsigned short.</param>
    /// <returns>The buffer advanced by the size of an unsigned short.
    public static ref byte ReadBig(ref byte buffer, out ushort value)
    {
        value = BinaryPrimitives.ReadUInt16BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<ushort>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<ushort>());
    }

    /// <summary>
    /// Reads a short from a buffer in big-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the short from.</param>
    /// <param name="value">The read short.</param>
    /// <returns>The buffer advanced by the size of a short.</returns>
    public static ref byte ReadBig(ref byte buffer, out short value)
    {
        value = BinaryPrimitives.ReadInt16BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<short>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<short>());
    }

    /// <summary>
    /// Reads a unsigned integer from a buffer in little-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the unsigned integer from.</param>
    /// <param name="value">The read unsigned integer.</param>
    /// <returns>The buffer advanced by the size of an unsigned integer.</returns>
    public static ref byte ReadLittle(ref byte buffer, out uint value)
    {
        value = BinaryPrimitives.ReadUInt32LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<uint>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<uint>());
    }

    /// <summary>
    /// Reads an integer from a buffer in little-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the integer from.</param>
    /// <param name="value">The read integer.</param>
    /// <returns>The buffer advanced by the size of an integer.</returns>
    public static ref byte ReadLittle(ref byte buffer, out int value)
    {
        value = BinaryPrimitives.ReadInt32LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<int>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<int>());
    }

    /// <summary>
    /// Reads a unsigned integer from a buffer in big-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the unsigned integer from.</param>
    /// <param name="value">The read unsigned integer.</param>
    /// <returns>The buffer advanced by the size of an unsigned integer.
    public static ref byte ReadBig(ref byte buffer, out uint value)
    {
        value = BinaryPrimitives.ReadUInt32BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<uint>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<uint>());
    }

    /// <summary>
    /// Reads an integer from a buffer in big-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the integer from.</param>
    /// <param name="value">The read integer.</param>
    /// <returns>The buffer advanced by the size of an integer.</returns>
    public static ref byte ReadBig(ref byte buffer, out int value)
    {
        value = BinaryPrimitives.ReadInt32BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<int>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<int>());
    }

    /// <summary>
    /// Reads a unsigned long from a buffer in little-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the unsigned long from.</param>
    /// <param name="value">The read unsigned long.</param>
    /// <returns>The buffer advanced by the size of an unsigned long.</returns>
    public static ref byte ReadLittle(ref byte buffer, out ulong value)
    {
        value = BinaryPrimitives.ReadUInt64LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<ulong>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<ulong>());
    }

    /// <summary>
    /// Reads a long from a buffer in little-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the long from.</param>
    /// <param name="value">The read long.</param>
    /// <returns>The buffer advanced by the size of a long.</returns>
    public static ref byte ReadLittle(ref byte buffer, out long value)
    {
        value = BinaryPrimitives.ReadInt64LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<long>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<long>());
    }

    /// <summary>
    /// Reads a unsigned long from a buffer in big-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the unsigned long from.</param>
    /// <param name="value">The read unsigned long.</param>
    /// <returns>The buffer advanced by the size of an unsigned long.</returns>
    public static ref byte ReadBig(ref byte buffer, out ulong value)
    {
        value = BinaryPrimitives.ReadUInt64BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<ulong>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<ulong>());
    }

    /// <summary>
    /// Reads a long from a buffer in big-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the long from.</param>
    /// <param name="value">The read long.</param>
    /// <returns>The buffer advanced by the size of a long.</returns>
    public static ref byte ReadBig(ref byte buffer, out long value)
    {
        value = BinaryPrimitives.ReadInt64BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<long>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<long>());
    }

    /// <summary>
    /// Reads a float from a buffer in little-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the float from.</param>
    /// <param name="value">The read float.</param>
    /// <returns>The buffer advanced by the size of a float.</returns>
    public static ref byte ReadLittle(ref byte buffer, out float value)
    {
        value = BinaryPrimitives.ReadSingleLittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<float>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<float>());
    }

    /// <summary>
    /// Reads a float from a buffer in big-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the float from.</param>
    /// <param name="value">The read float.</param>
    /// <returns>The buffer advanced by the size of a float.</returns>
    public static ref byte ReadBig(ref byte buffer, out float value)
    {
        value = BinaryPrimitives.ReadSingleBigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<float>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<float>());
    }

    /// <summary>
    /// Reads a double from a buffer in little-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the double from.</param>
    /// <param name="value">The read double.</param>
    /// <returns>The buffer advanced by the size of a double.</returns>
    public static ref byte ReadLittle(ref byte buffer, out double value)
    {
        value = BinaryPrimitives.ReadDoubleLittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<double>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<double>());
    }

    /// <summary>
    /// Reads a double from a buffer in big-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the double from.</param>
    /// <param name="value">The read double.</param>
    /// <returns>The buffer advanced by the size of a double.</returns>
    public static ref byte ReadBig(ref byte buffer, out double value)
    {
        value = BinaryPrimitives.ReadDoubleBigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<double>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<double>());
    }

    /// <summary>
    /// Reads a char from a buffer in little-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the char from.</param>
    /// <param name="value">The read char.</param>
    /// <returns>The buffer advanced by the size of a char.</returns>
    public static ref byte ReadLittle(ref byte buffer, out char value)
    {
        value = (char)BinaryPrimitives.ReadUInt16LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<char>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<char>());
    }

    /// <summary>
    /// Reads a char from a buffer in big-endian order and advances the buffer position.
    /// </summary>
    /// <param name="buffer">The buffer to read the char from.</param>
    /// <param name="value">The read char.</param>
    /// <returns>The buffer advanced by the size of a char.</returns>
    public static ref byte ReadBig(ref byte buffer, out char value)
    {
        value = (char)BinaryPrimitives.ReadUInt16BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<char>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<char>());
    }
}
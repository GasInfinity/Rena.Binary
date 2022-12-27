using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rena.Binary;

public static class ByteRefReading
{
    public static ref byte Read(ref byte buffer, out byte value)
    {
        value = buffer;
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<byte>());
    }

    public static ref byte Read(ref byte buffer, out sbyte value)
    {
        value = (sbyte)buffer;
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<sbyte>());
    }

    public static ref byte Read(ref byte buffer, out bool value)
    {
        value = Unsafe.As<byte, bool>(ref buffer);
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<bool>());
    }

    public static ref byte ReadLittle(ref byte buffer, out ushort value)
    {
        value = BinaryPrimitives.ReadUInt16LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<ushort>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<ushort>());
    }

    public static ref byte ReadLittle(ref byte buffer, out short value)
    {
        value = BinaryPrimitives.ReadInt16LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<short>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<short>());
    }

    public static ref byte ReadBig(ref byte buffer, out ushort value)
    {
        value = BinaryPrimitives.ReadUInt16BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<ushort>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<ushort>());
    }

    public static ref byte ReadBig(ref byte buffer, out short value)
    {
        value = BinaryPrimitives.ReadInt16BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<short>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<short>());
    }

    public static ref byte ReadLittle(ref byte buffer, out uint value)
    {
        value = BinaryPrimitives.ReadUInt32LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<uint>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<uint>());
    }

    public static ref byte ReadLittle(ref byte buffer, out int value)
    {
        value = BinaryPrimitives.ReadInt32LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<int>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<int>());
    }

    public static ref byte ReadBig(ref byte buffer, out uint value)
    {
        value = BinaryPrimitives.ReadUInt32BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<uint>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<uint>());
    }

    public static ref byte ReadBig(ref byte buffer, out int value)
    {
        value = BinaryPrimitives.ReadInt32BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<int>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<int>());
    }

    public static ref byte ReadLittle(ref byte buffer, out ulong value)
    {
        value = BinaryPrimitives.ReadUInt64LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<ulong>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<ulong>());
    }

    public static ref byte ReadLittle(ref byte buffer, out long value)
    {
        value = BinaryPrimitives.ReadInt64LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<long>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<long>());
    }

    public static ref byte ReadBig(ref byte buffer, out ulong value)
    {
        value = BinaryPrimitives.ReadUInt64BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<ulong>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<ulong>());
    }

    public static ref byte ReadBig(ref byte buffer, out long value)
    {
        value = BinaryPrimitives.ReadInt64BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<long>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<long>());
    }

    public static ref byte ReadLittle(ref byte buffer, out float value)
    {
        value = BinaryPrimitives.ReadSingleLittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<float>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<float>());
    }

    public static ref byte ReadBig(ref byte buffer, out float value)
    {
        value = BinaryPrimitives.ReadSingleBigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<float>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<float>());
    }

    public static ref byte ReadLittle(ref byte buffer, out double value)
    {
        value = BinaryPrimitives.ReadDoubleLittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<double>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<double>());
    }

    public static ref byte ReadBig(ref byte buffer, out double value)
    {
        value = BinaryPrimitives.ReadDoubleBigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<double>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<double>());
    }

    public static ref byte ReadLittle(ref byte buffer, out char value)
    {
        value = (char)BinaryPrimitives.ReadUInt16LittleEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<char>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<char>());
    }

    public static ref byte ReadBig(ref byte buffer, out char value)
    {
        value = (char)BinaryPrimitives.ReadUInt16BigEndian(MemoryMarshal.CreateReadOnlySpan(ref buffer, Unsafe.SizeOf<char>()));
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<char>());
    }
}
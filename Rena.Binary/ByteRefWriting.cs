using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rena.Binary;

public static class ByteRefWriting
{
    public static ref byte Write(ref byte buffer, byte value)
    {
        buffer = value;
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<byte>());
    }

    public static ref byte Write(ref byte buffer, sbyte value)
        => ref Write(ref buffer, (byte)value);

    public static ref byte Write(ref byte buffer, bool value)
        => ref Write(ref buffer, Unsafe.As<bool, byte>(ref value));

    public static ref byte WriteLittle(ref byte buffer, short value)
    {
        BinaryPrimitives.WriteInt16LittleEndian(MemoryMarshal.CreateSpan(ref buffer, Unsafe.SizeOf<short>()), value);
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<short>());
    }

    public static ref byte WriteLittle(ref byte buffer, ushort value)
        => ref WriteLittle(ref buffer, (short)value);

    public static ref byte WriteBig(ref byte buffer, short value)
    {
        BinaryPrimitives.WriteInt16BigEndian(MemoryMarshal.CreateSpan(ref buffer, Unsafe.SizeOf<short>()), value);
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<short>());
    }

    public static ref byte WriteBig(ref byte buffer, ushort value)
        => ref WriteBig(ref buffer, (short)value);

    public static ref byte WriteLittle(ref byte buffer, int value)
    {
        BinaryPrimitives.WriteInt32LittleEndian(MemoryMarshal.CreateSpan(ref buffer, Unsafe.SizeOf<int>()), value);
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<int>());
    }

    public static ref byte WriteLittle(ref byte buffer, uint value)
        => ref WriteLittle(ref buffer, (int)value);

    public static ref byte WriteBig(ref byte buffer, int value)
    {
        BinaryPrimitives.WriteInt32BigEndian(MemoryMarshal.CreateSpan(ref buffer, Unsafe.SizeOf<int>()), value);
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<int>());
    }

    public static ref byte WriteBig(ref byte buffer, uint value)
        => ref WriteBig(ref buffer, (int)value);

    public static ref byte WriteLittle(ref byte buffer, long value)
    {
        BinaryPrimitives.WriteInt64LittleEndian(MemoryMarshal.CreateSpan(ref buffer, Unsafe.SizeOf<long>()), value);
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<long>());
    }

    public static ref byte WriteLittle(ref byte buffer, ulong value)
        => ref WriteLittle(ref buffer, (long)value);

    public static ref byte WriteBig(ref byte buffer, long value)
    {
        BinaryPrimitives.WriteInt64BigEndian(MemoryMarshal.CreateSpan(ref buffer, Unsafe.SizeOf<long>()), value);
        return ref Unsafe.AddByteOffset(ref buffer, Unsafe.SizeOf<long>());
    }

    public static ref byte WriteBig(ref byte buffer, ulong value)
        => ref WriteBig(ref buffer, (long)value);

    public static ref byte WriteLittle(ref byte buffer, char value)
        => ref WriteLittle(ref buffer, (short)value);

    public static ref byte WriteBig(ref byte buffer, char value)
        => ref WriteBig(ref buffer, (short)value);
}
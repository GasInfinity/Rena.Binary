using System.Buffers;
using System.Runtime.CompilerServices;

namespace Rena.Binary;

public static class SequenceReaderExtensions
{
    public static bool TryRead(this ref SequenceReader<byte> reader, out sbyte value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryRead(out Unsafe.As<sbyte, byte>(ref value));
    }

    public static bool TryRead(this ref SequenceReader<byte> reader, out bool value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryRead(out Unsafe.As<bool, byte>(ref value));
    }

    public static bool TryReadLittleEndian(this ref SequenceReader<byte> reader, out ushort value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryReadLittleEndian(out Unsafe.As<ushort, short>(ref value));
    }

    public static bool TryReadBigEndian(this ref SequenceReader<byte> reader, out ushort value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryReadBigEndian(out Unsafe.As<ushort, short>(ref value));
    }

    public static bool TryReadLittleEndian(this ref SequenceReader<byte> reader, out uint value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryReadLittleEndian(out Unsafe.As<uint, int>(ref value));
    }

    public static bool TryReadBigEndian(this ref SequenceReader<byte> reader, out uint value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryReadBigEndian(out Unsafe.As<uint, int>(ref value));
    }

    public static bool TryReadLittleEndian(this ref SequenceReader<byte> reader, out ulong value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryReadLittleEndian(out Unsafe.As<ulong, long>(ref value));
    }

    public static bool TryReadBigEndian(this ref SequenceReader<byte> reader, out ulong value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryReadBigEndian(out Unsafe.As<ulong, long>(ref value));
    }

    public static bool TryReadLittleEndian(this ref SequenceReader<byte> reader, out float value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryReadLittleEndian(out Unsafe.As<float, int>(ref value));
    }

    public static bool TryReadBigEndian(this ref SequenceReader<byte> reader, out float value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryReadBigEndian(out Unsafe.As<float, int>(ref value));
    }

    public static bool TryReadLittleEndian(this ref SequenceReader<byte> reader, out double value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryReadLittleEndian(out Unsafe.As<double, long>(ref value));
    }

    public static bool TryReadBigEndian(this ref SequenceReader<byte> reader, out double value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryReadBigEndian(out Unsafe.As<double, long>(ref value));
    }

    public static bool TryReadLittleEndian(this ref SequenceReader<byte> reader, out char value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryReadLittleEndian(out Unsafe.As<char, short>(ref value));
    }

    public static bool TryReadBigEndian(this ref SequenceReader<byte> reader, out char value)
    {
        Unsafe.SkipInit(out value);
        return reader.TryReadBigEndian(out Unsafe.As<char, short>(ref value));
    }
}
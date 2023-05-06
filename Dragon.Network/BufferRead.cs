namespace Dragon.Network;

public sealed class BufferReader {
    private int index;
    private readonly byte[] _buffer;

    private const int Int16 = 2;
    private const int Int32 = 4;

    public BufferReader(byte[] arr, bool useAsReference) {
        _buffer = useAsReference ? arr : new byte[arr.Length];

        if (!useAsReference) {
            Buffer.BlockCopy(arr, 0, _buffer, 0, arr.Length);
        }
    }

    public void PointToStart() {
        index = 0;
    }

    public byte ReadByte() {
        var value = _buffer[index];

        ++index;

        return value;
    }

    public bool ReadBoolean() {
        return ReadByte() == 1;
    }

    public short ReadInt16() {
        var value = BitConverter.ToInt16(_buffer, index);

        index += Int16;

        return value;
    }

    public int ReadInt32() {
        var value = BitConverter.ToInt32(_buffer, index);

        index += Int32;

        return value;
    }

    public unsafe void MemoryCopy(void* destination, int destinationSizeInBytes, int sourceBytesToCopy) {
        fixed (byte* p = &_buffer[index]) {
            Buffer.MemoryCopy(p, destination, destinationSizeInBytes, sourceBytesToCopy);
        }

        index += sourceBytesToCopy;
    }

    public string ReadString() {
        var length = ReadInt32();

        if (length > 0) {

            var letters = new char[length];

            for (var i = 0; i < length; i++) {
                letters[i] = (char)_buffer[index++];
            }

            return new string(letters);
        }

        return string.Empty;
    }
}
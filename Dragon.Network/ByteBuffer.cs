using System.Text;

namespace Dragon.Network;
public sealed class ByteBuffer {
    int readPos = 0;

    readonly MemoryStream buffer;

    private const int ByteLength = 1;
    private const int Int16Length = 2;
    private const int Int32Length = 4;

    public ByteBuffer() {
        buffer = new MemoryStream(byte.MaxValue);
    }

    public ByteBuffer(int capacity) {
        buffer = new MemoryStream(capacity);
    }

    public ByteBuffer(byte[] arr) {
        buffer = new MemoryStream(arr.Length);
        buffer.Write(arr, 0, arr.Length);
    }

    public byte[] ToArray() {
        return buffer.ToArray();
    }

    public int Length() {
        return (int)(buffer.Length - readPos);
    }

    public void Flush() {
        buffer.Flush();
        buffer.SetLength(0);
        buffer.Position = 0;
        readPos = 0;
    }

    public void Dispose() {
        Flush();
        buffer.Dispose();
    }

    public void Trim() {
        if (readPos >= buffer.Length) {
            Flush();
        }
    }

    public void Write(byte[] values, int size) {
        if (buffer.Length + size > buffer.Capacity) {
            buffer.Capacity = (int)(buffer.Length + size);
        }

        buffer.Write(values, 0, size);
    }

    public void Write(byte[] values) {
        Write(values, values.Length);
    }

    public void Write(bool value) {
        Write(new byte[] { Convert.ToByte(value) }, ByteLength);
    }

    public void Write(byte value) {
        Write(new byte[] { value }, ByteLength);
    }

    public void Write(short value) {
        Write(BitConverter.GetBytes(value), Int16Length);
    }

    public void Write(int value) {
        Write(BitConverter.GetBytes(value), Int32Length);
    }

    public void Write(string value) {
        var values = new byte[value.Length];

        for (var i = 0; i < value.Length; i++) {
            values[i] = (byte)value[i];
        }

        Write(value.Length);
        Write(values);
    }

    public bool ReadBoolean(bool peek = true) {
        return ReadByte(peek) == 1;
    }

    public byte[] ReadBytes(int length, bool peek = true) {
        var values = new byte[length];

        buffer.Position = readPos;
        buffer.Read(values, 0, length);

        if (peek) {
            readPos += length;
        }

        return values;
    }

    public void ReadBytes(byte[] content, int length) {
        buffer.Position = readPos;
        buffer.Read(content, 0, length);

        readPos += length;
    }

    public byte ReadByte(bool peek = true) {
        buffer.Position = readPos;

        if (peek) {
            readPos += ByteLength;
        }

        return (byte)buffer.ReadByte();
    }

    public short ReadInt16(bool peek = true) {
        var values = new byte[Int16Length];

        buffer.Position = readPos;
        buffer.Read(values, 0, Int16Length);

        if (peek) {
            readPos += Int16Length;
        }

        return BitConverter.ToInt16(values, 0);
    }

    public int ReadInt32(bool peek = true) {
        var values = new byte[Int32Length];

        buffer.Position = readPos;
        buffer.Read(values, 0, Int32Length);

        if (peek) {
            readPos += Int32Length;
        }

        return BitConverter.ToInt32(values, 0);
    }

    public string ReadString(bool peek = true) {
        try {
            var length = ReadInt32(peek);
            var values = new byte[length];
  
            buffer.Position = readPos;
            buffer.Read(values, 0, length);

            if (peek) {
                readPos += length;
            }

            return Encoding.ASCII.GetString(values);
        }
        catch {
            return string.Empty;
        }
    }

    public void WriteEmptyBytes(int length) {
        if (buffer.Length + length > buffer.Capacity) {
            buffer.Capacity = (int)(buffer.Length + length);
        }

        for (var i = 0; i < length; ++i) {
            buffer.WriteByte(0);
        }
    }
}
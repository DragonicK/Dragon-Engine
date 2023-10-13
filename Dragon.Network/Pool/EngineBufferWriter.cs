using System.Text;

namespace Dragon.Network.Pool;

public sealed class EngineBufferWriter : IEngineBufferWriter {
    public int Length => position;
    public byte[] Content { get; private set; }

    public const int Size = 1024;

    int position = 0;

    public EngineBufferWriter() { 
        Content = new byte[Size];
    }

    public void Reset() {
        Array.Clear(Content);

        position = 0;
    }

    public void Write(byte value) {
        Content[position++] = value;
    }

    public void Write(bool value) {
        Content[position++] = (byte)(value ? 1 : 0);
    }

    public void Write(short value) {
        var values = BitConverter.GetBytes(value);

        Buffer.BlockCopy(values, 0, Content, position, values.Length);

        position += values.Length;
    }

    public void Write(int value) {
        var values = BitConverter.GetBytes(value);

        Buffer.BlockCopy(values, 0, Content, position, values.Length);

        position += values.Length;
    }

    public void Write(long value) {
        var values = BitConverter.GetBytes(value);

        Buffer.BlockCopy(values, 0, Content, position, values.Length);

        position += values.Length;
    }

    public void Write(float value) {
        var values = BitConverter.GetBytes(value);

        Buffer.BlockCopy(values, 0, Content, position, values.Length);

        position += values.Length;
    }

    public void Write(string value) {
        var values = Encoding.UTF8.GetBytes(value);

        Buffer.BlockCopy(values, 0, Content, position, values.Length);

        position += values.Length;
    }

    public void WriteEmptyBytes(int count) {
        position += count;
    }

    public void EnsureCapacity(int capacity) {

    }
}
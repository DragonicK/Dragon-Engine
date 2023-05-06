namespace Dragon.Network.Pool;

public sealed class EngineBuffer : IEngineBuffer { 
    public int Length { get; set; }
    public byte[] Content { get; private set; }
    public BufferReader Reader { get; private set; }

    public const int Size = 1024;

    public EngineBuffer() {
        Content = new byte[Size];
        Reader = new BufferReader(Content, true);
    }

    public void EnsureCapacity(int capacity) {
        Content = new byte[capacity];
        Reader = new BufferReader(Content, true);
    }

    public void Reset() {
        Length = 0;

        Array.Clear(Content);

        Reader.PointToStart();
    }
}
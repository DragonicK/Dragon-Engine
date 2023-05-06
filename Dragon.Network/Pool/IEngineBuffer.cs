namespace Dragon.Network.Pool;

public interface IEngineBuffer {
    byte[] Content { get; }
    int Length { get; set; }
    BufferReader Reader { get; }
    void EnsureCapacity(int capacity);
    void Reset();
}
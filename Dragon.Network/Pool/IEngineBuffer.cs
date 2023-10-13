namespace Dragon.Network.Pool;

public interface IEngineBufferReader {
    byte[] Content { get; }
    int Length { get; set; }
    void Reset();
    byte ReadByte();
    int ReadInt32();
    long ReadInt64();
    short ReadInt16();
    float ReadFloat();
    bool ReadBoolean();
    string ReadString();
    void ResetPosition();
    unsafe void MemoryCopy(void* destination, int destinationSizeInBytes, int sourceBytesToCopy);
}
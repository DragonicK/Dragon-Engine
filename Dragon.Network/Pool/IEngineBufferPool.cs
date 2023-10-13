namespace Dragon.Network.Pool;

public interface IEngineBufferPool {
    IEngineBufferReader GetNextBufferReader();
    IEngineBufferWriter GetNextBufferWriter();
}
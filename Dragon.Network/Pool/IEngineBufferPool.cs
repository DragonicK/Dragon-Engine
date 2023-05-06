namespace Dragon.Network.Pool;

public interface IEngineBufferPool {
    IEngineBuffer GetNextBuffer();
}
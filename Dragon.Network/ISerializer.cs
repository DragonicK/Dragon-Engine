using Dragon.Network.Pool;

namespace Dragon.Network;

public interface ISerializer {
    public IEngineBufferWriter Serialize<T>(T type, IEngineBufferWriter buffer);
    object Deserialize(IEngineBufferReader buffer, Type type);
}
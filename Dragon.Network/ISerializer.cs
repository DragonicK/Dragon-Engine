using Dragon.Network.Pool;

namespace Dragon.Network;

public interface ISerializer {
    public byte[] Serialize<T>(T type);
    object Deserialize(IEngineBuffer buffer, Type type);
}
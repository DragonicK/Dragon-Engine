namespace Dragon.Network;

public interface ISerializer {
    public byte[] Serialize<T>(T type);
    object Deserialize(byte[] buffer, Type type);
}
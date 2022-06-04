namespace Dragon.Network;

public interface IPacketRouter {
    void Add(Type key, Type value);
    void Process(IConnection connection, dynamic packet);
}
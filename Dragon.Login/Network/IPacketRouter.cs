using Dragon.Network;

namespace Dragon.Login.Network;

public interface IPacketRouter {
    void Add(Type key, IRoute value);
    void Process(IConnection connection, object packet);
}
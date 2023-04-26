using Dragon.Network;
using Dragon.Network.Messaging;

namespace Dragon.Login.Network;

public interface IRoute {
    MessageHeader Header { get; }
    void Process(IConnection connection, object packet);
}
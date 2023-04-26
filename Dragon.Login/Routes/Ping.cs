using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Login.Network;

namespace Dragon.Login.Routes;

public sealed class Ping : Route, IRoute {
    public MessageHeader Header => MessageHeader.Ping;

    public void Process(IConnection connection, object packet) { }
}
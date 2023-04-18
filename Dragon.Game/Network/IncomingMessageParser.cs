using Dragon.Network;
using Dragon.Network.Incoming;

namespace Dragon.Game.Network;

public class IncomingMessageParser : IIncomingMessageParser {
    public IConnectionRepository? ConnectionRepository { get; init; }
    public IPacketRouter? PacketRouter { get; init; }

    public void Process(IConnection connection, dynamic packet) {
        if (connection is not null) {
            PacketRouter?.Process(connection, packet);
        }
    }
}
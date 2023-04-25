using Dragon.Network;
using Dragon.Network.Incoming;

namespace Dragon.Chat.Network;

public sealed class IncomingMessageParser : IIncomingMessageParser {
    public IConnectionRepository? ConnectionRepository { get; init; }
    public IPacketRouter? PacketRouter { get; init; }

    public void Process(IConnection connection, object packet) {
        if (connection is not null) {
            PacketRouter?.Process(connection, packet);
        }
    }
}
using Crystalshire.Network;
using Crystalshire.Network.Incoming;

namespace Crystalshire.Login.Network {
    public class IncomingMessageParser : IIncomingMessageParser {
        public IConnectionRepository? ConnectionRepository { get; init; }
        public IPacketRouter? PacketRouter { get; init; }

        public void Process(int id, dynamic packet) {
            var connection = ConnectionRepository?.GetFromId(id);

            if (connection is not null) {
                PacketRouter?.Process(connection, packet);
            }
        }
    }
}
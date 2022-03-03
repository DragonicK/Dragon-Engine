using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpServerRates : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.ServerRates;
        public DataRate Rates { get; set; }
    }
}
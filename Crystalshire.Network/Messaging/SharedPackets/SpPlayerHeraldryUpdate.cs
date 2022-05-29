using Crystalshire.Network.Messaging.DTO;

namespace Crystalshire.Network.Messaging.SharedPackets {
    public sealed class SpPlayerHeraldryUpdate : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.HeraldryUpdate;
        public DataHeraldry Heraldry { get; set; }
    }
}
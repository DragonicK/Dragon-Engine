using Crystalshire.Core.Model;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class PacketPlayerDirection : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.PlayerDirection;
        public int Index { get; set; }
        public Direction Direction { get; set; }
    }
}
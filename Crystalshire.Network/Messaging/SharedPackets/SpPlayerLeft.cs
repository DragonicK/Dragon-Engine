namespace Crystalshire.Network.Messaging.SharedPackets {
    public class SpPlayerLeft : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.PlayerLeft;
        public int Index { get; set; }
    }
}
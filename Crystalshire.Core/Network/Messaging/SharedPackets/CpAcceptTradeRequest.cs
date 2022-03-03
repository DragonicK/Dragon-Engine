namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class CpAcceptTradeRequest : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.AcceptTradeRequest;
    }
}
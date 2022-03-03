namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class CpAcceptTrade : IMessagePacket {      
        public MessageHeader Header { get; set; } = MessageHeader.AcceptTrade;
    }
}
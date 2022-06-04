namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpAcceptTrade : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.AcceptTrade;
}
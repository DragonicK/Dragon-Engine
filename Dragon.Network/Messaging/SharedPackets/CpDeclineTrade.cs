namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpDeclineTrade : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.DeclineTrade;
}
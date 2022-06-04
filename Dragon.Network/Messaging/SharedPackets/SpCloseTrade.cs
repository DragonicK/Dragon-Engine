namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpCloseTrade : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.CloseTrade;
}
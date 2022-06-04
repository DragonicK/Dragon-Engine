namespace Dragon.Network.Messaging.SharedPackets;

public class SpAttributePoint : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.AttributePoint;
    public int Points { get; set; }
}
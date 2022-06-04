namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpReceiveMailItem : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ReceiveMailItem;
    public int Id { get; set; }
}
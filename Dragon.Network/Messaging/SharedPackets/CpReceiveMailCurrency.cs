namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpReceiveMailCurrency : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ReceiveMailCurrency;
    public int Id { get; set; }
}
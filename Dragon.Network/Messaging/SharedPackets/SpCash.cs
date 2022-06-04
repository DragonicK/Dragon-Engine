namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpCash : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Cash;
    public int Cash { get; set; }
}
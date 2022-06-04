namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpHighIndex : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.HighIndex;
    public int Index { get; set; }
}
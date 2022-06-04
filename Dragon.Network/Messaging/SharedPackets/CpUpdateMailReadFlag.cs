namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpUpdateMailReadFlag : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.UpdateMailReadFlag;
    public int Id { get; set; }
}
namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpStartCraft : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.StartCraft;
    public int Index { get; set; }
}
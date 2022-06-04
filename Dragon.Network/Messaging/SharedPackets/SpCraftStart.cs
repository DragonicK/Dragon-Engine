namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpCraftStart : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.StartCraftProgress;
    public int Step { get; set; }
}
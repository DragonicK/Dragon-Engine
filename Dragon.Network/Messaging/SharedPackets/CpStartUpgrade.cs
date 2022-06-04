namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpStartUpgrade : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.StartUpgrade;
    public int InventoryIndex { get; set; }
}
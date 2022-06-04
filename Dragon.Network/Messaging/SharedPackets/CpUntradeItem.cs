namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpUntradeItem : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.UntradeItem;
    public int InventoryIndex { get; set; }
}
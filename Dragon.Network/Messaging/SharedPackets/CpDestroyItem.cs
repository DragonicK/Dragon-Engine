namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpDestroyItem : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.DestroyItem;
    public int InventoryIndex { get; set; }
}
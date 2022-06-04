namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpUseItem : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.UseItem;
    public int InventoryIndex { get; set; }
}
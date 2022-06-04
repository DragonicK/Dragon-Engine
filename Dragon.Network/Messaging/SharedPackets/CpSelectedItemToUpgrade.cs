namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpSelectedItemToUpgrade : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.SelectedItemToUpgrade;
    public int InventoryIndex { get; set; }
}
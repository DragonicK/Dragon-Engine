namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpDepositItem : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.DepositItem;
    public int InventoryIndex { get; set; }
    public int Amount { get; set; }
}
namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpTradeItem : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.TradeItem;
    public int InventoryIndex { get; set; }
    public int Amount { get; set; }
}
namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpTradeMyInventory : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.TradeMyInventory;
    public int Index { get; set; }
    public int Inventory { get; set; }
    public int Amount { get; set; }
}
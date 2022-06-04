namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpShopSellItem : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ShopSellItem;
    public int Index { get; set; }
    public int Amount { get; set; }
}
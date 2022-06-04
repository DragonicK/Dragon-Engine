namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpShopBuyItem : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ShopBuyItem;
    public int Index { get; set; }
    public int Amount { get; set; }
}
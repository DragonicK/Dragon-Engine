namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpPurchaseBlackMarkteItem : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PurchaseBlackMarketItem;
    public int Id { get; set; }
    public int Amount { get; set; }
    public string Receiver { get; set; } = string.Empty;
}
namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpTradeInvite : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.TradeInvite;
    public string Name { get; set; } = string.Empty;
}
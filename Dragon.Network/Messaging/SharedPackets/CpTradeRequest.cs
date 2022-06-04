namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpTradeRequest : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.TradeRequest;
    public int Index { get; set; }
}
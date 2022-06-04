namespace Dragon.Network.Messaging.SharedPackets;
public sealed class CpDeclineTradeRequest : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.DeclineTradeRequest;
}
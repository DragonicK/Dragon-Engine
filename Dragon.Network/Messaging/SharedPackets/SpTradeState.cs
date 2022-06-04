using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpTradeState : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.TradeState;
    public TradeState State { get; set; }
    public TradeState MyState { get; set; }
    public TradeState OtherState { get; set; }
}
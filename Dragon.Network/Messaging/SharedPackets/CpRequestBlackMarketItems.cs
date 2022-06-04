using Dragon.Core.Model.BlackMarket;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpRequestBlackMarketItems : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.RequestBlackMarketItems;
    public BlackMarketItemCategory Category { get; set; }
    public int Page { get; set; }
}
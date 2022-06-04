using Dragon.Core.Model.BlackMarket;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpBlackMarketItems : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.BlackMarketItems;
    public int MaximumCategoryPages { get; set; }
    public BlackMarketItem[] Items { get; set; } = Array.Empty<BlackMarketItem>();
}

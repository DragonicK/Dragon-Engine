using Crystalshire.Core.Model.BlackMarket;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class CpRequestBlackMarketItems : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.RequestBlackMarketItems;
        public BlackMarketItemCategory Category { get; set; }
        public int Page { get; set; }
    }
}
using Crystalshire.Core.Model.BlackMarket;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpBlackMarketItems : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.BlackMarketItems;        
        public int MaximumCategoryPages{ get; set; }
        public BlackMarketItem[] Items { get; set; } = Array.Empty<BlackMarketItem>();  
    }
}
using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpTradeOtherInventory {
        public MessageHeader Header { get; set; } = MessageHeader.TradeOtherInventory;
        public DataInventory Inventory { get; set; }
    }
}
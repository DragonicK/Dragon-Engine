using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpPlayerInventoryUpdate : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.InventoryUpdate;
        public DataInventory Inventory { get; set; }
    }
}
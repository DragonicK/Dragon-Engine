using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpPlayerWarehouseUpdate : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.WarehouseUpdate;
        public DataInventory Inventory { get; set; }
    }
}
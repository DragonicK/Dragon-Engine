using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpPlayerWarehouse : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.Warehouse;
        public int MaximumWarehouse { get; set; }
        public DataInventory[] Inventories { get; set; } = Array.Empty<DataInventory>();
    }
}
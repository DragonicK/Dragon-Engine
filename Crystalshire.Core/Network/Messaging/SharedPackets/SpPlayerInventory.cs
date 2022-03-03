using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpPlayerInventory : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.Inventory;
        public int MaximumInventories { get; set; }
        public DataInventory[] Inventories { get; set; } = Array.Empty<DataInventory>();
    }
}
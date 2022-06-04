using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPlayerInventoryUpdate : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.InventoryUpdate;
    public DataInventory Inventory { get; set; }
}
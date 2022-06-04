using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPlayerWarehouseUpdate : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.WarehouseUpdate;
    public DataInventory Inventory { get; set; }
}
using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpTradeOtherInventory {
    public MessageHeader Header { get; set; } = MessageHeader.TradeOtherInventory;
    public DataInventory Inventory { get; set; }
}
using Crystalshire.Network.Messaging.DTO;

namespace Crystalshire.Network.Messaging.SharedPackets;

public sealed class SpQuickSlotUpdate : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.QuickSlotUpdate;
    public DataQuickSlot QuickSlot { get; set; }
}
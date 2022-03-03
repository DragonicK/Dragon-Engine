using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpQuickSlotUpdate : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.QuickSlotUpdate;
        public DataQuickSlot QuickSlot { get; set; }
    }
}
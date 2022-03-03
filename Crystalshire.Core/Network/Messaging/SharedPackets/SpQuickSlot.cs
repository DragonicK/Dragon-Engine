using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpQuickSlot : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.QuickSlot;
        public DataQuickSlot[] QuickSlot { get; set; } = Array.Empty<DataQuickSlot>();
    }
}
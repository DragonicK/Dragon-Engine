using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpQuickSlotChange : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.QuickSlotChange;
    public QuickSlotType QuickSlotType { get; set; }
    public int FromIndex { get; set; }
    public int QuickSlotIndex { get; set; }
}
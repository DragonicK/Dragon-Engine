namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpQuickSlotUse : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.QuickSlotUse;
    public int Index { get; set; }
}
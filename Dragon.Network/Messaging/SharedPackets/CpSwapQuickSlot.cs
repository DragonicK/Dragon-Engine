namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpSwapQuickSlot : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.SwapQuickSlot;
    public int OldIndex { get; set; }
    public int NewIndex { get; set; }
}
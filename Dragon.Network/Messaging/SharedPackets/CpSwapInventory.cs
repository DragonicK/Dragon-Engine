namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpSwapInventory : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.SwapInventory;
    public int OldIndex { get; set; }
    public int NewIndex { get; set; }
}
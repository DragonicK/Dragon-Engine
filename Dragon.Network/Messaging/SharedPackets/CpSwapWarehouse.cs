namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpSwapWarehouse : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.SwapWarehouse;
    public int OldIndex { get; set; }
    public int NewIndex { get; set; }
}
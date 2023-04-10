namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpTakeItemFromChest : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.TakeItemFromChest;
    public int Index { get; set; }
}
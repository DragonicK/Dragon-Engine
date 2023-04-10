namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpSortChestItemList : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.SortChestItemList;
    public int Index { get; set; }
}
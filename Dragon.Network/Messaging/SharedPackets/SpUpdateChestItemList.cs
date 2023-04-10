using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpUpdateChestItemList : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.UpdateChestItemList;
    public DataChestItem Item { get; set; }
}
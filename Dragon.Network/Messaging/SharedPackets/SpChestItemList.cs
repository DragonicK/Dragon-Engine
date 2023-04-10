using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets {
    public sealed class SpChestItemList {
        public MessageHeader Header { get; set; } = MessageHeader.ChestItemList;
        public DataChestItem[] Items { get; set; } = Array.Empty<DataChestItem>();
    }
}
using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpShopOpen : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ShopOpen;
    public string Name { get; set; } = string.Empty;
    public DataShopItem[] Items { get; set; } = Array.Empty<DataShopItem>();
}
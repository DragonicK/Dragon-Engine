using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpChests : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Chests;
    public DataChest[] Chests { get; set; } = Array.Empty<DataChest>();
}
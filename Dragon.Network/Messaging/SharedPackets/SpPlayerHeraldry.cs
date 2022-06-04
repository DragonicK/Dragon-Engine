using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPlayerHeraldry : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Heraldry;
    public DataHeraldry[] Heraldries { get; set; } = Array.Empty<DataHeraldry>();
}
using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpInstanceEntity : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.InstanceEntity;
    public DataEntity Entity { get; set; }
}
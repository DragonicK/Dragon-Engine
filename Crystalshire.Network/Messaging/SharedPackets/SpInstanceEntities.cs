using Crystalshire.Network.Messaging.DTO;

namespace Crystalshire.Network.Messaging.SharedPackets;

public sealed class SpInstanceEntities : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.InstanceEntities;
    public DataEntity[] Entities { get; set; } = Array.Empty<DataEntity>();
}
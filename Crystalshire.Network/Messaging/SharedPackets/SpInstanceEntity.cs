using Crystalshire.Network.Messaging.DTO;

namespace Crystalshire.Network.Messaging.SharedPackets {
    public sealed class SpInstanceEntity : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.InstanceEntity;
        public DataEntity Entity { get; set; }
    }
}
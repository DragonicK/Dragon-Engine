using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpSkill : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.Skill;
        public DataSkill[] Skills { get; set; } = Array.Empty<DataSkill>();
    }
}
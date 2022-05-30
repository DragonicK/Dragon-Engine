using Crystalshire.Network.Messaging.DTO;

namespace Crystalshire.Network.Messaging.SharedPackets;

public sealed class SpSkillUpdate : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.SkillUpdate;
    public DataSkill Skill { get; set; }
}
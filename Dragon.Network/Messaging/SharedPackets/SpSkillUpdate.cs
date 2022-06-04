using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpSkillUpdate : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.SkillUpdate;
    public DataSkill Skill { get; set; }
}
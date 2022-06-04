using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpSkill : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Skill;
    public DataSkill[] Skills { get; set; } = Array.Empty<DataSkill>();
}
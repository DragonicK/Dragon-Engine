using Crystalshire.Network.Messaging.DTO;

namespace Crystalshire.Network.Messaging.SharedPackets;

public sealed class SpSkill : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Skill;
    public DataSkill[] Skills { get; set; } = Array.Empty<DataSkill>();
}
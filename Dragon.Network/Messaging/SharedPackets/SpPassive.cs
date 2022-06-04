using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;
public sealed class SpPassive : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Passive;
    public DataSkill[] Passives { get; set; } = Array.Empty<DataSkill>();
}
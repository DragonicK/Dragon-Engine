using Crystalshire.Network.Messaging.DTO;

namespace Crystalshire.Network.Messaging.SharedPackets;
public sealed class SpPassive : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Passive;
    public DataSkill[] Passives { get; set; } = Array.Empty<DataSkill>();
}
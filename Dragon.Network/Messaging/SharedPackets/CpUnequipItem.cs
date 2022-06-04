using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpUnequipItem : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.UnequipItem;
    public PlayerEquipmentType EquipmentType { get; set; }
}
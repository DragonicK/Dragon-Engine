using Crystalshire.Network.Messaging.DTO;

namespace Crystalshire.Network.Messaging.SharedPackets;

public sealed class SpPlayerEquipment : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Equipment;
    public DataEquipment[] Equipments { get; set; } = Array.Empty<DataEquipment>();
}
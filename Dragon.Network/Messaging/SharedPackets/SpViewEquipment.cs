using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpViewEquipment : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ViewEquipment;
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public int ClassCode { get; set; }
    public DataEquipment[] Equipments { get; set; } = Array.Empty<DataEquipment>();
}
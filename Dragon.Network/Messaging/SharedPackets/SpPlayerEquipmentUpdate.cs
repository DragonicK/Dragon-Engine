using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPlayerEquipmentUpdate : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.EquipmentUpdate;
    public DataEquipment Equipment { get; set; }
}
using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpPlayerEquipment : IMessagePacket { 
        public MessageHeader Header { get; set; } = MessageHeader.Equipment;
        public DataEquipment[] Equipments { get; set; } = Array.Empty<DataEquipment>();
    }
}
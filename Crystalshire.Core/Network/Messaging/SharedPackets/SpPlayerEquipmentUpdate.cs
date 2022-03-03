using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpPlayerEquipmentUpdate : IMessagePacket {  
        public MessageHeader Header { get; set; } = MessageHeader.EquipmentUpdate;
        public DataEquipment Equipment { get; set; }
    }
}
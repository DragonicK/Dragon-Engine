using Crystalshire.Network.Messaging.DTO;

namespace Crystalshire.Network.Messaging.SharedPackets {
    public sealed class SpPassiveUpdate : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.PassiveUpdate;
        public DataSkill Passive { get; set; }
    }
}
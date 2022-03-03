using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpPassiveUpdate : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.PassiveUpdate;
        public DataSkill Passive { get; set; }
    }
}
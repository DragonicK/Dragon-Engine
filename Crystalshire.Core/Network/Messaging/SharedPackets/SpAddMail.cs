using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpAddMail : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.AddMail;
        public DataMail? Mail { get; set; } = default;
    }
}
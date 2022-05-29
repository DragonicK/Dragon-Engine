using Crystalshire.Network.Messaging.DTO;

namespace Crystalshire.Network.Messaging.SharedPackets {
    public sealed class SpMailing : IMessagePacket {   
        public MessageHeader Header { get; set; } = MessageHeader.Mailing;
        public DataMail[] Mails { get; set; }= Array.Empty<DataMail>();
    }
}
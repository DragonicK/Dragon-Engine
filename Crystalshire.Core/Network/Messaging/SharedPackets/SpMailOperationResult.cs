using Crystalshire.Core.Model.Mailing;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpMailOperationResult : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.MailOperationResult;
        public MailingOperationCode OperationCode { get; set; } 
    }
}
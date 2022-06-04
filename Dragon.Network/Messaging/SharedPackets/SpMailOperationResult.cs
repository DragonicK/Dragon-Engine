using Dragon.Core.Model.Mailing;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpMailOperationResult : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.MailOperationResult;
    public MailingOperationCode OperationCode { get; set; }
}
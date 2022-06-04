using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpMailing : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Mailing;
    public DataMail[] Mails { get; set; } = Array.Empty<DataMail>();
}
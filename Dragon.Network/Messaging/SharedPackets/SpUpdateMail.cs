namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpUpdateMail : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.UpdateMail;
    public int Id { get; set; }
    public bool AttachCurrencyReceiveFlag { get; set; }
    public bool AttachItemReceiveFlag { get; set; }
}
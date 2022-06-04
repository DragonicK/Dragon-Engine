namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpSendMail : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.SendMail;
    public string Receiver { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int AttachCurrency { get; set; }
    public int AttachInventoryIndex { get; set; }
    public int AttachAmount { get; set; }
}
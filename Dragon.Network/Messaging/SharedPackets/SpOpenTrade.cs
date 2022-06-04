namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpOpenTrade : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.OpenTrade;
    public string Text { get; set; } = string.Empty;
    public string InvitedText { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
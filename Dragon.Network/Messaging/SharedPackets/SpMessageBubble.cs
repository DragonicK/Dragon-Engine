using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpMessageBubble : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.MessageBubble;
    public string Target { get; set; } = string.Empty;
    public TargetType TargetType { get; set; }
    public byte[] Text { get; set; } = Array.Empty<byte>();
    public QbColor Color { get; set; }
}
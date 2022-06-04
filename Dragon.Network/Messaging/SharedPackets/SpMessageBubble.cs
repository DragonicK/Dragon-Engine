using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpMessageBubble : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.MessageBubble;
    public int Target { get; set; }
    public TargetType TargetType { get; set; }
    public string Text { get; set; } = string.Empty;
    public QbColor Color { get; set; }
}
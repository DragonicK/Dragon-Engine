using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpSystemMessage : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.SystemMessage;
    public SystemMessage Message { get; set; }
    public QbColor Color { get; set; }
    public string[]? Parameters { get; set; } = Array.Empty<string>();
}
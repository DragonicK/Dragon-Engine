using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class PacketBroadcastMessage : IMessagePacket {
    public MessageHeader Header { get; private set; } = MessageHeader.BroadcastMessage;
    public ChatChannel Channel { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public AccountLevel AccountLevel { get; set; }
    public QbColor Color { get; set; }
}
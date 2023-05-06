using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class PacketBroadcastMessage : IMessagePacket {
    public MessageHeader Header { get; private set; } = MessageHeader.BroadcastMessage;
    public ChatChannel Channel { get; set; }
    public byte[] Text { get; set; } = Array.Empty<byte>();
    public byte[] Name { get; set; } = Array.Empty<byte>();
    public AccountLevel AccountLevel { get; set; }
    public QbColor Color { get; set; } 
}
namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpInGame : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.InGame;
    public bool IsInGame { get; set; }
}
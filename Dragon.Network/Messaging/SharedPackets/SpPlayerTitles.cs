namespace Dragon.Network.Messaging.SharedPackets;

public class SpPlayerTitles : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PlayerTitles;
    public int[]? Titles { get; set; }
}
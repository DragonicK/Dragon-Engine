namespace Dragon.Network.Messaging.SharedPackets;

public class SpPlayerExperience : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Experience;
    public int Experience { get; set; }
    public int Maximum { get; set; }
}
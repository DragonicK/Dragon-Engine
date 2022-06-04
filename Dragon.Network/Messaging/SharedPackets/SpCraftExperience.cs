namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpCraftExperience : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.CraftExperience;
    public int Experience { get; set; }
    public int Maximum { get; set; }
}
namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpSkillCooldown : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.SkillCooldown;
    public int Index { get; set; }
}
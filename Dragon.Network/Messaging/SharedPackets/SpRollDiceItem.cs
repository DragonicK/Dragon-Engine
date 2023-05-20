namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpRollDiceItem : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.RollDiceItem;
    public int RemainingTime { get; set; }
    public int Id { get; set; }
    public int Value { get; set; }
    public int Level { get; set; }
    public bool Bound { get; set; }
    public int UpgradeId { get; set; }
    public int AttributeId { get; set; }
}
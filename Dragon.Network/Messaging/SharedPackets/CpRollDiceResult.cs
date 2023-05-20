namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpRollDiceResult : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.RollDiceResult;
    public byte IsRolled { get; set; }
}
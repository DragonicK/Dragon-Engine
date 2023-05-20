namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpRollDiceClose : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.RollDiceClose;
}
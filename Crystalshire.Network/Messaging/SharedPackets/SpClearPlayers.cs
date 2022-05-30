namespace Crystalshire.Network.Messaging.SharedPackets;

public sealed class SpClearPlayers : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ClearPlayers;
}
namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpCharacterDelete : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.CharacterDelete;
    public int Index { get; set; }
}
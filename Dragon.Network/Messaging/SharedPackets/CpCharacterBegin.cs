namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpCharacterBegin : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.CharacterBegin;
    public int CharacterIndex { get; set; }
}
namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpCharacterCreate : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.CharacterCreate;
    public string Name { get; set; } = string.Empty;
    public int Gender { get; set; }
    public int ClassIndex { get; set; }
    public int ModelIndex { get; set; }
    public int CharacterIndex { get; set; }
}
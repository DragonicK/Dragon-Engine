namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPlayerModel : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PlayerModel;
    public int Index { get; set; }
    public int Model { get; set; }
}
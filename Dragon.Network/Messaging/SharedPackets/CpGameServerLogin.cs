namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpGameServerLogin : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.GameServerLogin;
    public string Token { get; set; } = string.Empty;
}
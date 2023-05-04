namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpChatServerLogin : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ChatServerLogin;
    public string Token { get; set; } = string.Empty;
}
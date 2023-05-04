namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpConnectChatServer : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ConnectChatServer;
    public string Token { get; set; } = string.Empty;
}
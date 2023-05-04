namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpAuthenticationResult : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.AuthenticationResult;
    public string GameServerIpAddress { get; set; } = string.Empty;
    public int GameServerPort { get; set; }
    public string ChatServerIpAddress { get; set; } = string.Empty;
    public int ChatServePort { get; set; }
    public string Token { get; set; } = string.Empty;
}
namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpAuthenticationResult : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.AuthenticationResult;
        public string IpAddress { get; set; } = string.Empty;
        public int Port { get; set; }        
        public string Token { get; set; } = string.Empty;
    }
}
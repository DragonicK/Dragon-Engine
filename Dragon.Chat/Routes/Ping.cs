using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Chat.Network;
using Dragon.Chat.Services;

namespace Dragon.Chat.Routes;

public sealed class Ping : IRoute {
    public MessageHeader Header => MessageHeader.Ping;
    public LoggerService? LoggerService { get; set; }
    public GeoIpService? GeoIpService { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public ConfigurationService? ConfigurationService { get; set; }
    public OutgoingMessageService? OutgoingMessageService { get; set; }

    public void Process(IConnection connection, object packet) { }
}
using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Login.Network;
using Dragon.Login.Services;

namespace Dragon.Login.Routes;

public sealed class Ping : IRoute {
    public MessageHeader Header => MessageHeader.Ping;
    public LoggerService? LoggerService { get; set; }
    public GeoIpService? GeoIpService { get; set; }
    public DatabaseService? DatabaseService { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public ConfigurationService? ConfigurationService { get; set; }
    public OutgoingMessageService? OutgoingMessageService { get; set; }

    public void Process(IConnection connection, object packet) { }
}
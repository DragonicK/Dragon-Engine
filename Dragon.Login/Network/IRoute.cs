using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Login.Services;

namespace Dragon.Login.Network;

public interface IRoute {
    MessageHeader Header { get; }
    GeoIpService? GeoIpService { get; set; }
    LoggerService? LoggerService { get; set; }
    DatabaseService? DatabaseService { get; set; }
    ConnectionService? ConnectionService { get; set; }
    ConfigurationService? ConfigurationService { get; set; }
    OutgoingMessageService? OutgoingMessageService { get; set; }

    void Process(IConnection connection, object packet);
}
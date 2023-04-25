using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Chat.Services;

namespace Dragon.Chat.Network;

public interface IRoute {
    MessageHeader Header { get; }
    GeoIpService? GeoIpService { get; set; }
    LoggerService? LoggerService { get; set; }
    ConnectionService? ConnectionService { get; set; }
    ConfigurationService? ConfigurationService { get; set; }
    OutgoingMessageService? OutgoingMessageService { get; set; }

    void Process(IConnection connection, object packet);
}
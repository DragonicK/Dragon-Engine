using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Core.Services;

using Dragon.Chat.Services;

namespace Dragon.Chat.Network;

public interface IRoute {
    MessageHeader Header { get; }
    IServiceContainer? ServiceContainer { get; set; }
    GeoIpService? GeoIpService { get; set; }
    LoggerService? LoggerService { get; set; }
    ConnectionService? ConnectionService { get; set; }
    ConfigurationService? ConfigurationService { get; set; }
    OutgoingMessageService? OutgoingMessageService { get; set; }

    void Process(IConnection connection, object packet);
}
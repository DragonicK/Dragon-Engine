using Dragon.Core.Services;
using Dragon.Chat.Services;

namespace Dragon.Chat.Network;

public abstract class PacketRoute {
    public IServiceContainer? ServiceContainer { get; set; }
    public GeoIpService? GeoIpService { get; set; }
    public LoggerService? LoggerService { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public ConfigurationService? ConfigurationService { get; set; }
    public OutgoingMessageService? OutgoingMessageService { get; set; }
}
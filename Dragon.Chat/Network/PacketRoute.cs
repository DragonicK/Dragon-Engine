using Dragon.Core.Services;

using Dragon.Chat.Services;

namespace Dragon.Chat.Network;

public abstract class PacketRoute {
    public IServiceInjector ServiceInjector { get; private set; }
    public IServiceContainer? ServiceContainer { get; private set; }
    public GeoIpService? GeoIpService { get; private set; }
    public LoggerService? LoggerService { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public ConfigurationService? ConfigurationService { get; private set; }
    public OutgoingMessageService? OutgoingMessageService { get; private set; }

    public PacketRoute(IServiceInjector injector) {
        ServiceInjector = injector;
        ServiceInjector.Inject(this);
    }
}
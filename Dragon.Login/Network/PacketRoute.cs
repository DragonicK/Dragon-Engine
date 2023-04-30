using Dragon.Core.Services;

using Dragon.Login.Services;

namespace Dragon.Login.Network;

public abstract class PacketRoute {
    public IServiceInjector ServiceInjector { get; private set; }
    public IServiceContainer? ServiceContainer { get; private set; }
    public GeoIpService? GeoIpService { get; set; }
    public LoggerService? LoggerService { get; set; }
    public DatabaseService? DatabaseService { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public ConfigurationService? ConfigurationService { get; set; }
    public OutgoingMessageService? OutgoingMessageService { get; set; }

    public PacketRoute(IServiceInjector injector) {
        ServiceInjector = injector;
        ServiceInjector.Inject(this);
    }
}
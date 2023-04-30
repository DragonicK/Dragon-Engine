using Dragon.Core.Services;

using Dragon.Login.Services;

namespace Dragon.Login.Network;

public abstract class PacketRoute {
    public IServiceInjector ServiceInjector { get; protected set; }
    public IServiceContainer? ServiceContainer { get; protected set; }
    public GeoIpService? GeoIpService { get; protected set; }
    public LoggerService? LoggerService { get; protected set; }
    public DatabaseService? DatabaseService { get; protected set; }
    public ConfigurationService? Configuration { get; protected set; }
    public ConnectionService? ConnectionService { get; protected set; }
    public OutgoingMessageService? OutgoingMessageService { get; protected set; }

    public PacketRoute(IServiceInjector injector) {
        ServiceInjector = injector;
        ServiceInjector.Inject(this);
    }
}
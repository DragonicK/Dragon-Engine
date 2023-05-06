using Dragon.Core.Logs;
using Dragon.Core.Services;

using Dragon.Network;

using Dragon.Chat.Players;
using Dragon.Chat.Services;
using Dragon.Chat.Repository;
using Dragon.Chat.Pool;

namespace Dragon.Chat.Network;

public abstract class PacketRoute {
    public IServiceInjector ServiceInjector { get; protected set; }
    public IServiceContainer? ServiceContainer { get; protected set; }
    public PoolService? PoolService { get; protected set; }
    public GeoIpService? GeoIpService { get; protected set; }
    public LoggerService? LoggerService { get; protected set; }
    public ConfigurationService? Configuration { get; protected set; }
    public ConnectionService? ConnectionService { get; protected set; }
    public PacketSenderService? PacketSenderService { get; protected set; }
    public OutgoingMessageService? OutgoingMessageService { get; protected set; }

    public PacketRoute(IServiceInjector injector) {
        ServiceInjector = injector;
        ServiceInjector.Inject(this);
    }

    public BubblePool GetBubblePool() {
        return PoolService!.BubblePool!;
    }

    public TargetPool GetTargetPool() {
        return PoolService!.TargetPool!;
    }

    protected ILogger GetLogger() {
        return LoggerService!.Logger!;
    }

    protected IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    protected IPlayerRepository GetPlayerRepository() {
        return ConnectionService!.PlayerRepository!;
    }

    protected IPlayer? FindByConnection(IConnection connection) {
        return GetPlayerRepository().FindByConnection(connection);
    }
}
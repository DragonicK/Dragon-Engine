using Dragon.Core.Logs;
using Dragon.Core.Services;

using Dragon.Network;

using Dragon.Game.Services;
using Dragon.Game.Repository;
using Dragon.Game.Network.Senders;
using Dragon.Game.Instances;
using Dragon.Game.Players;

namespace Dragon.Game.Network;

public abstract class PacketRoute {
    public IServiceInjector ServiceInjector { get; protected set; }
    public IServiceContainer? ServiceContainer { get; protected set; }

    public GeoIpService? GeoIpService { get; protected set; }
    public LoggerService? LoggerService { get; protected set; }
    public EffectService? EffectService { get; protected set; }
    public CombatService? CombatService { get; protected set; }
    public ContentService? ContentService { get; protected set; }
    public InstanceService? InstanceService { get; protected set; }
    public DatabaseService? DatabaseService { get; protected set; }
    public CharacterService? CharacterService { get; protected set; }
    public ConfigurationService? Configuration { get; protected set; }
    public ConnectionService? ConnectionService { get; protected set; }
    public PassphraseService? PassphraseService { get; protected set; }
    public BlackMarketService? BlackMarketService { get; protected set; }
    public PacketSenderService? PacketSenderService { get; protected set; }
    public OutgoingMessageService? OutgoingMessageService { get; protected set; }

    public PacketRoute(IServiceInjector injector) {
        ServiceInjector = injector;
        ServiceInjector.Inject(this);
    }

    protected ILogger GetLogger() {
        return LoggerService!.Logger!;
    }

    protected IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
    
    public IDictionary<int, IInstance> GetInstances() {
        return InstanceService!.Instances;
    }

    protected IPlayerRepository GetPlayerRepository() {
        return ConnectionService!.PlayerRepository!;
    }

    protected IPlayer? FindByConnection(IConnection connection) {
        return GetPlayerRepository().FindByConnection(connection);
    }
}
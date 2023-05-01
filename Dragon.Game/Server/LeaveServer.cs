using Dragon.Network;

using Dragon.Core.Logs;
using Dragon.Core.GeoIpCountry;

using Dragon.Game.Services;
using Dragon.Core.Services;
using Dragon.Game.Repository;

namespace Dragon.Game.Server;

public sealed class LeaveServer {
    public GeoIpService? GeoIpService { get; private set; }
    public LoggerService? LoggerService { get; private set; }
    public DatabaseService? DatabaseService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private readonly LeaveGame LeaveManager;

    public LeaveServer(IServiceInjector injector) {
        injector.Inject(this);

        LeaveManager = new LeaveGame(injector);
    }

    public void DisconnectConnection(IConnection connection) {
        var logger = GetLogger();

        var (id, ipAddress) = GetIdAndIpAddress(connection);

        logger?.Info(GetType().Name, $"Disconnected Id: {id} IpAddress: {ipAddress}");

        GetConnectionRepository().RemoveFromId(id);
        GetIndexGenerator().Remove(id);

        var player = GetPlayerRepository().RemoveFromConnectionId(id);

        if (player is not null) {
            LeaveManager.Leave(player);
        }
    }

    public void RefuseConnection(IConnection connection) {
        var logger = GetLogger();

        var (id, ipAddress) = GetIdAndIpAddress(connection);
        var country = GetBlockedCountry(ipAddress);
        var text = country is not null ? $"{country.Name}-{country.Code}" : string.Empty;

        if (id > 0) {
            GetConnectionRepository().RemoveFromId(id);
            GetIndexGenerator().Remove(id);
            GetPlayerRepository().RemoveFromConnectionId(id);
        }

        logger?.Info(GetType().Name, $"Refused From {text} IpAddress: {ipAddress} Id: {id}");
    }

    private Country? GetBlockedCountry(string ipAddress) {
        return GetGeoIpAddress().GetBlockedCountry(ipAddress);
    }

    private (int id, string ipAddress) GetIdAndIpAddress(IConnection connection) {
        var id = connection is not null ? connection.Id : 0;
        var ipAddress = connection is not null ? connection.IpAddress : string.Empty;

        return (id, ipAddress);
    }

    private ILogger? GetLogger() {
        return LoggerService!.Logger;
    }

    private IGeoIpAddress GetGeoIpAddress() {
        return GeoIpService!.GeoIpAddress!;
    }

    private IIndexGenerator GetIndexGenerator() {
        return ConnectionService!.IndexGenerator!;
    }

    private IConnectionRepository GetConnectionRepository() {
        return ConnectionService!.ConnectionRepository!;
    }

    private IPlayerRepository GetPlayerRepository() {
        return ConnectionService!.PlayerRepository!;
    }
}
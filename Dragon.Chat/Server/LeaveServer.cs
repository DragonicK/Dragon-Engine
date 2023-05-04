using Dragon.Network;

using Dragon.Core.Logs;
using Dragon.Core.Services;
using Dragon.Core.GeoIpCountry;

using Dragon.Chat.Services;
using Dragon.Chat.Repository;

namespace Dragon.Chat.Server;

public sealed class LeaveServer {
    public GeoIpService? GeoIpService { get; private set; }
    public LoggerService? LoggerService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }

    public LeaveServer(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void DisconnectConnection(IConnection connection) {
        var logger = GetLogger();

        var (id, ipAddress) = GetIdAndIpAddress(connection);

        GetRepository().RemoveFromId(id);
        GetIndexGenerator()?.Remove(id);

        GetPlayerRepository().RemoveFromConnection(connection);

        logger?.Info(GetType().Name, $"Disconnected Id: {id} IpAddress: {ipAddress}");
    }

    public void RefuseConnection(IConnection connection) {
        var logger = GetLogger();

        var (id, ipAddress) = GetIdAndIpAddress(connection);
        var country = GetBlockedCountry(ipAddress);
        var text = country is not null ? $"{country.Name}-{country.Code}" : string.Empty;

        if (id > 0) {
            GetRepository().RemoveFromId(id);
            GetIndexGenerator().Remove(id);
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
        return ConnectionService!.IndexGenerator;
    }

    private IConnectionRepository GetRepository() {
        return ConnectionService!.ConnectionRepository!;
    }

    private IPlayerRepository GetPlayerRepository() {
        return ConnectionService!.PlayerRepository!;
    }
}
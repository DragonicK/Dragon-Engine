using Dragon.Network;
using Dragon.Core.Logs;
using Dragon.Core.GeoIpCountry;

using Dragon.Chat.Configurations;

namespace Dragon.Chat.Server;

public sealed class LeftServer {
    public ILogger? Logger { get; init; }
    public IConnection? Connection { get; init; }
    public IGeoIpAddress? GeoIpAddress { get; init; }
    public IConfiguration? Configuration { get; init; }
    public IIndexGenerator? IndexGenerator { get; init; }
    public IConnectionRepository? ConnectionRepository { get; init; }

    public void DisconnectConnection() {
        var (id, ipAddress) = GetIdAndIpAddress();

        ConnectionRepository?.RemoveFromId(id);
        IndexGenerator?.Remove(id);

        Logger?.Info(GetType().Name, $"Disconnected Id: {id} IpAddress: {ipAddress}");
    }

    public void RefuseConnection() {
        var (id, ipAddress) = GetIdAndIpAddress();
        var country = GetBlockedCountry(ipAddress);
        var text = country is not null ? $"{country.Name}-{country.Code}" : string.Empty;

        if (id > 0) {
            ConnectionRepository?.RemoveFromId(id);
            IndexGenerator?.Remove(id);
        }

        Logger?.Info(GetType().Name, $"Refused From {text} IpAddress: {ipAddress} Id: {id}");
    }

    private Country? GetBlockedCountry(string ipAddress) {
        return GeoIpAddress?.GetBlockedCountry(ipAddress);
    }

    private (int id, string ipAddress) GetIdAndIpAddress() {
        var id = Connection is not null ? Connection.Id : 0;
        var ipAddress = Connection is not null ? Connection.IpAddress : string.Empty;

        return (id, ipAddress);
    }
}
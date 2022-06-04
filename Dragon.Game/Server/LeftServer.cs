using Dragon.Network;

using Dragon.Core.Logs;
using Dragon.Core.GeoIpCountry;

using Dragon.Game.Services;
using Dragon.Game.Configurations;
using Dragon.Game.Repository;

namespace Dragon.Game.Server;

public sealed class LeftServer {
    public ILogger? Logger { get; init; }
    public IConnection? Connection { get; init; }
    public IGeoIpAddress? GeoIpAddress { get; init; }
    public IConfiguration? Configuration { get; init; }
    public IIndexGenerator? IndexGenerator { get; init; }
    public IConnectionRepository? ConnectionRepository { get; init; }
    public IPlayerRepository? PlayerRepository { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }
    public DatabaseService? DatabaseService { get; init; }

    public void DisconnectConnection() {
        var (id, ipAddress) = GetIdAndIpAddress();

        var description = new Description() {
            Name = "Connection Disconnected",
            WarningCode = WarningCode.Success,
            Message = $"ConnectionId: {id} IpAddress: {ipAddress}"
        };

        Logger?.Write(description);

        WriteOutputLog($"Connection Disconnect: {id} {ipAddress}");

        ConnectionRepository?.RemoveFromId(id);
        IndexGenerator?.Remove(id);

        var player = PlayerRepository?.RemoveFromConnectionId(id);

        if (player is not null) {
            var game = new LeftGame() {
                Player = player,
                Logger = Logger,
                Configuration = Configuration,
                DatabaseService = DatabaseService,
                PacketSenderService = PacketSenderService
            };

            game.Left();
        }
    }

    public void RefuseConnection() {
        var (id, ipAddress) = GetIdAndIpAddress();
        var country = GetBlockedCountry(ipAddress);
        var text = country is not null ? $"{country.Name}-{country.Code}" : string.Empty;

        var description = new Description() {
            Name = "Connection Refuse",
            WarningCode = WarningCode.Warning,
            Message = $"From {text} IpAddress: {ipAddress} Id: {id}"
        };

        Logger?.Write(description);

        if (id > 0) {
            ConnectionRepository?.RemoveFromId(id);
            IndexGenerator?.Remove(id);
            PlayerRepository?.RemoveFromConnectionId(id);
        }

        WriteOutputLog($"{description.Name}: {description.Message}");
    }

    private void WriteOutputLog(string description) {
        if (Configuration is not null) {
            if (Configuration.Debug) {
                OutputLog.Write(description);
            }
        }
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
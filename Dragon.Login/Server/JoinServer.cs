using Dragon.Network;
using Dragon.Core.Logs;

using Dragon.Login.Configurations;

namespace Dragon.Login.Server;

public sealed class JoinServer {
    public ILogger? Logger { get; init; }
    public IConnection? Connection { get; init; }
    public IConfiguration? Configuration { get; init; }

    public void AcceptConnection() {
        var id = Connection is not null ? Connection.Id : 0;
        var ipAddress = Connection is not null ? Connection.IpAddress : string.Empty;

        Logger?.Info("JoinServer", $"Approval Id: {id} IpAddress: {ipAddress}");
    }
}
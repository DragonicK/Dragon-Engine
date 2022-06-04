using Dragon.Core.Logs;
using Dragon.Core.Services;

namespace Dragon.Game.Services;

public class LoggerService : IService {
    public ServicePriority Priority => ServicePriority.High;
    public ILogger? ConnectionLogger { get; private set; }
    public ILogger? ServerLogger { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public void Start() {
        const string Folder = "./Server/Logs/";

        var connection = Configuration is not null && Configuration.ConnectionLogs;
        var server = Configuration is not null && Configuration.ServerLogs;

        ConnectionLogger = new Logger("Connection", Folder, connection);

        if (connection) {
            var error = ConnectionLogger.Open();

            if (!ConnectionLogger.Opened) {
                OutputLog.Write(error);
            }
        }

        ServerLogger = new Logger("Server", Folder, server);

        if (server) {
            var error = ServerLogger.Open();

            if (!ServerLogger.Opened) {
                OutputLog.Write(error);
            }
        }
    }
    public void Stop() {
        ConnectionLogger?.Close();
        ServerLogger?.Close();
    }
}
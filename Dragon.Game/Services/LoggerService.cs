using Dragon.Core.Logs;
using Dragon.Core.Services;

namespace Dragon.Game.Services;

public sealed class LoggerService : IService {
    public ServicePriority Priority => ServicePriority.First;
    public ILogger? Logger { get; private set; }
    public ConfigurationService? Configuration { get; private set; }

    public LoggerService() {
        const string Folder = "./Server/Logs/";

        Logger = new Logger(Folder);
    }

    public void Start() {
        Logger?.Start();
    }

    public void Stop() {
        Logger?.Stop();
    }
}
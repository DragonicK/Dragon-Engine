using Dragon.Network;

using Dragon.Core.Services;

using Dragon.Chat.Repository;

namespace Dragon.Chat.Services;

public sealed class ConnectionService : IService {
    public ServicePriority Priority => ServicePriority.High;
    public IConnectionRepository? ConnectionRepository { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public IPlayerRepository? PlayerRepository { get; private set; }
    public IIndexGenerator? IndexGenerator { get; private set; }
    public LoggerService? LoggerService { get; private set; }

    public void Start() {
        ConnectionRepository = new ConnectionRepository(LoggerService!.Logger!);

        if (Configuration is not null) {
            IndexGenerator = new IndexGenerator(Configuration.MaximumConnections);
            PlayerRepository = new PlayerRepository(Configuration.MaximumConnections);
        }
    }

    public void Stop() {
        ConnectionRepository?.Clear();
    }
}
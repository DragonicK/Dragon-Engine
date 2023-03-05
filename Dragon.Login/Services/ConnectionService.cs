using Dragon.Network;
using Dragon.Core.Services;

namespace Dragon.Login.Services;

public class ConnectionService : IService {
    public ServicePriority Priority => ServicePriority.High;
    public IConnectionRepository? ConnectionRepository { get; private set; }
    public IIndexGenerator? IndexGenerator { get; private set; }
    public LoggerService? LoggerService { get; private set; }
    public ConfigurationService? ConfigurationService { get; private set; }

    public void Start() {
        ConnectionRepository = new ConnectionRepository(LoggerService!.Logger!);

        if (ConfigurationService is not null) {
            IndexGenerator = new IndexGenerator(ConfigurationService.MaximumConnections);
        }
    }

    public void Stop() {
        ConnectionRepository?.Clear();
    }
}
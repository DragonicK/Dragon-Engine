using Crystalshire.Core.Network;
using Crystalshire.Core.Services;

namespace Crystalshire.Login.Services {
    public class ConnectionService : IService {
        public ServicePriority Priority => ServicePriority.High;
        public IConnectionRepository? ConnectionRepository { get; private set; }
        public IIndexGenerator? IndexGenerator { get; private set; }
        public ConfigurationService? ConfigurationService { get; private set; }

        public void Start() {
            ConnectionRepository = new ConnectionRepository();

            if (ConfigurationService is not null) {
                IndexGenerator = new IndexGenerator(ConfigurationService.MaximumConnections);
            }
        }

        public void Stop() {
            ConnectionRepository?.Clear();
        }
    }
}
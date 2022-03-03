using Crystalshire.Core.Network;
using Crystalshire.Core.Services;

using Crystalshire.Game.Repository;

namespace Crystalshire.Game.Services {
    public class ConnectionService : IService, IUpdatableService {
        public ServicePriority Priority => ServicePriority.High;
        public IConnectionRepository? ConnectionRepository { get; private set; }
        public IPlayerRepository? PlayerRepository { get; private set; }
        public IIndexGenerator? IndexGenerator { get; private set; }
        public ConfigurationService? ConfigurationService { get; private set; }

        public void Start() {
            ConnectionRepository = new ConnectionRepository();

            if (ConfigurationService is not null) {
                var maximum = ConfigurationService.MaximumConnections;

                IndexGenerator = new IndexGenerator(maximum);
                PlayerRepository = new PlayerRepository(maximum);
            }
        }

        public void Stop() {
            ConnectionRepository?.Clear();
        }

        public void Update(int deltaTime) {


        }
    }
}
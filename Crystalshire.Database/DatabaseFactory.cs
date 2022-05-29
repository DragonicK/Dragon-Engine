using Crystalshire.Database.Handler;
using Crystalshire.Database.Context;

namespace Crystalshire.Database {
    public class DatabaseFactory : IDatabaseFactory {
        public MembershipHandler GetMembershipHandler(DBConfiguration dBConfiguration) {
            var options = ContextOptions.GetMembershipOptions(dBConfiguration);
            var context = new MembershipContext(options);

            return new MembershipHandler(context);
        }

        public ServerHandler GetServerHandler(DBConfiguration dBConfiguration) {
            var options = ContextOptions.GetServerOptions(dBConfiguration);
            var context = new ServerContext(options);

            return new ServerHandler(context);
        }

        public GameHandler GetGameHandler(DBConfiguration dBConfiguration) {
            var options = ContextOptions.GetGameOptions(dBConfiguration);
            var context = new GameContext(options);

            return new GameHandler(context);
        }
    }
}
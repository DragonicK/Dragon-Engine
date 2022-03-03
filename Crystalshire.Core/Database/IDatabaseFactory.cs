using Crystalshire.Core.Database.Handler;

namespace Crystalshire.Core.Database {
    public interface IDatabaseFactory {
        MembershipHandler GetMembershipHandler(DBConfiguration dBConfiguration);
        ServerHandler GetServerHandler(DBConfiguration dBConfiguration);
        GameHandler GetGameHandler(DBConfiguration dBConfiguration);
    }
}
using Crystalshire.Database.Handler;

namespace Crystalshire.Database {
    public interface IDatabaseFactory {
        MembershipHandler GetMembershipHandler(DBConfiguration dBConfiguration);
        ServerHandler GetServerHandler(DBConfiguration dBConfiguration);
        GameHandler GetGameHandler(DBConfiguration dBConfiguration);
    }
}
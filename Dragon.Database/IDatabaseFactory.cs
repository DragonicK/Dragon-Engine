using Dragon.Database.Handler;

namespace Dragon.Database;

public interface IDatabaseFactory {
    MembershipHandler GetMembershipHandler(DBConfiguration dBConfiguration);
    ServerHandler GetServerHandler(DBConfiguration dBConfiguration);
    GameHandler GetGameHandler(DBConfiguration dBConfiguration);
}
using Dragon.Network;
using Dragon.Database;

using Dragon.Core.Jwt;
using Dragon.Core.GeoIpCountry;

using Dragon.Game.Configurations.Data;

namespace Dragon.Game.Configurations;
public interface IConfiguration {
    JwtSettings JwtSettings { get; }
    bool Debug { get; }
    bool ServerLogs { get; }
    bool ConnectionLogs { get; }
    int MaximumConnections { get; }
    int TimeOut { get; }
    IpAddress GameServer { get; }
    DBConfiguration DatabaseMembership { get; }
    DBConfiguration DatabaseServer { get; }
    DBConfiguration DatabaseGame { get; }
    BlockedCountry BlockedCountry { get; }

    #region Game Configuration
    BlackMarket BlackMarket { get; }
    Character Character { get; }
    Craft Craft { get; }
    Guild Guild { get; }
    ChestDrop ChestDrop { get; }
    Mail Mail { get; }
    Map Map { get;  }
    Group Group { get; }
    Player Player { get; }
    ProhibitedNames ProhibitedNames { get; }
    Rate Rates { get; }
    Ressurrection Ressurrection { get; }
    Trade Trade { get; }
    Allocation Allocation { get; }

    #endregion
}
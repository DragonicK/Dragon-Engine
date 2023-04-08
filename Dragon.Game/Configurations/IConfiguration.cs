using Dragon.Network;
using Dragon.Database;

using Dragon.Core.Jwt;
using Dragon.Core.GeoIpCountry;

using Dragon.Game.Configurations.Data;

namespace Dragon.Game.Configurations;
public interface IConfiguration {
    JwtSettings JwtSettings { get; set; }
    bool Debug { get; set; }
    bool ServerLogs { get; set; }
    bool ConnectionLogs { get; set; }
    int MaximumConnections { get; set; }
    int TimeOut { get; set; }
    IpAddress GameServer { get; set; }
    DBConfiguration DatabaseMembership { get; set; }
    DBConfiguration DatabaseServer { get; set; }
    DBConfiguration DatabaseGame { get; set; }
    BlockedCountry BlockedCountry { get; set; }

    #region Game Configuration
    BlackMarket BlackMarket { get; set; }
    Character Character { get; set; }
    Craft Craft { get; set; }
    Guild Guild { get; set; }
    Drop Drop { get; set; }
    Mail Mail { get; set; }
    Map Map { get; set; }
    Party Party { get; set; }
    Player Player { get; set; }
    ProhibitedNames ProhibitedNames { get; set; }
    Rate Rates { get; set; }
    Ressurrection Ressurrection { get; set; }
    Trade Trade { get; set; }
    Message Messages { get; set; }

    #endregion
}
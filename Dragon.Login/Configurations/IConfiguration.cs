using Dragon.Network;

using Dragon.Database;

using Dragon.Core.Jwt;
using Dragon.Core.Smtp;
using Dragon.Core.Common;
using Dragon.Core.GeoIpCountry;

namespace Dragon.Login.Configurations;

public interface IConfiguration {
    JwtSettings JwtSettings { get; }
    bool Maintenance { get; }
    bool Debug { get; }
    bool ServerLogs { get; }
    bool ConnectionLogs { get; }
    int MaximumConnections { get; }
    bool UseEmailAsLogin { get; }
    bool UseGeoIp { get; }
    bool UseClientCheckSum { get; }
    bool IpBlock { get; }
    int IpBlockLifeTime { get; }
    int FilterCheckAccessTime { get; }
    int FilterIpLifeTime { get; }
    int IpMaxAttempt { get; }
    int IpMaxAccessCount { get; }
    IpAddress LoginServer { get; }
    IpAddress ChatServer { get; }
    IpAddress GameServer { get; }
    ClientVersion ClientVersion { get; }
    SmtpConfiguration Smtp { get; }
    DBConfiguration DatabaseMembership { get; }
    DBConfiguration DatabaseServer { get; }
    BlockedCountry BlockedCountry { get; }
}
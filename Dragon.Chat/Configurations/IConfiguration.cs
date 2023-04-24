using Dragon.Network;

using Dragon.Core.Jwt;
using Dragon.Core.Common;
using Dragon.Core.GeoIpCountry;

namespace Dragon.Chat.Configurations;

public interface IConfiguration {
    JwtSettings JwtSettings { get; set; }
    bool Maintenance { get; set; }
    bool Debug { get; set; }
    bool ServerLogs { get; set; }
    bool ConnectionLogs { get; set; }
    int MaximumConnections { get; set; }
    bool UseGeoIp { get; set; }
    bool UseClientCheckSum { get; set; }
    bool IpBlock { get; set; }
    int IpBlockLifeTime { get; set; }
    int FilterCheckAccessTime { get; set; }
    int FilterIpLifeTime { get; set; }
    int IpMaxAttempt { get; set; }
    int IpMaxAccessCount { get; set; }
    IpAddress ChatServer { get; set; }
    ClientVersion ClientVersion { get; set; }
    BlockedCountry BlockedCountry { get; set; }
}
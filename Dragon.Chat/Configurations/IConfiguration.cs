using Dragon.Core.Jwt;
using Dragon.Core.Common;
using Dragon.Core.GeoIpCountry;

using Dragon.Network;

using Dragon.Chat.Configurations.Data;

namespace Dragon.Chat.Configurations;

public interface IConfiguration {
    JwtSettings JwtSettings { get; }
    bool Maintenance { get; }
    bool Debug { get; }
    bool ServerLogs { get; }
    bool ConnectionLogs { get; }
    int MaximumConnections { get; }
    bool UseGeoIp { get; }
    bool UseClientCheckSum { get; }
    bool IpBlock { get; }
    int IpBlockLifeTime { get; }
    int FilterCheckAccessTime { get; }
    int FilterIpLifeTime { get; }
    int IpMaxAttempt { get; }
    int IpMaxAccessCount { get; }
    IpAddress ChatServer { get; }
    ClientVersion ClientVersion { get; }
    BlockedCountry BlockedCountry { get;  }
    Message Message { get; }
    Allocation Allocation { get; }
}
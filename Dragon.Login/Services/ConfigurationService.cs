using System.Reflection;

using Dragon.Network;

using Dragon.Database;

using Dragon.Core.Jwt;
using Dragon.Core.Smtp;
using Dragon.Core.Common;
using Dragon.Core.Services;
using Dragon.Core.GeoIpCountry;
using Dragon.Core.Serialization;

using Dragon.Login.Configurations;
using Dragon.Login.Configurations.Data;

namespace Dragon.Login.Services;

public sealed class ConfigurationService : IService, IConfiguration {
    public ServicePriority Priority => ServicePriority.First;
    public JwtSettings JwtSettings { get; private set; }
    public bool Maintenance { get; private set; }
    public bool Debug { get; private set; }
    public bool ServerLogs { get; private set; }
    public bool ConnectionLogs { get; private set; }
    public int MaximumConnections { get; private set; }
    public bool UseEmailAsLogin { get; private set; }
    public bool UseGeoIp { get; private set; }
    public bool UseClientCheckSum { get; private set; }
    public bool IpBlock { get; private set; }
    public int IpBlockLifeTime { get; private set; }
    public int FilterCheckAccessTime { get; private set; }
    public int FilterIpLifeTime { get; private set; }
    public int IpMaxAttempt { get; private set; }
    public int IpMaxAccessCount { get; private set; }
    public IpAddress LoginServer { get; private set; }
    public IpAddress ChatServer { get; private set; }
    public IpAddress GameServer { get; private set; }
    public ClientVersion ClientVersion { get; private set; }
    public SmtpConfiguration Smtp { get; private set; }
    public DBConfiguration DatabaseMembership { get; private set; }
    public DBConfiguration DatabaseServer { get; private set; }
    public BlockedCountry BlockedCountry { get; private set; }
    public Allocation Allocation { get; private set; }

    public ConfigurationService() {
        ServerLogs = true;
        ConnectionLogs = true;

        JwtSettings = new JwtSettings() {
            SecurityKey = "7c8f9ad03beee8a2fe4275af8bb52c2e4559eca9",
            DataSecurityKey = "db2f8f86e94c225ddcc9fd04b40491c3",
            ExpirationMinutes = 1
        };

        LoginServer = new IpAddress() {
            Port = 7001
        };

        GameServer = new IpAddress() {
            Ip = "127.0.0.1",
            Port = 7002,
        };

        ChatServer = new IpAddress() {
            Ip = "127.0.0.1",
            Port = 7003,
        };

        ClientVersion = new ClientVersion() {
            ClientMajor = 2
        };

        Smtp = new SmtpConfiguration();

        DatabaseMembership = new DBConfiguration() {
            Database = "EngineMembership"
        };

        DatabaseServer = new DBConfiguration() {
            Database = "EngineServer"
        };

        BlockedCountry = new BlockedCountry();
        BlockedCountry.Add("CH");
        BlockedCountry.Add("JP");
        BlockedCountry.Add("RU");

        MaximumConnections = 1000;
        IpMaxAttempt = 20;
        IpMaxAccessCount = 20;

        IpBlockLifeTime = 600000;
        FilterCheckAccessTime = 3000;
        FilterIpLifeTime = 120000;

        Allocation = new Allocation() {
            IncomingMessageAllocatedSize = ushort.MaxValue,
            OutgoingMessageAllocatedSize = ushort.MaxValue,
        };
    }

    public void Start() {
        const string File = "./Server/Configuration.json";

        if (!Json.FileExists(File)) {
            Json.Save(File, this);
        }
        else {
            var configuration = Json.Get<ConfigurationService>(File);

            if (configuration is not null) {
                InjectObject(configuration);
            }
        }

        Json.Save(File, this);
    }

    public void Stop() {

    }

    internal void InjectObject(IConfiguration configuration) {
        var targetType = GetType();
        var properties = targetType.GetRuntimeProperties();

        var pairs = GetProperties(configuration);
        var values = properties.Select(p => p.Name).ToArray();

        foreach (var name in values) {
            var property = properties.Where(p => p.Name == name).First();

            if (pairs.ContainsKey(name)) {
                if (name.CompareTo(nameof(Priority)) != 0) {
                    property.SetValue(this, pairs[name].GetValue(configuration));
                }
            }
        }
    }

    internal Dictionary<string, PropertyInfo> GetProperties(IConfiguration configuration) {
        var pairs = new Dictionary<string, PropertyInfo>();
        var properties = configuration.GetType().GetRuntimeProperties();
        var values = properties.Select(p => p.Name).ToArray();

        foreach (var name in values) {
            pairs.Add(name, properties.Where(p => p.Name == name).First());
        }

        return pairs;
    }
}
﻿using System.Reflection;

using Dragon.Network;

using Dragon.Core.Jwt;
using Dragon.Core.Common;
using Dragon.Core.Services;
using Dragon.Core.GeoIpCountry;
using Dragon.Core.Serialization;

using Dragon.Chat.Configurations;
using Dragon.Chat.Configurations.Data;

namespace Dragon.Chat.Services;

public sealed class ConfigurationService : IService, IConfiguration {
    public ServicePriority Priority => ServicePriority.First;
    public JwtSettings JwtSettings { get; set; }
    public bool Maintenance { get; set; }
    public bool Debug { get; set; }
    public bool ServerLogs { get; set; }
    public bool ConnectionLogs { get; set; }
    public int MaximumConnections { get; set; }
    public bool UseGeoIp { get; set; }
    public bool UseClientCheckSum { get; set; }
    public bool IpBlock { get; set; }
    public int IpBlockLifeTime { get; set; }
    public int FilterCheckAccessTime { get; set; }
    public int FilterIpLifeTime { get; set; }
    public int IpMaxAttempt { get; set; }
    public int IpMaxAccessCount { get; set; }
    public int BubblePoolCapacity { get; set; }
    public int MessagePoolCapacity { get; set; }
    public IpAddress ChatServer { get; set; }
    public ClientVersion ClientVersion { get; set; }
    public BlockedCountry BlockedCountry { get; set; }
    public Message Message { get; set; }
    public Allocation Allocation { get; set; }

    public ConfigurationService() {
        ServerLogs = true;
        ConnectionLogs = true;

        JwtSettings = new JwtSettings() {
            SecurityKey = "7c8f9ad03beee8a2fe4275af8bb52c2e4559eca9",
            DataSecurityKey = "db2f8f86e94c225ddcc9fd04b40491c3",
            ExpirationMinutes = 1
        };

        ChatServer = new IpAddress() {
            Port = 7003
        };

        ClientVersion = new ClientVersion() { 
            ClientMajor = 2 
        };

        BlockedCountry = new BlockedCountry();
        BlockedCountry.Add("CH");
        BlockedCountry.Add("JP");
        BlockedCountry.Add("RU");

        IpMaxAttempt = 20;
        IpMaxAccessCount = 20;
        MaximumConnections = 1000;

        IpBlockLifeTime = 600000;
        FilterIpLifeTime = 120000;
        FilterCheckAccessTime = 3000;

        Message = new Message() {
            MaximumLength = byte.MaxValue
        };

        BubblePoolCapacity = 2048;
        MessagePoolCapacity = 2048;

        Allocation = new Allocation() {
            BubblesAllocatedSize = 4096,
            BubbleTextAllocatedSize = 64,
            IncomingMessageAllocatedSize = ushort.MaxValue,
            OutgoingMessageAllocatedSize = ushort.MaxValue,
            TargetsAllocatedSize = short.MaxValue,
            TargetListAllocatedSize = 128
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
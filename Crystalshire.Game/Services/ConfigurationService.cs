using System.Reflection;

using Crystalshire.Core.Jwt;
using Crystalshire.Core.Network;
using Crystalshire.Core.Services;
using Crystalshire.Core.Database;
using Crystalshire.Core.GeoIpCountry;
using Crystalshire.Core.Serialization;
using Crystalshire.Game.Configurations;
using Crystalshire.Game.Configurations.Data;

namespace Crystalshire.Game.Services {
    public class ConfigurationService : IService, IConfiguration {
        public ServicePriority Priority { get; private set; } = ServicePriority.First;
        public JwtSettings JwtSettings { get; set; }
        public bool Debug { get; set; }
        public bool ServerLogs { get; set; }
        public bool ConnectionLogs { get; set; }
        public int MaximumConnections { get; set; }
        public int TimeOut { get; set; }
        public int Delay { get; set; }
        public IpAddress GameServer { get; set; }
        public DBConfiguration DatabaseMembership { get; set; }
        public DBConfiguration DatabaseServer { get; set; }
        public DBConfiguration DatabaseGame { get; set; }
        public BlockedCountry BlockedCountry { get; set; }

        public BlackMarket BlackMarket { get; set; }
        public Rate Rates { get; set; }
        public Character Character { get; set; }
        public Corpse Corpse { get; set; }
        public Craft Craft { get; set; }
        public Guild Guild { get; set; }
        public Loot Loot { get; set; }
        public Map Map { get; set; }
        public Mail Mail { get; set; }
        public Party Party { get; set; }
        public Ressurrection Ressurrection { get; set; }
        public Trade Trade { get; set; }
        public Player Player { get; set; }
        public ProhibitedNames ProhibitedNames { get; set; }
        public Message Messages { get; set; }
     
        public ConfigurationService() {
            BlackMarket = new BlackMarket();
            Character = new Character();
            Corpse = new Corpse();
            Craft = new Craft();
            Guild = new Guild();
            Mail = new Mail();
            Map = new Map();
            Loot = new Loot();
            Party = new Party();
            Player = new Player();
            Rates = new Rate();
            Ressurrection = new Ressurrection();
            Trade = new Trade();
            ProhibitedNames = new ProhibitedNames();
            Messages = new Message();

            JwtSettings = new JwtSettings() {
                SecurityKey = "7c8f9ad03beee8a2fe4275af8bb52c2e4559eca9",
                DataSecurityKey = "db2f8f86e94c225ddcc9fd04b40491c3",
                ExpirationMinutes = 1
            };

            GameServer = new IpAddress() {
                Ip = "0.0.0.0",
                Port = 7002
            };

            DatabaseMembership = new DBConfiguration() {
                Database = "EngineMembership"
            };

            DatabaseServer = new DBConfiguration() {
                Database = "EngineServer"
            };

            DatabaseGame = new DBConfiguration() {
                Database = "EngineGame"
            };

            BlockedCountry = new BlockedCountry();
            BlockedCountry.Add("CH");
            BlockedCountry.Add("JP");
            BlockedCountry.Add("RU");

            TimeOut = 30;
            MaximumConnections = 1000;

            Delay = 45;
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
                    if (name.CompareTo("Priority") != 0) {
                        property.SetValue(this, pairs[name].GetValue(configuration));
                    }
                }
            }
        }

        internal IDictionary<string, PropertyInfo> GetProperties(IConfiguration configuration) {
            var pairs = new Dictionary<string, PropertyInfo>();
            var properties = configuration.GetType().GetRuntimeProperties();
            var values = properties.Select(p => p.Name).ToArray();

            foreach (var name in values) {
                pairs.Add(name, properties.Where(p => p.Name == name).First());
            }

            return pairs;
        }
    }
}

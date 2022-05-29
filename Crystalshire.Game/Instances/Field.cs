using Crystalshire.Network;
using Crystalshire.Core.Model.Maps;

using Crystalshire.Game.Players;
using Crystalshire.Game.Regions;
using Crystalshire.Game.Configurations;

namespace Crystalshire.Game.Instances {
    public sealed class Field : IMap, IInstance {
        public int Id { get; set; }
        public int MapId { get; set; }
        public string Name { get; set; }
        public string Music { get; set; }
        public string Ambience { get; set; }
        public int MaximumX { get; set; }
        public int MaximumY { get; set; }
        public Weather Weather { get; set; }
        public Moral Moral { get; set; }
        public Tile[,] Tile { get; set; }
        public Link Link { get; set; }
        public int HighIndex { get; set; }
        public IList<IInstanceEntity> Entities { get; set; }
        public IConfiguration Configuration { get; private set; }
        public IList<IRegionEntity> RegionEntities { get; set; }
        public IIndexGenerator IndexGenerator { get; set; }

        private readonly IDictionary<int, IPlayer> players;

        private int MaximumPlayers;
        private int MaximumCorpses;
        private int MaximumNpcs;

        public Field(IMap map, Region region, IConfiguration configuration) {
            Name = map.Name;
            Music = map.Music;
            Ambience = map.Ambience;
            MaximumX = map.MaximumX;
            MaximumY = map.MaximumY;
            Weather = map.Weather;
            Moral = map.Moral;

            Tile = new Tile[MaximumX, MaximumY];

            for (var x = 0; x < MaximumX; ++x) {
                for (var y = 0; y < MaximumY; ++y) {
                    Tile[x, y] = map.Tile[x, y].Clone();
                }
            }

            Id = region.Id;
            MapId = region.MapId;
            Link = region.Link;

            RegionEntities = new List<IRegionEntity>(region.Entities.Count);
            RegionEntities = region.Entities.ToList();

            Configuration = configuration;

            MaximumPlayers = configuration.Map.MaximumPlayers;
            MaximumNpcs = configuration.Map.MaximumNpcs;
            MaximumCorpses = configuration.Map.MaximumCorpses;

            Entities = new List<IInstanceEntity>(MaximumNpcs);

            IndexGenerator = new IndexGenerator(MaximumPlayers);
            players = new Dictionary<int, IPlayer>(MaximumPlayers);
        }

        public bool Add(IPlayer player) {
            if (!Contains(player)) {
                var index = IndexGenerator.GetNextIndex();

                players.Add(index, player);
                player.IndexOnInstance = index;

                CalculateHighIndex();

                return true;
            }

            return false;
        }

        public bool Contains(IPlayer player) {
            return players.Values.FirstOrDefault(p => p == player) is not null;
        }

        public bool Contains(int index) {
            return players.Values.FirstOrDefault(p => p.IndexOnInstance == index) is not null;
        }

        public bool Remove(IPlayer player) {
            if (Contains(player)) {
                var index = player.IndexOnInstance;

                IndexGenerator.Remove(index);

                player.IndexOnInstance = 0;
                players.Remove(index);

                CalculateHighIndex();

                return true;
            }

            return false;
        }

        public IPlayer? Get(int index) {
            if (players.ContainsKey(index)) {
                return players[index];
            }

            return null;
        }

        public IList<IPlayer> GetPlayers() {
            return players.Select(p => p.Value).ToList();
        }

        public bool IsBlocked(int x, int y) {
            return Tile[x, y].Type == TileType.Blocked;
        }

        private void CalculateHighIndex() {
            int i;

            for (i = MaximumPlayers; i >= 1; i--) {
                if (players.ContainsKey(i)) {
                    break;
                }
            }

            HighIndex = i;
        }
    }
}
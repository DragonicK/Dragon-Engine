using Dragon.Network;

using Dragon.Core.Model;
using Dragon.Core.Model.Maps;
using Dragon.Core.Model.Npcs;

using Dragon.Game.Players;
using Dragon.Game.Regions;
using Dragon.Game.Network;
using Dragon.Game.Configurations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Dragon.Game.Instances;

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

    /// <summary>
    /// Npcs instanciados.
    /// </summary>
    public IList<IInstanceEntity> Entities { get; set; }
    public IConfiguration Configuration { get; private set; }

    /// <summary>
    /// Dados dos npcs dessa região.
    /// </summary>
    public IList<IRegionEntity> RegionEntities { get; set; }
    public IIndexGenerator IndexGenerator { get; set; }

    private readonly IDictionary<int, IPlayer> players;
    private readonly IPacketSender _sender;
    private readonly Random _random;

    private readonly int MaximumPlayers;
    private readonly int MaximumCorpses;
    private readonly int MaximumNpcs;

    public Field(IMap map, Region region, IConfiguration configuration, IPacketSender sender) {
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
        _random = new Random();

        _sender = sender;
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

    public void Execute() {
        if (players.Count > 0) {
            for (var i = 0; i < Entities.Count; ++i) {
                var entity = Entities[i];

                if (!entity.IsDead) {
                    if (entity.Behaviour != NpcBehaviour.Shopkeeper) {
                        ExecuteEntity(i);
                    }
                }
            }
        }
    }

    private void ExecuteEntity(int index) {
        var direction = _random.Next(0, 8);
        var entity = Entities[index];

        var x = entity.X;
        var y = entity.Y;

        if (!entity.IsFixed) {
            if (Enum.IsDefined(typeof(Direction), direction)) {
                var dir = (Direction)direction;

                if (CheckForDirection(dir, x, y)) {
                    ExecuteMove(index, dir, MovementType.Walking);
                }
            }
        }
    }

    private bool CheckForDirection(Direction direction, int x, int y) {
        switch (direction) {
            case Direction.Up:
                if (y <= 0) {
                    return false;
                }

                y -= 1;

                break;
            case Direction.Down:
                if (y >= MaximumY - 1) {
                    return false;
                }

                y += 1;
                break;

            case Direction.Left:
                if (x <= 0) {
                    return false;
                }

                x -= 1;
                break;

            case Direction.Right:
                if (x >= MaximumX - 1) {
                    return false;
                }

                x += 1;
                break;
        }

        var type = Tile[x, y].Type;

        if (type != TileType.Walkable) {
            return false;
        }

        if (IsDirBlocked(Tile[x, y].DirBlock, (int)direction + 1)) {
            return false;
        }

        return true;
    }

    private bool IsDirBlocked(uint blockVar, int dir) {
        var dirResult = Convert.ToUInt32(Math.Pow(2, dir));
        var result = (~blockVar & dirResult);

        if (Convert.ToBoolean(result)) {
            return false;
        }

        return true;
    }

    private void ExecuteMove(int index, Direction direction, MovementType movementType) {
        var entity = Entities[index];

        var x = entity.X;
        var y = entity.Y;

        switch (direction) {
            case Direction.Up:
                y--;
                break;

            case Direction.Down:
                y++;
                break;

            case Direction.Left:
                x--;
                break;

            case Direction.Right:
                x++;
                break;
        }

        entity.X = x;
        entity.Y = y;
        entity.Direction = direction;

        _sender.SendInstanceEntityMove(this, movementType, index);
    }
}
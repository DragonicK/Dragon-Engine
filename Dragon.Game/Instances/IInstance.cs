﻿using Dragon.Network;

using Dragon.Game.Players;
using Dragon.Game.Regions;
using Dragon.Game.Configurations;
using Dragon.Game.Instances.Chests;

namespace Dragon.Game.Instances;

public interface IInstance {
    int Id { get; set; }
    int MapId { get; set; }
    int HighIndex { get; set; }
    int MaximumX { get; set; }
    int MaximumY { get; set; }
    Link Link { get; set; }
    IConfiguration Configuration { get; }
    IList<IRegionEntity> RegionEntities { get; set; }
    IList<IInstanceEntity> Entities { get; set; }
    IIndexGenerator IndexGenerator { get; set; }

    int Add(IInstanceChest chest);
    bool Add(IPlayer player);
    bool Contains(IPlayer player);
    bool Contains(int index);
    bool Remove(IPlayer player);
    bool Remove(IInstanceChest chest);
    IPlayer? GetPlayer(int index);
    IInstanceChest? GetChest(int index);
    IList<IPlayer> GetPlayers();
    IList<IInstanceChest> GetChests();
    bool IsBlocked(int x, int y);
    void Execute();
}
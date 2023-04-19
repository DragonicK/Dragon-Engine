﻿using Dragon.Core.Model.Chests;
using Dragon.Core.Model.Entity;

namespace Dragon.Game.Instances.Chests;

public interface IInstanceChest : IEntity {
    int Index { get; set; }
    int X { get; set; }
    int Y { get; set; }
    Chest Chest { get; set; }
    long OpenedByCharacterId { get; set; }
    long CreateFromCharacterId { get; set; }
    int PartyId { get; set; }
    int RemainingTime { get; set; }
    ChestState State { get; set; }
    IInstance Instance { get; set; }
    IList<IInstanceChestItem> Items { get; set; }
}
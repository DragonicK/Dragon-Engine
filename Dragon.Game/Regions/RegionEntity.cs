﻿using Dragon.Core.Model;

namespace Dragon.Game.Regions;

public sealed class RegionEntity : IRegionEntity {
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; }
    public int SpawnWait { get; set; }
    public int MaximumRangeX { get; set; }
    public int MaximumRangeY { get; set; }
    public bool IsFixed { get; set; }
}
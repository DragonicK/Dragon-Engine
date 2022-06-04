﻿using Dragon.Core.Model;
using Dragon.Core.Model.Entity;

namespace Dragon.Game.Combat;

public struct Target {
    public IEntity? Entity { get; set; }
    public TargetType Type { get; set; }
}
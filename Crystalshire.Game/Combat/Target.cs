using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Entity;

namespace Crystalshire.Game.Combat;

public struct Target {
    public IEntity? Entity { get; set; }
    public TargetType Type { get; set; }
}
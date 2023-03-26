using Dragon.Core.Model;
using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Entity;

namespace Dragon.Game.Instances;

public interface IInstanceEntity : IEntity {
    int X { get; set; }
    int Y { get; set; }
    bool IsDead { get; set; }
    Direction Direction { get; set; }
    int MaximumRangeX { get; set; }
    int MaximumRangeY { get; set; }
    bool IsFixed { get; set; }
    NpcBehaviour Behaviour { get; set; }
}
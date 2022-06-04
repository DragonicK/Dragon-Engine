using Dragon.Core.Model;
using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Entity;

namespace Dragon.Game.Instances;

public interface IInstanceEntity {
    int Id { get; set; }
    int X { get; set; }
    int Y { get; set; }
    bool IsDead { get; set; }
    Direction Direction { get; set; }
    int MaximumRangeX { get; set; }
    int MaximumRangeY { get; set; }
    bool IsFixed { get; set; }
    int IndexOnInstance { get; set; }
    IEntityAttribute Attributes { get; set; }
    IEntityVital Vitals { get; set; }
    IEntityEffect Effects { get; set; }
    NpcBehaviour Behaviour { get; set; }
}
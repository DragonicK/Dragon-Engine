using Dragon.Core.Model;
using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Entity;

namespace Dragon.Game.Instances;

public sealed class InstanceEntity : IEntity, IInstanceEntity {
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsDead { get; set; }
    public Direction Direction { get; set; }
    public int MaximumRangeX { get; set; }
    public int MaximumRangeY { get; set; }
    public bool IsFixed { get; set; }
    public IEntityAttribute Attributes { get; set; }
    public IEntityVital Vitals { get; set; }
    public IEntityCombat Combat { get; set; }
    public IEntityEffect Effects { get; set; }
    public IEntity? Target { get; set; }
    public TargetType TargetType { get; set; }
    public NpcBehaviour Behaviour { get; set; }
    public int IndexOnInstance { get; set; }

    public int GetX() {
        return X;
    }

    public int GetY() {
        return Y;
    }

    public InstanceEntity() {
        Effects = new EntityEffect();
        Attributes = new EntityAttribute();
        Vitals = new EntityVital();

        Combat = null;
    }
}
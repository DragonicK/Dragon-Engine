using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Npcs;
using Crystalshire.Core.Model.Entity;

namespace Crystalshire.Game.Instances {
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
}
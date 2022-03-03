namespace Crystalshire.Core.Model.Entity {
    public interface IEntity {
        int Id { get; set; }
        IEntityAttribute Attributes { get; set; }
        IEntityVital Vitals { get; set; }
        IEntityCombat Combat { get; set; }
        IEntityEffect Effects { get; set; }
        IEntity? Target { get; set; }
        TargetType TargetType { get; set; }
        int IndexOnInstance { get; set; }

        public int GetX();
        public int GetY();
    }
}
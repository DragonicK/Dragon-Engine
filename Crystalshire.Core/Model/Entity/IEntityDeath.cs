namespace Crystalshire.Core.Model.Entity {
    public interface IEntityDeath {
        IEntity? Entity { get; set; }
        void Execute();
    }
}
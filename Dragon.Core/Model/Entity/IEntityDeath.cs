namespace Dragon.Core.Model.Entity;

public interface IEntityDeath {
    void Execute(IEntity? attacker, IEntity receiver);
}
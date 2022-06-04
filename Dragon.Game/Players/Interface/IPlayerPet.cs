using Dragon.Core.Model.Entity;

namespace Dragon.Game.Players;

public interface IPlayerPet {
    IEntityAttribute Attributes { get; }
}
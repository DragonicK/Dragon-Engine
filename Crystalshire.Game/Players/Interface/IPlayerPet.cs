using Crystalshire.Core.Model.Entity;

namespace Crystalshire.Game.Players;

public interface IPlayerPet {
    IEntityAttribute Attributes { get; }
}
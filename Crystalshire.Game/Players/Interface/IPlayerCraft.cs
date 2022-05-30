using Crystalshire.Core.Model.Crafts;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players;

public interface IPlayerCraft {
    CraftState State { get; set; }
    int ProcessingRecipeId { get; set; }
    int NextLevelExperience { get; set; }
    int Experience { get; set; }
    int Level { get; set; }
    CraftType Profession { get; set; }
    CharacterCraft GetCharacterCraft();
}
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players;

public interface IPlayerSettings {
    bool ViewEquipment { get; set; }
    CharacterSettings GetSettings();
}
using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public interface IPlayerSettings {
    bool ViewEquipment { get; set; }
    CharacterSettings GetSettings();
}
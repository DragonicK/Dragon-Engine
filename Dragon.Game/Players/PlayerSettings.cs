using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public class PlayerSettings : IPlayerSettings {
    public bool ViewEquipment {
        get {
            return _settings.ViewEquipment;
        }
        set {
            _settings.ViewEquipment = value;
        }
    }

    private readonly CharacterSettings _settings;

    public PlayerSettings(long characterId, CharacterSettings settings) {
        _settings = settings;

        if (_settings is null) {
            _settings = new CharacterSettings() {
                CharacterId = characterId
            };
        }
    }

    public CharacterSettings GetSettings() {
        return _settings;
    }
}
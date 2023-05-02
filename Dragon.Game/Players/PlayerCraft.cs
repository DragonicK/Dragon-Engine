using Dragon.Core.Model.Crafts;
using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public sealed class PlayerCraft : IPlayerCraft {
    public int NextLevelExperience { get; set; }
    public int ProcessingRecipeId { get; set; }
    public CraftState State { get; set; }
    public int Experience {
        get {
            return _craft.Experience;
        }

        set {
            _craft.Experience = value;
        }
    }
    public CraftType Profession {
        get {
            return _craft.Type;
        }

        set {
            _craft.Type = value;
        }
    }
    public int Level {
        get {
            return _craft.Level;
        }

        set {
            _craft.Level = value;
        }
    }

    private readonly CharacterCraft _craft;

    public PlayerCraft(long characterId, CharacterCraft craft) {
        _craft = craft;

        if (_craft is null) {
            _craft = new CharacterCraft() {
                CharacterId = characterId
            };
        }
    }

    public CharacterCraft GetCharacterCraft() {
        return _craft;
    }
}
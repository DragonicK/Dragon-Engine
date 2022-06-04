using Dragon.Core.Model;
using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public class PlayerPrimaryAttribute : IPlayerPrimaryAttribute {
    private readonly CharacterPrimaryAttribute _attribute;

    public PlayerPrimaryAttribute(long characterId, CharacterPrimaryAttribute attribute) {
        _attribute = attribute;

        if (_attribute is null) {
            _attribute = new CharacterPrimaryAttribute() {
                CharacterId = characterId
            };
        }
    }

    public void Set(PrimaryAttribute attribute, int value) {
        switch (attribute) {
            case PrimaryAttribute.Strength:
                _attribute.Strength = value;
                break;

            case PrimaryAttribute.Agility:
                _attribute.Agility = value;
                break;

            case PrimaryAttribute.Constitution:
                _attribute.Constitution = value;
                break;

            case PrimaryAttribute.Intelligence:
                _attribute.Intelligence = value;
                break;

            case PrimaryAttribute.Spirit:
                _attribute.Spirit = value;
                break;

            case PrimaryAttribute.Will:
                _attribute.Will = value;
                break;
        }
    }

    public void Add(PrimaryAttribute attribute, int value) {
        switch (attribute) {
            case PrimaryAttribute.Strength:
                _attribute.Strength += value;
                break;

            case PrimaryAttribute.Agility:
                _attribute.Agility += value;
                break;

            case PrimaryAttribute.Constitution:
                _attribute.Constitution += value;
                break;

            case PrimaryAttribute.Intelligence:
                _attribute.Intelligence += value;
                break;

            case PrimaryAttribute.Spirit:
                _attribute.Spirit += value;
                break;

            case PrimaryAttribute.Will:
                _attribute.Will += value;
                break;
        }
    }

    public int Get(PrimaryAttribute attribute) => attribute switch {
        PrimaryAttribute.Strength => _attribute.Strength,
        PrimaryAttribute.Agility => _attribute.Agility,
        PrimaryAttribute.Constitution => _attribute.Constitution,
        PrimaryAttribute.Intelligence => _attribute.Intelligence,
        PrimaryAttribute.Spirit => _attribute.Spirit,
        PrimaryAttribute.Will => _attribute.Will,
        _ => 0
    };
}
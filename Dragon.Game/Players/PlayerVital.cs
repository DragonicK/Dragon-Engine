using Dragon.Core.Model;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public class PlayerVital : IEntityVital, IPlayerVital {
    private readonly CharacterVital _vital;

    private readonly int[] maximum;

    public PlayerVital(long characterId, CharacterVital vital) {
        _vital = vital;

        if (_vital is null) {
            _vital = new CharacterVital() {
                CharacterId = characterId
            };
        }

        maximum = new int[Enum.GetValues<Vital>().Length];
    }

    public int Get(Vital vital) {
        int value = _vital.Health;

        if (vital == Vital.MP) {
            value = _vital.Mana;
        }
        else if (vital == Vital.Special) {
            value = _vital.Special;
        }

        return value;
    }

    public int GetMaximum(Vital vital) {
        return maximum[(int)vital];
    }

    public void Set(Vital vital, int value) {
        if (vital == Vital.HP) {
            _vital.Health = value;
        }
        else if (vital == Vital.MP) {
            _vital.Mana = value;
        }
        else if (vital == Vital.Special) {
            _vital.Special = value;
        }
    }

    public void SetMaximum(Vital vital, int value) {
        if (Get(vital) == maximum[(int)vital]) {
            Set(vital, value);
        }

        maximum[(int)vital] = value;
    }

    public void Add(Vital vital, int value) {
        value = Get(vital) + value;

        if (value > GetMaximum(vital)) {
            value = GetMaximum(vital);
        }

        Set(vital, value);
    }

    public CharacterVital Get() {
        return _vital;
    }
}
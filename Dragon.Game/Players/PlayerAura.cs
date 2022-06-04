using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public class PlayerAura : IPlayerAura {
    public int Count => _auras.Count;

    private const int Capacity = 32;

    private readonly IList<CharacterAura> _auras;

    public PlayerAura() {
        _auras = new List<CharacterAura>(Capacity);
    }

    public void Add(int id, int level, int range) {
        if (!Contains(id)) {
            _auras.Add(new CharacterAura() {
                Id = id,
                Level = level,
                Range = range
            });
        }
    }

    public bool Contains(int id) {
        foreach (var aura in _auras) {
            if (aura.Id == id) {
                return true;
            }
        }

        return false;
    }

    public void Remove(int id) {
        foreach (var aura in _auras) {
            if (aura.Id == id) {
                _auras.Remove(aura);

                break;
            }
        }
    }

    public IList<CharacterAura> ToList() {
        return _auras;
    }
}
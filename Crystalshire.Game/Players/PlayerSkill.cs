using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players;

public class PlayerSkill : IPlayerSkill {
    public int Count => _skills.Count;

    private readonly IList<CharacterSkill> _skills;
    private readonly long _characterId;

    public PlayerSkill(long characterId, IList<CharacterSkill> skills) {
        _skills = skills;
        _characterId = characterId;
    }

    public CharacterSkill Add(int id, int level) {
        var selected = _skills.FirstOrDefault(p => p.SkillId == id);

        if (selected is null) {
            selected = new CharacterSkill() {
                CharacterId = _characterId,
                SkillId = id,
                SkillLevel = level
            };

            _skills.Add(selected);
        }

        return selected;
    }

    public bool Contains(int id) {
        var selected = _skills.FirstOrDefault(p => p.SkillId == id);

        return selected is not null;
    }

    public CharacterSkill? Get(int index) {
        if (index < 0 || index >= _skills.Count) {
            return null;
        }

        return _skills[index];
    }

    public IList<CharacterSkill> ToList() {
        return _skills;
    }
}
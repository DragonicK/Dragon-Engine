using Dragon.Core.Content;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Passives;
using Dragon.Core.Model.Characters;
using Dragon.Core.Model.Attributes;
using Dragon.Core.Model.Entity;

namespace Dragon.Game.Players;

public sealed class PlayerPassive : IPlayerPassive {
    public IEntityAttribute Attributes { get; }
    public IDatabase<Skill>? Skills { get; set; }
    public IDatabase<Passive>? Passives { get; set; }
    public IDatabase<GroupAttribute>? PassiveAttributes { get; set; }
    public IDatabase<GroupAttribute>? PassiveUpgrades { get; set; }

    private readonly IList<CharacterPassive> _passives;
    private readonly long _characterId;

    public PlayerPassive(long characterId, IList<CharacterPassive> passive) {
        _passives = passive;
        _characterId = characterId;
        Attributes = new EntityAttribute();
    }

    public bool Contains(int id) {
        var selected = _passives.FirstOrDefault(p => p.PassiveId == id);

        return selected is not null;
    }

    public CharacterPassive Add(int id, int level) {
        var selected = _passives.FirstOrDefault(p => p.PassiveId == id);

        if (selected is null) {
            selected = new CharacterPassive() {
                CharacterId = _characterId,
                PassiveId = id,
                PassiveLevel = level
            };

            _passives.Add(selected);
        }

        return selected;
    }

    public void UpdateAttributes() {
        Attributes.Clear();

        foreach (var inventory in _passives) {
            var skill = GetSkill(inventory.PassiveId);

            if (skill is not null) {
                if (skill.Type == SkillType.Passive) {
                    var passive = GetPassive(skill.PassiveId);

                    UpdateAttributes(passive, inventory);
                }
            }
        }
    }

    private void UpdateAttributes(Passive? passive, CharacterPassive inventory) {
        if (passive is not null) {
            if (passive.PassiveType == PassiveType.Attributes) {

                var attribute = GetAttribute(passive.AttributeId);

                if (attribute is not null) {
                    var upgrade = GetUpgrade(passive.UpgradeId);

                    Attributes.Add(inventory.PassiveLevel, attribute, upgrade);
                }

            }
        }
    }

    public IList<CharacterPassive> ToList() {
        return _passives;
    }

    private Skill? GetSkill(int id) {
        if (Skills is not null) {
            if (Skills.Contains(id)) {
                return Skills[id];

            }
        }

        return null;
    }

    private Passive? GetPassive(int id) {
        if (Passives is not null) {
            if (Passives.Contains(id)) {
                return Passives[id];

            }
        }

        return null;
    }

    private GroupAttribute? GetAttribute(int id) {
        if (PassiveAttributes is not null) {
            if (PassiveAttributes!.Contains(id)) {
                return PassiveAttributes[id];
            }
        }

        return null;
    }

    private GroupAttribute GetUpgrade(int id) {
        if (PassiveUpgrades is not null) {
            if (PassiveUpgrades.Contains(id)) {
                return PassiveUpgrades[id]!;
            }
        }

        return GroupAttribute.Empty;
    }

}
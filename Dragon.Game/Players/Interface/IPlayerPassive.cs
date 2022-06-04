using Dragon.Core.Content;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Passives;
using Dragon.Core.Model.Characters;
using Dragon.Core.Model.Attributes;
using Dragon.Core.Model.Entity;

namespace Dragon.Game.Players;

public interface IPlayerPassive {
    IEntityAttribute Attributes { get; }
    IDatabase<Skill>? Skills { get; set; }
    IDatabase<Passive>? Passives { get; set; }
    IDatabase<GroupAttribute>? PassiveAttributes { get; set; }
    IDatabase<GroupAttribute>? PassiveUpgrades { get; set; }
    bool Contains(int id);
    CharacterPassive Add(int id, int level);
    void UpdateAttributes();
    IList<CharacterPassive> ToList();
}
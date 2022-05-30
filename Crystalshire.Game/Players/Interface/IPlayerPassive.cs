using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Skills;
using Crystalshire.Core.Model.Passives;
using Crystalshire.Core.Model.Characters;
using Crystalshire.Core.Model.Attributes;
using Crystalshire.Core.Model.Entity;

namespace Crystalshire.Game.Players;

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
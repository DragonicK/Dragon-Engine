using Dragon.Core.Model.Classes;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;

namespace Dragon.Game.Characters;

public interface ICharacterCreator {
    IList<CharacterSkill> CreateSkills(IClass classJob, long characterId);
    IList<CharacterPassive> CreatePassives(IClass classJob, long characterId);
    IList<CharacterEquipment> CreateEquipments(IClass classJob, long characterId);
    IList<CharacterInventory> CreateInventories(IClass classJob, long characterId);
    Character CreateCharacter(IPlayer player, IClass classJob, int model, string name, int gender, int index);
}
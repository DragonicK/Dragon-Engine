using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Configurations;

namespace Dragon.Game.Characters;

public interface ICharacterCreation {
    IPlayer Player { get; }
    IConfiguration Configuration { get; }
    ICharacterValidation Validated { get; }
    Character CreateCharacter();
    IList<CharacterInventory> CreateInventories(Character character);
    IList<CharacterEquipment> CreateEquipments(Character character);
    IList<CharacterSkill> CreateSkills(Character character);
    IList<CharacterPassive> CreatePassives(Character character);
}
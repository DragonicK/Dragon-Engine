using Crystalshire.Core.Model.Characters;

using Crystalshire.Game.Players;
using Crystalshire.Game.Configurations;

namespace Crystalshire.Game.Characters {
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
}
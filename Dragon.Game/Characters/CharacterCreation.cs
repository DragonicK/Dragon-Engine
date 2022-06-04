using Dragon.Core.Model.Characters;
using Dragon.Game.Configurations;
using Dragon.Game.Players;

namespace Dragon.Game.Characters;

public class CharacterCreation : ICharacterCreation {
    public IPlayer Player { get; private set; }
    public IConfiguration Configuration { get; }
    public ICharacterValidation Validated { get; }

    public CharacterCreation(IPlayer player, IConfiguration configuration, ICharacterValidation validated) {
        Player = player;
        Configuration = configuration;
        Validated = validated;
    }

    public Character CreateCharacter() {
        var classe = Validated.CharacterClass;

        return new Character {
            AccountId = Player.AccountId,
            Name = Validated.CharacterName,
            CharacterIndex = (short)Validated.CharacterIndex,
            Gender = (short)Validated.Gender,
            Model = Validated.Model,
            Map = classe.MapId,
            X = classe.X,
            Y = classe.Y,
            ClassCode = (short)classe.Id,
            Level = classe.Level,
            Experience = classe.Experience,
            Points = classe.AttributePoint,
            MaximumWarehouse = (short)Configuration.Player.InitialWarehouseSize,
            MaximumInventories = (short)Configuration.Player.InitialInventorySize
        };
    }

    public IList<CharacterEquipment> CreateEquipments(Character character) {
        var classe = Validated.CharacterClass;
        var list = new List<CharacterEquipment>();

        for (var i = 0; i < classe.Equipments.Count; ++i) {
            var equipment = classe.Equipments[i];

            if (equipment.Id > 0) {
                list.Add(new CharacterEquipment() {
                    CharacterId = character.CharacterId,
                });
            }
        }

        return list;
    }

    public IList<CharacterInventory> CreateInventories(Character character) {
        var classe = Validated.CharacterClass;
        var list = new List<CharacterInventory>();

        for (var i = 0; i < classe.Inventories.Count; ++i) {
            var inventory = classe.Inventories[i];

            if (inventory.Id > 0) {
                list.Add(new CharacterInventory() {
                    CharacterId = character.CharacterId,
                    InventoryIndex = i + 1,
                    ItemId = inventory.Id,
                    Value = inventory.Value,
                    Level = inventory.Level,
                    Bound = inventory.Bound,
                    AttributeId = inventory.AttributeId,
                    UpgradeId = inventory.UpgradeId,
                    UniqueSerial = string.Empty,
                });
            }
        }
        return list;
    }

    public IList<CharacterPassive> CreatePassives(Character character) {
        var classe = Validated.CharacterClass;
        var list = new List<CharacterPassive>();

        for (var i = 0; i < classe.Passives.Count; ++i) {
            var passive = classe.Passives[i];

            if (passive.Id > 0) {
                list.Add(new CharacterPassive() {
                    CharacterId = character.CharacterId,
                    PassiveId = passive.Id,
                    PassiveLevel = passive.Level
                });
            }
        }

        return list;
    }

    public IList<CharacterSkill> CreateSkills(Character character) {
        var classe = Validated.CharacterClass;
        var list = new List<CharacterSkill>();

        for (var i = 0; i < classe.Skills.Count; ++i) {
            var skill = classe.Skills[i];

            if (skill.Id > 0) {
                list.Add(new CharacterSkill() {
                    CharacterId = character.CharacterId,
                    SkillId = skill.Id,
                    SkillLevel = skill.Level
                });
            }
        }

        return list;
    }
}
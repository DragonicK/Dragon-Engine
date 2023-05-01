using Dragon.Core.Services;
using Dragon.Core.Model.Classes;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Services;

namespace Dragon.Game.Characters;

public sealed class CharacterCreator : ICharacterCreator {
    public ConfigurationService? Configuration { get; private set; }

    public CharacterCreator(IServiceInjector injector) {
        injector.Inject(this);
    }

    public IList<CharacterSkill> CreateSkills(IClass classJob, long characterId) {
        var list = new List<CharacterSkill>(classJob.Skills.Count);

        foreach (var skill in classJob.Skills) {
            if (skill.Id > 0) {
                list.Add(new CharacterSkill() {
                    SkillId = skill.Id,
                    SkillLevel = skill.Level,
                    CharacterId = characterId
                });
            }
        }

        return list;
    }

    public IList<CharacterPassive> CreatePassives(IClass classJob, long characterId) {
        var list = new List<CharacterPassive>(classJob.Passives.Count);

        foreach (var passive in classJob.Passives) {
            if (passive.Id > 0) {
                list.Add(new CharacterPassive() {
                    PassiveId = passive.Id,
                    CharacterId = characterId,
                    PassiveLevel = passive.Level
                });
            }
        }

        return list;
    }

    public IList<CharacterEquipment> CreateEquipments(IClass classJob, long characterId) {
        var list = new List<CharacterEquipment>();
 
        foreach (var equipment in classJob.Equipments) { 
            if (equipment.Id > 0) {
                list.Add(new CharacterEquipment() {
                    CharacterId = characterId,
                    InventoryIndex = equipment.InventoryIndex,
                    ItemId = equipment.Id,
                    Value = equipment.Value,
                    Level = equipment.Level,
                    Bound = equipment.Bound,
                    AttributeId = equipment.AttributeId,
                    UpgradeId = equipment.UpgradeId,
                    UniqueSerial = string.Empty,
                });
            }
        }

        return list;
    }

    public IList<CharacterInventory> CreateInventories(IClass classJob, long characterId) {
        var list = new List<CharacterInventory>();
        var index = 0;

        foreach (var inventory in classJob.Inventories) { 
            if (inventory.Id > 0) {
                list.Add(new CharacterInventory() {
                    CharacterId = characterId,
                    InventoryIndex = ++index,
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

    public Character CreateCharacter(IPlayer player, IClass classJob, int model, string name, int gender, int index) {
        return new Character {
            Name = name,
            Model = model,
            X = classJob.X,
            Y = classJob.Y,
            Map = classJob.MapId,
            Level = classJob.Level,
            Gender = (short)gender,
            AccountId = player.AccountId,
            CharacterIndex = (short)index,
            ClassCode = (short)classJob.Id,
            Experience = classJob.Experience,
            Points = classJob.AttributePoint,
            MaximumWarehouse = (short)Configuration!.Player.InitialWarehouseSize,
            MaximumInventories = (short)Configuration!.Player.InitialInventorySize
        };
    }
}
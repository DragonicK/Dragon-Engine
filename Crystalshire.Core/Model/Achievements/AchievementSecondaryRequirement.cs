namespace Crystalshire.Core.Model.Achievements;

[Flags]
public enum AchievementSecondaryRequirement : long {
    None,
    AcquireItem, // = 0x1,
    AcquireCurrency, // = 0x2,
    DestroyNpc, // = 0x4,
    DestroyObject, // = 0x8,
    DestroyPlayer, // = 0x10,
    EquipItemById, // = 0x20,
    EquipItemByLevel, // = 0x40,
    EquipItemRarity, // = 0x80,
    EquipItemByType, // = 0x100,
    InstanceEnter, // = 0x200,
    InstanceCompleted, // = 0x400,
    ItemUpgradeByFailed, // = 0x800,
    ItemUpgradeById, // = 0x1000,
    ItemUpgradeByLevel, // = 0x2000,
    ItemUpgradeByRarity, // = 0x4000,
    ItemUpgradeByType, // = 0x8000,
    LevelUpByCharacter, // = 0x10000,
    LevelUpBySkill, // = 0x20000,
    LevelUpByParty, // = 0x40000,
    LevelUpByCraft, // = 0x80000,
    QuestDoneById, // = 0x100000,
    UseItemById // = 0x200000
}
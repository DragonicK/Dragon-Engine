using Crystalshire.Network;

using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Entity;
using Crystalshire.Core.Model.Classes;
using Crystalshire.Core.Model.Accounts;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players;

public interface IPlayer {
    Account Account { get; set; }
    long AccountId { get; set; }
    string Username { get; set; }
    string MachineId { get; set; }
    string IpAddress { get; set; }
    AccountLevel AccountLevel { get; set; }
    int IndexOnInstance { get; set; }
    bool IsWarehouseOpen { get; set; }
    int TradeId { get; set; }
    int PartyId { get; set; }
    int PartyInvitedId { get; set; }
    int Cash { get; set; }
    int ShopId { get; set; }
    bool InGame { get; set; }
    IEntityAttribute Attributes { get; set; }
    IEntityVital Vitals { get; set; }
    IEntityCombat Combat { get; set; }
    IEntity? Target { get; set; }
    TargetType TargetType { get; set; }
    IList<CharacterPreview> Characters { get; set; }
    Character Character { get; set; }
    IClass Class { get; set; }
    IPlayerAchievement Achievements { get; set; }
    IPlayerPrimaryAttribute PrimaryAttributes { get; set; }
    IPlayerAura Auras { get; set; }
    IPlayerCurrency Currencies { get; set; }
    IPlayerHeraldry Heraldries { get; set; }
    IPlayerMail Mails { get; set; }
    IPlayerQuickSlot QuickSlots { get; set; }
    IPlayerService Services { get; set; }
    IPlayerTitle Titles { get; set; }
    IPlayerPet Pets { get; set; }
    IPlayerAppearance Appearance { get; set; }
    IPlayerCraft Craft { get; set; }
    IPlayerSettings Settings { get; set; }
    IPlayerEquipment Equipments { get; set; }
    IPlayerInventory Inventories { get; set; }
    IEntityEffect Effects { get; set; }
    IPlayerAchievementProgress AchievementsProgress { get; set; }
    IPlayerWarehouse Warehouse { get; set; }
    IPlayerPassive Passives { get; set; }
    IPlayerRecipe Recipes { get; set; }
    IPlayerSkill Skills { get; set; }
    IConnection GetConnection();
    IEntityAttribute GetAttributes();
    void AllocateAttributes();
}
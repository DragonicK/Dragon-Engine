using Crystalshire.Network;

using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Entity;
using Crystalshire.Core.Model.Classes;
using Crystalshire.Core.Model.Accounts;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players;

public class Player : IEntity, IPlayer {
    public int Id { get; set; }
    public Account Account { get; set; }
    public long AccountId { get; set; }
    public string Username { get; set; }
    public string MachineId { get; set; }
    public string IpAddress { get; set; }
    public AccountLevel AccountLevel { get; set; }
    public IClass Class { get; set; }
    public int IndexOnInstance { get; set; }
    public bool IsWarehouseOpen { get; set; }
    public int TradeId { get; set; }
    public int PartyId { get; set; }
    public int PartyInvitedId { get; set; }
    public int Cash { get; set; }
    public int ShopId { get; set; }
    public bool InGame { get; set; }
    public IConnection Connection { get; set; }
    public IEntityAttribute Attributes { get; set; }
    public IEntityVital Vitals { get; set; }
    public IEntityCombat Combat { get; set; }
    public IEntity? Target { get; set; }
    public TargetType TargetType { get; set; }
    public IList<CharacterPreview> Characters { get; set; }
    public Character Character { get; set; }
    public IPlayerAchievement Achievements { get; set; }
    public IPlayerPrimaryAttribute PrimaryAttributes { get; set; }
    public IPlayerAura Auras { get; set; }
    public IPlayerCurrency Currencies { get; set; }
    public IPlayerHeraldry Heraldries { get; set; }
    public IPlayerMail Mails { get; set; }
    public IPlayerQuickSlot QuickSlots { get; set; }
    public IPlayerService Services { get; set; }
    public IPlayerTitle Titles { get; set; }
    public IPlayerPet Pets { get; set; }
    public IPlayerAppearance Appearance { get; set; }
    public IPlayerCraft Craft { get; set; }
    public IPlayerSettings Settings { get; set; }
    public IPlayerEquipment Equipments { get; set; }
    public IPlayerInventory Inventories { get; set; }
    public IEntityEffect Effects { get; set; }
    public IPlayerAchievementProgress AchievementsProgress { get; set; }
    public IPlayerWarehouse Warehouse { get; set; }
    public IPlayerPassive Passives { get; set; }
    public IPlayerRecipe Recipes { get; set; }
    public IPlayerSkill Skills { get; set; }

    public Player(string username, long accountId, int accessLevel, IConnection connection) {
        Username = username;
        AccountId = accountId;
        AccountLevel = (AccountLevel)accessLevel;
        MachineId = string.Empty;
        Connection = connection;
        IpAddress = connection.IpAddress;
        Attributes = new EntityAttribute();
    }

    #region Entity

    public IEntityAttribute GetAttributes() {
        return Attributes;
    }

    public IConnection GetConnection() {
        return Connection;
    }

    public void AllocateAttributes() {
        Attributes.Clear();

        Attributes.Add(PrimaryAttribute.Strength, PrimaryAttributes.Get(PrimaryAttribute.Strength));
        Attributes.Add(PrimaryAttribute.Agility, PrimaryAttributes.Get(PrimaryAttribute.Agility));
        Attributes.Add(PrimaryAttribute.Constitution, PrimaryAttributes.Get(PrimaryAttribute.Constitution));
        Attributes.Add(PrimaryAttribute.Intelligence, PrimaryAttributes.Get(PrimaryAttribute.Intelligence));
        Attributes.Add(PrimaryAttribute.Spirit, PrimaryAttributes.Get(PrimaryAttribute.Spirit));
        Attributes.Add(PrimaryAttribute.Will, PrimaryAttributes.Get(PrimaryAttribute.Will));

        Attributes.Add(Titles.Attributes);
        Attributes.Add(Passives.Attributes);
        Attributes.Add(Heraldries.Attributes);
        Attributes.Add(Equipments.Attributes);
        Attributes.Add(Effects.Attributes);
        Attributes.Add(Achievements.Attributes);

        Attributes.Calculate(Character.Level, Class);

        Vitals.SetMaximum(Vital.HP, Attributes.Get(Vital.HP));
        Vitals.SetMaximum(Vital.MP, Attributes.Get(Vital.MP));
        Vitals.SetMaximum(Vital.Special, Attributes.Get(Vital.Special));
    }

    public int GetX() {
        return Character.X;
    }

    public int GetY() {
        return Character.Y;
    }

    #endregion


}
using Dragon.Core.Logs;
using Dragon.Core.Model;
using Dragon.Core.Model.Classes;

using Dragon.Game.Combat;
using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Core.Services;
using Dragon.Game.Repository;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Server;

public sealed class JoinGame {
    public LoggerService? LoggerService { get; private set; }
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private readonly SkillManager SkillManager;
    private readonly WarperManager WarperManager;
    private readonly PartyReconnectManager PartyManager;

    public JoinGame(IServiceContainer services) {
        new ServiceInjector(services).Inject(this);

        SkillManager = new SkillManager(services);
        WarperManager = new WarperManager(services);
        PartyManager = new PartyReconnectManager(services);
    }

    public void Join(IPlayer player) {
        var logger = GetLogger();
        var sender = GetPacketSender();

        player!.Combat = new PlayerCombat(player, Configuration!, GetPacketSender(), ContentService!, InstanceService!) {
            PlayerRepository = ConnectionService!.PlayerRepository
        };

        #region Titles

        player.Titles.Titles = ContentService!.Titles;
        player.Titles.TitleAttributes = ContentService!.TitleAttributes;

        var titleId = player.Character.TitleId;

        if (titleId > 0) {
            player.Titles.Equip(titleId);
        }

        #endregion

        #region Inventories

        player.Inventories.Items = ContentService!.Items;

        #endregion

        #region Warehouse

        player.Warehouse.Items = ContentService!.Items;

        #endregion

        #region Equipments

        player.Equipments.Items = ContentService!.Items;
        player.Equipments.Equipments = ContentService!.Equipments;
        player.Equipments.EquipmentAttributes = ContentService!.EquipmentAttributes;
        player.Equipments.EquipmentUpgrades = ContentService!.EquipmentUpgrades;
        player.Equipments.EquipmentSets = ContentService!.EquipmentSets;
        player.Equipments.EquipmentSetAttributes = ContentService!.EquipmentSetAttributes;

        player.Equipments.UpdateAttributes();

        #endregion

        #region Heraldries

        player.Heraldries.Items = ContentService!.Items;
        player.Heraldries.Heraldries = ContentService!.Heraldries;
        player.Heraldries.HeraldryAttributes = ContentService!.HeraldryAttributes;
        player.Heraldries.HeraldryUpgrades = ContentService!.HeraldryUpgrades;

        player.Heraldries.UpdateAttributes();

        #endregion

        #region Effects

        player.Effects.Effects = ContentService!.Effects;
        player.Effects.EffectAttributes = ContentService!.EffectAttributes;
        player.Effects.EffectUpgrades = ContentService!.EffectUpgrades;

        player.Effects.UpdateAttributes();

        #endregion

        #region Skills & Passives

        SkillManager.AllocateSkills(player);
        SkillManager.AllocatePassives(player);

        player.Passives.Skills = ContentService!.Skills;
        player.Passives.Passives = ContentService!.Passives;
        player.Passives.PassiveAttributes = ContentService!.PassiveAttributes;
        player.Passives.PassiveUpgrades = ContentService!.PassiveUpgrades;

        player.Passives.UpdateAttributes();

        #endregion

        #region Class

        var jobClass = GetClass(player);

        if (jobClass is not null) {
            player.Class = jobClass;
            player.AllocateAttributes();
        }

        #endregion

        #region Costume

        UpdateCostume(player);

        #endregion

        sender.SendServerConfiguration(player);
        sender.SendServerRates(player);
        sender.SendSettings(player);

        WarpToInstance(player);

        #region Services

        player.Services.Premiums = ContentService!.Premiums;
        player.Services.AllocateRates();

        SendServices(player);

        #endregion

        SendExperience(sender, player);

        player.AllocateAttributes();

        sender.SendWarehouse(player);
        sender.SendCash(player);
        sender.SendMails(player);
        sender.SendSkills(player);
        sender.SendPassives(player);
        sender.SendCurrency(player);
        sender.SendQuickSlot(player);
        sender.SendCraftData(player);
        sender.SendRecipes(player);
        sender.SendHeraldry(player);
        sender.SendEquipment(player);
        sender.SendAttributes(player);
        sender.SendInventory(player);
        sender.SendTitles(player);

        ReJoinParty(player);

        sender!.SendInGame(player);

        player.InGame = true;

        logger.Info(GetType().Name, $"{player!.Username} Joined Game");
    }

    private IClass? GetClass(IPlayer player) {
        var code = player.Character.ClassCode;
        var database = ContentService!.Classes;

        if (database.Contains(code)) {
            return database[code];
        }

        return null;
    }

    private void SendExperience(IPacketSender sender, IPlayer player) {
        var level = player.Character.Level;
        var experience = ContentService!.PlayerExperience;

        var maximum = 0;

        if (level > 0) {
            if (level <= experience.MaximumLevel) {
                maximum = experience.Get(level);
            }
        }

        var minimum = player!.Character.Experience;

        if (minimum >= maximum) {
            minimum = maximum;

            player.Character.Experience = minimum;
        }

        sender.SendExperience(player!, minimum, maximum);
    }

    private void UpdateCostume(IPlayer player) {
        var equipments = ContentService!.Equipments;
        var inventory = player!.Equipments.Get(PlayerEquipmentType.Costume);

        if (equipments is not null) {
            if (inventory is not null) {
                if (equipments.Contains(inventory.ItemId)) {
                    var equipment = equipments[inventory.ItemId];
                    player.Character.CostumeModel = equipment!.ModelId;
                }
            }
        }
    }

    private void ReJoinParty(IPlayer player) {
        PartyManager.Reconnect(player);
    }

    private void SendServices(IPlayer player) {
        if (ContentService is not null) {
            var premiums = ContentService.Premiums;

            if (premiums is not null) {
                var services = player.Services.ToArray();

                foreach (var service in services) {
                    if (!service.Expired && service.ServiceId > 0) {
                        if (premiums.Contains(service.ServiceId)) {
                            var premium = premiums[service.ServiceId]!;

                            var date = service.EndTime;
                            var text = $"{date.ToShortDateString()} {date.ToShortTimeString()}";

                            GetPacketSender().SendPremiumService(player, premium, text);
                        }
                    }
                }
            }
        }
    }

    private void WarpToInstance(IPlayer player) {
        var instanceId = player.Character.Map;
        var x = player.Character.X;
        var y = player.Character.Y;

        if (InstanceService!.Instances.ContainsKey(instanceId)) {
            var instance = InstanceService!.Instances[instanceId];

            WarperManager.Warp(player, instance, x, y);
        }
    }

    private ILogger GetLogger() {
        return LoggerService!.Logger!;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}
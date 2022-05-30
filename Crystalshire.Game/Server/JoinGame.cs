using Crystalshire.Core.Logs;
using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Classes;

using Crystalshire.Game.Combat;
using Crystalshire.Game.Manager;
using Crystalshire.Game.Network;
using Crystalshire.Game.Players;
using Crystalshire.Game.Services;
using Crystalshire.Game.Configurations;

namespace Crystalshire.Game.Server;

public class JoinGame {
    public ILogger? Logger { get; init; }
    public IPlayer? Player { get; init; }
    public IConfiguration? Configuration { get; init; }
    public IPacketSender? PacketSender { get; init; }
    public InstanceService? InstanceService { get; init; }
    public ContentService? ContentService { get; init; }

    public void Join() {
        Player!.Combat = new PlayerCombat(Player, PacketSender!, ContentService!, InstanceService!);

        #region Titles

        Player!.Titles.Titles = ContentService!.Titles;
        Player!.Titles.TitleAttributes = ContentService!.TitleAttributes;

        var titleId = Player.Character.TitleId;

        if (titleId > 0) {
            Player.Titles.Equip(titleId);
        }

        #endregion

        #region Inventories

        Player!.Inventories.Items = ContentService!.Items;

        #endregion

        #region Warehouse

        Player!.Warehouse.Items = ContentService!.Items;

        #endregion

        #region Equipments

        Player!.Equipments.Items = ContentService!.Items;
        Player!.Equipments.Equipments = ContentService!.Equipments;
        Player!.Equipments.EquipmentAttributes = ContentService!.EquipmentAttributes;
        Player!.Equipments.EquipmentUpgrades = ContentService!.EquipmentUpgrades;
        Player!.Equipments.EquipmentSets = ContentService!.EquipmentSets;
        Player!.Equipments.EquipmentSetAttributes = ContentService!.EquipmentSetAttributes;

        Player!.Equipments.UpdateAttributes();

        #endregion

        #region Heraldries

        Player!.Heraldries.Items = ContentService!.Items;
        Player!.Heraldries.Heraldries = ContentService!.Heraldries;
        Player!.Heraldries.HeraldryAttributes = ContentService!.HeraldryAttributes;
        Player!.Heraldries.HeraldryUpgrades = ContentService!.HeraldryUpgrades;

        Player!.Heraldries.UpdateAttributes();

        #endregion

        #region Effects

        Player!.Effects.Effects = ContentService!.Effects;
        Player!.Effects.EffectAttributes = ContentService!.EffectAttributes;
        Player!.Effects.EffectUpgrades = ContentService!.EffectUpgrades;

        Player!.Effects.UpdateAttributes();

        #endregion

        #region Skills & Passives

        var manager = new SkillManager() {
            Player = Player,
            Passives = ContentService.Passives,
            Skills = ContentService.Skills
        };

        manager.AllocateSkills();
        manager.AllocatePassives();

        Player!.Passives.Skills = ContentService!.Skills;
        Player!.Passives.Passives = ContentService!.Passives;
        Player!.Passives.PassiveAttributes = ContentService!.PassiveAttributes;
        Player!.Passives.PassiveUpgrades = ContentService!.PassiveUpgrades;

        Player!.Passives.UpdateAttributes();

        #endregion

        #region Class

        var _class = GetClass();

        if (_class is not null) {
            Player!.Class = _class;
            Player.AllocateAttributes();
        }

        #endregion

        #region Costume

        UpdateCostume();

        #endregion

        PacketSender!.SendServerConfiguration(Player!);
        PacketSender!.SendServerRates(Player);
        PacketSender!.SendSettings(Player!);

        WarpToInstance();

        #region Services

        Player.Services.Premiums = ContentService!.Premiums;

        Player.Services.AllocateRates();

        SendServices();

        #endregion

        SendExperience();

        PacketSender!.SendWarehouse(Player);
        PacketSender!.SendCash(Player);
        PacketSender!.SendMails(Player);
        PacketSender!.SendSkills(Player);
        PacketSender!.SendPassives(Player);
        PacketSender!.SendCurrency(Player);
        PacketSender!.SendQuickSlot(Player);
        PacketSender!.SendCraftData(Player);
        PacketSender!.SendRecipes(Player);
        PacketSender!.SendHeraldry(Player);
        PacketSender!.SendEquipment(Player);
        PacketSender!.SendAttributes(Player);
        PacketSender!.SendInventory(Player);
        PacketSender!.SendTitles(Player);

        ReJoinParty();

        PacketSender!.SendInGame(Player);

        Player.InGame = true;
    }

    private IClass? GetClass() {
        var code = Player!.Character.ClassCode;
        var database = ContentService!.Classes;

        if (database.Contains(code)) {
            return database[code];
        }

        return null;
    }

    private void SendExperience() {
        var level = Player!.Character.Level;
        var experience = ContentService!.PlayerExperience;

        var maximum = 0;

        if (level > 0) {
            if (level <= experience.MaximumLevel) {
                maximum = experience.Get(level);
            }
        }

        var minimum = Player!.Character.Experience;

        if (minimum >= maximum) {
            minimum = maximum;

            Player!.Character.Experience = minimum;
        }

        PacketSender!.SendExperience(Player!, minimum, maximum);
    }

    private void UpdateCostume() {
        var equipments = ContentService!.Equipments;
        var inventory = Player!.Equipments.Get(PlayerEquipmentType.Costume);

        if (equipments is not null) {
            if (inventory is not null) {
                if (equipments.Contains(inventory.ItemId)) {
                    var equipment = equipments[inventory.ItemId];
                    Player.Character.CostumeModel = equipment!.ModelId;
                }
            }
        }
    }

    private void ReJoinParty() {
        var manager = new PartyReconnectManager() {
            InstanceService = InstanceService,
            PacketSender = PacketSender,
            Player = Player
        };

        manager.Reconnect();
    }

    private void SendServices() {
        if (ContentService is not null) {
            var premiums = ContentService.Premiums;

            if (premiums is not null) {
                var services = Player!.Services.ToArray();

                foreach (var service in services) {
                    if (!service.Expired && service.ServiceId > 0) {

                        if (premiums.Contains(service.ServiceId)) {
                            var premium = premiums[service.ServiceId]!;

                            var date = service.EndTime;

                            var text = $"{date.ToShortDateString()} {date.ToShortTimeString()}";

                            PacketSender!.SendPremiumService(Player, premium, text);
                        }
                    }
                }
            }
        }
    }

    private void WarpToInstance() {
        var instanceId = Player!.Character.Map;
        var x = Player.Character.X;
        var y = Player.Character.Y;

        if (InstanceService!.Instances.ContainsKey(instanceId)) {
            var warper = new WarperManager() {
                InstanceService = InstanceService,
                PacketSender = PacketSender,
                Player = Player
            };

            var instance = InstanceService!.Instances[instanceId];

            warper.Warp(instance, x, y);
        }
    }
}
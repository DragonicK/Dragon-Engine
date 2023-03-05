using Dragon.Core.Logs;

using Dragon.Game.Services;
using Dragon.Game.Configurations;
using Dragon.Game.Players;
using Dragon.Game.Manager;

namespace Dragon.Game.Server;

public class LeftGame {
    public ILogger? Logger { get; init; }
    public IPlayer? Player { get; init; }
    public IConfiguration? Configuration { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }
    public DatabaseService? DatabaseService { get; init; }

    public async void Left() {
        var sender = PacketSenderService!.PacketSender;
        var instances = PacketSenderService!.InstanceService!.Instances;

        if (Player!.Character is not null) {
            var instanceId = Player!.Character.Map;
            var index = Player!.IndexOnInstance;

            if (instances.ContainsKey(instanceId)) {
                var instance = instances[instanceId];
                var removed = instance.Remove(Player);

                if (removed) {
                    sender?.SendPlayerLeft(Player, instance, index);
                    sender?.SendHighIndex(instance);
                }
            }

            var membership = Configuration!.DatabaseMembership;
            var factory = DatabaseService!.DatabaseFactory!;

            var handler = factory.GetMembershipHandler(membership);

            await handler.SaveFullAccountAsync(Player.Account);
            await handler.SaveCharacterAsync(Player.Character);
            await handler.SaveSettings(Player.Settings.GetSettings());
            await handler.SaveCraftAsync(Player.Craft.GetCharacterCraft());
            await handler.SaveCurrencyAsync(Player.Currencies.ToList());
            await handler.SaveInventoryAsync(Player.Inventories.ToList());
            await handler.SaveEquipmentAsync(Player.Equipments.ToList());
            await handler.SaveHeraldryAsync(Player.Heraldries.ToList());
            await handler.SaveWarehouseAsync(Player.Warehouse.ToList());
            await handler.SaveRecipesAsync(Player.Recipes.ToList());
            await handler.SaveQuickSlotAsync(Player.QuickSlots.ToList());
            await handler.SaveEffectsAsync(Player.Effects.ToList());
            await handler.SaveSkillsAsync(Player.Skills.ToList());
            await handler.SavePassivesAsync(Player.Passives.ToList());
            await handler.SaveMailsAsync(Player.Mails.ToList());

            var vitals = (IPlayerVital)Player.Vitals;

            await handler.SaveVitalAsync(vitals.Get());
        }

        DisconnectParty();

        Logger?.Info(GetType().Name, $"{Player!.Username} Left Game");


        // TODO
        //PartyCollectedItem.UpdateDisconnectedPlayer(player);

        //var loot = new LootHandler(player);
        //loot.CloseLoot(0, TargetType.None);

        //if (player.PartyId > 0) {
        //    player.Auras.RemoveAllPartyAuras();
        //}

        //if (player.TradeId > 0) {
        //    var trade = Global.GetTrade(player.TradeId);

        //    if (trade != null) {
        //        trade.SendDisconnectedMessage();
        //        trade.CancelTrade();
        //    }
        //}

        //player.Combat.Clear();
        //player.Combat.Player = null;
    }

    private void DisconnectParty() {
        var party = new PartyDisconnectManager() {
            InstanceService = PacketSenderService!.InstanceService,
            PacketSender = PacketSenderService!.PacketSender,
            Player = Player
        };

        party.ProcessDisconnect();
    }
}
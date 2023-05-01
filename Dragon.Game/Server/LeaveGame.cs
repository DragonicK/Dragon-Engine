using Dragon.Core.Logs;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Manager;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Server;

public sealed class LeaveGame {
    public LoggerService? LoggerService { get; private set; }
    public DatabaseService? DatabaseService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private readonly PartyDisconnectManager PartyManager;

    public LeaveGame(IServiceInjector injector) {
        injector.Inject(this);

        PartyManager = new PartyDisconnectManager(injector);
    }

    public async void Leave(IPlayer? player) {
        var sender = GetPacketSender();
        var instances = GetInstances();

        if (player!.Character is not null) {
            var instanceId = player!.Character.Map;
            var index = player!.IndexOnInstance;

            if (instances.TryGetValue(instanceId, out var instance)) {
                var removed = instance.Remove(player);

                if (removed) {
                    sender?.SendPlayerLeft(player, instance, index);
                    sender?.SendHighIndex(instance);
                }
            }

            var membership = Configuration!.DatabaseMembership;
            var factory = DatabaseService!.DatabaseFactory!;

            var handler = factory.GetMembershipHandler(membership);

            await handler.SaveFullAccountAsync(player.Account);
            await handler.SaveCharacterAsync(player.Character);
            await handler.SaveSettings(player.Settings.GetSettings());
            await handler.SaveCraftAsync(player.Craft.GetCharacterCraft());
            await handler.SaveCurrencyAsync(player.Currencies.ToList());
            await handler.SaveInventoryAsync(player.Inventories.ToList());
            await handler.SaveEquipmentAsync(player.Equipments.ToList());
            await handler.SaveHeraldryAsync(player.Heraldries.ToList());
            await handler.SaveWarehouseAsync(player.Warehouse.ToList());
            await handler.SaveRecipesAsync(player.Recipes.ToList());
            await handler.SaveQuickSlotAsync(player.QuickSlots.ToList());
            await handler.SaveEffectsAsync(player.Effects.ToList());
            await handler.SaveSkillsAsync(player.Skills.ToList());
            await handler.SavePassivesAsync(player.Passives.ToList());
            await handler.SaveMailsAsync(player.Mails.ToList());

            var vitals = (IPlayerVital)player.Vitals;

            await handler.SaveVitalAsync(vitals.Get());

            ExecutePartyLeaveGame(player);
        }

        GetLogger().Info(GetType().Name, $"{player!.Username} Left Game");

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

    private void ExecutePartyLeaveGame(IPlayer player) {
        if (player.PartyId > 0) {
            PartyManager.ProcessDisconnect(player);
        }
    }

    private ILogger GetLogger() {
        return LoggerService!.Logger!;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IDictionary<int, IInstance> GetInstances() {
        return PacketSenderService!.InstanceService!.Instances;
    }
}
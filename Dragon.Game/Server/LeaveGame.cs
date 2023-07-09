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
    public InstanceService? InstanceService { get; private set; }
    public DatabaseService? DatabaseService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private readonly AuraManager AuraManager;
    private readonly PartyDisconnectManager PartyManager;

    public LeaveGame(IServiceInjector injector) {
        injector.Inject(this);

        AuraManager = new AuraManager(injector);
        PartyManager = new PartyDisconnectManager(injector);
    }

    public async void Leave(IPlayer player) {
        var sender = GetPacketSender();
        var instances = GetInstances();

        if (player.Character is not null) {
            var instanceId = player.Character.Map;
            var index = player.IndexOnInstance;

            if (instances.TryGetValue(instanceId, out var instance)) {
                var removed = instance.Remove(player);

                if (removed) {
                    sender.SendPlayerLeft(player, instance, index);
                    sender.SendHighIndex(instance);
                }
            }

            await Save(player);

            ExecuteTradeDecline(player);

            ExecutePartyRemoveAura(player);
            ExecutePartyLeaveGame(player);
        }

        GetLogger().Info(GetType().Name, $"{player!.Username} Left Game");

        // TODO
        //PartyCollectedItem.UpdateDisconnectedPlayer(player);
    }

    private async Task Save(IPlayer player) {
        var membership = Configuration!.DatabaseMembership;
        var factory = DatabaseService!.DatabaseFactory!;

        var handler = factory.GetMembershipHandler(membership);

        await handler.SaveFullAccountAsync(player.Account);
        await handler.SaveCharacterAsync(player.Character);
        await handler.SavePrimaryAttributesAsync(player.PrimaryAttributes.GetPrimaryAttributes());
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
        await handler.SaveTitlesAsync(player.Titles.ToList());

        var vitals = player.Vitals as IPlayerVital;

        if (vitals is not null) {
            await handler.SaveVitalAsync(vitals.Get());
        }

        handler.Dispose();
    }

    private void ExecuteTradeDecline(IPlayer player) {
        if (player.TradeId > 0) {
            var trades = InstanceService!.Trades;

            trades.TryGetValue(player.TradeId, out var trade);

            trade?.Decline();
        }
    }

    private void ExecutePartyLeaveGame(IPlayer player) {
        if (player.PartyId > 0) {
            PartyManager.ProcessDisconnect(player);
        }
    }

    private void ExecutePartyRemoveAura(IPlayer player) {
        if (player.PartyId > 0) {
            if (player.Auras.Count > 0) {
                var auras = player.Auras.ToList();

                for (var i = 0; i < auras.Count; ++i) {
                    var aura = auras[i].Id;

                    if (aura > 0) {
                        player.Auras.Remove(aura);

                        AuraManager.DeactivateAura(player, aura);
                    }
                }
            }
        }
    }

    private ILogger GetLogger() {
        return LoggerService!.Logger!;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IDictionary<int, IInstance> GetInstances() {
        return InstanceService!.Instances;
    }
}
using Dragon.Core.Logs;
using Dragon.Core.Services;

using Dragon.Database.Handler;

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

    private readonly MembershipHandler MembershipHandler;

    private readonly AuraManager AuraManager;
    private readonly PartyDisconnectManager PartyManager;

    public LeaveGame(IServiceInjector injector) {
        injector.Inject(this);

        var membership = Configuration!.DatabaseMembership;
        var factory = DatabaseService!.DatabaseFactory!;

        MembershipHandler = factory.GetMembershipHandler(membership);

        AuraManager = new AuraManager(injector);
        PartyManager = new PartyDisconnectManager(injector);
    }

    public void Leave(IPlayer player) {
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

            Save(player);

            ExecuteTradeDecline(player);

            ExecutePartyRemoveAura(player);
            ExecutePartyLeaveGame(player);
        }

        GetLogger().Info(GetType().Name, $"{player!.Username} Left Game");

        // TODO
        //PartyCollectedItem.UpdateDisconnectedPlayer(player);
    }

    private async void Save(IPlayer player) {
        await MembershipHandler.SaveFullAccountAsync(player.Account);
        await MembershipHandler.SaveCharacterAsync(player.Character);
        await MembershipHandler.SaveSettings(player.Settings.GetSettings());
        await MembershipHandler.SaveCraftAsync(player.Craft.GetCharacterCraft());
        await MembershipHandler.SaveCurrencyAsync(player.Currencies.ToList());
        await MembershipHandler.SaveInventoryAsync(player.Inventories.ToList());
        await MembershipHandler.SaveEquipmentAsync(player.Equipments.ToList());
        await MembershipHandler.SaveHeraldryAsync(player.Heraldries.ToList());
        await MembershipHandler.SaveWarehouseAsync(player.Warehouse.ToList());
        await MembershipHandler.SaveRecipesAsync(player.Recipes.ToList());
        await MembershipHandler.SaveQuickSlotAsync(player.QuickSlots.ToList());
        await MembershipHandler.SaveEffectsAsync(player.Effects.ToList());
        await MembershipHandler.SaveSkillsAsync(player.Skills.ToList());
        await MembershipHandler.SavePassivesAsync(player.Passives.ToList());
        await MembershipHandler.SaveMailsAsync(player.Mails.ToList());

        var vitals = player.Vitals as IPlayerVital;

        if (vitals is not null) {
            await MembershipHandler.SaveVitalAsync(vitals.Get());
        }
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
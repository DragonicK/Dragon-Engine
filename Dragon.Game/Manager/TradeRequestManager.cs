using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Services;
using Dragon.Game.Players;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class TradeRequestManager {
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    public TradeRequestManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void ProcessRequestInvite(IPlayer player, int index) {
        var sender = GetPacketSender();

        var id = player.TradeId;

        if (id == 0) {
            CheckValidPlayer(sender, player, index);
        }
        else {
            var trades = InstanceService!.Trades;

            trades.TryGetValue(id, out var trade);

            if (trade is not null) {
                if (trade.State == TradeState.Waiting) {
                    sender.SendMessage(SystemMessage.YouAreWaitingForConfirmation, QbColor.BrigthRed, player);
                }
                else if (trade.State > TradeState.Waiting) {
                    sender.SendMessage(SystemMessage.YouAreInTrade, QbColor.BrigthRed, player);
                }
            }
        }
    }

    private void CheckValidPlayer(IPacketSender sender, IPlayer starter, int index) {
        var instances = InstanceService!.Instances;
        var instanceId = starter.Character.Map;

        if (instances.ContainsKey(instanceId)) {
            var instance = instances[instanceId];
            if (index != starter.IndexOnInstance) {
                var invited = instance.GetPlayer(index);

                SendInvite(sender, starter, invited);
            }
        }
    }

    private void SendInvite(IPacketSender sender, IPlayer starter, IPlayer? invited) {
        if (invited is not null) {
            if (invited.TradeId > 0) {
                sender.SendMessage(SystemMessage.TheTargetIsInAnotherTrade, QbColor.BrigthRed, starter);
            }
            else {
                var id = InstanceService!.RegisterTrade(starter, invited);

                if (id > 0) {
                    starter.TradeId = id;
                    invited.TradeId = id;

                    sender.SendTradeInvite(starter, invited);
                }
            }
        }
        else {
            sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.BrigthRed, starter);
        }
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}
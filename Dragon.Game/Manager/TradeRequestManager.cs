using Dragon.Core.Model;

using Dragon.Game.Network;
using Dragon.Game.Services;
using Dragon.Game.Players;

namespace Dragon.Game.Manager;

public class TradeRequestManager {
    public InstanceService? InstanceService { get; init; }
    public IPacketSender? PacketSender { get; init; }

    public void ProcessRequestInvite(int index, IPlayer player) {
        var id = player.TradeId;

        if (id == 0) {
            CheckValidPlayer(index, player);
        }
        else {
            var trades = InstanceService!.Trades;

            if (trades.ContainsKey(id)) {
                var trade = trades[id];

                if (trade.State == TradeState.Waiting) {
                    PacketSender!.SendMessage(SystemMessage.YouAreWaitingForConfirmation, QbColor.BrigthRed, player);
                }
                else if (trade.State > TradeState.Waiting) {
                    PacketSender!.SendMessage(SystemMessage.YouAreInTrade, QbColor.BrigthRed, player);
                }
            }
        }
    }

    private void CheckValidPlayer(int index, IPlayer starter) {
        var instances = InstanceService!.Instances;
        var instanceId = starter.Character.Map;

        if (instances.ContainsKey(instanceId)) {
            var instance = instances[instanceId];
            if (index != starter.IndexOnInstance) {
                var invited = instance.GetPlayer(index);

                SendInvite(starter, invited);
            }
        }
    }

    private void SendInvite(IPlayer starter, IPlayer? invited) {
        if (invited is not null) {
            if (invited.TradeId > 0) {
                PacketSender!.SendMessage(SystemMessage.TheTargetIsInAnotherTrade, QbColor.BrigthRed, starter);
            }
            else {
                var id = InstanceService!.RegisterTrade(starter, invited);

                if (id > 0) {
                    starter.TradeId = id;
                    invited.TradeId = id;

                    PacketSender!.SendTradeInvite(starter, invited);
                }
            }
        }
        else {
            PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.BrigthRed, starter);
        }
    }
}
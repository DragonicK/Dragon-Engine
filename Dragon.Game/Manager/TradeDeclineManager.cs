using Dragon.Core.Model;

using Dragon.Game.Network;
using Dragon.Game.Services;
using Dragon.Game.Players;

namespace Dragon.Game.Manager;

public class TradeDeclineManager {
    public InstanceService? InstanceService { get; init; }
    public IPacketSender? PacketSender { get; init; }

    public void ProcessDeclineRequest(IPlayer player) {
        var id = player.TradeId;
        var trades = InstanceService!.Trades;

        if (trades.ContainsKey(id)) {
            var trade = trades[id];
            var starter = trade.Starter;

            player.TradeId = 0;
            trade.State = TradeState.Failed;

            if (starter is not null) {
                starter.TradeId = 0;

                PacketSender!.SendMessage(SystemMessage.DeclinedTradeRequest, QbColor.BrigthRed, starter);
            }
        }
    }
}
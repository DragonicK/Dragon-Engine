using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Services;
using Dragon.Game.Players;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class TradeDeclineManager {
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    public TradeDeclineManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void ProcessDeclineRequest(IPlayer player) {
        var id = player.TradeId;
        var trades = InstanceService!.Trades;

        trades.TryGetValue(id, out var trade);

        if (trade is not null) {
            var starter = trade.Starter;

            player.TradeId = 0;
            trade.State = TradeState.Failed;

            if (starter is not null) {
                starter.TradeId = 0;

                GetPacketSender().SendMessage(SystemMessage.DeclinedTradeRequest, QbColor.BrigthRed, starter);
            }
        }
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}
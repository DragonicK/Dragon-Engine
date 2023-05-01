using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class TradeAcceptManager {
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    public TradeAcceptManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void ProcessAcceptRequest(IPlayer player) {
        var trades = InstanceService!.Trades;
        var id = player.TradeId;

        var sender = GetPacketSender();

        trades.TryGetValue(id, out var trade);

        if (trade is not null) {
            var failed = false;

            var starter = trade.Starter;
            var invited = trade.Invited;

            if (starter is null) {
                failed = true;

                ProcessFail(sender, invited);
            }

            if (invited is null) {
                failed = true;

                ProcessFail(sender, starter);
            }

            if (failed) {
                trade.State = TradeState.Failed;
            }
            else {
                trade.State = TradeState.Accpeted;

                SendOpenTrade(sender, trade);
            }
        }
    }

    private static void ProcessFail(IPacketSender sender, IPlayer? player) {
        if (player is not null) {
            sender.SendMessage(SystemMessage.PlayerIsNowDisconnected, QbColor.BrigthRed, player);
            sender.SendCloseTrade(player);

            player.TradeId = 0;
        }
    }

    private static void SendOpenTrade(IPacketSender sender, TradeManager trade) {
        var starter = trade.Starter;
        var invited = trade.Invited;

        var starter_name = $"{starter.Character.Name} Lv. {starter.Character.Level}";
        var invited_name = $"{invited.Character.Name} Lv. {invited.Character.Level}";

        sender.SendOpenTrade(starter, starter_name, invited_name, starter.Character.Name);
        sender.SendOpenTrade(invited, starter_name, invited_name, starter.Character.Name);
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}
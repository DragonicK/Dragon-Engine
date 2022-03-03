using Crystalshire.Core.Model;

using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Manager {
    public class TradeAcceptManager {
        public InstanceService? InstanceService { get; init; }
        public IPacketSender? PacketSender { get; init; }

        public void ProcessAcceptRequest(IPlayer player) {
            var trades = InstanceService!.Trades;
            var id = player.TradeId;

            if (trades.ContainsKey(id)) {
                var trade = trades[id];
                var failed = false;

                var starter = trade.Starter;
                var invited = trade.Invited;

                if (starter is null) {
                    failed = true;

                    ProcessFail(invited);
                }

                if (invited is null) {
                    failed = true;

                    ProcessFail(starter);
                }

                if (failed) {
                    trade.State = TradeState.Failed;
                }
                else {
                    trade.State = TradeState.Accpeted;

                    SendOpenTrade(trade);
                }
            }  
        }

        private void ProcessFail(IPlayer? player) {
            if (player is not null) {
                PacketSender!.SendMessage(SystemMessage.PlayerIsNowDisconnected, QbColor.BrigthRed, player);
                PacketSender!.SendCloseTrade(player);

                player.TradeId = 0;
            }
        }

        private void SendOpenTrade(TradeManager trade) {
            var starter = trade.Starter;
            var invited = trade.Invited;

            var starter_name = $"{starter.Character.Name} Lv. {starter.Character.Level}";
            var invited_name = $"{invited.Character.Name} Lv. {invited.Character.Level}";

            PacketSender!.SendOpenTrade(starter, starter_name, invited_name, starter.Character.Name);
            PacketSender!.SendOpenTrade(invited, starter_name, invited_name, starter.Character.Name);
        } 
    }
}
using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Manager;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Routes {
    public sealed class TradeItem {
        public IConnection? Connection { get; set; }
        public CpTradeItem? Packet { get; set; }
        public ConnectionService? ConnectionService { get; init; }
        public LoggerService? LoggerService { get; init; }
        public InstanceService? InstanceService { get; init; }

        public void Process() {
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var index = Packet!.InventoryIndex;
                    var amount = Packet!.Amount;

                    if (IsValidPacket(player, index, amount)) {
                        var manager = GetTradeManager(player);

                        if (manager is not null) {
                            manager.TradeItem(player, index, amount);   
                        }
                    }
                }
            }
        }

        private bool IsValidPacket(IPlayer player, int index, int amount) {
            if (amount < 0) {
                return false;
            }

            if (index < 1 || index > player.Character.MaximumInventories) {
                return false;
            }

            return true;
        }

        private TradeManager? GetTradeManager(IPlayer player) {
            var id = player.TradeId;
            var trades = InstanceService!.Trades;

            if (trades.ContainsKey(id)) {
                return trades[id];
            }

            return null;
        }
    }
}
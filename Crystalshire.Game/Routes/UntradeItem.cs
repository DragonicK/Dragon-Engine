using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Players;
using Crystalshire.Game.Manager;

namespace Crystalshire.Game.Routes {
    public sealed class UntradeItem {
        public IConnection? Connection { get; set; }
        public CpUntradeItem? Packet { get; set; }
        public ConnectionService? ConnectionService { get; init; }
        public LoggerService? LoggerService { get; init; }
        public InstanceService? InstanceService { get; init; }

        public void Process() {
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var index = Packet!.InventoryIndex;

                    if (IsValidPacket(index)) {
                        var manager = GetTradeManager(player);

                        if (manager is not null) {
                            manager.UntradeItem(player, index);
                        }
                    }
                }
            }
        }

        private bool IsValidPacket(int index) {
            return index >= 1;
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

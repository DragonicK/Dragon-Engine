using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Services;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

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
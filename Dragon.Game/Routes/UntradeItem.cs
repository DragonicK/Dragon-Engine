using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Services;
using Dragon.Game.Players;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

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
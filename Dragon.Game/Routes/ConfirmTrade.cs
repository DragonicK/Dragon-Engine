using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Services;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;
public sealed class ConfirmTrade {
    public IConnection? Connection { get; set; }
    public CpConfirmTrade? Packet { get; set; }
    public PacketSenderService? PacketSenderService { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public LoggerService? LoggerService { get; init; }
    public InstanceService? InstanceService { get; init; }

    public void Process() {
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                var manager = GetTradeManager(player);

                if (manager is not null) {
                    manager.Confirm(player);
                }
            }
        }
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
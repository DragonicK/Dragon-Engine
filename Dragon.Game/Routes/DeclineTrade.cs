using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Game.Network;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class DeclineTrade : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.DeclineTrade;

    public DeclineTrade(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var player = GetPlayerRepository().FindByConnectionId(connection.Id);

        if (player is not null) {
            var manager = GetTradeManager(player);

            if (manager is not null) {
                manager.Decline();
            }
        }
    }

    private TradeManager? GetTradeManager(IPlayer player) {
        var id = player.TradeId;
        var trades = InstanceService!.Trades;

        trades.TryGetValue(id, out var trade);
 
        return trade;
    }
}
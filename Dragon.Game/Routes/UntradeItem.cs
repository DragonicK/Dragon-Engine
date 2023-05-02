using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Network;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class UntradeItem : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.UntradeItem;

    public UntradeItem(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpUntradeItem;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                var index = received.InventoryIndex;

                if (IsValidPacket(index)) {
                    GetTradeManager(player)?.UntradeItem(player, index);
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

        trades.TryGetValue(id, out var trade);

        return trade;
    }
}
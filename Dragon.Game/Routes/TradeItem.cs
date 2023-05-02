using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class TradeItem : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.TradeItem;

    public TradeItem(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpTradeItem;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }

    private void Execute(IPlayer player, CpTradeItem packet) {
        if (IsValidPacket(player, packet.InventoryIndex, packet.Amount)) {
            var index = packet.InventoryIndex;
            var amount = packet.Amount;

            GetTradeManager(player)?.TradeItem(player, index, amount);
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

        trades.TryGetValue(id, out var trade);

        return trade;
    }
}
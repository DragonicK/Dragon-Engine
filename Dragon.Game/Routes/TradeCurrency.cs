using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class TradeCurrency : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.TradeCurrency;

    public TradeCurrency(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var received = packet as PacketTradeCurrency;
        
        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }

    private void Execute(IPlayer player, PacketTradeCurrency packet) {
        if (IsValidPacket(packet)) {
            GetTradeManager(player)?.TradeCurrency(player, packet.StarterAmount);
        }
    }

    private bool IsValidPacket(PacketTradeCurrency packet) {
        return packet.StarterAmount >= 0;
    }

    private TradeManager? GetTradeManager(IPlayer player) {
        var id = player.TradeId;
        var trades = InstanceService!.Trades;

        trades.TryGetValue(id, out var trade);

        return trade;
    }
}
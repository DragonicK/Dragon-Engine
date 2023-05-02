using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class TradeRequest : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.TradeRequest;

    private readonly TradeRequestManager TradeRequestManager;

    public TradeRequest(IServiceInjector injector) : base(injector) {
        TradeRequestManager = new TradeRequestManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpTradeRequest;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }        
    }

    private void Execute(IPlayer player, CpTradeRequest packet) {
        TradeRequestManager.ProcessRequestInvite(player, packet.Index);
    }
}
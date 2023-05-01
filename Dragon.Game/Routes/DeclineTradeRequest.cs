using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Game.Network;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

public sealed class DeclineTradeRequest : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.DeclineTradeRequest;

    private readonly TradeDeclineManager TradeDeclineManager;

    public DeclineTradeRequest(IServiceInjector injector) : base(injector) {
        TradeDeclineManager = new TradeDeclineManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var player = GetPlayerRepository().FindByConnectionId(connection.Id);

        if (player is not null) {
            TradeDeclineManager.ProcessDeclineRequest(player);
        }
    }
}
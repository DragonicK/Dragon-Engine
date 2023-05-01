using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Game.Network;
using Dragon.Game.Manager;


namespace Dragon.Game.Routes;
   
public sealed class AcceptTradeRequest : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.AcceptTradeRequest;

    private readonly TradeAcceptManager TradeAcceptManager;

    public AcceptTradeRequest(IServiceInjector injector) : base(injector) {
        TradeAcceptManager = new TradeAcceptManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var player = GetPlayerRepository().FindByConnectionId(connection.Id);

        if (player is not null) {
            TradeAcceptManager.ProcessAcceptRequest(player);
        }
    }
}
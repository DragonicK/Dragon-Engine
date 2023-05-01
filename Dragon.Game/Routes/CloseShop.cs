using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class CloseShop : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.ShopClose;

    public CloseShop(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var player = GetPlayerRepository().FindByConnectionId(connection.Id);

        if (player is not null) {
            player.ShopId = 0;
        }
    }
}
using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class CloseWarehouse : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.WarehouseClose;

    public CloseWarehouse(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var player = FindByConnection(connection);

        if (player is not null) {
            player.IsWarehouseOpen = false;
        }
    }
}
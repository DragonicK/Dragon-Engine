using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Network;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class ShopBuyItem : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.ShopBuyItem;

    private readonly ShopManager ShopManager;

    public ShopBuyItem(IServiceInjector injector) : base(injector) {
        ShopManager = new ShopManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpShopBuyItem;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }        
    }

    private void Execute(IPlayer player, CpShopBuyItem packet) {
        if (IsValidPacket(packet)) {
            ShopManager.ProcessBuyRequest(player, packet.Index, packet.Amount);
        }
    }

    private bool IsValidPacket(CpShopBuyItem packet) {
        return packet.Index >= 1 && packet.Amount >= 1;
    }
}
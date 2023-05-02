using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Network;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class ShopSellItem : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.ShopSellItem;

    private readonly ShopManager ShopManager;

    public ShopSellItem(IServiceInjector injector) : base(injector) {
        ShopManager = new ShopManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpShopSellItem;

        if (received is not null) {
            var player = GetPlayerRepository().FindByConnectionId(connection.Id);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }

    private void Execute(IPlayer player, CpShopSellItem packet) {
        if (IsValidPacket(packet)) {
            ShopManager.ProcessSellRequest(player, packet.Index, packet.Amount);
        }
    }

    private bool IsValidPacket(CpShopSellItem packet) {
        return packet.Index >= 1 && packet.Amount >= 1;
    }
}
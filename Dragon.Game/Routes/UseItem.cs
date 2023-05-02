using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Players;
using Dragon.Game.Manager;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class UseItem : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.UseItem;

    private readonly ItemManager ItemManager;

    public UseItem(IServiceInjector injector) : base(injector) {
        ItemManager = new ItemManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpUseItem;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                var index = received.InventoryIndex;

                if (IsValidInventory(player, index)) {
                    ItemManager.UseItem(player, index);
                }
            }
        }
    }

    private static bool IsValidInventory(IPlayer player, int index) {
        return index >= 1 && index <= player.Character.MaximumInventories;
    }
}
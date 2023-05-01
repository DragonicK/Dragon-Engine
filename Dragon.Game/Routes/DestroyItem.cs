using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class DestroyItem : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.DestroyItem;

    private readonly ItemManager ItemManager;

    public DestroyItem(IServiceInjector injector) : base(injector) {
        ItemManager = new ItemManager(injector);
    }
 
    public void Process(IConnection connection, object packet) {
        var received = packet as CpDestroyItem;

        if (received is not null) {
            var player = GetPlayerRepository().FindByConnectionId(connection.Id);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }
    
    private void Execute(IPlayer player, CpDestroyItem packet) {
        var index = packet.InventoryIndex;

        if (IsValidInventory(player, index)) {
            ItemManager.DestroyItem(player, index);
        }
    }

    private static bool IsValidInventory(IPlayer player, int index) {
        return index >= 1 && index <= player.Character.MaximumInventories;
    }
}
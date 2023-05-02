using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Network;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class StartUpgrade : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.StartUpgrade;

    private readonly ItemUpgradeManager ItemUpgradeManager;

    public StartUpgrade(IServiceInjector injector) : base(injector) {
        ItemUpgradeManager = new ItemUpgradeManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpStartUpgrade;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }

    private void Execute(IPlayer player, CpStartUpgrade packet) {
        if (IsValidInventory(player, packet)) {
            ItemUpgradeManager.StartUpgrade(player, packet.InventoryIndex);
        }
    }

    private bool IsValidInventory(IPlayer player, CpStartUpgrade packet) {
        var inventoryIndex = packet.InventoryIndex;

        if (inventoryIndex < 1) {
            return false;
        }

        if (inventoryIndex > player.Character.MaximumInventories) {
            return false;
        }

        return true;
    }
}
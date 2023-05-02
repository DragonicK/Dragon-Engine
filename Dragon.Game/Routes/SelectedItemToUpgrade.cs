using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Players;
using Dragon.Game.Manager;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class SelectedItemToUpgrade : PacketRoute, IPacketRoute {
    public MessageHeader Header { get; set; } = MessageHeader.SelectedItemToUpgrade;

    private readonly ItemUpgradeManager ItemUpgradeManager;

    public SelectedItemToUpgrade(IServiceInjector injector) : base(injector) {
        ItemUpgradeManager = new ItemUpgradeManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpSelectedItemToUpgrade;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }

    private void Execute(IPlayer player, CpSelectedItemToUpgrade packet) {
        if (IsValidInventory(player, packet)) {
            ItemUpgradeManager.SendUpgradeData(player, packet.InventoryIndex);
        }
    }

    private bool IsValidInventory(IPlayer player, CpSelectedItemToUpgrade packet) {
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
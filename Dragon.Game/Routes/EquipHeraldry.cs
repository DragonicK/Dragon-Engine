using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Players;
using Dragon.Game.Manager;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class EquipHeraldry : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.EquipHeraldry;

    private readonly HeraldryManager HeraldryManager;

    public EquipHeraldry(IServiceInjector injector) : base(injector) {
        HeraldryManager = new HeraldryManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpEquipHeraldry;

        if (received is not null) { 
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }

    private void Execute(IPlayer player, CpEquipHeraldry packet) {
        if (IsValidInventory(player, packet)) {
            HeraldryManager.EquipHeraldryAtIndex(player, packet.HeraldryIndex, packet.InventoryIndex);
        }
    }

    private bool IsValidInventory(IPlayer player, CpEquipHeraldry packet) {
        var inventoryIndex = packet.InventoryIndex;
        var heraldryIndex = packet.HeraldryIndex;

        if (heraldryIndex < 1 || inventoryIndex < 1) {
            return false;
        }

        if (heraldryIndex > Configuration!.Player.MaximumHeraldries) {
            return false;
        }

        if (inventoryIndex > player.Character.MaximumInventories) {
            return false;
        }

        return true;
    }
}
using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class DepositItem : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.DepositItem;

    private readonly WarehouseManager WarehouseManager;

    public DepositItem(IServiceInjector injector) : base(injector) {
        WarehouseManager = new WarehouseManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpDepositItem;

        if (received is not null) {
            if (received.Amount > 0) {
                var player = GetPlayerRepository().FindByConnectionId(connection.Id);

                if (player is not null) {
                    Execute(player, received);
                }
            }
        }
    }

    private void Execute(IPlayer player, CpDepositItem packet) {
        var index = packet.InventoryIndex;

        if (IsValidInventory(player, index)) {
            WarehouseManager.Deposit(player, index, packet.Amount);
        }
    }

    private static bool IsValidInventory(IPlayer player, int index) {
        return index >= 1 && index <= player.Character.MaximumInventories;
    }
}
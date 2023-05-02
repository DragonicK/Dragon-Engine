using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Manager;
using Dragon.Game.Players;
 
namespace Dragon.Game.Routes;

public sealed class WithdrawItem : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.WithdrawItem;

    private readonly WarehouseManager WarehouseManager;

    public WithdrawItem(IServiceInjector injector) : base(injector) {
        WarehouseManager = new WarehouseManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpWithdrawItem;

        if (received is not null) {
            if (received.Amount <= 0) {
                return;
            }

            var player = FindByConnection(connection);

            if (player is not null) {
                var index = received.WarehouseIndex;

                if (IsValidInventory(player, index)) {
                    WarehouseManager.Withdraw(player, index, received.Amount);
                }
            }
        }
    }

    private static bool IsValidInventory(IPlayer player, int index) {
        return index >= 1 && index <= player.Character.MaximumWarehouse;
    }
}
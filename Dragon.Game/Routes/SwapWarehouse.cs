using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Players;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class SwapWarehouse : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.SwapWarehouse;

    public SwapWarehouse(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpSwapWarehouse;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }

    private void Execute(IPlayer player, CpSwapWarehouse packet) {
        var sender = GetPacketSender();

        var source = packet.OldIndex;
        var destination = packet.NewIndex;

        if (CanSwap(player, source, destination)) {
            player.Warehouse.Swap(source, destination);

            sender.SendWarehouseUpdate(player, source);
            sender.SendWarehouseUpdate(player, destination);
        }
    }

    private bool CanSwap(IPlayer player, int source, int destination) {
        if (source < 1 || destination < 1) {
            return false;
        }

        if (source > player.Character.MaximumWarehouse || destination > player.Character.MaximumWarehouse) {
            return false;
        }

        return true;
    }
}
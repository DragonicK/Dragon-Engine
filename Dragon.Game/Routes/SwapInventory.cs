using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Players;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class SwapInventory : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.SwapInventory;

    public SwapInventory(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpSwapInventory;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }

    private void Execute(IPlayer player, CpSwapInventory packet) {
        var sender = GetPacketSender();

        var source = packet.OldIndex;
        var destination = packet.NewIndex;

        if (CanSwap(player, source, destination)) {
            player.Inventories.Swap(source, destination);

            sender.SendInventoryUpdate(player, source);
            sender.SendInventoryUpdate(player, destination);
        }
    }

    private bool CanSwap(IPlayer player, int source, int destination) {
        if (source < 1 || destination < 1) {
            return false;
        }

        if (source > player.Character.MaximumInventories || destination > player.Character.MaximumInventories) {
            return false;
        }

        return true;
    }
}
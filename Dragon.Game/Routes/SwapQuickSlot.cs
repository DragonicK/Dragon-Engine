using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Players;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class SwapQuickSlot : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.SwapQuickSlot;

    private const int MaximumQuickSlot = 12;

    public SwapQuickSlot(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpSwapQuickSlot;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }

    private void Execute(IPlayer player, CpSwapQuickSlot packet) {
        var sender = GetPacketSender();

        var source = packet.OldIndex;
        var destination = packet.NewIndex;

        if (CanSwap(source, destination)) {
            player.QuickSlots.Swap(source, destination);

            sender.SendQuickSlotUpdate(player, source);
            sender.SendQuickSlotUpdate(player, destination);
        }
    }

    private bool CanSwap(int source, int destination) {
        if (source < 1 || destination < 1) {
            return false;
        }

        if (source > MaximumQuickSlot || destination > MaximumQuickSlot) {
            return false;
        }

        return true;
    }
}
using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Routes;

public sealed class SwapInventory {
    public IConnection? Connection { get; set; }
    public CpSwapInventory? Packet { get; set; }
    public PacketSenderService? PacketSenderService { get; init; }
    public InstanceService? InstanceService { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public LoggerService? LoggerService { get; init; }

    public void Process() {
        var sender = PacketSenderService!.PacketSender;
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                var source = Packet!.OldIndex;
                var destination = Packet!.NewIndex;

                if (CanSwap(player, source, destination)) {
                    player.Inventories.Swap(source, destination);

                    sender?.SendInventoryUpdate(player, source);
                    sender?.SendInventoryUpdate(player, destination);
                }
            }
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
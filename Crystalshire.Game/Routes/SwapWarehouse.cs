using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Routes;

public sealed class SwapWarehouse {
    public IConnection? Connection { get; set; }
    public CpSwapWarehouse? Packet { get; set; }
    public LoggerService? LoggerService { get; init; }
    public ConfigurationService? Configuration { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }

    public void Process() {
        var sender = PacketSenderService!.PacketSender;
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                var source = Packet!.OldIndex;
                var destination = Packet!.NewIndex;

                if (CanSwap(player, source, destination)) {
                    player.Warehouse.Swap(source, destination);

                    sender?.SendWarehouseUpdate(player, source);
                    sender?.SendWarehouseUpdate(player, destination);
                }
            }
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
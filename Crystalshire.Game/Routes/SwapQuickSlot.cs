using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Configurations;

namespace Crystalshire.Game.Routes {
    public sealed class SwapQuickSlot {
        public IConnection? Connection { get; set; }
        public CpSwapQuickSlot? Packet { get; set; }
        public IConfiguration? Configuration { get; init; }
        public PacketSenderService? PacketSenderService { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public LoggerService? LoggerService { get; init; }

        private const int MaximumQuickSlot = 12;

        public void Process() {
            var sender = PacketSenderService!.PacketSender;
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var source = Packet!.OldIndex;
                    var destination = Packet!.NewIndex;

                    if (CanSwap(source, destination)) {
                        player.QuickSlots.Swap(source, destination);

                        sender?.SendQuickSlotUpdate(player, source);
                        sender?.SendQuickSlotUpdate(player, destination);
                    }
                }
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
}

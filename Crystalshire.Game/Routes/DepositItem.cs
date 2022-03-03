using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Manager;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Routes {
    public sealed class DepositItem {
        public IConnection? Connection { get; set; }
        public CpDepositItem? Packet { get; set; }
        public LoggerService? LoggerService { get; init; }
        public ContentService? ContentService { get; init; }
        public ConfigurationService? Configuration { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public PacketSenderService? PacketSenderService { get; init; }

        public void Process() {
            if (Packet!.Amount <= 0) {
                return;
            }

            var sender = PacketSenderService!.PacketSender;
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var index = Packet!.InventoryIndex;

                    if (IsValidInventory(player, index)) {
                        var manager = new WarehouseManager() {
                            Player = player,
                            PacketSender = sender,
                            Items = ContentService!.Items
                        };

                        manager.Deposit(index, Packet!.Amount);
                    }
                }
            }
        }

        private static bool IsValidInventory(IPlayer player, int index) {
            return index >= 1 && index <= player.Character.MaximumInventories;
        }
    }
}
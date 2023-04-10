using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Services;

namespace Dragon.Game.Routes {
    public sealed class TakeItemFromChest {
        public IConnection? Connection { get; set; }
        public CpTakeItemFromChest? Packet { get; set; }
        public ContentService? ContentService { get; init; }
        public InstanceService? InstanceService { get; init; }
        public ConfigurationService? Configuration { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public PacketSenderService? PacketSenderService { get; init; }

        public void Process() {
            if (IsValid()) {
                var repository = ConnectionService!.PlayerRepository;

                if (Connection is not null) {
                    var player = repository!.FindByConnectionId(Connection.Id);

                    if (player is not null) {
                        var manager = new ChestManager() {
                            Player = player,
                            PlayerRepository = repository,
                            Configuration = Configuration,
                            Chests = ContentService!.Chests,
                            Drops = ContentService!.Drops,
                            InstanceService = InstanceService,
                            PacketSender = PacketSenderService!.PacketSender
                        };

                        manager.TakeItem(Packet!.Index);
                    }
                }
            }
        }

        private bool IsValid() {
            return Packet!.Index > 0;
        }
    }
}

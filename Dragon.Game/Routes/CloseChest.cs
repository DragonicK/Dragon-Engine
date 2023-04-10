using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Services;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes {
    public sealed class CloseChest {
        public IConnection? Connection { get; set; }
        public PacketCloseChest? Packet { get; set; }
        public ContentService? ContentService { get; init; }
        public InstanceService? InstanceService { get; init; }
        public ConfigurationService? Configuration { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public PacketSenderService? PacketSenderService { get; init; }

        public void Process() {
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var manager = new ChestManager() {
                        Player = player,
                        Configuration = Configuration,
                        Chests = ContentService!.Chests,
                        Drops = ContentService!.Drops,
                        InstanceService = InstanceService,
                        PacketSender = PacketSenderService!.PacketSender,
                        PlayerRepository = repository
                    };

                    manager.CloseChest();
                }
            }
        }
    }
}
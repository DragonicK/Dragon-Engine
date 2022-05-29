using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Manager;
using Crystalshire.Game.Services;

namespace Crystalshire.Game.Routes {
    public sealed class ReceiveMailItem {
        public IConnection? Connection { get; set; }
        public CpReceiveMailItem? Packet { get; set; }
        public ConnectionService? ConnectionService { get; set; }
        public PacketSenderService? PacketSenderService { get; set; }
        public ContentService? ContentService { get; set; }

        public void Process() {
            var sender = PacketSenderService!.PacketSender;
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {

                    var manager = new ReceiveFromMailManager() {
                        Player = player,
                        PacketSender = sender,
                        ContentService = ContentService
                    };

                    manager.ReceiveItem(Packet!.Id);
                }
            }
        }
    }
}
using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;

namespace Crystalshire.Game.Routes {
    public class UpdateMailReadFlag {
        public IConnection? Connection { get; set; }
        public CpUpdateMailReadFlag? Packet { get; set; }
        public ConnectionService? ConnectionService { get; set; }

        public void Process() {
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    player.Mails.UpdateReadFlag(Packet!.Id);
                }
            }
        }
    }
}
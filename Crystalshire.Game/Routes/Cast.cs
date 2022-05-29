using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;

namespace Crystalshire.Game.Routes {
    public sealed class Cast {
        public IConnection? Connection { get; set; }
        public PacketCast? Packet { get; set; }
        public ConnectionService? ConnectionService { get; init; }

        public void Process() {
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var index = Packet!.Index;

                    if (index > 0) {
                        index--;

                        player.Combat.Cast(index);
                    }
                }
            }
        }
    }
}
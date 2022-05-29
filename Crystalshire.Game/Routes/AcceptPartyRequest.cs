using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Manager;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Routes {
    public sealed class AcceptPartyRequest {
        public IConnection? Connection { get; set; }
        public CpAcceptPartyRequest? Packet { get; set; }
        public PacketSenderService? PacketSenderService { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public LoggerService? LoggerService { get; init; }
        public InstanceService? InstanceService { get; init; }

        public void Process() {
            var sender = PacketSenderService!.PacketSender;
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var party = GetPartyManager(player);

                    if (party is not null) {
                        var manager = new PartyAcceptManager() {
                            PacketSender = sender
                        };

                        manager.ProcessAcceptRequest(party, player);
                    }
                }
            }
        }

        private PartyManager? GetPartyManager(IPlayer player) {
            var id = player.PartyInvitedId;
            var parties = InstanceService!.Parties;

            if (parties.ContainsKey(id)) {
                return parties[id];
            }

            return null;
        }
    }
}

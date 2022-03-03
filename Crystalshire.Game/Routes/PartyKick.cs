using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Players;
using Crystalshire.Game.Manager;
using Crystalshire.Game.Services;

namespace Crystalshire.Game.Routes {
    public sealed class PartyKick {
        public IConnection? Connection { get; set; }
        public CpPartyKick? Packet { get; set; }
        public InstanceService? InstanceService { get; set; }
        public PacketSenderService? PacketSenderService { get; set; }
        public ConnectionService? ConnectionService { get; set; }

        public void Process() {
            var sender = PacketSenderService!.PacketSender;
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var party = GetPartyManager(player);

                    if (party is not null) {

                        var manager = new PartyKickManager() {
                            PacketSender = sender
                        };

                        manager.ProcessKickRequest(party, player, Packet!.MemberIndex);

                    }
                }
            }
        }

        private PartyManager? GetPartyManager(IPlayer player) {
            var id = player.PartyId;
            var parties = InstanceService!.Parties;

            if (parties.ContainsKey(id)) {
                return parties[id];
            }

            return null;
        }
    }
}
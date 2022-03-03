using Crystalshire.Core.Model;
using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Manager;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Routes {
    public sealed class PartyRequest {
        public IConnection? Connection { get; set; }
        public CpPartyRequest? Packet { get; set; }
        public PacketSenderService? PacketSenderService { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public ConfigurationService? Configuration { get; init; }
        public InstanceService? InstanceService { get; init; }
        public LoggerService? LoggerService { get; init; }

        public void Process() {
            var sender = PacketSenderService!.PacketSender;
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var invited = repository!.FindByName(Packet!.Character);

                    if (invited is not null) {
                        var manager = new PartyRequestManager() {
                            InstanceService = InstanceService,
                            Configuration = Configuration,
                            PacketSender = sender
                        };

                        var party = GetPartyManager(player);

                        // Invite to an existent party.
                        if (party is not null) {
                            if (player != invited) {
                                manager.ProcessRequestInvite(party, player, invited);
                            }
                        }
                        // Create a new party.
                        else {
                            if (player != invited) {
                                manager.ProcessRequestInvite(player, invited);
                            }
                        }
                    } 
                    else {
                        sender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.BrigthRed, player);
                    }
                }
            }
        }

        private PartyManager? GetPartyManager(IPlayer player) {
            var id = player.TradeId;
            var parties = InstanceService!.Parties;

            if (parties.ContainsKey(id)) {
                return parties[id];
            }

            return null;
        }
    }
}

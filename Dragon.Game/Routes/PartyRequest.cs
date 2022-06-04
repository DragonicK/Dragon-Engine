using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Model;

using Dragon.Game.Services;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

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
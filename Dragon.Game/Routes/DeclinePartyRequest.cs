using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Services;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class DeclinePartyRequest {
    public IConnection? Connection { get; set; }
    public CpDeclinePartyRequest? Packet { get; set; }
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
                    var manager = new PartyDeclineManager() {
                        PacketSender = sender
                    };

                    manager.ProcessDeclineRequest(party, player);
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
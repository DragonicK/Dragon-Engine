using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Players;
using Dragon.Game.Manager;
using Dragon.Game.Services;

namespace Dragon.Game.Routes;

public sealed class PartyLeave {
    public IConnection? Connection { get; set; }
    public PacketPartyLeave? Packet { get; set; }
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

                    var manager = new PartyLeaveManager() {
                        PacketSender = sender
                    };

                    manager.ProcessLeaveRequest(party, player);
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
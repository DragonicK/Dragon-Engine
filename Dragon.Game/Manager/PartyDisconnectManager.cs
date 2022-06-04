using Dragon.Game.Players;
using Dragon.Game.Parties;
using Dragon.Game.Network;
using Dragon.Game.Services;

namespace Dragon.Game.Manager;

public class PartyDisconnectManager {
    public IPacketSender? PacketSender { get; init; }
    public IPlayer? Player { get; init; }
    public InstanceService? InstanceService { get; init; }

    private const int MinimumPartyMember = 2;

    public void ProcessDisconnect() {
        if (Player is not null) {
            var party = GetPartyManager(Player);

            if (party is not null) {
                var members = party.Members;

                foreach (var member in members) {
                    if (member.Player == Player) {
                        member.Player = null;
                        member.Disconnected = true;
                        member.DisconnectionTimeOut = 0;
                    }
                }

                if (party.IsEverybodyDisconnected()) {
                    party.Disband();
                }

                if (party is not null) {
                    if (party.State != PartyState.Disbanded) {
                        if (members.Count >= MinimumPartyMember) {
                            PacketSender!.SendParty(party);
                        }
                        else {
                            party.Disband();
                        }
                    }
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
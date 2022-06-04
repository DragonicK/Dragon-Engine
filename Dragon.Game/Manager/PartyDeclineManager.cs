using Dragon.Core.Model;

using Dragon.Game.Players;
using Dragon.Game.Network;
using Dragon.Game.Parties;

namespace Dragon.Game.Manager;

public class PartyDeclineManager {
    public IPacketSender? PacketSender { get; init; }

    public void ProcessDeclineRequest(PartyManager party, IPlayer player) {
        var leader = party.GetLeader();

        if (leader is not null) {
            var parameters = new string[] { player.Character.Name };

            PacketSender!.SendMessage(SystemMessage.PlayerDeclinedPartyRequest, QbColor.BrigthRed, leader, parameters);

            if (party.InvitedMembers.Count == 1 && party.State == PartyState.Waiting) {
                party.State = PartyState.Disbanded;

                leader.PartyId = 0;
                leader.PartyInvitedId = 0;
            }
        }

        for (var i = 0; i < party.InvitedMembers.Count; ++i) {
            var member = party.InvitedMembers[i];

            if (member.Player == player) {
                member.CouldBeRemoved = true;
            }
        }

        player.PartyId = 0;
        player.PartyInvitedId = 0;

        PacketSender!.SendMessage(SystemMessage.YouDeclinedPartyRequest, QbColor.BrigthRed, player);
    }
}
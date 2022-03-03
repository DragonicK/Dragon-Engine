using Crystalshire.Core.Model
    ;
using Crystalshire.Game.Players;
using Crystalshire.Game.Network;
using Crystalshire.Game.Parties;

namespace Crystalshire.Game.Manager {
    public class PartyLeaveManager {
        public IPacketSender? PacketSender { get; init; }

        private const int MinimumPartyMember = 2;

        public void ProcessLeaveRequest(PartyManager party, IPlayer player) {
            var members = party.Members;

            var id = player.Character.CharacterId;

            var member = members.FirstOrDefault(p => p.CharacterId == id);

            if (member is not null) {
                if (member.Index == party.LeaderIndex) {
                    // Find next leader.
                    var index = GetNextLeader(party);

                    if (index > 0) {
                        party.LeaderIndex = index;

                        Leave(party, member, player);
                    }
                    else {
                        party.Disband();
                    }
                }
                else {
                    Leave(party, member, player);
                }
            }
        }

        private void Leave(PartyManager party, PartyMember member, IPlayer player) {
            var members = party.Members;

            player.PartyId = 0;
            player.PartyInvitedId = 0;

            members.Remove(member);

            var parameters = new string[] { player.Character.Name };

            foreach (var _member in members) {
                if (_member.Player is not null) {
                    PacketSender!.SendMessage(SystemMessage.PlayerLeftParty, QbColor.BrigthRed, _member.Player, parameters);
                }
            }

            PacketSender!.SendPartyLeave(player);
            PacketSender!.SendMessage(SystemMessage.YouLeftParty, QbColor.BrigthRed, player);

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

        private int GetNextLeader(PartyManager party) {
            var members = party.Members;
            var leader = party.LeaderIndex;

            if (members.Count > MinimumPartyMember) {
                foreach (var member in members) {
                    if (member.Index != leader) {
                        return member.Index;
                    }
                }
            }

            return 0;
        }
    }
}
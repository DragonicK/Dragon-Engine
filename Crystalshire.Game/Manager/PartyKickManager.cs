using Crystalshire.Core.Model;

using Crystalshire.Game.Players;
using Crystalshire.Game.Parties;
using Crystalshire.Game.Network;

namespace Crystalshire.Game.Manager {
    public class PartyKickManager {
        public IPacketSender? PacketSender { get; init; }

        private const int MinimumPartyMember = 2;

        public void ProcessKickRequest(PartyManager party, IPlayer player, int index) {
            var leader = party.GetLeader();

            if (leader == player) {
                var mIndex = party.GetIndex(player);

                if (mIndex != index) {
                    var members = party.Members;

                    PartyMember? kicked = default;

                    foreach (var member in members) {
                        if (member.Index == index) {

                            kicked = member;

                            break;
                        }
                    }

                    Leave(party, kicked);
                }
            }
            else {
                PacketSender!.SendMessage(SystemMessage.YouAreNotLeader, QbColor.BrigthRed, player);
            }
        }    

        private void Leave(PartyManager party, PartyMember? kicked) {
            if (kicked is not null) {
                var members = party.Members;

                members.Remove(kicked);

                if (kicked.Player is not null) {
                    kicked.Player.PartyId = 0;
                    kicked.Player.PartyInvitedId = 0;

                    var parameters = new string[] { kicked.Player.Character.Name };

                    foreach (var member in members) {
                        if (member.Player is not null) {
                            PacketSender!.SendMessage(SystemMessage.PlayerKickedFromParty, QbColor.BrigthRed, member.Player, parameters);
                        }
                    }

                    PacketSender!.SendPartyLeave(kicked.Player);
                    PacketSender!.SendMessage(SystemMessage.YouKickedFromParty, QbColor.BrigthRed, kicked.Player);
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
}
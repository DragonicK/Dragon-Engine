using Crystalshire.Core.Model;
using Crystalshire.Game.Parties;

using Crystalshire.Game.Players;
using Crystalshire.Game.Network;

namespace Crystalshire.Game.Manager {
    public class PartyAcceptManager {
        public IPacketSender? PacketSender { get; init; }

        public void ProcessAcceptRequest(PartyManager party, IPlayer player) {
            var invitation = FindInvitation(party, player);

            if (invitation is not null) {
                party.State = PartyState.Created;

                invitation.CouldBeRemoved = true;

                if (party.Members.Count == 1) {
                    var leader = party.GetLeader();

                    if (leader is not null) {
                        PacketSender!.SendMessage(SystemMessage.PartyCreated, QbColor.BrigthGreen, leader);
                    }
                }

                var index = party.Members.Count + 1;

                party.Members.Add(new PartyMember() {
                    Character = player.Character.Name,
                    CharacterId = player.Character.CharacterId,
                    Model = player.Character.Model,
                    Player = player,
                    Index = index
                });

                player.PartyId = party.Id;
                player.PartyInvitedId = 0;

                PacketSender!.SendMessage(SystemMessage.YouJoinedParty, QbColor.BrigthGreen, player);
                PacketSender!.SendParty(party);

                foreach (var member in party.Members) {
                    if (member.Player is not null) {
                        PacketSender!.SendPartyDisplayIcons(member.Player);
                    }
                }
            }
        }

        private PartyInvitedMember? FindInvitation(PartyManager party, IPlayer player) {
            for (var i = 0; i < party.InvitedMembers.Count; ++i) {
                var member = party.InvitedMembers[i];

                if (member.Player == player) {
                    return member;
                }
            }

            return null;
        }
    }
}
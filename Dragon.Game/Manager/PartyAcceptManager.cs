using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Parties;
using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class PartyAcceptManager {
    public PacketSenderService? PacketSenderService { get; private set; }

    public PartyAcceptManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void ProcessAcceptRequest(Party party, IPlayer player) {
        var sender = GetPacketSender();

        var invitation = FindInvitation(party, player);

        if (invitation is not null) {
            party.State = PartyState.Created;

            invitation.CouldBeRemoved = true;

            if (party.Members.Count == 1) {
                var leader = party.GetLeader();

                if (leader is not null) {
                    sender.SendMessage(SystemMessage.PartyCreated, QbColor.BrigthGreen, leader);
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

            sender.SendMessage(SystemMessage.YouJoinedParty, QbColor.BrigthGreen, player);
            sender.SendParty(party);

            foreach (var member in party.Members) {
                if (member.Player is not null) {
                    sender.SendPartyDisplayIcons(member.Player);
                }
            }
        }
    }

    private static PartyInvitedMember? FindInvitation(Party party, IPlayer player) {
        for (var i = 0; i < party.InvitedMembers.Count; ++i) {
            var member = party.InvitedMembers[i];

            if (member.Player == player) {
                return member;
            }
        }

        return null;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}
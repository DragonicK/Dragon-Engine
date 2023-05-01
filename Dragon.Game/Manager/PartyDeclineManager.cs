using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Parties;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public class PartyDeclineManager {
    public PacketSenderService? PacketSenderService { get; private set; }

    public PartyDeclineManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void ProcessDeclineRequest(PartyManager party, IPlayer player) {
        var sender = GetPacketSender();
        var leader = party.GetLeader();

        if (leader is not null) {
            var parameters = new string[] { player.Character.Name };

            sender.SendMessage(SystemMessage.PlayerDeclinedPartyRequest, QbColor.BrigthRed, leader, parameters);

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

        sender.SendMessage(SystemMessage.YouDeclinedPartyRequest, QbColor.BrigthRed, player);
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}
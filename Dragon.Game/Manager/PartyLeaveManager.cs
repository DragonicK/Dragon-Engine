using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Parties;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class PartyLeaveManager {
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MinimumPartyMember = 2;

    public PartyLeaveManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void ProcessLeaveRequest(PartyManager party, IPlayer player) {
        var sender = GetPacketSender();

        var members = party.Members;

        var id = player.Character.CharacterId;

        var member = members.FirstOrDefault(p => p.CharacterId == id);

        if (member is not null) {
            if (member.Index == party.LeaderIndex) {
                var index = GetNextLeader(party);

                if (index > 0) {
                    party.LeaderIndex = index;

                    Leave(sender, party, member, player);
                }
                else {
                    party.Disband();
                }
            }
            else {
                Leave(sender, party, member, player);
            }
        }
    }

    private void Leave(IPacketSender sender, PartyManager party, PartyMember member, IPlayer player) {
        var members = party.Members;

        player.PartyId = 0;
        player.PartyInvitedId = 0;

        members.Remove(member);

        var parameters = new string[] { player.Character.Name };

        foreach (var partyMember in members) {
            if (partyMember.Player is not null) {
                sender.SendMessage(SystemMessage.PlayerLeftParty, QbColor.BrigthRed, partyMember.Player, parameters);
            }
        }

        sender.SendPartyLeave(player);
        sender.SendMessage(SystemMessage.YouLeftParty, QbColor.BrigthRed, player);

        if (party.IsEverybodyDisconnected()) {
            party.Disband();
        }

        if (party is not null) {
            if (party.State != PartyState.Disbanded) {
                if (members.Count >= MinimumPartyMember) {
                    sender.SendParty(party);
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

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}
using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Parties;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class PartyKickManager {
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MinimumPartyMember = 2;

    public PartyKickManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void ProcessKickRequest(PartyManager party, IPlayer player, int index) {
        var sender = GetPacketSender();
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

                Leave(sender, party, kicked);
            }
        }
        else {
            sender.SendMessage(SystemMessage.YouAreNotLeader, QbColor.BrigthRed, player);
        }
    }

    private void Leave(IPacketSender sender, PartyManager party, PartyMember? kicked) {
        if (kicked is not null) {
            var members = party.Members;

            members.Remove(kicked);

            if (kicked.Player is not null) {
                kicked.Player.PartyId = 0;
                kicked.Player.PartyInvitedId = 0;

                var parameters = new string[] { kicked.Player.Character.Name };

                foreach (var member in members) {
                    if (member.Player is not null) {
                        sender.SendMessage(SystemMessage.PlayerKickedFromParty, QbColor.BrigthRed, member.Player, parameters);
                    }
                }

                sender.SendPartyLeave(kicked.Player);
                sender.SendMessage(SystemMessage.YouKickedFromParty, QbColor.BrigthRed, kicked.Player);
            }

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
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}
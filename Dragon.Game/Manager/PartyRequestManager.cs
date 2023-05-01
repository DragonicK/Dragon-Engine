using Dragon.Core.Model;
using Dragon.Core.Services;
using Dragon.Game.Network.Senders;
using Dragon.Game.Parties;
using Dragon.Game.Players;
using Dragon.Game.Services;

namespace Dragon.Game.Manager;

public class PartyRequestManager {
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    public PartyRequestManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void ProcessRequestInvite(PartyManager party, IPlayer starter, IPlayer invited) {
        var sender = GetPacketSender();

        var leader = party.GetLeader();
        var failed = false;

        if (leader is not null) {
            if (leader != starter) {
                sender.SendMessage(SystemMessage.YouAreNotLeader, QbColor.BrigthRed, starter);

                failed = true;
            }
            else {
                if (invited.PartyInvitedId > 0) {
                    sender.SendMessage(SystemMessage.PlayerIsAlreadyInvited, QbColor.BrigthRed, starter);

                    failed = true;
                }
                else {
                    if (invited.PartyId > 0) {
                        sender.SendMessage(SystemMessage.PlayerIsAlreadyInParty, QbColor.BrigthRed, starter);

                        failed = true;
                    }
                }
            }

            if (!failed) {
                if (party.Members.Count >= Configuration!.Party.MaximumMembers) {
                    sender.SendMessage(SystemMessage.PartyIsFull, QbColor.BrigthRed, starter);
                }
                else {
                    invited.PartyInvitedId = party.Id;

                    party.InvitedMembers.Add(new PartyInvitedMember() {
                        Player = invited
                    });

                    sender.SendPartyInvite(starter, invited);
                }
            }
        }
    }

    public void ProcessRequestInvite(IPlayer starter, IPlayer invited) {
        var sender = GetPacketSender();

        var failed = false;

        if (invited.PartyInvitedId > 0) {
            sender.SendMessage(SystemMessage.PlayerIsAlreadyInvited, QbColor.BrigthRed, starter);

            failed = true;
        }
        else {
            if (invited.PartyId > 0) {
                sender.SendMessage(SystemMessage.PlayerIsAlreadyInParty, QbColor.BrigthRed, starter);

                failed = true;
            }
        }

        if (!failed) {
            var partyId = InstanceService!.RegisterParty();
            var parties = InstanceService.Parties;

            parties.TryGetValue(partyId, out var party);

            if (party is not null) {
                party.Members.Add(new PartyMember() {
                    Index = 1,
                    Player = starter,
                    Model = starter.Character.Model,
                    Character = starter.Character.Name,
                    CharacterId = starter.Character.CharacterId
                });

                party.InvitedMembers.Add(new PartyInvitedMember() {
                    Player = invited
                });

                party.State = PartyState.Waiting;
                party.LeaderIndex = 1;
                party.Level = 1;

                starter.PartyId = partyId;
                invited.PartyInvitedId = partyId;

                sender.SendPartyInvite(starter, invited);
            }
        }
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}
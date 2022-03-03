using Crystalshire.Core.Model;

using Crystalshire.Game.Parties;
using Crystalshire.Game.Network;
using Crystalshire.Game.Players;
using Crystalshire.Game.Services;
using Crystalshire.Game.Configurations;

namespace Crystalshire.Game.Manager {
    public class PartyRequestManager {
        public InstanceService? InstanceService { get; init; }
        public IPacketSender? PacketSender { get; init; }
        public IConfiguration? Configuration { get; init; }

        public void ProcessRequestInvite(PartyManager party, IPlayer starter, IPlayer invited) {
            var leader = party.GetLeader();
            var failed = false;

            if (leader is not null) {
                if (leader != starter) {
                    PacketSender!.SendMessage(SystemMessage.YouAreNotLeader, QbColor.BrigthRed, starter);

                    failed = true;
                }
                else {
                    if (invited.PartyInvitedId > 0) {
                        PacketSender!.SendMessage(SystemMessage.PlayerIsAlreadyInvited, QbColor.BrigthRed, starter);

                        failed = true;
                    }
                    else {
                        if (invited.PartyId > 0) {
                            PacketSender!.SendMessage(SystemMessage.PlayerIsAlreadyInParty, QbColor.BrigthRed, starter);

                            failed = true;
                        }
                    }
                }

                if (!failed) {
                    if (party.Members.Count >= Configuration!.Party.MaximumMembers) {
                        PacketSender!.SendMessage(SystemMessage.PartyIsFull, QbColor.BrigthRed, starter);
                    }
                    else {
                        invited.PartyInvitedId = party.Id;

                        party.InvitedMembers.Add(new PartyInvitedMember() {
                            Player = invited
                        });

                        PacketSender!.SendPartyInvite(starter, invited);
                    }
                }
            }
        }

        public void ProcessRequestInvite(IPlayer starter, IPlayer invited) {
            var failed = false;

            if (invited.PartyInvitedId > 0) {
                PacketSender!.SendMessage(SystemMessage.PlayerIsAlreadyInvited, QbColor.BrigthRed, starter);

                failed = true;
            }
            else {
                if (invited.PartyId > 0) {
                    PacketSender!.SendMessage(SystemMessage.PlayerIsAlreadyInParty, QbColor.BrigthRed, starter);

                    failed = true;
                }
            }

            if (!failed) {
                if (InstanceService is not null) {
                    var partyId = InstanceService.RegisterParty();
                    var parties = InstanceService.Parties;

                    if (parties.ContainsKey(partyId)) {
                        var party = parties[partyId];

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

                        PacketSender!.SendPartyInvite(starter, invited);
                    }
                }
            }
        }
    }
}
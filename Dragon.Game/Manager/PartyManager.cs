using Dragon.Core.Model;

using Dragon.Game.Parties;
using Dragon.Game.Players;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class PartyManager {
    public int Id { get; set; }
    public IList<PartyMember> Members { get; set; }
    public IList<PartyInvitedMember> InvitedMembers { get; set; }
    public IPacketSender? PacketSender { get; set; }
    public int LeaderIndex { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
    public int MaximumExperience { get; set; }
    public int Count => Members.Count;
    public PartyState State { get; set; }

    private readonly int AcceptTimeOut;
    private readonly int DisconnectionTimeOut;
    private readonly int MaximumMembers;

    private const int MinimumPartyMember = 2;

    private readonly IList<PartyInvitedMember> list;

    public PartyManager(int id, int acceptTimeOut, int maximumMembers, int disconnectionTimeOut) {
        Id = id;
        AcceptTimeOut = acceptTimeOut;
        MaximumMembers = maximumMembers;
        DisconnectionTimeOut = disconnectionTimeOut;

        Members = new List<PartyMember>(MaximumMembers);
        list = new List<PartyInvitedMember>(MaximumMembers);
        InvitedMembers = new List<PartyInvitedMember>(MaximumMembers);
    }

    public IPlayer? GetLeader() {
        foreach (var member in Members) {
            if (member.Index == LeaderIndex) {
                return member.Player;
            }
        }

        return null;
    }

    public PartyMember? FindDisconnectedMember(IPlayer player) {
        foreach (var member in Members) {
            if (member is not null) {
                if (member.CharacterId == player.Character.CharacterId) {
                    return member;
                }
            }
        }

        return null;
    }

    public bool IsEverybodyDisconnected() {
        foreach (var member in Members) {
            if (member is not null) {
                if (!member.Disconnected) {
                    return false;
                }
            }
        }

        return true;
    }

    public bool IsCreationFailed() {
        CheckForPendingInvitations();

        return InvitedMembers.Count == 0;
    }

    public void UpdateFailedCreation() {
        foreach (var member in Members) {
            if (member.Index == LeaderIndex) {
                var player = member.Player;

                if (player is not null) {
                    PacketSender!.SendMessage(SystemMessage.PlayerFailedToAcceptParty, QbColor.BrigthRed, player);
                }

                break;
            }
        }

        Members.Clear();
        InvitedMembers.Clear();

        State = PartyState.Disbanded;
    }

    public void CheckForPendingInvitations() {
        if (InvitedMembers.Count > 0) {
            for (var i = 0; i < InvitedMembers.Count; ++i) {
                var member = InvitedMembers[i];

                if (member.CouldBeRemoved) {
                    list.Add(member);
                }
                else {
                    CheckMember(member);
                }
            }

            for (var i = 0; i < list.Count; ++i) {
                InvitedMembers.Remove(list[i]);
            }

            list.Clear();
        }
    }

    public void CheckForDisconnectedMembers() {
        if (Members.Count > 0) {
            foreach (var member in Members) {
                if (member.Disconnected) {
                    member.DisconnectionTimeOut++;

                    if (member.DisconnectionTimeOut >= DisconnectionTimeOut) {
                        Members.Remove(member);

                        if (Members.Count >= MinimumPartyMember) {
                            PacketSender!.SendParty(this);
                        }
                        else {
                            Disband();
                        }

                        break;
                    }
                }
            }
        }
    }

    public void Disband() {
        foreach (var member in Members) {
            if (member is not null) {
                if (member.Player is not null) {
                    member.Player.PartyId = 0;
                    member.Player.PartyInvitedId = 0;

                    PacketSender!.SendPartyLeave(member.Player);
                    PacketSender!.SendMessage(SystemMessage.PartyDisbanded, QbColor.BrigthRed, member.Player);
                }
            }
        }

        Members.Clear();
        InvitedMembers.Clear();

        State = PartyState.Disbanded;
    }

    public int GetIndex(IPlayer player) {
        for (var i = 0; i < Members.Count; i++) {
            if (Members[i].Player == player) {
                return Members[i].Index;
            }
        }

        return 0;
    }

    public IList<int>? GetConnectionIds() {
        if (Members.Count > 0) {
            var list = new List<int>(Members.Count);

            foreach (var member in Members) {
                if (!member.Disconnected) {
                    var player = member.Player;

                    if (player is not null) {
                        list.Add(player.GetConnection().Id);
                    }
                }
            }

            return list;
        }

        return null;
    }

    private void CheckMember(PartyInvitedMember member) {
        if (member.Player is not null) {
            member.AcceptTimeOut++;

            if (member.AcceptTimeOut >= AcceptTimeOut) {
                member.Player.PartyId = 0;
                member.Player.PartyInvitedId = 0;

                PacketSender!.SendClosePartyInvitation(member.Player);

                list.Add(member);
            }
        }
        else {
            list.Add(member);
        }
    }

}
using Crystalshire.Network;
using Crystalshire.Network.Messaging.DTO;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Core.Model;

using Crystalshire.Game.Manager;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Network;

public sealed partial class PacketSender {

    public void SendPartyInvite(IPlayer player, IPlayer invited) {
        var packet = new SpPartyInvite() {
            FromName = player.Character.Name
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(invited.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendClosePartyInvitation(IPlayer player) {
        var msg = Writer!.CreateMessage(new SpClosePartyInvitation());

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendParty(PartyManager party) {
        var members = party.Members;
        var count = members.Count;

        var packet = new SpParty() {
            LeaderIndex = party.LeaderIndex,
            Experience = party.Experience,
            Level = party.Level,
            MaximumExperience = party.MaximumExperience,
            Members = new DataPartyMember[count]
        };

        for (var i = 0; i < count; ++i) {
            packet.Members[i] = new DataPartyMember() {
                Index = members[i].Index,
                Disconnected = members[i].Disconnected,
                Model = members[i].Model,
                Name = members[i].Character
            };

            var player = members[i].Player;

            if (player is not null) {
                packet.Members[i].InstanceId = player.Character.Map;

                packet.Members[i].Vital = new int[] {
                        player.Vitals.Get(Vital.HP),
                        player.Vitals.Get(Vital.MP),
                        player.Vitals.Get(Vital.Special)
                    };

                packet.Members[i].MaximumVital = new int[] {
                        player.Vitals.GetMaximum(Vital.HP),
                        player.Vitals.GetMaximum(Vital.MP),
                        player.Vitals.GetMaximum(Vital.Special)
                    };
            }
        }

        var msg = Writer!.CreateMessage(packet);

        for (var i = 0; i < count; ++i) {
            var player = members[i].Player;

            if (player is not null) {
                msg.DestinationPeers.Add(player.GetConnection().Id);
            }
        }

        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendPartyData(PartyManager party) {
        var members = party.Members;
        var count = members.Count;

        var packet = new SpPartyData() {
            Experience = party.Experience,
            LeaderIndex = party.LeaderIndex,
            Level = party.Level,
            MaximumExperience = party.MaximumExperience
        };

        var msg = Writer!.CreateMessage(packet);

        for (var i = 0; i < count; ++i) {
            var player = members[i].Player;

            if (player is not null) {
                msg.DestinationPeers.Add(player.GetConnection().Id);
            }
        }

        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendPartyVital(IPlayer player) {
        var party = GetPartyManager(player);

        if (party is not null) {
            var index = party.GetIndex(player);

            if (index > 0) {
                var list = party.GetConnectionIds();

                if (list is not null) {
                    var packet = CreatePartyVitalPacket(player, index);

                    var msg = Writer!.CreateMessage(packet);

                    msg.DestinationPeers.AddRange(list);
                    msg.TransmissionTarget = TransmissionTarget.Destination;

                    Writer.Enqueue(msg);
                }
            }
        }
    }

    public void SendPartyLeave(IPlayer player) {
        var packet = new PacketPartyLeave();

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    private PartyManager? GetPartyManager(IPlayer player) {
        var partyId = player.PartyId;
        var parties = InstanceService!.Parties;

        if (parties.ContainsKey(partyId)) {
            return parties[partyId];
        }

        return null;
    }

    private SpPartyVital CreatePartyVitalPacket(IPlayer player, int index) {
        return new SpPartyVital() {
            Index = index,
            Vital = new int[] {
                    player.Vitals.Get(Vital.HP),
                    player.Vitals.Get(Vital.MP),
                    player.Vitals.Get(Vital.Special),
                },
            MaximumVital = new int[] {
                    player.Vitals.GetMaximum(Vital.HP),
                    player.Vitals.GetMaximum(Vital.MP),
                    player.Vitals.GetMaximum(Vital.Special)
                }
        };
    }
}
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Parties;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class PartyDisconnectManager {
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MinimumPartyMember = 2;

    public PartyDisconnectManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void ProcessDisconnect(IPlayer player) {
        var party = GetPartyManager(player);

        if (party is not null) {
            var members = party.Members;

            foreach (var member in members) {
                if (member.Player == player) {
                    member.Player = null;
                    member.Disconnected = true;
                    member.DisconnectionTimeOut = 0;
                }
            }

            if (party.IsEverybodyDisconnected()) {
                party.Disband();
            }

            if (party is not null) {
                if (party.State != PartyState.Disbanded) {
                    if (members.Count >= MinimumPartyMember) {
                        GetPacketSender().SendParty(party);
                    }
                    else {
                        party.Disband();
                    }
                }
            }
        }
    }

    private Party? GetPartyManager(IPlayer player) {
        var id = player.PartyId;
        var parties = GetParties();

        if (parties.TryGetValue(id, out var party)) {
            return party;
        }

        return null;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IDictionary<int, Party> GetParties() {
        return InstanceService!.Parties;
    }
}
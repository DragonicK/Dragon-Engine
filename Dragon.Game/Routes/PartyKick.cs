using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Players;
using Dragon.Game.Manager;
using Dragon.Game.Network;
using Dragon.Core.Services;

namespace Dragon.Game.Routes;

public sealed class PartyKick : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.PartyKick;

    private readonly PartyKickManager PartyKickManager;

    public PartyKick(IServiceInjector injector) : base(injector) {
        PartyKickManager = new PartyKickManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var receveid = packet as CpPartyKick;

        if (receveid is not null) {
            var player = GetPlayerRepository().FindByConnectionId(connection.Id);

            if (player is not null) {
                Execute(player, receveid);
            }
        }
    }

    private void Execute(IPlayer player, CpPartyKick packet) {
        var party = GetPartyManager(player);

        if (party is not null) {
            PartyKickManager.ProcessKickRequest(party, player, packet.MemberIndex);
        }
    }

    private PartyManager? GetPartyManager(IPlayer player) {
        var id = player.PartyId;
        var parties = InstanceService!.Parties;

        parties.TryGetValue(id, out var party);

        return party;
    }
}
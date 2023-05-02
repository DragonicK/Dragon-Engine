using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Game.Network;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class DeclinePartyRequest : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.DeclineParty;

    private readonly PartyDeclineManager PartyDeclineManager;

    public DeclinePartyRequest(IServiceInjector injector) : base(injector) {
        PartyDeclineManager = new PartyDeclineManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var player = FindByConnection(connection);

        if (player is not null) {
            var party = GetPartyManager(player);

            if (party is not null) {
                PartyDeclineManager.ProcessDeclineRequest(party, player);
            }
        }
    }

    private PartyManager? GetPartyManager(IPlayer player) {
        var id = player.PartyInvitedId;
        var parties = InstanceService!.Parties;

        parties.TryGetValue(id, out var party);

        return party;
    }
}
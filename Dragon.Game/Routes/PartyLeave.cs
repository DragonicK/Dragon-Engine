using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Game.Network;
using Dragon.Game.Players;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

public sealed class PartyLeave : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.PartyLeave;

    private readonly PartyLeaveManager PartyLeaveManager;

    public PartyLeave(IServiceInjector injector) : base(injector) {
        PartyLeaveManager = new PartyLeaveManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var player = GetPlayerRepository().FindByConnectionId(connection.Id);

        if (player is not null) {
            var party = GetPartyManager(player);

            if (party is not null) {
                PartyLeaveManager.ProcessLeaveRequest(party, player);
            }
        }
    }

    private PartyManager? GetPartyManager(IPlayer player) {
        var id = player.PartyId;
        var parties = InstanceService!.Parties;

        parties.TryGetValue(id, out var party);

        return party;
    }
}
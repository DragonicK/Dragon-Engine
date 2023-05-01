﻿using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Game.Network;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class AcceptPartyRequest : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.AcceptParty;

    private readonly PartyAcceptManager PartyManager;

    public AcceptPartyRequest(IServiceInjector injector) : base(injector) {
        PartyManager = new PartyAcceptManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var player = GetPlayerRepository().FindByConnectionId(connection.Id);

        if (player is not null) {
            var party = GetPartyManager(player);

            if (party is not null) {
                PartyManager.ProcessAcceptRequest(party, player);
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
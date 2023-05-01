using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class PartyRequest : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.PartyRequest;

    private readonly PartyRequestManager PartyRequestManager;

    public PartyRequest(IServiceInjector injector) : base(injector) {
        PartyRequestManager = new PartyRequestManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpPartyRequest;

        if (received is not null) {
            var player = GetPlayerRepository().FindByConnectionId(connection.Id);

            if (player is not null) {
                Execute(player, received);
            }
        }       
    }

    private void Execute(IPlayer player, CpPartyRequest packet) {
        var sender = GetPacketSender();
        var invited = GetPlayerRepository().FindByName(packet.Character);

        if (invited is not null) {
            var party = GetPartyManager(player);

            if (party is not null) {
                InviteToParty(party, player, invited);
            }
            else {
                CreateNewParty(player, invited);
            }
        }
        else {
            sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.BrigthRed, player);
        }
    }

    private void InviteToParty(PartyManager party, IPlayer player, IPlayer invited) {
        if (player != invited) {
            PartyRequestManager.ProcessRequestInvite(party, player, invited);
        }
    }

    private void CreateNewParty(IPlayer player, IPlayer invited) {
        if (player != invited) {
            PartyRequestManager.ProcessRequestInvite(player, invited);
        }
    }

    private PartyManager? GetPartyManager(IPlayer player) {
        var id = player.TradeId;
        var parties = InstanceService!.Parties;

        parties.TryGetValue(id, out var party);

        return party;
    }
}
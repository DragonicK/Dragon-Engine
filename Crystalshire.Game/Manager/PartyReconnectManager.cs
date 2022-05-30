using Crystalshire.Core.Model;

using Crystalshire.Game.Players;
using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Parties;

namespace Crystalshire.Game.Manager;

public class PartyReconnectManager {
    public IPacketSender? PacketSender { get; init; }
    public InstanceService? InstanceService { get; init; }
    public IPlayer? Player { get; init; }

    public void Reconnect() {
        if (Player is not null) {
            var parties = InstanceService!.Parties;

            foreach (var (id, party) in parties) {
                var member = party.FindDisconnectedMember(Player);

                if (member is not null) {
                    Player.PartyId = id;

                    member.Player = Player;
                    member.Disconnected = false;
                    member.DisconnectionTimeOut = 0;

                    PacketSender!.SendParty(party);

                    foreach (var _member in party.Members) {
                        if (_member.Player is not null) {
                            PacketSender!.SendPartyDisplayIcons(_member.Player);
                        }
                    }

                    SendReconnectedMessage(party, member);
                }
            }
        }
    }

    private void SendReconnectedMessage(PartyManager party, PartyMember except) {
        var parameters = new string[] { except.Character };

        var members = party.Members;

        foreach (var member in members) {
            if (member != except) {
                if (member.Player is not null) {
                    PacketSender!.SendMessage(SystemMessage.PlayerPartyReconnected, QbColor.BrigthGreen, member.Player, parameters);
                }
            }
        }
    }
}
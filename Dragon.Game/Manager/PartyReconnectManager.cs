using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Parties;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class PartyReconnectManager {
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    public PartyReconnectManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void Reconnect(IPlayer player) {
        var sender = GetPacketSender();
        var parties = InstanceService!.Parties;

        foreach (var (id, party) in parties) {
            var lastMember = party.FindDisconnectedMember(player);

            if (lastMember is not null) {
                player.PartyId = id;

                lastMember.Player = player;
                lastMember.Disconnected = false;
                lastMember.DisconnectionTimeOut = 0;

                sender.SendParty(party);

                foreach (var member in party.Members) {
                    if (member.Player is not null) {
                        sender!.SendPartyDisplayIcons(member.Player);
                    }
                }

                SendReconnectedMessage(sender, party, lastMember);
            }
        }
    }

    private void SendReconnectedMessage(IPacketSender sender, Party party, PartyMember except) {
        var parameters = new string[] { except.Character };

        var members = party.Members;

        foreach (var member in members) {
            if (member != except) {
                if (member.Player is not null) {
                    sender.SendMessage(SystemMessage.PlayerPartyReconnected, QbColor.BrigthGreen, member.Player, parameters);
                }
            }
        }
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}
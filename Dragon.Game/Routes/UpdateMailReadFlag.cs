using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Network.Messaging;

namespace Dragon.Game.Routes;

public class UpdateMailReadFlag : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.UpdateMailReadFlag;

    public UpdateMailReadFlag(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpUpdateMailReadFlag;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                player.Mails.UpdateReadFlag(received.Id);
            }
        }
    }
}
using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class Cast : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.Cast;

    public Cast(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var received = packet as PacketCast;

        if (received is not null) {
            ExecuteCast(connection, received);
        }
    }

    private void ExecuteCast(IConnection connection, PacketCast packet) {
        var player = FindByConnection(connection);

        if (player is not null) {
            var index = packet.Index;

            if (index > 0) {
                index--;

                player.Combat.BufferSkill(index);
            }
        }
    }
}
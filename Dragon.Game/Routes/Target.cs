using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

public sealed class Target : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.Target;

    private readonly TargetManager TargetManager;

    public Target(IServiceInjector injector) : base(injector) {
        TargetManager = new TargetManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as PacketTarget;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                TargetManager.ProcessTarget(player, received.Index, received.TargetType);
            }
        }
    }
}
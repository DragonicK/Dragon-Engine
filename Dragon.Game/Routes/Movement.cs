using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Players;
using Dragon.Game.Network;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

public sealed class Movement : PacketRoute, IPacketRoute {
    public MessageHeader Header { get; set; } = MessageHeader.PlayerMovement;

    private readonly PlayerMovementManager PlayerMovement;

    public Movement(IServiceInjector injector) : base(injector) {
        PlayerMovement = new PlayerMovementManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as PacketPlayerMovement;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }

    private void Execute(IPlayer player, PacketPlayerMovement packet) {
        if (IsValidPacketPacket(packet)) {
            PlayerMovement.Move(player, packet.Direction, packet.State, packet.X, packet.Y);
        }
    }

    private bool IsValidPacketPacket(PacketPlayerMovement packet) {
        var x = packet.X;
        var y = packet.Y;
        if (x < 0 || y < 0) {
            return false;
        }

        return true;
    }
}
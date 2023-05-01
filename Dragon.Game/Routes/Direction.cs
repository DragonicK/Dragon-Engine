using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class Direction : PacketRoute, IPacketRoute {
    public MessageHeader Header { get; set; } = MessageHeader.PlayerDirection;

    public Direction(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var received = packet as PacketPlayerDirection;

        if (received is not null) {
            var player = GetPlayerRepository().FindByConnectionId(connection.Id);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }

    private void Execute(IPlayer player, PacketPlayerDirection packet) {
        player.Character.Direction = packet.Direction;

        var instanceId = player.Character.Map;

        GetInstances().TryGetValue(instanceId, out var instance);

        if (instance is not null) {
            GetPacketSender().SendDirection(player, instance);
        }
    }
}
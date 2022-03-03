using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;

namespace Crystalshire.Game.Routes {
    public sealed class Direction {
        public IConnection? Connection { get; set; }
        public PacketPlayerDirection? Packet { get; set; }
        public PacketSenderService? PacketSenderService { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public LoggerService? LoggerService { get; init; }
        public InstanceService? InstanceService { get; init; }

        public void Process() {
            var sender = PacketSenderService!.PacketSender;
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    player.Character.Direction = Packet!.Direction;

                    var instances = InstanceService!.Instances;
                    var instanceId = player.Character.Map;

                    if (instances.ContainsKey(instanceId)) {
                        var instance = instances[instanceId];
                        sender!.SendDirection(player, instance);
                    }
                }
            }
        }
    }
}
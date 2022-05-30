using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Routes;

public sealed class Movement {
    public IConnection? Connection { get; set; }
    public PacketPlayerMovement? Packet { get; set; }
    public LoggerService? LoggerService { get; init; }
    public InstanceService? InstanceService { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }

    public void Process() {
        if (IsValidPacket()) {
            var sender = PacketSenderService!.PacketSender;
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var movement = new PlayerMovement() {
                        Player = player,
                        PacketSender = sender,
                        InstanceService = InstanceService
                    };

                    movement.Move(Packet!.Direction, Packet.State, Packet.X, Packet.Y);
                }
            }
        }
    }

    private bool IsValidPacket() {
        var x = Packet!.X;
        var y = Packet!.Y;

        if (x < 0 || y < 0) {
            return false;
        }

        return true;
    }
}
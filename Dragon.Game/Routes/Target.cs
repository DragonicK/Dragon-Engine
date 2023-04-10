using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Services;

namespace Dragon.Game.Routes;

public sealed class Target {
    public IConnection? Connection { get; set; }
    public PacketTarget? Packet { get; set; }
    //public LoggerService? LoggerService { get; init; }
    public ContentService? ContentService { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }
    public InstanceService? InstanceService { get; init; }

    public void Process() {
        var repository = ConnectionService!.PlayerRepository;
        var sender = PacketSenderService!.PacketSender;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {

                var manager = new TargetManager() {
                    Player = player,
                    PacketSender = sender,
                    ContentService = ContentService,
                    InstanceService = InstanceService
                };

                manager.ProcessTarget(Packet!.Index, Packet.TargetType);
            }
        }
    }
}
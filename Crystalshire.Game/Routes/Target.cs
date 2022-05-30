using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Core.Model;

using Crystalshire.Game.Services;
using Crystalshire.Game.Manager;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Routes;

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
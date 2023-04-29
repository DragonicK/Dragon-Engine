using Dragon.Core.Model;
using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Administrator.Commands;

public sealed class WarpMeTo : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.WarpMeTo;
    public IPlayer? Administrator { get; set; }
    public IPacketSender? PacketSender { get; set; }
    public InstanceService? InstanceService { get; set; }
    public ConfigurationService? Configuration { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public ContentService? ContentService { get; set; }

    private const int MaximumParameters = 1;

    public void Process(string[]? parameters) {
        if (parameters is not null) {
            if (parameters.Length >= MaximumParameters) {
                if (Administrator is not null) {
                    if (Administrator.AccountLevel >= AccountLevel.Monitor) {
                        var repository = ConnectionService!.PlayerRepository;
                        var destination = repository!.FindByName(parameters[0].Trim());

                        MoveToDestination(destination);
                    }
                }
            }
        }
    }

    private void MoveToDestination(IPlayer? destination) {
        if (destination is not null) {
            var instances = InstanceService!.Instances;

            var instanceId = destination.Character.Map;
            var x = destination.Character.X;
            var y = destination.Character.Y;

            if (instances.ContainsKey(instanceId)) {
                var instance = instances[instanceId];

                var warper = new WarperManager() {
                    Player = Administrator,
                    InstanceService = InstanceService,
                    PacketSender = PacketSender
                };

                Administrator!.Character.X = x > instance.MaximumX ? instance.MaximumX : x;
                Administrator!.Character.Y = y > instance.MaximumY ? instance.MaximumY : y;

                warper.Warp(instance, Administrator.Character.X, Administrator.Character.Y);
            }
        }
        else {
            PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, Administrator!);
        }
    }
}
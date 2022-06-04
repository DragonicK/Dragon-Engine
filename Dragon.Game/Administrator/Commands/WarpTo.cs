using Dragon.Core.Model;

using Dragon.Game.Manager;
using Dragon.Game.Network;
using Dragon.Game.Services;
using Dragon.Game.Players;
using Dragon.Game.Instances;

namespace Dragon.Game.Administrator.Commands;

public sealed class WarpTo : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.WarpTo;
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
                int.TryParse(parameters[0], out var instanceId);

                if (Administrator is not null) {
                    if (Administrator.AccountLevel >= AccountLevel.Monitor) {
                        var instances = InstanceService!.Instances;

                        if (instances.ContainsKey(instanceId)) {
                            MoveTo(instances[instanceId]);
                        }
                    }
                }
            }
        }
    }

    private void MoveTo(IInstance instance) {
        var warper = new WarperManager() {
            Player = Administrator,
            InstanceService = InstanceService,
            PacketSender = PacketSender
        };

        var x = Administrator!.Character.X;
        var y = Administrator!.Character.Y;

        Administrator.Character.X = x > instance.MaximumX ? instance.MaximumX : x;
        Administrator.Character.Y = y > instance.MaximumY ? instance.MaximumY : y;

        warper.Warp(instance, Administrator.Character.X, Administrator.Character.Y);
    }
}
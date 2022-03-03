using Crystalshire.Core.Model;

using Crystalshire.Game.Manager;
using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Administrator.Commands {
    public sealed class WarpToMe : IAdministratorCommand {
        public AdministratorCommands Command { get; } = AdministratorCommands.WarpToMe;
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
                            var target = repository!.FindByName(parameters[0].Trim());

                            MoveToAdministrator(target);
                        }
                    }
                }
            }
        }

        private void MoveToAdministrator(IPlayer? target) {
            if (target is not null) {
                var instances = InstanceService!.Instances;

                var instanceId = Administrator!.Character.Map;
                var x = Administrator!.Character.X;
                var y = Administrator!.Character.Y;

                if (instances.ContainsKey(instanceId)) {
                    var instance = instances[instanceId];

                    var warper = new WarperManager() {
                        Player = target,
                        InstanceService = InstanceService,
                        PacketSender = PacketSender
                    };

                    target.Character.X = x > instance.MaximumX ? instance.MaximumX : x;
                    target.Character.Y = y > instance.MaximumY ? instance.MaximumY : y;

                    warper.Warp(instance, target.Character.X, target.Character.Y);
                }
            }
            else {
                PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, Administrator!);
            }
        }
    }
}
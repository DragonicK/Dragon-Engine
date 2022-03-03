using Crystalshire.Core.Model;

using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Administrator.Commands {
    public sealed class WarpToLocation : IAdministratorCommand {
        public AdministratorCommands Command { get; } = AdministratorCommands.WarpToLocation;
        public IPlayer? Administrator { get; set; }
        public IPacketSender? PacketSender { get; set; }
        public InstanceService? InstanceService { get; set; }
        public ConfigurationService? Configuration { get; set; }
        public ConnectionService? ConnectionService { get; set; }
        public ContentService? ContentService { get; set; }

        private const int MaximumParameters = 2;

        public void Process(string[]? parameters) {
            if (parameters is not null) {
                if (parameters.Length >= MaximumParameters) {
                    int.TryParse(parameters[0], out var x);
                    int.TryParse(parameters[1], out var y);

                    if (Administrator is not null) {
                        if (Administrator.AccountLevel >= AccountLevel.Monitor) {
                            WarpLocation(x, y);
                        }
                    }
                }
            }
        }

        private void WarpLocation(int x, int y) {
            if (Administrator is not null) {
                if (Administrator.AccountLevel >= AccountLevel.GameMaster) {
                    var instanceId = Administrator.Character.Map;
                    var instances = InstanceService!.Instances;

                    if (instances.ContainsKey(instanceId)) {
                        var instance = instances[instanceId];

                        Administrator.Character.X = x < 0 ? 0 : x;
                        Administrator.Character.Y = y < 0 ? 0 : y;
                        Administrator.Character.X = x > instance.MaximumX ? instance.MaximumX : x;
                        Administrator.Character.Y = y > instance.MaximumY ? instance.MaximumY : y;

                        PacketSender?.SendPlayerXY(Administrator, instance);
                    }
                }
            }
        }
    }
}
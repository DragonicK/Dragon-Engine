using Crystalshire.Core.Model;
using Crystalshire.Game.Network;
using Crystalshire.Game.Players;
using Crystalshire.Game.Services;

namespace Crystalshire.Game.Administrator.Commands {
    public sealed class ChangeActiveTitle : IAdministratorCommand {
        public AdministratorCommands Command { get; } = AdministratorCommands.SetActiveTitle;
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
                    ContinueProcess(parameters);
                }
            }
        }

        private void ContinueProcess(string[] parameters) {
            if (Administrator is not null) {
                if (Administrator.AccountLevel >= AccountLevel.Superior) {
                    int.TryParse(parameters[1], out var id);

                    var repository = ConnectionService!.PlayerRepository;
                    var target = repository!.FindByName(parameters[0].Trim());

                    Set(target, id);
                }
            }
        }

        private void Set(IPlayer? player, int id) {
            var titles = ContentService!.Titles;

            if (player is not null) {
                if (titles.Contains(id)) {
                    player.Character.TitleId = id;

                    player.Titles.Equip(id);

                    player.AllocateAttributes();

                    PacketSender!.SendAttributes(player);

                    var instanceId = player.Character.Map;
                    var instances = InstanceService!.Instances;

                    if (instances.ContainsKey(instanceId)) {
                        PacketSender!.SendTitle(player, instances[instanceId]);
                        PacketSender!.SendPlayerVital(player, instances[instanceId]);
                    }
                }
            }
            else {
                PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, Administrator!);
            }
        }
    }
}

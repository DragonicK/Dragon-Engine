using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Entity;
using Crystalshire.Core.Model.DisplayIcon;

using Crystalshire.Game.Network;
using Crystalshire.Game.Players;
using Crystalshire.Game.Services;
using Crystalshire.Game.Instances;

namespace Crystalshire.Game.Administrator.Commands {
    public sealed class AddEffect : IAdministratorCommand {
        public AdministratorCommands Command => AdministratorCommands.AddEffect;
        public IPlayer? Administrator { get; set; }
        public IPacketSender? PacketSender { get; set; }
        public InstanceService? InstanceService { get; set; }
        public ConfigurationService? Configuration { get; set; }
        public ConnectionService? ConnectionService { get; set; }
        public ContentService? ContentService { get; set; }

        private const int MaximumParameters = 4;

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
                    int.TryParse(parameters[2], out var level);
                    int.TryParse(parameters[3], out var duration);

                    var repository = ConnectionService!.PlayerRepository;
                    var target = repository!.FindByName(parameters[0].Trim());

                    Add(target, id, level, duration);
                }
            }
        }

        private void Add(IPlayer? player, int id, int level, int duration) {
            var effects = ContentService!.Effects;

            if (player is not null) {
                var instance = GetInstance(player);

                if (effects.Contains(id)) {
                    player!.Effects.Add(id, level, duration);
                    player!.Effects.UpdateAttributes();

                    player!.AllocateAttributes();
                    PacketSender!.SendAttributes(player);

                    if (instance is not null) {
                        PacketSender!.SendPlayerVital(player, instance);
                    }
                    else {
                        PacketSender!.SendPlayerVital(player);
                    }

                    SendDisplayIcon(instance, player, id, level, duration);     
                }
            }
            else {
                PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, Administrator!);
            }
        }

        private void SendDisplayIcon(IInstance? instance, IPlayer player, int id, int level, int duration) {
            if (instance is not null) { 
                var icon = new DisplayIcon() {
                    Id = id,
                    Level = level,
                    Duration = duration,
                    Type = DisplayIconType.Effect,
                    DurationType = DisplayIconDuration.Limited,
                    ExhibitionType = DisplayIconExhibition.Player,
                    OperationType = DisplayIconOperation.Update,
                    SkillType = DisplayIconSkill.None
                };

                PacketSender!.SendDisplayIcon(ref icon, DisplayIconTarget.Player, (IEntity)player, instance);
            }            
        }

        private IInstance? GetInstance(IPlayer player) {
            var instanceId = player.Character.Map;
            var instances = InstanceService!.Instances;

            if (instances.ContainsKey(instanceId)) {
                return instances[instanceId];
            }

            return null;
        }
    }
}
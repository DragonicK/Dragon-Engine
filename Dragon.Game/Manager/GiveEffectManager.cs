using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.Characters;
using Dragon.Core.Model.DisplayIcon;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class GiveEffectManager {
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    public GiveEffectManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void RemoveEffect(IPlayer player, int id) {
        var inventory = player.Effects.GetEffectById(id);
        var sender = GetPacketSender();

        if (inventory is not null) {
            var instance = GetInstance(player);

            SendDisplayIcon(sender, DisplayIconOperation.Remove, inventory, instance, player);

            inventory.EffectId = 0;
            inventory.EffectLevel = 0;
            inventory.EffectDuration = 0;
            inventory.IsAura = false;

            player.Effects.UpdateAttributes();

            SendAttributes(sender, player, instance);
        }
    }

    public void GiveEffect(IPlayer player, int id, int level, int duration, bool isAura = false) {
        var effects = GetDatabaseEffect();
        var sender = GetPacketSender();

        effects.TryGet(id, out var effect);

        if (effect is not null) {
            if (duration <= 0) {
                duration = effect.Duration;
            }

            var inventory = player.Effects.GetEffectById(id);

            var instance = GetInstance(player);

            if (inventory is not null) {
                inventory.EffectId = id;
                inventory.EffectLevel = level;
                inventory.EffectDuration = duration;
                inventory.IsAura = isAura;

                player.Effects.UpdateAttributes();

                SendAttributes(sender, player, instance);

                SendDisplayIcon(sender, DisplayIconOperation.Update, inventory, instance, player);
            }
            else {
                inventory = player!.Effects.Add(id, level, duration, isAura);

                if (inventory is not null) {
                    player.Effects.UpdateAttributes();

                    SendAttributes(sender, player, instance);

                    SendDisplayIcon(sender, DisplayIconOperation.Update, inventory, instance, player);
                }
            }
        }
    }

    private void SendDisplayIcon(IPacketSender sender, DisplayIconOperation operation, CharacterAttributeEffect inventory, IInstance? instance, IPlayer player) {
        if (instance is not null) {
            var durationType = inventory.IsAura ? DisplayIconDuration.Unlimited : DisplayIconDuration.Limited;

            var icon = new DisplayIcon() {
                Id = inventory.EffectId,
                Level = inventory.EffectLevel,
                Duration = inventory.EffectDuration,
                Type = DisplayIconType.Effect,
                ExhibitionType = DisplayIconExhibition.Player,
                SkillType = DisplayIconSkill.None,
                DurationType = durationType,
                OperationType = operation
            };

            sender.SendDisplayIcon(ref icon, DisplayIconTarget.Player, player, instance);
        }
    }

    private void SendAttributes(IPacketSender sender, IPlayer player, IInstance? instance) {
        player.AllocateAttributes();

        sender.SendAttributes(player);

        if (instance is not null) {
            sender.SendPlayerVital(player, instance);
        }
        else {
            sender.SendPlayerVital(player);
        }
    }

    private IInstance? GetInstance(IPlayer player) {
        var instanceId = player.Character.Map;
        var instances = InstanceService!.Instances;

        instances.TryGetValue(instanceId, out var instance);

        return instance;
    }

    private IDatabase<Effect> GetDatabaseEffect() {
        return ContentService!.Effects!;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}
using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Entity;
using Crystalshire.Core.Model.Effects;
using Crystalshire.Core.Model.Characters;
using Crystalshire.Core.Model.DisplayIcon;

using Crystalshire.Game.Players;
using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Instances;

namespace Crystalshire.Game.Manager;

public class GiveEffectManager {
    public IDatabase<Effect>? Effects { get; init; }
    public IPacketSender? PacketSender { get; init; }
    public InstanceService? InstanceService { get; init; }

    public void RemoveEffect(IPlayer player, int id) {
        var inventory = player!.Effects.GetEffectById(id);

        if (inventory is not null) {
            var instance = GetInstance(player);

            SendDisplayIcon(DisplayIconOperation.Remove, inventory, instance, player!);

            inventory.EffectId = 0;
            inventory.EffectLevel = 0;
            inventory.EffectDuration = 0;
            inventory.IsAura = false;

            player!.Effects.UpdateAttributes();

            SendAttributes(player, instance);
        }
    }

    public void GiveEffect(IPlayer player, int id, int level, int duration, bool isAura = false) {
        if (Effects is not null) {
            var effect = Effects[id];

            if (effect is not null) {
                if (duration <= 0) {
                    duration = effect.Duration;
                }

                var inventory = player!.Effects.GetEffectById(id);

                var instance = GetInstance(player);

                if (inventory is not null) {
                    inventory.EffectId = id;
                    inventory.EffectLevel = level;
                    inventory.EffectDuration = duration;
                    inventory.IsAura = isAura;

                    player!.Effects.UpdateAttributes();

                    SendAttributes(player, instance);

                    SendDisplayIcon(DisplayIconOperation.Update, inventory, instance, player!);
                }
                else {
                    inventory = player!.Effects.Add(id, level, duration, isAura);

                    if (inventory is not null) {
                        player!.Effects.UpdateAttributes();

                        SendAttributes(player, instance);

                        SendDisplayIcon(DisplayIconOperation.Update, inventory, instance, player!);
                    }
                }
            }
        }
    }

    private void SendDisplayIcon(DisplayIconOperation operation, CharacterAttributeEffect inventory, IInstance? instance, IPlayer player) {
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

            PacketSender!.SendDisplayIcon(ref icon, DisplayIconTarget.Player, (IEntity)player, instance);
        }
    }

    private void SendAttributes(IPlayer player, IInstance? instance) {
        player!.AllocateAttributes();

        PacketSender!.SendAttributes(player);

        if (instance is not null) {
            PacketSender!.SendPlayerVital(player, instance);
        }
        else {
            PacketSender!.SendPlayerVital(player);
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
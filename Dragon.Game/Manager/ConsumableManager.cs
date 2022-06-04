using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.DisplayIcon;

using Dragon.Game.Players;
using Dragon.Game.Network;
using Dragon.Game.Messages;
using Dragon.Game.Services;
using Dragon.Game.Instances;

namespace Dragon.Game.Manager;

public class ConsumableManager {
    public IPlayer? Player { get; init; }
    public IPacketSender? PacketSender { get; init; }
    public IDatabase<Effect>? Effects { get; init; }
    public InstanceService? InstanceService { get; init; }

    public void UsePotion(int index, Item item) {
        var inventory = Player!.Inventories.FindByIndex(index);

        if (inventory is not null) {
            var instance = GetInstance();

            ApplyVitals(item, instance);

            inventory.Value--;

            if (inventory.Value <= 0) {
                inventory.Clear();
            }

            PacketSender!.SendInventoryUpdate(Player, index);
        }
    }

    public void UseConsumable(int index, Item item) {
        var inventory = Player!.Inventories.FindByIndex(index);

        if (inventory is not null) {
            var instance = GetInstance();

            ApplyEffect(item, instance);

            inventory.Value--;

            if (inventory.Value <= 0) {
                inventory.Clear();
            }

            PacketSender!.SendInventoryUpdate(Player, index);
        }
    }

    private void ApplyVitals(Item item, IInstance? instance) {
        var length = Enum.GetValues<Vital>().Length;

        for (var i = 0; i < length; ++i) {
            var value = item.Vital[i];

            if (value > 0) {
                Player!.Vitals.Add((Vital)i, value);

                if (instance is not null) {
                    PacketSender!.SendPlayerVital(Player, instance);
                    SendActionMessage(instance, (Vital)i, value);
                }
            }
        }
    }

    private void ApplyEffect(Item item, IInstance? instance) {
        var id = item.EffectId;
        var duration = item.EffectDuration;
        var level = item.EffectLevel;

        if (Effects is not null) {
            if (Effects.Contains(id)) {
                var effect = Effects[id]!;

                if (duration <= 0) {
                    duration = effect.Duration;
                }

                var overridable = Player!.Effects.GetOverridable(effect);

                if (overridable is not null) {
                    SendDisplayIcon(DisplayIconOperation.Remove, overridable.EffectId, 0, 0, instance);

                    overridable.EffectId = id;
                    overridable.EffectLevel = level;
                    overridable.EffectDuration = duration;

                    Player!.Effects.UpdateAttributes();

                    SendAttributes();

                    SendDisplayIcon(DisplayIconOperation.Update, id, level, duration, instance);
                }
                else {
                    Player!.Effects.Add(id, level, duration);
                    Player!.Effects.UpdateAttributes();

                    SendAttributes();

                    SendDisplayIcon(DisplayIconOperation.Update, id, level, duration, instance);
                }
            }
        }
    }

    private void SendDisplayIcon(DisplayIconOperation operation, int id, int level, int duration, IInstance? instance) {
        if (instance is not null) {
            var icon = new DisplayIcon() {
                Id = id,
                Level = level,
                Duration = duration,
                Type = DisplayIconType.Effect,
                DurationType = DisplayIconDuration.Limited,
                ExhibitionType = DisplayIconExhibition.Player,
                SkillType = DisplayIconSkill.None,
                OperationType = operation
            };

            PacketSender!.SendDisplayIcon(ref icon, DisplayIconTarget.Player, (IEntity)Player, instance);
        }
    }

    private void SendAttributes() {
        Player!.AllocateAttributes();
        PacketSender!.SendAttributes(Player);

        var instance = GetInstance();

        if (instance is not null) {
            PacketSender!.SendPlayerVital(Player, instance);
        }
        else {
            PacketSender!.SendPlayerVital(Player);
        }
    }

    private void SendActionMessage(IInstance instance, Vital vital, int value) {
        QbColor color = QbColor.HealingGreen;

        int x = Player!.Character.X;

        if (vital == Vital.HP) {
            x = Player!.Character.X - 1;
            color = QbColor.HealingGreen;
        }
        else if (vital == Vital.MP) {
            x = Player!.Character.X + 1;
            color = QbColor.DeepSkyBlue;
        }
        else if (vital == Vital.Special) {
            x = Player!.Character.X;
            color = QbColor.Coral;
        }

        var damage = new Damage() {
            Color = color,
            X = x,
            Y = Player!.Character.Y,
            FontType = ActionMessageFontType.Damage,
            Message = $"+{value}",
            MessageType = ActionMessageType.Scroll
        };

        PacketSender!.SendMessage(damage, instance);
    }

    private IInstance? GetInstance() {
        var instanceId = Player!.Character.Map;
        var instances = InstanceService!.Instances;

        if (instances.ContainsKey(instanceId)) {
            return instances[instanceId];
        }

        return null;
    }

}
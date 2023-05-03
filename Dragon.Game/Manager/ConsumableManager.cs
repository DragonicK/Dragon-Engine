using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.DisplayIcon;

using Dragon.Game.Players;
using Dragon.Game.Messages;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class ConsumableManager {
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    public ConsumableManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void UsePotion(IPlayer player, int index, Item item) {
        var inventory = player.Inventories.FindByIndex(index);

        if (inventory is not null) {
            var sender = GetPacketSender();
            var instance = GetInstance(player);

            ApplyVitals(sender, player, item, instance);

            inventory.Value--;

            if (inventory.Value <= 0) {
                inventory.Clear();
            }

            sender!.SendInventoryUpdate(player, index);
        }
    }

    public void UseConsumable(IPlayer player, int index, Item item) {
        var inventory = player.Inventories.FindByIndex(index);

        if (inventory is not null) {
            var sender = GetPacketSender();
            var instance = GetInstance(player);

            ApplyEffect(sender, player, item, instance);

            inventory.Value--;

            if (inventory.Value <= 0) {
                inventory.Clear();
            }

            sender.SendInventoryUpdate(player, index);
        }
    }

    private void ApplyVitals(IPacketSender sender, IPlayer player, Item item, IInstance? instance) {
        var length = Enum.GetValues<Vital>().Length;

        for (var i = 0; i < length; ++i) {
            var value = item.Vital[i];

            if (value > 0) {
                player.Vitals.Add((Vital)i, value);

                if (instance is not null) {
                    sender.SendPlayerVital(player, instance);

                    SendActionMessage(sender, player, instance, (Vital)i, value);
                }
            }
        }
    }

    private void ApplyEffect(IPacketSender sender, IPlayer player, Item item, IInstance? instance) {
        var id = item.EffectId;
        var level = item.EffectLevel;
        var duration = item.EffectDuration;

        var effects = GetDatabaseEffects();

        effects.TryGet(id, out var effect);

        if (effect is not null) {
            if (duration <= 0) {
                duration = effect.Duration;
            }

            var overridable = player.Effects.GetOverridable(effect);

            if (overridable is not null) {
                SendDisplayIcon(sender, player, DisplayIconOperation.Remove, overridable.EffectId, 0, 0, instance);

                overridable.EffectId = id;
                overridable.EffectLevel = level;
                overridable.EffectDuration = duration;

                player.Effects.UpdateAttributes();

                SendAttributes(sender, player);

                SendDisplayIcon(sender, player, DisplayIconOperation.Update, id, level, duration, instance);
            }
            else {
                player.Effects.Add(id, level, duration);
                player.Effects.UpdateAttributes();

                SendAttributes(sender, player);

                SendDisplayIcon(sender, player, DisplayIconOperation.Update, id, level, duration, instance);
            }
        }

    }

    private void SendDisplayIcon(IPacketSender sender, IPlayer player, DisplayIconOperation operation, int id, int level, int duration, IInstance? instance) {
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

            sender.SendDisplayIcon(ref icon, DisplayIconTarget.Player, player, instance);
        }
    }

    private void SendAttributes(IPacketSender sender, IPlayer player) {
        player.AllocateAttributes();

        sender.SendAttributes(player);

        var instance = GetInstance(player);

        if (instance is not null) {
            sender.SendPlayerVital(player, instance);
        }
        else {
            sender.SendPlayerVital(player);
        }
    }

    private void SendActionMessage(IPacketSender sender, IPlayer player, IInstance instance, Vital vital, int value) {
        QbColor color = QbColor.HealingGreen;

        var y = player.Character.Y;
        var x = player.Character.X;

        if (vital == Vital.HP) {
            x = player.Character.X - 1;
            color = QbColor.HealingGreen;
        }
        else if (vital == Vital.MP) {
            x = player.Character.X + 1;
            color = QbColor.DeepSkyBlue;
        }
        else if (vital == Vital.Special) {
            x = player.Character.X;
            color = QbColor.Coral;
        }

        var damage = new Damage() {
            X = x,
            Y = y,
            Color = color,
            Message = value,
            FontType = ActionMessageFontType.Damage,
            MessageType = ActionMessageType.Scroll
        };

        sender.SendMessage(ref damage, instance);
    }

    private IInstance? GetInstance(IPlayer player) {
        var instanceId = player!.Character.Map;
        var instances = InstanceService!.Instances;

        instances.TryGetValue(instanceId, out var instance);

        return instance;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IDatabase<Effect> GetDatabaseEffects() {
        return ContentService!.Effects;
    }
}
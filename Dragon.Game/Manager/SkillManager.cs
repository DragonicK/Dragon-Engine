using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Passives;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class SkillManager {
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int StarterLevel = 1;

    public SkillManager(IServiceContainer services) {
        new ServiceInjector(services).Inject(this);
    }

    public void LearnFromInventory(IPlayer player, int index, Item item) {
        var added = false;
        var alreadyUsed = false;

        var id = item.SkillId;

        var sender = GetPacketSender();
        var dbSkill = GetDatabaseSkill();

        if (dbSkill.TryGet(id, out var skill)) {
            if (skill.Type == SkillType.Active) {
                if (!player.Skills.Contains(id)) {

                    added = true;

                    var inventory = player.Skills.Add(id, StarterLevel);

                    AllocateSkills(player);

                    AllocatePassives(player);

                    sender.SendSkillUpdate(player, inventory);
                }
                else {
                    alreadyUsed = true;
                }
            }
            else if (skill.Type == SkillType.Passive) {
                if (!player.Passives.Contains(id)) {

                    added = true;

                    var inventory = player.Passives.Add(id, 1);

                    AllocateSkills(player);

                    AllocatePassives(player);

                    player.Passives.UpdateAttributes();

                    SendAttributes(sender, player);

                    sender.SendPassiveUpdate(player, inventory);
                }
                else {
                    alreadyUsed = true;
                }
            }
        }

        if (alreadyUsed) {
            sender.SendMessage(SystemMessage.YouAlreadyLearnedSkill, QbColor.BrigthRed, player);
        }

        if (added) {
            var inventory = player.Inventories.FindByIndex(index);

            if (inventory is not null) {
                inventory.Value--;

                if (inventory.Value <= 0) {
                    inventory.Clear();
                }

                sender.SendInventoryUpdate(player, index);
            }

            sender.SendMessage(SystemMessage.YouLearnedSkill, QbColor.BrigthGreen, player, new string[] { id.ToString() });
        }
    }

    public void AllocateSkills(IPlayer player) {
        var inventories = player.Skills.ToList();

        var dbSkill = GetDatabaseSkill();

        foreach (var inventory in inventories) {
            inventory.ClearData();

            if (dbSkill.TryGet(inventory.SkillId, out var skill)) {
                inventory.AllocateData(skill);
            }
        }
    }

    public void AllocatePassives(IPlayer player) {
        var inventories = player.Passives.ToList();

        var dbSkill = GetDatabaseSkill();
        var dbPassive = GetDatabasePassive();

        foreach (var inventory in inventories) {
            if (dbSkill.TryGet(inventory.PassiveId, out var skill)) {
                if (dbPassive.TryGet(skill.PassiveId, out var passive)) {
                    ApplyPassive(player, passive, inventory.PassiveLevel);
                }
            }     
        } 
    }

    private void ApplyPassive(IPlayer player, Passive passive, int level) {
        if (passive.PassiveType == PassiveType.Improvement) {
            var inventories = player.Skills.ToList();

            foreach (var inventory in inventories) {
                if (inventory.SkillId == passive.SkillId) {
                    inventory.AllocateData(passive, level);
                }
            }
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

    private IInstance? GetInstance(IPlayer player) {
        var instanceId = player.Character.Map;
        var instances = InstanceService!.Instances;

        if (instances.TryGetValue(instanceId, out var instance)) {
            return instance;
        }

        return null;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IDatabase<Skill> GetDatabaseSkill() {
        return ContentService!.Skills;
    }

    private IDatabase<Passive> GetDatabasePassive() {
        return ContentService!.Passives;
    }
}
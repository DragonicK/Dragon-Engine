using Crystalshire.Core.Model;
using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Items;
using Crystalshire.Core.Model.Skills;
using Crystalshire.Core.Model.Passives;

using Crystalshire.Game.Players;
using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Instances;

namespace Crystalshire.Game.Manager {
    public class SkillManager {
        public IPlayer? Player { get; init; }
        public IDatabase<Skill>? Skills { get; init; }
        public IDatabase<Passive>? Passives { get; init; }
        public IPacketSender? PacketSender { get; init; }
        public InstanceService? InstanceService { get; init; }

        public void LearnFromInventory(int index, Item item) {
            var added = false;
            var alreadyUsed = false;

            var id = item.SkillId;

            if (Skills is not null) {
                if (Skills.Contains(id)) {
                    var skill = Skills[id]!;

                    if (skill.Type == SkillType.Active) {
                        if (!Player!.Skills.Contains(id)) {

                            added = true;

                            var inventory = Player!.Skills.Add(id, 1);

                            AllocateSkills();

                            AllocatePassives();

                            PacketSender!.SendSkillUpdate(Player, inventory);
                        }
                        else {
                            alreadyUsed = true;
                        }
                    }
                    else if (skill.Type == SkillType.Passive) {
                        if (!Player!.Passives.Contains(id)) {

                            added = true;

                            var inventory = Player!.Passives.Add(id, 1);

                            AllocateSkills();

                            AllocatePassives();

                            Player!.Passives.UpdateAttributes();

                            SendAttributes();

                            PacketSender!.SendPassiveUpdate(Player, inventory);
                        }
                        else {
                            alreadyUsed = true;
                        }
                    }
                }
            }

            if (alreadyUsed) {
                PacketSender!.SendMessage(SystemMessage.YouAlreadyLearnedSkill, QbColor.BrigthRed, Player!);
            }

            if (added) {
                var inventory = Player!.Inventories.FindByIndex(index);

                if (inventory is not null) {
                    inventory.Value--;

                    if (inventory.Value <= 0) {
                        inventory.Clear();
                    }

                    PacketSender!.SendInventoryUpdate(Player, index);
                }

                PacketSender!.SendMessage(SystemMessage.YouLearnedSkill, QbColor.BrigthGreen, Player!, new string[] { id.ToString() });
            }
        }

        public void AllocateSkills() {
            if (Skills is not null) {
                var inventories = Player!.Skills.ToList();

                foreach (var inventory in inventories) {
                    inventory.ClearData();

                    if (Skills.Contains(inventory.SkillId)) {
                        var skill = Skills[inventory.SkillId]!;
                        inventory.AllocateData(skill);
                    }
                }
            }
        }

        public void AllocatePassives() {
            if (Passives is not null && Skills is not null) {
                var inventories = Player!.Passives.ToList();

                foreach (var inventory in inventories) {
                    if (Skills.Contains(inventory.PassiveId)) {
                        var skill = Skills[inventory.PassiveId]!;

                        if (Passives.Contains(skill.PassiveId)) {
                            var passive = Passives[skill.PassiveId]!;

                            ApplyPassive(passive, inventory.PassiveLevel);
                        }
                    }
                }
            }
        }

        private void ApplyPassive(Passive passive, int level) {
            if (passive.PassiveType == PassiveType.Improvement) {
                var inventories = Player!.Skills.ToList();

                foreach (var inventory in inventories) {
                    if (inventory.SkillId == passive.SkillId) {
                        inventory.AllocateData(passive, level);
                    }
                }
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

        private IInstance? GetInstance() {
            var instanceId = Player!.Character.Map;
            var instances = InstanceService!.Instances;

            if (instances.ContainsKey(instanceId)) {
                return instances[instanceId];
            }

            return null;
        }
    }
}
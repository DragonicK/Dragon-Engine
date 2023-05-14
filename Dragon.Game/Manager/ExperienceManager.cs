using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Parties;
using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager {
    public sealed class ExperienceManager {
        public ContentService? ContentService { get; private set; }
        public InstanceService? InstanceService { get; private set; }
        public ConfigurationService? Configuration { get; private set; }
        public PacketSenderService? PacketSenderService { get; private set; }

        private const int RequiredMembersToShareExperience = 2;

        public ExperienceManager(IServiceInjector injector) {
            injector.Inject(this);
        }

        public int GetExperience(IPlayer player, IInstanceEntity from) {
            var server = Configuration!.Rates.Character;
            var service = player.Services.Character;

            return Convert.ToInt32(GetRawExperience(from) * (server + service));
        }

        public int GetExperience(IPlayer player, int experience) {
            var server = Configuration!.Rates.Character;
            var service = player.Services.Character;

            return Convert.ToInt32(experience * (server + service));
        }

        public int GetRawExperience(IInstanceEntity from) {
            var id = from.Id;
            var npcs = ContentService!.Npcs;

            npcs.TryGet(id, out var npc);

            if (npc is not null) {
                var server = Configuration!.Rates.Character;

                var experience = npcs[id]!.Experience;

                return Convert.ToInt32(experience * server);
            }

            return 0;
        }

        public void GivePartyExperience(IPlayer player, IInstanceEntity entity, IInstance instance) {
            var party = GetPartyManager(player);

            if (party is not null) {
                var experience = GetRawExperience(entity);

                if (experience > 0) {
                    var count = party.GetMemberCountOnSameMap(player);

                    if (count >= RequiredMembersToShareExperience) {
                        var config = Configuration!.Group;

                        var multiplier = 1.0f;

                        if (count <= config.ExperienceByPartyMemberCount.Count) {
                            multiplier = config.ExperienceByPartyMemberCount[count];
                        }

                        var calculated = Convert.ToInt32(experience * multiplier);

                        ContinuePartyShareExperience(player, instance, party, calculated);
                    }                 
                    else {
                        GivePlayerExperience(player, entity, instance);
                    }
                }
            }
        }

        private void ContinuePartyShareExperience(IPlayer player, IInstance instance, Party party, int experience) {
            var members = party.Members;

            foreach (var member in members) {
                if (member.Player is not null) {
                    if (!member.Disconnected) {
                        if (player.Character.Map == member.Player.Character.Map) {
                             GivePlayerExperience(member.Player, instance, experience);
                        }
                    }
                }
            }
        }

        public void GivePlayerExperience(IPlayer player, IInstance instance, int experience) {
            var calculted = GetExperience(player, experience);

            if (calculted > 0) {
                AddExperienceAndSendToClient(player, instance, calculted);
            }
        }

        public void GivePlayerExperience(IPlayer player, IInstanceEntity entity, IInstance instance) {   
            var experience = GetExperience(player, entity);

            if (experience > 0) {
                AddExperienceAndSendToClient(player, instance, experience);
            }
        }

        private void AddExperienceAndSendToClient(IPlayer player, IInstance instance, int experience) {
            var sender = GetPacketSender();

            player.Character.Experience += experience;

            if (CheckForLevelUp(player)) {
                player.AllocateAttributes();

                sender.SendAttributes(player);
                sender.SendPlayerVital(player, instance);
                sender.SendPlayerDataTo(player, instance);
            }

            sender.SendMessage(SystemMessage.ReceivedExperience, QbColor.BrigthBlue, player, new string[] { experience.ToString() });

            SendExperience(sender, player);      
        }

        public bool CheckForLevelUp(IPlayer player) {
            var levelCount = 0;

            var level = player.Character.Level;
            var experience = player.Character.Experience;
            var database = ContentService!.PlayerExperience;

            if (level >= database.MaximumLevel) {
                if (experience >= database.Experiences[level]) {
                    return false;
                }
            }

            var points = player.Character.Points;
            var classId = player.Character.ClassCode;

            var classJob = ContentService!.Classes[classId]!;

            while (experience >= database.Experiences[level]) {
                experience -= database.Experiences[level]; ;

                points += classJob.AttributePointPerLevel;

                level++;
                levelCount++;

                if (level >= database.MaximumLevel) {
                    break;
                }
            }

            if (level >= database.MaximumLevel) {
                if (experience > database.Experiences[level]) {
                    experience = database.Experiences[level];
                }
            }

            player.Character.Points = points;
            player.Character.Level = level;
            player.Character.Experience = experience;

            return levelCount > 0;
        }

        public void SendExperience(IPacketSender sender, IPlayer player) {
            var level = player.Character.Level;
            var experience = ContentService!.PlayerExperience;

            var maximum = 0;

            if (level > 0) {
                if (level <= experience.MaximumLevel) {
                    maximum = experience.Get(level);
                }
            }

            var minimum = player.Character.Experience;

            if (minimum >= maximum) {
                minimum = maximum;

                player.Character.Experience = minimum;
            }

            sender.SendExperience(player, minimum, maximum);
        }

        private Party? GetPartyManager(IPlayer player) {
            var parties = InstanceService!.Parties;

            parties.TryGetValue(player.PartyId, out var party);

            return party;
        }

        private IPacketSender GetPacketSender() {
            return PacketSenderService!.PacketSender!;
        }
    }
}
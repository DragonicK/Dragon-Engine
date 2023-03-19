using Dragon.Core.Logs;
using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Maps;
using Dragon.Core.Model.Shops;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Quests;
using Dragon.Core.Model.Gashas;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Titles;
using Dragon.Core.Model.Recipes;
using Dragon.Core.Model.Classes;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.Passives;
using Dragon.Core.Model.Upgrades;
using Dragon.Core.Model.Premiums;
using Dragon.Core.Model.Equipments;
using Dragon.Core.Model.Heraldries;
using Dragon.Core.Model.Attributes;
using Dragon.Core.Model.Achievements;
using Dragon.Core.Model.Conversations;
using Dragon.Core.Model.EquipmentSets;
using Dragon.Core.Model.NotificationIcons;
using Dragon.Core.Serialization;

using Dragon.Game.Administrator;
using Dragon.Game.Instances;
using Dragon.Network.Messaging.SharedPackets;

namespace Dragon.Game.Services;

public class ContentService : IService {
    public ServicePriority Priority => ServicePriority.Mid;
    public ConfigurationService? Configuration { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }
    public LoggerService? LoggerService { get; init; }
    public IDatabase<IClass> Classes { get; }
    public IDatabase<IMap> Maps { get; set; }
    public IDatabase<Achievement> Achievements { get; }
    public IDatabase<GroupAttribute> AchivementAttributes { get; }
    public IDatabase<Effect> Effects { get; }
    public IDatabase<GroupAttribute> EffectAttributes { get; }
    public IDatabase<GroupAttribute> EffectUpgrades { get; }
    public IDatabase<Title> Titles { get; }
    public IDatabase<GroupAttribute> TitleAttributes { get; }
    public IDatabase<Passive> Passives { get; }
    public IDatabase<GroupAttribute> PassiveAttributes { get; }
    public IDatabase<GroupAttribute> PassiveUpgrades { get; }
    public IDatabase<Recipe> Recipes { get; }
    public IDatabase<EquipmentSet> EquipmentSets { get; }
    public IDatabase<GroupAttribute> EquipmentSetAttributes { get; }
    public IDatabase<Heraldry> Heraldries { get; }
    public IDatabase<GroupAttribute> HeraldryAttributes { get; }
    public IDatabase<GroupAttribute> HeraldryUpgrades { get; }
    public IDatabase<Equipment> Equipments { get; }
    public IDatabase<GroupAttribute> EquipmentAttributes { get; }
    public IDatabase<GroupAttribute> EquipmentUpgrades { get; }
    public IDatabase<Skill> Skills { get; }
    public IDatabase<Item> Items { get; }
    public IDatabase<Npc> Npcs { get; }
    public IDatabase<GroupAttribute> NpcAttributes { get; }
    public IDatabase<NotificationIcon> NotificationIcons { get; }
    public IDatabase<Gasha> Gashas { get; }
    public IDatabase<Premium> Premiums { get; }
    public IDatabase<Upgrade> Upgrades { get; }
    public IDatabase<Conversation> Conversations { get; }
    public IDatabase<Shop> Shops { get; }
    public IDatabase<Quest> Quests { get; }
    public ICommandRepository CommandRepository { get; }
    public Experience PlayerExperience { get; }
    public Experience GuildExperience { get; }
    public Experience PartyExperience { get; }
    public Experience PetExperience { get; }
    public Experience CraftExperience { get; }
    public Experience SkillExperience { get; }
    public Experience TalentExperience { get; }
    public IDictionary<int, IInstance> Instances { get; set; }

    public ContentService() {
        Instances = new Dictionary<int, IInstance>();

        Classes = new Classes() {
            Folder = "./Server/Classes"
        };

        Maps = new Maps() {
            Folder = "./Server/Maps"
        };

        Achievements = new Achievements() {
            Folder = "./Server/Content",
            FileName = "Achievements.dat"
        };

        AchivementAttributes = new Attributes() {
            Folder = "./Server/Content",
            FileName = "AchievementAttributes.dat"
        };

        Effects = new Effects() {
            Folder = "./Server/Content",
            FileName = "Effects.dat"
        };

        EffectAttributes = new Attributes() {
            Folder = "./Server/Content",
            FileName = "EffectAttributes.dat"
        };

        EffectUpgrades = new Attributes() {
            Folder = "./Server/Content",
            FileName = "EffectUpgrades.dat"
        };

        Titles = new Titles() {
            Folder = "./Server/Content",
            FileName = "Titles.dat"
        };

        TitleAttributes = new Attributes() {
            Folder = "./Server/Content",
            FileName = "TitleAttributes.dat"
        };

        Recipes = new Recipes() {
            Folder = "./Server/Content",
            FileName = "Recipes.dat"
        };

        Passives = new Passives() {
            Folder = "./Server/Content",
            FileName = "Passives.dat"
        };

        PassiveAttributes = new Attributes() {
            Folder = "./Server/Content",
            FileName = "PassiveAttributes.dat"
        };

        PassiveUpgrades = new Attributes() {
            Folder = "./Server/Content",
            FileName = "PassiveUpgrades.dat"
        };

        EquipmentSets = new EquipmentSets() {
            Folder = "./Server/Content",
            FileName = "EquipmentSets.dat"
        };

        EquipmentSetAttributes = new Attributes() {
            Folder = "./Server/Content",
            FileName = "EquipmentSetAttributes.dat"
        };

        Heraldries = new Heraldries() {
            Folder = "./Server/Content",
            FileName = "Heraldries.dat"
        };

        HeraldryAttributes = new Attributes() {
            Folder = "./Server/Content",
            FileName = "HeraldryAttributes.dat"
        };

        HeraldryUpgrades = new Attributes() {
            Folder = "./Server/Content",
            FileName = "HeraldryUpgrades.dat"
        };

        Equipments = new Equipments() {
            Folder = "./Server/Content",
            FileName = "Equipments.dat"
        };

        EquipmentAttributes = new Attributes() {
            Folder = "./Server/Content",
            FileName = "EquipmentAttributes.dat"
        };

        EquipmentUpgrades = new Attributes() {
            Folder = "./Server/Content",
            FileName = "EquipmentUpgrades.dat"
        };

        Skills = new Skills() {
            Folder = "./Server/Content",
            FileName = "Skills.dat"
        };

        Items = new Items() {
            Folder = "./Server/Content",
            FileName = "Items.dat"
        };

        Npcs = new Npcs() {
            Folder = "./Server/Content",
            FileName = "Npcs.dat"
        };

        NpcAttributes = new Attributes() {
            Folder = "./Server/Content",
            FileName = "NpcAttributes.dat"
        };

        NotificationIcons = new NotificationIcons() {
            Folder = "./Server/Content",
            FileName = "Icons.dat"
        };

        Gashas = new Gashas() {
            Folder = "./Server/Gashas"
        };

        Premiums = new Premiums() {
            Folder = "./Server/Premiums"
        };

        Upgrades = new Upgrades() {
            Folder = "./Server/Upgrades"
        };

        Conversations = new Conversations() {
            Folder = "./Server/Content",
            FileName = "Conversations.dat"
        };

        Shops = new Shops() {
            Folder = "./Server/Shops"
        };

        Quests = new Quests() {
            Folder = "./Server/Content",
            FileName = "Quests.dat"
        };

        CommandRepository = new CommandRepository();

        PlayerExperience = LoadExperience(new Experience(), "Player");
        GuildExperience = LoadExperience(new Experience(), "Guild");
        PartyExperience = LoadExperience(new Experience(), "Party");
        PetExperience = LoadExperience(new Experience(), "Pet");
        CraftExperience = LoadExperience(new Experience(), "Craft");
        SkillExperience = LoadExperience(new Experience(), "Skill");
        TalentExperience = LoadExperience(new Experience(), "Talent");
    }

    public void Start() {
        var logger = GetLogger();

        logger?.Info(GetType().Name, "Loading game content");

        Classes.Load();
        Maps.Load();
        Achievements.Load();
        AchivementAttributes.Load();
        Effects.Load();
        EffectAttributes.Load();
        EffectUpgrades.Load();
        Titles.Load();
        TitleAttributes.Load();
        Passives.Load();
        PassiveAttributes.Load();
        PassiveUpgrades.Load();
        Recipes.Load();
        EquipmentSets.Load();
        EquipmentSetAttributes.Load();
        Heraldries.Load();
        HeraldryAttributes.Load();
        HeraldryUpgrades.Load();
        Equipments.Load();
        EquipmentAttributes.Load();
        EquipmentUpgrades.Load();
        Skills.Load();
        Items.Load();
        Npcs.Load();
        NpcAttributes.Load();
        NotificationIcons.Load();
        Gashas.Load();
        Premiums.Load();
        Upgrades.Load();
        Conversations.Load();
        Shops.Load();
        Quests.Load();

        LoadInstances();
    }

    public void Stop() {
        var logger = GetLogger();

        logger?.Info(GetType().Name, "Cleaning game content");

        Classes.Clear();
        Maps.Clear();
        Achievements.Clear();
        AchivementAttributes.Clear();
        Effects.Clear();
        EffectAttributes.Clear();
        EffectUpgrades.Clear();
        Titles.Clear();
        TitleAttributes.Clear();
        Recipes.Clear();
        Passives.Clear();
        PassiveAttributes.Clear();
        PassiveUpgrades.Clear();
        EquipmentSets.Clear();
        EquipmentSetAttributes.Clear();
        Heraldries.Clear();
        HeraldryAttributes.Clear();
        HeraldryUpgrades.Clear();
        Equipments.Clear();
        EquipmentAttributes.Clear();
        EquipmentUpgrades.Clear();
        Skills.Clear();
        Items.Clear();
        Npcs.Clear();
        NpcAttributes.Clear();
        NotificationIcons.Clear();
        Gashas.Clear();
        Premiums.Clear();
        Upgrades.Clear();
        Conversations.Clear();
        Shops.Clear();
        Quests.Clear();

        Instances.Clear();
    }

    private Experience LoadExperience(Experience experience, string name) {
        string file = $"./Server/Experiences/{name}.json";

        if (!Json.FileExists(file)) {
            Json.Save(file, experience);
        }
        else {
            var _exp = Json.Get<Experience>(file)!;

            Json.Save(file, _exp);

            return _exp;
        }

        return experience;
    }

    private void LoadInstances() {
        var sender = PacketSenderService!.PacketSender;

        var loader = new InstanceLoader("./Server/Instances", Configuration!, Maps, sender);

        loader.LoadInstances();

        Instances = loader.Instances;
    }

    private ILogger? GetLogger() {
        return LoggerService!.Logger;
    }
}
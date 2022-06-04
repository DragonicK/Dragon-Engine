using Dragon.Core.Content;
using Dragon.Core.Serialization;

using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Quests;
using Dragon.Core.Model.Titles;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Recipes;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.Passives;
using Dragon.Core.Model.Heraldries;
using Dragon.Core.Model.Equipments;
using Dragon.Core.Model.Attributes;
using Dragon.Core.Model.Achievements;
using Dragon.Core.Model.Conversations;
using Dragon.Core.Model.EquipmentSets;
using Dragon.Core.Model.NotificationIcons;

using Dragon.Editor.Npcs;
using Dragon.Editor.Items;
using Dragon.Editor.Common;
using Dragon.Editor.Quests;
using Dragon.Editor.Titles;
using Dragon.Editor.Skills;
using Dragon.Editor.Effects;
using Dragon.Editor.Recipes;
using Dragon.Editor.Passives;
using Dragon.Editor.Heraldries;
using Dragon.Editor.Attributes;
using Dragon.Editor.Equipments;
using Dragon.Editor.Achievements;
using Dragon.Editor.Conversations;
using Dragon.Editor.EquipmentSets;
using Dragon.Editor.NotificationIcons;

namespace Dragon.Editor;

public partial class FormMain : Form {
    public JetBrainsMono? JetBrainsMono { get; set; }
    public Configuration Configuration { get; }
    public IDatabase<Achievement> Achievements { get; private set; }
    public IDatabase<GroupAttribute> AchievementAttributes { get; private set; }
    public IDatabase<Title> Titles { get; private set; }
    public IDatabase<GroupAttribute> TitleAttributes { get; private set; }
    public IDatabase<Effect> Effects { get; private set; }
    public IDatabase<GroupAttribute> EffectAttributes { get; private set; }
    public IDatabase<GroupAttribute> EffectUpgrades { get; private set; }
    public IDatabase<Recipe> Recipes { get; private set; }
    public IDatabase<Passive> Passives { get; private set; }
    public IDatabase<GroupAttribute> PassiveAttributes { get; private set; }
    public IDatabase<GroupAttribute> PassiveUpgrades { get; private set; }
    public IDatabase<EquipmentSet> EquipmentSets { get; private set; }
    public IDatabase<GroupAttribute> EquipmentSetAttributes { get; private set; }
    public IDatabase<Heraldry> Heraldries { get; private set; }
    public IDatabase<GroupAttribute> HeraldryAttributes { get; private set; }
    public IDatabase<GroupAttribute> HeraldryUpgrades { get; private set; }
    public IDatabase<Equipment> Equipments { get; private set; }
    public IDatabase<GroupAttribute> EquipmentAttributes { get; private set; }
    public IDatabase<GroupAttribute> EquipmentUpgrades { get; private set; }
    public IDatabase<Skill> Skills { get; private set; }
    public IDatabase<Item> Items { get; private set; }
    public IDatabase<Npc> Npcs { get; private set; }
    public IDatabase<GroupAttribute> NpcAttributes { get; private set; }
    public IDatabase<NotificationIcon> NotificationIcons { get; private set; }
    public IDatabase<Conversation> Conversations { get; private set; }
    public IDatabase<Quest> Quests { get; private set; }

    public FormMain() {
        InitializeComponent();

        const string _File = "Configuration.json";

        if (File.Exists(_File)) {
            Configuration = Json.Get<Configuration>(_File)!;
        }
        else {
            Configuration = new Configuration();
        }

        Json.Save(_File, Configuration);

        TextServerPath.Text = Configuration.ServerPath;
        TextServerOutput.Text = Configuration.OutputServerPath;
        TextClientOutput.Text = Configuration.OutputClientPath;

        Achievements = new Core.Content.Achievements {
            FileName = "Achievements.dat",
            Folder = "./Content"
        };

        AchievementAttributes = new Core.Content.Attributes() {
            FileName = "AchievementAttributes.dat",
            Folder = "./Content"
        };

        Titles = new Core.Content.Titles {
            FileName = "Titles.dat",
            Folder = "./Content"
        };

        TitleAttributes = new Core.Content.Attributes() {
            FileName = "TitleAttributes.dat",
            Folder = "./Content"
        };

        Effects = new Core.Content.Effects {
            FileName = "Effects.dat",
            Folder = "./Content"
        };

        EffectAttributes = new Core.Content.Attributes() {
            FileName = "EffectAttributes.dat",
            Folder = "./Content"
        };

        EffectUpgrades = new Core.Content.Attributes() {
            FileName = "EffectUpgrades.dat",
            Folder = "./Content"
        };

        Recipes = new Core.Content.Recipes() {
            FileName = "Recipes.dat",
            Folder = "./Content"
        };

        Passives = new Core.Content.Passives() {
            FileName = "Passives.dat",
            Folder = "./Content"
        };

        PassiveAttributes = new Core.Content.Attributes() {
            FileName = "PassiveAttributes.dat",
            Folder = "./Content"
        };

        PassiveUpgrades = new Core.Content.Attributes() {
            FileName = "PassiveUpgrades.dat",
            Folder = "./Content"
        };

        EquipmentSets = new Core.Content.EquipmentSets() {
            FileName = "EquipmentSets.dat",
            Folder = "./Content"
        };

        EquipmentSetAttributes = new Core.Content.Attributes() {
            FileName = "EquipmentSetAttributes.dat",
            Folder = "./Content"
        };

        Heraldries = new Core.Content.Heraldries() {
            FileName = "Heraldries.dat",
            Folder = "./Content"
        };

        HeraldryAttributes = new Core.Content.Attributes() {
            FileName = "HeraldryAttributes.dat",
            Folder = "./Content"
        };

        HeraldryUpgrades = new Core.Content.Attributes() {
            FileName = "HeraldryUpgrades.dat",
            Folder = "./Content"
        };

        Equipments = new Core.Content.Equipments() {
            FileName = "Equipments.dat",
            Folder = "./Content"
        };

        EquipmentAttributes = new Core.Content.Attributes() {
            FileName = "EquipmentAttributes.dat",
            Folder = "./Content"
        };

        EquipmentUpgrades = new Core.Content.Attributes() {
            FileName = "EquipmentUpgrades.dat",
            Folder = "./Content"
        };

        Skills = new Core.Content.Skills() {
            FileName = "Skills.dat",
            Folder = "./Content"
        };

        Items = new Core.Content.Items() {
            FileName = "Items.dat",
            Folder = "./Content"
        };

        Npcs = new Core.Content.Npcs() {
            FileName = "Npcs.dat",
            Folder = "./Content"
        };

        NpcAttributes = new Core.Content.Attributes() {
            FileName = "NpcAttributes.dat",
            Folder = "./Content"
        };

        NotificationIcons = new Core.Content.NotificationIcons() {
            FileName = "Icons.dat",
            Folder = "./Content"
        };

        Conversations = new Core.Content.Conversations() {
            FileName = "Conversations.dat",
            Folder = "./Content"
        };

        Quests = new Core.Content.Quests() {
            FileName = "Quests.dat",
            Folder = "./Content"
        };
    }

    private void MenuExit_Click(object sender, EventArgs e) {
        Application.Exit();
    }

    #region Achievements

    private void MenuAchievement_Click(object sender, EventArgs e) {
        Achievements.Clear();
        Achievements.Load();

        var f = new FormAchievement(Configuration, Achievements);

        ChangeFont(f);

        f.Show();
    }

    private void MenuAchievementAttribute_Click(object sender, EventArgs e) {
        AchievementAttributes.Clear();
        AchievementAttributes.Load();

        var f = new FormAttribute(Configuration, AchievementAttributes);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    #region Titles

    private void MenuTitle_Click(object sender, EventArgs e) {
        Titles.Clear();
        Titles.Load();

        var f = new FormTitle(Configuration, Titles);

        ChangeFont(f);

        f.Show();
    }

    private void MenuTitleAttribute_Click(object sender, EventArgs e) {
        TitleAttributes.Clear();
        TitleAttributes.Load();

        var f = new FormAttribute(Configuration, TitleAttributes);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    #region Effects

    private void MenuEffect_Click(object sender, EventArgs e) {
        Effects.Clear();
        Effects.Load();

        var f = new FormEffect(Configuration, Effects);

        ChangeFont(f);

        f.Show();
    }

    private void MenuEffectAttribute_Click(object sender, EventArgs e) {
        EffectAttributes.Clear();
        EffectAttributes.Load();

        var f = new FormAttribute(Configuration, EffectAttributes);

        ChangeFont(f);

        f.Show();
    }

    private void MenuEffectUpgrade_Click(object sender, EventArgs e) {
        EffectUpgrades.Clear();
        EffectUpgrades.Load();

        var f = new FormAttribute(Configuration, EffectUpgrades);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    #region Recipes

    private void MenuRecipe_Click(object sender, EventArgs e) {
        Recipes.Clear();
        Recipes.Load();

        var f = new FormRecipe(Configuration, Recipes);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    #region Passives

    private void MenuPassive_Click(object sender, EventArgs e) {
        Passives.Clear();
        Passives.Load();

        var f = new FormPassive(Configuration, Passives);

        ChangeFont(f);

        f.Show();
    }

    private void MenuPassiveAttribute_Click(object sender, EventArgs e) {
        PassiveAttributes.Clear();
        PassiveAttributes.Load();

        var f = new FormAttribute(Configuration, PassiveAttributes);

        ChangeFont(f);

        f.Show();
    }

    private void MenuPassiveUpgrade_Click(object sender, EventArgs e) {
        PassiveUpgrades.Clear();
        PassiveUpgrades.Load();

        var f = new FormAttribute(Configuration, PassiveUpgrades);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    #region Equipment Set

    private void MenuEquipmentSet_Click(object sender, EventArgs e) {
        EquipmentSets.Clear();
        EquipmentSets.Load();

        var f = new FormEquipmentSet(Configuration, EquipmentSets);

        ChangeFont(f);

        f.Show();
    }

    private void MenuEquipmentSetAttribute_Click(object sender, EventArgs e) {
        EquipmentSetAttributes.Clear();
        EquipmentSetAttributes.Load();

        var f = new FormAttribute(Configuration, EquipmentSetAttributes);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    #region Heraldries

    private void MenuHeraldry_Click(object sender, EventArgs e) {
        Heraldries.Clear();
        Heraldries.Load();

        var f = new FormHeraldry(Configuration, Heraldries);

        ChangeFont(f);

        f.Show();
    }

    private void MenuHeraldryAttribute_Click(object sender, EventArgs e) {
        HeraldryAttributes.Clear();
        HeraldryAttributes.Load();

        var f = new FormAttribute(Configuration, HeraldryAttributes);

        ChangeFont(f);

        f.Show();
    }

    private void MenuHeraldryUpgrade_Click(object sender, EventArgs e) {
        HeraldryUpgrades.Clear();
        HeraldryUpgrades.Load();

        var f = new FormAttribute(Configuration, HeraldryUpgrades);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    #region Equipments

    private void MenuEquipment_Click(object sender, EventArgs e) {
        Equipments.Clear();
        Equipments.Load();

        var f = new FormEquipment(Configuration, Equipments);

        ChangeFont(f);

        f.Show();
    }

    private void MenuEquipmentAttribute_Click(object sender, EventArgs e) {
        EquipmentAttributes.Clear();
        EquipmentAttributes.Load();

        var f = new FormAttribute(Configuration, EquipmentAttributes);

        ChangeFont(f);

        f.Show();
    }

    private void MenuEquipmentUpgrade_Click(object sender, EventArgs e) {
        EquipmentUpgrades.Clear();
        EquipmentUpgrades.Load();

        var f = new FormAttribute(Configuration, EquipmentUpgrades);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    #region Skills

    private void MenuSkill_Click(object sender, EventArgs e) {
        Skills.Clear();
        Skills.Load();

        var f = new FormSkill(Configuration, Skills);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    #region Items

    private void MenuItem_Click(object sender, EventArgs e) {
        Items.Clear();
        Items.Load();

        var f = new FormItem(Configuration, Items);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    #region Npcs

    private void MenuNpc_Click(object sender, EventArgs e) {
        Npcs.Clear();
        Npcs.Load();

        var f = new FormNpc(Configuration, Npcs);

        ChangeFont(f);

        f.Show();
    }

    private void MenuNpcAttribute_Click(object sender, EventArgs e) {
        NpcAttributes.Clear();
        NpcAttributes.Load();

        var f = new FormAttribute(Configuration, NpcAttributes);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    #region Notification Icons

    private void MenuInformationIcon_Click(object sender, EventArgs e) {
        NotificationIcons.Clear();
        NotificationIcons.Load();

        var f = new FormNotificationIcon(Configuration, NotificationIcons);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    #region Conversations

    private void MenuConversation_Click(object sender, EventArgs e) {
        Conversations.Clear();
        Conversations.Load();

        var f = new FormConversation(Configuration, Conversations);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    #region Quests

    private void MenuQuest_Click(object sender, EventArgs e) {
        Quests.Clear();
        Quests.Load();

        var f = new FormQuest(Configuration, Quests);

        ChangeFont(f);

        f.Show();
    }

    #endregion

    private void ChangeFont(Control control) {
        var controls = control.Controls;

        ChangeFontStye(control);

        if (control is MenuStrip) {
            var menu = control as MenuStrip;

            if (menu is not null) {
                foreach (ToolStripItem item in menu.Items) {
                    ChangeFontStye(item);
                }
            }
        }

        foreach (Control _control in controls) {
            ChangeFont(_control);
        }
    }

    private void ChangeFontStye(Control control) {
        if (JetBrainsMono is not null) {
            control.Font = JetBrainsMono.GetFont(FontStyle.Regular);
        }
    }

    private void ChangeFontStye(ToolStripItem control) {
        if (JetBrainsMono is not null) {
            control.Font = JetBrainsMono.GetFont(FontStyle.Regular);
        }
    }

    private void FormMain_Load(object sender, EventArgs e) {
        ChangeFont(this);
    }
}
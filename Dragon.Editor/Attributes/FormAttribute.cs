using Dragon.Core.Content;
using Dragon.Core.Model;
using Dragon.Core.Model.Attributes;

using Dragon.Editor.Common;

namespace Dragon.Editor.Attributes;

public partial class FormAttribute : Form {
    public IDatabase<GroupAttribute> Database { get; }
    public GroupAttribute? Element { get; private set; }
    public Configuration Configuration { get; private set; }
    public int SelectedIndex { get; private set; } = Util.NotSelected;

    private readonly Label[] LabelPrimary;
    private readonly TextBox[] TextPrimary;
    private readonly CheckBox[] CheckPrimary;

    private readonly Label[] LabelElementalDamage;
    private readonly TextBox[] TextElementalDamage;
    private readonly CheckBox[] CheckElementalDamage;

    private readonly Label[] LabelElementalResistance;
    private readonly TextBox[] TextElementalResistance;
    private readonly CheckBox[] CheckElementalResistance;

    public FormAttribute(Configuration configuration, IDatabase<GroupAttribute> database) {
        InitializeComponent();

        var names = Enum.GetNames<PrimaryAttribute>();

        LabelPrimary = new Label[names.Length];
        LabelPrimary[0] = LabelPrimary_0;
        LabelPrimary[1] = LabelPrimary_1;
        LabelPrimary[2] = LabelPrimary_2;
        LabelPrimary[3] = LabelPrimary_3;
        LabelPrimary[4] = LabelPrimary_4;
        LabelPrimary[5] = LabelPrimary_5;

        TextPrimary = new TextBox[names.Length];
        TextPrimary[0] = TextPrimary_0;
        TextPrimary[1] = TextPrimary_1;
        TextPrimary[2] = TextPrimary_2;
        TextPrimary[3] = TextPrimary_3;
        TextPrimary[4] = TextPrimary_4;
        TextPrimary[5] = TextPrimary_5;

        CheckPrimary = new CheckBox[names.Length];
        CheckPrimary[0] = CheckPrimary_0;
        CheckPrimary[1] = CheckPrimary_1;
        CheckPrimary[2] = CheckPrimary_2;
        CheckPrimary[3] = CheckPrimary_3;
        CheckPrimary[4] = CheckPrimary_4;
        CheckPrimary[5] = CheckPrimary_5;

        names = Enum.GetNames<ElementAttribute>();
        LabelElementalDamage = new Label[names.Length];
        LabelElementalDamage[1] = LabelElementalDamage_1;
        LabelElementalDamage[2] = LabelElementalDamage_2;
        LabelElementalDamage[3] = LabelElementalDamage_3;
        LabelElementalDamage[4] = LabelElementalDamage_4;
        LabelElementalDamage[5] = LabelElementalDamage_5;
        LabelElementalDamage[6] = LabelElementalDamage_6;

        TextElementalDamage = new TextBox[names.Length];
        TextElementalDamage[1] = TextElementalDamage_1;
        TextElementalDamage[2] = TextElementalDamage_2;
        TextElementalDamage[3] = TextElementalDamage_3;
        TextElementalDamage[4] = TextElementalDamage_4;
        TextElementalDamage[5] = TextElementalDamage_5;
        TextElementalDamage[6] = TextElementalDamage_6;

        CheckElementalDamage = new CheckBox[names.Length];
        CheckElementalDamage[1] = CheckElementalDamage_1;
        CheckElementalDamage[2] = CheckElementalDamage_2;
        CheckElementalDamage[3] = CheckElementalDamage_3;
        CheckElementalDamage[4] = CheckElementalDamage_4;
        CheckElementalDamage[5] = CheckElementalDamage_5;
        CheckElementalDamage[6] = CheckElementalDamage_6;

        LabelElementalResistance = new Label[names.Length];
        LabelElementalResistance[1] = LabelElementalResistance_1;
        LabelElementalResistance[2] = LabelElementalResistance_2;
        LabelElementalResistance[3] = LabelElementalResistance_3;
        LabelElementalResistance[4] = LabelElementalResistance_4;
        LabelElementalResistance[5] = LabelElementalResistance_5;
        LabelElementalResistance[6] = LabelElementalResistance_6;

        TextElementalResistance = new TextBox[names.Length];
        TextElementalResistance[1] = TextElementalResistance_1;
        TextElementalResistance[2] = TextElementalResistance_2;
        TextElementalResistance[3] = TextElementalResistance_3;
        TextElementalResistance[4] = TextElementalResistance_4;
        TextElementalResistance[5] = TextElementalResistance_5;
        TextElementalResistance[6] = TextElementalResistance_6;

        CheckElementalResistance = new CheckBox[names.Length];
        CheckElementalResistance[1] = CheckElementalResistance_1;
        CheckElementalResistance[2] = CheckElementalResistance_2;
        CheckElementalResistance[3] = CheckElementalResistance_3;
        CheckElementalResistance[4] = CheckElementalResistance_4;
        CheckElementalResistance[5] = CheckElementalResistance_5;
        CheckElementalResistance[6] = CheckElementalResistance_6;

        SetEnabled(false);

        Database = database;
        Configuration = configuration;
        Element = null;

        Util.UpdateList(Database, ListIndex);
    }

    private void MenuSave_Click(object sender, EventArgs e) {
        var folder = Database.Folder;

        Database.Save();

        Database.Folder = Configuration.OutputClientPath;

        Database.Save();

        Database.Folder = Configuration.OutputServerPath;

        Database.Save();

        Database.Folder = folder;

        MessageBox.Show("Saved");
    }

    private void MenuExit_Click(object sender, EventArgs e) {
        Close();
    }

    private void Initialize() {
        if (Element is not null) {
            TextId.Text = Element.Id.ToString();
            TextName.Text = Element.Name;
            TextDescription.Text = Element.Description;

            var names = Enum.GetNames<PrimaryAttribute>();

            for (var i = 0; i < names.Length; i++) {
                var attribute = Element.Primary[(PrimaryAttribute)i];

                TextPrimary[i].Text = attribute.GetIntergerValue().ToString();
                CheckPrimary[i].Checked = attribute.Percentage;
                LabelPrimary[i].Text = names[i] + ": " + attribute.GetValueText();
            }

            SetElement(TextAttack, CheckAttack, LabelAttack, "Attack: ", Element.Secondary[SecondaryAttribute.Attack]);
            SetElement(TextDefense, CheckDefense, LabelDefense, "Defense: ", Element.Secondary[SecondaryAttribute.Defense]);
            SetElement(TextAccuracy, CheckAccuracy, LabelAccuracy, "Accuracy: ", Element.Secondary[SecondaryAttribute.Accuracy]);
            SetElement(TextEvasion, CheckEvasion, LabelEvasion, "Evasion: ", Element.Secondary[SecondaryAttribute.Evasion]);
            SetElement(TextParry, CheckParry, LabelParry, "Parry: ", Element.Secondary[SecondaryAttribute.Parry]);
            SetElement(TextBlock, CheckBlock, LabelBlock, "Block: ", Element.Secondary[SecondaryAttribute.Block]);

            SetElement(TextMagicAttack, CheckMagicAttack, LabelMagicAttack, "Magic Attack: ", Element.Secondary[SecondaryAttribute.MagicAttack]);
            SetElement(TextMagicDefense, CheckMagicDefense, LabelMagicDefense, "Magic Defense: ", Element.Secondary[SecondaryAttribute.MagicDefense]);
            SetElement(TextMagicAccuracy, CheckMagicAccuracy, LabelMagicAccuracy, "Magic Accuracy: ", Element.Secondary[SecondaryAttribute.MagicAccuracy]);
            SetElement(TextMagicResist, CheckMagicResist, LabelMagicResist, "Magic Resist: ", Element.Secondary[SecondaryAttribute.MagicResist]);
            SetElement(TextConcentration, CheckConcentration, LabelConcentration, "Concentration: ", Element.Secondary[SecondaryAttribute.Concentration]);

            SetElement(TextCritRate, LabelCritRate, "Crit. Rate: ", Element.Unique[UniqueAttribute.CritRate]);
            SetElement(TextCritDamage, LabelCritDamage, "Crit. Damage: ", Element.Unique[UniqueAttribute.CritDamage]);
            SetElement(TextResistCritRate, LabelResistCritRate, "Resist Crit. Rate: ", Element.Unique[UniqueAttribute.ResistCritRate]);
            SetElement(TextResistCritDamage, LabelResistCritDamage, "Resist Crit. Damage: ", Element.Unique[UniqueAttribute.ResistCritDamage]);

            names = Enum.GetNames<ElementAttribute>();

            for (var i = 1; i < names.Length; i++) {
                var attribute = Element.ElementAttack[(ElementAttribute)i];

                TextElementalDamage[i].Text = attribute.GetIntergerValue().ToString();
                CheckElementalDamage[i].Checked = attribute.Percentage;
                LabelElementalDamage[i].Text = names[i] + " Damage: " + attribute.GetValueText();

                attribute = Element.ElementDefense[(ElementAttribute)i];

                TextElementalResistance[i].Text = attribute.GetIntergerValue().ToString();
                CheckElementalResistance[i].Checked = attribute.Percentage;
                LabelElementalResistance[i].Text = names[i] + " Resistance: " + attribute.GetValueText();
            }

            SetElement(TextSilence, CheckSilence, LabelResistSilence, "Resist Silence: ", Element.Secondary[SecondaryAttribute.SilenceResistance]);
            SetElement(TextBlind, CheckBlind, LabelResistBlind, "Resist Blind: ", Element.Secondary[SecondaryAttribute.BlindResistance]);
            SetElement(TextStun, CheckStun, LabelResistStun, "Resist Stun: ", Element.Secondary[SecondaryAttribute.StunResistance]);
            SetElement(TextStumble, CheckStumble, LabelResistStumble, "Resist Stumble: ", Element.Secondary[SecondaryAttribute.StumbleResistance]);

            SetElement(TextHp, CheckHp, LabelHp, "Maximum Hp: ", Element.Vital[Vital.HP]);
            SetElement(TextMp, CheckMp, LabelMp, "Maximum Mp: ", Element.Vital[Vital.MP]);

            SetElement(TextHealing, LabelHealing, "Healing Power: ", Element.Unique[UniqueAttribute.HealingPower]);
            SetElement(TextFinalDamage, LabelFinalDamage, "Final Damage: ", Element.Unique[UniqueAttribute.FinalDamage]);
            SetElement(TextAmplification, LabelAmplification, "Amplification: ", Element.Unique[UniqueAttribute.Amplification]);
            SetElement(TextEnmity, LabelEnmity, "Enmity: ", Element.Unique[UniqueAttribute.Enmity]);
            SetElement(TextAttackSpeed, LabelAttackSpeed, "Attack Speed: ", Element.Unique[UniqueAttribute.AttackSpeed]);
            SetElement(TextDamageSuppression, LabelDamageSuppression, "Damage Suppression: ", Element.Unique[UniqueAttribute.DamageSuppression]);
            SetElement(TextPveAttack, LabelPveAttack, "PvE Attack: ", Element.Unique[UniqueAttribute.PveAttack]);
            SetElement(TextPveDefense, LabelPveDefense, "PvE Defense: ", Element.Unique[UniqueAttribute.PveDefense]);
            SetElement(TextPvpAttack, LabelPvpAttack, "PvP Attack: ", Element.Unique[UniqueAttribute.PvpAttack]);
            SetElement(TextPvpDefense, LabelPvpDefense, "PvP Defense: ", Element.Unique[UniqueAttribute.PvpDefense]);
            SetElement(TextCastSpeed, LabelCastSpeed, "Cast Speed: ", Element.Unique[UniqueAttribute.CastSpeed]);
        }
    }

    private void Clear() {
        TextId.Text = "0";
        TextName.Text = string.Empty;
        TextDescription.Text = string.Empty;

        var names = Enum.GetNames<PrimaryAttribute>();

        for (var i = 0; i < names.Length; i++) {
            LabelPrimary[i].Text = names[i] + ": 0";
        }

        Util.FillTextBoxWithZero(GroupPrimary);
        Util.FillTextBoxWithZero(GroupPhysic);
        Util.FillTextBoxWithZero(GroupMagic);
        Util.FillTextBoxWithZero(GroupCritical);
        Util.FillTextBoxWithZero(GroupElemental);
        Util.FillTextBoxWithZero(GroupResistances);
        Util.FillTextBoxWithZero(GroupMiscellaneous);

        Util.ResetCheckBox(GroupPrimary);
        Util.ResetCheckBox(GroupPhysic);
        Util.ResetCheckBox(GroupMagic);
        Util.ResetCheckBox(GroupCritical);
        Util.ResetCheckBox(GroupElemental);
        Util.ResetCheckBox(GroupResistances);
        Util.ResetCheckBox(GroupMiscellaneous);

        LabelAttack.Text = "Attack: 0";
        LabelDefense.Text = "Defense: 0";
        LabelAccuracy.Text = "Accuracy: 0";
        LabelEvasion.Text = "Evasion: 0";
        LabelParry.Text = "Parry: 0";
        LabelBlock.Text = "Block: 0";

        LabelMagicAttack.Text = "Magic Attack: 0";
        LabelMagicDefense.Text = "Magic Defense: 0";
        LabelMagicAccuracy.Text = "Magic Accuracy: 0";
        LabelMagicResist.Text = "Magic Resist: 0";
        LabelConcentration.Text = "Concentration: 0";

        LabelCritRate.Text = "Crit. Rate: 0%";
        LabelCritDamage.Text = "Crit. Damage: 0%";
        LabelResistCritRate.Text = "Resist Crit. Rate: 0%";
        LabelResistCritDamage.Text = "Resist Crit. Damage: 0%";

        names = Enum.GetNames<ElementAttribute>();

        for (var i = 1; i < names.Length; i++) {
            LabelElementalDamage[i].Text = names[i] + " Damage: 0";
            LabelElementalResistance[i].Text = names[i] + " Resistance: 0";
        }

        LabelResistSilence.Text = "Resist Silence: 0";
        LabelResistBlind.Text = "Resist Blind: 0";
        LabelResistStun.Text = "Resist Stun: 0";
        LabelResistStumble.Text = "Resist Stumble: 0";

        LabelHp.Text = "Maximum Hp: 0";
        LabelMp.Text = "Maximum Mp: 0";
        LabelHealing.Text = "Healing Power: 0%";
        LabelFinalDamage.Text = "Final Damage: 0%";
        LabelAmplification.Text = "Amplification: 0%";
        LabelEnmity.Text = "Enmity: 0%";
        LabelAttackSpeed.Text = "Attack Speed: 0%";
        LabelDamageSuppression.Text = "Damage Suppression: 0%";
        LabelPveAttack.Text = "PvE Attack: 0%";
        LabelPveDefense.Text = "PvE Defense: 0%";
        LabelPvpAttack.Text = "PvP Attack: 0%";
        LabelPvpDefense.Text = "PvP Defense: 0%";
        LabelCastSpeed.Text = "Cast Speed: 0%";
    }

    private void SetElement(TextBox textBox, CheckBox checkBox, Label label, string text, SingleAttribute attribute) {
        textBox.Text = attribute.GetIntergerValue().ToString();
        checkBox.Checked = attribute.Percentage;
        label.Text = text + attribute.GetValueText();
    }

    private void SetElement(TextBox textBox, Label label, string text, float value) {
        textBox.Text = Convert.ToInt32(value * 100).ToString();
        label.Text = text + textBox.Text + "%";
    }

    private void SetEnabled(bool enabled) {
        TabAttributes.Enabled = enabled;
    }

    #region Add, Delete, Clear

    private void ButtonAdd_Click(object sender, EventArgs e) {
        for (var i = 1; i <= int.MaxValue; i++) {
            if (!Database.Contains(i)) {

                Database.Add(i, new GroupAttribute() { Id = i });

                Util.UpdateList(Database, ListIndex);

                return;
            }
        }
    }

    private void ButtonDelete_Click(object sender, EventArgs e) {
        if (Element is not null) {
            if (Element.Id > 0) {
                Database.Remove(Element.Id);

                SetEnabled(false);
                Element = null;

                Clear();

                SelectedIndex = Util.NotSelected;

                Util.UpdateList(Database, ListIndex);
            }
        }
    }

    private void ButtonClear_Click(object sender, EventArgs e) {
        SelectedIndex = Util.NotSelected;
        ListIndex.Items.Clear();
        SetEnabled(false);
        Database.Clear();
        Element = null;
        Clear();
    }

    #endregion

    #region List Index

    private void ListIndex_Click(object sender, EventArgs e) {
        if (ListIndex.Items.Count > 0) {
            var index = ListIndex.SelectedIndex;

            if (index > Util.NotSelected) {
                var selected = ListIndex.Items[index].ToString();

                if (selected is not null) {
                    var id = Util.GetListSelectedId(selected);

                    if (id > 0) {
                        SelectedIndex = index;
                        Element = Database[id];
                        SetEnabled(true);
                        Initialize();
                    }
                }
            }
        }
    }

    #endregion

    #region Attribute Data

    private void TextId_Validated(object sender, EventArgs e) {
        if (Element is not null) {
            var lastId = Element.Id;
            var id = Util.GetValue(TextId);

            if (id > 0) {
                if (id == lastId) {
                    return;
                }

                if (Database.Contains(id)) {
                    MessageBox.Show($"The Id {id} is already in use.");
                }
                else {
                    Database.Remove(Element.Id);
                    Element.Id = id;
                    Database.Add(id, Element);

                    Util.UpdateList(Database, ListIndex);
                }
            }
            else {
                MessageBox.Show($"Maybe failed to get Id. Any Id cannot be zero.");
            }
        }
    }

    private void TextName_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Name = TextName.Text;

            if (SelectedIndex > Util.NotSelected) {
                ListIndex.Items[SelectedIndex] = $"{Element.Id}: {Element.Name}";
            }
        }
    }

    private void TextDescription_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Description = TextDescription.Text;
        }
    }

    #endregion

    #region Attribute Primary 

    private void TextPrimary_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var type = (PrimaryAttribute)Convert.ToInt32(((TextBox)sender).Name.Replace("TextPrimary_", string.Empty));
            var value = Util.GetValue((TextBox)sender);

            var attribute = Element.Primary[type];

            attribute.SetValue(value);

            Element.Primary[type] = attribute;

            var names = Enum.GetNames<PrimaryAttribute>();
            LabelPrimary[(int)type].Text = names[(int)type] + ": " + attribute.GetValueText();
        }
    }

    private void CheckPrimary_Click(object sender, EventArgs e) {
        if (Element is not null) {
            var type = (PrimaryAttribute)Convert.ToInt32(((CheckBox)sender).Name.Replace("CheckPrimary_", string.Empty));
            var value = ((CheckBox)sender).Checked;

            var attribute = Element.Primary[type];

            attribute.Percentage = value;
            attribute.ConvertValue();

            Element.Primary[type] = attribute;

            var names = Enum.GetNames<PrimaryAttribute>();
            LabelPrimary[(int)type].Text = names[(int)type] + ": " + attribute.GetValueText();
        }
    }

    #endregion

    #region Attribute Physic

    #region TextBox

    private void TextAttack_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Attack;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;
            LabelAttack.Text = "Attack: " + attribute.GetValueText();
        }
    }

    private void TextDefense_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Defense;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;

            LabelDefense.Text = "Defense: " + attribute.GetValueText();
        }
    }

    private void TextAccuracy_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Accuracy;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;

            LabelAccuracy.Text = "Accuracy: " + attribute.GetValueText();
        }
    }

    private void TextEvasion_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Evasion;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;

            LabelEvasion.Text = "Evasion: " + attribute.GetValueText();
        }
    }

    private void TextParry_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Parry;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;

            LabelParry.Text = "Parry: " + attribute.GetValueText();
        }
    }

    private void TextBlock_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Block;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;

            LabelBlock.Text = "Block: " + attribute.GetValueText();
        }
    }

    #endregion

    #region CheckBox

    private void CheckAttack_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Attack;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelAttack.Text = "Attack: " + attribute.GetValueText();
        }
    }

    private void CheckDefense_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Defense;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelDefense.Text = "Defense: " + attribute.GetValueText();
        }
    }

    private void CheckAccuracy_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Accuracy;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelAccuracy.Text = "Accuracy: " + attribute.GetValueText();
        }
    }

    private void CheckEvasion_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Evasion;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelEvasion.Text = "Evasion: " + attribute.GetValueText();
        }
    }

    private void CheckParry_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Parry;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelParry.Text = "Parry: " + attribute.GetValueText();
        }
    }

    private void CheckBlock_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Block;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelBlock.Text = "Block: " + attribute.GetValueText();
        }
    }

    #endregion

    #endregion

    #region Attribute Magic

    #region TextBox

    private void TextMagicAttack_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.MagicAttack;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;
            LabelMagicAttack.Text = "Magic Attack: " + attribute.GetValueText();
        }
    }

    private void TextMagicDefense_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.MagicDefense;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;
            LabelMagicDefense.Text = "Magic Defense: " + attribute.GetValueText();
        }
    }

    private void TextMagicAccuracy_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.MagicAccuracy;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;
            LabelMagicAccuracy.Text = "Magic Accuracy: " + attribute.GetValueText();
        }
    }

    private void TextMagicResist_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.MagicResist;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;
            LabelMagicResist.Text = "Magic Resist: " + attribute.GetValueText();
        }
    }

    private void TextConcentration_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Concentration;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;
            LabelConcentration.Text = "Concentration: " + attribute.GetValueText();
        }
    }

    #endregion

    #region CheckBox

    private void CheckMagicAttack_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.MagicAttack;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelMagicAttack.Text = "Magic Attack: " + attribute.GetValueText();
        }
    }

    private void CheckMagicDefense_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.MagicDefense;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelMagicDefense.Text = "Magic Defense: " + attribute.GetValueText();
        }
    }

    private void CheckMagicAccuracy_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.MagicAccuracy;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelMagicAccuracy.Text = "Magic Accuracy: " + attribute.GetValueText();
        }
    }

    private void CheckMagicResist_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.MagicResist;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelMagicResist.Text = "Magic Resist: " + attribute.GetValueText();
        }
    }

    private void CheckConcentration_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.Concentration;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelConcentration.Text = "Concentration: " + attribute.GetValueText();
        }
    }

    #endregion

    #endregion

    #region Attribute Critical

    private void TextCritRate_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.CritRate] = value / 100f;

            LabelCritRate.Text = $"Crit. Rate: {value}%";
        }
    }

    private void TextCritDamage_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.CritDamage] = value / 100f;

            LabelCritDamage.Text = $"Crit. Damage: {value}%";
        }
    }

    private void TextResistCritRate_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.ResistCritRate] = value / 100f;

            LabelResistCritRate.Text = $"Resist Crit. Rate: {value}%";
        }
    }

    private void TextResistCritDamage_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.ResistCritDamage] = value / 100f;

            LabelResistCritDamage.Text = $"Resist Crit. Damage: {value}%";
        }
    }


    #endregion

    #region Attribute Elemental

    private void TextElementalDamage_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var type = (ElementAttribute)Convert.ToInt32(((TextBox)sender).Name.Replace("TextElementalDamage_", string.Empty));
            var value = Util.GetValue((TextBox)sender);

            var attribute = Element.ElementAttack[type];

            attribute.SetValue(value);

            Element.ElementAttack[type] = attribute;

            var names = Enum.GetNames<ElementAttribute>();
            LabelElementalDamage[(int)type].Text = names[(int)type] + " Damage: " + attribute.GetValueText();
        }
    }

    private void TextElementalResistance_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var type = (ElementAttribute)Convert.ToInt32(((TextBox)sender).Name.Replace("TextElementalResistance_", string.Empty));
            var value = Util.GetValue((TextBox)sender);

            var attribute = Element.ElementDefense[type];

            attribute.SetValue(value);

            Element.ElementDefense[type] = attribute;

            var names = Enum.GetNames<ElementAttribute>();
            LabelElementalResistance[(int)type].Text = names[(int)type] + " Resistance: " + attribute.GetValueText();
        }
    }

    private void CheckElementalDamage_Click(object sender, EventArgs e) {
        if (Element is not null) {
            var type = (ElementAttribute)Convert.ToInt32(((CheckBox)sender).Name.Replace("CheckElementalDamage_", string.Empty));
            var value = ((CheckBox)sender).Checked;

            var attribute = Element.ElementAttack[type];

            attribute.Percentage = value;
            attribute.ConvertValue();

            Element.ElementAttack[type] = attribute;

            var names = Enum.GetNames<ElementAttribute>();
            LabelElementalDamage[(int)type].Text = names[(int)type] + " Damage: " + attribute.GetValueText();
        }
    }

    private void CheckElementalResistance_Click(object sender, EventArgs e) {
        if (Element is not null) {
            var type = (ElementAttribute)Convert.ToInt32(((CheckBox)sender).Name.Replace("CheckElementalResistance_", string.Empty));
            var value = ((CheckBox)sender).Checked;

            var attribute = Element.ElementDefense[type];

            attribute.Percentage = value;
            attribute.ConvertValue();

            Element.ElementDefense[type] = attribute;

            var names = Enum.GetNames<ElementAttribute>();
            LabelElementalResistance[(int)type].Text = names[(int)type] + " Resistance: " + attribute.GetValueText();
        }
    }

    #endregion

    #region Attribute Resistances

    #region TextBox

    private void TextSilence_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.SilenceResistance;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;
            LabelResistSilence.Text = "Resist Silence: " + attribute.GetValueText();
        }
    }

    private void TextBlind_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.BlindResistance;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;
            LabelResistBlind.Text = "Resist Blind: " + attribute.GetValueText();
        }
    }

    private void TextStun_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.StunResistance;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;
            LabelResistStun.Text = "Resist Stun: " + attribute.GetValueText();
        }
    }

    private void TextStumble_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.StumbleResistance;
            var attribute = Element.Secondary[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Secondary[index] = attribute;
            LabelResistStumble.Text = "Resist Stumble: " + attribute.GetValueText();
        }
    }

    #endregion

    #region CheckBox

    private void CheckSilence_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.SilenceResistance;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelResistSilence.Text = "Resist Silence: " + attribute.GetValueText();
        }
    }

    private void CheckBlind_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.BlindResistance;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelResistBlind.Text = "Resist Blind: " + attribute.GetValueText();
        }
    }

    private void CheckStun_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.StunResistance;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelResistStun.Text = "Resist Stun: " + attribute.GetValueText();
        }
    }

    private void CheckStumble_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = SecondaryAttribute.StumbleResistance;
            var attribute = Element.Secondary[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Secondary[index] = attribute;
            LabelResistStumble.Text = "Resist Stumble: " + attribute.GetValueText();
        }
    }

    #endregion

    #endregion

    #region Attribute Miscellaneous

    #region Vital

    private void TextHp_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = Vital.HP;
            var attribute = Element.Vital[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Vital[index] = attribute;
            LabelHp.Text = "Maximum Hp: " + attribute.GetValueText();
        }
    }

    private void TextMp_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = Vital.MP;
            var attribute = Element.Vital[index];

            attribute.SetValue(Util.GetValue((TextBox)sender));

            Element.Vital[index] = attribute;
            LabelMp.Text = "Maximum Mp: " + attribute.GetValueText();
        }
    }

    private void CheckHp_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = Vital.HP;
            var attribute = Element.Vital[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Vital[index] = attribute;
            LabelHp.Text = "Maximum Hp: " + attribute.GetValueText();
        }
    }

    private void CheckMp_CheckedChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var index = Vital.MP;
            var attribute = Element.Vital[index];

            attribute.Percentage = ((CheckBox)sender).Checked;
            attribute.ConvertValue();

            Element.Vital[index] = attribute;
            LabelMp.Text = "Maximum Mp: " + attribute.GetValueText();
        }
    }

    #endregion

    private void TextHealing_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.HealingPower] = value / 100f;

            LabelHealing.Text = $"Healing Power: {value}%";
        }
    }

    private void TextFinalDamage_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.FinalDamage] = value / 100f;

            LabelFinalDamage.Text = $"Final Damage: {value}%";
        }
    }

    private void TextAmplification_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.Amplification] = value / 100f;

            LabelAmplification.Text = $"Amplification: {value}%";
        }
    }

    private void TextEnmity_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.Enmity] = value / 100f;

            LabelEnmity.Text = $"Enmity: {value}%";
        }
    }

    private void TextAttackSpeed_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.AttackSpeed] = value / 100f;

            LabelAttackSpeed.Text = $"Attack Speed: {value}%";
        }
    }

    private void TextDamageSuppression_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.DamageSuppression] = value / 100f;

            LabelDamageSuppression.Text = $"Damage Suppression: {value}%";
        }
    }

    private void TextPveAttack_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.PveAttack] = value / 100f;

            LabelPveAttack.Text = $"PvE Attack: {value}%";
        }
    }

    private void TextPveDefense_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.PveDefense] = value / 100f;

            LabelPveDefense.Text = $"PvE Defense: {value}%";
        }
    }

    private void TextPvpAttack_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.PvpAttack] = value / 100f;

            LabelPvpAttack.Text = $"PvP Attack: {value}%";
        }
    }

    private void TextPvpDefense_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.PvpDefense] = value / 100f;

            LabelPvpDefense.Text = $"PvP Defense: {value}%";
        }
    }

    private void TextCastSpeed_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            Element.Unique[UniqueAttribute.CastSpeed] = value / 100f;

            LabelCastSpeed.Text = $"Cast Speed: {value}%";
        }
    }

    #endregion
}
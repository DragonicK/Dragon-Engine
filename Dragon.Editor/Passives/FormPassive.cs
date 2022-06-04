using Dragon.Core.Content;
using Dragon.Core.Model;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Passives;

using Dragon.Editor.Common;

namespace Dragon.Editor.Passives;

public partial class FormPassive : Form {
    public IDatabase<Passive> Database { get; }
    public Passive? Element { get; private set; }
    public Configuration Configuration { get; private set; }
    public int SelectedIndex { get; private set; } = Util.NotSelected;

    public FormPassive(Configuration configuration, IDatabase<Passive> database) {
        InitializeComponent();

        SetEnabled(false);

        Util.FillComboBox<PassiveType>(ComboType);

        Util.FillComboBox<SkillTargetType>(ComboTarget);
        Util.FillComboBox<ElementAttribute>(ComboElement);

        Util.FillComboBox<PassiveEffectChange>(ComboEffectChange);
        Util.FillComboBox<SkillEffectType>(ComboSkillEffect);
        Util.FillComboBox<SkillVitalType>(ComboTargetVital);
        Util.FillComboBox<SkillTargetType>(ComboTarget_);

        Util.FillComboBox<PassiveActivation>(ComboActivation);
        Util.FillComboBox<PassiveConditional>(ComboConditional);
        Util.FillComboBox<PassiveActivationResult>(ComboActivationResult);

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
            TextAttributeId.Text = Element.AttributeId.ToString();
            TextUpgradeId.Text = Element.UpgradeId.ToString();
            ComboType.SelectedIndex = (int)Element.PassiveType;

            ComboTarget.SelectedIndex = (int)Element.TargetType;
            ComboElement.SelectedIndex = (int)Element.Element;
            TextAmplification.Text = Convert.ToInt32(Element.Amplification * 100).ToString();
            TextRange.Text = Element.Range.ToString();
            TextCastingTime.Text = Element.CastTime.ToString();
            TextCooldownTime.Text = Element.Cooldown.ToString();
            TextStunDuration.Text = Element.Stun.ToString();
            TextCost.Text = Element.Cost.ToString();
            TextSkillBaseId.Text = Element.SkillId.ToString();

            ComboEffectChange.SelectedIndex = (int)Element.EffectChange;
            ComboSkillEffect.SelectedIndex = (int)Element.Skill.EffectType;
            ComboTargetVital.SelectedIndex = (int)Element.Skill.VitalType;
            ComboTarget_.SelectedIndex = (int)Element.Skill.TargetType;
            TextDuration.Text = Element.Skill.Duration.ToString();
            TextInterval.Text = Element.Skill.Interval.ToString();
            TextDamage.Text = Element.Skill.Damage.ToString();
            TextDamagePerLevel.Text = Element.Skill.DamagePerLevel.ToString();
            TextSkillEffectId.Text = Element.SkillId.ToString();

            ComboActivation.SelectedIndex = (int)Element.Activation;
            ComboConditional.SelectedIndex = (int)Element.Conditional;
            ComboActivationResult.SelectedIndex = (int)Element.ActivationResult;
            LabelChance.Text = $"Activation Chance: {Element.ActivationChance}%";
            ScrollChance.Value = Element.ActivationChance;

            EnableByPassiveType();
        }
    }

    private void Clear() {
        TextId.Text = "0";
        TextName.Text = string.Empty;
        TextDescription.Text = string.Empty;
        TextAttributeId.Text = "0";
        TextUpgradeId.Text = "0";
        ComboType.SelectedIndex = 0;

        ComboTarget.SelectedIndex = 0;
        ComboElement.SelectedIndex = 0;

        Util.FillTextBoxWithZero(GroupSkill);

        ComboEffectChange.SelectedIndex = 0;
        ComboSkillEffect.SelectedIndex = 0;
        ComboTargetVital.SelectedIndex = 0;
        ComboTarget_.SelectedIndex = 0;

        Util.FillTextBoxWithZero(GroupSkillEffect);

        ComboActivation.SelectedIndex = 0;
        ComboConditional.SelectedIndex = 0;
        ComboActivationResult.SelectedIndex = 0;
        LabelChance.Text = $"Activation Chance: 0%";
        ScrollChance.Value = 0;

        GroupSkill.Enabled = true;
        GroupSkillEffect.Enabled = true;
        GroupConditional.Enabled = true;
    }

    private void SetEnabled(bool enabled) {
        TabPassive.Enabled = enabled;
    }

    #region Add, Delete, Clear

    private void ButtonAdd_Click(object sender, EventArgs e) {
        for (var i = 1; i <= int.MaxValue; i++) {
            if (!Database.Contains(i)) {

                Database.Add(i, new Passive() { Id = i });

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

    #region Passive Data

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
            Element.Description = ((TextBox)sender).Text;
        }
    }

    private void ComboType_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.PassiveType = (PassiveType)ComboType.SelectedIndex;

            EnableByPassiveType();
        }
    }

    private void TextAttributeId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.AttributeId = Util.GetValue((TextBox)sender);
        }
    }

    private void TextUpgradeId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.UpgradeId = Util.GetValue((TextBox)sender);
        }
    }

    #endregion

    #region Passive Skill

    private void ComboTargetType_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.TargetType = (SkillTargetType)ComboTarget.SelectedIndex;
        }
    }

    private void ComboElement_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Element = (ElementAttribute)ComboElement.SelectedIndex;
        }
    }

    private void TextAmplification_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Amplification = Util.GetValue((TextBox)sender) / 100f;
        }
    }

    private void TextRange_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            if (value > short.MaxValue) {
                value = short.MaxValue;
            }
            else if (value < short.MinValue) {
                value = short.MinValue;
            }

            Element.Range = (short)value;
        }
    }

    private void TextCastingTime_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            if (value > short.MaxValue) {
                value = short.MaxValue;
            }
            else if (value < short.MinValue) {
                value = short.MinValue;
            }

            Element.CastTime = (short)value;
        }
    }

    private void TextCooldownTime_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            if (value > short.MaxValue) {
                value = short.MaxValue;
            }
            else if (value < short.MinValue) {
                value = short.MinValue;
            }

            Element.Cooldown = (short)value;
        }
    }

    private void TextStunDuration_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            if (value > short.MaxValue) {
                value = short.MaxValue;
            }
            else if (value < short.MinValue) {
                value = short.MinValue;
            }

            Element.Stun = (short)value;
        }
    }

    private void TextCost_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue((TextBox)sender);

            if (value > short.MaxValue) {
                value = short.MaxValue;
            }
            else if (value < short.MinValue) {
                value = short.MinValue;
            }

            Element.Cost = (short)value;
        }
    }

    private void TextSkillBase_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.SkillId = Util.GetValue((TextBox)sender);
            TextSkillEffectId.Text = Element.SkillId.ToString();
        }
    }

    #endregion

    #region Passive Skill Effect

    private void ComboTargetVital_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Skill.VitalType = (SkillVitalType)ComboTargetVital.SelectedIndex;
        }
    }

    private void ComboEffectChange_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.EffectChange = (PassiveEffectChange)ComboEffectChange.SelectedIndex;
        }
    }

    private void ComboSkillEffect_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Skill.EffectType = (SkillEffectType)ComboSkillEffect.SelectedIndex;
        }
    }

    private void ComboTarget__SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Skill.TargetType = (SkillTargetType)ComboTarget_.SelectedIndex;
        }
    }

    private void TextDuration_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Skill.Duration = Util.GetValue((TextBox)sender);
        }
    }

    private void TextInterval_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Skill.Interval = Util.GetValue((TextBox)sender);
        }
    }

    private void TextDamage_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Skill.Damage = Util.GetValue((TextBox)sender);
        }
    }

    private void TextSkillEffectId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.SkillId = Util.GetValue((TextBox)sender);
            TextSkillBaseId.Text = Element.SkillId.ToString();
        }
    }

    private void TextDamagePerLevel_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Skill.DamagePerLevel = Util.GetValue((TextBox)sender);
        }
    }

    #endregion

    #region Passive Conditional

    private void ComboActivation_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Activation = (PassiveActivation)ComboActivation.SelectedIndex;
        }
    }

    private void ComboConditional_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Conditional = (PassiveConditional)ComboActivation.SelectedIndex;
        }
    }

    private void ComboActivationResult_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.ActivationResult = (PassiveActivationResult)ComboActivationResult.SelectedIndex;
        }
    }

    private void ScrollChance_Scroll(object sender, ScrollEventArgs e) {
        if (Element is not null) {
            var value = ScrollChance.Value;

            Element.ActivationChance = value;
            LabelChance.Text = $"Activation Chance: {value}%";
        }
    }

    #endregion

    private void EnableByPassiveType() {
        if (Element is not null) {
            if (Element.PassiveType == PassiveType.Attributes) {
                GroupAttribute.Enabled = true;
                GroupUpgrade.Enabled = true;
                GroupSkill.Enabled = false;
                GroupSkillEffect.Enabled = false;
                GroupConditional.Enabled = false;
            }
            else if (Element.PassiveType == PassiveType.Improvement) {
                GroupAttribute.Enabled = false;
                GroupUpgrade.Enabled = false;
                GroupSkill.Enabled = true;
                GroupSkillEffect.Enabled = true;
                GroupConditional.Enabled = false;
            }
            else if (Element.PassiveType == PassiveType.Activation) {
                GroupAttribute.Enabled = false;
                GroupUpgrade.Enabled = false;
                GroupSkill.Enabled = false;
                GroupSkillEffect.Enabled = false;
                GroupConditional.Enabled = true;
            }
        }
    }

}
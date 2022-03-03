using Crystalshire.Core.Model;
using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Skills;

using Crystalshire.Editor.Common;

namespace Crystalshire.Editor.Skills {
    public partial class FormSkill : Form {
        public IDatabase<Skill> Database { get; }
        public Skill? Element { get; private set; }
        public Configuration Configuration { get; private set; }
        public int SelectedIndex { get; private set; } = Util.NotSelected;

        public FormSkill(Configuration configuration, IDatabase<Skill> database) {
            InitializeComponent();

            SetEnabled(false);

            Util.FillComboBox<SkillType>(ComboType);
            Util.FillComboBox<SkillAttributeType>(ComboAttributeType);
            Util.FillComboBox<ElementAttribute>(ComboElementType);
            Util.FillComboBox<SkillTargetType>(ComboPrimaryTargetType);
            Util.FillComboBox<SkillEffectType>(ComboPrimaryEffectType);
            Util.FillComboBox<SkillCostType>(ComboCostType);

            Util.FillComboBox<SkillEffectType>(ComboEffectType);
            Util.FillComboBox<SkillTargetType>(ComboEffectTarget);
            Util.FillComboBox<SkillVitalType>(ComboTargetVitalType);

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

                ComboType.SelectedIndex = (int)Element.Type;
                TextSound.Text = Element.Sound;
                TextIconId.Text = Element.IconId.ToString();
                TextPassive.Text = Element.PassiveId.ToString();
                TextCastAnimation.Text = Element.CastAnimationId.ToString();
                TextAttackAnimation.Text = Element.AttackAnimationId.ToString();

                ComboAttributeType.SelectedIndex = (int)Element.AttributeType;
                ComboElementType.SelectedIndex = (int)Element.ElementType;
                ComboPrimaryTargetType.SelectedIndex = (int)Element.TargetType;
                ComboPrimaryEffectType.SelectedIndex = (int)Element.EffectType;
                ComboCostType.SelectedIndex = (int)Element.CostType;
                TextMaximumLevel.Text = Element.MaximumLevel.ToString();
                TextCost.Text = Element.Cost.ToString();
                TextCostPerLevel.Text = Element.CostPerLevel.ToString();

                TextAmplification.Text = Convert.ToInt32(Element.Amplification * 100).ToString();
                TextAmplificationPerLevel.Text = Convert.ToInt32(Element.AmplificationPerLevel * 100).ToString();
                TextRange.Text = Element.Range.ToString();
                TextCasting.Text = Element.CastTime.ToString();
                TextCooldown.Text = Element.Cooldown.ToString();
                TextStunDuration.Text = Element.StunDuration.ToString();

                UpdateIndexLabel();
                UpdateEffectText();
            }
        }

        private void Clear() {
            TextId.Text = "0";
            TextName.Text = string.Empty;
            TextDescription.Text = string.Empty;

            ComboType.SelectedIndex = 0;
            TextSound.Text = Util.None;
            TextPassive.Text = "0";
            TextIconId.Text = "0";
            TextCastAnimation.Text = "0";
            TextAttackAnimation.Text = "0";

            ComboAttributeType.SelectedIndex = 0;
            ComboElementType.SelectedIndex = 0;
            ComboPrimaryTargetType.SelectedIndex = 0;
            ComboPrimaryEffectType.SelectedIndex = 0;
            ComboCostType.SelectedIndex = 0;

            Util.FillTextBoxWithZero(GroupData2);
            Util.FillTextBoxWithZero(GroupAttribute);
            Util.FillTextBoxWithZero(GroupEffect);

            ScrollIndex.Minimum = 0;
            ScrollIndex.Value = 0;
            ScrollIndex.Maximum = 0;

            LabelIndex.Text = $"Index: 0 / 0";

            ComboEffectType.SelectedIndex = 0;
            ComboEffectTarget.SelectedIndex = 0;
            ComboTargetVitalType.SelectedIndex = 0;
            ScrollTrigger.Value = 0;
            LabelTrigger.Text = "Trigger Chance: 0%";
        }

        private void SetEnabled(bool enabled) {
            TabSkill.Enabled = enabled;
        }

        #region Add, Delete, Clear

        private void ButtonAdd_Click(object sender, EventArgs e) {
            for (var i = 1; i <= int.MaxValue; i++) {
                if (!Database.Contains(i)) {

                    Database.Add(i, new Skill() { Id = i });

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

        #region Skill Data

        private void TextId_Validated(object sender, EventArgs e) {
            if (Element is not null) {
                var lastId = Element.Id;
                var id = Util.GetValue((TextBox)sender);

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
                Element.Type = (SkillType)ComboType.SelectedIndex;
            }
        }

        private void TextPassive_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.PassiveId = Util.GetValue((TextBox)sender);
            }
        }

        private void TextSound_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Sound = ((TextBox)sender).Text;
            }
        }

        private void TextIconId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.IconId = Util.GetValue((TextBox)sender);
            }
        }

        private void TextCastAnimation_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.CastAnimationId = Util.GetValue((TextBox)sender);
            }
        }

        private void TextAttackAnimation_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.AttackAnimationId = Util.GetValue((TextBox)sender);
            }
        }


        #endregion

        #region Skill Data 2nd

        private void ComboAttributeType_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.AttributeType = (SkillAttributeType)((ComboBox)sender).SelectedIndex;
            }
        }

        private void ComboElementType_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.ElementType = (ElementAttribute)((ComboBox)sender).SelectedIndex;
            }
        }

        private void ComboTargetType_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.TargetType = (SkillTargetType)((ComboBox)sender).SelectedIndex;
            }
        }

        private void ComboPrimaryEffectType_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.EffectType = (SkillEffectType)((ComboBox)sender).SelectedIndex;
            }
        }

        private void ComboCostType_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.CostType = (SkillCostType)((ComboBox)sender).SelectedIndex;
            }
        }

        private void TextMaximumLevel_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.MaximumLevel = Util.GetValue((TextBox)sender);
            }
        }

        private void TextCost_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Cost = Util.GetValue((TextBox)sender);
            }
        }

        private void TextCostPerLevel_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.CostPerLevel = Util.GetValue((TextBox)sender);
            }
        }

        #endregion

        #region Skill Attribute

        private void TextAmplification_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Amplification = Util.GetValue((TextBox)sender) / 100f;
            }
        }

        private void TextAmplificationPerLevel_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.AmplificationPerLevel = Util.GetValue((TextBox)sender) / 100f;
            }
        }

        private void TextRange_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {            
                Element.Range = Util.GetValue((TextBox)sender);
            }
        }

        private void TextCasting_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.CastTime = Util.GetValue((TextBox)sender);
            }
        }

        private void TextCooldown_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Cooldown = Util.GetValue((TextBox)sender);
            }
        }

        private void TextStunDuration_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.StunDuration = Util.GetValue((TextBox)sender);
            }
        }

        #endregion

        #region Skill Effect

        private void ButtonEffectAdd_Click(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Effects.Add(new SkillEffect());

                UpdateIndexLabel();
                UpdateEffectText();
            }
        }

        private void ButtonEffectDelete_Click(object sender, EventArgs e) {
            if (Element is not null) {
                var index = ScrollIndex.Value;

                if (index > 0) {
                    index--;

                    if (Element.Effects.Count > index) {
                        Element.Effects.RemoveAt(index);

                        UpdateIndexLabel();
                        UpdateEffectText();
                    }
                }
            }
        }

        private void ScrollIndex_Scroll(object sender, ScrollEventArgs e) {
            if (Element is not null) {
                UpdateIndexLabel();
                UpdateEffectText();
            }
        }

        private bool IsValidIndex() {
            var index = ScrollIndex.Value;

            if (index > 0) {
                index--;

                if (Element!.Effects.Count > index) {
                    return true;
                }
            }

            return false;
        }

        private void UpdateIndexLabel() {
            var count = Element!.Effects.Count;

            if (count > 0) {
                ScrollIndex.Maximum = count;
                ScrollIndex.Minimum = 1;

                if (ScrollIndex.Value == 0) {
                    ScrollIndex.Value = 1;
                }
            }
            else {
                ScrollIndex.Minimum = 0;
                ScrollIndex.Value = 0;
                ScrollIndex.Maximum = 0;
            }

            var index = ScrollIndex.Value;

            LabelIndex.Text = $"Index: {index} / {count}";
        }

        private void UpdateEffectText() {
            if (Element is not null) {
                var item = new SkillEffect();
                var index = ScrollIndex.Value;

                index--;

                if (index > Util.NotSelected) {
                    item = Element.Effects[index];
                }

                ComboEffectType.SelectedIndex = (int)item.EffectType;
                ComboEffectTarget.SelectedIndex = (int)item.TargetType;
                ComboTargetVitalType.SelectedIndex = (int)item.VitalType;
                TextDamage.Text = item.Damage.ToString();
                TextDamagePerLevel.Text = item.DamagePerLevel.ToString();
                TextEffectDuration.Text = item.Duration.ToString();
                TextEffectInterval.Text = item.Interval.ToString();
                TextEffectAttributeEffect.Text = item.EffectId.ToString();
                ScrollTrigger.Value = item.Trigger;
                LabelTrigger.Text = $"Trigger Chance: {item.Trigger}%";
            }
        }

        private void ComboEffectType_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (IsValidIndex()) {
                    var index = ScrollIndex.Value;

                    index--;

                    var item = Element.Effects[index];

                    item.EffectType = (SkillEffectType)ComboEffectType.SelectedIndex;
                }
            }
        }

        private void ComboEffectTarget_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (IsValidIndex()) {
                    var index = ScrollIndex.Value;

                    index--;

                    var item = Element.Effects[index];

                    item.TargetType = (SkillTargetType)ComboEffectTarget.SelectedIndex;
                }
            }
        }

        private void ComboTargetVitalType_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (IsValidIndex()) {
                    var index = ScrollIndex.Value;

                    index--;

                    var item = Element.Effects[index];

                    item.VitalType = (SkillVitalType)ComboTargetVitalType.SelectedIndex;
                }
            }
        }

        private void TextDamage_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (IsValidIndex()) {
                    var index = ScrollIndex.Value;

                    index--;

                    var item = Element.Effects[index];

                    item.Damage = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextDamagePerLevel_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (IsValidIndex()) {
                    var index = ScrollIndex.Value;

                    index--;

                    var item = Element.Effects[index];

                    item.DamagePerLevel = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextEffectDuration_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (IsValidIndex()) {
                    var index = ScrollIndex.Value;

                    index--;

                    var item = Element.Effects[index];

                    item.Duration = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextEffectInterval_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (IsValidIndex()) {
                    var index = ScrollIndex.Value;

                    index--;

                    var item = Element.Effects[index];

                    item.Interval = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextEffectAttributeEffect_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (IsValidIndex()) {
                    var index = ScrollIndex.Value;

                    index--;

                    var item = Element.Effects[index];

                    item.EffectId = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void ScrollTrigger_Scroll(object sender, ScrollEventArgs e) {
            if (Element is not null) {
                if (IsValidIndex()) {
                    var index = ScrollIndex.Value;

                    index--;

                    var item = Element.Effects[index];

                    item.Trigger = ScrollTrigger.Value;

                    LabelTrigger.Text = $"Trigger Chance: {item.Trigger}%";
                }
            }
        }

        #endregion
    }
}

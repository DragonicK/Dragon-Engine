using Crystalshire.Core.Content;
using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Equipments;
using Crystalshire.Core.Model.Achievements;

using Crystalshire.Editor.Common;

namespace Crystalshire.Editor.Achievements {
    public partial class FormAchievement : Form {
        public IDatabase<Achievement> Database { get; }
        public Achievement? Element { get; private set; }
        public Configuration Configuration { get; private set; }
        public int SelectedIndex { get; private set; } = Util.NotSelected;

        private int rewardIndex = Util.NotSelected;
        private int requirementIndex = Util.NotSelected;

        public FormAchievement(Configuration configuration, IDatabase<Achievement> database) {
            InitializeComponent();

            SetEnabled(false);

            Util.FillComboBox<Rarity>(ComboRarity);
            Util.FillComboBox<EquipmentType>(ComboEquipment);        
            Util.FillComboBox<AchievementCategory>(ComboCategory);
            Util.FillComboBox<AchievementRewardType>(ComboRewardType);
            Util.FillComboBox<AchievementPrimaryRequirement>(ComboPrimary);
            Util.FillComboBox<AchievementSecondaryRequirement>(ComboSecondary);
            Util.FillComboBox<AchievementRewardType>(ComboRewardType);

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
                TextPoint.Text = Element.Point.ToString();
                TextAttributeId.Text = Element.AttributeId.ToString();
                ComboCategory.SelectedIndex = (int)Element.Category;

                if (Element.Rewards.Count > 0) {
                    rewardIndex = 0;
                }
                else {
                    rewardIndex = Util.NotSelected;
                }

                if (Element.Requirements.Count > 0) {
                    requirementIndex = 0;
                }
                else {
                    requirementIndex= Util.NotSelected;
                }

                UpdateRequirementIndexLabel();
                UpdateRequirementControls();
                UpdateRequirementText();

                UpdateRewardIndexLabel();
                UpdateRewardControls();
                UpdateRewardText();
            }
        }

        private void Clear() {
            TextId.Text = "0";
            TextName.Text = string.Empty;
            TextDescription.Text = string.Empty;
            TextPoint.Text = string.Empty;
            TextAttributeId.Text = string.Empty;
            ComboCategory.SelectedIndex = 0;

            ComboRewardType.SelectedIndex = 0;
            TextRewardId.Text = string.Empty;
            TextRewardValue.Text = string.Empty;
            TextRewardLevel.Text = string.Empty;
            ComboRewardBound.SelectedIndex = 0;
            TextRewardAttributeId.Text = string.Empty;
            TextRewardUpgradeId.Text = string.Empty;

            ComboPrimary.SelectedIndex = 0;
            ComboSecondary.SelectedIndex = 0;
            ComboRarity.SelectedIndex = 0;
            ComboEquipment.SelectedIndex = 0;
            TextRequirementId.Text = string.Empty;
            TextRequirementValue.Text = string.Empty;
            TextRequirementLevel.Text = string.Empty;
            TextRequirementCount.Text = string.Empty;
            TextRequirementDescription.Text = string.Empty;
        }

        private void SetEnabled(bool enabled) {
            TabAchievement.Enabled = enabled;
        }

        #region Add, Delete, Clear

        private void ButtonAdd_Click(object sender, EventArgs e) {
            for (var i = 1; i <= int.MaxValue; i++) {
                if (!Database.Contains(i)) {

                    Database.Add(i, new Achievement() { Id = i });

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

        #region Achievement Data

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

        private void TextPoint_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Point = Util.GetValue(TextPoint);
            }
        }

        private void ComboCategory_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Category = (AchievementCategory)ComboCategory.SelectedIndex;
            }
        }

        private void TextAttributeId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.AttributeId = Util.GetValue(TextAttributeId);
            }
        }

        #endregion

        #region Achievement Reward

        private void ButtonAddReward_Click(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Rewards.Add(new AchievementReward());

                if (Element.Rewards.Count == 1) {
                    if (rewardIndex <= Util.NotSelected) {
                        rewardIndex = 0;

                        UpdateRewardText();
                    }
                }

                UpdateRewardControls();
                UpdateRewardIndexLabel();
            }
        }

        private void ButtonRemoveReward_Click(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards.RemoveAt(rewardIndex);

                    if (rewardIndex >= Element.Rewards.Count) {
                        rewardIndex = Element.Rewards.Count - 1;
                    }
                }

                UpdateRewardText();

                UpdateRewardControls();
                UpdateRewardIndexLabel();
            }
        }

        private void ScrollRewardIndex_Scroll(object sender, ScrollEventArgs e) {
            if (Element is not null) {
                if (Element.Rewards.Count > 0) {
                    rewardIndex = ScrollRewardIndex.Value;
                }

                UpdateRewardIndexLabel();
                UpdateRewardText();
            }
        }

        private void ComboRewardType_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards[rewardIndex].Type = (AchievementRewardType)ComboRewardType.SelectedIndex;
                }
            }
        }

        private void TextRewardId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards[rewardIndex].Id = Util.GetValue(TextRewardId);
                }
            }
        }

        private void TextRewardValue_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards[rewardIndex].Value = Util.GetValue(TextRewardValue);
                }
            }
        }

        private void TextRewardLevel_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards[rewardIndex].Level = Util.GetValue(TextRewardLevel);
                }
            }
        }

        private void ComboRewardBound_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards[rewardIndex].Bound = Convert.ToBoolean(ComboRewardBound.SelectedIndex);
                }
            }
        }

        private void TextRewardAttributeId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards[rewardIndex].AttributeId = Util.GetValue(TextRewardAttributeId);
                }
            }
        }

        private void TextRewardUpgradeId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards[rewardIndex].UpgradeId = Util.GetValue(TextRewardUpgradeId);
                }
            }
        }

        private void UpdateRewardIndexLabel() {
            if (Element is not null) {
                LabelRewardIndex.Text = $"Reward Index: {rewardIndex + 1}/ {Element.Rewards.Count}";

                if (rewardIndex > Util.NotSelected) {
                    ScrollRewardIndex.Maximum = Element.Rewards.Count - 1;

                    if (rewardIndex == 0) {
                        ScrollRewardIndex.Value = rewardIndex;
                    }
                }
                else {
                    ScrollRewardIndex.Value = 0;
                    ScrollRewardIndex.Maximum = 0;
                }
            }
        }

        private void UpdateRewardControls() {
            if (Element is not null) {
                var isEnabled = Element.Rewards.Count > 0;

                ScrollRewardIndex.Enabled = isEnabled;
                ComboRewardType.Enabled = isEnabled;
                TextRewardId.Enabled = isEnabled;
                TextRewardValue.Enabled = isEnabled;
                TextRewardLevel.Enabled = isEnabled;
                TextRewardAttributeId.Enabled = isEnabled;
                TextRewardUpgradeId.Enabled = isEnabled;
                ComboRewardBound.Enabled = isEnabled;
            }
        }

        private void UpdateRewardText() {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    ComboRewardType.SelectedIndex = (int)Element.Rewards[rewardIndex].Type;
                    TextRewardId.Text = Element.Rewards[rewardIndex].Id.ToString();
                    TextRewardValue.Text = Element.Rewards[rewardIndex].Value.ToString();
                    TextRewardLevel.Text = Element.Rewards[rewardIndex].Level.ToString();
                    TextRewardAttributeId.Text = Element.Rewards[rewardIndex].AttributeId.ToString();
                    TextRewardUpgradeId.Text = Element.Rewards[rewardIndex].UpgradeId.ToString();
                    ComboRewardBound.SelectedIndex = Convert.ToInt32(Element.Rewards[rewardIndex].Bound);
                }
            }

            if (rewardIndex <= Util.NotSelected) {
                ComboRewardType.SelectedIndex = 0;
                TextRewardId.Text = "0";
                TextRewardValue.Text = "0";
                TextRewardLevel.Text = "0";
                TextRewardAttributeId.Text = "0";
                TextRewardUpgradeId.Text = "0";
                ComboRewardBound.SelectedIndex = 0;
            }

        }


        #endregion

        #region Achievement Requirement


        private void ScrollRequirementIndex_Scroll(object sender, ScrollEventArgs e) {
            if (Element is not null) {
                if (Element.Requirements.Count > 0) {
                    requirementIndex = ScrollRequirementIndex.Value;
                }

                UpdateRequirementIndexLabel();
                UpdateRequirementText();
            }
        }

        private void ButtonAddRequirement_Click(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Requirements.Add(new AchievementRequirementEntry());

                if (Element.Requirements.Count == 1) {
                    if (requirementIndex <= Util.NotSelected) {
                        requirementIndex = 0;

                        UpdateRequirementText();
                    }
                }

                UpdateRequirementControls();
                UpdateRequirementIndexLabel();
            }
        }

        private void ButtonRemoveRequirement_Click(object sender, EventArgs e) {
            if (Element is not null) {
                if (requirementIndex > Util.NotSelected) {
                    Element.Requirements.RemoveAt(requirementIndex);

                    if (requirementIndex >= Element.Requirements.Count) {
                        requirementIndex = Element.Requirements.Count - 1;
                    }
                }

                UpdateRequirementText();

                UpdateRequirementControls();
                UpdateRequirementIndexLabel();
            }
        }

        private void ComboPrimary_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (requirementIndex > Util.NotSelected) {
                    Element.Requirements[requirementIndex].PrimaryType = (AchievementPrimaryRequirement)ComboPrimary.SelectedIndex;
                }
            }
        }

        private void ComboSecondary_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (requirementIndex > Util.NotSelected) {
                    Element.Requirements[requirementIndex].SecondaryType = (AchievementSecondaryRequirement)ComboSecondary.SelectedIndex;
                }
            }
        }

        private void ComboRarity_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (requirementIndex > Util.NotSelected) {
                    Element.Requirements[requirementIndex].Rarity = (Rarity)ComboRarity.SelectedIndex;
                }
            }
        }

        private void ComboEquipment_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (requirementIndex > Util.NotSelected) {
                    Element.Requirements[requirementIndex].Equipment = (EquipmentType)ComboEquipment.SelectedIndex;
                }
            }
        }

        private void TextRequirementId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (requirementIndex > Util.NotSelected) {
                    Element.Requirements[requirementIndex].Id = Util.GetValue(TextRequirementId);
                }
            }
        }

        private void TextRequirementValue_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (requirementIndex > Util.NotSelected) {
                    Element.Requirements[requirementIndex].Value = Util.GetValue(TextRequirementValue);
                }
            }
        }

        private void TextRequirementLevel_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (requirementIndex > Util.NotSelected) {
                    Element.Requirements[requirementIndex].Level = Util.GetValue(TextRequirementLevel);
                }
            }
        }

        private void TextRequirementCount_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (requirementIndex > Util.NotSelected) {
                    Element.Requirements[requirementIndex].Count = Util.GetValue(TextRequirementCount);
                }
            }
        }

        private void TextRequirementDescription_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (requirementIndex > Util.NotSelected) {
                    Element.Requirements[requirementIndex].Description = TextRequirementDescription.Text;
                }
            }
        }

        private void UpdateRequirementIndexLabel() {
            if (Element is not null) {
                LabelRequirementIndex.Text = $"Requirement Index: {requirementIndex + 1}/ {Element.Requirements.Count}";

                if (requirementIndex > Util.NotSelected) {
                    ScrollRequirementIndex.Maximum = Element.Requirements.Count - 1;

                    if (requirementIndex == 0) {
                        ScrollRequirementIndex.Value = requirementIndex;
                    }
                }
                else {
                    ScrollRequirementIndex.Value = 0;
                    ScrollRequirementIndex.Maximum = 0;
                }
            }
        }

        private void UpdateRequirementControls() {
            if (Element is not null) {
                var isEnabled = Element.Requirements.Count > 0;

                ScrollRequirementIndex.Enabled = isEnabled;
                ComboPrimary.Enabled = isEnabled;
                ComboSecondary.Enabled = isEnabled;
                ComboRarity.Enabled = isEnabled;
                ComboEquipment.Enabled = isEnabled;
                TextRequirementId.Enabled = isEnabled;
                TextRequirementValue.Enabled = isEnabled;
                TextRequirementLevel.Enabled = isEnabled;
                TextRequirementCount.Enabled = isEnabled;
                TextRequirementDescription.Enabled = isEnabled;
            }
        }

        private void UpdateRequirementText() {
            if (Element is not null) {
                if (requirementIndex > Util.NotSelected) {
                    ComboPrimary.SelectedIndex = (int)Element.Requirements[requirementIndex].PrimaryType;
                    ComboSecondary.SelectedIndex = (int)Element.Requirements[requirementIndex].SecondaryType;
                    ComboRarity.SelectedIndex = (int)Element.Requirements[requirementIndex].Rarity;
                    ComboEquipment.SelectedIndex = (int)Element.Requirements[requirementIndex].Equipment;
                    TextRequirementId.Text = Element.Requirements[requirementIndex].Id.ToString();
                    TextRequirementValue.Text = Element.Requirements[requirementIndex].Value.ToString();
                    TextRequirementLevel.Text = Element.Requirements[requirementIndex].Level.ToString();
                    TextRequirementCount.Text = Element.Requirements[requirementIndex].Count.ToString();
                    TextRequirementDescription.Text = Element.Requirements[requirementIndex].Description;
                }
            }

            if (requirementIndex <= Util.NotSelected) {
                ComboPrimary.SelectedIndex = 0;
                ComboSecondary.SelectedIndex = 0;
                ComboRarity.SelectedIndex = 0;
                ComboEquipment.SelectedIndex = 0;
                TextRequirementId.Text = "0";
                TextRequirementValue.Text = "0";
                TextRequirementLevel.Text = "0";
                TextRequirementCount.Text = "0";
                TextRequirementDescription.Text = string.Empty;
            }
        }

        #endregion


    }
}
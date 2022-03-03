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

        public FormAchievement(Configuration configuration, IDatabase<Achievement> database) {
            InitializeComponent();

            SetEnabled(false);

            Util.FillComboBox<Rarity>(ComboRarity);
            Util.FillComboBox<EquipmentType>(ComboEquipment);        
            Util.FillComboBox<AchievementCategory>(ComboCategory);
            Util.FillComboBox<AchievementRewardType>(ComboRewardType);
            Util.FillComboBox<AchievementPrimaryRequirement>(ComboPrimary);
            Util.FillComboBox<AchievementSecondaryRequirement>(ComboSecondary);

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

                ComboRewardType.SelectedIndex = (int)Element.Reward.Type;
                TextRewardId.Text = Element.Reward.Id.ToString();
                TextRewardValue.Text = Element.Reward.Value.ToString();
                TextRewardLevel.Text = Element.Reward.Level.ToString();
                TextRewardBound.Text = Element.Reward.Bound.ToString();
                TextRewardAttributeId.Text = Element.Reward.AttributeId.ToString();
                TextRewardUpgradeId.Text = Element.Reward.UpgradeId.ToString();

                ComboPrimary.SelectedIndex = (int)Element.Entry.PrimaryType;
                ComboSecondary.SelectedIndex = (int)Element.Entry.SecondaryType;
                ComboRarity.SelectedIndex = (int)Element.Entry.Rarity;
                ComboEquipment.SelectedIndex = (int)Element.Entry.Equipment;
                TextRequirementId.Text = Element.Entry.Id.ToString();
                TextRequirementValue.Text = Element.Entry.Value.ToString();
                TextRequirementLevel.Text = Element.Entry.Level.ToString();
                TextRequirementCount.Text = Element.Entry.Count.ToString();
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
            TextRewardBound.Text = string.Empty;
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

        private void ComboRewardType_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Reward.Type = (AchievementRewardType)ComboRewardType.SelectedIndex;
            }
        }

        private void TextRewardId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Reward.Id = Util.GetValue(TextRewardId);
            }
        }

        private void TextRewardValue_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Reward.Value = Util.GetValue(TextRewardValue);
            }
        }

        private void TextRewardLevel_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Reward.Level = Util.GetValue(TextRewardLevel);
            }
        }

        private void TextRewardBound_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                byte activated = 1;
                byte deactivated = 0;

                var value = Util.GetValue(TextRewardBound);

                Element.Reward.Bound = value >= 1 ? activated : deactivated;
            }
        }

        private void TextRewardAttributeId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Reward.AttributeId = Util.GetValue(TextRewardAttributeId);
            }
        }

        private void TextRewardUpgradeId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Reward.UpgradeId = Util.GetValue(TextRewardUpgradeId);
            }
        }

        #endregion

        #region Achievement Requirement

        private void ComboPrimary_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Entry.PrimaryType = (AchievementPrimaryRequirement)ComboPrimary.SelectedIndex;
            }
        }

        private void ComboSecondary_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Entry.SecondaryType = (AchievementSecondaryRequirement)ComboSecondary.SelectedIndex;
            }
        }

        private void ComboRarity_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Entry.Rarity = (Rarity)ComboRarity.SelectedIndex;
            }
        }

        private void ComboEquipment_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Entry.Equipment = (EquipmentType)ComboEquipment.SelectedIndex;
            }
        }

        private void TextRequirementId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Entry.Id = Util.GetValue(TextRequirementId);
            }
        }

        private void TextRequirementValue_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Entry.Value = Util.GetValue(TextRequirementValue);
            }
        }

        private void TextRequirementLevel_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Entry.Level = Util.GetValue(TextRequirementLevel);
            }
        }

        private void TextRequirementCount_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Entry.Count = Util.GetValue(TextRequirementCount);
            }
        }

        #endregion

    }
}
using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Quests;

using Crystalshire.Editor.Common;

namespace Crystalshire.Editor.Quests {
    public partial class FormQuest : Form {
        public IDatabase<Quest> Database { get; }
        public Quest? Element { get; private set; }
        public Configuration Configuration { get; private set; }
        public int SelectedIndex { get; private set; } = Util.NotSelected;

        private int stepIndex = Util.NotSelected;
        private int rewardIndex = Util.NotSelected;

        public FormQuest(Configuration configuration, IDatabase<Quest> database) {
            InitializeComponent();

            SetEnabled(false);

            Util.FillComboBox<QuestType>(ComboType);
            Util.FillComboBox<QuestRepeatable>(ComboRepeatable);
            Util.FillComboBox<QuestShareable>(ComboShareable);
            Util.FillComboBox<QuestSelectableReward>(ComboSelectableReward);
            Util.FillComboBox<QuestActionType>(ComboActionType);
            Util.FillComboBox<QuestRewardType>(ComboRewardType);

            ComboRewardBound.SelectedIndex = 0;

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
                TextName.Text = Element.Title;
                TextSummary.Text = Element.Summary;
                ComboType.SelectedIndex = (int)Element.Type;
                ComboRepeatable.SelectedIndex = (int)Element.Repeatable;
                ComboShareable.SelectedIndex = (int)Element.Shareable;  
                ComboSelectableReward.SelectedIndex = (int)Element.SelectableReward;
                TextSelectableRewardCount.Text = Element.SelectableRewardCount.ToString();

                if (Element.Steps.Count > 0) {
                    stepIndex = 0;
                }
                else {
                    stepIndex = Util.NotSelected;
                }

                if (Element.Rewards.Count > 0) {
                    rewardIndex = 0;
                }
                else {
                    rewardIndex = Util.NotSelected;
                }

                UpdateStepIndexLabel();
                UpdateStepControls();
                UpdateStepText();

                UpdateRewardIndexLabel();
                UpdateRewardControls();
                UpdateRewardText();
            }
        }

        private void Clear() {
            TextId.Text = "0";
            TextName.Text = string.Empty;
        }

        private void SetEnabled(bool enabled) {
            TabQuest.Enabled = enabled;
        }

        #region Add, Delete, Clear

        private void ButtonAdd_Click(object sender, EventArgs e) {
            for (var i = 1; i <= int.MaxValue; i++) {
                if (!Database.Contains(i)) {

                    Database.Add(i, new Quest() { Id = i });

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

        #region Quest Data

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
                Element.Title = TextName.Text;

                if (SelectedIndex > Util.NotSelected) {
                    ListIndex.Items[SelectedIndex] = $"{Element.Id}: {Element.Title}";
                }
            }
        }

        private void TextSummary_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Summary = TextSummary.Text;
            }
        }

        private void ComboType_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Type = (QuestType)((ComboBox)sender).SelectedIndex;
            }
        }

        private void ComboRepeatable_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Repeatable = (QuestRepeatable)((ComboBox)sender).SelectedIndex;
            }
        }

        private void ComboShareable_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Shareable = (QuestShareable)((ComboBox)sender).SelectedIndex;
            }
        }

        private void ComboSelectableReward_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.SelectableReward = (QuestSelectableReward)((ComboBox)sender).SelectedIndex;
            }
        }

        private void TextSelectableRewardCount_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.SelectableRewardCount = Util.GetValue((TextBox)sender);
            }
        }

        #endregion

        #region Quest Steps

        private void ScrollStepIndex_Scroll(object sender, ScrollEventArgs e) {
            if (Element is not null) {
                if (Element.Steps.Count > 0) {
                    stepIndex = ScrollStepIndex.Value;
                }

                UpdateStepIndexLabel();
                UpdateStepText();
            }
        }

        private void ButtonAddStep_Click(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Steps.Add(new QuestStep());

                if (Element.Steps.Count == 1) {
                    if (stepIndex <= Util.NotSelected) {
                        stepIndex = 0;

                        UpdateStepText();
                    }
                }

                UpdateStepControls();
                UpdateStepIndexLabel();
            }
        }

        private void ButtonRemoveStep_Click(object sender, EventArgs e) {
            if (Element is not null) {
                if (stepIndex > Util.NotSelected) {
                    Element.Steps.RemoveAt(stepIndex);

                    if (stepIndex >= Element.Steps.Count) {
                        stepIndex = Element.Steps.Count - 1;
                    }
                }

                UpdateStepText();

                UpdateStepControls();
                UpdateStepIndexLabel();
            }
        }

        private void UpdateStepIndexLabel() {
            if (Element is not null) {
                LabelStepIndex.Text = $"Step Index: {stepIndex + 1}/ {Element.Steps.Count}";

                if (stepIndex > Util.NotSelected) {
                    ScrollStepIndex.Maximum = Element.Steps.Count - 1;

                    if (stepIndex == 0) {
                        ScrollStepIndex.Value = stepIndex;
                    }
                }
                else {
                    ScrollStepIndex.Value = 0;
                    ScrollStepIndex.Maximum = 0;
                }
            }
        }

        private void UpdateStepControls() {
            if (Element is not null) {
                var isEnabled = Element.Steps.Count > 0;

                ScrollStepIndex.Enabled = isEnabled;
                TextStepTitle.Enabled = isEnabled;
                TextStepSummary.Enabled = isEnabled;
                ComboActionType.Enabled = isEnabled;
                TextRequirementEntityId.Enabled = isEnabled;
                TextRequirementCount.Enabled = isEnabled;
                TextRequirementX.Enabled = isEnabled;
                TextRequirementY.Enabled = isEnabled;
            }
        }

        private void UpdateStepText() {
            if (Element is not null) {
                if (stepIndex > Util.NotSelected) {
                    TextStepTitle.Text = Element.Steps[stepIndex].Title;
                    TextStepSummary.Text = Element.Steps[stepIndex].Summary;
                    ComboActionType.SelectedIndex = (int)Element.Steps[stepIndex].ActionType;
                    TextRequirementEntityId.Text = Element.Steps[stepIndex].Requirement.EntityId.ToString();
                    TextRequirementCount.Text = Element.Steps[stepIndex].Requirement.Value.ToString();
                    TextRequirementX.Text = Element.Steps[stepIndex].Requirement.X.ToString();
                    TextRequirementY.Text = Element.Steps[stepIndex].Requirement.Y.ToString();
                }
            }
        }

        private void TextStepTitle_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (stepIndex > Util.NotSelected) {
                    Element.Steps[stepIndex].Title = ((TextBox)sender).Text;
                }
            }
        }

        private void TextStepSummary_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (stepIndex > Util.NotSelected) {
                    Element.Steps[stepIndex].Summary = ((TextBox)sender).Text;
                }
            }
        }

        private void ComboActionType_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (stepIndex > Util.NotSelected) {
                    Element.Steps[stepIndex].ActionType = (QuestActionType)ComboActionType.SelectedIndex;
                }
            }
        }

        private void TextRequirementEntityId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (stepIndex > Util.NotSelected) {
                    Element.Steps[stepIndex].Requirement.EntityId = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextRequirementCount_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (stepIndex > Util.NotSelected) {
                    Element.Steps[stepIndex].Requirement.Value = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextRequirementX_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (stepIndex > Util.NotSelected) {
                    Element.Steps[stepIndex].Requirement.X = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextRequirementY_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (stepIndex > Util.NotSelected) {
                    Element.Steps[stepIndex].Requirement.Y = Util.GetValue((TextBox)sender);
                }
            }
        }

        #endregion

        #region Quest Reward

        private void ScrollRewardIndex_Scroll(object sender, ScrollEventArgs e) {
            if (Element is not null) {
                if (Element.Rewards.Count > 0) {
                    rewardIndex = ScrollRewardIndex.Value;
                }

                UpdateRewardIndexLabel();
                UpdateRewardText();
            }
        }

        private void ButtonAddReward_Click(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Rewards.Add(new QuestReward());

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
        }

        private void ComboRewardType_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards[rewardIndex].Type = (QuestRewardType)ComboRewardType.SelectedIndex;  
                }
            }
        }

        private void TextRewardId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards[rewardIndex].Id = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextRewardValue_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards[rewardIndex].Value = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextRewardLevel_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards[rewardIndex].Level = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextRewardAttributeId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards[rewardIndex].AttributeId = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextRewardUpgradeId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (rewardIndex > Util.NotSelected) {
                    Element.Rewards[rewardIndex].UpgradeId = Util.GetValue((TextBox)sender);
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

        #endregion
    }
}
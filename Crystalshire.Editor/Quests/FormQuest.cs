using Crystalshire.Core.Content;
using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Items;
using Crystalshire.Core.Model.Quests;

using Crystalshire.Editor.Common;

namespace Crystalshire.Editor.Quests {
    public partial class FormQuest : Form {
        public IDatabase<Quest> Database { get; }
        public Quest? Element { get; private set; }
        public Configuration Configuration { get; private set; }
        public int SelectedIndex { get; private set; } = Util.NotSelected;

        public FormQuest(Configuration configuration, IDatabase<Quest> database) {
            InitializeComponent();

            SetEnabled(false);

            Util.FillComboBox<QuestType>(ComboType);
            Util.FillComboBox<QuestRepeatable>(ComboRepeatable);
            Util.FillComboBox<QuestShareable>(ComboShareable);
            Util.FillComboBox<QuestSelectableReward>(ComboSelectableReward);
            Util.FillComboBox<QuestActionType>(ComboActionType);
            Util.FillComboBox<QuestRewardType>(ComboRewardType);

            ComboBound.SelectedIndex = 0;

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
    }
}

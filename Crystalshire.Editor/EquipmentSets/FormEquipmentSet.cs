using Crystalshire.Core.Content;
using Crystalshire.Core.Model.EquipmentSets;

using Crystalshire.Editor.Common;

namespace Crystalshire.Editor.EquipmentSets {
    public partial class FormEquipmentSet : Form {
        public IDatabase<EquipmentSet> Database { get; }
        public EquipmentSet? Element { get; private set; }
        public Configuration Configuration { get; private set; }
        public int SelectedIndex { get; private set; } = Util.NotSelected;

        public FormEquipmentSet(Configuration configuration, IDatabase<EquipmentSet> database) {
            InitializeComponent();

            SetEnabled(false);

            Util.FillComboBox<EquipmentSetCount>(ComboCategory);

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
                ComboCategory.SelectedIndex = 0;
                TextAttributeId.Text = "0";
                TextSkillId.Text = "0";

                UpdateListSet();
            }
        }

        private void Clear() {
            TextId.Text = "0";
            TextName.Text = string.Empty;
            TextDescription.Text = string.Empty;
            ComboCategory.SelectedIndex = 0;
            TextAttributeId.Text = "0";
            TextSkillId.Text = "0";

            UpdateListSet();
        }

        private void SetEnabled(bool enabled) {
            TabEquipmentSet.Enabled = enabled;
        }

        #region Add, Delete, Clear

        private void ButtonAdd_Click(object sender, EventArgs e) {
            for (var i = 1; i <= int.MaxValue; i++) {
                if (!Database.Contains(i)) {

                    Database.Add(i, new EquipmentSet() { Id = i });

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

        #region Equipment Set Data

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

        #region Equipment Set Add, Delete, Clear

        private void ButtonAddSet_Click(object sender, EventArgs e) {
            if (Element is not null) {
                var indexCount = (EquipmentSetCount)ComboCategory.SelectedIndex;

                var effect = new EquipmentSetEffect() {
                    AttributeId = Util.GetValue(TextAttributeId),
                    SkillId = Util.GetValue(TextSkillId)
                };

                Element.Sets[indexCount] = effect;

                UpdateListSet();
            }
        }

        private void ButtonDeleteSet_Click(object sender, EventArgs e) {
            if (Element is not null) {
                var index = ListSet.SelectedIndex;

                if (index >= 0) {
                    var selected = ListSet.Items[index].ToString();
                    var indexOf = selected!.IndexOf(':');
                    var name = selected!.Substring(0, indexOf);

                    var found = Element.Sets.Select(p => p.Key).First(p => p.ToString() == name);

                    Element.Sets.Remove(found);

                    UpdateListSet();
                }
            }
        }

        private void ButtonClearSet_Click(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Sets.Clear();
                UpdateListSet();
            }
        }

        private void UpdateListSet() {
            ListSet.BeginUpdate();
            ListSet.Items.Clear();

            if (Element is not null) {
                var ordered = Element.Sets.OrderBy(p => p.Key);

                foreach (var (index, effect) in ordered) {
                    ListSet.Items.Add($"{index}: {{ Attribute: {effect.AttributeId}, Skill: {effect.SkillId} }}");
                }
            }

            ListSet.EndUpdate();
        }

        #endregion

    }
}

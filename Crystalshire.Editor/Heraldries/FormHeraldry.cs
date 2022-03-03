using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Heraldries;

using Crystalshire.Editor.Common;

namespace Crystalshire.Editor.Heraldries {
    public partial class FormHeraldry : Form {
        public IDatabase<Heraldry> Database { get; }
        public Heraldry? Element { get; private set; }
        public Configuration Configuration { get; private set; }

        public int SelectedIndex { get; private set; } = Util.NotSelected;

        public FormHeraldry(Configuration configuration, IDatabase<Heraldry> database) {
            InitializeComponent();

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
                TextUpgradeId.Text = Element.UpgradeId.ToString();

                UpdateIndexLabel();
                UpdateAttributeText();
            }
        }

        private void Clear() {
            TextId.Text = "0";
            TextName.Text = string.Empty;
            TextDescription.Text = string.Empty;
            TextUpgradeId.Text = "0";

            TextAttributeId.Text = "0";

            ScrollIndex.Minimum = 0;
            ScrollIndex.Value = 0;
            ScrollIndex.Maximum = 0;

            LabelIndex.Text = $"Index: 0 / 0";
        }

        private void SetEnabled(bool enabled) {
            TabHeraldry.Enabled = enabled;
        }

        #region Add, Delete, Clear

        private void ButtonAdd_Click(object sender, EventArgs e) {
            for (var i = 1; i <= int.MaxValue; i++) {
                if (!Database.Contains(i)) {

                    Database.Add(i, new Heraldry() { Id = i });

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

        #region Heraldry Data

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

        private void TextUpgradeId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.UpgradeId = Util.GetValue((TextBox)sender);
            }
        }

        #endregion

        #region Heraldry Attribute

        private void ButtonAttributeAdd_Click(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Attributes.Add(new HeraldryAttribute());

                UpdateIndexLabel();
                UpdateAttributeText();
            }
        }

        private void ButtonAttributeDelete_Click(object sender, EventArgs e) {
            if (Element is not null) {
                var index = ScrollIndex.Value;

                if (index > 0) {
                    index--;

                    if (Element.Attributes.Count > index) {
                        Element.Attributes.RemoveAt(index);

                        UpdateIndexLabel();
                        UpdateAttributeText();
                    }
                }
            }
        }

        private void ScrollIndex_Scroll(object sender, ScrollEventArgs e) {
            if (Element is not null) {
                UpdateIndexLabel();
                UpdateAttributeText();
            }
        }

        private void TextAttributeId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (IsValidIndex()) {
                    var index = ScrollIndex.Value;

                    index--;

                    var item = Element.Attributes[index];

                    item.AttributeId = Util.GetValue((TextBox)sender);

                    Element.Attributes[index] = item;
                }
            }
        }

        private void ScrollChance_Scroll(object sender, ScrollEventArgs e) {
            if (Element is not null) {
                if (IsValidIndex()) {
                    var index = ScrollIndex.Value;

                    index--;

                    var item = Element.Attributes[index];

                    item.Chance = ScrollChance.Value;

                    Element.Attributes[index] = item;

                    LabelChance.Text = $"Chance: {item.Chance}%";
                }
            }
        }

        private bool IsValidIndex() {
            var index = ScrollIndex.Value;

            if (index > 0) {
                index--;

                if (Element!.Attributes.Count > index) {
                    return true;
                }
            }

            return false;
        }

        private void UpdateIndexLabel() {
            var count = Element!.Attributes.Count;

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

        private void UpdateAttributeText() {
            if (Element is not null) {
                var item = new HeraldryAttribute();
                var index = ScrollIndex.Value;

                index--;

                if (index > Util.NotSelected) {
                    item = Element.Attributes[index];
                }

                TextAttributeId.Text = item.AttributeId.ToString();
                ScrollChance.Value = item.Chance;
                LabelChance.Text = $"Chance: {item.Chance}%";
            }
        }

        #endregion
    }
}
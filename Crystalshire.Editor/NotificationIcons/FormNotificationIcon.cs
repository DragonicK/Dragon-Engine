using Crystalshire.Core.Content;
using Crystalshire.Core.Model.NotificationIcons;

using Crystalshire.Editor.Common;

namespace Crystalshire.Editor.NotificationIcons {
    public partial class FormNotificationIcon : Form {
        public IDatabase<NotificationIcon> Database { get; }
        public NotificationIcon? Element { get; private set; }
        public Configuration Configuration { get; private set; }
        public int SelectedIndex { get; private set; } = Util.NotSelected;

        public FormNotificationIcon(Configuration configuration, IDatabase<NotificationIcon> database) {
            InitializeComponent();

            SetEnabled(false);

            Util.FillComboBox<NotificationIconType>(ComboIconType);

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
                TextIconId.Text = Element.IconId.ToString();
                ComboIconType.SelectedIndex = (int)Element.IconType;
            }
        }

        private void Clear() {
            TextId.Text = "0";
            TextName.Text = string.Empty;
            TextDescription.Text = string.Empty;
            TextIconId.Text = "0";
            ComboIconType.SelectedIndex = 0;
        }

        private void SetEnabled(bool enabled) {
            TabAchievement.Enabled = enabled;
        }

        #region Add, Delete, Clear

        private void ButtonAdd_Click(object sender, EventArgs e) {
            for (var i = 1; i <= int.MaxValue; i++) {
                if (!Database.Contains(i)) {

                    Database.Add(i, new NotificationIcon() { Id = i });

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

        #region Icon Data

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

        private void TextIconId_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.IconId = Util.GetValue(TextIconId);
            }
        }

        private void ComboIconType_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.IconType = (NotificationIconType)ComboIconType.SelectedIndex;
            }
        }

        #endregion

    }
}
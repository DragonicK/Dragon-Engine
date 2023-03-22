using Dragon.Core.Content;
using Dragon.Core.Model.Animations;

using Dragon.Animator.Common;

namespace Dragon.Animator.Animations {
    public partial class FormAnimation : Form {
        public IDatabase<Animation> Database { get; }
        public Animation? Element { get; private set; }
        public Configuration Configuration { get; private set; }
        public int SelectedIndex { get; private set; } = Util.NotSelected;

        public FormAnimation(Configuration configuration, IDatabase<Animation> database) {
            InitializeComponent();

            Configuration = configuration;
            Database = database;

            Element = null;

            Util.UpdateList(Database, ListIndex);
        }

        private void MenuSave_Click(object sender, EventArgs e) {
            var folder = Database.Folder;

            Database.Save();

            Database.Folder = Configuration.OutputClientPath;

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

                TextLowerSprite.Text = Element.LowerFrame.Sprite.ToString();
                TextLowerLoop.Text = Element.LowerFrame.LoopCount.ToString(); 
                TextLowerFrame.Text = Element.LowerFrame.FrameCount.ToString();
                TextLowerTime.Text = Element.LowerFrame.LoopTime.ToString();

                TextUpperSprite.Text = Element.UpperFrame.Sprite.ToString();
                TextUpperLoop.Text = Element.UpperFrame.LoopCount.ToString(); 
                TextUpperFrame.Text = Element.UpperFrame.FrameCount.ToString();    
                TextUpperTime.Text = Element.UpperFrame.LoopTime.ToString();   
            }
        }

        private void Clear() {
            TextId.Text = "0";
            TextName.Text = string.Empty;
        }

        private void SetEnabled(bool enabled) {
            GroupData.Enabled = enabled;
        }

        #region Add, Delete, Clear

        private void ButtonAdd_Click(object sender, EventArgs e) {
            for (var i = 1; i <= int.MaxValue; i++) {
                if (!Database.Contains(i)) {

                    Database.Add(i, new Animation() { Id = i });

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

        #region Cast Frame

        private void TextLowerSprite_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.LowerFrame.Sprite = Util.GetValue(TextLowerSprite.Text.Trim());
            }
        }

        private void TextLowerLoop_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.LowerFrame.LoopCount = Util.GetValue(TextLowerLoop.Text.Trim());
            }
        }

        private void TextLowerFrame_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.LowerFrame.FrameCount = Util.GetValue(TextLowerFrame.Text.Trim());
            }
        }

        private void TextLowerTime_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.LowerFrame.LoopTime = Util.GetValue(TextLowerTime.Text.Trim());
            }
        }

        #endregion

        #region Attack Frame

        private void TextUpperSprite_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.UpperFrame.Sprite = Util.GetValue(TextUpperSprite.Text.Trim());
            }
        }

        private void TextUpperLoop_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.UpperFrame.LoopCount = Util.GetValue(TextUpperLoop.Text.Trim());
            }
        }

        private void TextUpperFrame_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.UpperFrame.FrameCount = Util.GetValue(TextUpperFrame.Text.Trim());
            }
        }

        private void TextUpperTime_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.UpperFrame.LoopTime = Util.GetValue(TextUpperTime.Text.Trim());
            }
        }

        #endregion
    }
}
using Dragon.Core.Content;
using Dragon.Core.Model.Animations;

using Dragon.Animator.Common;

namespace Dragon.Animator.Animations {
    public partial class FormAnimation : Form {
        public IDatabase<Animation> Database { get; }
        public Animation? Element { get; private set; }
        public Configuration Configuration { get; private set; }
        public List<Bitmap> Resources { get; private set; }
        public int SelectedIndex { get; private set; } = Util.NotSelected;

        private const int AnimationColumn = 5;
        private const int PictureWidth = 192;
        private const int PictureHeight = 192;

        readonly Graphics lowerGraphics;
        readonly Graphics upperGraphics;

        int lowerIndex;
        int lowerSourceX;
        int lowerSourceY;
        int lowerCount;

        int upperIndex;
        int upperSourceX;
        int upperSourceY;
        int upperCount;

        public FormAnimation(Configuration configuration, IDatabase<Animation> database, List<Bitmap> resources) {
            InitializeComponent();

            lowerGraphics = PictureLower.CreateGraphics();
            upperGraphics = PictureUpper.CreateGraphics();

            Configuration = configuration;
            Resources = resources;
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
                TextSound.Text = Element.Sound;

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
            const string Zero = "0";

            TextId.Text = Zero;
            TextName.Text = string.Empty;
            TextSound.Text = ".None";

            TextLowerSprite.Text = Zero;
            TextLowerLoop.Text = Zero;
            TextLowerFrame.Text = Zero;
            TextLowerTime.Text = Zero;

            TextUpperSprite.Text = Zero;
            TextUpperLoop.Text = Zero;
            TextUpperFrame.Text = Zero;
            TextUpperTime.Text = Zero;
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

        private void TextSound_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Sound = TextSound.Text;
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
                var value = Util.GetValue(TextLowerTime.Text.Trim());

                Element.LowerFrame.LoopTime = value;

                if (value > 0) {
                    CasterTime.Interval = value;
                }
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
                var value = Util.GetValue(TextUpperTime.Text.Trim());

                Element.UpperFrame.LoopTime = value;

                if (value > 0) {
                    AttackTimer.Interval = value;
                }
            }
        }

        #endregion

        private void CastTimer_Tick(object sender, EventArgs e) {
            if (Resources.Count > 0) {
                if (Element is not null) {
                    Draw(lowerGraphics, Element.LowerFrame, ref lowerIndex, ref lowerCount, ref lowerSourceX, ref lowerSourceY);
                }
            }
        }

        private void AttackTimer_Tick(object sender, EventArgs e) {
            if (Resources.Count > 0) {
                if (Element is not null) {
                    Draw(upperGraphics, Element.UpperFrame, ref upperIndex, ref upperCount, ref upperSourceX, ref upperSourceY);
                }
            }
        }

        private void Draw(Graphics g, AnimationFrame frame, ref int index, ref int count, ref int x, ref int y) {
            var sprite = frame.Sprite - 1;
            var total = frame.FrameCount;
            var width = frame.Width;
            var height = frame.Height;

            if (sprite > -1 && sprite < Resources.Count) {
                if (index > AnimationColumn) {
                    x = 0;
                    y += 1;

                    index = 0;
                }
                else {
                    x++;
                }

                ++index;

                if (count > total) {
                    index = 0;
                    count = 0;

                    x = 0;
                    y = 0;
                }

                count++;

                g.Clear(Color.Transparent);
                g.DrawImage(Resources[sprite], new Rectangle(0, 0, width, height), new RectangleF(x * PictureWidth, y * PictureHeight, width, height), GraphicsUnit.Pixel);
            }
        }
    }
}
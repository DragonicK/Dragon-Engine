using Dragon.Model.Models;

namespace Dragon.Model.Forms;

public partial class FormFrames : Form {
    public Movement Movement { get; }
    public Bitmap Background { get; }
    public int SelectedFrameIndex { get; private set; }
    public int ScrollFrameIndex { get; private set; }
    public int PreviewFrameIndex { get; private set; }
    public float PreviewScale { get; private set; }
    public int PreviewX { get; private set; }
    public int PreviewY { get; private set; }
    public int PreviewWidth { get; private set; } = 160;
    public int PreviewHeight { get; private set; } = 160;

    private const int MaximumFrames = 4;
    private const int FrameWidth = 160;
    private const int FrameHeight = 160;
    private const int PictureWidth = 128;
    private const int PictureHeight = 128;

    private readonly Panel[] frames;
    private readonly Label[] labels;

    private readonly Bitmap buffer;

    public FormFrames(Movement movement) {
        InitializeComponent();

        var names = Enum.GetValues<AttackType>();

        for (var i = 0; i < names.Length; ++i) {
            ComboAttack.Items.Add(names[i]);
        }

        if (names.Length > 0) {
            ComboAttack.SelectedIndex = 0;
        }

        Movement = movement;
        Background = Properties.Resources.Background;
        buffer = new Bitmap(PicturePreview.Width, PicturePreview.Height);



        frames = new Panel[MaximumFrames];
        labels = new Label[MaximumFrames];

        labels[0] = LabelFrameIndex_0;
        labels[1] = LabelFrameIndex_1;
        labels[2] = LabelFrameIndex_2;
        labels[3] = LabelFrameIndex_3;

        frames[0] = FrameBox_0;
        frames[1] = FrameBox_1;
        frames[2] = FrameBox_2;
        frames[3] = FrameBox_3;

        ScrollFrame_Scroll(null!, null!);

        UpdateFormText();
        UpdateScrollFrames();
        UpdateMovementPropertyText();
    }

    private void UpdateFormText() {
        if (Movement is not null) {
            Text = $"Frame Editor - {Movement.Name}";
        }
    }

    #region Frame Property

    #region Animation

    private void TextAnimationId_TextChanged(object sender, EventArgs e) {
        var id = Util.GetValue((TextBox)sender);

        if (Movement is not null) {
            var frame = Movement[SelectedFrameIndex];

            if (frame is not null) {
                var animation = frame.Animation;

                animation.Id = id;

                frame.Animation = animation;
            }
        }
    }

    private void TextAnimationX_TextChanged(object sender, EventArgs e) {
        var x = Util.GetValue((TextBox)sender);

        if (Movement is not null) {
            var frame = Movement[SelectedFrameIndex];

            if (frame is not null) {
                var animation = frame.Animation;

                animation.OffsetX = x;

                frame.Animation = animation;
            }
        }
    }

    private void TextAnimationY_TextChanged(object sender, EventArgs e) {
        var y = Util.GetValue((TextBox)sender);

        if (Movement is not null) {
            var frame = Movement[SelectedFrameIndex];

            if (frame is not null) {
                var animation = frame.Animation;

                animation.OffsetY = y;

                frame.Animation = animation;
            }
        }
    }

    #endregion

    private void CheckCanMove_Click(object sender, EventArgs e) {
        if (Movement is not null) {
            var frame = Movement[SelectedFrameIndex];

            if (frame is not null) {
                frame.CanMove = CheckCanMove.Checked;
            }
        }
    }

    private void TextCastSkillId_TextChanged(object sender, EventArgs e) {
        if (Movement is not null) {
            var frame = Movement[SelectedFrameIndex];

            if (frame is not null) {
                frame.CastSkillId = Util.GetValue((TextBox)sender);
            }
        }
    }

    private void ComboAttack_SelectedIndexChanged(object sender, EventArgs e) {
        if (Movement is not null) {
            var frame = Movement[SelectedFrameIndex];

            if (frame is not null) {
                frame.AttackType = (AttackType)ComboAttack.SelectedIndex;
            }
        }
    }

    private void UpdateFramePropertyText() {
        var frame = Movement[SelectedFrameIndex];

        if (frame is not null) {
            TextAnimationId.Text = frame.Animation.Id.ToString();
            TextAnimationX.Text = frame.Animation.OffsetX.ToString();
            TextAnimationY.Text = frame.Animation.OffsetY.ToString();
            TextCastSkillId.Text = frame.CastSkillId.ToString();
            ComboAttack.SelectedIndex = (int)frame.AttackType;
            CheckCanMove.Checked = frame.CanMove;
        }
    }

    private void ClearFramePropertyText() {
        TextAnimationId.Text = "0";
        TextAnimationX.Text = "0";
        TextAnimationY.Text = "0";
        TextCastSkillId.Text = "0";
        ComboAttack.SelectedIndex = 0;
        CheckCanMove.Checked = false;
    }

    private void SetEnabledFrameProperty(bool enabled) {
        TextAnimationId.Enabled = enabled;
        TextAnimationX.Enabled = enabled;
        TextAnimationY.Enabled = enabled;
        TextCastSkillId.Enabled = enabled;
        ComboAttack.Enabled = enabled;
        CheckCanMove.Enabled = enabled;
    }

    #endregion

    #region Movement Property

    private void UpdateMovementPropertyText() {
        if (Movement != null) {
            TextName.Text = Movement.Name;
            TextMiliseconds.Text = Movement.Time.ToString();
            CheckContinuously.Checked = Movement.Continuously;
            CheckWaitResponse.Checked = Movement.WaitResponse;
        }
    }

    private void TextMiliseconds_TextChanged(object sender, EventArgs e) {
        if (Movement is not null) {
            Movement.Time = Util.GetValue((TextBox)sender);

            if (Movement.Time > 0) {
                TimerDraw.Interval = Movement.Time;
            }
        }
    }

    private void TextName_TextChanged(object sender, EventArgs e) {
        if (Movement is not null) {
            Movement.Name = ((TextBox)sender).Text;
        }
    }

    private void CheckContinuously_Click(object sender, EventArgs e) {
        if (Movement is not null) {
            Movement.Continuously = CheckContinuously.Checked;
        }
    }

    private void CheckWaitResponse_Click(object sender, EventArgs e) {
        if (Movement is not null) {
            Movement.WaitResponse = CheckWaitResponse.Checked;
        }
    }

    #endregion

    #region Frame Box

    private void SelectFrame(int index) {
        for (var i = 0; i < MaximumFrames; ++i) {
            if (index == i) {
                frames[i].BorderStyle = BorderStyle.Fixed3D;
            }
            else {
                frames[i].BorderStyle = BorderStyle.FixedSingle;
            }
        }
    }

    private void DiselectFrame() {
        for (var i = 0; i < MaximumFrames; ++i) {
            frames[i].BorderStyle = BorderStyle.FixedSingle;
        }
    }

    private void InvalidateFrameBox() {
        FrameBox_0.Invalidate();
        FrameBox_1.Invalidate();
        FrameBox_2.Invalidate();
        FrameBox_3.Invalidate();
    }

    public void FrameBox_MouseDown(object sender, MouseEventArgs e) {
        if (Movement is not null) {
            var index = Convert.ToInt32(((Panel)sender).Name.Replace("FrameBox_", string.Empty));
            var frame = Movement[index + ScrollFrameIndex];

            if (frame is not null) {
                SelectedFrameIndex = index + ScrollFrameIndex;

                SelectFrame(index);
                UpdateFramePropertyText();
                SetEnabledFrameProperty(true);

                GroupFrameProperty.Text = $"{frame.Name} - Frame Property";

                if (e.Button == MouseButtons.Right) {
                    var f = new FormMasking(frame);
                    f.ShowDialog();
                }
            }
            else {
                GroupFrameProperty.Text = "No Selected - Frame Property";
            }
        }
    }

    public void FrameBox_OnPaint(object sender, PaintEventArgs e) {
        if (Background is not null) {
            e.Graphics.DrawImage(Background, 0, 0);
        }

        if (Movement is not null) {
            var index = Convert.ToInt32(((Panel)sender).Name.Replace("FrameBox_", string.Empty));

            if (Movement.Count > 0) {
                var frame = Movement[index + ScrollFrameIndex];

                if (frame is not null) {
                    if (frame.Image is not null) {
                        e.Graphics.DrawImage(frame.Image, 0, 0, PictureWidth, PictureHeight);
                    }
                }
            }
        }
    }

    #endregion

    private void PictureLeft_Click(object sender, EventArgs e) {
        if (Movement is not null) {
            var index = Convert.ToInt32(((PictureBox)sender).Name.Replace("PictureLeft_", string.Empty));
            var position = index + ScrollFrameIndex;
            var frame = Movement[position];


            if (frame is not null) {
                if (position > 0) {
                    Movement.SwapFrames(position, position - 1);
                    InvalidateFrameBox();
                }
            }
        }
    }

    private void PictureRight_Click(object sender, EventArgs e) {
        if (Movement is not null) {
            var index = Convert.ToInt32(((PictureBox)sender).Name.Replace("PictureRight_", string.Empty));
            var position = index + ScrollFrameIndex;
            var frame = Movement[position];

            if (frame is not null) {
                if (position + 1 < Movement.Count) {
                    Movement.SwapFrames(position, position + 1);
                    InvalidateFrameBox();
                }
            }
        }
    }

    private void PictureLoad_Click(object sender, EventArgs e) {
        if (Movement is not null) {
            var index = Convert.ToInt32(((PictureBox)sender).Name.Replace("PictureLoad_", string.Empty));
            var frame = Movement[index + ScrollFrameIndex];

            if (frame is not null) {
                var dialog = new OpenFileDialog() {
                    InitialDirectory = Environment.CurrentDirectory,
                    Filter = "Png Files (*.png) | *.png",
                    CheckFileExists = true,
                    CheckPathExists = true,
                    Multiselect = false,
                    FilterIndex = 0
                };

                if (dialog.ShowDialog() == DialogResult.OK) {
                    frame.Name = dialog.SafeFileName;
                    frame.Image = new Bitmap(dialog.FileName);

                    InvalidateFrameBox();

                    if (SelectedFrameIndex == (index + ScrollFrameIndex)) {
                        GroupFrameProperty.Text = $"{frame.Name} - Frame Property";
                    }
                }
            }
        }
    }

    private void PictureRemove_Click(object sender, EventArgs e) {
        if (Movement is not null) {
            var index = Convert.ToInt32(((PictureBox)sender).Name.Replace("PictureRemove_", string.Empty));
            var frame = Movement[index + ScrollFrameIndex];

            if (frame is not null) {
                frame.Name = "No Name";
                frame.Image = null;
                InvalidateFrameBox();

                if (SelectedFrameIndex == (index + ScrollFrameIndex)) {
                    GroupFrameProperty.Text = $"{frame.Name} - Frame Property";
                }
            }
        }
    }

    private void PictureDelete_Click(object sender, EventArgs e) {
        if (Movement is not null) {
            var index = Convert.ToInt32(((PictureBox)sender).Name.Replace("PictureDelete_", string.Empty));

            Movement.Remove(index + ScrollFrameIndex);

            InvalidateFrameBox();
            UpdateTextFrames();
            UpdateScrollFrames();

            if (SelectedFrameIndex == (index + ScrollFrameIndex)) {
                SelectedFrameIndex = -1;
                GroupFrameProperty.Text = "No Selected - Frame Property";
            }
        }
    }

    #region Preview

    private void TimerDraw_Tick(object sender, EventArgs e) {
        if (Movement is not null) {
            if (Movement.Count > 0) {
                DrawPreview();
            }
            else {
                PicturePreview.Invalidate();
                PreviewFrameIndex = 0;
            }
        }
        else {
            PicturePreview.Invalidate();
            PreviewFrameIndex = 0;
        }
    }

    private void ScrollScale_Scroll(object sender, ScrollEventArgs e) {
        PreviewScale = ScrollScale.Value / 100f;

        PreviewWidth = Convert.ToInt32(FrameWidth * (1f + PreviewScale));
        PreviewHeight = Convert.ToInt32(FrameHeight * (1f + PreviewScale));

        LabelScale.Text = $"Preview Scale: {ScrollScale.Value}%";
    }

    private void ScrollX_Scroll(object sender, ScrollEventArgs e) {
        PreviewX = ScrollX.Value;
        LabelX.Text = $"Preview X: {PreviewX}";
    }

    private void ScrollY_Scroll(object sender, ScrollEventArgs e) {
        PreviewY = ScrollY.Value;
        LabelY.Text = $"Preview Y: {PreviewY}";
    }

    private void DrawPreview() {
        var g = Graphics.FromImage(buffer);
        g.Clear(Color.Black);

        if (PreviewFrameIndex < Movement.Count) {
            var frame = Movement[PreviewFrameIndex];

            if (frame is not null) {
                if (frame.Image is not null) {
                    g.DrawImage(frame.Image, PreviewX, PreviewY, PreviewWidth, PreviewHeight);
                }
            }
        }

        g = PicturePreview.CreateGraphics();
        g.DrawImage(buffer, 0, 0);

        PreviewFrameIndex++;

        if (PreviewFrameIndex >= Movement.Count) {
            PreviewFrameIndex = 0;
        }
    }

    #endregion

    private void ScrollFrame_Scroll(object sender, ScrollEventArgs e) {
        ScrollFrameIndex = ScrollFrame.Value;
        SelectedFrameIndex = -1;

        UpdateTextFrames();
        InvalidateFrameBox();
        DiselectFrame();
        ClearFramePropertyText();

        SetEnabledFrameProperty(false);
    }

    private void UpdateScrollFrames() {
        var count = Movement.Count - MaximumFrames;

        if (count < 0) {
            count = 0;
        }

        ScrollFrame.Maximum = count;
        ScrollFrame.Minimum = 0;

        if (ScrollFrame.Value > ScrollFrame.Maximum) {
            ScrollFrame.Value = ScrollFrame.Maximum;
            ScrollFrameIndex = ScrollFrame.Value;
        }
    }

    private void UpdateTextFrames() {
        var count = 0;

        if (Movement is not null) {
            count = Movement.Count;
        }

        LabelFrameCount.Text = "Frame Count: " + count;

        LabelFrameIndex_0.Text = (ScrollFrameIndex + 1).ToString();
        LabelFrameIndex_1.Text = (ScrollFrameIndex + 2).ToString();
        LabelFrameIndex_2.Text = (ScrollFrameIndex + 3).ToString();
        LabelFrameIndex_3.Text = (ScrollFrameIndex + 4).ToString();

        if (count == 0) {
            GroupFrameProperty.Text = "No Selected - Frame Property";
        }
    }

    #region Add, Remove, Clear

    private void PictureAdd_Click(object sender, EventArgs e) {
        if (Movement is not null) {
            Movement.Add(new Frame());
        }

        UpdateTextFrames();
        InvalidateFrameBox();
        UpdateScrollFrames();
    }

    private void PictureRemoveLast_Click(object sender, EventArgs e) {
        if (Movement is not null) {
            PreviewFrameIndex = 0;
            Movement.RemoveLast();
        }

        UpdateScrollFrames();
        UpdateTextFrames();

        ScrollFrame_Scroll(null!, null!);

        InvalidateFrameBox();
    }

    private void PictureClear_Click(object sender, EventArgs e) {
        if (Movement is not null) {
            PreviewFrameIndex = 0;
            Movement.Clear();
        }

        UpdateTextFrames();
        InvalidateFrameBox();
        UpdateScrollFrames();
    }

    #endregion

    #region Menu

    private void MenuAddFrame_Click(object sender, EventArgs e) {
        if (Movement is not null) {
            var dialog = new OpenFileDialog() {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "Png Files (*.png) | *.png",
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = true,
                FilterIndex = 0
            };

            if (dialog.ShowDialog() == DialogResult.OK) {
                for (var i = 0; i < dialog.FileNames.Length; ++i) {
                    var frame = new Frame() {
                        Name = dialog.SafeFileNames[i],
                        Image = new Bitmap(dialog.FileNames[i])
                    };

                    Movement.Add(frame);
                }

                InvalidateFrameBox();

                UpdateScrollFrames();
                ScrollFrame_Scroll(null!, null!);
                UpdateMovementPropertyText();
            }
        }
    }

    private void MenuExit_Click(object sender, EventArgs e) {
        Close();
    }

    #endregion
}
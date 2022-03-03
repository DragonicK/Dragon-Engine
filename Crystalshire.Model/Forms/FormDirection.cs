using Crystalshire.Model.Models;

namespace Crystalshire.Model.Forms {
    public partial class FormDirection : Form {
        public  Directions Directions { get; }
        public FormFrames UpFrames { get; private set; }
        public FormFrames DownFrames { get; private set; }
        public FormFrames LeftFrames { get; private set; }
        public FormFrames RightFrames { get; private set; }

        public FormDirection(Directions directions, bool isDefault) {
            InitializeComponent();

            Directions = directions;    

            if (isDefault) {
                TextId.Enabled = false;
                TextName.Enabled = false;
            }

            TextId.Text = directions.Id.ToString();
            TextName.Text = directions.Name;
            GroupDirections.Text = directions.Name;

            UpFrames = new FormFrames(directions.Up);
            DownFrames = new FormFrames(directions.Down);
            LeftFrames = new FormFrames(directions.Left);
            RightFrames = new FormFrames(directions.Right);
        }

        private void ButtonUp_Click(object sender, EventArgs e) {
            if (UpFrames.IsDisposed) {
                UpFrames = new FormFrames(Directions.Up);
            }

            UpFrames.Show();
        }

        private void ButtonLeft_Click(object sender, EventArgs e) {
            if (LeftFrames.IsDisposed) {
                LeftFrames = new FormFrames(Directions.Left);
            }

            LeftFrames.Show();
        }

        private void ButtonRight_Click(object sender, EventArgs e) {
            if (RightFrames.IsDisposed) {
                RightFrames = new FormFrames(Directions.Right);
            }

            RightFrames.Show();
        }

        private void ButtonDown_Click(object sender, EventArgs e) {
            if (DownFrames.IsDisposed) {
                DownFrames = new FormFrames(Directions.Down);
            }

            DownFrames.Show();
        }

        private void TextId_TextChanged(object sender, EventArgs e) {
            if (Directions is not null) {
                Directions.Id = Util.GetValue((TextBox)sender);
            }
        }

        private void TextName_TextChanged(object sender, EventArgs e) {
            if (Directions is not null) {
                Directions.Name = ((TextBox)sender).Text;
            }
        }
    }
}

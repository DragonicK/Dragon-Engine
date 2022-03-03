using Crystalshire.Model.Models;

namespace Crystalshire.Model.Forms {
    public partial class FormMasking : Form {
        private const int Count = 160 / 32;
        private const int X = 0;
        private const int Y = 0;
        private const int ModelWidth = 320;
        private const int ModelHeight = 320;
        private const int ModelSize = 64;

        private Frame Frame { get; }

        public FormMasking(Frame frame) {
            InitializeComponent();

            Frame = frame;
        }

        private void PictureFrame_Paint(object sender, PaintEventArgs e) {
            e.Graphics.Clear(Color.Black);

            if (Frame is not null) {
                if (Frame.Image is not null) {
                    e.Graphics.DrawImage(Frame.Image, 0, 0, ModelWidth, ModelHeight);
                }
            }

            for (var i = 1; i <= Count; ++i) {
                // Horizontal Lines
                e.Graphics.DrawLine(Pens.Coral, X, (Y + ModelSize) * i, ModelWidth, (Y + ModelSize) * i);
                // Vertical Lines
                e.Graphics.DrawLine(Pens.Coral, (X + ModelSize) * i, Y, (X + ModelSize) * i, ModelHeight);
            }            
        }
    }
}
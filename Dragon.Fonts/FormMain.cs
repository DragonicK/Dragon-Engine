namespace Dragon.Fonts {
    public partial class FormMain : Form {
        public JetBrainsMono? JetBrainsMono { get; set; }
   
        public FormMain() {
            InitializeComponent();
        }

        private void ChangeFont(Control control) {
            var controls = control.Controls;

            ChangeFontStye(control);

            if (control is MenuStrip) {
                var menu = control as MenuStrip;

                if (menu is not null) {
                    foreach (ToolStripItem item in menu.Items) {
                        ChangeFontStye(item);
                    }
                }
            }

            foreach (Control _control in controls) {
                ChangeFont(_control);
            }
        }

        private void ChangeFontStye(Control control) {
            if (JetBrainsMono is not null) {
                control.Font = JetBrainsMono.GetFont(FontStyle.Regular);
            }
        }

        private void ChangeFontStye(ToolStripItem control) {
            if (JetBrainsMono is not null) {
                control.Font = JetBrainsMono.GetFont(FontStyle.Regular);
            }
        }

        private void FormMain_Load(object sender, EventArgs e) {
            ChangeFont(this);
        }

        private void FormMain_Resize(object sender, EventArgs e) {
            var listHeight = ListIndex.Size.Height;

            ChangeButtonAddLocation(listHeight);
            ChangeButtonDeleteLocation(listHeight);
            ChangeButtonClearLocation(listHeight);
        }

        private void ChangeButtonAddLocation(int listHeight) {
            const int DefaultOffset = 40;

            var x = ButtonAdd.Location.X;

            ButtonAdd.Location = new Point(x, listHeight + DefaultOffset);
        }

        private void ChangeButtonDeleteLocation(int listHeight) {
            const int DefaultOffset = 40;

            var x = ButtonDelete.Location.X;

            ButtonDelete.Location = new Point(x, listHeight + DefaultOffset);
        }

        private void ChangeButtonClearLocation(int listHeight) {
            const int DefaultOffset = 70;

            var x = ButtonClear.Location.X;

            ButtonClear.Location = new Point(x, listHeight + DefaultOffset);
        }
    }
}
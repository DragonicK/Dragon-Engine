using Crystalshire.Maps.Editor;

namespace Crystalshire.Maps;

public partial class FormMain : Form {
    private FontLoader FontLoader { get; set; }
    private JetBrainsMono JetBrainsMono { get; set; }

    public FormMain() {
        InitializeComponent();

        FontLoader = new FontLoader();

        FontLoader.LoadFromResource();

        JetBrainsMono = new JetBrainsMono(FontLoader);
    }

    private void button1_Click(object sender, EventArgs e) {
        var f = new FormProperty(JetBrainsMono, null, this);
        f.ShowDialog();
    }

    public void UpdateMapSize() {

    }

    public void ChangeSelectedMapName(string name) {

    }
}
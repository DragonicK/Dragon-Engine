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

    }

    public void UpdateMapSize() {

    }

    public void ChangeSelectedMapName(string name) {

    }
}
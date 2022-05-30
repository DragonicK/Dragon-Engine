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
}
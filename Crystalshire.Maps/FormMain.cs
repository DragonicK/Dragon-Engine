using Crystalshire.Maps.Editor;
using Crystalshire.Maps.Images;

namespace Crystalshire.Maps;

public partial class FormMain : Form {
    private FontLoader FontLoader { get; set; }
    private JetBrainsMono JetBrainsMono { get; set; }
    private IGrid Grid { get; set; }
    private ITileset Tiles { get; set; }

    private const int NoMapSelected = -1;
    private const int MaxDirections = 4;
    private const int DirectionImageSize = 8;

    public FormMain() {
        InitializeComponent();

        FontLoader = new FontLoader();

        FontLoader.LoadFromResource();

        JetBrainsMono = new JetBrainsMono(FontLoader);
    }

    public FormMain(JetBrainsMono jetBrainsMono) {

    }

    private void ChangeFont(Control control) {
        var controls = control.Controls;

        ChangeFontStye(control);

        foreach (Control _control in controls) {
            ChangeFontStye(_control);
        }
    }

    private void ChangeFontStye(Control control) {
        control.Font = JetBrainsMono.GetFont(FontStyle.Regular);
    }

    public void UpdateMapSize() {

    }

    public void ChangeSelectedMapName(string name) {

    }

    #region Menu File

    private void MenuFileNew_Click(object sender, EventArgs e) {

    }

    private void MenuFileOpen_Click(object sender, EventArgs e) {

    }

    private void MenuFileOpenAs_Click(object sender, EventArgs e) {

    }

    private void MenuFileSave_Click(object sender, EventArgs e) {

    }

    private void MenuFileSaveAs_Click(object sender, EventArgs e) {

    }

    private void MenuFileSaveAll_Click(object sender, EventArgs e) {

    }

    private void MenuFileClose_Click(object sender, EventArgs e) {

    }

    private void MenuFileExit_Click(object sender, EventArgs e) {

    }

    #endregion

    #region Menu Edit

    private void MenuEditFill_Click(object sender, EventArgs e) {

    }

    private void MenuEditClear_Click(object sender, EventArgs e) {

    }

    #endregion

    #region Menu Export

    private void MenuExportEngine_Click(object sender, EventArgs e) {

    }

    private void MenuExportPng_Click(object sender, EventArgs e) {

    }

    #endregion

    #region Menu Property

    private void MenuProperty_Click(object sender, EventArgs e) {

    }

    #endregion

    #region Menu View

    private void MenuViewGround_Click(object sender, EventArgs e) {

    }

    private void MenuViewMask1_Click(object sender, EventArgs e) {

    }

    private void MenuViewMask2_Click(object sender, EventArgs e) {

    }

    private void MenuViewFringe1_Click(object sender, EventArgs e) {

    }

    private void MenuViewFringe2_Click(object sender, EventArgs e) {

    }

    private void MenuViewGrid_Click(object sender, EventArgs e) {

    }

    #endregion
}
using Dragon.Core.Content;
using Dragon.Core.Serialization;
using Dragon.Core.Model.Animations;

using Dragon.Animator.Common;
using Dragon.Animator.Animations;

namespace Dragon.Animator;

public partial class FormMain : Form {
    public JetBrainsMono? JetBrainsMono { get; set; }
    public Configuration Configuration { get; private set; }
    public IDatabase<Animation> Animations { get; private set; }
    public List<Bitmap> Resources { get; private set; }

    private const string _File = "Configuration.json";

    public FormMain() {
        InitializeComponent();


        if (File.Exists(_File)) {
            Configuration = Json.Get<Configuration>(_File)!;
        }
        else {
            Configuration = new Configuration();
        }

        Json.Save(_File, Configuration);

        TextResourcePath.Text = Configuration.ResourcePath;
        TextClientOutput.Text = Configuration.OutputClientPath;

        Animations = new Core.Content.Animations {
            FileName = "Animations.dat",
            Folder = "./Content"
        };

        Resources = new List<Bitmap>();

        LoadResources();
    }

    private void MenuExit_Click(object sender, EventArgs e) {
        Application.Exit();
    }

    private void ChangeFont(Control control) {
        var controls = control.Controls;

        ChangeFontStyle(control);

        if (control is MenuStrip) {
            var menu = control as MenuStrip;

            if (menu is not null) {
                foreach (ToolStripItem item in menu.Items) {
                    ChangeFontStyle(item);
                }
            }
        }

        foreach (Control _control in controls) {
            ChangeFont(_control);
        }
    }

    private void ChangeFontStyle(Control control) {
        if (JetBrainsMono is not null) {
            control.Font = JetBrainsMono.GetFont(FontStyle.Regular);
        }
    }

    private void ChangeFontStyle(ToolStripItem control) {
        if (JetBrainsMono is not null) {
            control.Font = JetBrainsMono.GetFont(FontStyle.Regular);
        }
    }

    private void LoadResources() {
        const string Resource = "./Resource";

        if (Directory.Exists(Resource)) {
            var files = Directory.GetFiles(Resource);

            foreach (var file in files) {
                Resources.Add(new Bitmap(file));
            }
        }
    }

    private void FormMain_Load(object sender, EventArgs e) {
        ChangeFont(this);
    }

    private void MenuAnimation_Click(object sender, EventArgs e) {
        Animations.Clear();
        Animations.Load();

        var f = new FormAnimation(Configuration, Animations, Resources);

        ChangeFont(f);

        f.Show();
    }

    private void FormMain_FormClosed(object sender, FormClosedEventArgs e) {
        Json.Save(_File, Configuration);
    }
}
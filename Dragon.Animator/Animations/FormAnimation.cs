using Dragon.Core.Content;
using Dragon.Core.Model.Animations;

using Dragon.Animator.Common;

namespace Dragon.Animator.Animations {
    public partial class FormAnimation : Form {
        public IDatabase<Animation> Database { get; }
        public Animation? Element { get; private set; }
        public Configuration Configuration { get; private set; }
        public int SelectedIndex { get; private set; } = Util.NotSelected;

        public FormAnimation(Configuration configuration, IDatabase<Animation> database) {
            InitializeComponent();

            Configuration = configuration;
            Database = database;

            Element = null;

            // Util.UpdateList(Database, ListIndex);
        }
    }
}

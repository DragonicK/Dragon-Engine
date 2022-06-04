using Crystalshire.Packer.Editor;

namespace Crystalshire.Packer;

public partial class FormMain : Form {
    public JetBrainsMono? JetBrainsMono { get; set; }

    private const int ExitSuccess = 0;
    private const int InvalidPosition = -1;

    private const int MoveUp = -1;
    private const int MoveDown = 1;

    private int[]? SelectedIndexes;

    readonly IPackage _package;

    public FormMain() {
        InitializeComponent();

        _package = new Package();
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

    private string GetNameWithoutExtension(string name) {
        const int NameIndex = 0;

        var names = name.Split('.');
        return names[NameIndex];
    }

    private string GetExtension(string name) {
        var parse = name.Split('.');

        parse[0] = string.Empty;

        var extension = string.Join(".", parse);

        return extension.Remove(0, 1);
    }

    private string GetPathToExport() {
        var path = string.Empty;

        if (_package.Count > 0) {
            var dialog = new FolderBrowserDialog() {
                ShowNewFolderButton = true,
                RootFolder = Environment.SpecialFolder.MyComputer
            };

            var r = dialog.ShowDialog();

            if (r == DialogResult.OK) {
                path = dialog.SelectedPath;
            }
        }

        return path;
    }

    #region Menu File

    private async void MenuFileOpen_Click(object sender, EventArgs e) {
        var dialog = new OpenFileDialog {
            Multiselect = false,
            CheckFileExists = true,
            CheckPathExists = true,
            InitialDirectory = Environment.CurrentDirectory,
            Filter = "Pak Files (*.pak)|*.pak"
        };

        var r = dialog.ShowDialog();

        if (r == DialogResult.OK) {
            var operation = PackageOperation.Success;
            var file = dialog.FileName;

            if (!IsValidPassphrase()) {
                operation = PackageOperation.PassphraseEmpty;
            }
            else {
                AllowMenu(false);

                var handler = new PackageHandler(OnProgressChanged);

                await Task.Run(() =>
                    operation = handler.Open(file, GetPassphrase(), _package)
                );
            }

            UpdateList();
            ShowMessage(operation);
            AllowMenu(true);
        }
    }

    private async void MenuFileSave_Click(object sender, EventArgs e) {
        var dialog = new SaveFileDialog {
            InitialDirectory = Environment.CurrentDirectory,
            Filter = "Pak Files (*.pak)|*.pak",
            CheckPathExists = true
        };

        var r = dialog.ShowDialog();

        if (r == DialogResult.OK) {
            var operation = PackageOperation.Success;
            var file = dialog.FileName;

            if (!IsValidPassphrase()) {
                operation = PackageOperation.PassphraseEmpty;
            }
            else {
                AllowMenu(false);

                var handler = new PackageHandler(OnProgressChanged);

                await Task.Run(() =>
                    operation = handler.Save(file, GetPassphrase(), _package)
                );
            }

            ShowMessage(operation);
            AllowMenu(true);
        }
    }

    private void MenuExportSelected_Click(object sender, EventArgs e) {
        ExportSelectedIndexes();
    }

    private void MenuExportAll_Click(object sender, EventArgs e) {
        ExportAll();
    }

    private void MenuFileExit_Click(object sender, EventArgs e) {
        _package.Clear();

        Application.Exit();
    }

    private void ShowMessage(PackageOperation r) {
        if (r != PackageOperation.Success) {
            MessageBox.Show(GetMessage(r), "Warning");
        }
    }

    private string GetMessage(PackageOperation r) => r switch {
        PackageOperation.WrongPassphrase => "Incorrect Passphrase!",
        PackageOperation.FailedKey => "Failed to create key!",
        PackageOperation.PassphraseEmpty => "Passphrase cannot be empty!",
        _ => string.Empty
    };

    #endregion

    #region Menu Edit

    private void MenuEditAdd_Click(object sender, EventArgs e) {
        AddFile(InvalidPosition);
    }

    private void MenuEditClear_Click(object sender, EventArgs e) {
        Clear();
    }

    private void MenuEditRemove_Click(object sender, EventArgs e) {
        if (SelectedIndexes is not null) {
            if (SelectedIndexes.Length > 0) {
                RemoveSelectedIndexes();
                UpdateList();
            }
        }
    }

    private void MenuEditReplace_Click(object sender, EventArgs e) {

    }

    #endregion

    #region Menu Context

    private void MenuContextAdd_Click(object sender, EventArgs e) {
        AddFile(InvalidPosition);
    }

    private void MenuContextMoveUp_Click(object sender, EventArgs e) {
        if (SelectedIndexes is not null) {
            if (ListPack.SelectedIndices.Count > 0) {
                if (_package.MoveUp(SelectedIndexes)) {
                    UpdateList();

                    ListPack.EnsureVisible(SelectedIndexes[0] - 1);

                    SelectItem(IncrementIndexPosition(MoveUp));
                }
            }
        }
    }

    private void MenuContextMoveDown_Click(object sender, EventArgs e) {
        if (SelectedIndexes is not null) {
            if (ListPack.SelectedIndices.Count > 0) {
                if (_package.MoveDown(SelectedIndexes)) {
                    UpdateList();

                    ListPack.EnsureVisible(SelectedIndexes[0] + 1);

                    SelectItem(IncrementIndexPosition(MoveDown));
                }
            }
        }
    }

    private void MenuContextMoveTo_Click(object sender, EventArgs e) {
        if (SelectedIndexes is not null) {
            var index = GetResponseFromInputBox();

            if (index > InvalidPosition) {
                if (_package.MoveTo(ref index, SelectedIndexes)) {
                    UpdateList();

                    ListPack.EnsureVisible(index);

                    SelectItem(CreateNewIndexPositionFrom(index));
                }
            }
        }
    }

    private void MenuContextInsert_Click(object sender, EventArgs e) {
        if (ListPack.SelectedIndices.Count > 0) {
            AddFile(ListPack.SelectedIndices[0]);
        }
    }

    private void MenuContextRemove_Click(object sender, EventArgs e) {
        if (SelectedIndexes is not null) {
            if (SelectedIndexes.Length > 0) {
                RemoveSelectedIndexes();
                UpdateList();
            }
        }
    }

    private void MenuContextReplace_Click(object sender, EventArgs e) {

    }

    private void MenuContextExportSelected_Click(object sender, EventArgs e) {
        ExportSelectedIndexes();
    }

    private void MenuContextExportAll_Click(object sender, EventArgs e) {
        ExportAll();
    }

    #endregion

    #region Export 

    private async void ExportAll() {
        if (_package.Count > 0) {
            var path = GetPathToExport();

            if (!string.IsNullOrEmpty(path)) {
                AllowMenu(false);

                var handler = new PackageExporter(OnProgressChanged);

                await Task.Run(() =>
                    handler.Export(path, _package)
                );

                AllowMenu(true);
            }
        }
    }

    private async void ExportSelectedIndexes() {
        if (SelectedIndexes is not null) {
            if (SelectedIndexes.Length > 0) {
                var path = GetPathToExport();

                if (!string.IsNullOrEmpty(path)) {
                    AllowMenu(false);

                    var handler = new PackageExporter(OnProgressChanged);

                    await Task.Run(() =>
                        handler.Export(path, SelectedIndexes, _package)
                    );

                    AllowMenu(true);
                }
            }
        }
    }

    #endregion

    #region Add

    private async void AddFile(int index) {
        var dialog = new OpenFileDialog {
            InitialDirectory = Environment.CurrentDirectory,
            Filter = "All Files (*.*)|*.*",
            CheckFileExists = true,
            CheckPathExists = true,
            Multiselect = true
        };

        var r = dialog.ShowDialog();

        if (r == DialogResult.OK) {
            AllowMenu(false);

            await Task.Run(() => ParseFileNames(index, dialog.FileNames, dialog.SafeFileNames));

            UpdateList();

            AllowMenu(true);

            CheckForAllowExtraMenu();
        }
    }

    private int ParseFileNames(int index, string[] fileNames, string[] safeNames) {
        var length = fileNames.Length;

        if (length > 0) {
            for (var i = 0; i < length; i++) {
                var file = fileNames[i];
                var name = safeNames[i];

                var f = new PackageFile {
                    Name = GetNameWithoutExtension(name),
                    Extension = GetExtension(name),
                    Bytes = File.ReadAllBytes(file)
                };

                if (f.Extension == "png" || f.Extension == "jpeg") {
                    var bitmap = new Bitmap(file);

                    f.Width = bitmap.Width;
                    f.Height = bitmap.Height;

                    bitmap.Dispose();
                }

                f.Length = f.Bytes.Length;

                if (index == InvalidPosition) {
                    _package.Add(f);
                }
                else {
                    _package.Insert(index, f);
                }

                var args = new PackageArgs() {
                    Name = name,
                    Count = i + 1,
                    Maximum = length
                };

                OnProgressChanged(this, args);
            }
        }

        return ExitSuccess;
    }

    #endregion

    #region List Pack

    private void SelectItem(int[] selected) {
        ListPack.Focus();

        if (ListPack.Items.Count > 0) {
            for (var i = 0; i < selected.Length; i++) {
                ListPack.Items[selected[i]].Selected = true;
                ListPack.Select();
            }
        }
    }

    private void ListPack_MouseDown(object sender, MouseEventArgs e) {
        if (e.Button == MouseButtons.Right) {
            MenuContextRemove.Enabled = false;
            MenuContextExport.Enabled = false;
            MenuContextMoveUp.Enabled = false;
            MenuContextMoveDown.Enabled = false;
            MenuContextMoveTo.Enabled = false;
            MenuContextInsert.Enabled = false;

            ContextMenuMain.Show(ListPack, e.Location);
        }
    }

    private void ListPack_MouseClick(object sender, MouseEventArgs e) {
        if (ListPack.Items.Count == 0) {
            SelectedIndexes = null;
        }

        var isAllowed = CouldAllowMenu();

        MenuContextRemove.Enabled = isAllowed;
        MenuContextExport.Enabled = isAllowed;
        MenuContextMoveUp.Enabled = isAllowed;
        MenuContextMoveDown.Enabled = isAllowed;
        MenuContextMoveTo.Enabled = isAllowed;

        if (SelectedIndexes is not null && SelectedIndexes.Length == 1 && ListPack.Items.Count > 0) {
            MenuContextInsert.Enabled = true;
        }
        else {
            MenuContextInsert.Enabled = false;
        }
    }

    private void ListPack_SelectedIndexChanged(object sender, EventArgs e) {
        var count = ListPack.SelectedItems.Count;

        SelectedIndexes = new int[count];

        ListPack.SelectedIndices.CopyTo(SelectedIndexes, 0);
    }

    private void UpdateList() {
        ListPack.BeginUpdate();
        ListPack.Items.Clear();

        var list = _package.ToList();

        for (var i = 0; i < list.Count; ++i) {
            var item = new ListViewItem(i.ToString());

            item.SubItems.Add(list[i].Name);
            item.SubItems.Add(list[i].Extension);
            item.SubItems.Add(Util.GetFileSize(list[i].Length));

            ListPack.Items.Add(item);
        }

        LabelCount.Text = $"File Count: {list.Count}";
        ListPack.EndUpdate();
    }

    private bool CouldAllowMenu() {
        if (SelectedIndexes is not null) {
            if (SelectedIndexes.Length > 0) {
                if (ListPack.Items.Count > 0) {
                    return true;
                }
            }
        }

        return false;
    }

    #endregion

    private void RemoveSelectedIndexes() {
        if (SelectedIndexes is not null) {
            var length = SelectedIndexes.Length;

            if (length > 0) {
                _package.Remove(SelectedIndexes);
            }

            SelectedIndexes = null;
        }

        CheckForAllowExtraMenu();
    }

    private void Clear() {
        SelectedIndexes = null;

        _package.Clear();

        UpdateList();

        CheckForAllowExtraMenu();
    }

    private int[] IncrementIndexPosition(int increment) {
        if (SelectedIndexes is not null) {
            var positions = new int[SelectedIndexes.Length];

            for (var i = 0; i < positions.Length; i++) {
                positions[i] = SelectedIndexes[i] + increment;
            }

            return positions;
        }

        return Array.Empty<int>();
    }

    private int[] CreateNewIndexPositionFrom(int index) {
        if (SelectedIndexes is not null) {
            var positions = new int[SelectedIndexes.Length];

            for (var i = 0; i < positions.Length; i++) {
                positions[i] = index++;
            }
            return positions;
        }

        return Array.Empty<int>();
    }

    private bool IsValidPassphrase() {
        return TextPassword.Text.Length > 0;
    }

    private string GetPassphrase() {
        return TextPassword.Text.Trim();
    }

    private void AllowMenu(bool enabled) {
        MenuFile.Enabled = enabled;
        MenuEdit.Enabled = enabled;
    }

    private void CheckForAllowExtraMenu() {
        var allowed = _package.Count > 0;

        MenuFileExport.Enabled = allowed;
        MenuEditClear.Enabled = allowed;
        MenuEditRemove.Enabled = allowed;
    }

    private void OnProgressChanged(object? sender, IPackageArgs args) {
        var count = args.Count;
        var maximum = args.Maximum;

        var percent = Util.GetProgressPercentage(count, maximum);

        LabelStatus.Text = $"Processed {percent}% {count}/{maximum} : {args.Name}";
    }

    public int GetResponseFromInputBox() {
        var input = new InputBoxDialog {
            Caption = "Move To Position ... ",
            Input = "0"
        };

        ChangeFont(input);

        input.ShowInput();

        var response = input.Response;

        var r = int.TryParse(response, out var index);

        return r ? index : InvalidPosition;
    }
}
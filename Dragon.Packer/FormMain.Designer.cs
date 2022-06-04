namespace Dragon.Packer;

partial class FormMain {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
        this.MenuMain = new System.Windows.Forms.MenuStrip();
        this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuFileSave = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuFileExport = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuExportSelected = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuExportAll = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuEditAdd = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuEditClear = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuEditRemove = new System.Windows.Forms.ToolStripMenuItem();
        this.LabelPassword = new System.Windows.Forms.Label();
        this.ListPack = new System.Windows.Forms.ListView();
        this.ColumnIndex = new System.Windows.Forms.ColumnHeader();
        this.ColumnName = new System.Windows.Forms.ColumnHeader();
        this.ColumnType = new System.Windows.Forms.ColumnHeader();
        this.ColumnSize = new System.Windows.Forms.ColumnHeader();
        this.TextPassword = new System.Windows.Forms.TextBox();
        this.StatusStrip = new System.Windows.Forms.StatusStrip();
        this.LabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
        this.LabelCount = new System.Windows.Forms.Label();
        this.ContextMenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.MenuContextAdd = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuContextMoveUp = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuContextMoveDown = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuContextMoveTo = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuContextInsert = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuContextRemove = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuContextExport = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuContextExportSelected = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuContextExportAll = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuMain.SuspendLayout();
        this.StatusStrip.SuspendLayout();
        this.ContextMenuMain.SuspendLayout();
        this.SuspendLayout();
        // 
        // MenuMain
        // 
        this.MenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuEdit});
        this.MenuMain.Location = new System.Drawing.Point(0, 0);
        this.MenuMain.Name = "MenuMain";
        this.MenuMain.Size = new System.Drawing.Size(507, 24);
        this.MenuMain.TabIndex = 0;
        this.MenuMain.Text = "menuStrip1";
        // 
        // MenuFile
        // 
        this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFileOpen,
            this.MenuFileSave,
            this.MenuFileExport,
            this.MenuFileExit});
        this.MenuFile.Name = "MenuFile";
        this.MenuFile.Size = new System.Drawing.Size(37, 20);
        this.MenuFile.Text = "File";
        // 
        // MenuFileOpen
        // 
        this.MenuFileOpen.Name = "MenuFileOpen";
        this.MenuFileOpen.Size = new System.Drawing.Size(108, 22);
        this.MenuFileOpen.Text = "Open";
        this.MenuFileOpen.Click += new System.EventHandler(this.MenuFileOpen_Click);
        // 
        // MenuFileSave
        // 
        this.MenuFileSave.Name = "MenuFileSave";
        this.MenuFileSave.Size = new System.Drawing.Size(108, 22);
        this.MenuFileSave.Text = "Save";
        this.MenuFileSave.Click += new System.EventHandler(this.MenuFileSave_Click);
        // 
        // MenuFileExport
        // 
        this.MenuFileExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuExportSelected,
            this.MenuExportAll});
        this.MenuFileExport.Enabled = false;
        this.MenuFileExport.Name = "MenuFileExport";
        this.MenuFileExport.Size = new System.Drawing.Size(108, 22);
        this.MenuFileExport.Text = "Export";
        // 
        // MenuExportSelected
        // 
        this.MenuExportSelected.Name = "MenuExportSelected";
        this.MenuExportSelected.Size = new System.Drawing.Size(144, 22);
        this.MenuExportSelected.Text = "Selected Files";
        this.MenuExportSelected.Click += new System.EventHandler(this.MenuExportSelected_Click);
        // 
        // MenuExportAll
        // 
        this.MenuExportAll.Name = "MenuExportAll";
        this.MenuExportAll.Size = new System.Drawing.Size(144, 22);
        this.MenuExportAll.Text = "All Files";
        this.MenuExportAll.Click += new System.EventHandler(this.MenuExportAll_Click);
        // 
        // MenuFileExit
        // 
        this.MenuFileExit.Name = "MenuFileExit";
        this.MenuFileExit.Size = new System.Drawing.Size(108, 22);
        this.MenuFileExit.Text = "Exit";
        this.MenuFileExit.Click += new System.EventHandler(this.MenuFileExit_Click);
        // 
        // MenuEdit
        // 
        this.MenuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuEditAdd,
            this.MenuEditClear,
            this.MenuEditRemove});
        this.MenuEdit.Name = "MenuEdit";
        this.MenuEdit.Size = new System.Drawing.Size(39, 20);
        this.MenuEdit.Text = "Edit";
        // 
        // MenuEditAdd
        // 
        this.MenuEditAdd.Name = "MenuEditAdd";
        this.MenuEditAdd.Size = new System.Drawing.Size(180, 22);
        this.MenuEditAdd.Text = "Add";
        this.MenuEditAdd.Click += new System.EventHandler(this.MenuEditAdd_Click);
        // 
        // MenuEditClear
        // 
        this.MenuEditClear.Enabled = false;
        this.MenuEditClear.Name = "MenuEditClear";
        this.MenuEditClear.Size = new System.Drawing.Size(180, 22);
        this.MenuEditClear.Text = "Clear";
        this.MenuEditClear.Click += new System.EventHandler(this.MenuEditClear_Click);
        // 
        // MenuEditRemove
        // 
        this.MenuEditRemove.Enabled = false;
        this.MenuEditRemove.Name = "MenuEditRemove";
        this.MenuEditRemove.Size = new System.Drawing.Size(180, 22);
        this.MenuEditRemove.Text = "Remove";
        this.MenuEditRemove.Click += new System.EventHandler(this.MenuEditRemove_Click);
        // 
        // LabelPassword
        // 
        this.LabelPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.LabelPassword.Location = new System.Drawing.Point(0, 24);
        this.LabelPassword.Name = "LabelPassword";
        this.LabelPassword.Size = new System.Drawing.Size(507, 22);
        this.LabelPassword.TabIndex = 1;
        this.LabelPassword.Text = "Passphrase";
        this.LabelPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // ListPack
        // 
        this.ListPack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.ListPack.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnIndex,
            this.ColumnName,
            this.ColumnType,
            this.ColumnSize});
        this.ListPack.FullRowSelect = true;
        this.ListPack.GridLines = true;
        this.ListPack.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
        this.ListPack.Location = new System.Drawing.Point(12, 78);
        this.ListPack.Name = "ListPack";
        this.ListPack.Size = new System.Drawing.Size(483, 431);
        this.ListPack.TabIndex = 2;
        this.ListPack.UseCompatibleStateImageBehavior = false;
        this.ListPack.View = System.Windows.Forms.View.Details;
        this.ListPack.SelectedIndexChanged += new System.EventHandler(this.ListPack_SelectedIndexChanged);
        this.ListPack.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListPack_MouseClick);
        this.ListPack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListPack_MouseDown);
        // 
        // ColumnIndex
        // 
        this.ColumnIndex.Text = "Index";
        // 
        // ColumnName
        // 
        this.ColumnName.Text = "Name";
        this.ColumnName.Width = 230;
        // 
        // ColumnType
        // 
        this.ColumnType.Text = "Type";
        this.ColumnType.Width = 70;
        // 
        // ColumnSize
        // 
        this.ColumnSize.Text = "Size";
        this.ColumnSize.Width = 120;
        // 
        // TextPassword
        // 
        this.TextPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.TextPassword.Location = new System.Drawing.Point(12, 49);
        this.TextPassword.Name = "TextPassword";
        this.TextPassword.Size = new System.Drawing.Size(483, 23);
        this.TextPassword.TabIndex = 3;
        this.TextPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // StatusStrip
        // 
        this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelStatus});
        this.StatusStrip.Location = new System.Drawing.Point(0, 539);
        this.StatusStrip.Name = "StatusStrip";
        this.StatusStrip.Size = new System.Drawing.Size(507, 22);
        this.StatusStrip.TabIndex = 4;
        this.StatusStrip.Text = "statusStrip1";
        // 
        // LabelStatus
        // 
        this.LabelStatus.Name = "LabelStatus";
        this.LabelStatus.Size = new System.Drawing.Size(39, 17);
        this.LabelStatus.Text = "Ready";
        // 
        // LabelCount
        // 
        this.LabelCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.LabelCount.Location = new System.Drawing.Point(0, 516);
        this.LabelCount.Name = "LabelCount";
        this.LabelCount.Size = new System.Drawing.Size(507, 15);
        this.LabelCount.TabIndex = 5;
        this.LabelCount.Text = "File Count: 0";
        // 
        // ContextMenuMain
        // 
        this.ContextMenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuContextAdd,
            this.MenuContextMoveUp,
            this.MenuContextMoveDown,
            this.MenuContextMoveTo,
            this.MenuContextInsert,
            this.MenuContextRemove,
            this.MenuContextExport});
        this.ContextMenuMain.Name = "ContextMenuMain";
        this.ContextMenuMain.Size = new System.Drawing.Size(181, 180);
        // 
        // MenuContextAdd
        // 
        this.MenuContextAdd.Name = "MenuContextAdd";
        this.MenuContextAdd.Size = new System.Drawing.Size(180, 22);
        this.MenuContextAdd.Text = "Add";
        this.MenuContextAdd.Click += new System.EventHandler(this.MenuContextAdd_Click);
        // 
        // MenuContextMoveUp
        // 
        this.MenuContextMoveUp.Name = "MenuContextMoveUp";
        this.MenuContextMoveUp.Size = new System.Drawing.Size(180, 22);
        this.MenuContextMoveUp.Text = "Move Up";
        this.MenuContextMoveUp.Click += new System.EventHandler(this.MenuContextMoveUp_Click);
        // 
        // MenuContextMoveDown
        // 
        this.MenuContextMoveDown.Name = "MenuContextMoveDown";
        this.MenuContextMoveDown.Size = new System.Drawing.Size(180, 22);
        this.MenuContextMoveDown.Text = "Move Down";
        this.MenuContextMoveDown.Click += new System.EventHandler(this.MenuContextMoveDown_Click);
        // 
        // MenuContextMoveTo
        // 
        this.MenuContextMoveTo.Name = "MenuContextMoveTo";
        this.MenuContextMoveTo.Size = new System.Drawing.Size(180, 22);
        this.MenuContextMoveTo.Text = "Move To";
        this.MenuContextMoveTo.Click += new System.EventHandler(this.MenuContextMoveTo_Click);
        // 
        // MenuContextInsert
        // 
        this.MenuContextInsert.Name = "MenuContextInsert";
        this.MenuContextInsert.Size = new System.Drawing.Size(180, 22);
        this.MenuContextInsert.Text = "Insert";
        this.MenuContextInsert.Click += new System.EventHandler(this.MenuContextInsert_Click);
        // 
        // MenuContextRemove
        // 
        this.MenuContextRemove.Name = "MenuContextRemove";
        this.MenuContextRemove.Size = new System.Drawing.Size(180, 22);
        this.MenuContextRemove.Text = "Remove";
        this.MenuContextRemove.Click += new System.EventHandler(this.MenuContextRemove_Click);
        // 
        // MenuContextExport
        // 
        this.MenuContextExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuContextExportSelected,
            this.MenuContextExportAll});
        this.MenuContextExport.Name = "MenuContextExport";
        this.MenuContextExport.Size = new System.Drawing.Size(180, 22);
        this.MenuContextExport.Text = "Export";
        // 
        // MenuContextExportSelected
        // 
        this.MenuContextExportSelected.Name = "MenuContextExportSelected";
        this.MenuContextExportSelected.Size = new System.Drawing.Size(180, 22);
        this.MenuContextExportSelected.Text = "Selected Files";
        this.MenuContextExportSelected.Click += new System.EventHandler(this.MenuContextExportSelected_Click);
        // 
        // MenuContextExportAll
        // 
        this.MenuContextExportAll.Name = "MenuContextExportAll";
        this.MenuContextExportAll.Size = new System.Drawing.Size(180, 22);
        this.MenuContextExportAll.Text = "All Files";
        this.MenuContextExportAll.Click += new System.EventHandler(this.MenuContextExportAll_Click);
        // 
        // FormMain
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(507, 561);
        this.Controls.Add(this.LabelCount);
        this.Controls.Add(this.StatusStrip);
        this.Controls.Add(this.TextPassword);
        this.Controls.Add(this.ListPack);
        this.Controls.Add(this.LabelPassword);
        this.Controls.Add(this.MenuMain);
        this.DoubleBuffered = true;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MainMenuStrip = this.MenuMain;
        this.Name = "FormMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Dragon Engine - Resource Packer";
        this.Load += new System.EventHandler(this.FormMain_Load);
        this.MenuMain.ResumeLayout(false);
        this.MenuMain.PerformLayout();
        this.StatusStrip.ResumeLayout(false);
        this.StatusStrip.PerformLayout();
        this.ContextMenuMain.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private MenuStrip MenuMain;
    private ToolStripMenuItem MenuFile;
    private ToolStripMenuItem MenuEdit;
    private Label LabelPassword;
    private ListView ListPack;
    private TextBox TextPassword;
    private StatusStrip StatusStrip;
    private ToolStripStatusLabel LabelStatus;
    private Label LabelCount;
    private ToolStripMenuItem MenuFileOpen;
    private ToolStripMenuItem MenuFileSave;
    private ToolStripMenuItem MenuFileExport;
    private ToolStripMenuItem MenuFileExit;
    private ToolStripMenuItem MenuEditAdd;
    private ToolStripMenuItem MenuEditRemove;
    private ToolStripMenuItem MenuEditClear;
    private ColumnHeader ColumnIndex;
    private ColumnHeader ColumnName;
    private ColumnHeader ColumnType;
    private ColumnHeader ColumnSize;
    private ToolStripMenuItem MenuExportSelected;
    private ToolStripMenuItem MenuExportAll;
    private ContextMenuStrip ContextMenuMain;
    private ToolStripMenuItem MenuContextAdd;
    private ToolStripMenuItem MenuContextMoveUp;
    private ToolStripMenuItem MenuContextMoveDown;
    private ToolStripMenuItem MenuContextMoveTo;
    private ToolStripMenuItem MenuContextInsert;
    private ToolStripMenuItem MenuContextRemove;
    private ToolStripMenuItem MenuContextExport;
    private ToolStripMenuItem MenuContextExportSelected;
    private ToolStripMenuItem MenuContextExportAll;
}
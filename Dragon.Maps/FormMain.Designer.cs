namespace Dragon.Maps;
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
        this.Menu = new System.Windows.Forms.MenuStrip();
        this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuFileNew = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuFileOpenAs = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuFileSave = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuFileSaveAll = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
        this.MenuFileClose = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
        this.MenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuEditClear = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuEditFill = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuExport = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuExportEngine = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuExportPng = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuProperty = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuView = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuViewGround = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuViewMask1 = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuViewMask2 = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuViewFringe1 = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuViewFringe2 = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuViewGrid = new System.Windows.Forms.ToolStripMenuItem();
        this.GroupMap = new System.Windows.Forms.GroupBox();
        this.GroupGridOpacity = new System.Windows.Forms.GroupBox();
        this.LabelGridOpacity = new System.Windows.Forms.Label();
        this.ScrollGridOpacity = new System.Windows.Forms.HScrollBar();
        this.GroupAttributes = new System.Windows.Forms.GroupBox();
        this.ButtonForeColor4 = new System.Windows.Forms.Button();
        this.ButtonForeColor3 = new System.Windows.Forms.Button();
        this.ButtonForeColor2 = new System.Windows.Forms.Button();
        this.ButtonForeColor1 = new System.Windows.Forms.Button();
        this.ButtonForeColor0 = new System.Windows.Forms.Button();
        this.ButtonClearAttribute = new System.Windows.Forms.Button();
        this.RadioWarp = new System.Windows.Forms.RadioButton();
        this.RadioChat = new System.Windows.Forms.RadioButton();
        this.RadioTrap = new System.Windows.Forms.RadioButton();
        this.RadioAvoid = new System.Windows.Forms.RadioButton();
        this.RadioBlock = new System.Windows.Forms.RadioButton();
        this.GroupRegion = new System.Windows.Forms.GroupBox();
        this.ButtonClearDirection = new System.Windows.Forms.Button();
        this.RadioDirection = new System.Windows.Forms.RadioButton();
        this.RadioAttributes = new System.Windows.Forms.RadioButton();
        this.RadioFringe2 = new System.Windows.Forms.RadioButton();
        this.RadioFringe1 = new System.Windows.Forms.RadioButton();
        this.RadioMask2 = new System.Windows.Forms.RadioButton();
        this.RadioMask1 = new System.Windows.Forms.RadioButton();
        this.RadioGround = new System.Windows.Forms.RadioButton();
        this.LabelTileId = new System.Windows.Forms.Label();
        this.ScrollTileX = new System.Windows.Forms.HScrollBar();
        this.ScrollTileY = new System.Windows.Forms.VScrollBar();
        this.ScrollTileId = new System.Windows.Forms.HScrollBar();
        this.PictureTile = new System.Windows.Forms.PictureBox();
        this.ScrollMapY = new System.Windows.Forms.HScrollBar();
        this.ScrollMapX = new System.Windows.Forms.HScrollBar();
        this.LabelMapY = new System.Windows.Forms.Label();
        this.LabelMapX = new System.Windows.Forms.Label();
        this.PictureMap = new System.Windows.Forms.PictureBox();
        this.TabMaps = new System.Windows.Forms.TabControl();
        this.Menu.SuspendLayout();
        this.GroupMap.SuspendLayout();
        this.GroupGridOpacity.SuspendLayout();
        this.GroupAttributes.SuspendLayout();
        this.GroupRegion.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.PictureTile)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.PictureMap)).BeginInit();
        this.SuspendLayout();
        // 
        // Menu
        // 
        this.Menu.Font = new System.Drawing.Font("JetBrains Mono", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuEdit,
            this.MenuExport,
            this.MenuProperty,
            this.MenuView});
        this.Menu.Location = new System.Drawing.Point(0, 0);
        this.Menu.Name = "Menu";
        this.Menu.Size = new System.Drawing.Size(1008, 24);
        this.Menu.TabIndex = 0;
        this.Menu.Text = "menuStrip1";
        // 
        // MenuFile
        // 
        this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFileNew,
            this.MenuFileOpen,
            this.MenuFileOpenAs,
            this.MenuFileSave,
            this.MenuFileSaveAs,
            this.MenuFileSaveAll,
            this.toolStripMenuItem2,
            this.MenuFileClose,
            this.toolStripMenuItem1,
            this.MenuFileExit});
        this.MenuFile.Name = "MenuFile";
        this.MenuFile.Size = new System.Drawing.Size(47, 20);
        this.MenuFile.Text = "File";
        // 
        // MenuFileNew
        // 
        this.MenuFileNew.Name = "MenuFileNew";
        this.MenuFileNew.Size = new System.Drawing.Size(158, 22);
        this.MenuFileNew.Text = "New";
        this.MenuFileNew.Click += new System.EventHandler(this.MenuFileNew_Click);
        // 
        // MenuFileOpen
        // 
        this.MenuFileOpen.Name = "MenuFileOpen";
        this.MenuFileOpen.Size = new System.Drawing.Size(158, 22);
        this.MenuFileOpen.Text = "Open";
        this.MenuFileOpen.Click += new System.EventHandler(this.MenuFileOpen_Click);
        // 
        // MenuFileOpenAs
        // 
        this.MenuFileOpenAs.Name = "MenuFileOpenAs";
        this.MenuFileOpenAs.Size = new System.Drawing.Size(158, 22);
        this.MenuFileOpenAs.Text = "Open as new";
        this.MenuFileOpenAs.Click += new System.EventHandler(this.MenuFileOpenAs_Click);
        // 
        // MenuFileSave
        // 
        this.MenuFileSave.Name = "MenuFileSave";
        this.MenuFileSave.Size = new System.Drawing.Size(158, 22);
        this.MenuFileSave.Text = "Save";
        this.MenuFileSave.Click += new System.EventHandler(this.MenuFileSave_Click);
        // 
        // MenuFileSaveAs
        // 
        this.MenuFileSaveAs.Name = "MenuFileSaveAs";
        this.MenuFileSaveAs.Size = new System.Drawing.Size(158, 22);
        this.MenuFileSaveAs.Text = "Save as copy";
        this.MenuFileSaveAs.Click += new System.EventHandler(this.MenuFileSaveAs_Click);
        // 
        // MenuFileSaveAll
        // 
        this.MenuFileSaveAll.Enabled = false;
        this.MenuFileSaveAll.Name = "MenuFileSaveAll";
        this.MenuFileSaveAll.Size = new System.Drawing.Size(158, 22);
        this.MenuFileSaveAll.Text = "Save All";
        this.MenuFileSaveAll.Click += new System.EventHandler(this.MenuFileSaveAll_Click);
        // 
        // toolStripMenuItem2
        // 
        this.toolStripMenuItem2.Name = "toolStripMenuItem2";
        this.toolStripMenuItem2.Size = new System.Drawing.Size(155, 6);
        // 
        // MenuFileClose
        // 
        this.MenuFileClose.Name = "MenuFileClose";
        this.MenuFileClose.Size = new System.Drawing.Size(158, 22);
        this.MenuFileClose.Text = "Close";
        this.MenuFileClose.Click += new System.EventHandler(this.MenuFileClose_Click);
        // 
        // toolStripMenuItem1
        // 
        this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 6);
        // 
        // MenuFileExit
        // 
        this.MenuFileExit.Name = "MenuFileExit";
        this.MenuFileExit.Size = new System.Drawing.Size(158, 22);
        this.MenuFileExit.Text = "Exit";
        this.MenuFileExit.Click += new System.EventHandler(this.MenuFileExit_Click);
        // 
        // MenuEdit
        // 
        this.MenuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuEditClear,
            this.MenuEditFill});
        this.MenuEdit.Name = "MenuEdit";
        this.MenuEdit.Size = new System.Drawing.Size(47, 20);
        this.MenuEdit.Text = "Edit";
        // 
        // MenuEditClear
        // 
        this.MenuEditClear.Name = "MenuEditClear";
        this.MenuEditClear.Size = new System.Drawing.Size(109, 22);
        this.MenuEditClear.Text = "Clear";
        this.MenuEditClear.Click += new System.EventHandler(this.MenuEditClear_Click);
        // 
        // MenuEditFill
        // 
        this.MenuEditFill.Name = "MenuEditFill";
        this.MenuEditFill.Size = new System.Drawing.Size(109, 22);
        this.MenuEditFill.Text = "Fill";
        this.MenuEditFill.Click += new System.EventHandler(this.MenuEditFill_Click);
        // 
        // MenuExport
        // 
        this.MenuExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuExportEngine,
            this.MenuExportPng});
        this.MenuExport.Name = "MenuExport";
        this.MenuExport.Size = new System.Drawing.Size(61, 20);
        this.MenuExport.Text = "Export";
        // 
        // MenuExportEngine
        // 
        this.MenuExportEngine.Name = "MenuExportEngine";
        this.MenuExportEngine.Size = new System.Drawing.Size(130, 22);
        this.MenuExportEngine.Text = "Engine";
        this.MenuExportEngine.Click += new System.EventHandler(this.MenuExportEngine_Click);
        // 
        // MenuExportPng
        // 
        this.MenuExportPng.Name = "MenuExportPng";
        this.MenuExportPng.Size = new System.Drawing.Size(130, 22);
        this.MenuExportPng.Text = "Png File";
        this.MenuExportPng.Click += new System.EventHandler(this.MenuExportPng_Click);
        // 
        // MenuProperty
        // 
        this.MenuProperty.Name = "MenuProperty";
        this.MenuProperty.Size = new System.Drawing.Size(75, 20);
        this.MenuProperty.Text = "Property";
        this.MenuProperty.Click += new System.EventHandler(this.MenuProperty_Click);
        // 
        // MenuView
        // 
        this.MenuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuViewGround,
            this.MenuViewMask1,
            this.MenuViewMask2,
            this.MenuViewFringe1,
            this.MenuViewFringe2,
            this.MenuViewGrid});
        this.MenuView.Name = "MenuView";
        this.MenuView.Size = new System.Drawing.Size(47, 20);
        this.MenuView.Text = "View";
        // 
        // MenuViewGround
        // 
        this.MenuViewGround.Checked = true;
        this.MenuViewGround.CheckOnClick = true;
        this.MenuViewGround.CheckState = System.Windows.Forms.CheckState.Checked;
        this.MenuViewGround.Name = "MenuViewGround";
        this.MenuViewGround.Size = new System.Drawing.Size(130, 22);
        this.MenuViewGround.Text = "Ground";
        this.MenuViewGround.Click += new System.EventHandler(this.MenuViewItem_Click);
        // 
        // MenuViewMask1
        // 
        this.MenuViewMask1.Checked = true;
        this.MenuViewMask1.CheckOnClick = true;
        this.MenuViewMask1.CheckState = System.Windows.Forms.CheckState.Checked;
        this.MenuViewMask1.Name = "MenuViewMask1";
        this.MenuViewMask1.Size = new System.Drawing.Size(130, 22);
        this.MenuViewMask1.Text = "Mask 1";
        this.MenuViewMask1.Click += new System.EventHandler(this.MenuViewItem_Click);
        // 
        // MenuViewMask2
        // 
        this.MenuViewMask2.Checked = true;
        this.MenuViewMask2.CheckOnClick = true;
        this.MenuViewMask2.CheckState = System.Windows.Forms.CheckState.Checked;
        this.MenuViewMask2.Name = "MenuViewMask2";
        this.MenuViewMask2.Size = new System.Drawing.Size(130, 22);
        this.MenuViewMask2.Text = "Mask 2";
        this.MenuViewMask2.Click += new System.EventHandler(this.MenuViewItem_Click);
        // 
        // MenuViewFringe1
        // 
        this.MenuViewFringe1.Checked = true;
        this.MenuViewFringe1.CheckOnClick = true;
        this.MenuViewFringe1.CheckState = System.Windows.Forms.CheckState.Checked;
        this.MenuViewFringe1.Name = "MenuViewFringe1";
        this.MenuViewFringe1.Size = new System.Drawing.Size(130, 22);
        this.MenuViewFringe1.Text = "Fringe 1";
        this.MenuViewFringe1.Click += new System.EventHandler(this.MenuViewItem_Click);
        // 
        // MenuViewFringe2
        // 
        this.MenuViewFringe2.Checked = true;
        this.MenuViewFringe2.CheckOnClick = true;
        this.MenuViewFringe2.CheckState = System.Windows.Forms.CheckState.Checked;
        this.MenuViewFringe2.Name = "MenuViewFringe2";
        this.MenuViewFringe2.Size = new System.Drawing.Size(130, 22);
        this.MenuViewFringe2.Text = "Fringe 2";
        this.MenuViewFringe2.Click += new System.EventHandler(this.MenuViewItem_Click);
        // 
        // MenuViewGrid
        // 
        this.MenuViewGrid.Checked = true;
        this.MenuViewGrid.CheckOnClick = true;
        this.MenuViewGrid.CheckState = System.Windows.Forms.CheckState.Checked;
        this.MenuViewGrid.Name = "MenuViewGrid";
        this.MenuViewGrid.Size = new System.Drawing.Size(130, 22);
        this.MenuViewGrid.Text = "Grid";
        this.MenuViewGrid.Click += new System.EventHandler(this.MenuViewItem_Click);
        // 
        // GroupMap
        // 
        this.GroupMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.GroupMap.Controls.Add(this.GroupGridOpacity);
        this.GroupMap.Controls.Add(this.GroupAttributes);
        this.GroupMap.Controls.Add(this.GroupRegion);
        this.GroupMap.Controls.Add(this.LabelTileId);
        this.GroupMap.Controls.Add(this.ScrollTileX);
        this.GroupMap.Controls.Add(this.ScrollTileY);
        this.GroupMap.Controls.Add(this.ScrollTileId);
        this.GroupMap.Controls.Add(this.PictureTile);
        this.GroupMap.Controls.Add(this.ScrollMapY);
        this.GroupMap.Controls.Add(this.ScrollMapX);
        this.GroupMap.Controls.Add(this.LabelMapY);
        this.GroupMap.Controls.Add(this.LabelMapX);
        this.GroupMap.Controls.Add(this.PictureMap);
        this.GroupMap.Controls.Add(this.TabMaps);
        this.GroupMap.Font = new System.Drawing.Font("JetBrains Mono", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.GroupMap.Location = new System.Drawing.Point(12, 37);
        this.GroupMap.Name = "GroupMap";
        this.GroupMap.Size = new System.Drawing.Size(984, 600);
        this.GroupMap.TabIndex = 1;
        this.GroupMap.TabStop = false;
        this.GroupMap.Text = "Map Editor";
        // 
        // GroupGridOpacity
        // 
        this.GroupGridOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.GroupGridOpacity.Controls.Add(this.LabelGridOpacity);
        this.GroupGridOpacity.Controls.Add(this.ScrollGridOpacity);
        this.GroupGridOpacity.Location = new System.Drawing.Point(796, 492);
        this.GroupGridOpacity.Name = "GroupGridOpacity";
        this.GroupGridOpacity.Size = new System.Drawing.Size(175, 87);
        this.GroupGridOpacity.TabIndex = 13;
        this.GroupGridOpacity.TabStop = false;
        this.GroupGridOpacity.Text = "Grid Opacity";
        // 
        // LabelGridOpacity
        // 
        this.LabelGridOpacity.Location = new System.Drawing.Point(20, 31);
        this.LabelGridOpacity.Name = "LabelGridOpacity";
        this.LabelGridOpacity.Size = new System.Drawing.Size(133, 15);
        this.LabelGridOpacity.TabIndex = 11;
        this.LabelGridOpacity.Text = "Grid Map: 255";
        // 
        // ScrollGridOpacity
        // 
        this.ScrollGridOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.ScrollGridOpacity.LargeChange = 1;
        this.ScrollGridOpacity.Location = new System.Drawing.Point(20, 46);
        this.ScrollGridOpacity.Maximum = 255;
        this.ScrollGridOpacity.Name = "ScrollGridOpacity";
        this.ScrollGridOpacity.Size = new System.Drawing.Size(135, 17);
        this.ScrollGridOpacity.TabIndex = 10;
        this.ScrollGridOpacity.Value = 255;
        this.ScrollGridOpacity.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollGridOpacity_Scroll);
        // 
        // GroupAttributes
        // 
        this.GroupAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.GroupAttributes.Controls.Add(this.ButtonForeColor4);
        this.GroupAttributes.Controls.Add(this.ButtonForeColor3);
        this.GroupAttributes.Controls.Add(this.ButtonForeColor2);
        this.GroupAttributes.Controls.Add(this.ButtonForeColor1);
        this.GroupAttributes.Controls.Add(this.ButtonForeColor0);
        this.GroupAttributes.Controls.Add(this.ButtonClearAttribute);
        this.GroupAttributes.Controls.Add(this.RadioWarp);
        this.GroupAttributes.Controls.Add(this.RadioChat);
        this.GroupAttributes.Controls.Add(this.RadioTrap);
        this.GroupAttributes.Controls.Add(this.RadioAvoid);
        this.GroupAttributes.Controls.Add(this.RadioBlock);
        this.GroupAttributes.Enabled = false;
        this.GroupAttributes.Location = new System.Drawing.Point(796, 277);
        this.GroupAttributes.Name = "GroupAttributes";
        this.GroupAttributes.Size = new System.Drawing.Size(175, 209);
        this.GroupAttributes.TabIndex = 12;
        this.GroupAttributes.TabStop = false;
        this.GroupAttributes.Text = "Area - Layer";
        // 
        // ButtonForeColor4
        // 
        this.ButtonForeColor4.Location = new System.Drawing.Point(120, 128);
        this.ButtonForeColor4.Name = "ButtonForeColor4";
        this.ButtonForeColor4.Size = new System.Drawing.Size(44, 23);
        this.ButtonForeColor4.TabIndex = 12;
        this.ButtonForeColor4.Text = "F";
        this.ButtonForeColor4.UseVisualStyleBackColor = true;
        this.ButtonForeColor4.Click += new System.EventHandler(this.ButtonForeColor_Click);
        // 
        // ButtonForeColor3
        // 
        this.ButtonForeColor3.Location = new System.Drawing.Point(120, 105);
        this.ButtonForeColor3.Name = "ButtonForeColor3";
        this.ButtonForeColor3.Size = new System.Drawing.Size(44, 23);
        this.ButtonForeColor3.TabIndex = 11;
        this.ButtonForeColor3.Text = "F";
        this.ButtonForeColor3.UseVisualStyleBackColor = true;
        this.ButtonForeColor3.Click += new System.EventHandler(this.ButtonForeColor_Click);
        // 
        // ButtonForeColor2
        // 
        this.ButtonForeColor2.Location = new System.Drawing.Point(120, 80);
        this.ButtonForeColor2.Name = "ButtonForeColor2";
        this.ButtonForeColor2.Size = new System.Drawing.Size(44, 23);
        this.ButtonForeColor2.TabIndex = 10;
        this.ButtonForeColor2.Text = "F";
        this.ButtonForeColor2.UseVisualStyleBackColor = true;
        this.ButtonForeColor2.Click += new System.EventHandler(this.ButtonForeColor_Click);
        // 
        // ButtonForeColor1
        // 
        this.ButtonForeColor1.Location = new System.Drawing.Point(120, 55);
        this.ButtonForeColor1.Name = "ButtonForeColor1";
        this.ButtonForeColor1.Size = new System.Drawing.Size(44, 23);
        this.ButtonForeColor1.TabIndex = 9;
        this.ButtonForeColor1.Text = "F";
        this.ButtonForeColor1.UseVisualStyleBackColor = true;
        this.ButtonForeColor1.Click += new System.EventHandler(this.ButtonForeColor_Click);
        // 
        // ButtonForeColor0
        // 
        this.ButtonForeColor0.Location = new System.Drawing.Point(120, 30);
        this.ButtonForeColor0.Name = "ButtonForeColor0";
        this.ButtonForeColor0.Size = new System.Drawing.Size(44, 23);
        this.ButtonForeColor0.TabIndex = 8;
        this.ButtonForeColor0.Text = "F";
        this.ButtonForeColor0.UseVisualStyleBackColor = true;
        this.ButtonForeColor0.Click += new System.EventHandler(this.ButtonForeColor_Click);
        // 
        // ButtonClearAttribute
        // 
        this.ButtonClearAttribute.Location = new System.Drawing.Point(20, 167);
        this.ButtonClearAttribute.Name = "ButtonClearAttribute";
        this.ButtonClearAttribute.Size = new System.Drawing.Size(133, 23);
        this.ButtonClearAttribute.TabIndex = 7;
        this.ButtonClearAttribute.Text = "Clear Attribute";
        this.ButtonClearAttribute.UseVisualStyleBackColor = true;
        this.ButtonClearAttribute.Click += new System.EventHandler(this.ButtonClearAttribute_Click);
        // 
        // RadioWarp
        // 
        this.RadioWarp.AutoSize = true;
        this.RadioWarp.Location = new System.Drawing.Point(28, 132);
        this.RadioWarp.Name = "RadioWarp";
        this.RadioWarp.Size = new System.Drawing.Size(53, 19);
        this.RadioWarp.TabIndex = 4;
        this.RadioWarp.Text = "Warp";
        this.RadioWarp.UseVisualStyleBackColor = true;
        // 
        // RadioChat
        // 
        this.RadioChat.AutoSize = true;
        this.RadioChat.Location = new System.Drawing.Point(28, 107);
        this.RadioChat.Name = "RadioChat";
        this.RadioChat.Size = new System.Drawing.Size(53, 19);
        this.RadioChat.TabIndex = 3;
        this.RadioChat.Text = "Chat";
        this.RadioChat.UseVisualStyleBackColor = true;
        // 
        // RadioTrap
        // 
        this.RadioTrap.AutoSize = true;
        this.RadioTrap.Location = new System.Drawing.Point(28, 82);
        this.RadioTrap.Name = "RadioTrap";
        this.RadioTrap.Size = new System.Drawing.Size(53, 19);
        this.RadioTrap.TabIndex = 2;
        this.RadioTrap.Text = "Trap";
        this.RadioTrap.UseVisualStyleBackColor = true;
        // 
        // RadioAvoid
        // 
        this.RadioAvoid.AutoSize = true;
        this.RadioAvoid.Location = new System.Drawing.Point(28, 57);
        this.RadioAvoid.Name = "RadioAvoid";
        this.RadioAvoid.Size = new System.Drawing.Size(88, 19);
        this.RadioAvoid.TabIndex = 1;
        this.RadioAvoid.Text = "Npc Avoid";
        this.RadioAvoid.UseVisualStyleBackColor = true;
        // 
        // RadioBlock
        // 
        this.RadioBlock.AutoSize = true;
        this.RadioBlock.Checked = true;
        this.RadioBlock.Location = new System.Drawing.Point(28, 32);
        this.RadioBlock.Name = "RadioBlock";
        this.RadioBlock.Size = new System.Drawing.Size(60, 19);
        this.RadioBlock.TabIndex = 0;
        this.RadioBlock.TabStop = true;
        this.RadioBlock.Text = "Block";
        this.RadioBlock.UseVisualStyleBackColor = true;
        // 
        // GroupRegion
        // 
        this.GroupRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.GroupRegion.Controls.Add(this.ButtonClearDirection);
        this.GroupRegion.Controls.Add(this.RadioDirection);
        this.GroupRegion.Controls.Add(this.RadioAttributes);
        this.GroupRegion.Controls.Add(this.RadioFringe2);
        this.GroupRegion.Controls.Add(this.RadioFringe1);
        this.GroupRegion.Controls.Add(this.RadioMask2);
        this.GroupRegion.Controls.Add(this.RadioMask1);
        this.GroupRegion.Controls.Add(this.RadioGround);
        this.GroupRegion.Location = new System.Drawing.Point(796, 21);
        this.GroupRegion.Name = "GroupRegion";
        this.GroupRegion.Size = new System.Drawing.Size(175, 250);
        this.GroupRegion.TabIndex = 11;
        this.GroupRegion.TabStop = false;
        this.GroupRegion.Text = "Area - Layer";
        // 
        // ButtonClearDirection
        // 
        this.ButtonClearDirection.Enabled = false;
        this.ButtonClearDirection.Location = new System.Drawing.Point(20, 211);
        this.ButtonClearDirection.Name = "ButtonClearDirection";
        this.ButtonClearDirection.Size = new System.Drawing.Size(133, 23);
        this.ButtonClearDirection.TabIndex = 7;
        this.ButtonClearDirection.Text = "Clear Direction";
        this.ButtonClearDirection.UseVisualStyleBackColor = true;
        this.ButtonClearDirection.Click += new System.EventHandler(this.ButtonClearDirection_Click);
        // 
        // RadioDirection
        // 
        this.RadioDirection.AutoSize = true;
        this.RadioDirection.Location = new System.Drawing.Point(28, 186);
        this.RadioDirection.Name = "RadioDirection";
        this.RadioDirection.Size = new System.Drawing.Size(88, 19);
        this.RadioDirection.TabIndex = 6;
        this.RadioDirection.Text = "Direction";
        this.RadioDirection.UseVisualStyleBackColor = true;
        this.RadioDirection.CheckedChanged += new System.EventHandler(this.RadioLayer_CheckedChanged);
        // 
        // RadioAttributes
        // 
        this.RadioAttributes.AutoSize = true;
        this.RadioAttributes.Location = new System.Drawing.Point(28, 161);
        this.RadioAttributes.Name = "RadioAttributes";
        this.RadioAttributes.Size = new System.Drawing.Size(88, 19);
        this.RadioAttributes.TabIndex = 5;
        this.RadioAttributes.Text = "Attribute";
        this.RadioAttributes.UseVisualStyleBackColor = true;
        this.RadioAttributes.CheckedChanged += new System.EventHandler(this.RadioLayer_CheckedChanged);
        // 
        // RadioFringe2
        // 
        this.RadioFringe2.AutoSize = true;
        this.RadioFringe2.Location = new System.Drawing.Point(28, 136);
        this.RadioFringe2.Name = "RadioFringe2";
        this.RadioFringe2.Size = new System.Drawing.Size(81, 19);
        this.RadioFringe2.TabIndex = 4;
        this.RadioFringe2.Text = "Fringe 2";
        this.RadioFringe2.UseVisualStyleBackColor = true;
        this.RadioFringe2.CheckedChanged += new System.EventHandler(this.RadioLayer_CheckedChanged);
        // 
        // RadioFringe1
        // 
        this.RadioFringe1.AutoSize = true;
        this.RadioFringe1.Location = new System.Drawing.Point(28, 111);
        this.RadioFringe1.Name = "RadioFringe1";
        this.RadioFringe1.Size = new System.Drawing.Size(81, 19);
        this.RadioFringe1.TabIndex = 3;
        this.RadioFringe1.Text = "Fringe 1";
        this.RadioFringe1.UseVisualStyleBackColor = true;
        this.RadioFringe1.CheckedChanged += new System.EventHandler(this.RadioLayer_CheckedChanged);
        // 
        // RadioMask2
        // 
        this.RadioMask2.AutoSize = true;
        this.RadioMask2.Location = new System.Drawing.Point(28, 86);
        this.RadioMask2.Name = "RadioMask2";
        this.RadioMask2.Size = new System.Drawing.Size(67, 19);
        this.RadioMask2.TabIndex = 2;
        this.RadioMask2.Text = "Mask 2";
        this.RadioMask2.UseVisualStyleBackColor = true;
        this.RadioMask2.CheckedChanged += new System.EventHandler(this.RadioLayer_CheckedChanged);
        // 
        // RadioMask1
        // 
        this.RadioMask1.AutoSize = true;
        this.RadioMask1.Location = new System.Drawing.Point(28, 61);
        this.RadioMask1.Name = "RadioMask1";
        this.RadioMask1.Size = new System.Drawing.Size(67, 19);
        this.RadioMask1.TabIndex = 1;
        this.RadioMask1.Text = "Mask 1";
        this.RadioMask1.UseVisualStyleBackColor = true;
        this.RadioMask1.CheckedChanged += new System.EventHandler(this.RadioLayer_CheckedChanged);
        // 
        // RadioGround
        // 
        this.RadioGround.AutoSize = true;
        this.RadioGround.Checked = true;
        this.RadioGround.Location = new System.Drawing.Point(28, 36);
        this.RadioGround.Name = "RadioGround";
        this.RadioGround.Size = new System.Drawing.Size(67, 19);
        this.RadioGround.TabIndex = 0;
        this.RadioGround.TabStop = true;
        this.RadioGround.Text = "Ground";
        this.RadioGround.UseVisualStyleBackColor = true;
        this.RadioGround.CheckedChanged += new System.EventHandler(this.RadioLayer_CheckedChanged);
        // 
        // LabelTileId
        // 
        this.LabelTileId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.LabelTileId.Location = new System.Drawing.Point(503, 70);
        this.LabelTileId.Name = "LabelTileId";
        this.LabelTileId.Size = new System.Drawing.Size(256, 15);
        this.LabelTileId.TabIndex = 10;
        this.LabelTileId.Text = "Tile Id: 0";
        this.LabelTileId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // ScrollTileX
        // 
        this.ScrollTileX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.ScrollTileX.LargeChange = 1;
        this.ScrollTileX.Location = new System.Drawing.Point(503, 562);
        this.ScrollTileX.Maximum = 0;
        this.ScrollTileX.Name = "ScrollTileX";
        this.ScrollTileX.Size = new System.Drawing.Size(256, 17);
        this.ScrollTileX.TabIndex = 9;
        this.ScrollTileX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollTileX_Scroll);
        // 
        // ScrollTileY
        // 
        this.ScrollTileY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.ScrollTileY.Location = new System.Drawing.Point(771, 107);
        this.ScrollTileY.Name = "ScrollTileY";
        this.ScrollTileY.Size = new System.Drawing.Size(17, 442);
        this.ScrollTileY.TabIndex = 8;
        this.ScrollTileY.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollTileY_Scroll);
        // 
        // ScrollTileId
        // 
        this.ScrollTileId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.ScrollTileId.LargeChange = 1;
        this.ScrollTileId.Location = new System.Drawing.Point(503, 87);
        this.ScrollTileId.Maximum = 0;
        this.ScrollTileId.Name = "ScrollTileId";
        this.ScrollTileId.Size = new System.Drawing.Size(256, 17);
        this.ScrollTileId.TabIndex = 7;
        this.ScrollTileId.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollTileId_Scroll);
        // 
        // PictureTile
        // 
        this.PictureTile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.PictureTile.BackColor = System.Drawing.Color.Black;
        this.PictureTile.Location = new System.Drawing.Point(503, 107);
        this.PictureTile.Name = "PictureTile";
        this.PictureTile.Size = new System.Drawing.Size(256, 448);
        this.PictureTile.TabIndex = 6;
        this.PictureTile.TabStop = false;
        this.PictureTile.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureTile_Paint);
        this.PictureTile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureTile_MouseDown);
        this.PictureTile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureTile_MouseMove);
        this.PictureTile.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureTile_MouseUp);
        // 
        // ScrollMapY
        // 
        this.ScrollMapY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.ScrollMapY.LargeChange = 1;
        this.ScrollMapY.Location = new System.Drawing.Point(147, 40);
        this.ScrollMapY.Maximum = 0;
        this.ScrollMapY.Name = "ScrollMapY";
        this.ScrollMapY.Size = new System.Drawing.Size(633, 17);
        this.ScrollMapY.TabIndex = 5;
        this.ScrollMapY.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollMapY_Scroll);
        // 
        // ScrollMapX
        // 
        this.ScrollMapX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.ScrollMapX.LargeChange = 1;
        this.ScrollMapX.Location = new System.Drawing.Point(147, 21);
        this.ScrollMapX.Maximum = 0;
        this.ScrollMapX.Name = "ScrollMapX";
        this.ScrollMapX.Size = new System.Drawing.Size(633, 17);
        this.ScrollMapX.TabIndex = 4;
        this.ScrollMapX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollMapX_Scroll);
        // 
        // LabelMapY
        // 
        this.LabelMapY.AutoSize = true;
        this.LabelMapY.Location = new System.Drawing.Point(16, 41);
        this.LabelMapY.Name = "LabelMapY";
        this.LabelMapY.Size = new System.Drawing.Size(84, 15);
        this.LabelMapY.TabIndex = 3;
        this.LabelMapY.Text = "Scroll Y: 0";
        // 
        // LabelMapX
        // 
        this.LabelMapX.AutoSize = true;
        this.LabelMapX.Location = new System.Drawing.Point(16, 22);
        this.LabelMapX.Name = "LabelMapX";
        this.LabelMapX.Size = new System.Drawing.Size(84, 15);
        this.LabelMapX.TabIndex = 2;
        this.LabelMapX.Text = "Scroll X: 0";
        // 
        // PictureMap
        // 
        this.PictureMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.PictureMap.BackColor = System.Drawing.SystemColors.ControlDark;
        this.PictureMap.Location = new System.Drawing.Point(10, 99);
        this.PictureMap.Name = "PictureMap";
        this.PictureMap.Size = new System.Drawing.Size(480, 480);
        this.PictureMap.TabIndex = 1;
        this.PictureMap.TabStop = false;
        this.PictureMap.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureMap_Paint);
        this.PictureMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureMap_MouseDown);
        this.PictureMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureMap_MouseMove);
        this.PictureMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureMap_MouseUp);
        // 
        // TabMaps
        // 
        this.TabMaps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.TabMaps.HotTrack = true;
        this.TabMaps.Location = new System.Drawing.Point(10, 72);
        this.TabMaps.Name = "TabMaps";
        this.TabMaps.SelectedIndex = 0;
        this.TabMaps.Size = new System.Drawing.Size(480, 26);
        this.TabMaps.TabIndex = 0;
        this.TabMaps.SelectedIndexChanged += new System.EventHandler(this.TabMaps_SelectedIndexChanged);
        // 
        // FormMain
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1008, 651);
        this.Controls.Add(this.GroupMap);
        this.Controls.Add(this.Menu);
        this.DoubleBuffered = true;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.KeyPreview = true;
        this.MainMenuStrip = this.Menu;
        this.Name = "FormMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Dragon Engine - Map Editor";
        this.Load += new System.EventHandler(this.FormMain_Load);
        this.Resize += new System.EventHandler(this.FormMain_Resize);
        this.Menu.ResumeLayout(false);
        this.Menu.PerformLayout();
        this.GroupMap.ResumeLayout(false);
        this.GroupMap.PerformLayout();
        this.GroupGridOpacity.ResumeLayout(false);
        this.GroupAttributes.ResumeLayout(false);
        this.GroupAttributes.PerformLayout();
        this.GroupRegion.ResumeLayout(false);
        this.GroupRegion.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.PictureTile)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.PictureMap)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private MenuStrip Menu;
    private ToolStripMenuItem MenuFile;
    private ToolStripMenuItem MenuFileNew;
    private ToolStripMenuItem MenuFileOpen;
    private ToolStripMenuItem MenuFileOpenAs;
    private ToolStripMenuItem MenuFileSave;
    private ToolStripMenuItem MenuFileSaveAs;
    private ToolStripMenuItem MenuFileSaveAll;
    private ToolStripSeparator toolStripMenuItem2;
    private ToolStripMenuItem MenuFileClose;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem MenuFileExit;
    private ToolStripMenuItem MenuEdit;
    private ToolStripMenuItem MenuEditClear;
    private ToolStripMenuItem MenuEditFill;
    private ToolStripMenuItem MenuExport;
    private ToolStripMenuItem MenuExportEngine;
    private ToolStripMenuItem MenuExportPng;
    private ToolStripMenuItem MenuProperty;
    private ToolStripMenuItem MenuView;
    private ToolStripMenuItem MenuViewGround;
    private ToolStripMenuItem MenuViewMask1;
    private ToolStripMenuItem MenuViewMask2;
    private ToolStripMenuItem MenuViewFringe1;
    private ToolStripMenuItem MenuViewFringe2;
    private ToolStripMenuItem MenuViewGrid;
    private GroupBox GroupMap;
    private TabControl TabMaps;
    private PictureBox PictureMap;
    private Label LabelMapY;
    private Label LabelMapX;
    private HScrollBar ScrollMapY;
    private HScrollBar ScrollMapX;
    private PictureBox PictureTile;
    private VScrollBar ScrollTileY;
    private HScrollBar ScrollTileId;
    private HScrollBar ScrollTileX;
    private Label LabelTileId;
    private GroupBox GroupRegion;
    private RadioButton RadioDirection;
    private RadioButton RadioAttributes;
    private RadioButton RadioFringe2;
    private RadioButton RadioFringe1;
    private RadioButton RadioMask2;
    private RadioButton RadioMask1;
    private RadioButton RadioGround;
    private Button ButtonClearDirection;
    private GroupBox GroupAttributes;
    private Button ButtonClearAttribute;
    private RadioButton RadioWarp;
    private RadioButton RadioChat;
    private RadioButton RadioTrap;
    private RadioButton RadioAvoid;
    private RadioButton RadioBlock;
    private Button ButtonForeColor4;
    private Button ButtonForeColor3;
    private Button ButtonForeColor2;
    private Button ButtonForeColor1;
    private Button ButtonForeColor0;
    private GroupBox GroupGridOpacity;
    private HScrollBar ScrollGridOpacity;
    private Label LabelGridOpacity;
}
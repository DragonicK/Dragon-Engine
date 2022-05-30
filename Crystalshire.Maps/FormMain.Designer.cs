namespace Crystalshire.Maps {
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
            this.PictureMap = new System.Windows.Forms.PictureBox();
            this.TabMaps = new System.Windows.Forms.TabControl();
            this.Menu.SuspendLayout();
            this.GroupMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureMap)).BeginInit();
            this.SuspendLayout();
            // 
            // Menu
            // 
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
            this.MenuFile.Size = new System.Drawing.Size(37, 20);
            this.MenuFile.Text = "File";
            // 
            // MenuFileNew
            // 
            this.MenuFileNew.Name = "MenuFileNew";
            this.MenuFileNew.Size = new System.Drawing.Size(142, 22);
            this.MenuFileNew.Text = "New";
            this.MenuFileNew.Click += new System.EventHandler(this.MenuFileNew_Click);
            // 
            // MenuFileOpen
            // 
            this.MenuFileOpen.Name = "MenuFileOpen";
            this.MenuFileOpen.Size = new System.Drawing.Size(142, 22);
            this.MenuFileOpen.Text = "Open";
            this.MenuFileOpen.Click += new System.EventHandler(this.MenuFileOpen_Click);
            // 
            // MenuFileOpenAs
            // 
            this.MenuFileOpenAs.Name = "MenuFileOpenAs";
            this.MenuFileOpenAs.Size = new System.Drawing.Size(142, 22);
            this.MenuFileOpenAs.Text = "Open as new";
            this.MenuFileOpenAs.Click += new System.EventHandler(this.MenuFileOpenAs_Click);
            // 
            // MenuFileSave
            // 
            this.MenuFileSave.Name = "MenuFileSave";
            this.MenuFileSave.Size = new System.Drawing.Size(142, 22);
            this.MenuFileSave.Text = "Save";
            this.MenuFileSave.Click += new System.EventHandler(this.MenuFileSave_Click);
            // 
            // MenuFileSaveAs
            // 
            this.MenuFileSaveAs.Name = "MenuFileSaveAs";
            this.MenuFileSaveAs.Size = new System.Drawing.Size(142, 22);
            this.MenuFileSaveAs.Text = "Save as copy";
            this.MenuFileSaveAs.Click += new System.EventHandler(this.MenuFileSaveAs_Click);
            // 
            // MenuFileSaveAll
            // 
            this.MenuFileSaveAll.Name = "MenuFileSaveAll";
            this.MenuFileSaveAll.Size = new System.Drawing.Size(142, 22);
            this.MenuFileSaveAll.Text = "Save All";
            this.MenuFileSaveAll.Click += new System.EventHandler(this.MenuFileSaveAll_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(139, 6);
            // 
            // MenuFileClose
            // 
            this.MenuFileClose.Name = "MenuFileClose";
            this.MenuFileClose.Size = new System.Drawing.Size(142, 22);
            this.MenuFileClose.Text = "Close";
            this.MenuFileClose.Click += new System.EventHandler(this.MenuFileClose_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(139, 6);
            // 
            // MenuFileExit
            // 
            this.MenuFileExit.Name = "MenuFileExit";
            this.MenuFileExit.Size = new System.Drawing.Size(142, 22);
            this.MenuFileExit.Text = "Exit";
            this.MenuFileExit.Click += new System.EventHandler(this.MenuFileExit_Click);
            // 
            // MenuEdit
            // 
            this.MenuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuEditClear,
            this.MenuEditFill});
            this.MenuEdit.Name = "MenuEdit";
            this.MenuEdit.Size = new System.Drawing.Size(39, 20);
            this.MenuEdit.Text = "Edit";
            // 
            // MenuEditClear
            // 
            this.MenuEditClear.Name = "MenuEditClear";
            this.MenuEditClear.Size = new System.Drawing.Size(100, 22);
            this.MenuEditClear.Text = "Clear";
            this.MenuEditClear.Click += new System.EventHandler(this.MenuEditClear_Click);
            // 
            // MenuEditFill
            // 
            this.MenuEditFill.Name = "MenuEditFill";
            this.MenuEditFill.Size = new System.Drawing.Size(100, 22);
            this.MenuEditFill.Text = "Fill";
            this.MenuEditFill.Click += new System.EventHandler(this.MenuEditFill_Click);
            // 
            // MenuExport
            // 
            this.MenuExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuExportEngine,
            this.MenuExportPng});
            this.MenuExport.Name = "MenuExport";
            this.MenuExport.Size = new System.Drawing.Size(53, 20);
            this.MenuExport.Text = "Export";
            // 
            // MenuExportEngine
            // 
            this.MenuExportEngine.Name = "MenuExportEngine";
            this.MenuExportEngine.Size = new System.Drawing.Size(110, 22);
            this.MenuExportEngine.Text = "Engine";
            this.MenuExportEngine.Click += new System.EventHandler(this.MenuExportEngine_Click);
            // 
            // MenuExportPng
            // 
            this.MenuExportPng.Name = "MenuExportPng";
            this.MenuExportPng.Size = new System.Drawing.Size(110, 22);
            this.MenuExportPng.Text = "Png";
            this.MenuExportPng.Click += new System.EventHandler(this.MenuExportPng_Click);
            // 
            // MenuProperty
            // 
            this.MenuProperty.Name = "MenuProperty";
            this.MenuProperty.Size = new System.Drawing.Size(64, 20);
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
            this.MenuView.Size = new System.Drawing.Size(44, 20);
            this.MenuView.Text = "View";
            // 
            // MenuViewGround
            // 
            this.MenuViewGround.Name = "MenuViewGround";
            this.MenuViewGround.Size = new System.Drawing.Size(115, 22);
            this.MenuViewGround.Text = "Ground";
            this.MenuViewGround.Click += new System.EventHandler(this.MenuViewGround_Click);
            // 
            // MenuViewMask1
            // 
            this.MenuViewMask1.Name = "MenuViewMask1";
            this.MenuViewMask1.Size = new System.Drawing.Size(115, 22);
            this.MenuViewMask1.Text = "Mask 1";
            this.MenuViewMask1.Click += new System.EventHandler(this.MenuViewMask1_Click);
            // 
            // MenuViewMask2
            // 
            this.MenuViewMask2.Name = "MenuViewMask2";
            this.MenuViewMask2.Size = new System.Drawing.Size(115, 22);
            this.MenuViewMask2.Text = "Mask 2";
            this.MenuViewMask2.Click += new System.EventHandler(this.MenuViewMask2_Click);
            // 
            // MenuViewFringe1
            // 
            this.MenuViewFringe1.Name = "MenuViewFringe1";
            this.MenuViewFringe1.Size = new System.Drawing.Size(115, 22);
            this.MenuViewFringe1.Text = "Fringe 1";
            this.MenuViewFringe1.Click += new System.EventHandler(this.MenuViewFringe1_Click);
            // 
            // MenuViewFringe2
            // 
            this.MenuViewFringe2.Name = "MenuViewFringe2";
            this.MenuViewFringe2.Size = new System.Drawing.Size(115, 22);
            this.MenuViewFringe2.Text = "Fringe 2";
            this.MenuViewFringe2.Click += new System.EventHandler(this.MenuViewFringe2_Click);
            // 
            // MenuViewGrid
            // 
            this.MenuViewGrid.Name = "MenuViewGrid";
            this.MenuViewGrid.Size = new System.Drawing.Size(115, 22);
            this.MenuViewGrid.Text = "Grid";
            this.MenuViewGrid.Click += new System.EventHandler(this.MenuViewGrid_Click);
            // 
            // GroupMap
            // 
            this.GroupMap.Controls.Add(this.PictureMap);
            this.GroupMap.Controls.Add(this.TabMaps);
            this.GroupMap.Location = new System.Drawing.Point(12, 37);
            this.GroupMap.Name = "GroupMap";
            this.GroupMap.Size = new System.Drawing.Size(984, 600);
            this.GroupMap.TabIndex = 1;
            this.GroupMap.TabStop = false;
            this.GroupMap.Text = "Map Editor";
            // 
            // PictureMap
            // 
            this.PictureMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureMap.Location = new System.Drawing.Point(10, 99);
            this.PictureMap.Name = "PictureMap";
            this.PictureMap.Size = new System.Drawing.Size(480, 480);
            this.PictureMap.TabIndex = 1;
            this.PictureMap.TabStop = false;
            // 
            // TabMaps
            // 
            this.TabMaps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabMaps.HotTrack = true;
            this.TabMaps.Location = new System.Drawing.Point(10, 72);
            this.TabMaps.Name = "TabMaps";
            this.TabMaps.SelectedIndex = 0;
            this.TabMaps.Size = new System.Drawing.Size(480, 26);
            this.TabMaps.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 651);
            this.Controls.Add(this.GroupMap);
            this.Controls.Add(this.Menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.Menu;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dragon Engine - Map Editor";
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.GroupMap.ResumeLayout(false);
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
    }
}
namespace Crystalshire.Model {
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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSaveNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAnimation = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAttack = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDeath = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEmote = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuGathering = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuIdle = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuRunning = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuRessurrection = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSpecial = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTalk = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuWalking = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TextPassphrase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextModelId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextProjectName = new System.Windows.Forms.TextBox();
            this.MenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuAnimation,
            this.MenuExport});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(384, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuNew,
            this.MenuClose,
            this.toolStripMenuItem2,
            this.MenuOpen,
            this.MenuSave,
            this.MenuSaveNew,
            this.toolStripMenuItem1,
            this.MenuExit});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(47, 20);
            this.MenuFile.Text = "File";
            // 
            // MenuNew
            // 
            this.MenuNew.Name = "MenuNew";
            this.MenuNew.Size = new System.Drawing.Size(151, 22);
            this.MenuNew.Text = "New";
            this.MenuNew.Click += new System.EventHandler(this.MenuNew_Click);
            // 
            // MenuClose
            // 
            this.MenuClose.Enabled = false;
            this.MenuClose.Name = "MenuClose";
            this.MenuClose.Size = new System.Drawing.Size(151, 22);
            this.MenuClose.Text = "Close";
            this.MenuClose.Click += new System.EventHandler(this.MenuClose_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(148, 6);
            // 
            // MenuOpen
            // 
            this.MenuOpen.Name = "MenuOpen";
            this.MenuOpen.Size = new System.Drawing.Size(151, 22);
            this.MenuOpen.Text = "Open";
            this.MenuOpen.Click += new System.EventHandler(this.MenuOpen_Click);
            // 
            // MenuSave
            // 
            this.MenuSave.Enabled = false;
            this.MenuSave.Name = "MenuSave";
            this.MenuSave.Size = new System.Drawing.Size(151, 22);
            this.MenuSave.Text = "Save";
            this.MenuSave.Click += new System.EventHandler(this.MenuSave_Click);
            // 
            // MenuSaveNew
            // 
            this.MenuSaveNew.Enabled = false;
            this.MenuSaveNew.Name = "MenuSaveNew";
            this.MenuSaveNew.Size = new System.Drawing.Size(151, 22);
            this.MenuSaveNew.Text = "Save As New";
            this.MenuSaveNew.Click += new System.EventHandler(this.MenuSaveNew_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 6);
            // 
            // MenuExit
            // 
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(151, 22);
            this.MenuExit.Text = "Exit";
            this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // MenuAnimation
            // 
            this.MenuAnimation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAttack,
            this.MenuDeath,
            this.MenuEmote,
            this.MenuGathering,
            this.MenuIdle,
            this.MenuRunning,
            this.MenuRessurrection,
            this.MenuSpecial,
            this.MenuTalk,
            this.MenuWalking});
            this.MenuAnimation.Enabled = false;
            this.MenuAnimation.Name = "MenuAnimation";
            this.MenuAnimation.Size = new System.Drawing.Size(82, 20);
            this.MenuAnimation.Text = "Animation";
            // 
            // MenuAttack
            // 
            this.MenuAttack.Name = "MenuAttack";
            this.MenuAttack.Size = new System.Drawing.Size(165, 22);
            this.MenuAttack.Text = "Attack";
            this.MenuAttack.Click += new System.EventHandler(this.MenuAttack_Click);
            // 
            // MenuDeath
            // 
            this.MenuDeath.Name = "MenuDeath";
            this.MenuDeath.Size = new System.Drawing.Size(165, 22);
            this.MenuDeath.Text = "Death";
            this.MenuDeath.Click += new System.EventHandler(this.MenuDeath_Click);
            // 
            // MenuEmote
            // 
            this.MenuEmote.Enabled = false;
            this.MenuEmote.Name = "MenuEmote";
            this.MenuEmote.Size = new System.Drawing.Size(165, 22);
            this.MenuEmote.Text = "Emote";
            this.MenuEmote.Click += new System.EventHandler(this.MenuEmote_Click);
            // 
            // MenuGathering
            // 
            this.MenuGathering.Name = "MenuGathering";
            this.MenuGathering.Size = new System.Drawing.Size(165, 22);
            this.MenuGathering.Text = "Gathering";
            this.MenuGathering.Click += new System.EventHandler(this.MenuGathering_Click);
            // 
            // MenuIdle
            // 
            this.MenuIdle.Name = "MenuIdle";
            this.MenuIdle.Size = new System.Drawing.Size(165, 22);
            this.MenuIdle.Text = "Idle";
            this.MenuIdle.Click += new System.EventHandler(this.MenuIdle_Click);
            // 
            // MenuRunning
            // 
            this.MenuRunning.Name = "MenuRunning";
            this.MenuRunning.Size = new System.Drawing.Size(165, 22);
            this.MenuRunning.Text = "Running";
            this.MenuRunning.Click += new System.EventHandler(this.MenuRunning_Click);
            // 
            // MenuRessurrection
            // 
            this.MenuRessurrection.Name = "MenuRessurrection";
            this.MenuRessurrection.Size = new System.Drawing.Size(165, 22);
            this.MenuRessurrection.Text = "Ressurrection";
            this.MenuRessurrection.Click += new System.EventHandler(this.MenuRessurrection_Click);
            // 
            // MenuSpecial
            // 
            this.MenuSpecial.Enabled = false;
            this.MenuSpecial.Name = "MenuSpecial";
            this.MenuSpecial.Size = new System.Drawing.Size(165, 22);
            this.MenuSpecial.Text = "Special";
            this.MenuSpecial.Click += new System.EventHandler(this.MenuSpecial_Click);
            // 
            // MenuTalk
            // 
            this.MenuTalk.Name = "MenuTalk";
            this.MenuTalk.Size = new System.Drawing.Size(165, 22);
            this.MenuTalk.Text = "Talk";
            this.MenuTalk.Click += new System.EventHandler(this.MenuTalk_Click);
            // 
            // MenuWalking
            // 
            this.MenuWalking.Name = "MenuWalking";
            this.MenuWalking.Size = new System.Drawing.Size(165, 22);
            this.MenuWalking.Text = "Walking";
            this.MenuWalking.Click += new System.EventHandler(this.MenuWalking_Click);
            // 
            // MenuExport
            // 
            this.MenuExport.Enabled = false;
            this.MenuExport.Name = "MenuExport";
            this.MenuExport.Size = new System.Drawing.Size(61, 20);
            this.MenuExport.Text = "Export";
            this.MenuExport.Click += new System.EventHandler(this.MenuExport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TextPassphrase);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TextModelId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TextProjectName);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 322);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Model Data";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Export Passphrase:";
            // 
            // TextPassphrase
            // 
            this.TextPassphrase.Enabled = false;
            this.TextPassphrase.Location = new System.Drawing.Point(17, 134);
            this.TextPassphrase.Name = "TextPassphrase";
            this.TextPassphrase.Size = new System.Drawing.Size(325, 23);
            this.TextPassphrase.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Model Id:";
            // 
            // TextModelId
            // 
            this.TextModelId.Enabled = false;
            this.TextModelId.Location = new System.Drawing.Point(17, 45);
            this.TextModelId.Name = "TextModelId";
            this.TextModelId.Size = new System.Drawing.Size(325, 23);
            this.TextModelId.TabIndex = 2;
            this.TextModelId.TextChanged += new System.EventHandler(this.TextModelId_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Project Name:";
            // 
            // TextProjectName
            // 
            this.TextProjectName.Enabled = false;
            this.TextProjectName.Location = new System.Drawing.Point(17, 90);
            this.TextProjectName.Name = "TextProjectName";
            this.TextProjectName.Size = new System.Drawing.Size(325, 23);
            this.TextProjectName.TabIndex = 0;
            this.TextProjectName.TextChanged += new System.EventHandler(this.TextProjectName_TextChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Model Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip MenuStrip;
        private ToolStripMenuItem MenuFile;
        private ToolStripMenuItem MenuOpen;
        private ToolStripMenuItem MenuSave;
        private ToolStripMenuItem MenuSaveNew;
        private ToolStripMenuItem MenuExit;
        private ToolStripMenuItem MenuAnimation;
        private ToolStripMenuItem MenuAttack;
        private ToolStripMenuItem MenuGathering;
        private ToolStripMenuItem MenuDeath;
        private ToolStripMenuItem MenuIdle;
        private ToolStripMenuItem MenuWalking;
        private ToolStripMenuItem MenuRunning;
        private ToolStripMenuItem MenuRessurrection;
        private ToolStripMenuItem MenuTalk;
        private ToolStripMenuItem MenuSpecial;
        private ToolStripMenuItem MenuEmote;
        private GroupBox groupBox1;
        private ToolStripMenuItem MenuExport;
        private ToolStripMenuItem MenuNew;
        private ToolStripMenuItem MenuClose;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripSeparator toolStripMenuItem1;
        private Label label1;
        private TextBox TextProjectName;
        private Label label2;
        private TextBox TextModelId;
        private Label label3;
        private TextBox TextPassphrase;
    }
}
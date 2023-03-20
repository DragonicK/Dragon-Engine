namespace Dragon.Animator {
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
            TextClientOutput = new TextBox();
            MenuStrip = new MenuStrip();
            MenuFile = new ToolStripMenuItem();
            MenuExit = new ToolStripMenuItem();
            MenuContent = new ToolStripMenuItem();
            MenuAnimation = new ToolStripMenuItem();
            GroupOutput = new GroupBox();
            ButtonClienteOutput = new Button();
            label3 = new Label();
            TextResourcePath = new TextBox();
            label1 = new Label();
            ButtonServerPath = new Button();
            GroupProject = new GroupBox();
            MenuStrip.SuspendLayout();
            GroupOutput.SuspendLayout();
            GroupProject.SuspendLayout();
            SuspendLayout();
            // 
            // TextClientOutput
            // 
            TextClientOutput.Location = new Point(26, 41);
            TextClientOutput.Name = "TextClientOutput";
            TextClientOutput.Size = new Size(257, 23);
            TextClientOutput.TabIndex = 1;
            // 
            // MenuStrip
            // 
            MenuStrip.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            MenuStrip.Items.AddRange(new ToolStripItem[] { MenuFile, MenuContent });
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Size = new Size(385, 24);
            MenuStrip.TabIndex = 3;
            MenuStrip.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            MenuFile.DropDownItems.AddRange(new ToolStripItem[] { MenuExit });
            MenuFile.Name = "MenuFile";
            MenuFile.Size = new Size(47, 20);
            MenuFile.Text = "File";
            // 
            // MenuExit
            // 
            MenuExit.Name = "MenuExit";
            MenuExit.Size = new Size(102, 22);
            MenuExit.Text = "Exit";
            MenuExit.Click += MenuExit_Click;
            // 
            // MenuContent
            // 
            MenuContent.DropDownItems.AddRange(new ToolStripItem[] { MenuAnimation });
            MenuContent.Name = "MenuContent";
            MenuContent.Size = new Size(68, 20);
            MenuContent.Text = "Content";
            // 
            // MenuAnimation
            // 
            MenuAnimation.Name = "MenuAnimation";
            MenuAnimation.Size = new Size(137, 22);
            MenuAnimation.Text = "Animation";
            MenuAnimation.Click += MenuAnimation_Click;
            // 
            // GroupOutput
            // 
            GroupOutput.Controls.Add(ButtonClienteOutput);
            GroupOutput.Controls.Add(label3);
            GroupOutput.Controls.Add(TextClientOutput);
            GroupOutput.Location = new Point(13, 145);
            GroupOutput.Name = "GroupOutput";
            GroupOutput.Size = new Size(360, 112);
            GroupOutput.TabIndex = 5;
            GroupOutput.TabStop = false;
            GroupOutput.Text = "Content Output Path";
            // 
            // ButtonClienteOutput
            // 
            ButtonClienteOutput.Location = new Point(284, 41);
            ButtonClienteOutput.Name = "ButtonClienteOutput";
            ButtonClienteOutput.Size = new Size(47, 24);
            ButtonClienteOutput.TabIndex = 5;
            ButtonClienteOutput.Text = "...";
            ButtonClienteOutput.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 23);
            label3.Name = "label3";
            label3.Size = new Size(40, 15);
            label3.TabIndex = 3;
            label3.Text = "Client:";
            // 
            // TextResourcePath
            // 
            TextResourcePath.Location = new Point(27, 47);
            TextResourcePath.Name = "TextResourcePath";
            TextResourcePath.Size = new Size(257, 23);
            TextResourcePath.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 29);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 2;
            label1.Text = "Resource:";
            // 
            // ButtonServerPath
            // 
            ButtonServerPath.Location = new Point(285, 46);
            ButtonServerPath.Name = "ButtonServerPath";
            ButtonServerPath.Size = new Size(47, 24);
            ButtonServerPath.TabIndex = 4;
            ButtonServerPath.Text = "...";
            ButtonServerPath.UseVisualStyleBackColor = true;
            // 
            // GroupProject
            // 
            GroupProject.Controls.Add(ButtonServerPath);
            GroupProject.Controls.Add(label1);
            GroupProject.Controls.Add(TextResourcePath);
            GroupProject.Location = new Point(12, 27);
            GroupProject.Name = "GroupProject";
            GroupProject.Size = new Size(360, 112);
            GroupProject.TabIndex = 4;
            GroupProject.TabStop = false;
            GroupProject.Text = "Resource Path";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(385, 273);
            Controls.Add(GroupProject);
            Controls.Add(MenuStrip);
            Controls.Add(GroupOutput);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Animator";
            Load += FormMain_Load;
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            GroupOutput.ResumeLayout(false);
            GroupOutput.PerformLayout();
            GroupProject.ResumeLayout(false);
            GroupProject.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TextClientOutput;
        private MenuStrip MenuStrip;
        private ToolStripMenuItem MenuFile;
        private ToolStripMenuItem MenuExit;
        private ToolStripMenuItem MenuContent;
        private GroupBox GroupOutput;
        private Button ButtonClienteOutput;
        private Label label3;
        private TextBox TextResourcePath;
        private Label label1;
        private Button ButtonServerPath;
        private GroupBox GroupProject;
        private ToolStripMenuItem MenuAnimation;
    }
}
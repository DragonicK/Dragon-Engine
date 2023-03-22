namespace Dragon.Animator.Animations {
    partial class FormAnimation {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            MenuStrip = new MenuStrip();
            FileMenuItem = new ToolStripMenuItem();
            MenuSave = new ToolStripMenuItem();
            MenuExit = new ToolStripMenuItem();
            GroupList = new GroupBox();
            ButtonClear = new Button();
            ButtonDelete = new Button();
            ButtonAdd = new Button();
            ListIndex = new ListBox();
            GroupData = new GroupBox();
            label10 = new Label();
            TextName = new TextBox();
            label9 = new Label();
            TextId = new TextBox();
            groupBox3 = new GroupBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            PictureUpper = new PictureBox();
            TextUpperTime = new TextBox();
            TextUpperFrame = new TextBox();
            TextUpperLoop = new TextBox();
            TextUpperSprite = new TextBox();
            groupBox2 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            PictureLower = new PictureBox();
            TextLowerTime = new TextBox();
            TextLowerFrame = new TextBox();
            TextLowerLoop = new TextBox();
            TextLowerSprite = new TextBox();
            MenuStrip.SuspendLayout();
            GroupList.SuspendLayout();
            GroupData.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureUpper).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureLower).BeginInit();
            SuspendLayout();
            // 
            // MenuStrip
            // 
            MenuStrip.Items.AddRange(new ToolStripItem[] { FileMenuItem });
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Size = new Size(769, 24);
            MenuStrip.TabIndex = 4;
            MenuStrip.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            FileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { MenuSave, MenuExit });
            FileMenuItem.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FileMenuItem.Name = "FileMenuItem";
            FileMenuItem.Size = new Size(47, 20);
            FileMenuItem.Text = "&File";
            // 
            // MenuSave
            // 
            MenuSave.Name = "MenuSave";
            MenuSave.Size = new Size(102, 22);
            MenuSave.Text = "&Save";
            MenuSave.Click += MenuSave_Click;
            // 
            // MenuExit
            // 
            MenuExit.Name = "MenuExit";
            MenuExit.Size = new Size(102, 22);
            MenuExit.Text = "&Exit";
            MenuExit.Click += MenuExit_Click;
            // 
            // GroupList
            // 
            GroupList.Controls.Add(ButtonClear);
            GroupList.Controls.Add(ButtonDelete);
            GroupList.Controls.Add(ButtonAdd);
            GroupList.Controls.Add(ListIndex);
            GroupList.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            GroupList.Location = new Point(12, 27);
            GroupList.Name = "GroupList";
            GroupList.Padding = new Padding(10, 3, 10, 10);
            GroupList.Size = new Size(218, 595);
            GroupList.TabIndex = 5;
            GroupList.TabStop = false;
            GroupList.Text = "Animations";
            // 
            // ButtonClear
            // 
            ButtonClear.Location = new Point(13, 559);
            ButtonClear.Name = "ButtonClear";
            ButtonClear.Size = new Size(192, 23);
            ButtonClear.TabIndex = 3;
            ButtonClear.Text = "Clear";
            ButtonClear.UseVisualStyleBackColor = true;
            ButtonClear.Click += ButtonClear_Click;
            // 
            // ButtonDelete
            // 
            ButtonDelete.Location = new Point(115, 530);
            ButtonDelete.Name = "ButtonDelete";
            ButtonDelete.Size = new Size(90, 23);
            ButtonDelete.TabIndex = 2;
            ButtonDelete.Text = "Delete";
            ButtonDelete.UseVisualStyleBackColor = true;
            ButtonDelete.Click += ButtonDelete_Click;
            // 
            // ButtonAdd
            // 
            ButtonAdd.Location = new Point(13, 530);
            ButtonAdd.Name = "ButtonAdd";
            ButtonAdd.Size = new Size(90, 23);
            ButtonAdd.TabIndex = 1;
            ButtonAdd.Text = "Add";
            ButtonAdd.UseVisualStyleBackColor = true;
            ButtonAdd.Click += ButtonAdd_Click;
            // 
            // ListIndex
            // 
            ListIndex.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ListIndex.FormattingEnabled = true;
            ListIndex.ItemHeight = 15;
            ListIndex.Location = new Point(13, 22);
            ListIndex.Name = "ListIndex";
            ListIndex.Size = new Size(192, 499);
            ListIndex.TabIndex = 0;
            ListIndex.Click += ListIndex_Click;
            // 
            // GroupData
            // 
            GroupData.Controls.Add(label10);
            GroupData.Controls.Add(TextName);
            GroupData.Controls.Add(label9);
            GroupData.Controls.Add(TextId);
            GroupData.Controls.Add(groupBox3);
            GroupData.Controls.Add(groupBox2);
            GroupData.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            GroupData.Location = new Point(240, 27);
            GroupData.Name = "GroupData";
            GroupData.Size = new Size(515, 595);
            GroupData.TabIndex = 6;
            GroupData.TabStop = false;
            GroupData.Text = "Data";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(28, 70);
            label10.Name = "label10";
            label10.Size = new Size(42, 15);
            label10.TabIndex = 9;
            label10.Text = "Name:";
            // 
            // TextName
            // 
            TextName.Location = new Point(28, 88);
            TextName.Name = "TextName";
            TextName.Size = new Size(192, 23);
            TextName.TabIndex = 8;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(28, 22);
            label9.Name = "label9";
            label9.Size = new Size(28, 15);
            label9.TabIndex = 7;
            label9.Text = "Id:";
            // 
            // TextId
            // 
            TextId.Location = new Point(28, 40);
            TextId.Name = "TextId";
            TextId.Size = new Size(192, 23);
            TextId.TabIndex = 6;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(PictureUpper);
            groupBox3.Controls.Add(TextUpperTime);
            groupBox3.Controls.Add(TextUpperFrame);
            groupBox3.Controls.Add(TextUpperLoop);
            groupBox3.Controls.Add(TextUpperSprite);
            groupBox3.Location = new Point(258, 131);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(233, 441);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "Attack Frame";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 165);
            label5.Name = "label5";
            label5.Size = new Size(77, 15);
            label5.TabIndex = 8;
            label5.Text = "Loop Time:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 121);
            label6.Name = "label6";
            label6.Size = new Size(91, 15);
            label6.TabIndex = 7;
            label6.Text = "Frame Count:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(20, 77);
            label7.Name = "label7";
            label7.Size = new Size(84, 15);
            label7.TabIndex = 6;
            label7.Text = "Loop Count:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(20, 33);
            label8.Name = "label8";
            label8.Size = new Size(56, 15);
            label8.TabIndex = 5;
            label8.Text = "Sprite:";
            // 
            // PictureUpper
            // 
            PictureUpper.BackColor = Color.White;
            PictureUpper.BorderStyle = BorderStyle.FixedSingle;
            PictureUpper.Location = new Point(20, 224);
            PictureUpper.Name = "PictureUpper";
            PictureUpper.Size = new Size(192, 192);
            PictureUpper.TabIndex = 4;
            PictureUpper.TabStop = false;
            // 
            // TextUpperTime
            // 
            TextUpperTime.Location = new Point(20, 183);
            TextUpperTime.Name = "TextUpperTime";
            TextUpperTime.Size = new Size(192, 23);
            TextUpperTime.TabIndex = 3;
            // 
            // TextUpperFrame
            // 
            TextUpperFrame.Location = new Point(20, 139);
            TextUpperFrame.Name = "TextUpperFrame";
            TextUpperFrame.Size = new Size(192, 23);
            TextUpperFrame.TabIndex = 2;
            // 
            // TextUpperLoop
            // 
            TextUpperLoop.Location = new Point(20, 95);
            TextUpperLoop.Name = "TextUpperLoop";
            TextUpperLoop.Size = new Size(192, 23);
            TextUpperLoop.TabIndex = 1;
            // 
            // TextUpperSprite
            // 
            TextUpperSprite.Location = new Point(20, 51);
            TextUpperSprite.Name = "TextUpperSprite";
            TextUpperSprite.Size = new Size(192, 23);
            TextUpperSprite.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(PictureLower);
            groupBox2.Controls.Add(TextLowerTime);
            groupBox2.Controls.Add(TextLowerFrame);
            groupBox2.Controls.Add(TextLowerLoop);
            groupBox2.Controls.Add(TextLowerSprite);
            groupBox2.Location = new Point(19, 131);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(233, 441);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Cast Frame";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 165);
            label4.Name = "label4";
            label4.Size = new Size(77, 15);
            label4.TabIndex = 8;
            label4.Text = "Loop Time:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 121);
            label3.Name = "label3";
            label3.Size = new Size(91, 15);
            label3.TabIndex = 7;
            label3.Text = "Frame Count:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 77);
            label2.Name = "label2";
            label2.Size = new Size(84, 15);
            label2.TabIndex = 6;
            label2.Text = "Loop Count:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 33);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 5;
            label1.Text = "Sprite:";
            // 
            // PictureLower
            // 
            PictureLower.BackColor = Color.White;
            PictureLower.BorderStyle = BorderStyle.FixedSingle;
            PictureLower.Location = new Point(20, 224);
            PictureLower.Name = "PictureLower";
            PictureLower.Size = new Size(192, 192);
            PictureLower.TabIndex = 4;
            PictureLower.TabStop = false;
            // 
            // TextLowerTime
            // 
            TextLowerTime.Location = new Point(20, 183);
            TextLowerTime.Name = "TextLowerTime";
            TextLowerTime.Size = new Size(192, 23);
            TextLowerTime.TabIndex = 3;
            // 
            // TextLowerFrame
            // 
            TextLowerFrame.Location = new Point(20, 139);
            TextLowerFrame.Name = "TextLowerFrame";
            TextLowerFrame.Size = new Size(192, 23);
            TextLowerFrame.TabIndex = 2;
            // 
            // TextLowerLoop
            // 
            TextLowerLoop.Location = new Point(20, 95);
            TextLowerLoop.Name = "TextLowerLoop";
            TextLowerLoop.Size = new Size(192, 23);
            TextLowerLoop.TabIndex = 1;
            // 
            // TextLowerSprite
            // 
            TextLowerSprite.Location = new Point(20, 51);
            TextLowerSprite.Name = "TextLowerSprite";
            TextLowerSprite.Size = new Size(192, 23);
            TextLowerSprite.TabIndex = 0;
            // 
            // FormAnimation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(769, 638);
            Controls.Add(GroupData);
            Controls.Add(GroupList);
            Controls.Add(MenuStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormAnimation";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Animation Editor";
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            GroupList.ResumeLayout(false);
            GroupData.ResumeLayout(false);
            GroupData.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureUpper).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureLower).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MenuStrip;
        private ToolStripMenuItem FileMenuItem;
        private ToolStripMenuItem MenuSave;
        private ToolStripMenuItem MenuExit;
        private GroupBox GroupList;
        private Button ButtonClear;
        private Button ButtonDelete;
        private Button ButtonAdd;
        private ListBox ListIndex;
        private GroupBox GroupData;
        private GroupBox groupBox2;
        private PictureBox PictureLower;
        private TextBox TextLowerTime;
        private TextBox TextLowerFrame;
        private TextBox TextLowerLoop;
        private TextBox TextLowerSprite;
        private Label label1;
        private Label label4;
        private Label label3;
        private Label label2;
        private GroupBox groupBox3;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private PictureBox PictureUpper;
        private TextBox TextUpperTime;
        private TextBox TextUpperFrame;
        private TextBox TextUpperLoop;
        private TextBox TextUpperSprite;
        private Label label10;
        private TextBox TextName;
        private Label label9;
        private TextBox TextId;
    }
}
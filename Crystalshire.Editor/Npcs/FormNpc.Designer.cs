namespace Crystalshire.Editor.Npcs {
    partial class FormNpc {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNpc));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupList = new System.Windows.Forms.GroupBox();
            this.ButtonClear = new System.Windows.Forms.Button();
            this.ButtonDelete = new System.Windows.Forms.Button();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ListIndex = new System.Windows.Forms.ListBox();
            this.TabAchievement = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.GroupData = new System.Windows.Forms.GroupBox();
            this.TextTitle = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TextExperience = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TextAttributeId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ComboBehaviour = new System.Windows.Forms.ComboBox();
            this.TextSound = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TextLevel = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.TextModelId = new System.Windows.Forms.TextBox();
            this.LabelModel = new System.Windows.Forms.Label();
            this.TextName = new System.Windows.Forms.TextBox();
            this.LabelName = new System.Windows.Forms.Label();
            this.TextId = new System.Windows.Forms.TextBox();
            this.LabelId = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.GroupConversation = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TextGreetings = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextConversationId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ButtonAddConversation = new System.Windows.Forms.Button();
            this.ButtonClearConversation = new System.Windows.Forms.Button();
            this.ButtonRemoveConversation = new System.Windows.Forms.Button();
            this.ListConversation = new System.Windows.Forms.ListBox();
            this.MenuStrip.SuspendLayout();
            this.GroupList.SuspendLayout();
            this.TabAchievement.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.GroupData.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.GroupConversation.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(543, 24);
            this.MenuStrip.TabIndex = 7;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSave,
            this.MenuExit});
            this.FileMenuItem.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(47, 20);
            this.FileMenuItem.Text = "&File";
            // 
            // MenuSave
            // 
            this.MenuSave.Name = "MenuSave";
            this.MenuSave.Size = new System.Drawing.Size(102, 22);
            this.MenuSave.Text = "&Save";
            this.MenuSave.Click += new System.EventHandler(this.MenuSave_Click);
            // 
            // MenuExit
            // 
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(102, 22);
            this.MenuExit.Text = "&Exit";
            this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // GroupList
            // 
            this.GroupList.Controls.Add(this.ButtonClear);
            this.GroupList.Controls.Add(this.ButtonDelete);
            this.GroupList.Controls.Add(this.ButtonAdd);
            this.GroupList.Controls.Add(this.ListIndex);
            this.GroupList.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GroupList.Location = new System.Drawing.Point(12, 27);
            this.GroupList.Name = "GroupList";
            this.GroupList.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.GroupList.Size = new System.Drawing.Size(218, 576);
            this.GroupList.TabIndex = 8;
            this.GroupList.TabStop = false;
            this.GroupList.Text = "Items";
            // 
            // ButtonClear
            // 
            this.ButtonClear.Location = new System.Drawing.Point(13, 538);
            this.ButtonClear.Name = "ButtonClear";
            this.ButtonClear.Size = new System.Drawing.Size(192, 23);
            this.ButtonClear.TabIndex = 3;
            this.ButtonClear.Text = "Clear";
            this.ButtonClear.UseVisualStyleBackColor = true;
            this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.Location = new System.Drawing.Point(115, 509);
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(90, 23);
            this.ButtonDelete.TabIndex = 2;
            this.ButtonDelete.Text = "Delete";
            this.ButtonDelete.UseVisualStyleBackColor = true;
            this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(13, 509);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(90, 23);
            this.ButtonAdd.TabIndex = 1;
            this.ButtonAdd.Text = "Add";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // ListIndex
            // 
            this.ListIndex.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ListIndex.FormattingEnabled = true;
            this.ListIndex.ItemHeight = 15;
            this.ListIndex.Location = new System.Drawing.Point(13, 22);
            this.ListIndex.Name = "ListIndex";
            this.ListIndex.Size = new System.Drawing.Size(192, 469);
            this.ListIndex.TabIndex = 0;
            this.ListIndex.Click += new System.EventHandler(this.ListIndex_Click);
            // 
            // TabAchievement
            // 
            this.TabAchievement.Controls.Add(this.tabPage1);
            this.TabAchievement.Controls.Add(this.tabPage2);
            this.TabAchievement.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TabAchievement.Location = new System.Drawing.Point(236, 32);
            this.TabAchievement.Name = "TabAchievement";
            this.TabAchievement.SelectedIndex = 0;
            this.TabAchievement.Size = new System.Drawing.Size(296, 571);
            this.TabAchievement.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GroupData);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(288, 543);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // GroupData
            // 
            this.GroupData.Controls.Add(this.TextTitle);
            this.GroupData.Controls.Add(this.label5);
            this.GroupData.Controls.Add(this.TextExperience);
            this.GroupData.Controls.Add(this.label3);
            this.GroupData.Controls.Add(this.TextAttributeId);
            this.GroupData.Controls.Add(this.label2);
            this.GroupData.Controls.Add(this.label1);
            this.GroupData.Controls.Add(this.ComboBehaviour);
            this.GroupData.Controls.Add(this.TextSound);
            this.GroupData.Controls.Add(this.label6);
            this.GroupData.Controls.Add(this.TextLevel);
            this.GroupData.Controls.Add(this.label16);
            this.GroupData.Controls.Add(this.TextModelId);
            this.GroupData.Controls.Add(this.LabelModel);
            this.GroupData.Controls.Add(this.TextName);
            this.GroupData.Controls.Add(this.LabelName);
            this.GroupData.Controls.Add(this.TextId);
            this.GroupData.Controls.Add(this.LabelId);
            this.GroupData.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GroupData.Location = new System.Drawing.Point(12, 8);
            this.GroupData.Name = "GroupData";
            this.GroupData.Size = new System.Drawing.Size(263, 522);
            this.GroupData.TabIndex = 4;
            this.GroupData.TabStop = false;
            this.GroupData.Text = "Data";
            // 
            // TextTitle
            // 
            this.TextTitle.Location = new System.Drawing.Point(15, 127);
            this.TextTitle.Name = "TextTitle";
            this.TextTitle.Size = new System.Drawing.Size(230, 23);
            this.TextTitle.TabIndex = 20;
            this.TextTitle.TextChanged += new System.EventHandler(this.TextTitle_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 15);
            this.label5.TabIndex = 19;
            this.label5.Text = "Title:";
            // 
            // TextExperience
            // 
            this.TextExperience.Location = new System.Drawing.Point(15, 361);
            this.TextExperience.Name = "TextExperience";
            this.TextExperience.Size = new System.Drawing.Size(230, 23);
            this.TextExperience.TabIndex = 12;
            this.TextExperience.Text = "0";
            this.TextExperience.TextChanged += new System.EventHandler(this.TextExperience_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 342);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Experience:";
            // 
            // TextAttributeId
            // 
            this.TextAttributeId.Location = new System.Drawing.Point(15, 314);
            this.TextAttributeId.Name = "TextAttributeId";
            this.TextAttributeId.Size = new System.Drawing.Size(230, 23);
            this.TextAttributeId.TabIndex = 11;
            this.TextAttributeId.Text = "0";
            this.TextAttributeId.TextChanged += new System.EventHandler(this.TextAttributeId_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "Attribute:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Behaviour";
            // 
            // ComboBehaviour
            // 
            this.ComboBehaviour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBehaviour.FormattingEnabled = true;
            this.ComboBehaviour.Location = new System.Drawing.Point(15, 175);
            this.ComboBehaviour.Name = "ComboBehaviour";
            this.ComboBehaviour.Size = new System.Drawing.Size(230, 23);
            this.ComboBehaviour.TabIndex = 8;
            this.ComboBehaviour.SelectedIndexChanged += new System.EventHandler(this.ComboBehaviour_SelectedIndexChanged);
            // 
            // TextSound
            // 
            this.TextSound.Location = new System.Drawing.Point(15, 409);
            this.TextSound.Name = "TextSound";
            this.TextSound.Size = new System.Drawing.Size(230, 23);
            this.TextSound.TabIndex = 13;
            this.TextSound.Text = "None.";
            this.TextSound.TextChanged += new System.EventHandler(this.TextSound_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 389);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Sound:";
            // 
            // TextLevel
            // 
            this.TextLevel.Location = new System.Drawing.Point(15, 269);
            this.TextLevel.Name = "TextLevel";
            this.TextLevel.Size = new System.Drawing.Size(230, 23);
            this.TextLevel.TabIndex = 10;
            this.TextLevel.Text = "0";
            this.TextLevel.TextChanged += new System.EventHandler(this.TextLevel_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 250);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 15);
            this.label16.TabIndex = 10;
            this.label16.Text = "Level:";
            // 
            // TextModelId
            // 
            this.TextModelId.Location = new System.Drawing.Point(15, 225);
            this.TextModelId.Name = "TextModelId";
            this.TextModelId.Size = new System.Drawing.Size(230, 23);
            this.TextModelId.TabIndex = 9;
            this.TextModelId.Text = "0";
            this.TextModelId.TextChanged += new System.EventHandler(this.TextModelId_TextChanged);
            // 
            // LabelModel
            // 
            this.LabelModel.AutoSize = true;
            this.LabelModel.Location = new System.Drawing.Point(15, 207);
            this.LabelModel.Name = "LabelModel";
            this.LabelModel.Size = new System.Drawing.Size(70, 15);
            this.LabelModel.TabIndex = 8;
            this.LabelModel.Text = "Model Id:";
            // 
            // TextName
            // 
            this.TextName.Location = new System.Drawing.Point(15, 83);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(230, 23);
            this.TextName.TabIndex = 6;
            this.TextName.TextChanged += new System.EventHandler(this.TextName_TextChanged);
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Location = new System.Drawing.Point(15, 65);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(42, 15);
            this.LabelName.TabIndex = 2;
            this.LabelName.Text = "Name:";
            // 
            // TextId
            // 
            this.TextId.Location = new System.Drawing.Point(15, 39);
            this.TextId.Name = "TextId";
            this.TextId.Size = new System.Drawing.Size(230, 23);
            this.TextId.TabIndex = 5;
            this.TextId.Text = "0";
            this.TextId.Validated += new System.EventHandler(this.TextId_Validated);
            // 
            // LabelId
            // 
            this.LabelId.AutoSize = true;
            this.LabelId.Location = new System.Drawing.Point(15, 21);
            this.LabelId.Name = "LabelId";
            this.LabelId.Size = new System.Drawing.Size(28, 15);
            this.LabelId.TabIndex = 0;
            this.LabelId.Text = "Id:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.GroupConversation);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(288, 543);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Conversation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // GroupConversation
            // 
            this.GroupConversation.Controls.Add(this.groupBox2);
            this.GroupConversation.Controls.Add(this.groupBox1);
            this.GroupConversation.Controls.Add(this.ButtonClearConversation);
            this.GroupConversation.Controls.Add(this.ButtonRemoveConversation);
            this.GroupConversation.Controls.Add(this.ListConversation);
            this.GroupConversation.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GroupConversation.Location = new System.Drawing.Point(12, 8);
            this.GroupConversation.Name = "GroupConversation";
            this.GroupConversation.Size = new System.Drawing.Size(263, 522);
            this.GroupConversation.TabIndex = 5;
            this.GroupConversation.TabStop = false;
            this.GroupConversation.Text = "Conversation";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TextGreetings);
            this.groupBox2.Location = new System.Drawing.Point(15, 338);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 145);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Greetings";
            // 
            // TextGreetings
            // 
            this.TextGreetings.Location = new System.Drawing.Point(18, 22);
            this.TextGreetings.Multiline = true;
            this.TextGreetings.Name = "TextGreetings";
            this.TextGreetings.Size = new System.Drawing.Size(197, 105);
            this.TextGreetings.TabIndex = 8;
            this.TextGreetings.TextChanged += new System.EventHandler(this.TextGreetings_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextConversationId);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ButtonAddConversation);
            this.groupBox1.Location = new System.Drawing.Point(15, 227);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 105);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add ";
            // 
            // TextConversationId
            // 
            this.TextConversationId.Location = new System.Drawing.Point(18, 38);
            this.TextConversationId.Name = "TextConversationId";
            this.TextConversationId.Size = new System.Drawing.Size(197, 23);
            this.TextConversationId.TabIndex = 8;
            this.TextConversationId.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Conversation Id:";
            // 
            // ButtonAddConversation
            // 
            this.ButtonAddConversation.Location = new System.Drawing.Point(18, 67);
            this.ButtonAddConversation.Name = "ButtonAddConversation";
            this.ButtonAddConversation.Size = new System.Drawing.Size(197, 23);
            this.ButtonAddConversation.TabIndex = 2;
            this.ButtonAddConversation.Text = "Add";
            this.ButtonAddConversation.UseVisualStyleBackColor = true;
            this.ButtonAddConversation.Click += new System.EventHandler(this.ButtonAddConversation_Click);
            // 
            // ButtonClearConversation
            // 
            this.ButtonClearConversation.Location = new System.Drawing.Point(15, 182);
            this.ButtonClearConversation.Name = "ButtonClearConversation";
            this.ButtonClearConversation.Size = new System.Drawing.Size(110, 23);
            this.ButtonClearConversation.TabIndex = 4;
            this.ButtonClearConversation.Text = "Clear";
            this.ButtonClearConversation.UseVisualStyleBackColor = true;
            this.ButtonClearConversation.Click += new System.EventHandler(this.ButtonClearConversation_Click);
            // 
            // ButtonRemoveConversation
            // 
            this.ButtonRemoveConversation.Location = new System.Drawing.Point(138, 182);
            this.ButtonRemoveConversation.Name = "ButtonRemoveConversation";
            this.ButtonRemoveConversation.Size = new System.Drawing.Size(110, 23);
            this.ButtonRemoveConversation.TabIndex = 3;
            this.ButtonRemoveConversation.Text = "Remove";
            this.ButtonRemoveConversation.UseVisualStyleBackColor = true;
            this.ButtonRemoveConversation.Click += new System.EventHandler(this.ButtonRemoveConversation_Click);
            // 
            // ListConversation
            // 
            this.ListConversation.FormattingEnabled = true;
            this.ListConversation.ItemHeight = 15;
            this.ListConversation.Location = new System.Drawing.Point(15, 22);
            this.ListConversation.Name = "ListConversation";
            this.ListConversation.Size = new System.Drawing.Size(233, 154);
            this.ListConversation.TabIndex = 0;
            // 
            // FormNpc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 611);
            this.Controls.Add(this.GroupList);
            this.Controls.Add(this.TabAchievement);
            this.Controls.Add(this.MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormNpc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Npc Editor";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.GroupList.ResumeLayout(false);
            this.TabAchievement.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.GroupData.ResumeLayout(false);
            this.GroupData.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.GroupConversation.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private TabControl TabAchievement;
        private TabPage tabPage1;
        private GroupBox GroupData;
        private TextBox TextSound;
        private Label label6;
        private TextBox TextLevel;
        private Label label16;
        private TextBox TextModelId;
        private Label LabelModel;
        private TextBox TextName;
        private Label LabelName;
        private TextBox TextId;
        private Label LabelId;
        private Label label1;
        private ComboBox ComboBehaviour;
        private TextBox TextAttributeId;
        private Label label2;
        private TextBox TextExperience;
        private Label label3;
        private TabPage tabPage2;
        private GroupBox GroupConversation;
        private ListBox ListConversation;
        private GroupBox groupBox1;
        private Button ButtonAddConversation;
        private Button ButtonClearConversation;
        private Button ButtonRemoveConversation;
        private TextBox TextConversationId;
        private Label label4;
        private GroupBox groupBox2;
        private TextBox TextGreetings;
        private TextBox TextTitle;
        private Label label5;
    }
}
namespace Crystalshire.Editor.Titles {
    partial class FormTitle {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTitle));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupList = new System.Windows.Forms.GroupBox();
            this.ButtonClear = new System.Windows.Forms.Button();
            this.ButtonDelete = new System.Windows.Forms.Button();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ListIndex = new System.Windows.Forms.ListBox();
            this.TabTitle = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.GroupData = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LabelAttribute = new System.Windows.Forms.Label();
            this.TextAttributeId = new System.Windows.Forms.TextBox();
            this.ComboRarity = new System.Windows.Forms.ComboBox();
            this.LabelRanking = new System.Windows.Forms.Label();
            this.TextDescription = new System.Windows.Forms.TextBox();
            this.LabelDescription = new System.Windows.Forms.Label();
            this.TextName = new System.Windows.Forms.TextBox();
            this.LabelName = new System.Windows.Forms.Label();
            this.TextId = new System.Windows.Forms.TextBox();
            this.LabelId = new System.Windows.Forms.Label();
            this.MenuStrip.SuspendLayout();
            this.GroupList.SuspendLayout();
            this.TabTitle.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.GroupData.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(545, 24);
            this.MenuStrip.TabIndex = 3;
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
            this.GroupList.TabIndex = 4;
            this.GroupList.TabStop = false;
            this.GroupList.Text = "Titles";
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
            // TabTitle
            // 
            this.TabTitle.Controls.Add(this.tabPage1);
            this.TabTitle.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TabTitle.Location = new System.Drawing.Point(236, 32);
            this.TabTitle.Name = "TabTitle";
            this.TabTitle.SelectedIndex = 0;
            this.TabTitle.Size = new System.Drawing.Size(297, 571);
            this.TabTitle.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GroupData);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(289, 543);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // GroupData
            // 
            this.GroupData.Controls.Add(this.groupBox2);
            this.GroupData.Controls.Add(this.ComboRarity);
            this.GroupData.Controls.Add(this.LabelRanking);
            this.GroupData.Controls.Add(this.TextDescription);
            this.GroupData.Controls.Add(this.LabelDescription);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LabelAttribute);
            this.groupBox2.Controls.Add(this.TextAttributeId);
            this.groupBox2.Location = new System.Drawing.Point(15, 357);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 111);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Attribute:";
            // 
            // LabelAttribute
            // 
            this.LabelAttribute.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelAttribute.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelAttribute.Location = new System.Drawing.Point(3, 19);
            this.LabelAttribute.Name = "LabelAttribute";
            this.LabelAttribute.Size = new System.Drawing.Size(224, 60);
            this.LabelAttribute.TabIndex = 11;
            this.LabelAttribute.Text = "Unselected";
            this.LabelAttribute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextAttributeId
            // 
            this.TextAttributeId.Location = new System.Drawing.Point(6, 82);
            this.TextAttributeId.Name = "TextAttributeId";
            this.TextAttributeId.Size = new System.Drawing.Size(218, 23);
            this.TextAttributeId.TabIndex = 10;
            this.TextAttributeId.Text = "0";
            this.TextAttributeId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextAttributeId.TextChanged += new System.EventHandler(this.TextAttributeId_TextChanged);
            // 
            // ComboRarity
            // 
            this.ComboRarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboRarity.FormattingEnabled = true;
            this.ComboRarity.Location = new System.Drawing.Point(15, 328);
            this.ComboRarity.Name = "ComboRarity";
            this.ComboRarity.Size = new System.Drawing.Size(230, 23);
            this.ComboRarity.TabIndex = 9;
            this.ComboRarity.SelectedIndexChanged += new System.EventHandler(this.ComboRarity_SelectedIndexChanged);
            // 
            // LabelRanking
            // 
            this.LabelRanking.AutoSize = true;
            this.LabelRanking.Location = new System.Drawing.Point(15, 310);
            this.LabelRanking.Name = "LabelRanking";
            this.LabelRanking.Size = new System.Drawing.Size(63, 15);
            this.LabelRanking.TabIndex = 6;
            this.LabelRanking.Text = "Ranking:";
            // 
            // TextDescription
            // 
            this.TextDescription.Location = new System.Drawing.Point(15, 126);
            this.TextDescription.Multiline = true;
            this.TextDescription.Name = "TextDescription";
            this.TextDescription.Size = new System.Drawing.Size(230, 178);
            this.TextDescription.TabIndex = 7;
            this.TextDescription.TextChanged += new System.EventHandler(this.TextDescription_TextChanged);
            // 
            // LabelDescription
            // 
            this.LabelDescription.AutoSize = true;
            this.LabelDescription.Location = new System.Drawing.Point(15, 108);
            this.LabelDescription.Name = "LabelDescription";
            this.LabelDescription.Size = new System.Drawing.Size(91, 15);
            this.LabelDescription.TabIndex = 4;
            this.LabelDescription.Text = "Description:";
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
            // FormTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 611);
            this.Controls.Add(this.TabTitle);
            this.Controls.Add(this.GroupList);
            this.Controls.Add(this.MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormTitle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Title Editor";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.GroupList.ResumeLayout(false);
            this.TabTitle.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.GroupData.ResumeLayout(false);
            this.GroupData.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private TabControl TabTitle;
        private TabPage tabPage1;
        private GroupBox GroupData;
        private GroupBox groupBox2;
        private Label LabelAttribute;
        private TextBox TextAttributeId;
        private ComboBox ComboRarity;
        private Label LabelRanking;
        private TextBox TextDescription;
        private Label LabelDescription;
        private TextBox TextName;
        private Label LabelName;
        private TextBox TextId;
        private Label LabelId;
    }
}
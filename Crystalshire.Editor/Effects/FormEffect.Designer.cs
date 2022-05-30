namespace Crystalshire.Editor.Effects;

partial class FormEffect {
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEffect));
        this.MenuStrip = new System.Windows.Forms.MenuStrip();
        this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
        this.GroupList = new System.Windows.Forms.GroupBox();
        this.ButtonClear = new System.Windows.Forms.Button();
        this.ButtonDelete = new System.Windows.Forms.Button();
        this.ButtonAdd = new System.Windows.Forms.Button();
        this.ListIndex = new System.Windows.Forms.ListBox();
        this.TabEffect = new System.Windows.Forms.TabControl();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.GroupData = new System.Windows.Forms.GroupBox();
        this.TextDuration = new System.Windows.Forms.TextBox();
        this.label7 = new System.Windows.Forms.Label();
        this.TextIconId = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.CheckUnlimited = new System.Windows.Forms.CheckBox();
        this.CheckDispellable = new System.Windows.Forms.CheckBox();
        this.CheckRemoveOnDeath = new System.Windows.Forms.CheckBox();
        this.ComboType = new System.Windows.Forms.ComboBox();
        this.LabelRanking = new System.Windows.Forms.Label();
        this.TextDescription = new System.Windows.Forms.TextBox();
        this.LabelDescription = new System.Windows.Forms.Label();
        this.TextName = new System.Windows.Forms.TextBox();
        this.LabelName = new System.Windows.Forms.Label();
        this.TextId = new System.Windows.Forms.TextBox();
        this.LabelId = new System.Windows.Forms.Label();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.GroupAttribute = new System.Windows.Forms.GroupBox();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.LabelUpgrade = new System.Windows.Forms.Label();
        this.TextUpgradeId = new System.Windows.Forms.TextBox();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.LabelAttribute = new System.Windows.Forms.Label();
        this.TextAttributeId = new System.Windows.Forms.TextBox();
        this.ComboOverride = new System.Windows.Forms.ComboBox();
        this.label1 = new System.Windows.Forms.Label();
        this.MenuStrip.SuspendLayout();
        this.GroupList.SuspendLayout();
        this.TabEffect.SuspendLayout();
        this.tabPage1.SuspendLayout();
        this.GroupData.SuspendLayout();
        this.tabPage2.SuspendLayout();
        this.GroupAttribute.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.groupBox3.SuspendLayout();
        this.SuspendLayout();
        // 
        // MenuStrip
        // 
        this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
        this.MenuStrip.Location = new System.Drawing.Point(0, 0);
        this.MenuStrip.Name = "MenuStrip";
        this.MenuStrip.Size = new System.Drawing.Size(548, 24);
        this.MenuStrip.TabIndex = 4;
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
        this.GroupList.TabIndex = 5;
        this.GroupList.TabStop = false;
        this.GroupList.Text = "Effects";
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
        // TabEffect
        // 
        this.TabEffect.Controls.Add(this.tabPage1);
        this.TabEffect.Controls.Add(this.tabPage2);
        this.TabEffect.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.TabEffect.Location = new System.Drawing.Point(236, 32);
        this.TabEffect.Name = "TabEffect";
        this.TabEffect.SelectedIndex = 0;
        this.TabEffect.Size = new System.Drawing.Size(298, 571);
        this.TabEffect.TabIndex = 6;
        // 
        // tabPage1
        // 
        this.tabPage1.Controls.Add(this.GroupData);
        this.tabPage1.Location = new System.Drawing.Point(4, 24);
        this.tabPage1.Name = "tabPage1";
        this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage1.Size = new System.Drawing.Size(290, 543);
        this.tabPage1.TabIndex = 0;
        this.tabPage1.Text = "Data";
        this.tabPage1.UseVisualStyleBackColor = true;
        // 
        // GroupData
        // 
        this.GroupData.Controls.Add(this.ComboOverride);
        this.GroupData.Controls.Add(this.label1);
        this.GroupData.Controls.Add(this.TextDuration);
        this.GroupData.Controls.Add(this.label7);
        this.GroupData.Controls.Add(this.TextIconId);
        this.GroupData.Controls.Add(this.label6);
        this.GroupData.Controls.Add(this.CheckUnlimited);
        this.GroupData.Controls.Add(this.CheckDispellable);
        this.GroupData.Controls.Add(this.CheckRemoveOnDeath);
        this.GroupData.Controls.Add(this.ComboType);
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
        // TextDuration
        // 
        this.TextDuration.Location = new System.Drawing.Point(15, 486);
        this.TextDuration.Name = "TextDuration";
        this.TextDuration.Size = new System.Drawing.Size(230, 23);
        this.TextDuration.TabIndex = 16;
        this.TextDuration.Text = "0";
        this.TextDuration.TextChanged += new System.EventHandler(this.TextDuration_TextChanged);
        // 
        // label7
        // 
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(15, 468);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(140, 15);
        this.label7.TabIndex = 15;
        this.label7.Text = "Duration (seconds):";
        // 
        // TextIconId
        // 
        this.TextIconId.Location = new System.Drawing.Point(15, 443);
        this.TextIconId.Name = "TextIconId";
        this.TextIconId.Size = new System.Drawing.Size(230, 23);
        this.TextIconId.TabIndex = 14;
        this.TextIconId.Text = "0";
        this.TextIconId.TextChanged += new System.EventHandler(this.TextIconId_TextChanged);
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(15, 425);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(63, 15);
        this.label6.TabIndex = 13;
        this.label6.Text = "Icon Id:";
        // 
        // CheckUnlimited
        // 
        this.CheckUnlimited.AutoSize = true;
        this.CheckUnlimited.Location = new System.Drawing.Point(15, 398);
        this.CheckUnlimited.Name = "CheckUnlimited";
        this.CheckUnlimited.Size = new System.Drawing.Size(89, 19);
        this.CheckUnlimited.TabIndex = 12;
        this.CheckUnlimited.Text = "Unlimited";
        this.CheckUnlimited.UseVisualStyleBackColor = true;
        this.CheckUnlimited.Click += new System.EventHandler(this.CheckUnlimited_Click);
        // 
        // CheckDispellable
        // 
        this.CheckDispellable.AutoSize = true;
        this.CheckDispellable.Location = new System.Drawing.Point(15, 373);
        this.CheckDispellable.Name = "CheckDispellable";
        this.CheckDispellable.Size = new System.Drawing.Size(103, 19);
        this.CheckDispellable.TabIndex = 11;
        this.CheckDispellable.Text = "Dispellable";
        this.CheckDispellable.UseVisualStyleBackColor = true;
        this.CheckDispellable.Click += new System.EventHandler(this.CheckDispellable_Click);
        // 
        // CheckRemoveOnDeath
        // 
        this.CheckRemoveOnDeath.AutoSize = true;
        this.CheckRemoveOnDeath.Location = new System.Drawing.Point(15, 348);
        this.CheckRemoveOnDeath.Name = "CheckRemoveOnDeath";
        this.CheckRemoveOnDeath.Size = new System.Drawing.Size(131, 19);
        this.CheckRemoveOnDeath.TabIndex = 10;
        this.CheckRemoveOnDeath.Text = "Remove On Death";
        this.CheckRemoveOnDeath.UseVisualStyleBackColor = true;
        this.CheckRemoveOnDeath.Click += new System.EventHandler(this.CheckRemoveOnDeath_Click);
        // 
        // ComboType
        // 
        this.ComboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboType.FormattingEnabled = true;
        this.ComboType.Location = new System.Drawing.Point(15, 266);
        this.ComboType.Name = "ComboType";
        this.ComboType.Size = new System.Drawing.Size(230, 23);
        this.ComboType.TabIndex = 9;
        this.ComboType.SelectedIndexChanged += new System.EventHandler(this.ComboType_SelectedIndexChanged);
        // 
        // LabelRanking
        // 
        this.LabelRanking.AutoSize = true;
        this.LabelRanking.Location = new System.Drawing.Point(15, 248);
        this.LabelRanking.Name = "LabelRanking";
        this.LabelRanking.Size = new System.Drawing.Size(42, 15);
        this.LabelRanking.TabIndex = 6;
        this.LabelRanking.Text = "Type:";
        // 
        // TextDescription
        // 
        this.TextDescription.Location = new System.Drawing.Point(15, 126);
        this.TextDescription.Multiline = true;
        this.TextDescription.Name = "TextDescription";
        this.TextDescription.Size = new System.Drawing.Size(230, 116);
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
        // tabPage2
        // 
        this.tabPage2.Controls.Add(this.GroupAttribute);
        this.tabPage2.Location = new System.Drawing.Point(4, 24);
        this.tabPage2.Name = "tabPage2";
        this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage2.Size = new System.Drawing.Size(290, 543);
        this.tabPage2.TabIndex = 1;
        this.tabPage2.Text = "Attribute";
        this.tabPage2.UseVisualStyleBackColor = true;
        // 
        // GroupAttribute
        // 
        this.GroupAttribute.Controls.Add(this.groupBox2);
        this.GroupAttribute.Controls.Add(this.groupBox3);
        this.GroupAttribute.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.GroupAttribute.Location = new System.Drawing.Point(12, 8);
        this.GroupAttribute.Name = "GroupAttribute";
        this.GroupAttribute.Size = new System.Drawing.Size(263, 522);
        this.GroupAttribute.TabIndex = 5;
        this.GroupAttribute.TabStop = false;
        this.GroupAttribute.Text = "Data";
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.LabelUpgrade);
        this.groupBox2.Controls.Add(this.TextUpgradeId);
        this.groupBox2.Location = new System.Drawing.Point(15, 152);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(230, 111);
        this.groupBox2.TabIndex = 11;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Upgrade:";
        // 
        // LabelUpgrade
        // 
        this.LabelUpgrade.Dock = System.Windows.Forms.DockStyle.Top;
        this.LabelUpgrade.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.LabelUpgrade.Location = new System.Drawing.Point(3, 19);
        this.LabelUpgrade.Name = "LabelUpgrade";
        this.LabelUpgrade.Size = new System.Drawing.Size(224, 60);
        this.LabelUpgrade.TabIndex = 11;
        this.LabelUpgrade.Text = "Unselected";
        this.LabelUpgrade.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // TextUpgradeId
        // 
        this.TextUpgradeId.Location = new System.Drawing.Point(6, 82);
        this.TextUpgradeId.Name = "TextUpgradeId";
        this.TextUpgradeId.Size = new System.Drawing.Size(218, 23);
        this.TextUpgradeId.TabIndex = 10;
        this.TextUpgradeId.Text = "0";
        this.TextUpgradeId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.TextUpgradeId.TextChanged += new System.EventHandler(this.TextUpgradeId_TextChanged);
        // 
        // groupBox3
        // 
        this.groupBox3.Controls.Add(this.LabelAttribute);
        this.groupBox3.Controls.Add(this.TextAttributeId);
        this.groupBox3.Location = new System.Drawing.Point(15, 22);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(230, 111);
        this.groupBox3.TabIndex = 10;
        this.groupBox3.TabStop = false;
        this.groupBox3.Text = "Attribute:";
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
        // ComboOverride
        // 
        this.ComboOverride.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboOverride.FormattingEnabled = true;
        this.ComboOverride.Location = new System.Drawing.Point(15, 309);
        this.ComboOverride.Name = "ComboOverride";
        this.ComboOverride.Size = new System.Drawing.Size(230, 23);
        this.ComboOverride.TabIndex = 18;
        this.ComboOverride.SelectedIndexChanged += new System.EventHandler(this.ComboOverride_SelectedIndexChanged);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(15, 291);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(105, 15);
        this.label1.TabIndex = 17;
        this.label1.Text = "Override Type:";
        // 
        // FormEffect
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(548, 611);
        this.Controls.Add(this.TabEffect);
        this.Controls.Add(this.GroupList);
        this.Controls.Add(this.MenuStrip);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.Name = "FormEffect";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Effect Editor";
        this.MenuStrip.ResumeLayout(false);
        this.MenuStrip.PerformLayout();
        this.GroupList.ResumeLayout(false);
        this.TabEffect.ResumeLayout(false);
        this.tabPage1.ResumeLayout(false);
        this.GroupData.ResumeLayout(false);
        this.GroupData.PerformLayout();
        this.tabPage2.ResumeLayout(false);
        this.GroupAttribute.ResumeLayout(false);
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox3.ResumeLayout(false);
        this.groupBox3.PerformLayout();
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
    private TabControl TabEffect;
    private TabPage tabPage1;
    private GroupBox GroupData;
    private ComboBox ComboType;
    private Label LabelRanking;
    private TextBox TextDescription;
    private Label LabelDescription;
    private TextBox TextName;
    private Label LabelName;
    private TextBox TextId;
    private Label LabelId;
    private TabPage tabPage2;
    private GroupBox GroupAttribute;
    private GroupBox groupBox3;
    private Label LabelAttribute;
    private TextBox TextAttributeId;
    private CheckBox CheckUnlimited;
    private CheckBox CheckDispellable;
    private CheckBox CheckRemoveOnDeath;
    private TextBox TextDuration;
    private Label label7;
    private TextBox TextIconId;
    private Label label6;
    private GroupBox groupBox2;
    private Label LabelUpgrade;
    private TextBox TextUpgradeId;
    private ComboBox ComboOverride;
    private Label label1;
}
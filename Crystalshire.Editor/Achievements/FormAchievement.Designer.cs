namespace Crystalshire.Editor.Achievements;

partial class FormAchievement {
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAchievement));
        this.GroupList = new System.Windows.Forms.GroupBox();
        this.ButtonClear = new System.Windows.Forms.Button();
        this.ButtonDelete = new System.Windows.Forms.Button();
        this.ButtonAdd = new System.Windows.Forms.Button();
        this.ListIndex = new System.Windows.Forms.ListBox();
        this.MenuStrip = new System.Windows.Forms.MenuStrip();
        this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
        this.TabAchievement = new System.Windows.Forms.TabControl();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.GroupData = new System.Windows.Forms.GroupBox();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.LabelAttribute = new System.Windows.Forms.Label();
        this.TextAttributeId = new System.Windows.Forms.TextBox();
        this.TextPoint = new System.Windows.Forms.TextBox();
        this.LabelPoint = new System.Windows.Forms.Label();
        this.ComboCategory = new System.Windows.Forms.ComboBox();
        this.LabelCategory = new System.Windows.Forms.Label();
        this.TextDescription = new System.Windows.Forms.TextBox();
        this.LabelDescription = new System.Windows.Forms.Label();
        this.TextName = new System.Windows.Forms.TextBox();
        this.LabelName = new System.Windows.Forms.Label();
        this.TextId = new System.Windows.Forms.TextBox();
        this.LabelId = new System.Windows.Forms.Label();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.GroupReward = new System.Windows.Forms.GroupBox();
        this.ComboRewardBound = new System.Windows.Forms.ComboBox();
        this.LabelRewardIndex = new System.Windows.Forms.Label();
        this.ScrollRewardIndex = new System.Windows.Forms.HScrollBar();
        this.ButtonRemoveReward = new System.Windows.Forms.Button();
        this.ButtonAddReward = new System.Windows.Forms.Button();
        this.label9 = new System.Windows.Forms.Label();
        this.ComboRewardType = new System.Windows.Forms.ComboBox();
        this.label5 = new System.Windows.Forms.Label();
        this.TextRewardUpgradeId = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.TextRewardAttributeId = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.TextRewardLevel = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.TextRewardValue = new System.Windows.Forms.TextBox();
        this.label7 = new System.Windows.Forms.Label();
        this.TextRewardId = new System.Windows.Forms.TextBox();
        this.label8 = new System.Windows.Forms.Label();
        this.tabPage3 = new System.Windows.Forms.TabPage();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.LabelRequirementIndex = new System.Windows.Forms.Label();
        this.ScrollRequirementIndex = new System.Windows.Forms.HScrollBar();
        this.ButtonRemoveRequirement = new System.Windows.Forms.Button();
        this.ButtonAddRequirement = new System.Windows.Forms.Button();
        this.ComboEquipment = new System.Windows.Forms.ComboBox();
        this.label11 = new System.Windows.Forms.Label();
        this.ComboRarity = new System.Windows.Forms.ComboBox();
        this.label10 = new System.Windows.Forms.Label();
        this.TextRequirementCount = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.ComboSecondary = new System.Windows.Forms.ComboBox();
        this.label15 = new System.Windows.Forms.Label();
        this.ComboPrimary = new System.Windows.Forms.ComboBox();
        this.label2 = new System.Windows.Forms.Label();
        this.TextRequirementLevel = new System.Windows.Forms.TextBox();
        this.label12 = new System.Windows.Forms.Label();
        this.TextRequirementValue = new System.Windows.Forms.TextBox();
        this.label13 = new System.Windows.Forms.Label();
        this.TextRequirementId = new System.Windows.Forms.TextBox();
        this.label14 = new System.Windows.Forms.Label();
        this.TextRequirementDescription = new System.Windows.Forms.TextBox();
        this.label16 = new System.Windows.Forms.Label();
        this.GroupList.SuspendLayout();
        this.MenuStrip.SuspendLayout();
        this.TabAchievement.SuspendLayout();
        this.tabPage1.SuspendLayout();
        this.GroupData.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.tabPage2.SuspendLayout();
        this.GroupReward.SuspendLayout();
        this.tabPage3.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.SuspendLayout();
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
        this.GroupList.Size = new System.Drawing.Size(218, 618);
        this.GroupList.TabIndex = 0;
        this.GroupList.TabStop = false;
        this.GroupList.Text = "Achievements";
        // 
        // ButtonClear
        // 
        this.ButtonClear.Location = new System.Drawing.Point(13, 582);
        this.ButtonClear.Name = "ButtonClear";
        this.ButtonClear.Size = new System.Drawing.Size(192, 23);
        this.ButtonClear.TabIndex = 3;
        this.ButtonClear.Text = "Clear";
        this.ButtonClear.UseVisualStyleBackColor = true;
        this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
        // 
        // ButtonDelete
        // 
        this.ButtonDelete.Location = new System.Drawing.Point(115, 553);
        this.ButtonDelete.Name = "ButtonDelete";
        this.ButtonDelete.Size = new System.Drawing.Size(90, 23);
        this.ButtonDelete.TabIndex = 2;
        this.ButtonDelete.Text = "Delete";
        this.ButtonDelete.UseVisualStyleBackColor = true;
        this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
        // 
        // ButtonAdd
        // 
        this.ButtonAdd.Location = new System.Drawing.Point(13, 553);
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
        this.ListIndex.Size = new System.Drawing.Size(192, 514);
        this.ListIndex.TabIndex = 0;
        this.ListIndex.Click += new System.EventHandler(this.ListIndex_Click);
        // 
        // MenuStrip
        // 
        this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
        this.MenuStrip.Location = new System.Drawing.Point(0, 0);
        this.MenuStrip.Name = "MenuStrip";
        this.MenuStrip.Size = new System.Drawing.Size(542, 24);
        this.MenuStrip.TabIndex = 2;
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
        // TabAchievement
        // 
        this.TabAchievement.Controls.Add(this.tabPage1);
        this.TabAchievement.Controls.Add(this.tabPage2);
        this.TabAchievement.Controls.Add(this.tabPage3);
        this.TabAchievement.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.TabAchievement.Location = new System.Drawing.Point(236, 32);
        this.TabAchievement.Name = "TabAchievement";
        this.TabAchievement.SelectedIndex = 0;
        this.TabAchievement.Size = new System.Drawing.Size(296, 617);
        this.TabAchievement.TabIndex = 4;
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
        this.GroupData.Controls.Add(this.groupBox2);
        this.GroupData.Controls.Add(this.TextPoint);
        this.GroupData.Controls.Add(this.LabelPoint);
        this.GroupData.Controls.Add(this.ComboCategory);
        this.GroupData.Controls.Add(this.LabelCategory);
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
        this.groupBox2.Location = new System.Drawing.Point(15, 398);
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
        // TextPoint
        // 
        this.TextPoint.Location = new System.Drawing.Point(15, 326);
        this.TextPoint.Name = "TextPoint";
        this.TextPoint.Size = new System.Drawing.Size(230, 23);
        this.TextPoint.TabIndex = 8;
        this.TextPoint.Text = "0";
        this.TextPoint.TextChanged += new System.EventHandler(this.TextPoint_TextChanged);
        // 
        // LabelPoint
        // 
        this.LabelPoint.AutoSize = true;
        this.LabelPoint.Location = new System.Drawing.Point(15, 308);
        this.LabelPoint.Name = "LabelPoint";
        this.LabelPoint.Size = new System.Drawing.Size(56, 15);
        this.LabelPoint.TabIndex = 8;
        this.LabelPoint.Text = "Points:";
        // 
        // ComboCategory
        // 
        this.ComboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboCategory.FormattingEnabled = true;
        this.ComboCategory.Location = new System.Drawing.Point(15, 369);
        this.ComboCategory.Name = "ComboCategory";
        this.ComboCategory.Size = new System.Drawing.Size(230, 23);
        this.ComboCategory.TabIndex = 9;
        this.ComboCategory.SelectedIndexChanged += new System.EventHandler(this.ComboCategory_SelectedIndexChanged);
        // 
        // LabelCategory
        // 
        this.LabelCategory.AutoSize = true;
        this.LabelCategory.Location = new System.Drawing.Point(15, 351);
        this.LabelCategory.Name = "LabelCategory";
        this.LabelCategory.Size = new System.Drawing.Size(70, 15);
        this.LabelCategory.TabIndex = 6;
        this.LabelCategory.Text = "Category:";
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
        // tabPage2
        // 
        this.tabPage2.Controls.Add(this.GroupReward);
        this.tabPage2.Location = new System.Drawing.Point(4, 24);
        this.tabPage2.Name = "tabPage2";
        this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage2.Size = new System.Drawing.Size(288, 543);
        this.tabPage2.TabIndex = 1;
        this.tabPage2.Text = "Reward";
        this.tabPage2.UseVisualStyleBackColor = true;
        // 
        // GroupReward
        // 
        this.GroupReward.Controls.Add(this.ComboRewardBound);
        this.GroupReward.Controls.Add(this.LabelRewardIndex);
        this.GroupReward.Controls.Add(this.ScrollRewardIndex);
        this.GroupReward.Controls.Add(this.ButtonRemoveReward);
        this.GroupReward.Controls.Add(this.ButtonAddReward);
        this.GroupReward.Controls.Add(this.label9);
        this.GroupReward.Controls.Add(this.ComboRewardType);
        this.GroupReward.Controls.Add(this.label5);
        this.GroupReward.Controls.Add(this.TextRewardUpgradeId);
        this.GroupReward.Controls.Add(this.label3);
        this.GroupReward.Controls.Add(this.TextRewardAttributeId);
        this.GroupReward.Controls.Add(this.label4);
        this.GroupReward.Controls.Add(this.TextRewardLevel);
        this.GroupReward.Controls.Add(this.label6);
        this.GroupReward.Controls.Add(this.TextRewardValue);
        this.GroupReward.Controls.Add(this.label7);
        this.GroupReward.Controls.Add(this.TextRewardId);
        this.GroupReward.Controls.Add(this.label8);
        this.GroupReward.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.GroupReward.Location = new System.Drawing.Point(12, 8);
        this.GroupReward.Name = "GroupReward";
        this.GroupReward.Size = new System.Drawing.Size(263, 522);
        this.GroupReward.TabIndex = 5;
        this.GroupReward.TabStop = false;
        this.GroupReward.Text = "Reward";
        // 
        // ComboRewardBound
        // 
        this.ComboRewardBound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboRewardBound.FormattingEnabled = true;
        this.ComboRewardBound.Items.AddRange(new object[] {
            "False",
            "True"});
        this.ComboRewardBound.Location = new System.Drawing.Point(19, 288);
        this.ComboRewardBound.Name = "ComboRewardBound";
        this.ComboRewardBound.Size = new System.Drawing.Size(230, 23);
        this.ComboRewardBound.TabIndex = 34;
        this.ComboRewardBound.SelectedIndexChanged += new System.EventHandler(this.ComboRewardBound_SelectedIndexChanged);
        // 
        // LabelRewardIndex
        // 
        this.LabelRewardIndex.Location = new System.Drawing.Point(19, 47);
        this.LabelRewardIndex.Name = "LabelRewardIndex";
        this.LabelRewardIndex.Size = new System.Drawing.Size(229, 15);
        this.LabelRewardIndex.TabIndex = 32;
        this.LabelRewardIndex.Text = "Reward Index: 0/0";
        this.LabelRewardIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // ScrollRewardIndex
        // 
        this.ScrollRewardIndex.Enabled = false;
        this.ScrollRewardIndex.LargeChange = 1;
        this.ScrollRewardIndex.Location = new System.Drawing.Point(19, 64);
        this.ScrollRewardIndex.Maximum = 0;
        this.ScrollRewardIndex.Name = "ScrollRewardIndex";
        this.ScrollRewardIndex.Size = new System.Drawing.Size(229, 19);
        this.ScrollRewardIndex.TabIndex = 33;
        this.ScrollRewardIndex.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollRewardIndex_Scroll);
        // 
        // ButtonRemoveReward
        // 
        this.ButtonRemoveReward.Location = new System.Drawing.Point(150, 18);
        this.ButtonRemoveReward.Name = "ButtonRemoveReward";
        this.ButtonRemoveReward.Size = new System.Drawing.Size(75, 23);
        this.ButtonRemoveReward.TabIndex = 31;
        this.ButtonRemoveReward.Text = "Remove";
        this.ButtonRemoveReward.UseVisualStyleBackColor = true;
        this.ButtonRemoveReward.Click += new System.EventHandler(this.ButtonRemoveReward_Click);
        // 
        // ButtonAddReward
        // 
        this.ButtonAddReward.Location = new System.Drawing.Point(49, 18);
        this.ButtonAddReward.Name = "ButtonAddReward";
        this.ButtonAddReward.Size = new System.Drawing.Size(75, 23);
        this.ButtonAddReward.TabIndex = 30;
        this.ButtonAddReward.Text = "Add";
        this.ButtonAddReward.UseVisualStyleBackColor = true;
        this.ButtonAddReward.Click += new System.EventHandler(this.ButtonAddReward_Click);
        // 
        // label9
        // 
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(19, 272);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(56, 15);
        this.label9.TabIndex = 14;
        this.label9.Text = "Bound: ";
        // 
        // ComboRewardType
        // 
        this.ComboRewardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboRewardType.FormattingEnabled = true;
        this.ComboRewardType.Location = new System.Drawing.Point(19, 117);
        this.ComboRewardType.Name = "ComboRewardType";
        this.ComboRewardType.Size = new System.Drawing.Size(230, 23);
        this.ComboRewardType.TabIndex = 11;
        this.ComboRewardType.SelectedIndexChanged += new System.EventHandler(this.ComboRewardType_SelectedIndexChanged);
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(19, 99);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(42, 15);
        this.label5.TabIndex = 12;
        this.label5.Text = "Type:";
        // 
        // TextRewardUpgradeId
        // 
        this.TextRewardUpgradeId.Location = new System.Drawing.Point(19, 374);
        this.TextRewardUpgradeId.Name = "TextRewardUpgradeId";
        this.TextRewardUpgradeId.Size = new System.Drawing.Size(230, 23);
        this.TextRewardUpgradeId.TabIndex = 17;
        this.TextRewardUpgradeId.Text = "0";
        this.TextRewardUpgradeId.TextChanged += new System.EventHandler(this.TextRewardUpgradeId_TextChanged);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(19, 356);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(84, 15);
        this.label3.TabIndex = 10;
        this.label3.Text = "Upgrade Id:";
        // 
        // TextRewardAttributeId
        // 
        this.TextRewardAttributeId.Location = new System.Drawing.Point(19, 332);
        this.TextRewardAttributeId.Name = "TextRewardAttributeId";
        this.TextRewardAttributeId.Size = new System.Drawing.Size(230, 23);
        this.TextRewardAttributeId.TabIndex = 16;
        this.TextRewardAttributeId.Text = "0";
        this.TextRewardAttributeId.TextChanged += new System.EventHandler(this.TextRewardAttributeId_TextChanged);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(19, 314);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(98, 15);
        this.label4.TabIndex = 8;
        this.label4.Text = "Attribute Id:";
        // 
        // TextRewardLevel
        // 
        this.TextRewardLevel.Location = new System.Drawing.Point(19, 248);
        this.TextRewardLevel.Name = "TextRewardLevel";
        this.TextRewardLevel.Size = new System.Drawing.Size(230, 23);
        this.TextRewardLevel.TabIndex = 14;
        this.TextRewardLevel.Text = "0";
        this.TextRewardLevel.TextChanged += new System.EventHandler(this.TextRewardLevel_TextChanged);
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(19, 230);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(49, 15);
        this.label6.TabIndex = 4;
        this.label6.Text = "Level:";
        // 
        // TextRewardValue
        // 
        this.TextRewardValue.Location = new System.Drawing.Point(19, 205);
        this.TextRewardValue.Name = "TextRewardValue";
        this.TextRewardValue.Size = new System.Drawing.Size(230, 23);
        this.TextRewardValue.TabIndex = 13;
        this.TextRewardValue.Text = "0";
        this.TextRewardValue.TextChanged += new System.EventHandler(this.TextRewardValue_TextChanged);
        // 
        // label7
        // 
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(19, 187);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(49, 15);
        this.label7.TabIndex = 2;
        this.label7.Text = "Value:";
        // 
        // TextRewardId
        // 
        this.TextRewardId.Location = new System.Drawing.Point(19, 161);
        this.TextRewardId.Name = "TextRewardId";
        this.TextRewardId.Size = new System.Drawing.Size(230, 23);
        this.TextRewardId.TabIndex = 12;
        this.TextRewardId.Text = "0";
        this.TextRewardId.TextChanged += new System.EventHandler(this.TextRewardId_TextChanged);
        // 
        // label8
        // 
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(19, 143);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(28, 15);
        this.label8.TabIndex = 0;
        this.label8.Text = "Id:";
        // 
        // tabPage3
        // 
        this.tabPage3.Controls.Add(this.groupBox1);
        this.tabPage3.Location = new System.Drawing.Point(4, 24);
        this.tabPage3.Name = "tabPage3";
        this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage3.Size = new System.Drawing.Size(288, 589);
        this.tabPage3.TabIndex = 2;
        this.tabPage3.Text = "Requirement";
        this.tabPage3.UseVisualStyleBackColor = true;
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.TextRequirementDescription);
        this.groupBox1.Controls.Add(this.label16);
        this.groupBox1.Controls.Add(this.LabelRequirementIndex);
        this.groupBox1.Controls.Add(this.ScrollRequirementIndex);
        this.groupBox1.Controls.Add(this.ButtonRemoveRequirement);
        this.groupBox1.Controls.Add(this.ButtonAddRequirement);
        this.groupBox1.Controls.Add(this.ComboEquipment);
        this.groupBox1.Controls.Add(this.label11);
        this.groupBox1.Controls.Add(this.ComboRarity);
        this.groupBox1.Controls.Add(this.label10);
        this.groupBox1.Controls.Add(this.TextRequirementCount);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.ComboSecondary);
        this.groupBox1.Controls.Add(this.label15);
        this.groupBox1.Controls.Add(this.ComboPrimary);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.TextRequirementLevel);
        this.groupBox1.Controls.Add(this.label12);
        this.groupBox1.Controls.Add(this.TextRequirementValue);
        this.groupBox1.Controls.Add(this.label13);
        this.groupBox1.Controls.Add(this.TextRequirementId);
        this.groupBox1.Controls.Add(this.label14);
        this.groupBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.groupBox1.Location = new System.Drawing.Point(12, 8);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(263, 568);
        this.groupBox1.TabIndex = 6;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Requirement";
        // 
        // LabelRequirementIndex
        // 
        this.LabelRequirementIndex.Location = new System.Drawing.Point(17, 51);
        this.LabelRequirementIndex.Name = "LabelRequirementIndex";
        this.LabelRequirementIndex.Size = new System.Drawing.Size(229, 15);
        this.LabelRequirementIndex.TabIndex = 28;
        this.LabelRequirementIndex.Text = "Requirement Index: 0/0";
        this.LabelRequirementIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // ScrollRequirementIndex
        // 
        this.ScrollRequirementIndex.Enabled = false;
        this.ScrollRequirementIndex.LargeChange = 1;
        this.ScrollRequirementIndex.Location = new System.Drawing.Point(17, 68);
        this.ScrollRequirementIndex.Maximum = 0;
        this.ScrollRequirementIndex.Name = "ScrollRequirementIndex";
        this.ScrollRequirementIndex.Size = new System.Drawing.Size(229, 19);
        this.ScrollRequirementIndex.TabIndex = 29;
        this.ScrollRequirementIndex.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollRequirementIndex_Scroll);
        // 
        // ButtonRemoveRequirement
        // 
        this.ButtonRemoveRequirement.Location = new System.Drawing.Point(148, 22);
        this.ButtonRemoveRequirement.Name = "ButtonRemoveRequirement";
        this.ButtonRemoveRequirement.Size = new System.Drawing.Size(75, 23);
        this.ButtonRemoveRequirement.TabIndex = 27;
        this.ButtonRemoveRequirement.Text = "Remove";
        this.ButtonRemoveRequirement.UseVisualStyleBackColor = true;
        this.ButtonRemoveRequirement.Click += new System.EventHandler(this.ButtonRemoveRequirement_Click);
        // 
        // ButtonAddRequirement
        // 
        this.ButtonAddRequirement.Location = new System.Drawing.Point(47, 22);
        this.ButtonAddRequirement.Name = "ButtonAddRequirement";
        this.ButtonAddRequirement.Size = new System.Drawing.Size(75, 23);
        this.ButtonAddRequirement.TabIndex = 26;
        this.ButtonAddRequirement.Text = "Add";
        this.ButtonAddRequirement.UseVisualStyleBackColor = true;
        this.ButtonAddRequirement.Click += new System.EventHandler(this.ButtonAddRequirement_Click);
        // 
        // ComboEquipment
        // 
        this.ComboEquipment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboEquipment.FormattingEnabled = true;
        this.ComboEquipment.Location = new System.Drawing.Point(17, 255);
        this.ComboEquipment.Name = "ComboEquipment";
        this.ComboEquipment.Size = new System.Drawing.Size(230, 23);
        this.ComboEquipment.TabIndex = 24;
        this.ComboEquipment.SelectedIndexChanged += new System.EventHandler(this.ComboEquipment_SelectedIndexChanged);
        // 
        // label11
        // 
        this.label11.AutoSize = true;
        this.label11.Location = new System.Drawing.Point(17, 237);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(77, 15);
        this.label11.TabIndex = 25;
        this.label11.Text = "Equipment:";
        // 
        // ComboRarity
        // 
        this.ComboRarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboRarity.FormattingEnabled = true;
        this.ComboRarity.Location = new System.Drawing.Point(17, 210);
        this.ComboRarity.Name = "ComboRarity";
        this.ComboRarity.Size = new System.Drawing.Size(230, 23);
        this.ComboRarity.TabIndex = 22;
        this.ComboRarity.SelectedIndexChanged += new System.EventHandler(this.ComboRarity_SelectedIndexChanged);
        // 
        // label10
        // 
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(17, 192);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(56, 15);
        this.label10.TabIndex = 23;
        this.label10.Text = "Rarity:";
        // 
        // TextRequirementCount
        // 
        this.TextRequirementCount.Location = new System.Drawing.Point(17, 428);
        this.TextRequirementCount.Name = "TextRequirementCount";
        this.TextRequirementCount.Size = new System.Drawing.Size(230, 23);
        this.TextRequirementCount.TabIndex = 21;
        this.TextRequirementCount.Text = "0";
        this.TextRequirementCount.TextChanged += new System.EventHandler(this.TextRequirementCount_TextChanged);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(17, 410);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(49, 15);
        this.label1.TabIndex = 20;
        this.label1.Text = "Count:";
        // 
        // ComboSecondary
        // 
        this.ComboSecondary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboSecondary.FormattingEnabled = true;
        this.ComboSecondary.Location = new System.Drawing.Point(17, 167);
        this.ComboSecondary.Name = "ComboSecondary";
        this.ComboSecondary.Size = new System.Drawing.Size(230, 23);
        this.ComboSecondary.TabIndex = 18;
        this.ComboSecondary.SelectedIndexChanged += new System.EventHandler(this.ComboSecondary_SelectedIndexChanged);
        // 
        // label15
        // 
        this.label15.AutoSize = true;
        this.label15.Location = new System.Drawing.Point(17, 149);
        this.label15.Name = "label15";
        this.label15.Size = new System.Drawing.Size(77, 15);
        this.label15.TabIndex = 19;
        this.label15.Text = "Secondary:";
        // 
        // ComboPrimary
        // 
        this.ComboPrimary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboPrimary.FormattingEnabled = true;
        this.ComboPrimary.Location = new System.Drawing.Point(17, 125);
        this.ComboPrimary.Name = "ComboPrimary";
        this.ComboPrimary.Size = new System.Drawing.Size(230, 23);
        this.ComboPrimary.TabIndex = 11;
        this.ComboPrimary.SelectedIndexChanged += new System.EventHandler(this.ComboPrimary_SelectedIndexChanged);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(17, 107);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(63, 15);
        this.label2.TabIndex = 12;
        this.label2.Text = "Primary:";
        // 
        // TextRequirementLevel
        // 
        this.TextRequirementLevel.Location = new System.Drawing.Point(17, 387);
        this.TextRequirementLevel.Name = "TextRequirementLevel";
        this.TextRequirementLevel.Size = new System.Drawing.Size(230, 23);
        this.TextRequirementLevel.TabIndex = 14;
        this.TextRequirementLevel.Text = "0";
        this.TextRequirementLevel.TextChanged += new System.EventHandler(this.TextRequirementLevel_TextChanged);
        // 
        // label12
        // 
        this.label12.AutoSize = true;
        this.label12.Location = new System.Drawing.Point(17, 369);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(49, 15);
        this.label12.TabIndex = 4;
        this.label12.Text = "Level:";
        // 
        // TextRequirementValue
        // 
        this.TextRequirementValue.Location = new System.Drawing.Point(17, 344);
        this.TextRequirementValue.Name = "TextRequirementValue";
        this.TextRequirementValue.Size = new System.Drawing.Size(230, 23);
        this.TextRequirementValue.TabIndex = 13;
        this.TextRequirementValue.Text = "0";
        this.TextRequirementValue.TextChanged += new System.EventHandler(this.TextRequirementValue_TextChanged);
        // 
        // label13
        // 
        this.label13.AutoSize = true;
        this.label13.Location = new System.Drawing.Point(17, 326);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(49, 15);
        this.label13.TabIndex = 2;
        this.label13.Text = "Value:";
        // 
        // TextRequirementId
        // 
        this.TextRequirementId.Location = new System.Drawing.Point(17, 300);
        this.TextRequirementId.Name = "TextRequirementId";
        this.TextRequirementId.Size = new System.Drawing.Size(230, 23);
        this.TextRequirementId.TabIndex = 12;
        this.TextRequirementId.Text = "0";
        this.TextRequirementId.TextChanged += new System.EventHandler(this.TextRequirementId_TextChanged);
        // 
        // label14
        // 
        this.label14.AutoSize = true;
        this.label14.Location = new System.Drawing.Point(17, 282);
        this.label14.Name = "label14";
        this.label14.Size = new System.Drawing.Size(28, 15);
        this.label14.TabIndex = 0;
        this.label14.Text = "Id:";
        // 
        // TextRequirementDescription
        // 
        this.TextRequirementDescription.Location = new System.Drawing.Point(16, 473);
        this.TextRequirementDescription.Multiline = true;
        this.TextRequirementDescription.Name = "TextRequirementDescription";
        this.TextRequirementDescription.Size = new System.Drawing.Size(230, 77);
        this.TextRequirementDescription.TabIndex = 31;
        this.TextRequirementDescription.TextChanged += new System.EventHandler(this.TextRequirementDescription_TextChanged);
        // 
        // label16
        // 
        this.label16.AutoSize = true;
        this.label16.Location = new System.Drawing.Point(16, 455);
        this.label16.Name = "label16";
        this.label16.Size = new System.Drawing.Size(91, 15);
        this.label16.TabIndex = 30;
        this.label16.Text = "Description:";
        // 
        // FormAchievement
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(542, 661);
        this.Controls.Add(this.TabAchievement);
        this.Controls.Add(this.MenuStrip);
        this.Controls.Add(this.GroupList);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MainMenuStrip = this.MenuStrip;
        this.MaximizeBox = false;
        this.Name = "FormAchievement";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Achievement Editor";
        this.GroupList.ResumeLayout(false);
        this.MenuStrip.ResumeLayout(false);
        this.MenuStrip.PerformLayout();
        this.TabAchievement.ResumeLayout(false);
        this.tabPage1.ResumeLayout(false);
        this.GroupData.ResumeLayout(false);
        this.GroupData.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.tabPage2.ResumeLayout(false);
        this.GroupReward.ResumeLayout(false);
        this.GroupReward.PerformLayout();
        this.tabPage3.ResumeLayout(false);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private GroupBox GroupList;
    private ListBox ListIndex;
    private MenuStrip MenuStrip;
    private ToolStripMenuItem FileMenuItem;
    private ToolStripMenuItem MenuSave;
    private ToolStripMenuItem MenuExit;
    private Button ButtonClear;
    private Button ButtonDelete;
    private Button ButtonAdd;
    private TabControl TabAchievement;
    private TabPage tabPage1;
    private GroupBox GroupData;
    private GroupBox groupBox2;
    private Label LabelAttribute;
    private TextBox TextAttributeId;
    private TextBox TextPoint;
    private Label LabelPoint;
    private ComboBox ComboCategory;
    private Label LabelCategory;
    private TextBox TextDescription;
    private Label LabelDescription;
    private TextBox TextName;
    private Label LabelName;
    private TextBox TextId;
    private Label LabelId;
    private TabPage tabPage2;
    private GroupBox GroupReward;
    private TextBox TextRewardAttributeId;
    private Label label4;
    private TextBox TextRewardLevel;
    private Label label6;
    private TextBox TextRewardValue;
    private Label label7;
    private TextBox TextRewardId;
    private Label label8;
    private TextBox TextRewardUpgradeId;
    private Label label3;
    private ComboBox ComboRewardType;
    private Label label5;
    private Label label9;
    private TabPage tabPage3;
    private GroupBox groupBox1;
    private ComboBox ComboPrimary;
    private Label label2;
    private TextBox TextRequirementLevel;
    private Label label12;
    private TextBox TextRequirementValue;
    private Label label13;
    private TextBox TextRequirementId;
    private Label label14;
    private ComboBox ComboSecondary;
    private Label label15;
    private TextBox TextRequirementCount;
    private Label label1;
    private ComboBox ComboEquipment;
    private Label label11;
    private ComboBox ComboRarity;
    private Label label10;
    private Label LabelRequirementIndex;
    private HScrollBar ScrollRequirementIndex;
    private Button ButtonRemoveRequirement;
    private Button ButtonAddRequirement;
    private Label LabelRewardIndex;
    private HScrollBar ScrollRewardIndex;
    private Button ButtonRemoveReward;
    private Button ButtonAddReward;
    private ComboBox ComboRewardBound;
    private TextBox TextRequirementDescription;
    private Label label16;
}
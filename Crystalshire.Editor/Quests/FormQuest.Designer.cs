namespace Crystalshire.Editor.Quests;

partial class FormQuest {
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQuest));
        this.MenuStrip = new System.Windows.Forms.MenuStrip();
        this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
        this.GroupList = new System.Windows.Forms.GroupBox();
        this.ButtonClear = new System.Windows.Forms.Button();
        this.ButtonDelete = new System.Windows.Forms.Button();
        this.ButtonAdd = new System.Windows.Forms.Button();
        this.ListIndex = new System.Windows.Forms.ListBox();
        this.TabQuest = new System.Windows.Forms.TabControl();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.GroupData = new System.Windows.Forms.GroupBox();
        this.TextSelectableRewardCount = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.ComboSelectableReward = new System.Windows.Forms.ComboBox();
        this.label4 = new System.Windows.Forms.Label();
        this.ComboShareable = new System.Windows.Forms.ComboBox();
        this.label3 = new System.Windows.Forms.Label();
        this.ComboType = new System.Windows.Forms.ComboBox();
        this.label2 = new System.Windows.Forms.Label();
        this.ComboRepeatable = new System.Windows.Forms.ComboBox();
        this.LabelRepeatable = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.TextSummary = new System.Windows.Forms.TextBox();
        this.TextName = new System.Windows.Forms.TextBox();
        this.LabelName = new System.Windows.Forms.Label();
        this.TextId = new System.Windows.Forms.TextBox();
        this.LabelId = new System.Windows.Forms.Label();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.TextRequirementY = new System.Windows.Forms.TextBox();
        this.label12 = new System.Windows.Forms.Label();
        this.TextRequirementX = new System.Windows.Forms.TextBox();
        this.label11 = new System.Windows.Forms.Label();
        this.TextRequirementCount = new System.Windows.Forms.TextBox();
        this.label10 = new System.Windows.Forms.Label();
        this.TextRequirementEntityId = new System.Windows.Forms.TextBox();
        this.label9 = new System.Windows.Forms.Label();
        this.ComboActionType = new System.Windows.Forms.ComboBox();
        this.label8 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.TextStepSummary = new System.Windows.Forms.TextBox();
        this.TextStepTitle = new System.Windows.Forms.TextBox();
        this.label7 = new System.Windows.Forms.Label();
        this.LabelStepIndex = new System.Windows.Forms.Label();
        this.ScrollStepIndex = new System.Windows.Forms.HScrollBar();
        this.ButtonRemoveStep = new System.Windows.Forms.Button();
        this.ButtonAddStep = new System.Windows.Forms.Button();
        this.tabPage3 = new System.Windows.Forms.TabPage();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.ComboRewardBound = new System.Windows.Forms.ComboBox();
        this.label20 = new System.Windows.Forms.Label();
        this.TextRewardUpgradeId = new System.Windows.Forms.TextBox();
        this.label14 = new System.Windows.Forms.Label();
        this.TextRewardAttributeId = new System.Windows.Forms.TextBox();
        this.label15 = new System.Windows.Forms.Label();
        this.TextRewardLevel = new System.Windows.Forms.TextBox();
        this.label16 = new System.Windows.Forms.Label();
        this.TextRewardValue = new System.Windows.Forms.TextBox();
        this.label17 = new System.Windows.Forms.Label();
        this.TextRewardId = new System.Windows.Forms.TextBox();
        this.label18 = new System.Windows.Forms.Label();
        this.ComboRewardType = new System.Windows.Forms.ComboBox();
        this.label19 = new System.Windows.Forms.Label();
        this.LabelRewardIndex = new System.Windows.Forms.Label();
        this.ScrollRewardIndex = new System.Windows.Forms.HScrollBar();
        this.ButtonRemoveReward = new System.Windows.Forms.Button();
        this.ButtonAddReward = new System.Windows.Forms.Button();
        this.MenuStrip.SuspendLayout();
        this.GroupList.SuspendLayout();
        this.TabQuest.SuspendLayout();
        this.tabPage1.SuspendLayout();
        this.GroupData.SuspendLayout();
        this.tabPage2.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.tabPage3.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.SuspendLayout();
        // 
        // MenuStrip
        // 
        this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
        this.MenuStrip.Location = new System.Drawing.Point(0, 0);
        this.MenuStrip.Name = "MenuStrip";
        this.MenuStrip.Size = new System.Drawing.Size(543, 24);
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
        this.GroupList.Size = new System.Drawing.Size(218, 628);
        this.GroupList.TabIndex = 4;
        this.GroupList.TabStop = false;
        this.GroupList.Text = "Quests";
        // 
        // ButtonClear
        // 
        this.ButtonClear.Location = new System.Drawing.Point(13, 592);
        this.ButtonClear.Name = "ButtonClear";
        this.ButtonClear.Size = new System.Drawing.Size(192, 23);
        this.ButtonClear.TabIndex = 3;
        this.ButtonClear.Text = "Clear";
        this.ButtonClear.UseVisualStyleBackColor = true;
        this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
        // 
        // ButtonDelete
        // 
        this.ButtonDelete.Location = new System.Drawing.Point(115, 563);
        this.ButtonDelete.Name = "ButtonDelete";
        this.ButtonDelete.Size = new System.Drawing.Size(90, 23);
        this.ButtonDelete.TabIndex = 2;
        this.ButtonDelete.Text = "Delete";
        this.ButtonDelete.UseVisualStyleBackColor = true;
        this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
        // 
        // ButtonAdd
        // 
        this.ButtonAdd.Location = new System.Drawing.Point(13, 563);
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
        this.ListIndex.Size = new System.Drawing.Size(192, 529);
        this.ListIndex.TabIndex = 0;
        this.ListIndex.Click += new System.EventHandler(this.ListIndex_Click);
        // 
        // TabQuest
        // 
        this.TabQuest.Controls.Add(this.tabPage1);
        this.TabQuest.Controls.Add(this.tabPage2);
        this.TabQuest.Controls.Add(this.tabPage3);
        this.TabQuest.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.TabQuest.Location = new System.Drawing.Point(236, 32);
        this.TabQuest.Name = "TabQuest";
        this.TabQuest.SelectedIndex = 0;
        this.TabQuest.Size = new System.Drawing.Size(296, 627);
        this.TabQuest.TabIndex = 5;
        // 
        // tabPage1
        // 
        this.tabPage1.Controls.Add(this.GroupData);
        this.tabPage1.Location = new System.Drawing.Point(4, 24);
        this.tabPage1.Name = "tabPage1";
        this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage1.Size = new System.Drawing.Size(288, 599);
        this.tabPage1.TabIndex = 0;
        this.tabPage1.Text = "Data";
        this.tabPage1.UseVisualStyleBackColor = true;
        // 
        // GroupData
        // 
        this.GroupData.Controls.Add(this.TextSelectableRewardCount);
        this.GroupData.Controls.Add(this.label5);
        this.GroupData.Controls.Add(this.ComboSelectableReward);
        this.GroupData.Controls.Add(this.label4);
        this.GroupData.Controls.Add(this.ComboShareable);
        this.GroupData.Controls.Add(this.label3);
        this.GroupData.Controls.Add(this.ComboType);
        this.GroupData.Controls.Add(this.label2);
        this.GroupData.Controls.Add(this.ComboRepeatable);
        this.GroupData.Controls.Add(this.LabelRepeatable);
        this.GroupData.Controls.Add(this.label1);
        this.GroupData.Controls.Add(this.TextSummary);
        this.GroupData.Controls.Add(this.TextName);
        this.GroupData.Controls.Add(this.LabelName);
        this.GroupData.Controls.Add(this.TextId);
        this.GroupData.Controls.Add(this.LabelId);
        this.GroupData.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.GroupData.Location = new System.Drawing.Point(12, 8);
        this.GroupData.Name = "GroupData";
        this.GroupData.Size = new System.Drawing.Size(263, 578);
        this.GroupData.TabIndex = 4;
        this.GroupData.TabStop = false;
        this.GroupData.Text = "Data";
        // 
        // TextSelectableRewardCount
        // 
        this.TextSelectableRewardCount.Location = new System.Drawing.Point(15, 480);
        this.TextSelectableRewardCount.Name = "TextSelectableRewardCount";
        this.TextSelectableRewardCount.Size = new System.Drawing.Size(230, 23);
        this.TextSelectableRewardCount.TabIndex = 19;
        this.TextSelectableRewardCount.Text = "0";
        this.TextSelectableRewardCount.TextChanged += new System.EventHandler(this.TextSelectableRewardCount_TextChanged);
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(15, 460);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(210, 15);
        this.label5.TabIndex = 18;
        this.label5.Text = "Item Selectable Reward Count:";
        // 
        // ComboSelectableReward
        // 
        this.ComboSelectableReward.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboSelectableReward.FormattingEnabled = true;
        this.ComboSelectableReward.Location = new System.Drawing.Point(15, 431);
        this.ComboSelectableReward.Name = "ComboSelectableReward";
        this.ComboSelectableReward.Size = new System.Drawing.Size(230, 23);
        this.ComboSelectableReward.TabIndex = 17;
        this.ComboSelectableReward.SelectedIndexChanged += new System.EventHandler(this.ComboSelectableReward_SelectedIndexChanged);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(15, 413);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(168, 15);
        this.label4.TabIndex = 16;
        this.label4.Text = "Item Selectable Reward:";
        // 
        // ComboShareable
        // 
        this.ComboShareable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboShareable.FormattingEnabled = true;
        this.ComboShareable.Location = new System.Drawing.Point(15, 385);
        this.ComboShareable.Name = "ComboShareable";
        this.ComboShareable.Size = new System.Drawing.Size(230, 23);
        this.ComboShareable.TabIndex = 15;
        this.ComboShareable.SelectedIndexChanged += new System.EventHandler(this.ComboShareable_SelectedIndexChanged);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(15, 367);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(77, 15);
        this.label3.TabIndex = 14;
        this.label3.Text = "Shareable:";
        // 
        // ComboType
        // 
        this.ComboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboType.FormattingEnabled = true;
        this.ComboType.Location = new System.Drawing.Point(15, 295);
        this.ComboType.Name = "ComboType";
        this.ComboType.Size = new System.Drawing.Size(230, 23);
        this.ComboType.TabIndex = 13;
        this.ComboType.SelectedIndexChanged += new System.EventHandler(this.ComboType_SelectedIndexChanged);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(15, 278);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(42, 15);
        this.label2.TabIndex = 12;
        this.label2.Text = "Type:";
        // 
        // ComboRepeatable
        // 
        this.ComboRepeatable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboRepeatable.FormattingEnabled = true;
        this.ComboRepeatable.Location = new System.Drawing.Point(15, 339);
        this.ComboRepeatable.Name = "ComboRepeatable";
        this.ComboRepeatable.Size = new System.Drawing.Size(230, 23);
        this.ComboRepeatable.TabIndex = 11;
        this.ComboRepeatable.SelectedIndexChanged += new System.EventHandler(this.ComboRepeatable_SelectedIndexChanged);
        // 
        // LabelRepeatable
        // 
        this.LabelRepeatable.AutoSize = true;
        this.LabelRepeatable.Location = new System.Drawing.Point(15, 321);
        this.LabelRepeatable.Name = "LabelRepeatable";
        this.LabelRepeatable.Size = new System.Drawing.Size(84, 15);
        this.LabelRepeatable.TabIndex = 10;
        this.LabelRepeatable.Text = "Repeatable:";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(15, 109);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(63, 15);
        this.label1.TabIndex = 8;
        this.label1.Text = "Summary:";
        // 
        // TextSummary
        // 
        this.TextSummary.Location = new System.Drawing.Point(15, 127);
        this.TextSummary.Multiline = true;
        this.TextSummary.Name = "TextSummary";
        this.TextSummary.Size = new System.Drawing.Size(230, 142);
        this.TextSummary.TabIndex = 7;
        this.TextSummary.TextChanged += new System.EventHandler(this.TextSummary_TextChanged);
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
        this.LabelName.Size = new System.Drawing.Size(49, 15);
        this.LabelName.TabIndex = 2;
        this.LabelName.Text = "Title:";
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
        this.tabPage2.Controls.Add(this.groupBox1);
        this.tabPage2.Location = new System.Drawing.Point(4, 24);
        this.tabPage2.Name = "tabPage2";
        this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage2.Size = new System.Drawing.Size(288, 599);
        this.tabPage2.TabIndex = 1;
        this.tabPage2.Text = "Steps";
        this.tabPage2.UseVisualStyleBackColor = true;
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.TextRequirementY);
        this.groupBox1.Controls.Add(this.label12);
        this.groupBox1.Controls.Add(this.TextRequirementX);
        this.groupBox1.Controls.Add(this.label11);
        this.groupBox1.Controls.Add(this.TextRequirementCount);
        this.groupBox1.Controls.Add(this.label10);
        this.groupBox1.Controls.Add(this.TextRequirementEntityId);
        this.groupBox1.Controls.Add(this.label9);
        this.groupBox1.Controls.Add(this.ComboActionType);
        this.groupBox1.Controls.Add(this.label8);
        this.groupBox1.Controls.Add(this.label6);
        this.groupBox1.Controls.Add(this.TextStepSummary);
        this.groupBox1.Controls.Add(this.TextStepTitle);
        this.groupBox1.Controls.Add(this.label7);
        this.groupBox1.Controls.Add(this.LabelStepIndex);
        this.groupBox1.Controls.Add(this.ScrollStepIndex);
        this.groupBox1.Controls.Add(this.ButtonRemoveStep);
        this.groupBox1.Controls.Add(this.ButtonAddStep);
        this.groupBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.groupBox1.Location = new System.Drawing.Point(12, 8);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(263, 578);
        this.groupBox1.TabIndex = 5;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Step";
        // 
        // TextRequirementY
        // 
        this.TextRequirementY.Location = new System.Drawing.Point(15, 495);
        this.TextRequirementY.Name = "TextRequirementY";
        this.TextRequirementY.Size = new System.Drawing.Size(230, 23);
        this.TextRequirementY.TabIndex = 23;
        this.TextRequirementY.TextChanged += new System.EventHandler(this.TextRequirementY_TextChanged);
        // 
        // label12
        // 
        this.label12.AutoSize = true;
        this.label12.Location = new System.Drawing.Point(15, 477);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(21, 15);
        this.label12.TabIndex = 22;
        this.label12.Text = "Y:";
        // 
        // TextRequirementX
        // 
        this.TextRequirementX.Location = new System.Drawing.Point(16, 451);
        this.TextRequirementX.Name = "TextRequirementX";
        this.TextRequirementX.Size = new System.Drawing.Size(230, 23);
        this.TextRequirementX.TabIndex = 21;
        this.TextRequirementX.TextChanged += new System.EventHandler(this.TextRequirementX_TextChanged);
        // 
        // label11
        // 
        this.label11.AutoSize = true;
        this.label11.Location = new System.Drawing.Point(16, 433);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(21, 15);
        this.label11.TabIndex = 20;
        this.label11.Text = "X:";
        // 
        // TextRequirementCount
        // 
        this.TextRequirementCount.Location = new System.Drawing.Point(15, 405);
        this.TextRequirementCount.Name = "TextRequirementCount";
        this.TextRequirementCount.Size = new System.Drawing.Size(230, 23);
        this.TextRequirementCount.TabIndex = 19;
        this.TextRequirementCount.TextChanged += new System.EventHandler(this.TextRequirementCount_TextChanged);
        // 
        // label10
        // 
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(15, 387);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(49, 15);
        this.label10.TabIndex = 18;
        this.label10.Text = "Value:";
        // 
        // TextRequirementEntityId
        // 
        this.TextRequirementEntityId.Location = new System.Drawing.Point(16, 361);
        this.TextRequirementEntityId.Name = "TextRequirementEntityId";
        this.TextRequirementEntityId.Size = new System.Drawing.Size(230, 23);
        this.TextRequirementEntityId.TabIndex = 17;
        this.TextRequirementEntityId.TextChanged += new System.EventHandler(this.TextRequirementEntityId_TextChanged);
        // 
        // label9
        // 
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(16, 343);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(77, 15);
        this.label9.TabIndex = 16;
        this.label9.Text = "Entity Id:";
        // 
        // ComboActionType
        // 
        this.ComboActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboActionType.FormattingEnabled = true;
        this.ComboActionType.Location = new System.Drawing.Point(16, 317);
        this.ComboActionType.Name = "ComboActionType";
        this.ComboActionType.Size = new System.Drawing.Size(230, 23);
        this.ComboActionType.TabIndex = 15;
        this.ComboActionType.SelectedIndexChanged += new System.EventHandler(this.ComboActionType_SelectedIndexChanged);
        // 
        // label8
        // 
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(16, 300);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(91, 15);
        this.label8.TabIndex = 14;
        this.label8.Text = "Action Type:";
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(16, 137);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(63, 15);
        this.label6.TabIndex = 12;
        this.label6.Text = "Summary:";
        // 
        // TextStepSummary
        // 
        this.TextStepSummary.Location = new System.Drawing.Point(16, 155);
        this.TextStepSummary.Multiline = true;
        this.TextStepSummary.Name = "TextStepSummary";
        this.TextStepSummary.Size = new System.Drawing.Size(230, 142);
        this.TextStepSummary.TabIndex = 11;
        this.TextStepSummary.TextChanged += new System.EventHandler(this.TextStepSummary_TextChanged);
        // 
        // TextStepTitle
        // 
        this.TextStepTitle.Location = new System.Drawing.Point(16, 111);
        this.TextStepTitle.Name = "TextStepTitle";
        this.TextStepTitle.Size = new System.Drawing.Size(230, 23);
        this.TextStepTitle.TabIndex = 10;
        this.TextStepTitle.TextChanged += new System.EventHandler(this.TextStepTitle_TextChanged);
        // 
        // label7
        // 
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(16, 93);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(49, 15);
        this.label7.TabIndex = 9;
        this.label7.Text = "Title:";
        // 
        // LabelStepIndex
        // 
        this.LabelStepIndex.Location = new System.Drawing.Point(16, 51);
        this.LabelStepIndex.Name = "LabelStepIndex";
        this.LabelStepIndex.Size = new System.Drawing.Size(229, 15);
        this.LabelStepIndex.TabIndex = 7;
        this.LabelStepIndex.Text = "Step Index: 0/0";
        this.LabelStepIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // ScrollStepIndex
        // 
        this.ScrollStepIndex.Enabled = false;
        this.ScrollStepIndex.LargeChange = 1;
        this.ScrollStepIndex.Location = new System.Drawing.Point(16, 68);
        this.ScrollStepIndex.Maximum = 0;
        this.ScrollStepIndex.Name = "ScrollStepIndex";
        this.ScrollStepIndex.Size = new System.Drawing.Size(229, 19);
        this.ScrollStepIndex.TabIndex = 8;
        this.ScrollStepIndex.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollStepIndex_Scroll);
        // 
        // ButtonRemoveStep
        // 
        this.ButtonRemoveStep.Location = new System.Drawing.Point(147, 22);
        this.ButtonRemoveStep.Name = "ButtonRemoveStep";
        this.ButtonRemoveStep.Size = new System.Drawing.Size(75, 23);
        this.ButtonRemoveStep.TabIndex = 1;
        this.ButtonRemoveStep.Text = "Remove";
        this.ButtonRemoveStep.UseVisualStyleBackColor = true;
        this.ButtonRemoveStep.Click += new System.EventHandler(this.ButtonRemoveStep_Click);
        // 
        // ButtonAddStep
        // 
        this.ButtonAddStep.Location = new System.Drawing.Point(46, 22);
        this.ButtonAddStep.Name = "ButtonAddStep";
        this.ButtonAddStep.Size = new System.Drawing.Size(75, 23);
        this.ButtonAddStep.TabIndex = 0;
        this.ButtonAddStep.Text = "Add";
        this.ButtonAddStep.UseVisualStyleBackColor = true;
        this.ButtonAddStep.Click += new System.EventHandler(this.ButtonAddStep_Click);
        // 
        // tabPage3
        // 
        this.tabPage3.Controls.Add(this.groupBox2);
        this.tabPage3.Location = new System.Drawing.Point(4, 24);
        this.tabPage3.Name = "tabPage3";
        this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage3.Size = new System.Drawing.Size(288, 599);
        this.tabPage3.TabIndex = 2;
        this.tabPage3.Text = "Reward";
        this.tabPage3.UseVisualStyleBackColor = true;
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.ComboRewardBound);
        this.groupBox2.Controls.Add(this.label20);
        this.groupBox2.Controls.Add(this.TextRewardUpgradeId);
        this.groupBox2.Controls.Add(this.label14);
        this.groupBox2.Controls.Add(this.TextRewardAttributeId);
        this.groupBox2.Controls.Add(this.label15);
        this.groupBox2.Controls.Add(this.TextRewardLevel);
        this.groupBox2.Controls.Add(this.label16);
        this.groupBox2.Controls.Add(this.TextRewardValue);
        this.groupBox2.Controls.Add(this.label17);
        this.groupBox2.Controls.Add(this.TextRewardId);
        this.groupBox2.Controls.Add(this.label18);
        this.groupBox2.Controls.Add(this.ComboRewardType);
        this.groupBox2.Controls.Add(this.label19);
        this.groupBox2.Controls.Add(this.LabelRewardIndex);
        this.groupBox2.Controls.Add(this.ScrollRewardIndex);
        this.groupBox2.Controls.Add(this.ButtonRemoveReward);
        this.groupBox2.Controls.Add(this.ButtonAddReward);
        this.groupBox2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.groupBox2.Location = new System.Drawing.Point(12, 8);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(263, 578);
        this.groupBox2.TabIndex = 6;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Step";
        // 
        // ComboRewardBound
        // 
        this.ComboRewardBound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboRewardBound.FormattingEnabled = true;
        this.ComboRewardBound.Items.AddRange(new object[] {
            "False",
            "True"});
        this.ComboRewardBound.Location = new System.Drawing.Point(16, 381);
        this.ComboRewardBound.Name = "ComboRewardBound";
        this.ComboRewardBound.Size = new System.Drawing.Size(230, 23);
        this.ComboRewardBound.TabIndex = 27;
        this.ComboRewardBound.SelectedIndexChanged += new System.EventHandler(this.ComboRewardBound_SelectedIndexChanged);
        // 
        // label20
        // 
        this.label20.AutoSize = true;
        this.label20.Location = new System.Drawing.Point(16, 364);
        this.label20.Name = "label20";
        this.label20.Size = new System.Drawing.Size(49, 15);
        this.label20.TabIndex = 26;
        this.label20.Text = "Bound:";
        // 
        // TextRewardUpgradeId
        // 
        this.TextRewardUpgradeId.Location = new System.Drawing.Point(15, 337);
        this.TextRewardUpgradeId.Name = "TextRewardUpgradeId";
        this.TextRewardUpgradeId.Size = new System.Drawing.Size(230, 23);
        this.TextRewardUpgradeId.TabIndex = 25;
        this.TextRewardUpgradeId.TextChanged += new System.EventHandler(this.TextRewardUpgradeId_TextChanged);
        // 
        // label14
        // 
        this.label14.AutoSize = true;
        this.label14.Location = new System.Drawing.Point(15, 319);
        this.label14.Name = "label14";
        this.label14.Size = new System.Drawing.Size(84, 15);
        this.label14.TabIndex = 24;
        this.label14.Text = "Upgrade Id:";
        // 
        // TextRewardAttributeId
        // 
        this.TextRewardAttributeId.Location = new System.Drawing.Point(16, 288);
        this.TextRewardAttributeId.Name = "TextRewardAttributeId";
        this.TextRewardAttributeId.Size = new System.Drawing.Size(230, 23);
        this.TextRewardAttributeId.TabIndex = 23;
        this.TextRewardAttributeId.TextChanged += new System.EventHandler(this.TextRewardAttributeId_TextChanged);
        // 
        // label15
        // 
        this.label15.AutoSize = true;
        this.label15.Location = new System.Drawing.Point(16, 270);
        this.label15.Name = "label15";
        this.label15.Size = new System.Drawing.Size(98, 15);
        this.label15.TabIndex = 22;
        this.label15.Text = "Attribute Id:";
        // 
        // TextRewardLevel
        // 
        this.TextRewardLevel.Location = new System.Drawing.Point(16, 246);
        this.TextRewardLevel.Name = "TextRewardLevel";
        this.TextRewardLevel.Size = new System.Drawing.Size(230, 23);
        this.TextRewardLevel.TabIndex = 21;
        this.TextRewardLevel.TextChanged += new System.EventHandler(this.TextRewardLevel_TextChanged);
        // 
        // label16
        // 
        this.label16.AutoSize = true;
        this.label16.Location = new System.Drawing.Point(16, 228);
        this.label16.Name = "label16";
        this.label16.Size = new System.Drawing.Size(49, 15);
        this.label16.TabIndex = 20;
        this.label16.Text = "Level:";
        // 
        // TextRewardValue
        // 
        this.TextRewardValue.Location = new System.Drawing.Point(15, 200);
        this.TextRewardValue.Name = "TextRewardValue";
        this.TextRewardValue.Size = new System.Drawing.Size(230, 23);
        this.TextRewardValue.TabIndex = 19;
        this.TextRewardValue.TextChanged += new System.EventHandler(this.TextRewardValue_TextChanged);
        // 
        // label17
        // 
        this.label17.AutoSize = true;
        this.label17.Location = new System.Drawing.Point(15, 182);
        this.label17.Name = "label17";
        this.label17.Size = new System.Drawing.Size(49, 15);
        this.label17.TabIndex = 18;
        this.label17.Text = "Value:";
        // 
        // TextRewardId
        // 
        this.TextRewardId.Location = new System.Drawing.Point(16, 156);
        this.TextRewardId.Name = "TextRewardId";
        this.TextRewardId.Size = new System.Drawing.Size(230, 23);
        this.TextRewardId.TabIndex = 17;
        this.TextRewardId.TextChanged += new System.EventHandler(this.TextRewardId_TextChanged);
        // 
        // label18
        // 
        this.label18.AutoSize = true;
        this.label18.Location = new System.Drawing.Point(16, 138);
        this.label18.Name = "label18";
        this.label18.Size = new System.Drawing.Size(28, 15);
        this.label18.TabIndex = 16;
        this.label18.Text = "Id:";
        // 
        // ComboRewardType
        // 
        this.ComboRewardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboRewardType.FormattingEnabled = true;
        this.ComboRewardType.Location = new System.Drawing.Point(15, 112);
        this.ComboRewardType.Name = "ComboRewardType";
        this.ComboRewardType.Size = new System.Drawing.Size(230, 23);
        this.ComboRewardType.TabIndex = 15;
        this.ComboRewardType.SelectedIndexChanged += new System.EventHandler(this.ComboRewardType_SelectedIndexChanged);
        // 
        // label19
        // 
        this.label19.AutoSize = true;
        this.label19.Location = new System.Drawing.Point(15, 95);
        this.label19.Name = "label19";
        this.label19.Size = new System.Drawing.Size(91, 15);
        this.label19.TabIndex = 14;
        this.label19.Text = "Reward Type:";
        // 
        // LabelRewardIndex
        // 
        this.LabelRewardIndex.Location = new System.Drawing.Point(16, 51);
        this.LabelRewardIndex.Name = "LabelRewardIndex";
        this.LabelRewardIndex.Size = new System.Drawing.Size(229, 15);
        this.LabelRewardIndex.TabIndex = 7;
        this.LabelRewardIndex.Text = "Reward Index: 0/0";
        this.LabelRewardIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // ScrollRewardIndex
        // 
        this.ScrollRewardIndex.Enabled = false;
        this.ScrollRewardIndex.LargeChange = 1;
        this.ScrollRewardIndex.Location = new System.Drawing.Point(16, 68);
        this.ScrollRewardIndex.Maximum = 0;
        this.ScrollRewardIndex.Name = "ScrollRewardIndex";
        this.ScrollRewardIndex.Size = new System.Drawing.Size(229, 19);
        this.ScrollRewardIndex.TabIndex = 8;
        this.ScrollRewardIndex.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollRewardIndex_Scroll);
        // 
        // ButtonRemoveReward
        // 
        this.ButtonRemoveReward.Location = new System.Drawing.Point(147, 22);
        this.ButtonRemoveReward.Name = "ButtonRemoveReward";
        this.ButtonRemoveReward.Size = new System.Drawing.Size(75, 23);
        this.ButtonRemoveReward.TabIndex = 1;
        this.ButtonRemoveReward.Text = "Remove";
        this.ButtonRemoveReward.UseVisualStyleBackColor = true;
        this.ButtonRemoveReward.Click += new System.EventHandler(this.ButtonRemoveReward_Click);
        // 
        // ButtonAddReward
        // 
        this.ButtonAddReward.Location = new System.Drawing.Point(46, 22);
        this.ButtonAddReward.Name = "ButtonAddReward";
        this.ButtonAddReward.Size = new System.Drawing.Size(75, 23);
        this.ButtonAddReward.TabIndex = 0;
        this.ButtonAddReward.Text = "Add";
        this.ButtonAddReward.UseVisualStyleBackColor = true;
        this.ButtonAddReward.Click += new System.EventHandler(this.ButtonAddReward_Click);
        // 
        // FormQuest
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(543, 671);
        this.Controls.Add(this.TabQuest);
        this.Controls.Add(this.GroupList);
        this.Controls.Add(this.MenuStrip);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.Name = "FormQuest";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Quest Editor";
        this.MenuStrip.ResumeLayout(false);
        this.MenuStrip.PerformLayout();
        this.GroupList.ResumeLayout(false);
        this.TabQuest.ResumeLayout(false);
        this.tabPage1.ResumeLayout(false);
        this.GroupData.ResumeLayout(false);
        this.GroupData.PerformLayout();
        this.tabPage2.ResumeLayout(false);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.tabPage3.ResumeLayout(false);
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
    private TabControl TabQuest;
    private TabPage tabPage1;
    private GroupBox GroupData;
    private TextBox TextName;
    private Label LabelName;
    private TextBox TextId;
    private Label LabelId;
    private TabPage tabPage2;
    private TabPage tabPage3;
    private Label label1;
    private TextBox TextSummary;
    private ComboBox ComboRepeatable;
    private Label LabelRepeatable;
    private ComboBox ComboType;
    private Label label2;
    private ComboBox ComboShareable;
    private Label label3;
    private ComboBox ComboSelectableReward;
    private Label label4;
    private TextBox TextSelectableRewardCount;
    private Label label5;
    private GroupBox groupBox1;
    private Button ButtonRemoveStep;
    private Button ButtonAddStep;
    private Label LabelStepIndex;
    private HScrollBar ScrollStepIndex;
    private Label label6;
    private TextBox TextStepSummary;
    private TextBox TextStepTitle;
    private Label label7;
    private ComboBox ComboActionType;
    private Label label8;
    private TextBox TextRequirementY;
    private Label label12;
    private TextBox TextRequirementX;
    private Label label11;
    private TextBox TextRequirementCount;
    private Label label10;
    private TextBox TextRequirementEntityId;
    private Label label9;
    private GroupBox groupBox2;
    private TextBox TextRewardUpgradeId;
    private Label label14;
    private TextBox TextRewardAttributeId;
    private Label label15;
    private TextBox TextRewardLevel;
    private Label label16;
    private TextBox TextRewardValue;
    private Label label17;
    private TextBox TextRewardId;
    private Label label18;
    private ComboBox ComboRewardType;
    private Label label19;
    private Label LabelRewardIndex;
    private HScrollBar ScrollRewardIndex;
    private Button ButtonRemoveReward;
    private Button ButtonAddReward;
    private ComboBox ComboRewardBound;
    private Label label20;
}
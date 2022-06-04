namespace Dragon.Editor.Equipments;

partial class FormEquipment {
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEquipment));
        this.MenuStrip = new System.Windows.Forms.MenuStrip();
        this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
        this.TabEquipment = new System.Windows.Forms.TabControl();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.GroupData = new System.Windows.Forms.GroupBox();
        this.TextDisassembleId = new System.Windows.Forms.TextBox();
        this.label21 = new System.Windows.Forms.Label();
        this.TextCostumeModel = new System.Windows.Forms.TextBox();
        this.label22 = new System.Windows.Forms.Label();
        this.LabelSockets = new System.Windows.Forms.Label();
        this.ScrollSockets = new System.Windows.Forms.HScrollBar();
        this.LabelAttackSpeed = new System.Windows.Forms.Label();
        this.ScrollBaseAttack = new System.Windows.Forms.HScrollBar();
        this.ComboSound = new System.Windows.Forms.ComboBox();
        this.label19 = new System.Windows.Forms.Label();
        this.ComboHandStyle = new System.Windows.Forms.ComboBox();
        this.label17 = new System.Windows.Forms.Label();
        this.ComboProfeciency = new System.Windows.Forms.ComboBox();
        this.label16 = new System.Windows.Forms.Label();
        this.ComboType = new System.Windows.Forms.ComboBox();
        this.LabelCategory = new System.Windows.Forms.Label();
        this.TextName = new System.Windows.Forms.TextBox();
        this.LabelName = new System.Windows.Forms.Label();
        this.TextId = new System.Windows.Forms.TextBox();
        this.LabelId = new System.Windows.Forms.Label();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.GroupSkill = new System.Windows.Forms.GroupBox();
        this.GroupNewSkill = new System.Windows.Forms.GroupBox();
        this.ButtonAddSkill = new System.Windows.Forms.Button();
        this.TextUnlockAtLevel = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.TextSkillLevel = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.TextSkillId = new System.Windows.Forms.TextBox();
        this.label8 = new System.Windows.Forms.Label();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.ListSkill = new System.Windows.Forms.ListBox();
        this.ButtonRemoveSkill = new System.Windows.Forms.Button();
        this.ButtonClearSkill = new System.Windows.Forms.Button();
        this.tabPage3 = new System.Windows.Forms.TabPage();
        this.GroupEquipmentSet = new System.Windows.Forms.GroupBox();
        this.LabelAttribute = new System.Windows.Forms.Label();
        this.TextEquipmentSetId = new System.Windows.Forms.TextBox();
        this.GroupUpgrade = new System.Windows.Forms.GroupBox();
        this.groupBox4 = new System.Windows.Forms.GroupBox();
        this.label4 = new System.Windows.Forms.Label();
        this.TextUpgradeId = new System.Windows.Forms.TextBox();
        this.GroupAttribute = new System.Windows.Forms.GroupBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.label2 = new System.Windows.Forms.Label();
        this.TextAttributeId = new System.Windows.Forms.TextBox();
        this.ScrollChance = new System.Windows.Forms.HScrollBar();
        this.ScrollIndex = new System.Windows.Forms.HScrollBar();
        this.ButtonAttributeDelete = new System.Windows.Forms.Button();
        this.ButtonAttributeAdd = new System.Windows.Forms.Button();
        this.LabelIndex = new System.Windows.Forms.Label();
        this.LabelChance = new System.Windows.Forms.Label();
        this.GroupList = new System.Windows.Forms.GroupBox();
        this.ButtonClear = new System.Windows.Forms.Button();
        this.ButtonDelete = new System.Windows.Forms.Button();
        this.ButtonAdd = new System.Windows.Forms.Button();
        this.ListIndex = new System.Windows.Forms.ListBox();
        this.MenuStrip.SuspendLayout();
        this.TabEquipment.SuspendLayout();
        this.tabPage1.SuspendLayout();
        this.GroupData.SuspendLayout();
        this.tabPage2.SuspendLayout();
        this.GroupSkill.SuspendLayout();
        this.GroupNewSkill.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.tabPage3.SuspendLayout();
        this.GroupEquipmentSet.SuspendLayout();
        this.GroupUpgrade.SuspendLayout();
        this.groupBox4.SuspendLayout();
        this.GroupAttribute.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.GroupList.SuspendLayout();
        this.SuspendLayout();
        // 
        // MenuStrip
        // 
        this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
        this.MenuStrip.Location = new System.Drawing.Point(0, 0);
        this.MenuStrip.Name = "MenuStrip";
        this.MenuStrip.Size = new System.Drawing.Size(547, 24);
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
        // TabEquipment
        // 
        this.TabEquipment.Controls.Add(this.tabPage1);
        this.TabEquipment.Controls.Add(this.tabPage2);
        this.TabEquipment.Controls.Add(this.tabPage3);
        this.TabEquipment.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.TabEquipment.Location = new System.Drawing.Point(236, 28);
        this.TabEquipment.Name = "TabEquipment";
        this.TabEquipment.SelectedIndex = 0;
        this.TabEquipment.Size = new System.Drawing.Size(300, 571);
        this.TabEquipment.TabIndex = 6;
        // 
        // tabPage1
        // 
        this.tabPage1.Controls.Add(this.GroupData);
        this.tabPage1.Location = new System.Drawing.Point(4, 24);
        this.tabPage1.Name = "tabPage1";
        this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage1.Size = new System.Drawing.Size(292, 543);
        this.tabPage1.TabIndex = 0;
        this.tabPage1.Text = "Data";
        this.tabPage1.UseVisualStyleBackColor = true;
        // 
        // GroupData
        // 
        this.GroupData.Controls.Add(this.TextDisassembleId);
        this.GroupData.Controls.Add(this.label21);
        this.GroupData.Controls.Add(this.TextCostumeModel);
        this.GroupData.Controls.Add(this.label22);
        this.GroupData.Controls.Add(this.LabelSockets);
        this.GroupData.Controls.Add(this.ScrollSockets);
        this.GroupData.Controls.Add(this.LabelAttackSpeed);
        this.GroupData.Controls.Add(this.ScrollBaseAttack);
        this.GroupData.Controls.Add(this.ComboSound);
        this.GroupData.Controls.Add(this.label19);
        this.GroupData.Controls.Add(this.ComboHandStyle);
        this.GroupData.Controls.Add(this.label17);
        this.GroupData.Controls.Add(this.ComboProfeciency);
        this.GroupData.Controls.Add(this.label16);
        this.GroupData.Controls.Add(this.ComboType);
        this.GroupData.Controls.Add(this.LabelCategory);
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
        // TextDisassembleId
        // 
        this.TextDisassembleId.Location = new System.Drawing.Point(15, 431);
        this.TextDisassembleId.Name = "TextDisassembleId";
        this.TextDisassembleId.Size = new System.Drawing.Size(230, 23);
        this.TextDisassembleId.TabIndex = 28;
        this.TextDisassembleId.Text = "0";
        this.TextDisassembleId.TextChanged += new System.EventHandler(this.TextDisassembleId_TextChanged);
        // 
        // label21
        // 
        this.label21.AutoSize = true;
        this.label21.Location = new System.Drawing.Point(15, 413);
        this.label21.Name = "label21";
        this.label21.Size = new System.Drawing.Size(112, 15);
        this.label21.TabIndex = 27;
        this.label21.Text = "Disassemble Id:";
        // 
        // TextCostumeModel
        // 
        this.TextCostumeModel.Location = new System.Drawing.Point(15, 388);
        this.TextCostumeModel.Name = "TextCostumeModel";
        this.TextCostumeModel.Size = new System.Drawing.Size(230, 23);
        this.TextCostumeModel.TabIndex = 26;
        this.TextCostumeModel.Text = "0";
        this.TextCostumeModel.TextChanged += new System.EventHandler(this.TextCostumeModel_TextChanged);
        // 
        // label22
        // 
        this.label22.AutoSize = true;
        this.label22.Location = new System.Drawing.Point(15, 370);
        this.label22.Name = "label22";
        this.label22.Size = new System.Drawing.Size(126, 15);
        this.label22.TabIndex = 25;
        this.label22.Text = "Costume Model Id:";
        // 
        // LabelSockets
        // 
        this.LabelSockets.AutoSize = true;
        this.LabelSockets.Location = new System.Drawing.Point(15, 331);
        this.LabelSockets.Name = "LabelSockets";
        this.LabelSockets.Size = new System.Drawing.Size(133, 15);
        this.LabelSockets.TabIndex = 24;
        this.LabelSockets.Text = "Maximum Sockets: 0";
        // 
        // ScrollSockets
        // 
        this.ScrollSockets.LargeChange = 1;
        this.ScrollSockets.Location = new System.Drawing.Point(15, 349);
        this.ScrollSockets.Maximum = 10;
        this.ScrollSockets.Name = "ScrollSockets";
        this.ScrollSockets.Size = new System.Drawing.Size(228, 18);
        this.ScrollSockets.TabIndex = 23;
        this.ScrollSockets.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollSockets_Scroll);
        // 
        // LabelAttackSpeed
        // 
        this.LabelAttackSpeed.AutoSize = true;
        this.LabelAttackSpeed.Location = new System.Drawing.Point(13, 287);
        this.LabelAttackSpeed.Name = "LabelAttackSpeed";
        this.LabelAttackSpeed.Size = new System.Drawing.Size(203, 15);
        this.LabelAttackSpeed.TabIndex = 20;
        this.LabelAttackSpeed.Text = "Weapon Base Attack Speed: 0%";
        // 
        // ScrollBaseAttack
        // 
        this.ScrollBaseAttack.LargeChange = 1;
        this.ScrollBaseAttack.Location = new System.Drawing.Point(15, 308);
        this.ScrollBaseAttack.Maximum = 3000;
        this.ScrollBaseAttack.Name = "ScrollBaseAttack";
        this.ScrollBaseAttack.Size = new System.Drawing.Size(229, 18);
        this.ScrollBaseAttack.TabIndex = 19;
        this.ScrollBaseAttack.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBaseAttack_Scroll);
        // 
        // ComboSound
        // 
        this.ComboSound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboSound.FormattingEnabled = true;
        this.ComboSound.Location = new System.Drawing.Point(13, 261);
        this.ComboSound.Name = "ComboSound";
        this.ComboSound.Size = new System.Drawing.Size(230, 23);
        this.ComboSound.TabIndex = 18;
        this.ComboSound.SelectedIndexChanged += new System.EventHandler(this.ComboSound_SelectedIndexChanged);
        // 
        // label19
        // 
        this.label19.AutoSize = true;
        this.label19.Location = new System.Drawing.Point(14, 243);
        this.label19.Name = "label19";
        this.label19.Size = new System.Drawing.Size(49, 15);
        this.label19.TabIndex = 17;
        this.label19.Text = "Sound:";
        // 
        // ComboHandStyle
        // 
        this.ComboHandStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboHandStyle.FormattingEnabled = true;
        this.ComboHandStyle.Location = new System.Drawing.Point(15, 214);
        this.ComboHandStyle.Name = "ComboHandStyle";
        this.ComboHandStyle.Size = new System.Drawing.Size(230, 23);
        this.ComboHandStyle.TabIndex = 14;
        this.ComboHandStyle.SelectedIndexChanged += new System.EventHandler(this.ComboHandStyle_SelectedIndexChanged);
        // 
        // label17
        // 
        this.label17.AutoSize = true;
        this.label17.Location = new System.Drawing.Point(15, 196);
        this.label17.Name = "label17";
        this.label17.Size = new System.Drawing.Size(84, 15);
        this.label17.TabIndex = 13;
        this.label17.Text = "Hand Style:";
        // 
        // ComboProfeciency
        // 
        this.ComboProfeciency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboProfeciency.FormattingEnabled = true;
        this.ComboProfeciency.Location = new System.Drawing.Point(15, 170);
        this.ComboProfeciency.Name = "ComboProfeciency";
        this.ComboProfeciency.Size = new System.Drawing.Size(230, 23);
        this.ComboProfeciency.TabIndex = 12;
        this.ComboProfeciency.SelectedIndexChanged += new System.EventHandler(this.ComboProfeciency_SelectedIndexChanged);
        // 
        // label16
        // 
        this.label16.AutoSize = true;
        this.label16.Location = new System.Drawing.Point(15, 152);
        this.label16.Name = "label16";
        this.label16.Size = new System.Drawing.Size(91, 15);
        this.label16.TabIndex = 11;
        this.label16.Text = "Proficiency:";
        // 
        // ComboType
        // 
        this.ComboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboType.FormattingEnabled = true;
        this.ComboType.Location = new System.Drawing.Point(15, 127);
        this.ComboType.Name = "ComboType";
        this.ComboType.Size = new System.Drawing.Size(230, 23);
        this.ComboType.TabIndex = 9;
        this.ComboType.SelectedIndexChanged += new System.EventHandler(this.ComboType_SelectedIndexChanged);
        // 
        // LabelCategory
        // 
        this.LabelCategory.AutoSize = true;
        this.LabelCategory.Location = new System.Drawing.Point(15, 109);
        this.LabelCategory.Name = "LabelCategory";
        this.LabelCategory.Size = new System.Drawing.Size(42, 15);
        this.LabelCategory.TabIndex = 6;
        this.LabelCategory.Text = "Type:";
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
        this.tabPage2.Controls.Add(this.GroupSkill);
        this.tabPage2.Location = new System.Drawing.Point(4, 24);
        this.tabPage2.Name = "tabPage2";
        this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage2.Size = new System.Drawing.Size(292, 543);
        this.tabPage2.TabIndex = 1;
        this.tabPage2.Text = "Skill";
        this.tabPage2.UseVisualStyleBackColor = true;
        // 
        // GroupSkill
        // 
        this.GroupSkill.Controls.Add(this.GroupNewSkill);
        this.GroupSkill.Controls.Add(this.groupBox2);
        this.GroupSkill.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.GroupSkill.Location = new System.Drawing.Point(12, 8);
        this.GroupSkill.Name = "GroupSkill";
        this.GroupSkill.Size = new System.Drawing.Size(263, 522);
        this.GroupSkill.TabIndex = 5;
        this.GroupSkill.TabStop = false;
        this.GroupSkill.Text = "Skill";
        // 
        // GroupNewSkill
        // 
        this.GroupNewSkill.Controls.Add(this.ButtonAddSkill);
        this.GroupNewSkill.Controls.Add(this.TextUnlockAtLevel);
        this.GroupNewSkill.Controls.Add(this.label3);
        this.GroupNewSkill.Controls.Add(this.TextSkillLevel);
        this.GroupNewSkill.Controls.Add(this.label6);
        this.GroupNewSkill.Controls.Add(this.TextSkillId);
        this.GroupNewSkill.Controls.Add(this.label8);
        this.GroupNewSkill.Location = new System.Drawing.Point(16, 302);
        this.GroupNewSkill.Name = "GroupNewSkill";
        this.GroupNewSkill.Size = new System.Drawing.Size(233, 202);
        this.GroupNewSkill.TabIndex = 22;
        this.GroupNewSkill.TabStop = false;
        this.GroupNewSkill.Text = "Add New Skill";
        // 
        // ButtonAddSkill
        // 
        this.ButtonAddSkill.Location = new System.Drawing.Point(17, 162);
        this.ButtonAddSkill.Name = "ButtonAddSkill";
        this.ButtonAddSkill.Size = new System.Drawing.Size(198, 23);
        this.ButtonAddSkill.TabIndex = 27;
        this.ButtonAddSkill.Text = "Add";
        this.ButtonAddSkill.UseVisualStyleBackColor = true;
        this.ButtonAddSkill.Click += new System.EventHandler(this.ButtonAddSkill_Click);
        // 
        // TextUnlockAtLevel
        // 
        this.TextUnlockAtLevel.Location = new System.Drawing.Point(17, 133);
        this.TextUnlockAtLevel.Name = "TextUnlockAtLevel";
        this.TextUnlockAtLevel.Size = new System.Drawing.Size(198, 23);
        this.TextUnlockAtLevel.TabIndex = 26;
        this.TextUnlockAtLevel.Text = "0";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(17, 115);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(126, 15);
        this.label3.TabIndex = 25;
        this.label3.Text = "Unlock At Level: ";
        // 
        // TextSkillLevel
        // 
        this.TextSkillLevel.Location = new System.Drawing.Point(17, 87);
        this.TextSkillLevel.Name = "TextSkillLevel";
        this.TextSkillLevel.Size = new System.Drawing.Size(198, 23);
        this.TextSkillLevel.TabIndex = 24;
        this.TextSkillLevel.Text = "0";
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(17, 69);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(49, 15);
        this.label6.TabIndex = 22;
        this.label6.Text = "Level:";
        // 
        // TextSkillId
        // 
        this.TextSkillId.Location = new System.Drawing.Point(17, 40);
        this.TextSkillId.Name = "TextSkillId";
        this.TextSkillId.Size = new System.Drawing.Size(198, 23);
        this.TextSkillId.TabIndex = 23;
        this.TextSkillId.Text = "0";
        // 
        // label8
        // 
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(17, 22);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(28, 15);
        this.label8.TabIndex = 21;
        this.label8.Text = "Id:";
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.ListSkill);
        this.groupBox2.Controls.Add(this.ButtonRemoveSkill);
        this.groupBox2.Controls.Add(this.ButtonClearSkill);
        this.groupBox2.Location = new System.Drawing.Point(15, 20);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(233, 274);
        this.groupBox2.TabIndex = 21;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "List";
        // 
        // ListSkill
        // 
        this.ListSkill.FormattingEnabled = true;
        this.ListSkill.HorizontalScrollbar = true;
        this.ListSkill.ItemHeight = 15;
        this.ListSkill.Location = new System.Drawing.Point(18, 22);
        this.ListSkill.Name = "ListSkill";
        this.ListSkill.Size = new System.Drawing.Size(198, 199);
        this.ListSkill.TabIndex = 15;
        // 
        // ButtonRemoveSkill
        // 
        this.ButtonRemoveSkill.Location = new System.Drawing.Point(18, 236);
        this.ButtonRemoveSkill.Name = "ButtonRemoveSkill";
        this.ButtonRemoveSkill.Size = new System.Drawing.Size(90, 23);
        this.ButtonRemoveSkill.TabIndex = 17;
        this.ButtonRemoveSkill.Text = "Remove";
        this.ButtonRemoveSkill.UseVisualStyleBackColor = true;
        this.ButtonRemoveSkill.Click += new System.EventHandler(this.ButtonRemoveSkill_Click);
        // 
        // ButtonClearSkill
        // 
        this.ButtonClearSkill.Location = new System.Drawing.Point(126, 236);
        this.ButtonClearSkill.Name = "ButtonClearSkill";
        this.ButtonClearSkill.Size = new System.Drawing.Size(90, 23);
        this.ButtonClearSkill.TabIndex = 16;
        this.ButtonClearSkill.Text = "Clear";
        this.ButtonClearSkill.UseVisualStyleBackColor = true;
        this.ButtonClearSkill.Click += new System.EventHandler(this.ButtonClearSkill_Click);
        // 
        // tabPage3
        // 
        this.tabPage3.Controls.Add(this.GroupEquipmentSet);
        this.tabPage3.Controls.Add(this.GroupUpgrade);
        this.tabPage3.Controls.Add(this.GroupAttribute);
        this.tabPage3.Location = new System.Drawing.Point(4, 24);
        this.tabPage3.Name = "tabPage3";
        this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage3.Size = new System.Drawing.Size(292, 543);
        this.tabPage3.TabIndex = 2;
        this.tabPage3.Text = "Attribute";
        this.tabPage3.UseVisualStyleBackColor = true;
        // 
        // GroupEquipmentSet
        // 
        this.GroupEquipmentSet.Controls.Add(this.LabelAttribute);
        this.GroupEquipmentSet.Controls.Add(this.TextEquipmentSetId);
        this.GroupEquipmentSet.Location = new System.Drawing.Point(12, 274);
        this.GroupEquipmentSet.Name = "GroupEquipmentSet";
        this.GroupEquipmentSet.Size = new System.Drawing.Size(263, 106);
        this.GroupEquipmentSet.TabIndex = 10;
        this.GroupEquipmentSet.TabStop = false;
        this.GroupEquipmentSet.Text = "Equipment Attribute Set";
        // 
        // LabelAttribute
        // 
        this.LabelAttribute.Dock = System.Windows.Forms.DockStyle.Top;
        this.LabelAttribute.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.LabelAttribute.Location = new System.Drawing.Point(3, 19);
        this.LabelAttribute.Name = "LabelAttribute";
        this.LabelAttribute.Size = new System.Drawing.Size(257, 39);
        this.LabelAttribute.TabIndex = 11;
        this.LabelAttribute.Text = "Unselected";
        this.LabelAttribute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // TextEquipmentSetId
        // 
        this.TextEquipmentSetId.Location = new System.Drawing.Point(17, 61);
        this.TextEquipmentSetId.Name = "TextEquipmentSetId";
        this.TextEquipmentSetId.Size = new System.Drawing.Size(227, 23);
        this.TextEquipmentSetId.TabIndex = 10;
        this.TextEquipmentSetId.Text = "0";
        this.TextEquipmentSetId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.TextEquipmentSetId.TextChanged += new System.EventHandler(this.TextEquipmentSetId_TextChanged);
        // 
        // GroupUpgrade
        // 
        this.GroupUpgrade.Controls.Add(this.groupBox4);
        this.GroupUpgrade.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.GroupUpgrade.Location = new System.Drawing.Point(12, 386);
        this.GroupUpgrade.Name = "GroupUpgrade";
        this.GroupUpgrade.Size = new System.Drawing.Size(263, 119);
        this.GroupUpgrade.TabIndex = 9;
        this.GroupUpgrade.TabStop = false;
        this.GroupUpgrade.Text = "Attribute Upgrade";
        // 
        // groupBox4
        // 
        this.groupBox4.Controls.Add(this.label4);
        this.groupBox4.Controls.Add(this.TextUpgradeId);
        this.groupBox4.Location = new System.Drawing.Point(17, 22);
        this.groupBox4.Name = "groupBox4";
        this.groupBox4.Size = new System.Drawing.Size(230, 77);
        this.groupBox4.TabIndex = 21;
        this.groupBox4.TabStop = false;
        this.groupBox4.Text = "Upgrade";
        // 
        // label4
        // 
        this.label4.Dock = System.Windows.Forms.DockStyle.Top;
        this.label4.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.label4.Location = new System.Drawing.Point(3, 19);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(224, 23);
        this.label4.TabIndex = 11;
        this.label4.Text = "Unselected";
        this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // TextUpgradeId
        // 
        this.TextUpgradeId.Location = new System.Drawing.Point(9, 45);
        this.TextUpgradeId.Name = "TextUpgradeId";
        this.TextUpgradeId.Size = new System.Drawing.Size(215, 23);
        this.TextUpgradeId.TabIndex = 10;
        this.TextUpgradeId.Text = "0";
        this.TextUpgradeId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.TextUpgradeId.TextChanged += new System.EventHandler(this.TextUpgradeId_TextChanged);
        // 
        // GroupAttribute
        // 
        this.GroupAttribute.Controls.Add(this.groupBox1);
        this.GroupAttribute.Controls.Add(this.ScrollChance);
        this.GroupAttribute.Controls.Add(this.ScrollIndex);
        this.GroupAttribute.Controls.Add(this.ButtonAttributeDelete);
        this.GroupAttribute.Controls.Add(this.ButtonAttributeAdd);
        this.GroupAttribute.Controls.Add(this.LabelIndex);
        this.GroupAttribute.Controls.Add(this.LabelChance);
        this.GroupAttribute.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.GroupAttribute.Location = new System.Drawing.Point(12, 8);
        this.GroupAttribute.Name = "GroupAttribute";
        this.GroupAttribute.Size = new System.Drawing.Size(263, 260);
        this.GroupAttribute.TabIndex = 8;
        this.GroupAttribute.TabStop = false;
        this.GroupAttribute.Text = "Random Attribute";
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.TextAttributeId);
        this.groupBox1.Location = new System.Drawing.Point(17, 103);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(230, 101);
        this.groupBox1.TabIndex = 20;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Attribute:";
        // 
        // label2
        // 
        this.label2.Dock = System.Windows.Forms.DockStyle.Top;
        this.label2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.label2.Location = new System.Drawing.Point(3, 19);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(224, 44);
        this.label2.TabIndex = 11;
        this.label2.Text = "Unselected";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // TextAttributeId
        // 
        this.TextAttributeId.Location = new System.Drawing.Point(6, 66);
        this.TextAttributeId.Name = "TextAttributeId";
        this.TextAttributeId.Size = new System.Drawing.Size(215, 23);
        this.TextAttributeId.TabIndex = 10;
        this.TextAttributeId.Text = "0";
        this.TextAttributeId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.TextAttributeId.TextChanged += new System.EventHandler(this.TextAttributeId_TextChanged);
        // 
        // ScrollChance
        // 
        this.ScrollChance.LargeChange = 1;
        this.ScrollChance.Location = new System.Drawing.Point(17, 230);
        this.ScrollChance.Name = "ScrollChance";
        this.ScrollChance.Size = new System.Drawing.Size(230, 18);
        this.ScrollChance.TabIndex = 19;
        this.ScrollChance.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollChance_Scroll);
        // 
        // ScrollIndex
        // 
        this.ScrollIndex.LargeChange = 1;
        this.ScrollIndex.Location = new System.Drawing.Point(17, 82);
        this.ScrollIndex.Maximum = 0;
        this.ScrollIndex.Name = "ScrollIndex";
        this.ScrollIndex.Size = new System.Drawing.Size(230, 18);
        this.ScrollIndex.TabIndex = 18;
        this.ScrollIndex.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollIndex_Scroll);
        // 
        // ButtonAttributeDelete
        // 
        this.ButtonAttributeDelete.Location = new System.Drawing.Point(132, 22);
        this.ButtonAttributeDelete.Name = "ButtonAttributeDelete";
        this.ButtonAttributeDelete.Size = new System.Drawing.Size(75, 23);
        this.ButtonAttributeDelete.TabIndex = 17;
        this.ButtonAttributeDelete.Text = "Delete";
        this.ButtonAttributeDelete.UseVisualStyleBackColor = true;
        this.ButtonAttributeDelete.Click += new System.EventHandler(this.ButtonAttributeDelete_Click);
        // 
        // ButtonAttributeAdd
        // 
        this.ButtonAttributeAdd.Location = new System.Drawing.Point(51, 22);
        this.ButtonAttributeAdd.Name = "ButtonAttributeAdd";
        this.ButtonAttributeAdd.Size = new System.Drawing.Size(75, 23);
        this.ButtonAttributeAdd.TabIndex = 16;
        this.ButtonAttributeAdd.Text = "Add";
        this.ButtonAttributeAdd.UseVisualStyleBackColor = true;
        this.ButtonAttributeAdd.Click += new System.EventHandler(this.ButtonAttributeAdd_Click);
        // 
        // LabelIndex
        // 
        this.LabelIndex.Location = new System.Drawing.Point(17, 57);
        this.LabelIndex.Name = "LabelIndex";
        this.LabelIndex.Size = new System.Drawing.Size(230, 15);
        this.LabelIndex.TabIndex = 15;
        this.LabelIndex.Text = "Index: 0 / 0";
        this.LabelIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // LabelChance
        // 
        this.LabelChance.AutoSize = true;
        this.LabelChance.Location = new System.Drawing.Point(17, 210);
        this.LabelChance.Name = "LabelChance";
        this.LabelChance.Size = new System.Drawing.Size(77, 15);
        this.LabelChance.TabIndex = 2;
        this.LabelChance.Text = "Chance: 0%";
        // 
        // GroupList
        // 
        this.GroupList.Controls.Add(this.ButtonClear);
        this.GroupList.Controls.Add(this.ButtonDelete);
        this.GroupList.Controls.Add(this.ButtonAdd);
        this.GroupList.Controls.Add(this.ListIndex);
        this.GroupList.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.GroupList.Location = new System.Drawing.Point(12, 23);
        this.GroupList.Name = "GroupList";
        this.GroupList.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
        this.GroupList.Size = new System.Drawing.Size(218, 576);
        this.GroupList.TabIndex = 5;
        this.GroupList.TabStop = false;
        this.GroupList.Text = "Equipments";
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
        // FormEquipment
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(547, 611);
        this.Controls.Add(this.TabEquipment);
        this.Controls.Add(this.GroupList);
        this.Controls.Add(this.MenuStrip);
        this.DoubleBuffered = true;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.Name = "FormEquipment";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Equipment Editor";
        this.MenuStrip.ResumeLayout(false);
        this.MenuStrip.PerformLayout();
        this.TabEquipment.ResumeLayout(false);
        this.tabPage1.ResumeLayout(false);
        this.GroupData.ResumeLayout(false);
        this.GroupData.PerformLayout();
        this.tabPage2.ResumeLayout(false);
        this.GroupSkill.ResumeLayout(false);
        this.GroupNewSkill.ResumeLayout(false);
        this.GroupNewSkill.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.tabPage3.ResumeLayout(false);
        this.GroupEquipmentSet.ResumeLayout(false);
        this.GroupEquipmentSet.PerformLayout();
        this.GroupUpgrade.ResumeLayout(false);
        this.groupBox4.ResumeLayout(false);
        this.groupBox4.PerformLayout();
        this.GroupAttribute.ResumeLayout(false);
        this.GroupAttribute.PerformLayout();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.GroupList.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private MenuStrip MenuStrip;
    private ToolStripMenuItem FileMenuItem;
    private ToolStripMenuItem MenuSave;
    private ToolStripMenuItem MenuExit;
    private TabControl TabEquipment;
    private TabPage tabPage1;
    private GroupBox GroupData;
    private ComboBox ComboType;
    private Label LabelCategory;
    private TextBox TextName;
    private Label LabelName;
    private TextBox TextId;
    private Label LabelId;
    private TabPage tabPage2;
    private GroupBox GroupSkill;
    private TabPage tabPage3;
    private GroupBox GroupList;
    private Button ButtonClear;
    private Button ButtonDelete;
    private Button ButtonAdd;
    private ListBox ListIndex;
    private ComboBox ComboHandStyle;
    private Label label17;
    private ComboBox ComboProfeciency;
    private Label label16;
    private ComboBox ComboSound;
    private Label label19;
    private Label LabelAttackSpeed;
    private HScrollBar ScrollBaseAttack;
    private Label LabelSockets;
    private HScrollBar ScrollSockets;
    private TextBox TextCostumeModel;
    private Label label22;
    private TextBox TextDisassembleId;
    private Label label21;
    private Button ButtonRemoveSkill;
    private Button ButtonClearSkill;
    private ListBox ListSkill;
    private GroupBox groupBox2;
    private GroupBox GroupNewSkill;
    private Button ButtonAddSkill;
    private TextBox TextUnlockAtLevel;
    private Label label3;
    private TextBox TextSkillLevel;
    private Label label6;
    private TextBox TextSkillId;
    private Label label8;
    private GroupBox GroupAttribute;
    private GroupBox groupBox1;
    private Label label2;
    private TextBox TextAttributeId;
    private HScrollBar ScrollChance;
    private HScrollBar ScrollIndex;
    private Button ButtonAttributeDelete;
    private Button ButtonAttributeAdd;
    private Label LabelIndex;
    private Label LabelChance;
    private GroupBox GroupUpgrade;
    private GroupBox GroupEquipmentSet;
    private Label LabelAttribute;
    private TextBox TextEquipmentSetId;
    private GroupBox groupBox4;
    private Label label4;
    private TextBox TextUpgradeId;
}
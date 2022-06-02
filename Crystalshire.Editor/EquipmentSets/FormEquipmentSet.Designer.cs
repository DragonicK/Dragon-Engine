﻿namespace Crystalshire.Editor.EquipmentSets;

partial class FormEquipmentSet {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEquipmentSet));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupList = new System.Windows.Forms.GroupBox();
            this.ButtonClear = new System.Windows.Forms.Button();
            this.ButtonDelete = new System.Windows.Forms.Button();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ListIndex = new System.Windows.Forms.ListBox();
            this.TabEquipmentSet = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ButtonClearSet = new System.Windows.Forms.Button();
            this.ButtonDeleteSet = new System.Windows.Forms.Button();
            this.ListSet = new System.Windows.Forms.ListBox();
            this.GroupData = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ButtonAddSet = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextSkillId = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LabelAttribute = new System.Windows.Forms.Label();
            this.TextAttributeId = new System.Windows.Forms.TextBox();
            this.ComboCategory = new System.Windows.Forms.ComboBox();
            this.LabelCategory = new System.Windows.Forms.Label();
            this.TextDescription = new System.Windows.Forms.TextBox();
            this.LabelDescription = new System.Windows.Forms.Label();
            this.TextName = new System.Windows.Forms.TextBox();
            this.LabelName = new System.Windows.Forms.Label();
            this.TextId = new System.Windows.Forms.TextBox();
            this.LabelId = new System.Windows.Forms.Label();
            this.MenuStrip.SuspendLayout();
            this.GroupList.SuspendLayout();
            this.TabEquipmentSet.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GroupData.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(815, 24);
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
            this.GroupList.Text = "Equipment Set";
            // 
            // ButtonClear
            // 
            this.ButtonClear.ForeColor = System.Drawing.Color.Black;
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
            this.ButtonDelete.ForeColor = System.Drawing.Color.Black;
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
            this.ButtonAdd.ForeColor = System.Drawing.Color.Black;
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
            // TabEquipmentSet
            // 
            this.TabEquipmentSet.Controls.Add(this.tabPage1);
            this.TabEquipmentSet.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TabEquipmentSet.Location = new System.Drawing.Point(236, 32);
            this.TabEquipmentSet.Name = "TabEquipmentSet";
            this.TabEquipmentSet.SelectedIndex = 0;
            this.TabEquipmentSet.Size = new System.Drawing.Size(569, 571);
            this.TabEquipmentSet.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.GroupData);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(561, 543);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ButtonClearSet);
            this.groupBox1.Controls.Add(this.ButtonDeleteSet);
            this.groupBox1.Controls.Add(this.ListSet);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(285, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 522);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Equipment Set";
            // 
            // ButtonClearSet
            // 
            this.ButtonClearSet.ForeColor = System.Drawing.Color.Black;
            this.ButtonClearSet.Location = new System.Drawing.Point(160, 472);
            this.ButtonClearSet.Name = "ButtonClearSet";
            this.ButtonClearSet.Size = new System.Drawing.Size(90, 23);
            this.ButtonClearSet.TabIndex = 4;
            this.ButtonClearSet.Text = "Clear";
            this.ButtonClearSet.UseVisualStyleBackColor = true;
            this.ButtonClearSet.Click += new System.EventHandler(this.ButtonClearSet_Click);
            // 
            // ButtonDeleteSet
            // 
            this.ButtonDeleteSet.ForeColor = System.Drawing.Color.Black;
            this.ButtonDeleteSet.Location = new System.Drawing.Point(15, 472);
            this.ButtonDeleteSet.Name = "ButtonDeleteSet";
            this.ButtonDeleteSet.Size = new System.Drawing.Size(90, 23);
            this.ButtonDeleteSet.TabIndex = 3;
            this.ButtonDeleteSet.Text = "Delete";
            this.ButtonDeleteSet.UseVisualStyleBackColor = true;
            this.ButtonDeleteSet.Click += new System.EventHandler(this.ButtonDeleteSet_Click);
            // 
            // ListSet
            // 
            this.ListSet.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ListSet.FormattingEnabled = true;
            this.ListSet.HorizontalScrollbar = true;
            this.ListSet.ItemHeight = 15;
            this.ListSet.Location = new System.Drawing.Point(15, 25);
            this.ListSet.Name = "ListSet";
            this.ListSet.Size = new System.Drawing.Size(235, 439);
            this.ListSet.TabIndex = 1;
            // 
            // GroupData
            // 
            this.GroupData.Controls.Add(this.groupBox2);
            this.GroupData.Controls.Add(this.TextDescription);
            this.GroupData.Controls.Add(this.LabelDescription);
            this.GroupData.Controls.Add(this.TextName);
            this.GroupData.Controls.Add(this.LabelName);
            this.GroupData.Controls.Add(this.TextId);
            this.GroupData.Controls.Add(this.LabelId);
            this.GroupData.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GroupData.Location = new System.Drawing.Point(12, 8);
            this.GroupData.Name = "GroupData";
            this.GroupData.Size = new System.Drawing.Size(260, 522);
            this.GroupData.TabIndex = 4;
            this.GroupData.TabStop = false;
            this.GroupData.Text = "Data";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ButtonAddSet);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.ComboCategory);
            this.groupBox2.Controls.Add(this.LabelCategory);
            this.groupBox2.Location = new System.Drawing.Point(15, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 313);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add New Set:";
            // 
            // ButtonAddSet
            // 
            this.ButtonAddSet.ForeColor = System.Drawing.Color.Black;
            this.ButtonAddSet.Location = new System.Drawing.Point(17, 278);
            this.ButtonAddSet.Name = "ButtonAddSet";
            this.ButtonAddSet.Size = new System.Drawing.Size(199, 23);
            this.ButtonAddSet.TabIndex = 13;
            this.ButtonAddSet.Text = "Add";
            this.ButtonAddSet.UseVisualStyleBackColor = true;
            this.ButtonAddSet.Click += new System.EventHandler(this.ButtonAddSet_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.TextSkillId);
            this.groupBox4.Location = new System.Drawing.Point(14, 168);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(202, 95);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Skill:";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 40);
            this.label1.TabIndex = 11;
            this.label1.Text = "Unselected";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextSkillId
            // 
            this.TextSkillId.Location = new System.Drawing.Point(8, 62);
            this.TextSkillId.Name = "TextSkillId";
            this.TextSkillId.Size = new System.Drawing.Size(188, 23);
            this.TextSkillId.TabIndex = 10;
            this.TextSkillId.Text = "0";
            this.TextSkillId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LabelAttribute);
            this.groupBox3.Controls.Add(this.TextAttributeId);
            this.groupBox3.Location = new System.Drawing.Point(14, 71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(202, 95);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Attribute:";
            // 
            // LabelAttribute
            // 
            this.LabelAttribute.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelAttribute.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelAttribute.Location = new System.Drawing.Point(3, 19);
            this.LabelAttribute.Name = "LabelAttribute";
            this.LabelAttribute.Size = new System.Drawing.Size(196, 40);
            this.LabelAttribute.TabIndex = 11;
            this.LabelAttribute.Text = "Unselected";
            this.LabelAttribute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextAttributeId
            // 
            this.TextAttributeId.Location = new System.Drawing.Point(8, 62);
            this.TextAttributeId.Name = "TextAttributeId";
            this.TextAttributeId.Size = new System.Drawing.Size(188, 23);
            this.TextAttributeId.TabIndex = 10;
            this.TextAttributeId.Text = "0";
            this.TextAttributeId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ComboCategory
            // 
            this.ComboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboCategory.FormattingEnabled = true;
            this.ComboCategory.Location = new System.Drawing.Point(14, 42);
            this.ComboCategory.Name = "ComboCategory";
            this.ComboCategory.Size = new System.Drawing.Size(202, 23);
            this.ComboCategory.TabIndex = 9;
            // 
            // LabelCategory
            // 
            this.LabelCategory.AutoSize = true;
            this.LabelCategory.Location = new System.Drawing.Point(12, 24);
            this.LabelCategory.Name = "LabelCategory";
            this.LabelCategory.Size = new System.Drawing.Size(77, 15);
            this.LabelCategory.TabIndex = 6;
            this.LabelCategory.Text = "Requisite:";
            // 
            // TextDescription
            // 
            this.TextDescription.Location = new System.Drawing.Point(15, 126);
            this.TextDescription.Multiline = true;
            this.TextDescription.Name = "TextDescription";
            this.TextDescription.Size = new System.Drawing.Size(230, 62);
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
            // FormEquipmentSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 611);
            this.Controls.Add(this.TabEquipmentSet);
            this.Controls.Add(this.GroupList);
            this.Controls.Add(this.MenuStrip);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormEquipmentSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Equipment Set Editor";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.GroupList.ResumeLayout(false);
            this.TabEquipmentSet.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.GroupData.ResumeLayout(false);
            this.GroupData.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
    private TabControl TabEquipmentSet;
    private TabPage tabPage1;
    private GroupBox GroupData;
    private ComboBox ComboCategory;
    private Label LabelCategory;
    private TextBox TextDescription;
    private Label LabelDescription;
    private TextBox TextName;
    private Label LabelName;
    private TextBox TextId;
    private Label LabelId;
    private GroupBox groupBox1;
    private ListBox ListSet;
    private Button ButtonClearSet;
    private Button ButtonDeleteSet;
    private GroupBox groupBox2;
    private GroupBox groupBox3;
    private Label LabelAttribute;
    private TextBox TextAttributeId;
    private GroupBox groupBox4;
    private Label label1;
    private TextBox TextSkillId;
    private Button ButtonAddSet;
}
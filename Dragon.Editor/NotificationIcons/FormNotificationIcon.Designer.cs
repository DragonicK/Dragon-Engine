﻿namespace Dragon.Editor.NotificationIcons;

partial class FormNotificationIcon {
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNotificationIcon));
        this.MenuStrip = new System.Windows.Forms.MenuStrip();
        this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
        this.TabAchievement = new System.Windows.Forms.TabControl();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.GroupData = new System.Windows.Forms.GroupBox();
        this.ComboIconType = new System.Windows.Forms.ComboBox();
        this.LabelCategory = new System.Windows.Forms.Label();
        this.TextIconId = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.TextDescription = new System.Windows.Forms.TextBox();
        this.LabelDescription = new System.Windows.Forms.Label();
        this.TextName = new System.Windows.Forms.TextBox();
        this.LabelName = new System.Windows.Forms.Label();
        this.TextId = new System.Windows.Forms.TextBox();
        this.LabelId = new System.Windows.Forms.Label();
        this.GroupList = new System.Windows.Forms.GroupBox();
        this.ButtonClear = new System.Windows.Forms.Button();
        this.ButtonDelete = new System.Windows.Forms.Button();
        this.ButtonAdd = new System.Windows.Forms.Button();
        this.ListIndex = new System.Windows.Forms.ListBox();
        this.MenuStrip.SuspendLayout();
        this.TabAchievement.SuspendLayout();
        this.tabPage1.SuspendLayout();
        this.GroupData.SuspendLayout();
        this.GroupList.SuspendLayout();
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
        // TabAchievement
        // 
        this.TabAchievement.Controls.Add(this.tabPage1);
        this.TabAchievement.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.TabAchievement.Location = new System.Drawing.Point(236, 39);
        this.TabAchievement.Name = "TabAchievement";
        this.TabAchievement.SelectedIndex = 0;
        this.TabAchievement.Size = new System.Drawing.Size(296, 571);
        this.TabAchievement.TabIndex = 6;
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
        this.GroupData.Controls.Add(this.ComboIconType);
        this.GroupData.Controls.Add(this.LabelCategory);
        this.GroupData.Controls.Add(this.TextIconId);
        this.GroupData.Controls.Add(this.label1);
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
        // ComboIconType
        // 
        this.ComboIconType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboIconType.FormattingEnabled = true;
        this.ComboIconType.Location = new System.Drawing.Point(15, 375);
        this.ComboIconType.Name = "ComboIconType";
        this.ComboIconType.Size = new System.Drawing.Size(230, 23);
        this.ComboIconType.TabIndex = 11;
        this.ComboIconType.SelectedIndexChanged += new System.EventHandler(this.ComboIconType_SelectedIndexChanged);
        // 
        // LabelCategory
        // 
        this.LabelCategory.AutoSize = true;
        this.LabelCategory.Location = new System.Drawing.Point(15, 357);
        this.LabelCategory.Name = "LabelCategory";
        this.LabelCategory.Size = new System.Drawing.Size(77, 15);
        this.LabelCategory.TabIndex = 10;
        this.LabelCategory.Text = "Icon Type:";
        // 
        // TextIconId
        // 
        this.TextIconId.Location = new System.Drawing.Point(15, 329);
        this.TextIconId.Name = "TextIconId";
        this.TextIconId.Size = new System.Drawing.Size(230, 23);
        this.TextIconId.TabIndex = 9;
        this.TextIconId.Text = "0";
        this.TextIconId.TextChanged += new System.EventHandler(this.TextIconId_TextChanged);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(15, 311);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(42, 15);
        this.label1.TabIndex = 8;
        this.label1.Text = "Icon:";
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
        // GroupList
        // 
        this.GroupList.Controls.Add(this.ButtonClear);
        this.GroupList.Controls.Add(this.ButtonDelete);
        this.GroupList.Controls.Add(this.ButtonAdd);
        this.GroupList.Controls.Add(this.ListIndex);
        this.GroupList.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.GroupList.Location = new System.Drawing.Point(12, 34);
        this.GroupList.Name = "GroupList";
        this.GroupList.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
        this.GroupList.Size = new System.Drawing.Size(218, 576);
        this.GroupList.TabIndex = 5;
        this.GroupList.TabStop = false;
        this.GroupList.Text = "Achievements";
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
        // FormNotificationIcon
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(543, 623);
        this.Controls.Add(this.TabAchievement);
        this.Controls.Add(this.GroupList);
        this.Controls.Add(this.MenuStrip);
        this.DoubleBuffered = true;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.Name = "FormNotificationIcon";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Display Icon";
        this.MenuStrip.ResumeLayout(false);
        this.MenuStrip.PerformLayout();
        this.TabAchievement.ResumeLayout(false);
        this.tabPage1.ResumeLayout(false);
        this.GroupData.ResumeLayout(false);
        this.GroupData.PerformLayout();
        this.GroupList.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private MenuStrip MenuStrip;
    private ToolStripMenuItem FileMenuItem;
    private ToolStripMenuItem MenuSave;
    private ToolStripMenuItem MenuExit;
    private TabControl TabAchievement;
    private TabPage tabPage1;
    private GroupBox GroupData;
    private TextBox TextIconId;
    private Label label1;
    private TextBox TextDescription;
    private Label LabelDescription;
    private TextBox TextName;
    private Label LabelName;
    private TextBox TextId;
    private Label LabelId;
    private GroupBox GroupList;
    private Button ButtonClear;
    private Button ButtonDelete;
    private Button ButtonAdd;
    private ListBox ListIndex;
    private ComboBox ComboIconType;
    private Label LabelCategory;
}
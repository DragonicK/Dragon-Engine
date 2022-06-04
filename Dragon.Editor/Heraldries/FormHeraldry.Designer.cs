namespace Dragon.Editor.Heraldries;

partial class FormHeraldry {
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHeraldry));
        this.MenuStrip = new System.Windows.Forms.MenuStrip();
        this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
        this.GroupList = new System.Windows.Forms.GroupBox();
        this.ButtonClear = new System.Windows.Forms.Button();
        this.ButtonDelete = new System.Windows.Forms.Button();
        this.ButtonAdd = new System.Windows.Forms.Button();
        this.ListIndex = new System.Windows.Forms.ListBox();
        this.TabHeraldry = new System.Windows.Forms.TabControl();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.GroupData = new System.Windows.Forms.GroupBox();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.LabelAttribute = new System.Windows.Forms.Label();
        this.TextUpgradeId = new System.Windows.Forms.TextBox();
        this.TextDescription = new System.Windows.Forms.TextBox();
        this.LabelDescription = new System.Windows.Forms.Label();
        this.TextName = new System.Windows.Forms.TextBox();
        this.LabelName = new System.Windows.Forms.Label();
        this.TextId = new System.Windows.Forms.TextBox();
        this.LabelId = new System.Windows.Forms.Label();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.GroupRequired = new System.Windows.Forms.GroupBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.label2 = new System.Windows.Forms.Label();
        this.TextAttributeId = new System.Windows.Forms.TextBox();
        this.ScrollChance = new System.Windows.Forms.HScrollBar();
        this.ScrollIndex = new System.Windows.Forms.HScrollBar();
        this.ButtonAttributeDelete = new System.Windows.Forms.Button();
        this.ButtonAttributeAdd = new System.Windows.Forms.Button();
        this.LabelIndex = new System.Windows.Forms.Label();
        this.LabelChance = new System.Windows.Forms.Label();
        this.MenuStrip.SuspendLayout();
        this.GroupList.SuspendLayout();
        this.TabHeraldry.SuspendLayout();
        this.tabPage1.SuspendLayout();
        this.GroupData.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.tabPage2.SuspendLayout();
        this.GroupRequired.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.SuspendLayout();
        // 
        // MenuStrip
        // 
        this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
        this.MenuStrip.Location = new System.Drawing.Point(0, 0);
        this.MenuStrip.Name = "MenuStrip";
        this.MenuStrip.Size = new System.Drawing.Size(538, 24);
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
        this.GroupList.Text = "Heraldries";
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
        // TabHeraldry
        // 
        this.TabHeraldry.Controls.Add(this.tabPage1);
        this.TabHeraldry.Controls.Add(this.tabPage2);
        this.TabHeraldry.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.TabHeraldry.Location = new System.Drawing.Point(236, 32);
        this.TabHeraldry.Name = "TabHeraldry";
        this.TabHeraldry.SelectedIndex = 0;
        this.TabHeraldry.Size = new System.Drawing.Size(296, 571);
        this.TabHeraldry.TabIndex = 5;
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
        this.groupBox2.Controls.Add(this.TextUpgradeId);
        this.groupBox2.Location = new System.Drawing.Point(15, 310);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(230, 111);
        this.groupBox2.TabIndex = 10;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Upgrade:";
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
        this.tabPage2.Controls.Add(this.GroupRequired);
        this.tabPage2.Location = new System.Drawing.Point(4, 24);
        this.tabPage2.Name = "tabPage2";
        this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage2.Size = new System.Drawing.Size(288, 543);
        this.tabPage2.TabIndex = 1;
        this.tabPage2.Text = "Attribute";
        this.tabPage2.UseVisualStyleBackColor = true;
        // 
        // GroupRequired
        // 
        this.GroupRequired.Controls.Add(this.groupBox1);
        this.GroupRequired.Controls.Add(this.ScrollChance);
        this.GroupRequired.Controls.Add(this.ScrollIndex);
        this.GroupRequired.Controls.Add(this.ButtonAttributeDelete);
        this.GroupRequired.Controls.Add(this.ButtonAttributeAdd);
        this.GroupRequired.Controls.Add(this.LabelIndex);
        this.GroupRequired.Controls.Add(this.LabelChance);
        this.GroupRequired.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.GroupRequired.Location = new System.Drawing.Point(12, 8);
        this.GroupRequired.Name = "GroupRequired";
        this.GroupRequired.Size = new System.Drawing.Size(263, 522);
        this.GroupRequired.TabIndex = 7;
        this.GroupRequired.TabStop = false;
        this.GroupRequired.Text = "Random Attribute";
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.TextAttributeId);
        this.groupBox1.Location = new System.Drawing.Point(17, 135);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(230, 111);
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
        this.label2.Size = new System.Drawing.Size(224, 60);
        this.label2.TabIndex = 11;
        this.label2.Text = "Unselected";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
        // ScrollChance
        // 
        this.ScrollChance.LargeChange = 1;
        this.ScrollChance.Location = new System.Drawing.Point(17, 278);
        this.ScrollChance.Name = "ScrollChance";
        this.ScrollChance.Size = new System.Drawing.Size(230, 18);
        this.ScrollChance.TabIndex = 19;
        this.ScrollChance.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollChance_Scroll);
        // 
        // ScrollIndex
        // 
        this.ScrollIndex.LargeChange = 1;
        this.ScrollIndex.Location = new System.Drawing.Point(17, 99);
        this.ScrollIndex.Maximum = 0;
        this.ScrollIndex.Name = "ScrollIndex";
        this.ScrollIndex.Size = new System.Drawing.Size(230, 18);
        this.ScrollIndex.TabIndex = 18;
        this.ScrollIndex.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollIndex_Scroll);
        // 
        // ButtonAttributeDelete
        // 
        this.ButtonAttributeDelete.Location = new System.Drawing.Point(132, 39);
        this.ButtonAttributeDelete.Name = "ButtonAttributeDelete";
        this.ButtonAttributeDelete.Size = new System.Drawing.Size(75, 23);
        this.ButtonAttributeDelete.TabIndex = 17;
        this.ButtonAttributeDelete.Text = "Delete";
        this.ButtonAttributeDelete.UseVisualStyleBackColor = true;
        this.ButtonAttributeDelete.Click += new System.EventHandler(this.ButtonAttributeDelete_Click);
        // 
        // ButtonAttributeAdd
        // 
        this.ButtonAttributeAdd.Location = new System.Drawing.Point(51, 39);
        this.ButtonAttributeAdd.Name = "ButtonAttributeAdd";
        this.ButtonAttributeAdd.Size = new System.Drawing.Size(75, 23);
        this.ButtonAttributeAdd.TabIndex = 16;
        this.ButtonAttributeAdd.Text = "Add";
        this.ButtonAttributeAdd.UseVisualStyleBackColor = true;
        this.ButtonAttributeAdd.Click += new System.EventHandler(this.ButtonAttributeAdd_Click);
        // 
        // LabelIndex
        // 
        this.LabelIndex.Location = new System.Drawing.Point(17, 74);
        this.LabelIndex.Name = "LabelIndex";
        this.LabelIndex.Size = new System.Drawing.Size(230, 15);
        this.LabelIndex.TabIndex = 15;
        this.LabelIndex.Text = "Index: 0 / 0";
        this.LabelIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // LabelChance
        // 
        this.LabelChance.AutoSize = true;
        this.LabelChance.Location = new System.Drawing.Point(17, 258);
        this.LabelChance.Name = "LabelChance";
        this.LabelChance.Size = new System.Drawing.Size(77, 15);
        this.LabelChance.TabIndex = 2;
        this.LabelChance.Text = "Chance: 0%";
        // 
        // FormHeraldry
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(538, 611);
        this.Controls.Add(this.TabHeraldry);
        this.Controls.Add(this.GroupList);
        this.Controls.Add(this.MenuStrip);
        this.DoubleBuffered = true;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.Name = "FormHeraldry";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Heraldry Editor";
        this.MenuStrip.ResumeLayout(false);
        this.MenuStrip.PerformLayout();
        this.GroupList.ResumeLayout(false);
        this.TabHeraldry.ResumeLayout(false);
        this.tabPage1.ResumeLayout(false);
        this.GroupData.ResumeLayout(false);
        this.GroupData.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.tabPage2.ResumeLayout(false);
        this.GroupRequired.ResumeLayout(false);
        this.GroupRequired.PerformLayout();
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
    private TabControl TabHeraldry;
    private TabPage tabPage1;
    private GroupBox GroupData;
    private GroupBox groupBox2;
    private Label LabelAttribute;
    private TextBox TextUpgradeId;
    private TextBox TextDescription;
    private Label LabelDescription;
    private TextBox TextName;
    private Label LabelName;
    private TextBox TextId;
    private Label LabelId;
    private TabPage tabPage2;
    private GroupBox GroupRequired;
    private HScrollBar ScrollIndex;
    private Button ButtonAttributeDelete;
    private Button ButtonAttributeAdd;
    private Label LabelIndex;
    private Label LabelChance;
    private HScrollBar ScrollChance;
    private GroupBox groupBox1;
    private Label label2;
    private TextBox TextAttributeId;
}
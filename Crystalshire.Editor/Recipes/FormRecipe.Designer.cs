namespace Crystalshire.Editor.Recipes;

partial class FormRecipe {
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRecipe));
        this.MenuStrip = new System.Windows.Forms.MenuStrip();
        this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
        this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
        this.GroupList = new System.Windows.Forms.GroupBox();
        this.ButtonClear = new System.Windows.Forms.Button();
        this.ButtonDelete = new System.Windows.Forms.Button();
        this.ButtonAdd = new System.Windows.Forms.Button();
        this.ListIndex = new System.Windows.Forms.ListBox();
        this.TabRecipe = new System.Windows.Forms.TabControl();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.GroupData = new System.Windows.Forms.GroupBox();
        this.TextExperience = new System.Windows.Forms.TextBox();
        this.label16 = new System.Windows.Forms.Label();
        this.TextLevel = new System.Windows.Forms.TextBox();
        this.LabelLevel = new System.Windows.Forms.Label();
        this.ComboCategory = new System.Windows.Forms.ComboBox();
        this.LabelCategory = new System.Windows.Forms.Label();
        this.TextDescription = new System.Windows.Forms.TextBox();
        this.LabelDescription = new System.Windows.Forms.Label();
        this.TextName = new System.Windows.Forms.TextBox();
        this.LabelName = new System.Windows.Forms.Label();
        this.TextId = new System.Windows.Forms.TextBox();
        this.LabelId = new System.Windows.Forms.Label();
        this.tabPage3 = new System.Windows.Forms.TabPage();
        this.GroupRequired = new System.Windows.Forms.GroupBox();
        this.ScrollIndex = new System.Windows.Forms.HScrollBar();
        this.ButtonRequiredDelete = new System.Windows.Forms.Button();
        this.ButtonRequiredAdd = new System.Windows.Forms.Button();
        this.LabelIndex = new System.Windows.Forms.Label();
        this.TextRequiredLevel = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.TextRequiredValue = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.TextRequiredId = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.GroupReward = new System.Windows.Forms.GroupBox();
        this.TextRewardBound = new System.Windows.Forms.TextBox();
        this.label9 = new System.Windows.Forms.Label();
        this.TextRewardLevel = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.TextRewardValue = new System.Windows.Forms.TextBox();
        this.label7 = new System.Windows.Forms.Label();
        this.TextRewardId = new System.Windows.Forms.TextBox();
        this.label8 = new System.Windows.Forms.Label();
        this.MenuStrip.SuspendLayout();
        this.GroupList.SuspendLayout();
        this.TabRecipe.SuspendLayout();
        this.tabPage1.SuspendLayout();
        this.GroupData.SuspendLayout();
        this.tabPage3.SuspendLayout();
        this.GroupRequired.SuspendLayout();
        this.tabPage2.SuspendLayout();
        this.GroupReward.SuspendLayout();
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
        this.GroupList.Text = "Recipes";
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
        // TabRecipe
        // 
        this.TabRecipe.Controls.Add(this.tabPage1);
        this.TabRecipe.Controls.Add(this.tabPage3);
        this.TabRecipe.Controls.Add(this.tabPage2);
        this.TabRecipe.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.TabRecipe.Location = new System.Drawing.Point(236, 32);
        this.TabRecipe.Name = "TabRecipe";
        this.TabRecipe.SelectedIndex = 0;
        this.TabRecipe.Size = new System.Drawing.Size(298, 571);
        this.TabRecipe.TabIndex = 5;
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
        this.GroupData.Controls.Add(this.TextExperience);
        this.GroupData.Controls.Add(this.label16);
        this.GroupData.Controls.Add(this.TextLevel);
        this.GroupData.Controls.Add(this.LabelLevel);
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
        // TextExperience
        // 
        this.TextExperience.Location = new System.Drawing.Point(15, 313);
        this.TextExperience.Name = "TextExperience";
        this.TextExperience.Size = new System.Drawing.Size(230, 23);
        this.TextExperience.TabIndex = 11;
        this.TextExperience.Text = "0";
        this.TextExperience.TextChanged += new System.EventHandler(this.TextExperience_TextChanged);
        // 
        // label16
        // 
        this.label16.AutoSize = true;
        this.label16.Location = new System.Drawing.Point(15, 295);
        this.label16.Name = "label16";
        this.label16.Size = new System.Drawing.Size(84, 15);
        this.label16.TabIndex = 12;
        this.label16.Text = "Experience:";
        // 
        // TextLevel
        // 
        this.TextLevel.Location = new System.Drawing.Point(15, 271);
        this.TextLevel.Name = "TextLevel";
        this.TextLevel.Size = new System.Drawing.Size(230, 23);
        this.TextLevel.TabIndex = 8;
        this.TextLevel.Text = "0";
        this.TextLevel.TextChanged += new System.EventHandler(this.TextLevel_TextChanged);
        // 
        // LabelLevel
        // 
        this.LabelLevel.AutoSize = true;
        this.LabelLevel.Location = new System.Drawing.Point(15, 253);
        this.LabelLevel.Name = "LabelLevel";
        this.LabelLevel.Size = new System.Drawing.Size(49, 15);
        this.LabelLevel.TabIndex = 8;
        this.LabelLevel.Text = "Level:";
        // 
        // ComboCategory
        // 
        this.ComboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboCategory.FormattingEnabled = true;
        this.ComboCategory.Location = new System.Drawing.Point(15, 360);
        this.ComboCategory.Name = "ComboCategory";
        this.ComboCategory.Size = new System.Drawing.Size(230, 23);
        this.ComboCategory.TabIndex = 9;
        this.ComboCategory.SelectedIndexChanged += new System.EventHandler(this.ComboCategory_SelectedIndexChanged);
        // 
        // LabelCategory
        // 
        this.LabelCategory.AutoSize = true;
        this.LabelCategory.Location = new System.Drawing.Point(15, 342);
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
        this.TextDescription.Size = new System.Drawing.Size(230, 125);
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
        // tabPage3
        // 
        this.tabPage3.Controls.Add(this.GroupRequired);
        this.tabPage3.Location = new System.Drawing.Point(4, 24);
        this.tabPage3.Name = "tabPage3";
        this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage3.Size = new System.Drawing.Size(290, 543);
        this.tabPage3.TabIndex = 2;
        this.tabPage3.Text = "Required";
        this.tabPage3.UseVisualStyleBackColor = true;
        // 
        // GroupRequired
        // 
        this.GroupRequired.Controls.Add(this.ScrollIndex);
        this.GroupRequired.Controls.Add(this.ButtonRequiredDelete);
        this.GroupRequired.Controls.Add(this.ButtonRequiredAdd);
        this.GroupRequired.Controls.Add(this.LabelIndex);
        this.GroupRequired.Controls.Add(this.TextRequiredLevel);
        this.GroupRequired.Controls.Add(this.label2);
        this.GroupRequired.Controls.Add(this.TextRequiredValue);
        this.GroupRequired.Controls.Add(this.label3);
        this.GroupRequired.Controls.Add(this.TextRequiredId);
        this.GroupRequired.Controls.Add(this.label4);
        this.GroupRequired.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.GroupRequired.Location = new System.Drawing.Point(12, 8);
        this.GroupRequired.Name = "GroupRequired";
        this.GroupRequired.Size = new System.Drawing.Size(263, 522);
        this.GroupRequired.TabIndex = 6;
        this.GroupRequired.TabStop = false;
        this.GroupRequired.Text = "Required";
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
        // ButtonRequiredDelete
        // 
        this.ButtonRequiredDelete.Location = new System.Drawing.Point(132, 39);
        this.ButtonRequiredDelete.Name = "ButtonRequiredDelete";
        this.ButtonRequiredDelete.Size = new System.Drawing.Size(75, 23);
        this.ButtonRequiredDelete.TabIndex = 17;
        this.ButtonRequiredDelete.Text = "Delete";
        this.ButtonRequiredDelete.UseVisualStyleBackColor = true;
        this.ButtonRequiredDelete.Click += new System.EventHandler(this.ButtonRequiredDelete_Click);
        // 
        // ButtonRequiredAdd
        // 
        this.ButtonRequiredAdd.Location = new System.Drawing.Point(51, 39);
        this.ButtonRequiredAdd.Name = "ButtonRequiredAdd";
        this.ButtonRequiredAdd.Size = new System.Drawing.Size(75, 23);
        this.ButtonRequiredAdd.TabIndex = 16;
        this.ButtonRequiredAdd.Text = "Add";
        this.ButtonRequiredAdd.UseVisualStyleBackColor = true;
        this.ButtonRequiredAdd.Click += new System.EventHandler(this.ButtonRequiredAdd_Click);
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
        // TextRequiredLevel
        // 
        this.TextRequiredLevel.Location = new System.Drawing.Point(17, 242);
        this.TextRequiredLevel.Name = "TextRequiredLevel";
        this.TextRequiredLevel.Size = new System.Drawing.Size(230, 23);
        this.TextRequiredLevel.TabIndex = 14;
        this.TextRequiredLevel.Text = "0";
        this.TextRequiredLevel.TextChanged += new System.EventHandler(this.TextRequiredLevel_TextChanged);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(17, 224);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(49, 15);
        this.label2.TabIndex = 4;
        this.label2.Text = "Level:";
        // 
        // TextRequiredValue
        // 
        this.TextRequiredValue.Location = new System.Drawing.Point(17, 199);
        this.TextRequiredValue.Name = "TextRequiredValue";
        this.TextRequiredValue.Size = new System.Drawing.Size(230, 23);
        this.TextRequiredValue.TabIndex = 13;
        this.TextRequiredValue.Text = "0";
        this.TextRequiredValue.TextChanged += new System.EventHandler(this.TextRequiredValue_TextChanged);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(17, 181);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(49, 15);
        this.label3.TabIndex = 2;
        this.label3.Text = "Value:";
        // 
        // TextRequiredId
        // 
        this.TextRequiredId.Location = new System.Drawing.Point(17, 155);
        this.TextRequiredId.Name = "TextRequiredId";
        this.TextRequiredId.Size = new System.Drawing.Size(230, 23);
        this.TextRequiredId.TabIndex = 12;
        this.TextRequiredId.Text = "0";
        this.TextRequiredId.TextChanged += new System.EventHandler(this.TextRequiredId_TextChanged);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(17, 137);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(28, 15);
        this.label4.TabIndex = 0;
        this.label4.Text = "Id:";
        // 
        // tabPage2
        // 
        this.tabPage2.Controls.Add(this.GroupReward);
        this.tabPage2.Location = new System.Drawing.Point(4, 24);
        this.tabPage2.Name = "tabPage2";
        this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage2.Size = new System.Drawing.Size(290, 543);
        this.tabPage2.TabIndex = 1;
        this.tabPage2.Text = "Reward";
        this.tabPage2.UseVisualStyleBackColor = true;
        // 
        // GroupReward
        // 
        this.GroupReward.Controls.Add(this.TextRewardBound);
        this.GroupReward.Controls.Add(this.label9);
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
        // TextRewardBound
        // 
        this.TextRewardBound.Location = new System.Drawing.Point(16, 166);
        this.TextRewardBound.Name = "TextRewardBound";
        this.TextRewardBound.Size = new System.Drawing.Size(230, 23);
        this.TextRewardBound.TabIndex = 15;
        this.TextRewardBound.Text = "0";
        this.TextRewardBound.TextChanged += new System.EventHandler(this.TextRewardBound_TextChanged);
        // 
        // label9
        // 
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(16, 148);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(56, 15);
        this.label9.TabIndex = 14;
        this.label9.Text = "Bound: ";
        // 
        // TextRewardLevel
        // 
        this.TextRewardLevel.Location = new System.Drawing.Point(16, 124);
        this.TextRewardLevel.Name = "TextRewardLevel";
        this.TextRewardLevel.Size = new System.Drawing.Size(230, 23);
        this.TextRewardLevel.TabIndex = 14;
        this.TextRewardLevel.Text = "0";
        this.TextRewardLevel.TextChanged += new System.EventHandler(this.TextRewardLevel_TextChanged);
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(16, 106);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(49, 15);
        this.label6.TabIndex = 4;
        this.label6.Text = "Level:";
        // 
        // TextRewardValue
        // 
        this.TextRewardValue.Location = new System.Drawing.Point(16, 81);
        this.TextRewardValue.Name = "TextRewardValue";
        this.TextRewardValue.Size = new System.Drawing.Size(230, 23);
        this.TextRewardValue.TabIndex = 13;
        this.TextRewardValue.Text = "0";
        this.TextRewardValue.TextChanged += new System.EventHandler(this.TextRewardValue_TextChanged);
        // 
        // label7
        // 
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(16, 63);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(49, 15);
        this.label7.TabIndex = 2;
        this.label7.Text = "Value:";
        // 
        // TextRewardId
        // 
        this.TextRewardId.Location = new System.Drawing.Point(16, 37);
        this.TextRewardId.Name = "TextRewardId";
        this.TextRewardId.Size = new System.Drawing.Size(230, 23);
        this.TextRewardId.TabIndex = 12;
        this.TextRewardId.Text = "0";
        this.TextRewardId.TextChanged += new System.EventHandler(this.TextRewardId_TextChanged);
        // 
        // label8
        // 
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(16, 19);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(28, 15);
        this.label8.TabIndex = 0;
        this.label8.Text = "Id:";
        // 
        // FormRecipe
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(545, 611);
        this.Controls.Add(this.TabRecipe);
        this.Controls.Add(this.GroupList);
        this.Controls.Add(this.MenuStrip);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.Name = "FormRecipe";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Recipe Editor";
        this.MenuStrip.ResumeLayout(false);
        this.MenuStrip.PerformLayout();
        this.GroupList.ResumeLayout(false);
        this.TabRecipe.ResumeLayout(false);
        this.tabPage1.ResumeLayout(false);
        this.GroupData.ResumeLayout(false);
        this.GroupData.PerformLayout();
        this.tabPage3.ResumeLayout(false);
        this.GroupRequired.ResumeLayout(false);
        this.GroupRequired.PerformLayout();
        this.tabPage2.ResumeLayout(false);
        this.GroupReward.ResumeLayout(false);
        this.GroupReward.PerformLayout();
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
    private TabControl TabRecipe;
    private TabPage tabPage1;
    private GroupBox GroupData;
    private TextBox TextLevel;
    private Label LabelLevel;
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
    private TextBox TextRewardLevel;
    private Label label6;
    private TextBox TextRewardValue;
    private Label label7;
    private TextBox TextRewardId;
    private Label label8;
    private TextBox TextExperience;
    private Label label16;
    private TextBox TextRewardBound;
    private Label label9;
    private TabPage tabPage3;
    private GroupBox GroupRequired;
    private TextBox TextRequiredLevel;
    private Label label2;
    private TextBox TextRequiredValue;
    private Label label3;
    private TextBox TextRequiredId;
    private Label label4;
    private Button ButtonRequiredDelete;
    private Button ButtonRequiredAdd;
    private Label LabelIndex;
    private HScrollBar ScrollIndex;
}
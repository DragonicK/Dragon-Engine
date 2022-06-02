namespace Crystalshire.Editor.Conversations;

partial class FormConversation {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConversation));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupList = new System.Windows.Forms.GroupBox();
            this.ButtonClear = new System.Windows.Forms.Button();
            this.ButtonDelete = new System.Windows.Forms.Button();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ListIndex = new System.Windows.Forms.ListBox();
            this.TabConversation = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.GroupData = new System.Windows.Forms.GroupBox();
            this.TextQuest = new System.Windows.Forms.TextBox();
            this.LabelPoint = new System.Windows.Forms.Label();
            this.ComboType = new System.Windows.Forms.ComboBox();
            this.LabelType = new System.Windows.Forms.Label();
            this.TextName = new System.Windows.Forms.TextBox();
            this.LabelName = new System.Windows.Forms.Label();
            this.TextId = new System.Windows.Forms.TextBox();
            this.LabelId = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ButtonRemoveChat = new System.Windows.Forms.Button();
            this.ButtonAddChat = new System.Windows.Forms.Button();
            this.GroupEvent = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TextData3 = new System.Windows.Forms.TextBox();
            this.TextData2 = new System.Windows.Forms.TextBox();
            this.TextData1 = new System.Windows.Forms.TextBox();
            this.ComboEvent = new System.Windows.Forms.ComboBox();
            this.GroupReply = new System.Windows.Forms.GroupBox();
            this.ComboReply_3 = new System.Windows.Forms.ComboBox();
            this.TextReply_3 = new System.Windows.Forms.TextBox();
            this.ComboReply_2 = new System.Windows.Forms.ComboBox();
            this.TextReply_2 = new System.Windows.Forms.TextBox();
            this.ComboReply_1 = new System.Windows.Forms.ComboBox();
            this.TextReply_1 = new System.Windows.Forms.TextBox();
            this.ComboReply_0 = new System.Windows.Forms.ComboBox();
            this.TextReply_0 = new System.Windows.Forms.TextBox();
            this.TextChat = new System.Windows.Forms.TextBox();
            this.LabelIndex = new System.Windows.Forms.Label();
            this.ScrollIndex = new System.Windows.Forms.HScrollBar();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GroupDone = new System.Windows.Forms.GroupBox();
            this.TextDone = new System.Windows.Forms.TextBox();
            this.GroupMeanwhile = new System.Windows.Forms.GroupBox();
            this.TextMeanwhile = new System.Windows.Forms.TextBox();
            this.MenuStrip.SuspendLayout();
            this.GroupList.SuspendLayout();
            this.TabConversation.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.GroupData.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.GroupEvent.SuspendLayout();
            this.GroupReply.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GroupDone.SuspendLayout();
            this.GroupMeanwhile.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(620, 24);
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
            this.GroupList.Location = new System.Drawing.Point(12, 23);
            this.GroupList.Name = "GroupList";
            this.GroupList.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.GroupList.Size = new System.Drawing.Size(218, 576);
            this.GroupList.TabIndex = 4;
            this.GroupList.TabStop = false;
            this.GroupList.Text = "Conversation";
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
            // TabConversation
            // 
            this.TabConversation.Controls.Add(this.tabPage1);
            this.TabConversation.Controls.Add(this.tabPage2);
            this.TabConversation.Controls.Add(this.tabPage3);
            this.TabConversation.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TabConversation.Location = new System.Drawing.Point(236, 28);
            this.TabConversation.Name = "TabConversation";
            this.TabConversation.SelectedIndex = 0;
            this.TabConversation.Size = new System.Drawing.Size(374, 571);
            this.TabConversation.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GroupData);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(366, 543);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // GroupData
            // 
            this.GroupData.Controls.Add(this.TextQuest);
            this.GroupData.Controls.Add(this.LabelPoint);
            this.GroupData.Controls.Add(this.ComboType);
            this.GroupData.Controls.Add(this.LabelType);
            this.GroupData.Controls.Add(this.TextName);
            this.GroupData.Controls.Add(this.LabelName);
            this.GroupData.Controls.Add(this.TextId);
            this.GroupData.Controls.Add(this.LabelId);
            this.GroupData.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GroupData.Location = new System.Drawing.Point(12, 8);
            this.GroupData.Name = "GroupData";
            this.GroupData.Size = new System.Drawing.Size(339, 522);
            this.GroupData.TabIndex = 4;
            this.GroupData.TabStop = false;
            this.GroupData.Text = "Data";
            // 
            // TextQuest
            // 
            this.TextQuest.Enabled = false;
            this.TextQuest.Location = new System.Drawing.Point(15, 174);
            this.TextQuest.Name = "TextQuest";
            this.TextQuest.Size = new System.Drawing.Size(305, 23);
            this.TextQuest.TabIndex = 3;
            this.TextQuest.Text = "0";
            this.TextQuest.TextChanged += new System.EventHandler(this.TextQuest_TextChanged);
            // 
            // LabelPoint
            // 
            this.LabelPoint.AutoSize = true;
            this.LabelPoint.Location = new System.Drawing.Point(15, 156);
            this.LabelPoint.Name = "LabelPoint";
            this.LabelPoint.Size = new System.Drawing.Size(70, 15);
            this.LabelPoint.TabIndex = 8;
            this.LabelPoint.Text = "Quest Id:";
            // 
            // ComboType
            // 
            this.ComboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboType.FormattingEnabled = true;
            this.ComboType.Location = new System.Drawing.Point(15, 128);
            this.ComboType.Name = "ComboType";
            this.ComboType.Size = new System.Drawing.Size(305, 23);
            this.ComboType.TabIndex = 2;
            this.ComboType.SelectedIndexChanged += new System.EventHandler(this.ComboType_SelectedIndexChanged);
            // 
            // LabelType
            // 
            this.LabelType.AutoSize = true;
            this.LabelType.Location = new System.Drawing.Point(15, 110);
            this.LabelType.Name = "LabelType";
            this.LabelType.Size = new System.Drawing.Size(42, 15);
            this.LabelType.TabIndex = 6;
            this.LabelType.Text = "Type:";
            // 
            // TextName
            // 
            this.TextName.Location = new System.Drawing.Point(15, 83);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(305, 23);
            this.TextName.TabIndex = 1;
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
            this.TextId.Size = new System.Drawing.Size(305, 23);
            this.TextId.TabIndex = 0;
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
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(366, 543);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Conversation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ButtonRemoveChat);
            this.groupBox2.Controls.Add(this.ButtonAddChat);
            this.groupBox2.Controls.Add(this.GroupEvent);
            this.groupBox2.Controls.Add(this.GroupReply);
            this.groupBox2.Controls.Add(this.TextChat);
            this.groupBox2.Controls.Add(this.LabelIndex);
            this.groupBox2.Controls.Add(this.ScrollIndex);
            this.groupBox2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(12, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(339, 522);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conversation";
            // 
            // ButtonRemoveChat
            // 
            this.ButtonRemoveChat.Location = new System.Drawing.Point(176, 22);
            this.ButtonRemoveChat.Name = "ButtonRemoveChat";
            this.ButtonRemoveChat.Size = new System.Drawing.Size(100, 23);
            this.ButtonRemoveChat.TabIndex = 5;
            this.ButtonRemoveChat.Text = "Remove";
            this.ButtonRemoveChat.UseVisualStyleBackColor = true;
            this.ButtonRemoveChat.Click += new System.EventHandler(this.ButtonRemoveChat_Click);
            // 
            // ButtonAddChat
            // 
            this.ButtonAddChat.Location = new System.Drawing.Point(65, 22);
            this.ButtonAddChat.Name = "ButtonAddChat";
            this.ButtonAddChat.Size = new System.Drawing.Size(100, 23);
            this.ButtonAddChat.TabIndex = 4;
            this.ButtonAddChat.Text = "Add";
            this.ButtonAddChat.UseVisualStyleBackColor = true;
            this.ButtonAddChat.Click += new System.EventHandler(this.ButtonAddChat_Click);
            // 
            // GroupEvent
            // 
            this.GroupEvent.Controls.Add(this.label3);
            this.GroupEvent.Controls.Add(this.label2);
            this.GroupEvent.Controls.Add(this.label1);
            this.GroupEvent.Controls.Add(this.TextData3);
            this.GroupEvent.Controls.Add(this.TextData2);
            this.GroupEvent.Controls.Add(this.TextData1);
            this.GroupEvent.Controls.Add(this.ComboEvent);
            this.GroupEvent.Enabled = false;
            this.GroupEvent.Location = new System.Drawing.Point(16, 350);
            this.GroupEvent.Name = "GroupEvent";
            this.GroupEvent.Size = new System.Drawing.Size(305, 156);
            this.GroupEvent.TabIndex = 4;
            this.GroupEvent.TabStop = false;
            this.GroupEvent.Text = "Events";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "Data 3:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "Data 2:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Data 1:";
            // 
            // TextData3
            // 
            this.TextData3.Location = new System.Drawing.Point(85, 111);
            this.TextData3.Name = "TextData3";
            this.TextData3.Size = new System.Drawing.Size(199, 23);
            this.TextData3.TabIndex = 19;
            this.TextData3.Text = "0";
            this.TextData3.TextChanged += new System.EventHandler(this.TextData3_TextChanged);
            // 
            // TextData2
            // 
            this.TextData2.Location = new System.Drawing.Point(85, 80);
            this.TextData2.Name = "TextData2";
            this.TextData2.Size = new System.Drawing.Size(199, 23);
            this.TextData2.TabIndex = 18;
            this.TextData2.Text = "0";
            this.TextData2.TextChanged += new System.EventHandler(this.TextData2_TextChanged);
            // 
            // TextData1
            // 
            this.TextData1.Location = new System.Drawing.Point(85, 51);
            this.TextData1.Name = "TextData1";
            this.TextData1.Size = new System.Drawing.Size(199, 23);
            this.TextData1.TabIndex = 17;
            this.TextData1.Text = "0";
            this.TextData1.TextChanged += new System.EventHandler(this.TextData1_TextChanged);
            // 
            // ComboEvent
            // 
            this.ComboEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboEvent.FormattingEnabled = true;
            this.ComboEvent.Location = new System.Drawing.Point(18, 22);
            this.ComboEvent.Name = "ComboEvent";
            this.ComboEvent.Size = new System.Drawing.Size(266, 23);
            this.ComboEvent.TabIndex = 16;
            this.ComboEvent.SelectedIndexChanged += new System.EventHandler(this.ComboEvent_SelectedIndexChanged);
            // 
            // GroupReply
            // 
            this.GroupReply.Controls.Add(this.ComboReply_3);
            this.GroupReply.Controls.Add(this.TextReply_3);
            this.GroupReply.Controls.Add(this.ComboReply_2);
            this.GroupReply.Controls.Add(this.TextReply_2);
            this.GroupReply.Controls.Add(this.ComboReply_1);
            this.GroupReply.Controls.Add(this.TextReply_1);
            this.GroupReply.Controls.Add(this.ComboReply_0);
            this.GroupReply.Controls.Add(this.TextReply_0);
            this.GroupReply.Enabled = false;
            this.GroupReply.Location = new System.Drawing.Point(16, 198);
            this.GroupReply.Name = "GroupReply";
            this.GroupReply.Size = new System.Drawing.Size(305, 146);
            this.GroupReply.TabIndex = 3;
            this.GroupReply.TabStop = false;
            this.GroupReply.Text = "Replies";
            // 
            // ComboReply_3
            // 
            this.ComboReply_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboReply_3.FormattingEnabled = true;
            this.ComboReply_3.Location = new System.Drawing.Point(228, 109);
            this.ComboReply_3.Name = "ComboReply_3";
            this.ComboReply_3.Size = new System.Drawing.Size(59, 23);
            this.ComboReply_3.TabIndex = 15;
            // 
            // TextReply_3
            // 
            this.TextReply_3.Location = new System.Drawing.Point(18, 109);
            this.TextReply_3.Name = "TextReply_3";
            this.TextReply_3.Size = new System.Drawing.Size(198, 23);
            this.TextReply_3.TabIndex = 14;
            // 
            // ComboReply_2
            // 
            this.ComboReply_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboReply_2.FormattingEnabled = true;
            this.ComboReply_2.Location = new System.Drawing.Point(228, 80);
            this.ComboReply_2.Name = "ComboReply_2";
            this.ComboReply_2.Size = new System.Drawing.Size(59, 23);
            this.ComboReply_2.TabIndex = 13;
            // 
            // TextReply_2
            // 
            this.TextReply_2.Location = new System.Drawing.Point(18, 80);
            this.TextReply_2.Name = "TextReply_2";
            this.TextReply_2.Size = new System.Drawing.Size(198, 23);
            this.TextReply_2.TabIndex = 12;
            // 
            // ComboReply_1
            // 
            this.ComboReply_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboReply_1.FormattingEnabled = true;
            this.ComboReply_1.Location = new System.Drawing.Point(228, 51);
            this.ComboReply_1.Name = "ComboReply_1";
            this.ComboReply_1.Size = new System.Drawing.Size(59, 23);
            this.ComboReply_1.TabIndex = 11;
            // 
            // TextReply_1
            // 
            this.TextReply_1.Location = new System.Drawing.Point(18, 51);
            this.TextReply_1.Name = "TextReply_1";
            this.TextReply_1.Size = new System.Drawing.Size(198, 23);
            this.TextReply_1.TabIndex = 10;
            // 
            // ComboReply_0
            // 
            this.ComboReply_0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboReply_0.FormattingEnabled = true;
            this.ComboReply_0.Location = new System.Drawing.Point(228, 22);
            this.ComboReply_0.Name = "ComboReply_0";
            this.ComboReply_0.Size = new System.Drawing.Size(59, 23);
            this.ComboReply_0.TabIndex = 9;
            // 
            // TextReply_0
            // 
            this.TextReply_0.Location = new System.Drawing.Point(18, 22);
            this.TextReply_0.Name = "TextReply_0";
            this.TextReply_0.Size = new System.Drawing.Size(198, 23);
            this.TextReply_0.TabIndex = 8;
            // 
            // TextChat
            // 
            this.TextChat.Enabled = false;
            this.TextChat.Location = new System.Drawing.Point(16, 111);
            this.TextChat.Multiline = true;
            this.TextChat.Name = "TextChat";
            this.TextChat.Size = new System.Drawing.Size(305, 81);
            this.TextChat.TabIndex = 7;
            this.TextChat.TextChanged += new System.EventHandler(this.TextChat_TextChanged);
            // 
            // LabelIndex
            // 
            this.LabelIndex.Location = new System.Drawing.Point(16, 60);
            this.LabelIndex.Name = "LabelIndex";
            this.LabelIndex.Size = new System.Drawing.Size(305, 15);
            this.LabelIndex.TabIndex = 1;
            this.LabelIndex.Text = "Conversation Index: 0/0";
            this.LabelIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScrollIndex
            // 
            this.ScrollIndex.Enabled = false;
            this.ScrollIndex.LargeChange = 1;
            this.ScrollIndex.Location = new System.Drawing.Point(16, 77);
            this.ScrollIndex.Maximum = 0;
            this.ScrollIndex.Name = "ScrollIndex";
            this.ScrollIndex.Size = new System.Drawing.Size(305, 19);
            this.ScrollIndex.TabIndex = 6;
            this.ScrollIndex.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollIndex_Scroll);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(366, 543);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Quest";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GroupDone);
            this.groupBox1.Controls.Add(this.GroupMeanwhile);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 522);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Quest";
            // 
            // GroupDone
            // 
            this.GroupDone.Controls.Add(this.TextDone);
            this.GroupDone.Enabled = false;
            this.GroupDone.Location = new System.Drawing.Point(17, 183);
            this.GroupDone.Name = "GroupDone";
            this.GroupDone.Size = new System.Drawing.Size(305, 156);
            this.GroupDone.TabIndex = 6;
            this.GroupDone.TabStop = false;
            this.GroupDone.Text = "Done Chat";
            // 
            // TextDone
            // 
            this.TextDone.Location = new System.Drawing.Point(15, 22);
            this.TextDone.Multiline = true;
            this.TextDone.Name = "TextDone";
            this.TextDone.Size = new System.Drawing.Size(276, 118);
            this.TextDone.TabIndex = 21;
            this.TextDone.TextChanged += new System.EventHandler(this.TextDone_TextChanged);
            // 
            // GroupMeanwhile
            // 
            this.GroupMeanwhile.Controls.Add(this.TextMeanwhile);
            this.GroupMeanwhile.Enabled = false;
            this.GroupMeanwhile.Location = new System.Drawing.Point(16, 22);
            this.GroupMeanwhile.Name = "GroupMeanwhile";
            this.GroupMeanwhile.Size = new System.Drawing.Size(305, 156);
            this.GroupMeanwhile.TabIndex = 5;
            this.GroupMeanwhile.TabStop = false;
            this.GroupMeanwhile.Text = "Meanwhile Chat";
            // 
            // TextMeanwhile
            // 
            this.TextMeanwhile.Location = new System.Drawing.Point(15, 22);
            this.TextMeanwhile.Multiline = true;
            this.TextMeanwhile.Name = "TextMeanwhile";
            this.TextMeanwhile.Size = new System.Drawing.Size(276, 118);
            this.TextMeanwhile.TabIndex = 20;
            this.TextMeanwhile.TextChanged += new System.EventHandler(this.TextMeanwhile_TextChanged);
            // 
            // FormConversation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 611);
            this.Controls.Add(this.TabConversation);
            this.Controls.Add(this.GroupList);
            this.Controls.Add(this.MenuStrip);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormConversation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conversation Editor";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.GroupList.ResumeLayout(false);
            this.TabConversation.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.GroupData.ResumeLayout(false);
            this.GroupData.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.GroupEvent.ResumeLayout(false);
            this.GroupEvent.PerformLayout();
            this.GroupReply.ResumeLayout(false);
            this.GroupReply.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.GroupDone.ResumeLayout(false);
            this.GroupDone.PerformLayout();
            this.GroupMeanwhile.ResumeLayout(false);
            this.GroupMeanwhile.PerformLayout();
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
    private TabControl TabConversation;
    private TabPage tabPage1;
    private GroupBox GroupData;
    private TextBox TextQuest;
    private Label LabelPoint;
    private Label LabelType;
    private TextBox TextName;
    private Label LabelName;
    private TextBox TextId;
    private Label LabelId;
    private TabPage tabPage2;
    private TabPage tabPage3;
    private ComboBox ComboType;
    private GroupBox groupBox2;
    private GroupBox groupBox1;
    private GroupBox GroupEvent;
    private GroupBox GroupReply;
    private TextBox TextChat;
    private Label LabelIndex;
    private HScrollBar ScrollIndex;
    private ComboBox ComboReply_3;
    private TextBox TextReply_3;
    private ComboBox ComboReply_2;
    private TextBox TextReply_2;
    private ComboBox ComboReply_1;
    private TextBox TextReply_1;
    private ComboBox ComboReply_0;
    private TextBox TextReply_0;
    private TextBox TextData3;
    private TextBox TextData2;
    private TextBox TextData1;
    private ComboBox ComboEvent;
    private GroupBox GroupMeanwhile;
    private TextBox TextMeanwhile;
    private GroupBox GroupDone;
    private TextBox TextDone;
    private Button ButtonRemoveChat;
    private Button ButtonAddChat;
    private Label label3;
    private Label label2;
    private Label label1;
}
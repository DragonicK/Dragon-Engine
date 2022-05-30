namespace Crystalshire.Maps;

partial class FormProperty {
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProperty));
        this.GroupProperty = new System.Windows.Forms.GroupBox();
        this.GroupFog = new System.Windows.Forms.GroupBox();
        this.ComboBlending = new System.Windows.Forms.ComboBox();
        this.LabelBlending = new System.Windows.Forms.Label();
        this.LabelFogId = new System.Windows.Forms.Label();
        this.TextFogId = new System.Windows.Forms.TextBox();
        this.GroupWeather = new System.Windows.Forms.GroupBox();
        this.ComboWeather = new System.Windows.Forms.ComboBox();
        this.GroupMoral = new System.Windows.Forms.GroupBox();
        this.ComboMoral = new System.Windows.Forms.ComboBox();
        this.GroupMapLink = new System.Windows.Forms.GroupBox();
        this.TextDown = new System.Windows.Forms.TextBox();
        this.TextRight = new System.Windows.Forms.TextBox();
        this.TextLeft = new System.Windows.Forms.TextBox();
        this.TextUp = new System.Windows.Forms.TextBox();
        this.LabelName = new System.Windows.Forms.Label();
        this.TextName = new System.Windows.Forms.TextBox();
        this.ScrollOpacity = new System.Windows.Forms.HScrollBar();
        this.LabelOpacity = new System.Windows.Forms.Label();
        this.LabelRed = new System.Windows.Forms.Label();
        this.ScrollRed = new System.Windows.Forms.HScrollBar();
        this.LabelGreen = new System.Windows.Forms.Label();
        this.ScrollGreen = new System.Windows.Forms.HScrollBar();
        this.LabelBlue = new System.Windows.Forms.Label();
        this.ScrollBlue = new System.Windows.Forms.HScrollBar();
        this.GroupBoot = new System.Windows.Forms.GroupBox();
        this.TextBootId = new System.Windows.Forms.TextBox();
        this.LabelBootId = new System.Windows.Forms.Label();
        this.LabelX = new System.Windows.Forms.Label();
        this.TextBootX = new System.Windows.Forms.TextBox();
        this.LabelY = new System.Windows.Forms.Label();
        this.TextBootY = new System.Windows.Forms.TextBox();
        this.GroupSound = new System.Windows.Forms.GroupBox();
        this.LabelAmbience = new System.Windows.Forms.Label();
        this.TextAmbience = new System.Windows.Forms.TextBox();
        this.LabelMusic = new System.Windows.Forms.Label();
        this.TextMusic = new System.Windows.Forms.TextBox();
        this.GroupSize = new System.Windows.Forms.GroupBox();
        this.LabelHeight = new System.Windows.Forms.Label();
        this.TextHeight = new System.Windows.Forms.TextBox();
        this.LabelWidth = new System.Windows.Forms.Label();
        this.TextWidth = new System.Windows.Forms.TextBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.LabelKeyC = new System.Windows.Forms.Label();
        this.TextKeyC = new System.Windows.Forms.TextBox();
        this.LabelKeyB = new System.Windows.Forms.Label();
        this.TextKeyB = new System.Windows.Forms.TextBox();
        this.LabelKeyA = new System.Windows.Forms.Label();
        this.TextKeyA = new System.Windows.Forms.TextBox();
        this.ButtonCopy = new System.Windows.Forms.Button();
        this.GroupProperty.SuspendLayout();
        this.GroupFog.SuspendLayout();
        this.GroupWeather.SuspendLayout();
        this.GroupMoral.SuspendLayout();
        this.GroupMapLink.SuspendLayout();
        this.GroupBoot.SuspendLayout();
        this.GroupSound.SuspendLayout();
        this.GroupSize.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.SuspendLayout();
        // 
        // GroupProperty
        // 
        this.GroupProperty.Controls.Add(this.groupBox1);
        this.GroupProperty.Controls.Add(this.GroupSize);
        this.GroupProperty.Controls.Add(this.GroupSound);
        this.GroupProperty.Controls.Add(this.GroupBoot);
        this.GroupProperty.Controls.Add(this.GroupFog);
        this.GroupProperty.Controls.Add(this.GroupWeather);
        this.GroupProperty.Controls.Add(this.GroupMoral);
        this.GroupProperty.Controls.Add(this.GroupMapLink);
        this.GroupProperty.Controls.Add(this.LabelName);
        this.GroupProperty.Controls.Add(this.TextName);
        this.GroupProperty.Location = new System.Drawing.Point(12, 9);
        this.GroupProperty.Name = "GroupProperty";
        this.GroupProperty.Size = new System.Drawing.Size(615, 460);
        this.GroupProperty.TabIndex = 0;
        this.GroupProperty.TabStop = false;
        this.GroupProperty.Text = "Property";
        // 
        // GroupFog
        // 
        this.GroupFog.Controls.Add(this.LabelBlue);
        this.GroupFog.Controls.Add(this.ScrollBlue);
        this.GroupFog.Controls.Add(this.LabelGreen);
        this.GroupFog.Controls.Add(this.ScrollGreen);
        this.GroupFog.Controls.Add(this.LabelRed);
        this.GroupFog.Controls.Add(this.ScrollRed);
        this.GroupFog.Controls.Add(this.LabelOpacity);
        this.GroupFog.Controls.Add(this.ScrollOpacity);
        this.GroupFog.Controls.Add(this.ComboBlending);
        this.GroupFog.Controls.Add(this.LabelBlending);
        this.GroupFog.Controls.Add(this.LabelFogId);
        this.GroupFog.Controls.Add(this.TextFogId);
        this.GroupFog.Location = new System.Drawing.Point(21, 272);
        this.GroupFog.Name = "GroupFog";
        this.GroupFog.Size = new System.Drawing.Size(239, 170);
        this.GroupFog.TabIndex = 5;
        this.GroupFog.TabStop = false;
        this.GroupFog.Text = "Fog";
        // 
        // ComboBlending
        // 
        this.ComboBlending.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboBlending.FormattingEnabled = true;
        this.ComboBlending.Location = new System.Drawing.Point(14, 78);
        this.ComboBlending.Name = "ComboBlending";
        this.ComboBlending.Size = new System.Drawing.Size(98, 23);
        this.ComboBlending.TabIndex = 8;
        this.ComboBlending.SelectedIndexChanged += new System.EventHandler(this.ComboBlending_SelectedIndexChanged);
        // 
        // LabelBlending
        // 
        this.LabelBlending.AutoSize = true;
        this.LabelBlending.Location = new System.Drawing.Point(14, 60);
        this.LabelBlending.Name = "LabelBlending";
        this.LabelBlending.Size = new System.Drawing.Size(57, 15);
        this.LabelBlending.TabIndex = 7;
        this.LabelBlending.Text = "Blending:";
        // 
        // LabelFogId
        // 
        this.LabelFogId.AutoSize = true;
        this.LabelFogId.Location = new System.Drawing.Point(14, 16);
        this.LabelFogId.Name = "LabelFogId";
        this.LabelFogId.Size = new System.Drawing.Size(20, 15);
        this.LabelFogId.TabIndex = 6;
        this.LabelFogId.Text = "Id:";
        // 
        // TextFogId
        // 
        this.TextFogId.Location = new System.Drawing.Point(14, 34);
        this.TextFogId.Name = "TextFogId";
        this.TextFogId.Size = new System.Drawing.Size(98, 23);
        this.TextFogId.TabIndex = 5;
        this.TextFogId.Text = "0";
        this.TextFogId.TextChanged += new System.EventHandler(this.TextFogId_TextChanged);
        // 
        // GroupWeather
        // 
        this.GroupWeather.Controls.Add(this.ComboWeather);
        this.GroupWeather.Location = new System.Drawing.Point(20, 201);
        this.GroupWeather.Name = "GroupWeather";
        this.GroupWeather.Size = new System.Drawing.Size(239, 65);
        this.GroupWeather.TabIndex = 4;
        this.GroupWeather.TabStop = false;
        this.GroupWeather.Text = "Weather";
        // 
        // ComboWeather
        // 
        this.ComboWeather.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboWeather.FormattingEnabled = true;
        this.ComboWeather.Location = new System.Drawing.Point(15, 23);
        this.ComboWeather.Name = "ComboWeather";
        this.ComboWeather.Size = new System.Drawing.Size(210, 23);
        this.ComboWeather.TabIndex = 0;
        this.ComboWeather.SelectedIndexChanged += new System.EventHandler(this.ComboWeather_SelectedIndexChanged);
        // 
        // GroupMoral
        // 
        this.GroupMoral.Controls.Add(this.ComboMoral);
        this.GroupMoral.Location = new System.Drawing.Point(266, 69);
        this.GroupMoral.Name = "GroupMoral";
        this.GroupMoral.Size = new System.Drawing.Size(162, 65);
        this.GroupMoral.TabIndex = 3;
        this.GroupMoral.TabStop = false;
        this.GroupMoral.Text = "Moral";
        // 
        // ComboMoral
        // 
        this.ComboMoral.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ComboMoral.FormattingEnabled = true;
        this.ComboMoral.Location = new System.Drawing.Point(15, 23);
        this.ComboMoral.Name = "ComboMoral";
        this.ComboMoral.Size = new System.Drawing.Size(130, 23);
        this.ComboMoral.TabIndex = 0;
        this.ComboMoral.SelectedIndexChanged += new System.EventHandler(this.ComboMoral_SelectedIndexChanged);
        // 
        // GroupMapLink
        // 
        this.GroupMapLink.Controls.Add(this.TextDown);
        this.GroupMapLink.Controls.Add(this.TextRight);
        this.GroupMapLink.Controls.Add(this.TextLeft);
        this.GroupMapLink.Controls.Add(this.TextUp);
        this.GroupMapLink.Location = new System.Drawing.Point(20, 69);
        this.GroupMapLink.Name = "GroupMapLink";
        this.GroupMapLink.Size = new System.Drawing.Size(239, 126);
        this.GroupMapLink.TabIndex = 2;
        this.GroupMapLink.TabStop = false;
        this.GroupMapLink.Text = "Map Link";
        // 
        // TextDown
        // 
        this.TextDown.Location = new System.Drawing.Point(82, 86);
        this.TextDown.Name = "TextDown";
        this.TextDown.Size = new System.Drawing.Size(81, 23);
        this.TextDown.TabIndex = 7;
        this.TextDown.Text = "0";
        this.TextDown.TextChanged += new System.EventHandler(this.TextDown_TextChanged);
        // 
        // TextRight
        // 
        this.TextRight.Location = new System.Drawing.Point(124, 55);
        this.TextRight.Name = "TextRight";
        this.TextRight.Size = new System.Drawing.Size(81, 23);
        this.TextRight.TabIndex = 6;
        this.TextRight.Text = "0";
        this.TextRight.TextChanged += new System.EventHandler(this.TextRight_TextChanged);
        // 
        // TextLeft
        // 
        this.TextLeft.Location = new System.Drawing.Point(35, 55);
        this.TextLeft.Name = "TextLeft";
        this.TextLeft.Size = new System.Drawing.Size(81, 23);
        this.TextLeft.TabIndex = 5;
        this.TextLeft.Text = "0";
        this.TextLeft.TextChanged += new System.EventHandler(this.TextLeft_TextChanged);
        // 
        // TextUp
        // 
        this.TextUp.Location = new System.Drawing.Point(82, 23);
        this.TextUp.Name = "TextUp";
        this.TextUp.Size = new System.Drawing.Size(81, 23);
        this.TextUp.TabIndex = 4;
        this.TextUp.Text = "0";
        this.TextUp.TextChanged += new System.EventHandler(this.TextUp_TextChanged);
        // 
        // LabelName
        // 
        this.LabelName.AutoSize = true;
        this.LabelName.Location = new System.Drawing.Point(20, 19);
        this.LabelName.Name = "LabelName";
        this.LabelName.Size = new System.Drawing.Size(41, 15);
        this.LabelName.TabIndex = 1;
        this.LabelName.Text = "Name:";
        // 
        // TextName
        // 
        this.TextName.Location = new System.Drawing.Point(21, 40);
        this.TextName.Name = "TextName";
        this.TextName.Size = new System.Drawing.Size(575, 23);
        this.TextName.TabIndex = 0;
        this.TextName.TextChanged += new System.EventHandler(this.TextName_TextChanged);
        // 
        // ScrollOpacity
        // 
        this.ScrollOpacity.LargeChange = 1;
        this.ScrollOpacity.Location = new System.Drawing.Point(14, 125);
        this.ScrollOpacity.Name = "ScrollOpacity";
        this.ScrollOpacity.Size = new System.Drawing.Size(98, 17);
        this.ScrollOpacity.TabIndex = 9;
        this.ScrollOpacity.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollOpacity_Scroll);
        // 
        // LabelOpacity
        // 
        this.LabelOpacity.AutoSize = true;
        this.LabelOpacity.Location = new System.Drawing.Point(14, 105);
        this.LabelOpacity.Name = "LabelOpacity";
        this.LabelOpacity.Size = new System.Drawing.Size(70, 15);
        this.LabelOpacity.TabIndex = 10;
        this.LabelOpacity.Text = "Opacity: 0%";
        // 
        // LabelRed
        // 
        this.LabelRed.AutoSize = true;
        this.LabelRed.Location = new System.Drawing.Point(126, 20);
        this.LabelRed.Name = "LabelRed";
        this.LabelRed.Size = new System.Drawing.Size(39, 15);
        this.LabelRed.TabIndex = 12;
        this.LabelRed.Text = "Red: 0";
        // 
        // ScrollRed
        // 
        this.ScrollRed.LargeChange = 1;
        this.ScrollRed.Location = new System.Drawing.Point(126, 40);
        this.ScrollRed.Name = "ScrollRed";
        this.ScrollRed.Size = new System.Drawing.Size(98, 17);
        this.ScrollRed.TabIndex = 11;
        this.ScrollRed.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollRed_Scroll);
        // 
        // LabelGreen
        // 
        this.LabelGreen.AutoSize = true;
        this.LabelGreen.Location = new System.Drawing.Point(126, 64);
        this.LabelGreen.Name = "LabelGreen";
        this.LabelGreen.Size = new System.Drawing.Size(50, 15);
        this.LabelGreen.TabIndex = 14;
        this.LabelGreen.Text = "Green: 0";
        // 
        // ScrollGreen
        // 
        this.ScrollGreen.LargeChange = 1;
        this.ScrollGreen.Location = new System.Drawing.Point(126, 84);
        this.ScrollGreen.Name = "ScrollGreen";
        this.ScrollGreen.Size = new System.Drawing.Size(98, 17);
        this.ScrollGreen.TabIndex = 13;
        this.ScrollGreen.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollGreen_Scroll);
        // 
        // LabelBlue
        // 
        this.LabelBlue.AutoSize = true;
        this.LabelBlue.Location = new System.Drawing.Point(126, 105);
        this.LabelBlue.Name = "LabelBlue";
        this.LabelBlue.Size = new System.Drawing.Size(42, 15);
        this.LabelBlue.TabIndex = 16;
        this.LabelBlue.Text = "Blue: 0";
        // 
        // ScrollBlue
        // 
        this.ScrollBlue.LargeChange = 1;
        this.ScrollBlue.Location = new System.Drawing.Point(126, 125);
        this.ScrollBlue.Name = "ScrollBlue";
        this.ScrollBlue.Size = new System.Drawing.Size(98, 17);
        this.ScrollBlue.TabIndex = 15;
        this.ScrollBlue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBlue_Scroll);
        // 
        // GroupBoot
        // 
        this.GroupBoot.Controls.Add(this.LabelY);
        this.GroupBoot.Controls.Add(this.TextBootY);
        this.GroupBoot.Controls.Add(this.LabelX);
        this.GroupBoot.Controls.Add(this.TextBootX);
        this.GroupBoot.Controls.Add(this.LabelBootId);
        this.GroupBoot.Controls.Add(this.TextBootId);
        this.GroupBoot.Location = new System.Drawing.Point(266, 142);
        this.GroupBoot.Name = "GroupBoot";
        this.GroupBoot.Size = new System.Drawing.Size(162, 168);
        this.GroupBoot.TabIndex = 6;
        this.GroupBoot.TabStop = false;
        this.GroupBoot.Text = "Boot";
        // 
        // TextBootId
        // 
        this.TextBootId.Location = new System.Drawing.Point(15, 36);
        this.TextBootId.Name = "TextBootId";
        this.TextBootId.Size = new System.Drawing.Size(130, 23);
        this.TextBootId.TabIndex = 4;
        this.TextBootId.Text = "0";
        this.TextBootId.TextChanged += new System.EventHandler(this.TextBootId_TextChanged);
        // 
        // LabelBootId
        // 
        this.LabelBootId.AutoSize = true;
        this.LabelBootId.Location = new System.Drawing.Point(15, 18);
        this.LabelBootId.Name = "LabelBootId";
        this.LabelBootId.Size = new System.Drawing.Size(47, 15);
        this.LabelBootId.TabIndex = 8;
        this.LabelBootId.Text = "Map Id:";
        // 
        // LabelX
        // 
        this.LabelX.AutoSize = true;
        this.LabelX.Location = new System.Drawing.Point(15, 64);
        this.LabelX.Name = "LabelX";
        this.LabelX.Size = new System.Drawing.Size(17, 15);
        this.LabelX.TabIndex = 10;
        this.LabelX.Text = "X:";
        // 
        // TextBootX
        // 
        this.TextBootX.Location = new System.Drawing.Point(15, 82);
        this.TextBootX.Name = "TextBootX";
        this.TextBootX.Size = new System.Drawing.Size(130, 23);
        this.TextBootX.TabIndex = 9;
        this.TextBootX.Text = "0";
        this.TextBootX.TextChanged += new System.EventHandler(this.TextBootX_TextChanged);
        // 
        // LabelY
        // 
        this.LabelY.AutoSize = true;
        this.LabelY.Location = new System.Drawing.Point(15, 112);
        this.LabelY.Name = "LabelY";
        this.LabelY.Size = new System.Drawing.Size(16, 15);
        this.LabelY.TabIndex = 12;
        this.LabelY.Text = "Y:";
        // 
        // TextBootY
        // 
        this.TextBootY.Location = new System.Drawing.Point(15, 130);
        this.TextBootY.Name = "TextBootY";
        this.TextBootY.Size = new System.Drawing.Size(130, 23);
        this.TextBootY.TabIndex = 11;
        this.TextBootY.Text = "0";
        this.TextBootY.TextChanged += new System.EventHandler(this.TextBootY_TextChanged);
        // 
        // GroupSound
        // 
        this.GroupSound.Controls.Add(this.LabelAmbience);
        this.GroupSound.Controls.Add(this.TextAmbience);
        this.GroupSound.Controls.Add(this.LabelMusic);
        this.GroupSound.Controls.Add(this.TextMusic);
        this.GroupSound.Location = new System.Drawing.Point(266, 317);
        this.GroupSound.Name = "GroupSound";
        this.GroupSound.Size = new System.Drawing.Size(162, 125);
        this.GroupSound.TabIndex = 7;
        this.GroupSound.TabStop = false;
        this.GroupSound.Text = "Sound";
        // 
        // LabelAmbience
        // 
        this.LabelAmbience.AutoSize = true;
        this.LabelAmbience.Location = new System.Drawing.Point(15, 64);
        this.LabelAmbience.Name = "LabelAmbience";
        this.LabelAmbience.Size = new System.Drawing.Size(63, 15);
        this.LabelAmbience.TabIndex = 10;
        this.LabelAmbience.Text = "Ambience:";
        // 
        // TextAmbience
        // 
        this.TextAmbience.Location = new System.Drawing.Point(15, 82);
        this.TextAmbience.Name = "TextAmbience";
        this.TextAmbience.Size = new System.Drawing.Size(130, 23);
        this.TextAmbience.TabIndex = 9;
        this.TextAmbience.Text = "0";
        this.TextAmbience.TextChanged += new System.EventHandler(this.TextAmbience_TextChanged);
        // 
        // LabelMusic
        // 
        this.LabelMusic.AutoSize = true;
        this.LabelMusic.Location = new System.Drawing.Point(15, 18);
        this.LabelMusic.Name = "LabelMusic";
        this.LabelMusic.Size = new System.Drawing.Size(42, 15);
        this.LabelMusic.TabIndex = 8;
        this.LabelMusic.Text = "Music:";
        // 
        // TextMusic
        // 
        this.TextMusic.Location = new System.Drawing.Point(15, 36);
        this.TextMusic.Name = "TextMusic";
        this.TextMusic.Size = new System.Drawing.Size(130, 23);
        this.TextMusic.TabIndex = 4;
        this.TextMusic.Text = "0";
        this.TextMusic.TextChanged += new System.EventHandler(this.TextMusic_TextChanged);
        // 
        // GroupSize
        // 
        this.GroupSize.Controls.Add(this.LabelHeight);
        this.GroupSize.Controls.Add(this.TextHeight);
        this.GroupSize.Controls.Add(this.LabelWidth);
        this.GroupSize.Controls.Add(this.TextWidth);
        this.GroupSize.Location = new System.Drawing.Point(434, 76);
        this.GroupSize.Name = "GroupSize";
        this.GroupSize.Size = new System.Drawing.Size(162, 125);
        this.GroupSize.TabIndex = 8;
        this.GroupSize.TabStop = false;
        this.GroupSize.Text = "Size";
        // 
        // LabelHeight
        // 
        this.LabelHeight.AutoSize = true;
        this.LabelHeight.Location = new System.Drawing.Point(15, 64);
        this.LabelHeight.Name = "LabelHeight";
        this.LabelHeight.Size = new System.Drawing.Size(46, 15);
        this.LabelHeight.TabIndex = 10;
        this.LabelHeight.Text = "Height:";
        // 
        // TextHeight
        // 
        this.TextHeight.Location = new System.Drawing.Point(15, 82);
        this.TextHeight.Name = "TextHeight";
        this.TextHeight.Size = new System.Drawing.Size(130, 23);
        this.TextHeight.TabIndex = 9;
        this.TextHeight.Text = "0";
        this.TextHeight.TextChanged += new System.EventHandler(this.TextHeight_TextChanged);
        // 
        // LabelWidth
        // 
        this.LabelWidth.AutoSize = true;
        this.LabelWidth.Location = new System.Drawing.Point(15, 18);
        this.LabelWidth.Name = "LabelWidth";
        this.LabelWidth.Size = new System.Drawing.Size(42, 15);
        this.LabelWidth.TabIndex = 8;
        this.LabelWidth.Text = "Width:";
        // 
        // TextWidth
        // 
        this.TextWidth.Location = new System.Drawing.Point(15, 36);
        this.TextWidth.Name = "TextWidth";
        this.TextWidth.Size = new System.Drawing.Size(130, 23);
        this.TextWidth.TabIndex = 4;
        this.TextWidth.Text = "0";
        this.TextWidth.TextChanged += new System.EventHandler(this.TextWidth_TextChanged);
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.ButtonCopy);
        this.groupBox1.Controls.Add(this.LabelKeyC);
        this.groupBox1.Controls.Add(this.TextKeyC);
        this.groupBox1.Controls.Add(this.LabelKeyB);
        this.groupBox1.Controls.Add(this.TextKeyB);
        this.groupBox1.Controls.Add(this.LabelKeyA);
        this.groupBox1.Controls.Add(this.TextKeyA);
        this.groupBox1.Location = new System.Drawing.Point(434, 208);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(162, 234);
        this.groupBox1.TabIndex = 9;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Passwords";
        // 
        // LabelKeyC
        // 
        this.LabelKeyC.AutoSize = true;
        this.LabelKeyC.Location = new System.Drawing.Point(15, 112);
        this.LabelKeyC.Name = "LabelKeyC";
        this.LabelKeyC.Size = new System.Drawing.Size(39, 15);
        this.LabelKeyC.TabIndex = 12;
        this.LabelKeyC.Text = "Key C:";
        // 
        // TextKeyC
        // 
        this.TextKeyC.Location = new System.Drawing.Point(15, 130);
        this.TextKeyC.Name = "TextKeyC";
        this.TextKeyC.Size = new System.Drawing.Size(130, 23);
        this.TextKeyC.TabIndex = 11;
        this.TextKeyC.Text = "0";
        this.TextKeyC.TextChanged += new System.EventHandler(this.TextKeyC_TextChanged);
        // 
        // LabelKeyB
        // 
        this.LabelKeyB.AutoSize = true;
        this.LabelKeyB.Location = new System.Drawing.Point(15, 64);
        this.LabelKeyB.Name = "LabelKeyB";
        this.LabelKeyB.Size = new System.Drawing.Size(39, 15);
        this.LabelKeyB.TabIndex = 10;
        this.LabelKeyB.Text = "Key B:";
        // 
        // TextKeyB
        // 
        this.TextKeyB.Location = new System.Drawing.Point(15, 82);
        this.TextKeyB.Name = "TextKeyB";
        this.TextKeyB.Size = new System.Drawing.Size(130, 23);
        this.TextKeyB.TabIndex = 9;
        this.TextKeyB.Text = "0";
        this.TextKeyB.TextChanged += new System.EventHandler(this.TextKeyB_TextChanged);
        // 
        // LabelKeyA
        // 
        this.LabelKeyA.AutoSize = true;
        this.LabelKeyA.Location = new System.Drawing.Point(15, 18);
        this.LabelKeyA.Name = "LabelKeyA";
        this.LabelKeyA.Size = new System.Drawing.Size(40, 15);
        this.LabelKeyA.TabIndex = 8;
        this.LabelKeyA.Text = "Key A:";
        // 
        // TextKeyA
        // 
        this.TextKeyA.Location = new System.Drawing.Point(15, 36);
        this.TextKeyA.Name = "TextKeyA";
        this.TextKeyA.Size = new System.Drawing.Size(130, 23);
        this.TextKeyA.TabIndex = 4;
        this.TextKeyA.Text = "0";
        this.TextKeyA.TextChanged += new System.EventHandler(this.TextKeyA_TextChanged);
        // 
        // ButtonCopy
        // 
        this.ButtonCopy.Location = new System.Drawing.Point(15, 169);
        this.ButtonCopy.Name = "ButtonCopy";
        this.ButtonCopy.Size = new System.Drawing.Size(130, 45);
        this.ButtonCopy.TabIndex = 13;
        this.ButtonCopy.Text = "Copy Key To Clipboard";
        this.ButtonCopy.UseVisualStyleBackColor = true;
        this.ButtonCopy.Click += new System.EventHandler(this.ButtonCopy_Click);
        // 
        // FormProperty
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(640, 481);
        this.Controls.Add(this.GroupProperty);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.Name = "FormProperty";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Property";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProperty_FormClosing);
        this.GroupProperty.ResumeLayout(false);
        this.GroupProperty.PerformLayout();
        this.GroupFog.ResumeLayout(false);
        this.GroupFog.PerformLayout();
        this.GroupWeather.ResumeLayout(false);
        this.GroupMoral.ResumeLayout(false);
        this.GroupMapLink.ResumeLayout(false);
        this.GroupMapLink.PerformLayout();
        this.GroupBoot.ResumeLayout(false);
        this.GroupBoot.PerformLayout();
        this.GroupSound.ResumeLayout(false);
        this.GroupSound.PerformLayout();
        this.GroupSize.ResumeLayout(false);
        this.GroupSize.PerformLayout();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.ResumeLayout(false);

    }

    #endregion

    private GroupBox GroupProperty;
    private Label LabelName;
    private TextBox TextName;
    private GroupBox GroupMapLink;
    private TextBox TextDown;
    private TextBox TextRight;
    private TextBox TextLeft;
    private TextBox TextUp;
    private GroupBox GroupMoral;
    private ComboBox ComboMoral;
    private GroupBox GroupWeather;
    private ComboBox ComboWeather;
    private GroupBox GroupFog;
    private Label LabelFogId;
    private TextBox TextFogId;
    private ComboBox ComboBlending;
    private Label LabelBlending;
    private Label LabelOpacity;
    private HScrollBar ScrollOpacity;
    private Label LabelBlue;
    private HScrollBar ScrollBlue;
    private Label LabelGreen;
    private HScrollBar ScrollGreen;
    private Label LabelRed;
    private HScrollBar ScrollRed;
    private GroupBox GroupBoot;
    private TextBox TextBootId;
    private Label LabelY;
    private TextBox TextBootY;
    private Label LabelX;
    private TextBox TextBootX;
    private Label LabelBootId;
    private GroupBox GroupSound;
    private Label LabelAmbience;
    private TextBox TextAmbience;
    private Label LabelMusic;
    private TextBox TextMusic;
    private GroupBox GroupSize;
    private Label LabelHeight;
    private TextBox TextHeight;
    private Label LabelWidth;
    private TextBox TextWidth;
    private GroupBox groupBox1;
    private Label LabelKeyC;
    private TextBox TextKeyC;
    private Label LabelKeyB;
    private TextBox TextKeyB;
    private Label LabelKeyA;
    private TextBox TextKeyA;
    private Button ButtonCopy;
}
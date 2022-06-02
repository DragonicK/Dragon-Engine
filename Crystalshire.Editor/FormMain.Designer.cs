namespace Crystalshire.Editor;

partial class FormMain {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuContent = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAchievements = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAchievement = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAchievementAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuConversation = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEffects = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEffect = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEffectAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEffectUpgrade = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEquipments = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEquipment = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEquipmentAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEquipmentUpgrade = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEquipmentSets = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEquipmentSet = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEquipmentSetAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHeraldries = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHeraldry = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHeraldryAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHeraldryUpgrade = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuInformationIcon = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuNpcs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuNpc = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuNpcAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPassives = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPassive = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPassiveAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPassiveUpgrade = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuQuest = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuRecipe = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTitles = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTitleAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSkill = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.GroupProject = new System.Windows.Forms.GroupBox();
            this.ButtonServerPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TextServerPath = new System.Windows.Forms.TextBox();
            this.GroupOutput = new System.Windows.Forms.GroupBox();
            this.ButtonClienteOutput = new System.Windows.Forms.Button();
            this.ButtonServerOutput = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TextClientOutput = new System.Windows.Forms.TextBox();
            this.TextServerOutput = new System.Windows.Forms.TextBox();
            this.MenuStrip.SuspendLayout();
            this.GroupProject.SuspendLayout();
            this.GroupOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuExit});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(47, 20);
            this.MenuFile.Text = "File";
            // 
            // MenuExit
            // 
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(102, 22);
            this.MenuExit.Text = "Exit";
            this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // MenuContent
            // 
            this.MenuContent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAchievements,
            this.MenuConversation,
            this.MenuEffects,
            this.MenuEquipments,
            this.MenuEquipmentSets,
            this.MenuHeraldries,
            this.MenuInformationIcon,
            this.MenuItem,
            this.MenuNpcs,
            this.MenuPassives,
            this.MenuQuest,
            this.MenuRecipe,
            this.MenuTitles,
            this.MenuSkill});
            this.MenuContent.Name = "MenuContent";
            this.MenuContent.Size = new System.Drawing.Size(68, 20);
            this.MenuContent.Text = "Content";
            // 
            // MenuAchievements
            // 
            this.MenuAchievements.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAchievement,
            this.MenuAchievementAttribute});
            this.MenuAchievements.Name = "MenuAchievements";
            this.MenuAchievements.Size = new System.Drawing.Size(186, 22);
            this.MenuAchievements.Text = "Achievement";
            // 
            // MenuAchievement
            // 
            this.MenuAchievement.Name = "MenuAchievement";
            this.MenuAchievement.Size = new System.Drawing.Size(137, 22);
            this.MenuAchievement.Text = "Editor";
            this.MenuAchievement.Click += new System.EventHandler(this.MenuAchievement_Click);
            // 
            // MenuAchievementAttribute
            // 
            this.MenuAchievementAttribute.Name = "MenuAchievementAttribute";
            this.MenuAchievementAttribute.Size = new System.Drawing.Size(137, 22);
            this.MenuAchievementAttribute.Text = "Attribute";
            this.MenuAchievementAttribute.Click += new System.EventHandler(this.MenuAchievementAttribute_Click);
            // 
            // MenuConversation
            // 
            this.MenuConversation.Name = "MenuConversation";
            this.MenuConversation.Size = new System.Drawing.Size(186, 22);
            this.MenuConversation.Text = "Conversation";
            this.MenuConversation.Click += new System.EventHandler(this.MenuConversation_Click);
            // 
            // MenuEffects
            // 
            this.MenuEffects.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuEffect,
            this.MenuEffectAttribute,
            this.MenuEffectUpgrade});
            this.MenuEffects.Name = "MenuEffects";
            this.MenuEffects.Size = new System.Drawing.Size(186, 22);
            this.MenuEffects.Text = "Effect";
            // 
            // MenuEffect
            // 
            this.MenuEffect.Name = "MenuEffect";
            this.MenuEffect.Size = new System.Drawing.Size(137, 22);
            this.MenuEffect.Text = "Editor";
            this.MenuEffect.Click += new System.EventHandler(this.MenuEffect_Click);
            // 
            // MenuEffectAttribute
            // 
            this.MenuEffectAttribute.Name = "MenuEffectAttribute";
            this.MenuEffectAttribute.Size = new System.Drawing.Size(137, 22);
            this.MenuEffectAttribute.Text = "Attribute";
            this.MenuEffectAttribute.Click += new System.EventHandler(this.MenuEffectAttribute_Click);
            // 
            // MenuEffectUpgrade
            // 
            this.MenuEffectUpgrade.Name = "MenuEffectUpgrade";
            this.MenuEffectUpgrade.Size = new System.Drawing.Size(137, 22);
            this.MenuEffectUpgrade.Text = "Upgrade";
            this.MenuEffectUpgrade.Click += new System.EventHandler(this.MenuEffectUpgrade_Click);
            // 
            // MenuEquipments
            // 
            this.MenuEquipments.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuEquipment,
            this.MenuEquipmentAttribute,
            this.MenuEquipmentUpgrade});
            this.MenuEquipments.Name = "MenuEquipments";
            this.MenuEquipments.Size = new System.Drawing.Size(186, 22);
            this.MenuEquipments.Text = "Equipment";
            // 
            // MenuEquipment
            // 
            this.MenuEquipment.Name = "MenuEquipment";
            this.MenuEquipment.Size = new System.Drawing.Size(137, 22);
            this.MenuEquipment.Text = "Editor";
            this.MenuEquipment.Click += new System.EventHandler(this.MenuEquipment_Click);
            // 
            // MenuEquipmentAttribute
            // 
            this.MenuEquipmentAttribute.Name = "MenuEquipmentAttribute";
            this.MenuEquipmentAttribute.Size = new System.Drawing.Size(137, 22);
            this.MenuEquipmentAttribute.Text = "Attribute";
            this.MenuEquipmentAttribute.Click += new System.EventHandler(this.MenuEquipmentAttribute_Click);
            // 
            // MenuEquipmentUpgrade
            // 
            this.MenuEquipmentUpgrade.Name = "MenuEquipmentUpgrade";
            this.MenuEquipmentUpgrade.Size = new System.Drawing.Size(137, 22);
            this.MenuEquipmentUpgrade.Text = "Upgrade";
            this.MenuEquipmentUpgrade.Click += new System.EventHandler(this.MenuEquipmentUpgrade_Click);
            // 
            // MenuEquipmentSets
            // 
            this.MenuEquipmentSets.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuEquipmentSet,
            this.MenuEquipmentSetAttribute});
            this.MenuEquipmentSets.Name = "MenuEquipmentSets";
            this.MenuEquipmentSets.Size = new System.Drawing.Size(186, 22);
            this.MenuEquipmentSets.Text = "Equipment Set";
            // 
            // MenuEquipmentSet
            // 
            this.MenuEquipmentSet.Name = "MenuEquipmentSet";
            this.MenuEquipmentSet.Size = new System.Drawing.Size(137, 22);
            this.MenuEquipmentSet.Text = "Editor";
            this.MenuEquipmentSet.Click += new System.EventHandler(this.MenuEquipmentSet_Click);
            // 
            // MenuEquipmentSetAttribute
            // 
            this.MenuEquipmentSetAttribute.Name = "MenuEquipmentSetAttribute";
            this.MenuEquipmentSetAttribute.Size = new System.Drawing.Size(137, 22);
            this.MenuEquipmentSetAttribute.Text = "Attribute";
            this.MenuEquipmentSetAttribute.Click += new System.EventHandler(this.MenuEquipmentSetAttribute_Click);
            // 
            // MenuHeraldries
            // 
            this.MenuHeraldries.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuHeraldry,
            this.MenuHeraldryAttribute,
            this.MenuHeraldryUpgrade});
            this.MenuHeraldries.Name = "MenuHeraldries";
            this.MenuHeraldries.Size = new System.Drawing.Size(186, 22);
            this.MenuHeraldries.Text = "Heraldry";
            // 
            // MenuHeraldry
            // 
            this.MenuHeraldry.Name = "MenuHeraldry";
            this.MenuHeraldry.Size = new System.Drawing.Size(137, 22);
            this.MenuHeraldry.Text = "Editor";
            this.MenuHeraldry.Click += new System.EventHandler(this.MenuHeraldry_Click);
            // 
            // MenuHeraldryAttribute
            // 
            this.MenuHeraldryAttribute.Name = "MenuHeraldryAttribute";
            this.MenuHeraldryAttribute.Size = new System.Drawing.Size(137, 22);
            this.MenuHeraldryAttribute.Text = "Attribute";
            this.MenuHeraldryAttribute.Click += new System.EventHandler(this.MenuHeraldryAttribute_Click);
            // 
            // MenuHeraldryUpgrade
            // 
            this.MenuHeraldryUpgrade.Name = "MenuHeraldryUpgrade";
            this.MenuHeraldryUpgrade.Size = new System.Drawing.Size(137, 22);
            this.MenuHeraldryUpgrade.Text = "Upgrade";
            this.MenuHeraldryUpgrade.Click += new System.EventHandler(this.MenuHeraldryUpgrade_Click);
            // 
            // MenuInformationIcon
            // 
            this.MenuInformationIcon.Name = "MenuInformationIcon";
            this.MenuInformationIcon.Size = new System.Drawing.Size(186, 22);
            this.MenuInformationIcon.Text = "Information Icon";
            this.MenuInformationIcon.Click += new System.EventHandler(this.MenuInformationIcon_Click);
            // 
            // MenuItem
            // 
            this.MenuItem.Name = "MenuItem";
            this.MenuItem.Size = new System.Drawing.Size(186, 22);
            this.MenuItem.Text = "Item";
            this.MenuItem.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // MenuNpcs
            // 
            this.MenuNpcs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuNpc,
            this.MenuNpcAttribute});
            this.MenuNpcs.Name = "MenuNpcs";
            this.MenuNpcs.Size = new System.Drawing.Size(186, 22);
            this.MenuNpcs.Text = "Npc";
            // 
            // MenuNpc
            // 
            this.MenuNpc.Name = "MenuNpc";
            this.MenuNpc.Size = new System.Drawing.Size(137, 22);
            this.MenuNpc.Text = "Editor";
            this.MenuNpc.Click += new System.EventHandler(this.MenuNpc_Click);
            // 
            // MenuNpcAttribute
            // 
            this.MenuNpcAttribute.Name = "MenuNpcAttribute";
            this.MenuNpcAttribute.Size = new System.Drawing.Size(137, 22);
            this.MenuNpcAttribute.Text = "Attribute";
            this.MenuNpcAttribute.Click += new System.EventHandler(this.MenuNpcAttribute_Click);
            // 
            // MenuPassives
            // 
            this.MenuPassives.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuPassive,
            this.MenuPassiveAttribute,
            this.MenuPassiveUpgrade});
            this.MenuPassives.Name = "MenuPassives";
            this.MenuPassives.Size = new System.Drawing.Size(186, 22);
            this.MenuPassives.Text = "Passive";
            // 
            // MenuPassive
            // 
            this.MenuPassive.Name = "MenuPassive";
            this.MenuPassive.Size = new System.Drawing.Size(137, 22);
            this.MenuPassive.Text = "Editor";
            this.MenuPassive.Click += new System.EventHandler(this.MenuPassive_Click);
            // 
            // MenuPassiveAttribute
            // 
            this.MenuPassiveAttribute.Name = "MenuPassiveAttribute";
            this.MenuPassiveAttribute.Size = new System.Drawing.Size(137, 22);
            this.MenuPassiveAttribute.Text = "Attribute";
            this.MenuPassiveAttribute.Click += new System.EventHandler(this.MenuPassiveAttribute_Click);
            // 
            // MenuPassiveUpgrade
            // 
            this.MenuPassiveUpgrade.Name = "MenuPassiveUpgrade";
            this.MenuPassiveUpgrade.Size = new System.Drawing.Size(137, 22);
            this.MenuPassiveUpgrade.Text = "Upgrade";
            this.MenuPassiveUpgrade.Click += new System.EventHandler(this.MenuPassiveUpgrade_Click);
            // 
            // MenuQuest
            // 
            this.MenuQuest.Name = "MenuQuest";
            this.MenuQuest.Size = new System.Drawing.Size(186, 22);
            this.MenuQuest.Text = "Quest";
            this.MenuQuest.Click += new System.EventHandler(this.MenuQuest_Click);
            // 
            // MenuRecipe
            // 
            this.MenuRecipe.Name = "MenuRecipe";
            this.MenuRecipe.Size = new System.Drawing.Size(186, 22);
            this.MenuRecipe.Text = "Recipe";
            this.MenuRecipe.Click += new System.EventHandler(this.MenuRecipe_Click);
            // 
            // MenuTitles
            // 
            this.MenuTitles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuTitle,
            this.MenuTitleAttribute});
            this.MenuTitles.Name = "MenuTitles";
            this.MenuTitles.Size = new System.Drawing.Size(186, 22);
            this.MenuTitles.Text = "Title";
            // 
            // MenuTitle
            // 
            this.MenuTitle.Name = "MenuTitle";
            this.MenuTitle.Size = new System.Drawing.Size(137, 22);
            this.MenuTitle.Text = "Editor";
            this.MenuTitle.Click += new System.EventHandler(this.MenuTitle_Click);
            // 
            // MenuTitleAttribute
            // 
            this.MenuTitleAttribute.Name = "MenuTitleAttribute";
            this.MenuTitleAttribute.Size = new System.Drawing.Size(137, 22);
            this.MenuTitleAttribute.Text = "Attribute";
            this.MenuTitleAttribute.Click += new System.EventHandler(this.MenuTitleAttribute_Click);
            // 
            // MenuSkill
            // 
            this.MenuSkill.Name = "MenuSkill";
            this.MenuSkill.Size = new System.Drawing.Size(186, 22);
            this.MenuSkill.Text = "Skill";
            this.MenuSkill.Click += new System.EventHandler(this.MenuSkill_Click);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuContent});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(385, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // GroupProject
            // 
            this.GroupProject.Controls.Add(this.ButtonServerPath);
            this.GroupProject.Controls.Add(this.label1);
            this.GroupProject.Controls.Add(this.TextServerPath);
            this.GroupProject.Location = new System.Drawing.Point(12, 27);
            this.GroupProject.Name = "GroupProject";
            this.GroupProject.Size = new System.Drawing.Size(360, 112);
            this.GroupProject.TabIndex = 1;
            this.GroupProject.TabStop = false;
            this.GroupProject.Text = "Project Path";
            // 
            // ButtonServerPath
            // 
            this.ButtonServerPath.Location = new System.Drawing.Point(285, 46);
            this.ButtonServerPath.Name = "ButtonServerPath";
            this.ButtonServerPath.Size = new System.Drawing.Size(47, 24);
            this.ButtonServerPath.TabIndex = 4;
            this.ButtonServerPath.Text = "...";
            this.ButtonServerPath.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server:";
            // 
            // TextServerPath
            // 
            this.TextServerPath.Location = new System.Drawing.Point(27, 47);
            this.TextServerPath.Name = "TextServerPath";
            this.TextServerPath.Size = new System.Drawing.Size(257, 23);
            this.TextServerPath.TabIndex = 0;
            // 
            // GroupOutput
            // 
            this.GroupOutput.Controls.Add(this.ButtonClienteOutput);
            this.GroupOutput.Controls.Add(this.ButtonServerOutput);
            this.GroupOutput.Controls.Add(this.label3);
            this.GroupOutput.Controls.Add(this.label4);
            this.GroupOutput.Controls.Add(this.TextClientOutput);
            this.GroupOutput.Controls.Add(this.TextServerOutput);
            this.GroupOutput.Location = new System.Drawing.Point(13, 145);
            this.GroupOutput.Name = "GroupOutput";
            this.GroupOutput.Size = new System.Drawing.Size(360, 140);
            this.GroupOutput.TabIndex = 2;
            this.GroupOutput.TabStop = false;
            this.GroupOutput.Text = "Content Output Path";
            // 
            // ButtonClienteOutput
            // 
            this.ButtonClienteOutput.Location = new System.Drawing.Point(285, 91);
            this.ButtonClienteOutput.Name = "ButtonClienteOutput";
            this.ButtonClienteOutput.Size = new System.Drawing.Size(47, 24);
            this.ButtonClienteOutput.TabIndex = 5;
            this.ButtonClienteOutput.Text = "...";
            this.ButtonClienteOutput.UseVisualStyleBackColor = true;
            // 
            // ButtonServerOutput
            // 
            this.ButtonServerOutput.Location = new System.Drawing.Point(285, 46);
            this.ButtonServerOutput.Name = "ButtonServerOutput";
            this.ButtonServerOutput.Size = new System.Drawing.Size(47, 24);
            this.ButtonServerOutput.TabIndex = 4;
            this.ButtonServerOutput.Text = "...";
            this.ButtonServerOutput.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Cliente:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Server:";
            // 
            // TextClientOutput
            // 
            this.TextClientOutput.Location = new System.Drawing.Point(27, 91);
            this.TextClientOutput.Name = "TextClientOutput";
            this.TextClientOutput.Size = new System.Drawing.Size(257, 23);
            this.TextClientOutput.TabIndex = 1;
            // 
            // TextServerOutput
            // 
            this.TextServerOutput.Location = new System.Drawing.Point(27, 47);
            this.TextServerOutput.Name = "TextServerOutput";
            this.TextServerOutput.Size = new System.Drawing.Size(257, 23);
            this.TextServerOutput.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 327);
            this.Controls.Add(this.GroupOutput);
            this.Controls.Add(this.GroupProject);
            this.Controls.Add(this.MenuStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Editor";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.GroupProject.ResumeLayout(false);
            this.GroupProject.PerformLayout();
            this.GroupOutput.ResumeLayout(false);
            this.GroupOutput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private ToolStripMenuItem MenuFile;
    private ToolStripMenuItem MenuContent;
    private ToolStripMenuItem MenuAchievements;
    private ToolStripMenuItem MenuAchievement;
    private ToolStripMenuItem MenuAchievementAttribute;
    private ToolStripMenuItem MenuTitles;
    private ToolStripMenuItem MenuTitle;
    private ToolStripMenuItem MenuTitleAttribute;
    private ToolStripMenuItem MenuEffects;
    private ToolStripMenuItem MenuEffect;
    private ToolStripMenuItem MenuEffectAttribute;
    private ToolStripMenuItem MenuEffectUpgrade;
    private ToolStripMenuItem MenuRecipe;
    private ToolStripMenuItem MenuPassives;
    private ToolStripMenuItem MenuPassive;
    private ToolStripMenuItem MenuPassiveAttribute;
    private ToolStripMenuItem MenuPassiveUpgrade;
    private MenuStrip MenuStrip;
    private GroupBox GroupProject;
    private TextBox TextServerPath;
    private Label label1;
    private Button ButtonServerPath;
    private ToolStripMenuItem MenuEquipmentSets;
    private ToolStripMenuItem MenuEquipmentSet;
    private ToolStripMenuItem MenuEquipmentSetAttribute;
    private ToolStripMenuItem MenuHeraldries;
    private ToolStripMenuItem MenuHeraldry;
    private ToolStripMenuItem MenuHeraldryAttribute;
    private ToolStripMenuItem MenuHeraldryUpgrade;
    private ToolStripMenuItem MenuExit;
    private GroupBox GroupOutput;
    private Button ButtonClienteOutput;
    private Button ButtonServerOutput;
    private Label label3;
    private Label label4;
    private TextBox TextClientOutput;
    private TextBox TextServerOutput;
    private ToolStripMenuItem MenuEquipments;
    private ToolStripMenuItem MenuEquipment;
    private ToolStripMenuItem MenuEquipmentAttribute;
    private ToolStripMenuItem MenuEquipmentUpgrade;
    private ToolStripMenuItem MenuSkill;
    private ToolStripMenuItem MenuItem;
    private ToolStripMenuItem MenuNpcs;
    private ToolStripMenuItem MenuNpc;
    private ToolStripMenuItem MenuNpcAttribute;
    private ToolStripMenuItem MenuInformationIcon;
    private ToolStripMenuItem MenuConversation;
    private ToolStripMenuItem MenuQuest;
}
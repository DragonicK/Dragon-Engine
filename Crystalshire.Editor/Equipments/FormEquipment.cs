using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Equipments;

using Crystalshire.Editor.Common;

namespace Crystalshire.Editor.Equipments;

public partial class FormEquipment : Form {
    public IDatabase<Equipment> Database { get; }
    public Equipment? Element { get; private set; }
    public Configuration Configuration { get; private set; }
    public int SelectedIndex { get; private set; } = Util.NotSelected;

    public FormEquipment(Configuration configuration, IDatabase<Equipment> database) {
        InitializeComponent();

        SetEnabled(false);

        Util.FillComboBox<EquipmentType>(ComboType);
        Util.FillComboBox<EquipmentProficiency>(ComboProfeciency);
        Util.FillComboBox<EquipmentHandStyle>(ComboHandStyle);

        Database = database;
        Configuration = configuration;
        Element = null;

        Util.UpdateList(Database, ListIndex);
    }

    private void MenuSave_Click(object sender, EventArgs e) {
        var folder = Database.Folder;

        Database.Save();

        Database.Folder = Configuration.OutputClientPath;

        Database.Save();

        Database.Folder = Configuration.OutputServerPath;

        Database.Save();

        Database.Folder = folder;

        MessageBox.Show("Saved");
    }

    private void MenuExit_Click(object sender, EventArgs e) {
        Close();
    }

    private void Initialize() {
        if (Element is not null) {
            TextId.Text = Element.Id.ToString();
            TextName.Text = Element.Name;
            ComboType.SelectedIndex = (int)Element.Type;
            ComboProfeciency.SelectedIndex = (int)Element.Proficiency;
            ComboHandStyle.SelectedIndex = (int)Element.HandStyle;
            //ComboSound
            ScrollBaseAttack.Value = Element.BaseAttackSpeed;
            LabelAttackSpeed.Text = $"Weapon Base Attack Speed: {Element.BaseAttackSpeed}%";
            ScrollSockets.Value = Element.MaximumSocket;
            LabelSockets.Text = $"Maximum Sockets: {Element.MaximumSocket}";
            TextCostumeModel.Text = Element.ModelId.ToString();
            TextDisassembleId.Text = Element.DisassembleId.ToString();

            UpdateSkillList();
            UpdateIndexLabel();
            UpdateAttributeText();

            TextEquipmentSetId.Text = Element.EquipmentSetId.ToString();
            TextUpgradeId.Text = Element.UpgradeId.ToString();
        }
    }

    private void Clear() {
        TextId.Text = "0";
        TextName.Text = string.Empty;
        ComboType.SelectedIndex = 0;
        ComboProfeciency.SelectedIndex = 0;
        ComboHandStyle.SelectedIndex = 0;
        //ComboClass
        //ComboSound
        ScrollBaseAttack.Value = 0;
        LabelAttackSpeed.Text = $"Weapon Base Attack Speed: 0%";
        ScrollSockets.Value = 0;
        LabelSockets.Text = $"Maximum Sockets: 0";
        TextCostumeModel.Text = "0";
        TextDisassembleId.Text = "0";

        Util.FillTextBoxWithZero(GroupNewSkill);
        ListSkill.Items.Clear();

        LabelIndex.Text = "Index: 0 / 0";
        ScrollIndex.Minimum = 0;
        ScrollIndex.Value = 0;
        ScrollIndex.Maximum = 0;
        TextAttributeId.Text = "0";
        LabelChance.Text = "Chance: 0%";
        ScrollChance.Value = 0;

        TextEquipmentSetId.Text = "0";
        TextUpgradeId.Text = "0";
    }

    private void SetEnabled(bool enabled) {
        TabEquipment.Enabled = enabled;
    }

    #region Add, Delete, Clear

    private void ButtonAdd_Click(object sender, EventArgs e) {
        for (var i = 1; i <= int.MaxValue; i++) {
            if (!Database.Contains(i)) {

                Database.Add(i, new Equipment() { Id = i });

                Util.UpdateList(Database, ListIndex);

                return;
            }
        }
    }

    private void ButtonDelete_Click(object sender, EventArgs e) {
        if (Element is not null) {
            if (Element.Id > 0) {
                Database.Remove(Element.Id);

                SetEnabled(false);
                Element = null;

                Clear();

                SelectedIndex = Util.NotSelected;

                Util.UpdateList(Database, ListIndex);
            }
        }
    }

    private void ButtonClear_Click(object sender, EventArgs e) {
        SelectedIndex = Util.NotSelected;
        ListIndex.Items.Clear();
        SetEnabled(false);
        Database.Clear();
        Element = null;
        Clear();
    }

    #endregion

    #region List Index

    private void ListIndex_Click(object sender, EventArgs e) {
        if (ListIndex.Items.Count > 0) {
            var index = ListIndex.SelectedIndex;

            if (index > Util.NotSelected) {
                var selected = ListIndex.Items[index].ToString();

                if (selected is not null) {
                    var id = Util.GetListSelectedId(selected);

                    if (id > 0) {
                        SelectedIndex = index;
                        Element = Database[id];
                        SetEnabled(true);
                        Initialize();
                    }
                }
            }
        }
    }

    #endregion

    #region Equipment Data

    private void TextId_Validated(object sender, EventArgs e) {
        if (Element is not null) {
            var lastId = Element.Id;
            var id = Util.GetValue(TextId);

            if (id > 0) {
                if (id == lastId) {
                    return;
                }

                if (Database.Contains(id)) {
                    MessageBox.Show($"The Id {id} is already in use.");
                }
                else {
                    Database.Remove(Element.Id);
                    Element.Id = id;
                    Database.Add(id, Element);

                    Util.UpdateList(Database, ListIndex);
                }
            }
            else {
                MessageBox.Show($"Maybe failed to get Id. Any Id cannot be zero.");
            }
        }
    }

    private void TextName_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Name = TextName.Text;

            if (SelectedIndex > Util.NotSelected) {
                ListIndex.Items[SelectedIndex] = $"{Element.Id}: {Element.Name}";
            }
        }
    }

    private void ComboType_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Type = (EquipmentType)ComboType.SelectedIndex;
        }
    }

    private void ComboProfeciency_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Proficiency = (EquipmentProficiency)ComboProfeciency.SelectedIndex;
        }
    }

    private void ComboHandStyle_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.HandStyle = (EquipmentHandStyle)ComboHandStyle.SelectedIndex;
        }
    }

    private void ComboSound_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {

        }
    }

    private void ScrollBaseAttack_Scroll(object sender, ScrollEventArgs e) {
        if (Element is not null) {
            Element.BaseAttackSpeed = ScrollBaseAttack.Value;
            LabelAttackSpeed.Text = $"Weapon Base Attack Speed: {Element.BaseAttackSpeed}%";
        }
    }

    private void ScrollSockets_Scroll(object sender, ScrollEventArgs e) {
        if (Element is not null) {
            Element.MaximumSocket = ScrollSockets.Value;
            LabelSockets.Text = $"Maximum Sockets: {Element.MaximumSocket}";
        }
    }

    private void TextCostumeModel_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.ModelId = Util.GetValue((TextBox)sender);
        }
    }

    private void TextDisassembleId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.DisassembleId = Util.GetValue((TextBox)sender);
        }
    }

    #endregion

    #region Equipment Skill

    private void ButtonRemoveSkill_Click(object sender, EventArgs e) {
        if (Element is not null) {
            var index = ListSkill.SelectedIndex;

            if (index > Util.NotSelected) {
                Element.Skills.RemoveAt(index);
                UpdateSkillList();
            }
        }
    }

    private void ButtonAddSkill_Click(object sender, EventArgs e) {
        if (Element is not null) {
            var id = Util.GetValue(TextSkillId);
            var level = Util.GetValue(TextSkillLevel);
            var unlock = Util.GetValue(TextUnlockAtLevel);

            Element.Skills.Add(new EquipmentSkill() {
                Id = id,
                Level = level,
                UnlockAtLevel = unlock
            });

            Util.FillTextBoxWithZero(GroupNewSkill);

            UpdateSkillList();
        }
    }

    private void ButtonClearSkill_Click(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Skills.Clear();
            UpdateSkillList();
        }
    }

    private void UpdateSkillList() {
        ListSkill.BeginUpdate();
        ListSkill.Items.Clear();

        if (Element is not null) {
            for (var i = 0; i < Element.Skills.Count; ++i) {
                var skill = Element.Skills[i];

                ListSkill.Items.Add($"{{ Id: {skill.Id}, Level: {skill.Level}, Unlock Level: {skill.UnlockAtLevel} }}");
            }
        }

        ListSkill.EndUpdate();
    }

    #endregion

    #region Equipment Attribute


    private void ButtonAttributeAdd_Click(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Attributes.Add(new EquipmentAttribute());

            UpdateIndexLabel();
            UpdateAttributeText();
        }
    }

    private void ButtonAttributeDelete_Click(object sender, EventArgs e) {
        if (Element is not null) {
            var index = ScrollIndex.Value;

            if (index > 0) {
                index--;

                if (Element.Attributes.Count > index) {
                    Element.Attributes.RemoveAt(index);

                    UpdateIndexLabel();
                    UpdateAttributeText();
                }
            }
        }
    }

    private void ScrollIndex_Scroll(object sender, ScrollEventArgs e) {
        if (Element is not null) {
            UpdateIndexLabel();
            UpdateAttributeText();
        }
    }

    private void TextAttributeId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            if (IsValidIndex()) {
                var index = ScrollIndex.Value;

                index--;

                var item = Element.Attributes[index];

                item.AttributeId = Util.GetValue((TextBox)sender);

                Element.Attributes[index] = item;
            }
        }
    }

    private void ScrollChance_Scroll(object sender, ScrollEventArgs e) {
        if (Element is not null) {
            if (IsValidIndex()) {
                var index = ScrollIndex.Value;

                index--;

                var item = Element.Attributes[index];

                item.Chance = ScrollChance.Value;

                Element.Attributes[index] = item;

                LabelChance.Text = $"Chance: {item.Chance}%";
            }
        }
    }

    private bool IsValidIndex() {
        var index = ScrollIndex.Value;

        if (index > 0) {
            index--;

            if (Element!.Attributes.Count > index) {
                return true;
            }
        }

        return false;
    }

    private void UpdateIndexLabel() {
        var count = Element!.Attributes.Count;

        if (count > 0) {
            ScrollIndex.Maximum = count;
            ScrollIndex.Minimum = 1;

            if (ScrollIndex.Value == 0) {
                ScrollIndex.Value = 1;
            }
        }
        else {
            ScrollIndex.Minimum = 0;
            ScrollIndex.Value = 0;
            ScrollIndex.Maximum = 0;
        }

        var index = ScrollIndex.Value;

        LabelIndex.Text = $"Index: {index} / {count}";
    }

    private void UpdateAttributeText() {
        if (Element is not null) {
            var item = new EquipmentAttribute();
            var index = ScrollIndex.Value;

            index--;

            if (index > Util.NotSelected) {
                item = Element.Attributes[index];
            }

            TextAttributeId.Text = item.AttributeId.ToString();
            ScrollChance.Value = item.Chance;
            LabelChance.Text = $"Chance: {item.Chance}%";
        }
    }

    private void TextEquipmentSetId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.EquipmentSetId = Util.GetValue((TextBox)sender);
        }
    }

    private void TextUpgradeId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.UpgradeId = Util.GetValue((TextBox)sender);
        }
    }

    #endregion

}
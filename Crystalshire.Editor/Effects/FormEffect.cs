using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Effects;

using Crystalshire.Editor.Common;

namespace Crystalshire.Editor.Effects;

public partial class FormEffect : Form {
    public IDatabase<Effect> Database { get; }
    public Effect? Element { get; private set; }
    public Configuration Configuration { get; private set; }
    public int SelectedIndex { get; private set; } = Util.NotSelected;

    public FormEffect(Configuration configuration, IDatabase<Effect> database) {
        InitializeComponent();

        SetEnabled(false);

        Util.FillComboBox<EffectType>(ComboType);
        Util.FillComboBox<EffectOverride>(ComboOverride);

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
            TextDescription.Text = Element.Description;

            ComboType.SelectedIndex = (int)Element.EffectType;
            ComboOverride.SelectedIndex = (int)Element.Override;

            CheckRemoveOnDeath.Checked = Element.RemoveOnDeath;
            CheckDispellable.Checked = Element.Dispellable;
            CheckUnlimited.Checked = Element.Unlimited;

            TextIconId.Text = Element.IconId.ToString();
            TextDuration.Text = Element.Duration.ToString();

            TextAttributeId.Text = Element.AttributeId.ToString();
            TextUpgradeId.Text = Element.UpgradeId.ToString();
        }
    }

    private void Clear() {
        TextId.Text = "0";
        TextName.Text = string.Empty;
        TextDescription.Text = string.Empty;

        ComboType.SelectedIndex = 0;
        ComboOverride.SelectedIndex = 0;

        CheckRemoveOnDeath.Checked = false;
        CheckDispellable.Checked = false;
        CheckUnlimited.Checked = false;

        TextIconId.Text = "0";
        TextDuration.Text = "0";

        TextAttributeId.Text = "0";
        TextUpgradeId.Text = "0";
    }

    private void SetEnabled(bool enabled) {
        TabEffect.Enabled = enabled;
    }

    #region Add, Delete, Clear

    private void ButtonAdd_Click(object sender, EventArgs e) {
        for (var i = 1; i <= int.MaxValue; i++) {
            if (!Database.Contains(i)) {

                Database.Add(i, new Effect() { Id = i });

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

    #region Effect Data

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

    private void TextDescription_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Description = TextDescription.Text;
        }
    }

    private void ComboType_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.EffectType = (EffectType)ComboType.SelectedIndex;
        }
    }

    private void CheckRemoveOnDeath_Click(object sender, EventArgs e) {
        if (Element is not null) {
            Element.RemoveOnDeath = CheckRemoveOnDeath.Checked;
        }
    }

    private void CheckDispellable_Click(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Dispellable = CheckDispellable.Checked;
        }
    }

    private void CheckUnlimited_Click(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Unlimited = CheckUnlimited.Checked;
        }
    }

    private void TextIconId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.IconId = Util.GetValue((TextBox)sender);
        }
    }

    private void TextDuration_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Duration = Util.GetValue((TextBox)sender);
        }
    }

    private void ComboOverride_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Override = (EffectOverride)ComboOverride.SelectedIndex;
        }
    }

    #endregion

    #region Effect Attribute

    private void TextAttributeId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.AttributeId = Util.GetValue((TextBox)sender);
        }
    }

    private void TextUpgradeId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.UpgradeId = Util.GetValue((TextBox)sender);
        }
    }

    #endregion

}
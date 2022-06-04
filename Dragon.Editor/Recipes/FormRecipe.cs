using Dragon.Core.Content;
using Dragon.Core.Model.Crafts;
using Dragon.Core.Model.Recipes;

using Dragon.Editor.Common;

namespace Dragon.Editor.Recipes;

public partial class FormRecipe : Form {
    public IDatabase<Recipe> Database { get; }
    public Recipe? Element { get; private set; }
    public Configuration Configuration { get; private set; }
    public int SelectedIndex { get; private set; } = Util.NotSelected;

    public FormRecipe(Configuration configuration, IDatabase<Recipe> database) {
        InitializeComponent();

        SetEnabled(false);

        Util.FillComboBox<CraftType>(ComboCategory);

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
            ComboCategory.SelectedIndex = (int)Element.CraftType;

            TextLevel.Text = Element.Level.ToString();
            TextExperience.Text = Element.Experience.ToString();

            UpdateIndexLabel();
            UpdateRequiredText();

            TextRewardId.Text = Element.Reward.Id.ToString();
            TextRewardValue.Text = Element.Reward.Value.ToString();
            TextRewardLevel.Text = Element.Reward.Level.ToString();
            TextRewardBound.Text = Element.Reward.Bound ? "1" : "0";
        }
    }

    private void Clear() {
        TextId.Text = "0";
        TextName.Text = string.Empty;
        TextDescription.Text = string.Empty;
        ComboCategory.SelectedIndex = 0;

        TextLevel.Text = "0";
        TextExperience.Text = "0";

        Util.FillTextBoxWithZero(GroupRequired);
        Util.FillTextBoxWithZero(GroupReward);

        ScrollIndex.Minimum = 0;
        ScrollIndex.Value = 0;
        ScrollIndex.Maximum = 0;

        LabelIndex.Text = $"Index: 0 / 0";
    }

    private void SetEnabled(bool enabled) {
        TabRecipe.Enabled = enabled;
    }

    #region Add, Delete, Clear

    private void ButtonAdd_Click(object sender, EventArgs e) {
        for (var i = 1; i <= int.MaxValue; i++) {
            if (!Database.Contains(i)) {

                Database.Add(i, new Recipe() { Id = i });

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

    #region Recipe Data

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

    private void TextLevel_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Level = Util.GetValue((TextBox)sender);
        }
    }

    private void ComboCategory_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.CraftType = (CraftType)ComboCategory.SelectedIndex;
        }
    }

    private void TextExperience_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Experience = Util.GetValue((TextBox)sender);
        }
    }


    #endregion

    #region Recipe Reward

    private void TextRewardId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var reward = Element.Reward;

            reward.Id = Util.GetValue((TextBox)sender);

            Element.Reward = reward;
        }
    }

    private void TextRewardValue_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var reward = Element.Reward;

            reward.Value = Util.GetValue((TextBox)sender);

            Element.Reward = reward;
        }
    }

    private void TextRewardLevel_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var reward = Element.Reward;

            reward.Level = Util.GetValue((TextBox)sender);

            Element.Reward = reward;
        }
    }

    private void TextRewardBound_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            var reward = Element.Reward;

            reward.Bound = Util.GetValue((TextBox)sender) > 0;

            Element.Reward = reward;
        }
    }

    #endregion

    #region Recipe Required

    private void ButtonRequiredAdd_Click(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Required.Add(new RecipeItem());

            UpdateIndexLabel();
            UpdateRequiredText();
        }
    }

    private void ButtonRequiredDelete_Click(object sender, EventArgs e) {
        if (Element is not null) {
            var index = ScrollIndex.Value;

            if (index > 0) {
                index--;

                if (Element.Required.Count > index) {
                    Element.Required.RemoveAt(index);

                    UpdateIndexLabel();
                    UpdateRequiredText();
                }
            }
        }
    }

    private void ScrollIndex_Scroll(object sender, ScrollEventArgs e) {
        if (Element is not null) {
            UpdateIndexLabel();
            UpdateRequiredText();
        }
    }

    private void TextRequiredId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            if (IsValidIndex()) {
                var index = ScrollIndex.Value;

                index--;

                var item = Element.Required[index];

                item.Id = Util.GetValue((TextBox)sender);

                Element.Required[index] = item;
            }
        }
    }

    private void TextRequiredValue_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            if (IsValidIndex()) {
                var index = ScrollIndex.Value;

                index--;

                var item = Element.Required[index];

                item.Value = Util.GetValue((TextBox)sender);

                Element.Required[index] = item;
            }
        }
    }

    private void TextRequiredLevel_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            if (IsValidIndex()) {
                var index = ScrollIndex.Value;

                index--;

                var item = Element.Required[index];

                item.Level = Util.GetValue((TextBox)sender);

                Element.Required[index] = item;
            }
        }
    }

    private bool IsValidIndex() {
        var index = ScrollIndex.Value;

        if (index > 0) {
            index--;

            if (Element!.Required.Count > index) {
                return true;
            }
        }

        return false;
    }

    private void UpdateIndexLabel() {
        var count = Element!.Required.Count;

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

    private void UpdateRequiredText() {
        if (Element is not null) {
            var item = new RecipeItem();
            var index = ScrollIndex.Value;

            index--;

            if (index > Util.NotSelected) {
                item = Element.Required[index];
            }

            TextRequiredId.Text = item.Id.ToString();
            TextRequiredValue.Text = item.Value.ToString();
            TextRequiredLevel.Text = item.Level.ToString();
        }
    }

    #endregion
}
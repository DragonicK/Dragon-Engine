using Crystalshire.Core.Content;
using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Items;

using Crystalshire.Editor.Common;

namespace Crystalshire.Editor.Items;

public partial class FormItem : Form {
    public IDatabase<Item> Database { get; }
    public Item? Element { get; private set; }
    public Configuration Configuration { get; private set; }
    public int SelectedIndex { get; private set; } = Util.NotSelected;

    public FormItem(Configuration configuration, IDatabase<Item> database) {
        InitializeComponent();

        SetEnabled(false);

        Util.FillComboBox<ItemType>(ComboType);
        Util.FillComboBox<Rarity>(ComboRarity);
        Util.FillComboBox<BindType>(ComboBind);

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
            TextRequiredLevel.Text = Element.RequiredLevel.ToString();
            TextIconId.Text = Element.IconId.ToString();
            TextSound.Text = Element.Sound;
            TextClassId.Text = Element.ClassCode.ToString();

            ComboType.SelectedIndex = (int)Element.Type;
            ComboRarity.SelectedIndex = (int)Element.Rarity;
            ComboBind.SelectedIndex = (int)Element.Bind;
            TextPrice.Text = Element.Price.ToString();
            TextGashaId.Text = Element.GashaBoxId.ToString();
            TextMaximumStack.Text = Element.MaximumStack.ToString();
            TextRecipeId.Text = Element.RecipeId.ToString();
            TextSkillId.Text = Element.SkillId.ToString();
            TextEquipmentId.Text = Element.EquipmentId.ToString();
            TextMaximumLevel.Text = Element.MaximumLevel.ToString();
            TextUpgradeId.Text = Element.UpgradeId.ToString();

            TextHp.Text = Element.Vital[(int)Vital.HP].ToString();
            TextMp.Text = Element.Vital[(int)Vital.MP].ToString();
            TextCooldown.Text = Element.Cooldown.ToString();
            TextDuration.Text = Element.Duration.ToString();
            TextInterval.Text = Element.Interval.ToString();
            TextEffectId.Text = Element.EffectId.ToString();
            TextEffectLevel.Text = Element.EffectLevel.ToString();
            TextEffectDuration.Text = Element.EffectDuration.ToString();
        }
    }

    private void Clear() {
        TextId.Text = "0";
        TextName.Text = string.Empty;
        TextDescription.Text = string.Empty;
        TextRequiredLevel.Text = "0";
        TextIconId.Text = "0";
        TextSound.Text = "None.";
        TextClassId.Text = "0";

        ComboType.SelectedIndex = 0;
        ComboRarity.SelectedIndex = 0;
        ComboBind.SelectedIndex = 0;

        Util.FillTextBoxWithZero(GroupGeneral);
        Util.FillTextBoxWithZero(GroupConsume);
    }

    private void SetEnabled(bool enabled) {
        TabAchievement.Enabled = enabled;
    }

    #region Add, Delete, Clear

    private void ButtonAdd_Click(object sender, EventArgs e) {
        for (var i = 1; i <= int.MaxValue; i++) {
            if (!Database.Contains(i)) {

                Database.Add(i, new Item() { Id = i });

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

    #region Item Data

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
            Element.Description = ((TextBox)sender).Text;
        }
    }

    private void TextIconId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.IconId = Util.GetValue((TextBox)sender);
        }
    }

    private void TextRequiredLevel_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.RequiredLevel = Util.GetValue((TextBox)sender);
        }
    }

    private void TextSound_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Sound = ((TextBox)sender).Text;
        }
    }

    private void TextClassId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.ClassCode = Util.GetValue((TextBox)sender);
        }
    }

    #endregion

    #region Item Data

    private void ComboType_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Type = (ItemType)ComboType.SelectedIndex;
        }

        SetEnabled((ItemType)ComboType.SelectedIndex);
    }

    private void ComboRarity_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Rarity = (Rarity)ComboRarity.SelectedIndex;
        }
    }

    private void ComboBind_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Bind = (BindType)ComboBind.SelectedIndex;
        }
    }

    private void TextPrice_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Price = Util.GetValue((TextBox)sender);
        }
    }

    private void TextGashaId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.GashaBoxId = Util.GetValue((TextBox)sender);
        }
    }

    private void TextMaximumStack_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.MaximumStack = Util.GetValue((TextBox)sender);
        }
    }

    private void TextRecipeId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.RecipeId = Util.GetValue((TextBox)sender);
        }
    }

    private void TextSkillId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.SkillId = Util.GetValue((TextBox)sender);
        }
    }

    private void TextEquipmentId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.EquipmentId = Util.GetValue((TextBox)sender);
        }
    }

    private void TextMaximumLevel_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.MaximumLevel = Util.GetValue((TextBox)sender);
        }
    }

    private void TextUpgradeId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.UpgradeId = Util.GetValue((TextBox)sender);
        }
    }

    #endregion

    #region Item Consume

    private void TextHp_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Vital[(int)Vital.HP] = Util.GetValue((TextBox)sender);
        }
    }

    private void TextMp_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Vital[(int)Vital.MP] = Util.GetValue((TextBox)sender);
        }
    }

    private void TextCooldown_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Cooldown = Util.GetValue((TextBox)sender);
        }
    }

    private void TextDuration_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Duration = Util.GetValue((TextBox)sender);
        }
    }

    private void TextInterval_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Interval = Util.GetValue((TextBox)sender);
        }
    }

    private void TextEffectId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.EffectId = Util.GetValue((TextBox)sender);
        }
    }

    private void TextEffectLevel_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.EffectLevel = Util.GetValue((TextBox)sender);
        }
    }

    private void TextEffectDuration_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.EffectDuration = Util.GetValue((TextBox)sender);
        }
    }

    #endregion


    private void SetEnabled(ItemType type) {
        switch (type) {
            case ItemType.None:
                GroupConsume.Enabled = false;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = false;
                TextEffectLevel.Enabled = false;
                TextEffectDuration.Enabled = false;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = true;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = false;
                TextMaximumLevel.Enabled = false;
                TextUpgradeId.Enabled = false;
                break;

            case ItemType.Equipment:
                GroupConsume.Enabled = true;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = false;
                TextEffectLevel.Enabled = false;
                TextEffectDuration.Enabled = false;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = false;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = true;
                TextMaximumLevel.Enabled = true;
                TextUpgradeId.Enabled = true;
                break;
            case ItemType.Key:
                GroupConsume.Enabled = false;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = true;
                TextEffectLevel.Enabled = false;
                TextEffectDuration.Enabled = false;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = false;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = false;
                TextMaximumLevel.Enabled = false;
                TextUpgradeId.Enabled = false;

                break;
            case ItemType.Skill:
                GroupConsume.Enabled = true;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = false;
                TextEffectLevel.Enabled = false;
                TextEffectDuration.Enabled = false;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = false;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = true;
                TextEquipmentId.Enabled = false;
                TextMaximumLevel.Enabled = false;
                TextUpgradeId.Enabled = false;

                break;
            case ItemType.Food:
                GroupConsume.Enabled = true;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = true;
                TextEffectLevel.Enabled = true;
                TextEffectDuration.Enabled = true;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = false;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = false;
                TextMaximumLevel.Enabled = false;
                TextUpgradeId.Enabled = false;

                break;
            case ItemType.Potion:
                GroupConsume.Enabled = true;
                TextHp.Enabled = true;
                TextMp.Enabled = true;
                TextCooldown.Enabled = true;
                TextDuration.Enabled = true;
                TextInterval.Enabled = true;
                TextEffectId.Enabled = false;
                TextEffectLevel.Enabled = false;
                TextEffectDuration.Enabled = false;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = false;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = false;
                TextMaximumLevel.Enabled = false;
                TextUpgradeId.Enabled = false;

                break;
            case ItemType.Upgrade:
                GroupConsume.Enabled = false;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = false;
                TextEffectLevel.Enabled = false;
                TextEffectDuration.Enabled = false;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = true;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = false;
                TextMaximumLevel.Enabled = false;
                TextUpgradeId.Enabled = false;
                break;

            case ItemType.Supplement:
                GroupConsume.Enabled = false;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = false;
                TextEffectLevel.Enabled = false;
                TextEffectDuration.Enabled = false;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = true;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = false;
                TextMaximumLevel.Enabled = false;
                TextUpgradeId.Enabled = false;
                break;

            case ItemType.Recipe:
                GroupConsume.Enabled = false;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = false;
                TextEffectLevel.Enabled = false;
                TextEffectDuration.Enabled = false;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = true;
                TextRecipeId.Enabled = true;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = false;
                TextMaximumLevel.Enabled = false;
                TextUpgradeId.Enabled = false;
                break;

            case ItemType.GashaBox:
                GroupConsume.Enabled = false;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = false;
                TextEffectLevel.Enabled = false;
                TextEffectDuration.Enabled = false;

                TextGashaId.Enabled = true;
                TextMaximumStack.Enabled = true;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = false;
                TextMaximumLevel.Enabled = false;
                TextUpgradeId.Enabled = false;
                break;

            case ItemType.Quest:
                GroupConsume.Enabled = false;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = false;
                TextEffectLevel.Enabled = false;
                TextEffectDuration.Enabled = false;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = true;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = false;
                TextMaximumLevel.Enabled = false;
                TextUpgradeId.Enabled = false;
                break;

            case ItemType.Heraldry:
                GroupConsume.Enabled = false;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = false;
                TextEffectLevel.Enabled = false;
                TextEffectDuration.Enabled = false;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = false;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = true;
                TextMaximumLevel.Enabled = true;
                TextUpgradeId.Enabled = true;
                break;

            case ItemType.Talisman:
                GroupConsume.Enabled = false;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = false;
                TextEffectLevel.Enabled = false;
                TextEffectDuration.Enabled = false;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = false;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = true;
                TextMaximumLevel.Enabled = true;
                TextUpgradeId.Enabled = true;
                break;

            case ItemType.Material:
                GroupConsume.Enabled = false;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = false;
                TextEffectLevel.Enabled = false;
                TextEffectDuration.Enabled = false;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = true;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = false;
                TextMaximumLevel.Enabled = false;
                TextUpgradeId.Enabled = false;
                break;

            case ItemType.Scroll:
                GroupConsume.Enabled = true;
                TextHp.Enabled = false;
                TextMp.Enabled = false;
                TextCooldown.Enabled = false;
                TextDuration.Enabled = false;
                TextInterval.Enabled = false;
                TextEffectId.Enabled = true;
                TextEffectLevel.Enabled = true;
                TextEffectDuration.Enabled = true;

                TextGashaId.Enabled = false;
                TextMaximumStack.Enabled = false;
                TextRecipeId.Enabled = false;
                TextSkillId.Enabled = false;
                TextEquipmentId.Enabled = false;
                TextMaximumLevel.Enabled = false;
                TextUpgradeId.Enabled = false;

                break;
        }
    }
}
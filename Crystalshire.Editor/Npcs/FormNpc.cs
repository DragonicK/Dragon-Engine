using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Npcs;

using Crystalshire.Editor.Common;

namespace Crystalshire.Editor.Npcs;

public partial class FormNpc : Form {
    public IDatabase<Npc> Database { get; }
    public Npc? Element { get; private set; }
    public Configuration Configuration { get; private set; }
    public int SelectedIndex { get; private set; } = Util.NotSelected;

    public FormNpc(Configuration configuration, IDatabase<Npc> database) {
        InitializeComponent();

        SetEnabled(false);

        Util.FillComboBox<NpcBehaviour>(ComboBehaviour);

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
            TextTitle.Text = Element.Title;

            ComboBehaviour.SelectedIndex = (int)Element.Behaviour;

            TextModelId.Text = Element.ModelId.ToString();
            TextLevel.Text = Element.Level.ToString();
            TextAttributeId.Text = Element.AttributeId.ToString();
            TextExperience.Text = Element.Experience.ToString();
            TextSound.Text = Element.Sound.ToString();

            UpdateConversationList();

            TextGreetings.Text = Element.Greetings;
            TextConversationId.Text = "0";
        }
    }

    private void Clear() {
        TextId.Text = "0";
        TextName.Text = string.Empty;
        TextTitle.Text = string.Empty;

        ComboBehaviour.SelectedIndex = 0;

        TextModelId.Text = "0";
        TextLevel.Text = "0";
        TextAttributeId.Text = "0";
        TextExperience.Text = "0";
        TextSound.Text = "None.";

        TextGreetings.Text = string.Empty;
        TextConversationId.Text = "0";
        ListConversation.Items.Clear();
    }

    private void SetEnabled(bool enabled) {
        TabAchievement.Enabled = enabled;
    }

    #region Add, Delete, Clear

    private void ButtonAdd_Click(object sender, EventArgs e) {
        for (var i = 1; i <= int.MaxValue; i++) {
            if (!Database.Contains(i)) {

                Database.Add(i, new Npc() { Id = i });

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

    #region Npc Data

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


    private void TextTitle_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Title = TextTitle.Text;
        }
    }

    private void ComboBehaviour_SelectedIndexChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Behaviour = (NpcBehaviour)ComboBehaviour.SelectedIndex;
        }
    }

    private void TextModelId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.ModelId = Util.GetValue((TextBox)sender);
        }
    }

    private void TextLevel_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Level = Util.GetValue((TextBox)sender);
        }
    }

    private void TextAttributeId_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.AttributeId = Util.GetValue((TextBox)sender);
        }
    }

    private void TextExperience_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Experience = Util.GetValue((TextBox)sender);
        }
    }

    private void TextSound_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Sound = TextSound.Text;
        }
    }

    #endregion

    #region Npc Conversation

    private void ButtonClearConversation_Click(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Conversations.Clear();

            UpdateConversationList();
        }
    }

    private void ButtonRemoveConversation_Click(object sender, EventArgs e) {
        if (Element is not null) {

            var index = ListConversation.SelectedIndex;

            if (index > Util.NotSelected) {
                Element.Conversations.RemoveAt(index);

                UpdateConversationList();
            }
        }
    }

    private void ButtonAddConversation_Click(object sender, EventArgs e) {
        if (Element is not null) {
            var value = Util.GetValue(TextConversationId);

            if (value > 0) {
                Element.Conversations.Add(value);

                UpdateConversationList();
            }

            TextConversationId.Text = "0";
        }
    }

    private void TextGreetings_TextChanged(object sender, EventArgs e) {
        if (Element is not null) {
            Element.Greetings = TextGreetings.Text;
        }
    }

    private void UpdateConversationList() {
        ListConversation.BeginUpdate();

        ListConversation.Items.Clear();

        if (Element is not null) {
            var array = Element.Conversations.ToArray();

            for (var i = 0; i < array.Length; ++i) {
                ListConversation.Items.Add(array[i]);
            }
        }

        ListConversation.EndUpdate();
    }

    #endregion

}
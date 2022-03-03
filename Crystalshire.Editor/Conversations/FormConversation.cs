using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Conversations;

using Crystalshire.Editor.Common;

namespace Crystalshire.Editor.Conversations {
    public partial class FormConversation : Form {
        public IDatabase<Conversation> Database { get; }
        public Conversation? Element { get; private set; }
        public Configuration Configuration { get; private set; }
        public int SelectedIndex { get; private set; } = Util.NotSelected;

        private const int MaximumConversationOptions = 4;

        private int chatIndex = Util.NotSelected;

        private readonly TextBox[] TextReply = new TextBox[MaximumConversationOptions];
        private readonly ComboBox[] ComboReply = new ComboBox[MaximumConversationOptions];

        public FormConversation(Configuration configuration, IDatabase<Conversation> database) {
            InitializeComponent();

            SetEnabled(false);

            Util.FillComboBox<ConversationType>(ComboType);
            Util.FillComboBox<ConversationEvent>(ComboEvent);

            Database = database;
            Configuration = configuration;
            Element = null;

            Util.UpdateList(Database, ListIndex);

            TextReply[0] = TextReply_0;
            TextReply[1] = TextReply_1;
            TextReply[2] = TextReply_2;
            TextReply[3] = TextReply_3;

            ComboReply[0] = ComboReply_0;
            ComboReply[1] = ComboReply_1;
            ComboReply[2] = ComboReply_2;
            ComboReply[3] = ComboReply_3;

            for (var i = 0; i < MaximumConversationOptions; ++i) {
                TextReply[i].TextChanged += TextReply_TextChanged;
                ComboReply[i].SelectedIndexChanged += ComboReplay_SelectedIndexChanged;
            }
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
                TextQuest.Text = Element.QuestId.ToString();

                chatIndex = Util.NotSelected;

                var isEnabled = Element.Type == ConversationType.Quest;

                GroupMeanwhile.Enabled = isEnabled;
                GroupDone.Enabled = isEnabled;
                TextQuest.Enabled = isEnabled;

                if (Element.Chats.Count > 0) {
                    chatIndex = 0;

                    TextChat.Text = Element.Chats[chatIndex].Text;

                    ComboEvent.SelectedIndex = (int)Element.Chats[chatIndex].Event;
                    TextData1.Text = Element.Chats[chatIndex].Data1.ToString();
                    TextData2.Text = Element.Chats[chatIndex].Data2.ToString();
                    TextData3.Text = Element.Chats[chatIndex].Data3.ToString();
                }
                else {
                    TextChat.Text = string.Empty;

                    ComboEvent.SelectedIndex = 0;
                    TextData1.Text = "0";
                    TextData2.Text = "0";
                    TextData3.Text = "0";

                    for (var i = 0; i < MaximumConversationOptions; ++i) {
                        TextReply[i].Text = string.Empty;
                    }
                }

                if (chatIndex > Util.NotSelected) {
                    for (var i = 0; i < MaximumConversationOptions; ++i) {
                        TextReply[i].Text = Element.Chats[chatIndex].Reply[i].Text;
                    }
                }

                UpdateControls();
                UpdateIndexLabel();

                PopulateReplyComboBox();
                UpdateReplyComboBox();
                UpdateReplyTextBox();

                TextMeanwhile.Text = Element.MeanwhileText;
                TextDone.Text = Element.DoneText;
            }
        }

        private void Clear() {
            TextId.Text = "0";
            TextName.Text = string.Empty;
            ComboType.SelectedIndex = 0;
            TextQuest.Text = "0";

            LabelIndex.Text = "Conversation Index: 0/0";
            TextChat.Text = string.Empty;
            ScrollIndex.Minimum = 0;
            ScrollIndex.Value = 0;
            ScrollIndex.Maximum = 0;

            for (var i = 0; i < MaximumConversationOptions; ++i) {
                TextReply[i].Text = string.Empty;
                ComboReply[i].Items.Clear();
            }

            ComboEvent.SelectedIndex = 0;
            TextData1.Text = "0";
            TextData2.Text = "0";
            TextData3.Text = "0";

            TextMeanwhile.Text = string.Empty;
            TextDone.Text = string.Empty;
        }

        private void SetEnabled(bool enabled) {
            TabConversation.Enabled = enabled;
        }

        #region Add, Delete, Clear

        private void ButtonAdd_Click(object sender, EventArgs e) {
            for (var i = 1; i <= int.MaxValue; i++) {
                if (!Database.Contains(i)) {

                    Database.Add(i, new Conversation() { Id = i });

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

        #region Conversation Data

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
                Element.Type = (ConversationType)ComboType.SelectedIndex;

                var isEnabled = Element.Type == ConversationType.Quest;

                GroupMeanwhile.Enabled = isEnabled;
                GroupDone.Enabled = isEnabled;
                TextQuest.Enabled = isEnabled;
            }
        }

        private void TextQuest_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.QuestId = Util.GetValue((TextBox)sender);
            }
        }

        #endregion

        #region Conversation Chat

        private void ScrollIndex_Scroll(object sender, ScrollEventArgs e) {
            if (Element is not null) {
                if (Element.ChatCount > 0) {
                    chatIndex = ScrollIndex.Value;
                }

                UpdateIndexLabel();
                UpdateReplyComboBox();
                UpdateReplyTextBox();
                UpdateEvent();
            }
        }

        private void ButtonAddChat_Click(object sender, EventArgs e) {
            if (Element is not null) {
                Element.Chats.Add(new Chat(MaximumConversationOptions));

                PopulateReplyComboBox();

                if (Element.ChatCount == 1) {
                    if (chatIndex <= Util.NotSelected) {
                        chatIndex = 0;

                        UpdateReplyTextBox();
                        UpdateReplyComboBox();
                    }
                }

                UpdateControls();
                UpdateIndexLabel();
            }
        }

        private void ButtonRemoveChat_Click(object sender, EventArgs e) {
            if (Element is not null) {
                if (chatIndex > Util.NotSelected) {
                    Element.Chats.RemoveAt(chatIndex);

                    if (chatIndex >= Element.ChatCount) {
                        chatIndex = Element.ChatCount - 1;
                    }
                }

                PopulateReplyComboBox();

                UpdateReplyTextBox();
                UpdateReplyComboBox();

                UpdateControls();
                UpdateIndexLabel();
            }
        }

        private void UpdateControls() {
            if (Element is not null) {
                var isEnabled = Element.ChatCount > 0;

                ScrollIndex.Enabled = isEnabled;
                TextChat.Enabled = isEnabled;
                GroupReply.Enabled = isEnabled;
                GroupEvent.Enabled = isEnabled;
            }
        }

        private void UpdateIndexLabel() {
            if (Element is not null) {
                LabelIndex.Text = $"Conversation Index: {chatIndex + 1}/ {Element.ChatCount}";
               
                if (chatIndex > Util.NotSelected) {
                    ScrollIndex.Maximum = Element.ChatCount - 1;

                    if (chatIndex == 0) {
                        ScrollIndex.Value = chatIndex;  
                    }               
                }
                else {
                    ScrollIndex.Value = 0;
                    ScrollIndex.Maximum = 0;
                }
            }
        }

        private void PopulateReplyComboBox() {
            for (var i = 0; i < MaximumConversationOptions; ++i) {
                var combo = ComboReply[i];

                combo.BeginUpdate();

                combo.Items.Clear();

                combo.Items.Add("None");

                if (Element is not null) {
                    for (var x = 0; x < Element.ChatCount; x++) {
                        combo.Items.Add(x + 1);
                    }
                }

                combo.EndUpdate();
            }
        }

        private void UpdateReplyComboBox() {
            if (Element is not null) {
                if (chatIndex > Util.NotSelected) {
                    for (var i = 0; i < MaximumConversationOptions; ++i) {
                        var count = Element.ChatCount;
                        var chat = Element.Chats[chatIndex];

                        if (chat.Reply[i].Target > count) {
                            chat.Reply[i].Target = 0;
                        }

                        ComboReply[i].SelectedIndex = chat.Reply[i].Target;
                    }                    
                }
            }
        }

        private void UpdateReplyTextBox() {
            if (Element is not null) {
                if (chatIndex > Util.NotSelected) {
                    for (var i = 0; i < MaximumConversationOptions; ++i) {
                        TextReply[i].Text = Element.Chats[chatIndex].Reply[i].Text;
                    }
                }
            }
        }

        private void UpdateEvent() {
            if (Element is not null) {
                if (chatIndex > Util.NotSelected) {
                    TextChat.Text = Element.Chats[chatIndex].Text;

                    ComboEvent.SelectedIndex = (int)Element.Chats[chatIndex].Event;
                    TextData1.Text = Element.Chats[chatIndex].Data1.ToString();
                    TextData2.Text = Element.Chats[chatIndex].Data2.ToString();
                    TextData3.Text = Element.Chats[chatIndex].Data3.ToString();
                }
            }
        }

        private void TextChat_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (chatIndex > Util.NotSelected) {
                    Element.Chats[chatIndex].Text = TextChat.Text;
                }
            }
        }

        private void ComboEvent_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (chatIndex > Util.NotSelected) {
                    Element.Chats[chatIndex].Event = (ConversationEvent)ComboEvent.SelectedIndex;
                }
            }
        }

        private void TextData1_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (chatIndex > Util.NotSelected) {
                    Element.Chats[chatIndex].Data1 = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextData2_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (chatIndex > Util.NotSelected) {
                    Element.Chats[chatIndex].Data2 = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextData3_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (chatIndex > Util.NotSelected) {
                    Element.Chats[chatIndex].Data3 = Util.GetValue((TextBox)sender);
                }
            }
        }

        private void TextReply_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (chatIndex > Util.NotSelected) {
                    var index = int.Parse(((TextBox)sender).Name.Replace("TextReply_", string.Empty));

                    Element.Chats[chatIndex].Reply[index].Text = ((TextBox)sender).Text;
                }
            }           
        }

        private void ComboReplay_SelectedIndexChanged(object sender, EventArgs e) {
            if (Element is not null) {
                if (chatIndex > Util.NotSelected) {
                    var index = int.Parse(((ComboBox)sender).Name.Replace("ComboReply_", string.Empty));

                    Element.Chats[chatIndex].Reply[index].Target = ((ComboBox)sender).SelectedIndex;
                }
            }
        }

        #endregion

        #region Conversation Quest

        private void TextMeanwhile_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.MeanwhileText = TextMeanwhile.Text;
            }
        }

        private void TextDone_TextChanged(object sender, EventArgs e) {
            if (Element is not null) {
                Element.DoneText = TextDone.Text;
            }
        }

        #endregion

    }
}
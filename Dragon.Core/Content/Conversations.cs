using Dragon.Core.Model.Conversations;

namespace Dragon.Core.Content;

public class Conversations : Database<Conversation> {

    public override void Load() {
        var path = $"{Folder}/{FileName}";

        if (File.Exists(path)) {
            using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(file);

            var count = reader.ReadInt32();

            for (var i = 0; i < count; i++) {
                var conversation = new Conversation() {
                    Id = reader.ReadInt32(),
                    Name = reader.ReadString(),
                    Type = (ConversationType)reader.ReadInt32(),
                    QuestId = reader.ReadInt32()
                };

                var chatCount = reader.ReadInt32();

                for (var x = 0; x < chatCount; ++x) {
                    var optionCount = reader.ReadInt32();

                    var chat = new Chat(optionCount) {
                        Text = reader.ReadString(),
                        Event = (ConversationEvent)reader.ReadInt32(),
                        Data1 = reader.ReadInt32(),
                        Data2 = reader.ReadInt32(),
                        Data3 = reader.ReadInt32()
                    };

                    for (var y = 0; y < optionCount; ++y) {
                        chat.Reply[y].Target = reader.ReadInt32();
                        chat.Reply[y].Text = reader.ReadString();
                    }

                    conversation.Chats.Add(chat);
                }

                conversation.MeanwhileText = reader.ReadString();
                conversation.DoneText = reader.ReadString();

                values.Add(conversation.Id, conversation);
            }
        }
    }


    public override void Save() {
        var path = $"{Folder}/{FileName}";

        using var file = new FileStream(path, FileMode.Create, FileAccess.Write);
        using var writer = new BinaryWriter(file);

        var count = values.Count;

        writer.Write(count);

        var ordered = values.Select(p => p.Value).OrderBy(p => p.Id).ToList();

        for (var i = 0; i < ordered.Count; ++i) {
            var conversation = ordered[i];

            writer.Write(conversation.Id);
            writer.Write(conversation.Name);
            writer.Write((int)conversation.Type);
            writer.Write(conversation.QuestId);
            writer.Write(conversation.ChatCount);

            for (var x = 0; x < conversation.ChatCount; ++x) {
                var chat = conversation.Chats[x];

                writer.Write(chat.Reply.Count);

                writer.Write(chat.Text);
                writer.Write((int)chat.Event);
                writer.Write(chat.Data1);
                writer.Write(chat.Data2);
                writer.Write(chat.Data3);

                for (var y = 0; y < chat.Reply.Count; y++) {
                    writer.Write(chat.Reply[y].Target);
                    writer.Write(chat.Reply[y].Text);
                }
            }

            writer.Write(conversation.MeanwhileText);
            writer.Write(conversation.DoneText);
        }
    }
}
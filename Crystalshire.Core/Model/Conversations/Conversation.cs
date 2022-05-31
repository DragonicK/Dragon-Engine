namespace Crystalshire.Core.Model.Conversations;

public class Conversation {
    public int Id { get; set; }
    public string Name { get; set; }
    public int QuestId { get; set; }
    public ConversationType Type { get; set; }
    public IList<Chat> Chats { get; set; }
    public string MeanwhileText { get; set; } = string.Empty;
    public string DoneText { get; set; } = string.Empty;
    public int ChatCount => Chats.Count;


    public Conversation() {
        Name = string.Empty;
        Chats = new List<Chat>();
    }

    public override string ToString() {
        return Name;
    }
}
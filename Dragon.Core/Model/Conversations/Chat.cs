namespace Dragon.Core.Model.Conversations;

public class Chat {
    public IList<ChatReply> Reply { get; set; }
    public ConversationEvent Event { get; set; }
    public int Data1 { get; set; }
    public int Data2 { get; set; }
    public int Data3 { get; set; }
    public string Text { get; set; }

    public Chat(int maximumReplies) {
        Text = string.Empty;

        Reply = new List<ChatReply>(maximumReplies);

        for (var i = 0; i < maximumReplies; ++i) {
            Reply.Add(new ChatReply());
        }
    }
}
namespace Dragon.Chat.Configurations.Data;

public sealed class Allocation {
    public int TargetsAllocatedSize { get; set; }
    public int TargetListAllocatedSize { get; set; }
    public int BubblesAllocatedSize { get; set; }
    public int BubbleTextAllocatedSize { get; set; }
    public int OutgoingMessageAllocatedSize { get; set; }
    public int IncomingMessageAllocatedSize { get; set; }
}
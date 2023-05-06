namespace Dragon.Chat.Pool;

public sealed class TargetPool {
    private int index;

    private readonly IList<Target> messages;

    public TargetPool(int capacity, int connectionCapacity) {
        messages = new List<Target>(capacity);

        for (var i = 0; i < capacity; ++i) {
            messages.Add(new Target(connectionCapacity));
        }
    }

    public Target GetNextTarget() {
        index = index >= messages.Count ? 0 : index++;

        return messages[index];
    }
}
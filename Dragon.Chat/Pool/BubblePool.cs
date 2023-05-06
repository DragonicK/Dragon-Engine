using Dragon.Network.Messaging.SharedPackets;

namespace Dragon.Chat.Pool;

public sealed class BubblePool {
    private int index;

    private readonly IList<SpMessageBubble> bubbles;

    public BubblePool(int capacity) {
        bubbles = new List<SpMessageBubble>(capacity);

        for (var i = 0; i < capacity; ++i) {
            bubbles.Add(new SpMessageBubble());
        }
    }

    public SpMessageBubble GetNextBubble() {
        index = index >= bubbles.Count ? 0 : ++index;

        return bubbles[index];
    }
}
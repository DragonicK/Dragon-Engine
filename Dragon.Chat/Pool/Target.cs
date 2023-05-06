using Dragon.Chat.Players;

namespace Dragon.Chat.Pool;

public sealed class Target {
    public IList<IPlayer> Players { get; private set; }

    public Target(int capacity) {
        Players = new List<IPlayer>(capacity);
    }

    public void Reset() {
        Players.Clear();
    }
}
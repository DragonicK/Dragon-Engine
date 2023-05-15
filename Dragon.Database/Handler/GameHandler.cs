using Dragon.Database.Context;

namespace Dragon.Database.Handler;

public sealed class GameHandler : IDisposable {
    private readonly GameContext Context;
    private bool disposed = false;

    public GameHandler(GameContext context) {
        Context = context;
    }

    public bool CanConnect() {
        return Context.Database.CanConnect();
    }

    public void Dispose() {
        Dispose(true);
    }

    private void Dispose(bool disposing) {
        if (!disposed) {
            if (disposing) {
                Context.Dispose();
            }

            disposed = true;
        }
    }
}
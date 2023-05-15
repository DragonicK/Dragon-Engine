using Dragon.Database.Context;

namespace Dragon.Database.Handler;

public sealed class ServerHandler : IDisposable {
    private readonly ServerContext Context;
    private bool disposed = false;

    public ServerHandler(ServerContext context) {
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
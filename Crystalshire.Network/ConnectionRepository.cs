using System.Collections.Concurrent;

namespace Crystalshire.Network;

public class ConnectionRepository : IConnectionRepository {
    public ConcurrentDictionary<int, IConnection> Connections { get; private set; }

    public ConnectionRepository() {
        Connections = new ConcurrentDictionary<int, IConnection>();
    }

    public IConnection AddClientFromId(int connectionId) {
        if (!Connections.ContainsKey(connectionId)) {
            Connections.TryAdd(connectionId, new Connection { Id = connectionId });
        }

        return Connections.GetOrAdd(connectionId, default(IConnection)!);

        //return Connections[connectionId];
    }

    public IConnection GetFromId(int connectionId) {
        return Connections.GetOrAdd(connectionId, default(IConnection)!);

        //return Connections[connectionId];
    }

    public IConnection RemoveFromId(int connectionId) {
        Connections.TryRemove(connectionId, out var connection);

        return connection;
    }

    public bool Contains(int connectionId) {
        return Connections.ContainsKey(connectionId);
    }

    public void Clear() {
        Connections.Clear();
    }

    public int Count() {
        return Connections.Count;
    }

    public IEnumerator<KeyValuePair<int, IConnection>> GetEnumerator() {
        return Connections.GetEnumerator();
    }
}
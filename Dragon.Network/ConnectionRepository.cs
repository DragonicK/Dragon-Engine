using Dragon.Core.Logs;

using System.Collections.Concurrent;

namespace Dragon.Network;

public sealed class ConnectionRepository : IConnectionRepository {
    private readonly ConcurrentDictionary<int, IConnection> connections;
    private readonly ILogger _logger;

    public ConnectionRepository(ILogger logger) {
        _logger = logger;   
        connections = new ConcurrentDictionary<int, IConnection>();
    }

    public IConnection AddClientFromId(int connectionId) {
        if (!connections.ContainsKey(connectionId)) {
            connections.TryAdd(connectionId, new Connection { Id = connectionId, Logger = _logger });
        }

        return connections.GetOrAdd(connectionId, default(IConnection)!);

        //return Connections[connectionId];
    }

    public IConnection GetFromId(int connectionId) {
        return connections.GetOrAdd(connectionId, default(IConnection)!);

        //return Connections[connectionId];
    }

    public IConnection RemoveFromId(int connectionId) {
        connections.TryRemove(connectionId, out var connection);

        return connection;
    }

    public bool Contains(int connectionId) {
        return connections.ContainsKey(connectionId);
    }

    public void DisconnectAll() {
        foreach (var (_, connection) in connections) {
            connection?.Disconnect();
        }
    }

    public void Clear() {
        connections.Clear();
    }

    public int Count() {
        return connections.Count;
    }

    public IEnumerator<KeyValuePair<int, IConnection>> GetEnumerator() {
        return connections.GetEnumerator();
    }
}
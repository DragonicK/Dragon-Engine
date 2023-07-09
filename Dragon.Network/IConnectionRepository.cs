using System.Collections.Concurrent;

namespace Dragon.Network;

public interface IConnectionRepository {
    IConnection AddClientFromId(int connectionId);
    IConnection GetFromId(int connectionId);
    IConnection RemoveFromId(int connectionId);
    bool Contains(int connectionId);
    void DisconnectAll();
    void Clear();
    int Count();
    IEnumerator<KeyValuePair<int, IConnection>> GetEnumerator();
}
using System.Net.Sockets;
using Dragon.Network.Incoming;

namespace Dragon.Network;

public interface IConnection {
    int Id { get; set; }
    bool Connected { get; }
    string IpAddress { get; set; }
    Socket? Socket { get; set; }
    bool Authenticated { get; set; }
    IEngineCrypto CryptoEngine { get; set; }
    IIncomingMessageQueue? IncomingMessageQueue { get; set; }
    EventHandler<IConnection>? OnDisconnect { get; set; }

    void StartBeginReceive();
    void Disconnect();
    void UpdateKey(byte[] buffer);
    void Send(byte[] buffer, int length);
}
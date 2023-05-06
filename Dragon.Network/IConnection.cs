using System.Net.Sockets;

using Dragon.Network.Pool;
using Dragon.Network.Security;
using Dragon.Network.Incoming;

namespace Dragon.Network;

public interface IConnection {
    int Id { get; set; }
    bool Connected { get; }
    Socket? Socket { get; set; }
    byte[] CipherKey { get; set; }
    string IpAddress { get; set; }
    bool Authenticated { get; set; }
    IEngineCrypto CryptoEngine { get; set; }
    IEngineBufferPool? EngineBufferPool { get; set; }
    IIncomingMessageQueue? IncomingMessageQueue { get; set; }
    EventHandler<IConnection>? OnDisconnect { get; set; }

    void StartBeginReceive();
    void Disconnect();
    void UpdateKey(byte[] buffer);
    void Send(byte[] buffer, int length);
}
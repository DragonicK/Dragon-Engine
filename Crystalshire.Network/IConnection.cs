using System.Net.Sockets;

using Crystalshire.Network.Incoming;

namespace Crystalshire.Network {
    public interface IConnection {
        int Id { get; set; }
        bool Connected { get; }
        string IpAddress { get; set; }
        TcpClient Socket { get; set; }
        bool Authenticated { get; set; }
        IIncomingMessageQueue? IncomingMessageQueue { get; set; }
        EventHandler<IConnection>? OnDisconnect { get; set; }

        void Disconnect();
        void Receive();
        void Send(byte[] buffer);
    }
}
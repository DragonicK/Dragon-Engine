using System.Net.Sockets;
using Dragon.Network.Incoming;

namespace Dragon.Network;

public class Connection : IConnection {
    public int Id { get; set; }
    public bool Authenticated { get; set; }
    public string IpAddress { get; set; }
    public TcpClient Socket {
        get {
            return socket;
        }

        set {
            socket = value;
            socket.NoDelay = true;
        }
    }
    public bool Connected => connected;
    public IIncomingMessageQueue? IncomingMessageQueue { get; set; }
    public EventHandler<IConnection>? OnDisconnect { get; set; }

    /// <summary>
    /// Tempo limite de leitura em microsegundos.
    /// </summary>
    public const int ReceiveTimeOut = 15 * 1000 * 1000;

    /// <summary>
    /// Tempo limite de envio em microsegundos.
    /// </summary>
    public const int SendTimeOut = 15 * 1000 * 1000;

    private const int BufferSize = 1024;

    private TcpClient? socket;
    private readonly ByteBuffer msg;
    private readonly byte[] buffer;
    private bool connected = false;

    public Connection() {
        IpAddress = string.Empty;

        buffer = new byte[BufferSize];
        msg = new ByteBuffer(BufferSize);

        connected = true;
    }

    public void Disconnect() {
        connected = false;

        Socket?.Close();

        OnDisconnect?.Invoke(null, this);
    }

    public void Receive() {
        if (socket!.Client is null) {
            return;
        }

        var size = socket.Available;

        if (size == 0) {
            return;
        }

        if (size > BufferSize) {
            size = BufferSize;
        }

        if (socket.Client.Poll(ReceiveTimeOut, SelectMode.SelectRead)) {
            try {
                socket.Client.Receive(buffer, size, SocketFlags.None);
            }
            catch {
                Disconnect();
                return;
            }
        }
        else {
            Disconnect();
            return;
        }

        var pLength = 0;

        msg.Write(buffer, size);

        Array.Clear(buffer, 0, size);

        if (msg.Length() >= 4) {
            pLength = msg.ReadInt32(false);

            if (pLength <= 0) {
                return;
            }
        }

        while (pLength > 0 && pLength <= msg.Length() - 4) {
            if (pLength <= msg.Length() - 4) {
                // Remove the first packet (Size of Packet).
                msg.ReadInt32();

                IncomingMessageQueue?.Enqueue(Id, msg.ReadBytes(pLength));
            }

            pLength = 0;

            if (msg.Length() >= 4) {
                pLength = msg.ReadInt32(false);

                if (pLength < 0) {
                    return;
                }
            }
        }

        msg.Trim();
    }

    public void Send(byte[] buffer) {
        if (socket!.Client is null) {
            return;
        }

        if (socket.Client.Poll(SendTimeOut, SelectMode.SelectWrite)) {
            try {
                socket.Client.Send(buffer, SocketFlags.None);
            }
            catch {
                Disconnect();
            }
        }
        else {
            Disconnect();
        }
    }
}
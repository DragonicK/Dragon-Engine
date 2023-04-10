using System.Net.Sockets;
using Dragon.Core.Logs;
using Dragon.Network.Incoming;

namespace Dragon.Network;

public class Connection : IConnection {
    public int Id { get; set; }
    public bool Authenticated { get; set; }
    public string IpAddress { get; set; }
    public Socket? Socket { get; set; }
    public ILogger? Logger { get; set; }
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

    public void Send(byte[] buffer) {
        Socket?.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, OnSend, null);
    }

    public void StartBeginReceive() {
        Socket?.BeginReceive(buffer, 0, BufferSize, SocketFlags.None, OnReceive, null);
    }

    private void OnReceive(IAsyncResult ar) {
        try {
            var length = Socket!.EndReceive(ar);

            if (length == 0) {
                Disconnect();
            }
            else {
                var pLength = 0;

                msg.Write(buffer, length);

                Array.Clear(buffer, 0, length);

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

                Socket.BeginReceive(buffer, 0, BufferSize, SocketFlags.None, OnReceive, null);
            }
        }
        catch (Exception ex) {
            Socket?.Disconnect(false);

            Logger?.Write(WarningLevel.Error, GetType().Name, $"OnReceive: {ex.Message}");

            Disconnect();
        }
    }

    private void OnSend(IAsyncResult ar) {
        try {
            Socket?.EndSend(ar);
        }
        catch (Exception ex) {
            Socket?.Disconnect(false);

            Logger?.Write(WarningLevel.Error, GetType().Name, $"OnSend: {ex.Message}");

            Disconnect();
        }
    }
}
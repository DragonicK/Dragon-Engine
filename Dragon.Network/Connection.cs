using System.Net.Sockets;
using System.Security.Cryptography;

using Dragon.Core.Logs;

using Dragon.Network.Security;
using Dragon.Network.Incoming;

using Dragon.Network.Pool;

namespace Dragon.Network;

public sealed class Connection : IConnection {
    public int Id { get; set; }
    public bool Authenticated { get; set; }
    public string IpAddress { get; set; }
    public Socket? Socket { get; set; }
    public ILogger? Logger { get; set; }
    public bool Connected => connected;
    public byte[] CipherKey { get; set; }
    public IEngineCrypto CryptoEngine { get; set; }
    public IEngineBufferPool? EngineBufferPool { get; set; }
    public IIncomingMessageQueue? IncomingMessageQueue { get; set; }
    public EventHandler<IConnection>? OnDisconnect { get; set; }

    private const int ReceiveBufferSize = 1024;

    private readonly byte[] buffer;
    private readonly ByteBuffer reader;

    private bool connected = false;

    public Connection() {
        IpAddress = string.Empty;

        buffer = new byte[ReceiveBufferSize];
        reader = new ByteBuffer(ReceiveBufferSize);

        CryptoEngine = new BlowFishCipher();

        CipherKey = RandomNumberGenerator.GetBytes(16);

        connected = true;
    }

    public void Disconnect() {
        connected = false;

        Socket?.Close();

        OnDisconnect?.Invoke(null, this);
    }

    public void Send(byte[] buffer, int length) {
        Socket?.BeginSend(buffer, 0, length, SocketFlags.None, OnSend, null);
    }

    public void StartBeginReceive() {
        Socket?.BeginReceive(buffer, 0, ReceiveBufferSize, SocketFlags.None, OnReceive, null);
    }

    private void OnReceive(IAsyncResult ar) {
        try {
            var length = Socket!.EndReceive(ar);

            if (length == 0) {
                Disconnect();
            }
            else {
                var pLength = 0;

                reader.Write(buffer, length);
                Array.Clear(buffer, 0, length);

                if (reader.Length() >= 4) {
                    pLength = reader.ReadInt32(false);

                    if (pLength <= 0) {
                        return;
                    }
                }

                while (pLength > 0 && pLength <= reader.Length() - 4) {
                    if (pLength <= reader.Length() - 4) {
                        reader.ReadInt32();

                        var sequence = EngineBufferPool?.GetNextBuffer();

                        sequence!.Reset();

                        reader.ReadBytes(sequence.Content, pLength);

                        sequence.Length = pLength;

                        IncomingMessageQueue?.Enqueue(this, Id, sequence);
                    }

                    pLength = 0;

                    if (reader.Length() >= 4) {
                        pLength = reader.ReadInt32(false);

                        if (pLength < 0) {
                            return;
                        }
                    }
                }

                reader.Trim();

                Socket.BeginReceive(buffer, 0, ReceiveBufferSize, SocketFlags.None, OnReceive, null);
            }
        }
        catch (Exception ex) {
            Logger?.Write(WarningLevel.Error, GetType().Name, $"OnReceive: {ex.Message}");

            Disconnect();
        }
    }

    private void OnSend(IAsyncResult ar) {
        try {
            Socket?.EndSend(ar);
        }
        catch (Exception ex) {
            Logger?.Write(WarningLevel.Error, GetType().Name, $"OnSend: {ex.Message}");

            Disconnect();
        }
    }

    public void UpdateKey(byte[] key) {
        CryptoEngine.UpdateKey(key);
    }
}
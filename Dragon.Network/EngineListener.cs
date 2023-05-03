using Dragon.Core.Logs;
using Dragon.Core.GeoIpCountry;

using Dragon.Network.Outgoing;
using Dragon.Network.Incoming;

using System.Net;
using System.Net.Sockets;

namespace Dragon.Network;

public class EngineListener : IEngineListener {
    public int Port { get; set; }
    public int BackLog { get; set; }
    public int MaximumConnections { get; set; }
    public ILogger? Logger { get; set; }
    public IGeoIpAddress GeoIpAddress { get; init; }
    public IIndexGenerator IndexGenerator { get; init; }
    public IConnectionRepository ConnectionRepository { get; init; }
    public IIncomingMessageQueue IncomingMessageQueue { get; init; }
    public IOutgoingMessageWriter OutgoingMessageWriter { get; init; }
    public EventHandler<IConnection>? ConnectionApprovalEvent { get; set; }
    public EventHandler<IConnection>? ConnectionRefuseEvent { get; set; }
    public EventHandler<IConnection>? ConnectionDisconnectEvent { get; set; }

    private const int IpAddressArraySplit = 4;

    private Socket? listener;
    private bool accepting;

    public void Start() {
        listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        listener.Bind(new IPEndPoint(IPAddress.Any, Port));
        listener.Listen(BackLog);
        listener.BeginAccept(OnAccept, null);

        accepting = true;
    }

    public void Stop() {
        accepting = false;
        listener?.Close();
    }

    private void OnAccept(IAsyncResult ar) {
        Socket? socket = null;

        try {
            socket = listener!.EndAccept(ar);

            if (socket is not null) {
                var ipAddress = socket.RemoteEndPoint!.ToString();

                ipAddress = ipAddress!.Remove(ipAddress.IndexOf(':'));

                if (CanAccept(ipAddress)) {
                    var index = IndexGenerator.GetNextIndex();
                    var connection = ConnectionRepository.AddClientFromId(index);

                    if (connection is null) {
                        ConnectionRepository.RemoveFromId(index);
                        connection = ConnectionRepository.AddClientFromId(index);
                    }

                    socket.Blocking = false;
                    socket.NoDelay = true;

                    connection.Socket = socket;
                    connection.IpAddress = ipAddress;
                    connection.IncomingMessageQueue = IncomingMessageQueue;
                    connection.OnDisconnect += OnConnectionDisconnected;

                    connection.StartBeginReceive();

                    RaiseConnectionApproval(connection);
                }
                else {
                    socket.Close();
                }
            }

            if (accepting) {
                listener.BeginAccept(OnAccept, null);
            }
        }
        catch (Exception ex) {
            socket?.Disconnect(false);

            Logger?.Write(WarningLevel.Error, GetType().Name, ex.Message);
        }
    }

    private void RaiseConnectionApproval(IConnection connection) {
        ConnectionApprovalEvent?.Invoke(null, connection);
    }

    private void RaiseConnectionRefuse(string ipAddress) {
        ConnectionRefuseEvent?.Invoke(null, new Connection() { IpAddress = ipAddress });
    }

    private void RaiseConnectionDisconnect(string ipAddress) {
        ConnectionDisconnectEvent?.Invoke(null, new Connection() { IpAddress = ipAddress });
    }

    private void OnConnectionDisconnected(object? sender, IConnection connection) {
        ConnectionDisconnectEvent?.Invoke(null, connection);
    }

    private bool IsValidIpAddress(string ipAddress) {
        if (string.IsNullOrWhiteSpace(ipAddress) || string.IsNullOrEmpty(ipAddress)) {
            return false;
        }

        var values = ipAddress.Split('.');
        if (values.Length != IpAddressArraySplit) {
            return false;
        }

        return values.All(r => byte.TryParse(r, out byte parsing));
    }

    private bool CanAccept(string ipAddress) {
        if (ipAddress is null || !IsValidIpAddress(ipAddress)) {
            RaiseConnectionDisconnect(ipAddress);

            return false;
        }

        if (IsIpAddressBlocked(ipAddress)) {
            RaiseConnectionRefuse(ipAddress);

            return false;
        }

        return true;
    }

    private bool IsIpAddressBlocked(string ipAddress) {
        return GeoIpAddress.IsCountryBlocked(ipAddress);
    }
}
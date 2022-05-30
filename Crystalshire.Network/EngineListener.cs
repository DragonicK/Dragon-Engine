using System.Net;
using System.Net.Sockets;

using Crystalshire.Core.GeoIpCountry;
using Crystalshire.Network.Incoming;
using Crystalshire.Network.Outgoing;
using Crystalshire.Network.Messaging.SharedPackets;

namespace Crystalshire.Network;

public class EngineListener : IEngineListener {
    public int Port { get; set; }
    public int MaximumConnections { get; set; }
    public IGeoIpAddress GeoIpAddress { get; init; }
    public IIndexGenerator IndexGenerator { get; init; }
    public IConnectionRepository ConnectionRepository { get; init; }
    public IIncomingMessageQueue IncomingMessageQueue { get; init; }
    public IOutgoingMessageWriter OutgoingMessageWriter { get; init; }
    public EventHandler<IConnection> ConnectionApprovalEvent { get; set; }
    public EventHandler<IConnection> ConnectionRefuseEvent { get; set; }
    public EventHandler<IConnection> ConnectionDisconnectEvent { get; set; }

    private const int IpAddressArraySplit = 4;
    private const int PingTicket = 5000;

    private TcpListener? listener = null;
    private bool accepting = false;
    private long lastTick;

    public void Start() {
        if (listener is null) {
            listener = new TcpListener(IPAddress.Any, Port);
        }

        listener.Start();
        accepting = true;
    }

    public void Stop() {
        accepting = false;

        listener?.Stop();
    }

    public void Accept() {
        if (accepting) {
            if (listener is not null) {
                if (listener.Pending()) {
                    var socket = listener.AcceptTcpClient();
                    var ipAddress = socket.Client.RemoteEndPoint.ToString();

                    ipAddress = ipAddress!.Remove(ipAddress.IndexOf(':'));

                    if (CanAccept(ipAddress)) {
                        var index = IndexGenerator.GetNextIndex();
                        var connection = ConnectionRepository.AddClientFromId(index);

                        if (connection is null) {
                            ConnectionRepository.RemoveFromId(index);
                            connection = ConnectionRepository.AddClientFromId(index);
                        }

                        connection.Socket = socket;
                        connection.IpAddress = ipAddress;
                        connection.IncomingMessageQueue = IncomingMessageQueue;
                        connection.OnDisconnect += OnConnectionDisconnected;

                        RaiseConnectionApproval(connection);
                    }
                    else {
                        socket.Close();
                    }
                }
            }
        }
    }

    public void Receive() {
        if (ConnectionRepository is not null) {
            foreach (var (_, connection) in ConnectionRepository) {
                if (connection is not null) {
                    connection.Receive();
                }
            }

            #region Check Ping For Disconnection 

            if (OutgoingMessageWriter is not null) {
                var tick = Environment.TickCount64;

                if (tick >= lastTick) {
                    lastTick = tick + PingTicket;

                    var packet = OutgoingMessageWriter.CreateMessage(new PacketPing());

                    packet.TransmissionTarget = TransmissionTarget.Broadcast;

                    OutgoingMessageWriter.Enqueue(packet);
                }
            }

            #endregion
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
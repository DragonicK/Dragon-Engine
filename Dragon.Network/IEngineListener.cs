using Dragon.Core.GeoIpCountry;
using Dragon.Network.Incoming;
using Dragon.Network.Outgoing;

namespace Dragon.Network;

public interface IEngineListener {
    int Port { get; set; }
    int MaximumConnections { get; set; }
    IGeoIpAddress GeoIpAddress { get; init; }
    IIndexGenerator IndexGenerator { get; init; }
    IIncomingMessageQueue IncomingMessageQueue { get; init; }
    IOutgoingMessageWriter OutgoingMessageWriter { get; init; }
    IConnectionRepository ConnectionRepository { get; init; }
    EventHandler<IConnection>? ConnectionApprovalEvent { get; set; }
    EventHandler<IConnection>? ConnectionRefuseEvent { get; set; }
    EventHandler<IConnection>? ConnectionDisconnectEvent { get; set; }
    void Start();
    void Stop();
    void Accept();
    void Receive();
}
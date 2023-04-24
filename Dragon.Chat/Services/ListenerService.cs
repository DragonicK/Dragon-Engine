using Dragon.Network;
using Dragon.Core.Services;

using Dragon.Chat.Server;

namespace Dragon.Chat.Services;

public sealed class ListenerService : IService {
    public ServicePriority Priority => ServicePriority.Low;
    public IEngineListener? ServerListener { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public IncomingMessageService? IncomingMessageService { get; private set; }
    public OutgoingMessageService? OutgoingMessageService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public LoggerService? LoggerService { get; private set; }
    public GeoIpService? GeoIpService { get; private set; }
    public bool IsRunning { get; private set; } = false;

    public void Start() {
        var repository = ConnectionService!.ConnectionRepository;
        var queue = IncomingMessageService!.IncomingMessageQueue;
        var geoIp = GeoIpService!.GeoIpAddress;
        var generator = ConnectionService.IndexGenerator;
        var incomingMessage = IncomingMessageService.IncomingMessageQueue;
        var outgoingWriter = OutgoingMessageService!.OutgoingMessageWriter;

        ServerListener = new EngineListener() {
            MaximumConnections = Configuration!.MaximumConnections,
            Port = Configuration.ChatServer.Port,
            IncomingMessageQueue = incomingMessage!,
            OutgoingMessageWriter = outgoingWriter!,
            ConnectionRepository = repository!,
            Logger = LoggerService!.Logger,
            IndexGenerator = generator!,
            GeoIpAddress = geoIp!
        };

        ServerListener.ConnectionApprovalEvent += WriteFromConnectionApproval;
        ServerListener.ConnectionDisconnectEvent += WriteFromConnectionDisconnect;
        ServerListener.ConnectionRefuseEvent += WriteFromConnectionRefuse;

        ServerListener.Start();

        IsRunning = true;
    }

    public void Stop() {
        IsRunning = false;

        ServerListener?.Stop();
    }

    private void WriteFromConnectionApproval(object? sender, IConnection connection) {
        var join = new JoinServer() {
            Connection = connection,
            Configuration = Configuration,
            Logger = LoggerService!.Logger,
            OutgoingMessageService = OutgoingMessageService
        };

        join.AcceptConnection();
    }

    private void WriteFromConnectionRefuse(object? sender, IConnection connection) {
        var left = new LeftServer() {
            ConnectionRepository = ConnectionService!.ConnectionRepository,
            GeoIpAddress = GeoIpService!.GeoIpAddress,
            Logger = LoggerService!.Logger,
            Configuration = Configuration,
            Connection = connection
        };

        left.RefuseConnection();
    }

    private void WriteFromConnectionDisconnect(object? sender, IConnection connection) {
        var left = new LeftServer() {
            ConnectionRepository = ConnectionService!.ConnectionRepository,
            IndexGenerator = ConnectionService.IndexGenerator,
            GeoIpAddress = GeoIpService!.GeoIpAddress,
            Logger = LoggerService!.Logger,
            Configuration = Configuration,
            Connection = connection
        };

        left.DisconnectConnection();
    }
}
using Dragon.Network;
using Dragon.Core.Services;

using Dragon.Login.Server;

namespace Dragon.Login.Services;

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

    private JoinServer? JoinServer { get; set; }
    private LeftServer? LeftServer { get; set; }

    public void Start() {
        var repository = ConnectionService!.ConnectionRepository;
        var queue = IncomingMessageService!.IncomingMessageQueue;
        var geoIp = GeoIpService!.GeoIpAddress;
        var generator = ConnectionService.IndexGenerator;
        var incomingMessage = IncomingMessageService.IncomingMessageQueue;
        var outgoingWriter = OutgoingMessageService!.OutgoingMessageWriter;

        JoinServer = new JoinServer() {
            LoggerService = LoggerService,
            Configuration = Configuration,
            OutgoingMessageService = OutgoingMessageService
        };

        LeftServer = new LeftServer() {
            GeoIpService = GeoIpService,
            Configuration = Configuration,
            LoggerService = LoggerService,
            ConnectionService = ConnectionService
        };

        ServerListener = new EngineListener() {
            MaximumConnections = Configuration!.MaximumConnections,
            Port = Configuration.LoginServer.Port,
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
        JoinServer?.AcceptConnection(connection);
    }

    private void WriteFromConnectionRefuse(object? sender, IConnection connection) {
        LeftServer?.RefuseConnection(connection);
    }

    private void WriteFromConnectionDisconnect(object? sender, IConnection connection) {
        LeftServer?.DisconnectConnection(connection);
    }
}
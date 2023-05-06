using Dragon.Network;
using Dragon.Core.Services;

using Dragon.Chat.Server;

namespace Dragon.Chat.Services;

public sealed class ListenerService : IService {
    public ServicePriority Priority => ServicePriority.Low;
    public PoolService? PoolService { get; private set; }
    public GeoIpService? GeoIpService { get; private set; }
    public LoggerService? LoggerService { get; private set; }
    public IEngineListener? ServerListener { get; private set; }
    public IServiceContainer? ServiceContainer { get; private set; }
    public IServiceInjector? ServiceInjector { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public OutgoingMessageService? OutgoingMessageService { get; private set; }
    public IncomingMessageService? IncomingMessageService { get; private set; }
    public bool IsRunning { get; private set; } = false;

    private JoinServer? JoinServer { get; set; }
    private LeaveServer? LeaveServer { get; set; }

    public void Start() {
        var geoIp = GeoIpService!.GeoIpAddress;
        var bufferPool = PoolService!.EngineBufferPool!; 
        var generator = ConnectionService!.IndexGenerator;
        var repository = ConnectionService!.ConnectionRepository;
        var queue = IncomingMessageService!.IncomingMessageQueue;
        var incomingMessage = IncomingMessageService.IncomingMessageQueue;
        var outgoingWriter = OutgoingMessageService!.OutgoingMessageWriter;

        JoinServer = new JoinServer(ServiceInjector!);
        LeaveServer = new LeaveServer(ServiceInjector!);

        ServerListener = new EngineListener() {
            MaximumConnections = Configuration!.MaximumConnections,
            Port = Configuration.ChatServer.Port,
            IncomingMessageQueue = incomingMessage!,
            OutgoingMessageWriter = outgoingWriter!,
            ConnectionRepository = repository!,
            Logger = LoggerService!.Logger,
            EngineBufferPool = bufferPool,
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
        LeaveServer?.RefuseConnection(connection);
    }

    private void WriteFromConnectionDisconnect(object? sender, IConnection connection) {
        LeaveServer?.DisconnectConnection(connection);
    }
}
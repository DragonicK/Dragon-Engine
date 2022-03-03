using Crystalshire.Core.Network;
using Crystalshire.Core.Services;

using Crystalshire.Game.Server;

namespace Crystalshire.Game.Services {
    public class ListenerService : IService {
        public ServicePriority Priority => ServicePriority.Low;
        public IEngineListener? ServerListener { get; private set; }
        public ConnectionService? ConnectionService { get; private set; }
        public IncomingMessageService? IncomingMessageService { get; private set; }
        public OutgoingMessageService? OutgoingMessageService { get; private set; }
        public ConfigurationService? Configuration { get; private set; }
        public LoggerService? LoggerService { get; private set; }
        public GeoIpService? GeoIpService { get; private set; }
        public PacketSenderService? PacketSenderService { get; private set; }
        public InstanceService? InstanceService { get; private set; }
        public DatabaseService? DatabaseService { get; private set; }
        public bool IsRunning { get; private set; } = false;

        private Thread? t;

        public void Start() {
            var repository = ConnectionService!.ConnectionRepository;
            var queue = IncomingMessageService!.IncomingMessageQueue;
            var geoIp = GeoIpService!.GeoIpAddress;
            var generator = ConnectionService.IndexGenerator;
            var incomingMessage = IncomingMessageService.IncomingMessageQueue;
            var outgoingWriter = OutgoingMessageService!.OutgoingMessageWriter;

            ServerListener = new EngineListener() {
                MaximumConnections = Configuration!.MaximumConnections,
                Port = Configuration.GameServer.Port,
                IncomingMessageQueue = incomingMessage,
                OutgoingMessageWriter = outgoingWriter,
                ConnectionRepository = repository,
                IndexGenerator = generator,
                GeoIpAddress = geoIp,
            };

            ServerListener.ConnectionApprovalEvent += WriteFromConnectionApproval;
            ServerListener.ConnectionDisconnectEvent += WriteFromConnectionDisconnect;
            ServerListener.ConnectionRefuseEvent += WriteFromConnectionRefuse;

            ServerListener.Start();

            IsRunning = true;

            t = new Thread(Receive);
            t.Start();
        }

        public void Stop() {
            IsRunning = false;
            ServerListener?.Stop();
        }

        private void WriteFromConnectionApproval(object? sender, IConnection connection) {
            var join = new JoinServer() {
                Logger = LoggerService!.ConnectionLogger,
                Configuration = Configuration,
                Connection = connection
            };

            join.AcceptConnection();
        }

        private void WriteFromConnectionRefuse(object? sender, IConnection connection) {
            var join = new LeftServer() {
                ConnectionRepository = ConnectionService.ConnectionRepository,
                PlayerRepository = ConnectionService.PlayerRepository,
                IndexGenerator = ConnectionService.IndexGenerator,
                GeoIpAddress = GeoIpService.GeoIpAddress,
                Logger = LoggerService.ConnectionLogger,
                Configuration = Configuration,
                Connection = connection
            };

            join.RefuseConnection();
        }

        private void WriteFromConnectionDisconnect(object? sender, IConnection connection) {
            var left = new LeftServer() {
                ConnectionRepository = ConnectionService.ConnectionRepository,
                PlayerRepository = ConnectionService.PlayerRepository,
                IndexGenerator = ConnectionService.IndexGenerator,
                PacketSenderService = PacketSenderService,
                GeoIpAddress = GeoIpService.GeoIpAddress,
                Logger = LoggerService.ConnectionLogger,
                DatabaseService = DatabaseService,
                Configuration = Configuration,
                Connection = connection
            };

            left.DisconnectConnection();
        }

        private void Receive() {
            var delay = Configuration!.Delay;

            while (IsRunning) {
                ServerListener?.Accept();
                ServerListener?.Receive();

                if (delay > 0) {
                    Thread.Sleep(delay);
                }
            }
        }
    }
}
using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Services;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

public sealed class UnequipItem {
    public IConnection? Connection { get; set; }
    public CpUnequipItem? Packet { get; set; }
    public LoggerService? LoggerService { get; init; }
    public ContentService? ContentService { get; init; }
    public ConfigurationService? Configuration { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }

    public void Process() {
        var sender = PacketSenderService!.PacketSender;
        var instances = PacketSenderService!.InstanceService;
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                var manager = new ItemManager() {
                    Player = player,
                    PacketSender = sender,
                    InstanceService = instances,
                    Configuration = Configuration,
                    ContentService = ContentService
                };

                manager.UnequipItem(Packet!.EquipmentType);
            }
        }
    }
}
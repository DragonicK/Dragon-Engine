using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Manager;

namespace Crystalshire.Game.Routes;

public sealed class UnequipHeraldry {
    public IConnection? Connection { get; set; }
    public CpUnequipHeraldry? Packet { get; set; }
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
                if (IsValidInventory()) {
                    var manager = new HeraldryManager() {
                        Player = player,
                        PacketSender = sender,
                        InstanceService = instances,
                        Configuration = Configuration,
                        Heraldries = ContentService!.Heraldries,
                        Items = ContentService!.Items
                    };

                    manager.UnequipHeraldry(Packet!.Index);
                }
            }
        }
    }

    private bool IsValidInventory() {
        var index = Packet!.Index;
        return index >= 1 && index <= Configuration!.Player.MaximumHeraldries;
    }
}
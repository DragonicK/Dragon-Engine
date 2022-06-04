using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Services;

namespace Dragon.Game.Routes;

public sealed class ViewEquipmentVisibility {
    public IConnection? Connection { get; set; }
    public CpViewEquipmentVisibility? Packet { get; set; }
    public ConnectionService? ConnectionService { get; init; }
    public ConfigurationService? Configuration { get; init; }

    public void Process() {
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                player.Settings.ViewEquipment = Packet!.IsVisible;
            }
        }
    }
}
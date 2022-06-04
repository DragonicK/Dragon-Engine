using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Model.Crafts;

using Dragon.Game.Services;

namespace Dragon.Game.Routes;

public sealed class StopCraft {
    public IConnection? Connection { get; set; }
    public CpStopCraft? Packet { get; set; }
    public LoggerService? LoggerService { get; init; }
    public ConfigurationService? Configuration { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }

    public void Process() {
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                player.Craft.ProcessingRecipeId = 0;
                player.Craft.State = CraftState.Stopped;
            }
        }
    }
}
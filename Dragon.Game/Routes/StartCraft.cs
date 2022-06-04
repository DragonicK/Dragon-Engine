using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Services;
using Dragon.Game.Players;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

public sealed class StartCraft {
    public IConnection? Connection { get; set; }
    public CpStartCraft? Packet { get; set; }
    public LoggerService? LoggerService { get; init; }
    public ContentService? ContentService { get; init; }
    public ConfigurationService? Configuration { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }

    public void Process() {
        var sender = PacketSenderService!.PacketSender;
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                if (IsValidPacket(player)) {
                    var index = Packet!.Index - 1;

                    var manager = new CraftManager() {
                        Player = player,
                        PacketSender = sender,
                        Configuration = Configuration,
                        Items = ContentService!.Items,
                        Recipes = ContentService!.Recipes,
                        Experience = ContentService!.CraftExperience
                    };

                    manager.Start(index);
                }
            }
        }
    }

    private bool IsValidPacket(IPlayer player) {
        var index = Packet!.Index;

        if (index <= 0) {
            return false;
        }

        if (index > player.Recipes.Count) {
            return false;
        }

        return true;
    }
}
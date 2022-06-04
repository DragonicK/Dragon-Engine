using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Services;

namespace Dragon.Game.Routes;

public sealed class DeleteCraft {
    public IConnection? Connection { get; set; }
    public CpDeleteCraft? Packet { get; set; }
    public LoggerService? LoggerService { get; init; }
    public ConfigurationService? Configuration { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }

    public void Process() {
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                // TODO
            }
        }
    }
}
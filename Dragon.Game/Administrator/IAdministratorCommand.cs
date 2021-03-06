using Dragon.Core.Model;

using Dragon.Game.Network;
using Dragon.Game.Services;
using Dragon.Game.Players;

namespace Dragon.Game.Administrator;

public interface IAdministratorCommand {
    IPlayer? Administrator { get; set; }
    IPacketSender? PacketSender { get; set; }
    AdministratorCommands Command { get; }
    InstanceService? InstanceService { get; set; }
    ConfigurationService? Configuration { get; set; }
    ConnectionService? ConnectionService { get; set; }
    ContentService? ContentService { get; set; }

    void Process(string[]? parameters);
}
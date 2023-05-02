using Dragon.Core.Model;

using Dragon.Game.Services;
using Dragon.Game.Players;

namespace Dragon.Game.Administrator;

public interface IAdministratorCommand {
    AdministratorCommands Command { get; }
    ContentService? ContentService { get; }
    InstanceService? InstanceService { get; }
    ConfigurationService? Configuration { get; }
    ConnectionService? ConnectionService { get; }
    PacketSenderService? PacketSenderService { get; }

    void Process(IPlayer administrator, string[]? parameters);
}
using Crystalshire.Core.Model;

using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Administrator;

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
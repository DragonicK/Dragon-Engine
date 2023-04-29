using Dragon.Core.Model;
using Dragon.Game.Network.Senders;
using Dragon.Game.Players;
using Dragon.Game.Services;


namespace Dragon.Game.Administrator.Commands;

public sealed class AddTitle : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.AddTitle;
    public IPlayer? Administrator { get; set; }
    public IPacketSender? PacketSender { get; set; }
    public InstanceService? InstanceService { get; set; }
    public ConfigurationService? Configuration { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public ContentService? ContentService { get; set; }

    private const int MaximumParameters = 2;

    public void Process(string[]? parameters) {
        if (parameters is not null) {
            if (parameters.Length >= MaximumParameters) {
                ContinueProcess(parameters);
            }
        }
    }

    private void ContinueProcess(string[] parameters) {
        if (Administrator is not null) {
            if (Administrator.AccountLevel >= AccountLevel.Superior) {
                int.TryParse(parameters[1], out var id);

                var repository = ConnectionService!.PlayerRepository;
                var target = repository!.FindByName(parameters[0].Trim());

                Add(target, id);
            }
        }
    }

    private void Add(IPlayer? player, int id) {
        var titles = ContentService!.Titles;

        if (player is not null) {
            if (titles.Contains(id)) {
                var result = player!.Titles.Add(id);

                if (result) {
                    PacketSender!.SendTitles(player);
                }
            }
        }
        else {
            PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, Administrator!);
        }
    }
}
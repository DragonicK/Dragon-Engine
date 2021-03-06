using Dragon.Network;
using Dragon.Network.Outgoing;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Logs;
using Dragon.Core.Model;
using Dragon.Core.Model.Maps;

using Dragon.Game.Server;
using Dragon.Game.Network;
using Dragon.Game.Services;
using Dragon.Game.Players;
using Dragon.Game.Repository;
using Dragon.Game.Characters;

namespace Dragon.Game.Routes;

public sealed class CharacterBegin {
    public IConnection? Connection { get; set; }
    public CpCharacterBegin? Packet { get; set; }
    public LoggerService? LoggerService { get; init; }
    public ContentService? ContentService { get; init; }
    public InstanceService? InstanceService { get; init; }
    public DatabaseService? DatabaseService { get; init; }
    public ConfigurationService? Configuration { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }

    public async void Process() {
        var index = Packet.CharacterIndex;

        var repository = GetPlayerRepository();
        var player = repository.FindByConnectionId(Connection.Id);

        if (index < Configuration.Character.Maximum) {
            if (player.Characters is not null) {
                var count = player.Characters.Count;

                if (index < count) {
                    var result = await LoadPlayer(player, index);

                    if (result) {
                        JoinGame(player);
                    }
                }
            }
        }
        else {
            var sender = GetSender();
            sender?.SendAlertMessage(player, AlertMessageType.Failed, MenuResetType.Characters);
        }
    }

    private async Task<bool> LoadPlayer(IPlayer player, int index) {
        var characterId = player.Characters[index].CharacterId;
        var result = false;

        ICharacterDatabase? database = null;

        try {
            database = new CharacterDatabase(Configuration, DatabaseService.DatabaseFactory);

            result = await database.LoadCharacterAsync(characterId, player);
        }
        catch (Exception ex) {
            await WriteExceptionLog(player.Username, ex.Message);
        }
        finally {
            database?.Dispose();
        }

        return result;
    }

    private void JoinGame(IPlayer player) {
        var game = new JoinGame() {
            Player = player,
            Logger = GetLogger(),
            PacketSender = GetSender(),
            Configuration = Configuration,
            InstanceService = InstanceService,
            ContentService = ContentService
        };

        game.Join();
    }

    private IPlayerRepository? GetPlayerRepository() {
        return ConnectionService?.PlayerRepository;
    }

    private ILogger? GetLogger() {
        return LoggerService?.Logger;
    }

    private MapPassphrase? GetMapPassphrase() {
        return InstanceService?.Passphrases;
    }

    private Task WriteExceptionLog(string username, string message) {
        var logger = GetLogger();

        logger?.Error("CharacterBegin", $"Character: An error ocurred by {username} ... {message}");

        return Task.CompletedTask;
    }

    private IPacketSender? GetSender() {
        return PacketSenderService?.PacketSender;
    }
}
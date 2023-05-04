using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Logs;
using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Server;
using Dragon.Game.Network;
using Dragon.Game.Players;
using Dragon.Game.Characters;

namespace Dragon.Game.Routes;

public sealed class CharacterBegin : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.CharacterBegin;

    private readonly JoinGame JoinGame;
    private readonly ICharacterDatabase Database;

    public CharacterBegin(IServiceInjector injector) : base(injector) {
        JoinGame = new JoinGame(injector);
        Database = new CharacterDatabase(Configuration!, DatabaseService!.DatabaseFactory!);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpCharacterBegin;

        if (received is not null) {
            Execute(connection, received);
        }
    }

    private void Execute(IConnection connection, CpCharacterBegin packet) {
        var index = packet.CharacterIndex;

        var logger = GetLogger();
        var sender = GetPacketSender();
        var repository = GetPlayerRepository();

        var player = repository.FindByConnection(connection);

        if (player is not null) {
            if (index < Configuration!.Character.Maximum) {
                ExecuteLoad(logger, player, index);
            }
            else {
                sender.SendAlertMessage(player, AlertMessageType.Failed, MenuResetType.Characters);
            }
        }
    }

    private async void ExecuteLoad(ILogger logger, IPlayer player, int index) {
        if (player.Characters is not null) {
            var count = player.Characters.Count;

            if (index < count) {
                var success = await LoadPlayer(logger, player, index);

                if (success) {
                    CharacterService!.CancelExclusion(player.Character.CharacterId);

                    JoinGame.Join(player);
                }
            }
        }
    }

    private async Task<bool> LoadPlayer(ILogger logger, IPlayer player, int index) {
        var success = false;
        var characterId = player.Characters[index].CharacterId;

        try {
            success = await Database.LoadCharacterAsync(characterId, player);
        }
        catch (Exception ex) {
            await WriteExceptionLog(logger, player.Username, ex.Message);
        }

        return success;
    }

    private Task WriteExceptionLog(ILogger logger, string username, string message) {
        logger.Error(GetType().Name, $"Character: An error ocurred by {username} ... {message}");

        return Task.CompletedTask;
    }
}
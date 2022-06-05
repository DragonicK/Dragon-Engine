using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Logs;
using Dragon.Core.Model;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Repository;
using Dragon.Game.Services;

namespace Dragon.Game.Routes;

public sealed class CharacterDelete {
    public IConnection? Connection { get; set; }
    public CpCharacterDelete? Packet { get; set; }
    public LoggerService? LoggerService { get; init; }
    public CharacterService? CharacterService { get; init; }
    public ConfigurationService? Configuration { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }

    public void Process() {
        if (!Configuration.Character.Delete) {
            SendCharacterDeleteIsDisabled();
            return;
        }

        var logger = GetLogger();
        var index = Packet.Index;
        var repository = GetPlayerRepository();
        var player = repository.FindByConnectionId(Connection.Id);

        if (IsValidPlayer(player)) {
            if (IsValidIndex(player, index)) {
                var character = player.Characters[index];

                logger?.Warning("CharacterDelete", $"Requesting Exclusion From Id {character.CharacterId}");

                if (CanDelete(character)) {
                    ExecuteExclusion(character, player);

                    logger?.Warning("CharacterDelete", $"Executing Exclusion From Id {character.CharacterId}");
                }
            }
        }
        else {
            WriteInvalidPlayerLog();
        }
    }

    private bool IsValidIndex(IPlayer player, int index) {
        return index >= 0 && index < player.Characters.Count;
    }

    private bool IsValidPlayer(IPlayer? player) {
        return player is not null && player.Characters is not null;
    }

    private async void ExecuteExclusion(CharacterPreview character, IPlayer player) {
        if (!CharacterService.IsAdded(character.CharacterId)) {
            var level = character.Level;
            var minutes = GetMinutes(level);
            var sender = PacketSenderService.PacketSender;

            var request = await CharacterService.AddExclusion(character, player, minutes);

            character.DeleteRequest = request;

            sender?.SendCharacters(player);
        }
    }

    private int GetMinutes(int level) {
        var minutes = 0;
        var index = GetRangeIndex(level);

        if (index >= 0) {
            minutes = Configuration.Character.DeletionLevelRanges[index].Minutes;
        }

        return minutes;
    }

    private int GetRangeIndex(int level) {
        return Configuration.Character.DeletionLevelRanges.FindIndex(x => level >= x.Minimum && level <= x.Maximum);
    }

    private bool CanDelete(CharacterPreview character) {
        return !CharacterService.IsAdded(character.CharacterId);
    }

    private void WriteInvalidPlayerLog() {
        var logger = GetLogger();

        logger?.Warning("CharacterDelete", "Character Exclusion: Player not found");
    }

    private IPlayerRepository? GetPlayerRepository() {
        return ConnectionService?.PlayerRepository;
    }

    private ILogger? GetLogger() {
        return LoggerService?.Logger;
    }

    private void SendCharacterDeleteIsDisabled() {
        var sender = PacketSenderService.PacketSender;
        sender?.SendAlertMessage(Connection, AlertMessageType.CharacterDelete, MenuResetType.Characters);
    }
}
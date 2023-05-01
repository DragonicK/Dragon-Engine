using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Logs;
using Dragon.Core.Model;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Network;
using Dragon.Game.Services;
using Dragon.Core.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Routes;

public sealed class CharacterDelete : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.CharacterDelete;

    public CharacterDelete(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var sender = GetPacketSender();

        if (!Configuration!.Character.Delete) {
            SendCharacterDeleteIsDisabled(sender, connection);
        }
        else {
            var received = packet as CpCharacterDelete;

            if (received is not null) {
                Execute(sender, connection, received);
            }
        }
    }

    private void Execute(IPacketSender sender, IConnection connection, CpCharacterDelete packet) {
        var index = packet.Index;

        var logger = GetLogger();
        var player = GetPlayerRepository().FindByConnectionId(connection.Id);

        if (IsValidPlayer(player)) {
            if (IsValidIndex(player!, index)) {
                var character = player!.Characters[index];

                logger.Warning(GetType().Name, $"Requesting Exclusion From Id {character.CharacterId}");

                if (CanDelete(character)) {
                    ExecuteExclusion(sender, character, player);

                    logger.Warning(GetType().Name, $"Executing Exclusion From Id {character.CharacterId}");
                }
            }
        }
        else {
            WriteInvalidPlayerLog(logger);
        }
    }

    private static bool IsValidIndex(IPlayer player, int index) {
        return index >= 0 && index < player.Characters.Count;
    }

    private static bool IsValidPlayer(IPlayer? player) {
        return player is not null && player.Characters is not null;
    }

    private async void ExecuteExclusion(IPacketSender sender, CharacterPreview character, IPlayer player) {
        if (!CharacterService!.IsAdded(character.CharacterId)) {
            var level = character.Level;

            var minutes = GetMinutes(level);

            var request = await CharacterService.AddExclusion(character, player, minutes);

            character.DeleteRequest = request;

            sender.SendCharacters(player);
        }
    }

    private int GetMinutes(int level) {
        var index = GetRangeIndex(level);

        return index >= 0 ? Configuration!.Character.DeletionLevelRanges[index].Minutes : 0;
    }

    private int GetRangeIndex(int level) {
        return Configuration!.Character.DeletionLevelRanges.FindIndex(x => level >= x.Minimum && level <= x.Maximum);
    }

    private bool CanDelete(CharacterPreview character) {
        return !CharacterService!.IsAdded(character.CharacterId);
    }

    private void WriteInvalidPlayerLog(ILogger logger) {
        logger.Warning(GetType().Name, "Character Exclusion: Player not found");
    }

    private void SendCharacterDeleteIsDisabled(IPacketSender sender, IConnection connection) {
        sender.SendAlertMessage(connection, AlertMessageType.CharacterDelete, MenuResetType.Characters);
    }
}
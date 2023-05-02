using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Players;
using Dragon.Game.Characters;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Routes;

public sealed class CharacterCreate : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.CharacterCreate;

    private readonly ICharacterValidator Validator;
    private readonly ICharacterDatabase CharacterDatabase;
    private readonly ICharacterCreator CharacterCreator;

    public CharacterCreate(IServiceInjector injector) : base(injector) {
        Validator = new CharacterValidator(injector);
        CharacterCreator = new CharacterCreator(injector);
        CharacterDatabase = new CharacterDatabase(Configuration!, DatabaseService!.DatabaseFactory!);
    }
   
    public async void Process(IConnection connection, object packet) {
        var received = packet as CpCharacterCreate;

        if (received is not null) {
            var sender = GetPacketSender();
            var player = FindByConnection(connection);

            if (player is not null) {
                if (!Configuration!.Character.Create) {
                    sender.SendAlertMessage(connection, AlertMessageType.CharacterCreation, MenuResetType.Characters);

                    return;
                }

                if (player.Characters.Count >= Configuration.Character.Maximum) {
                    sender.SendAlertMessage(connection, AlertMessageType.Connection, MenuResetType.Characters);

                    return;
                }

                if (IsValidPacket(connection, sender, received)) {
                    var success = await CouldCreateCharacter(player, sender, received);

                    if (success) {
                        sender.SendCharacters(player);
                    }
                }
            }
        }
    }

    private async Task<bool> CouldCreateCharacter(IPlayer player, IPacketSender sender, CpCharacterCreate packet) {
        var exists = false;
        var isCreated = 0;

        try {
            exists = await CharacterDatabase.ExistCharacterAsync(packet.Name);

            if (!exists) {
                var classJob = Validator.GetValidClass(packet.ClassIndex);
                var classModel = Validator.GetClassModel(classJob, (Gender)packet.Gender, packet.ModelIndex);

                var character = CharacterCreator.CreateCharacter(player, classJob, classModel, packet.Name, packet.Gender, packet.CharacterIndex);

                isCreated = await CharacterDatabase.AddCharacter(character);

                if (isCreated > 0) {
                    var characterId = character.CharacterId;

                    var skills = CharacterCreator.CreateSkills(classJob, characterId);
                    var inventories = CharacterCreator.CreateInventories(classJob, characterId);
                    var passives = CharacterCreator.CreatePassives(classJob, characterId);
                    var equipments = CharacterCreator.CreateEquipments(classJob, characterId);

                    await CharacterDatabase.AddSkills(skills);
                    await CharacterDatabase.AddPassives(passives);
                    await CharacterDatabase.AddInventories(inventories);
                    await CharacterDatabase.AddEquipments(equipments);
                }

                player.Characters = await CharacterDatabase.GetCharactersPreviewAsync(player.AccountId);
            }
        }
        catch (Exception ex) {
            await WriteExceptionLog(player.Username, ex.Message);
        }

        if (exists) {
            sender.SendAlertMessage(player, AlertMessageType.NameTaken, MenuResetType.Characters);

            return false;
        }

        return isCreated > 0;
    }

    private bool IsValidPacket(IConnection connection, IPacketSender sender, CpCharacterCreate packet) {
        var validated = Validator.ValidateName(packet.Name);

        if (!CanValidateNext(ref validated)) {
            SendValidationResult(connection, sender, ref validated);

            return false;
        }

        validated = Validator.ValidateGender(packet.Gender);

        if (!CanValidateNext(ref validated)) {
            SendValidationResult(connection, sender, ref validated);
           
            return false;
        }

        validated = Validator.ValidateClass(packet.ClassIndex);

        if (!CanValidateNext(ref validated)) {
            SendValidationResult(connection, sender, ref validated);

            return false;
        }

        validated = Validator.ValidateCharacterIndex(packet.CharacterIndex);

        if (!CanValidateNext(ref validated)) {
            SendValidationResult(connection, sender, ref validated);

            return false;
        }

        validated = Validator.ValidateModel(Validator.GetValidClass(packet.ClassIndex), (Gender)packet.Gender, packet.ModelIndex);

        if (!CanValidateNext(ref validated)) {
            SendValidationResult(connection, sender, ref validated);

            return false;
        }

        return true;
    }

    private bool CanValidateNext(ref CharacterValidatorResult validationResult) {
        return validationResult.AlertMessageType == AlertMessageType.None;
    }

    private void SendValidationResult(IConnection connection, IPacketSender sender, ref CharacterValidatorResult validationResult) {
        sender.SendAlertMessage(connection, validationResult.AlertMessageType, validationResult.MenuResetType, validationResult.Disconnect);
    }

    private Task WriteExceptionLog(string username, string message) {
        GetLogger().Error(GetType().Name, $"Character: An error ocurred by {username} ... {message}");

        return Task.CompletedTask;
    }
}
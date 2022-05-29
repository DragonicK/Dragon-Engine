using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Core.Logs;
using Crystalshire.Core.Model;

using Crystalshire.Game.Network;
using Crystalshire.Game.Players;
using Crystalshire.Game.Repository;
using Crystalshire.Game.Services;
using Crystalshire.Game.Characters;

namespace Crystalshire.Game.Routes {
    public sealed class CharacterCreate {
        public IConnection? Connection { get; set; }
        public CpCharacterCreate? Packet { get; set; }
        public LoggerService? LoggerService { get; init; }
        public ContentService? ContentService { get; init; }
        public DatabaseService? DatabaseService { get; init; }
        public CharacterService? CharacterService { get; init; }
        public ConfigurationService? Configuration { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public PacketSenderService? PacketSenderService { get; init; }

        private ICharacterValidation? validation;

        public async void Process() {
            var player = GetPlayerRepository().FindByConnectionId(Connection.Id);
            var sender = GetSender();

            if (!Configuration.Character.Create) {
                sender?.SendAlertMessage(Connection, AlertMessageType.CharacterCreation, MenuResetType.Characters);
                return;
            }

            if (player.Characters.Count >= Configuration.Character.Maximum) {
                sender?.SendAlertMessage(Connection, AlertMessageType.Connection, MenuResetType.Characters);
                return;
            }
     
            if (IsDataValidated()) {
                var result = await CouldCreateCharacter(player);

                if (result) {
                    sender?.SendCharacters(player);
                }
            }
        }

        private async Task<bool> CouldCreateCharacter(IPlayer player) {
            ICharacterDatabase? database = null;
            ICharacterCreation creation;

            var exists = false;
            var isCreated = 0;

            try {
                database = new CharacterDatabase(Configuration, DatabaseService.DatabaseFactory);
                exists = await database.ExistCharacterAsync(validation.CharacterName);

                if (!exists) {
                    creation = new CharacterCreation(player, Configuration, validation);

                    var character = creation.CreateCharacter();

                    isCreated = await database.AddCharacter(character);

                    if (isCreated > 0) {
                        var skills = creation.CreateSkills(character);
                        var inventories = creation.CreateInventories(character);
                        var passives = creation.CreatePassives(character);
                        var equipments = creation.CreateEquipments(character);

                        await database.AddSkills(skills);
                        await database.AddPassives(passives);
                        await database.AddInventories(inventories);
                        await database.AddEquipments(equipments);
                    }

                    player.Characters = await database.GetCharactersPreviewAsync(player.AccountId);
                }
            }
            catch (Exception ex) {
                await WriteExceptionLog(player.Username, ex.Message);
            }
            finally {
                database?.Dispose();
            }

            if (exists) {
                var sender = GetSender();

                sender?.SendAlertMessage(player, AlertMessageType.NameTaken, MenuResetType.Characters);

                return false;
            }

            return isCreated > 0;
        }

        private bool IsDataValidated() {
            var name = Packet.Name;
            var gender = Packet.Gender;
            var classIndex = Packet.ClassIndex;
            var model = Packet.ModelIndex;
            var index = Packet.CharacterIndex;

            validation = new CharacterValidation(ContentService.Classes, Configuration);

            var validated = validation.ValidateName(name);

            if (!CanValidateNext(validated)) {
                SendValidationResult(validated);
                DisconnectWhenNeeded(validated);
                return false;
            }

            validated = validation.ValidateGender(gender);

            if (!CanValidateNext(validated)) {
                SendValidationResult(validated);
                DisconnectWhenNeeded(validated);
                return false;
            }

            validated = validation.ValidateClass(classIndex);

            if (!CanValidateNext(validated)) {
                SendValidationResult(validated);
                DisconnectWhenNeeded(validated);
                return false;
            }

            validated = validation.ValidateModel(model);

            if (!CanValidateNext(validated)) {
                SendValidationResult(validated);
                DisconnectWhenNeeded(validated);
                return false;
            }

            validated = validation.ValidateCharacterIndex(index);

            if (!CanValidateNext(validated)) {
                SendValidationResult(validated);
                DisconnectWhenNeeded(validated);
                return false;
            }

            return true;
        }

        private void DisconnectWhenNeeded(CharacterValidationResult validationResult) {
            if (validationResult.Disconnect) {
                Connection.Disconnect();
            }
        }

        private bool CanValidateNext(CharacterValidationResult validationResult) {
            return validationResult.AlertMessageType == AlertMessageType.None;
        }

        private void SendValidationResult(CharacterValidationResult validationResult) {
            var sender = GetSender();

            sender?.SendAlertMessage(Connection,                
                validationResult.AlertMessageType,
                validationResult.MenuResetType,
                validationResult.Disconnect
                );
        }

        private IPlayerRepository? GetPlayerRepository() {
            return ConnectionService?.PlayerRepository;
        }

        private ILogger? GetLogger() {
            return LoggerService?.ServerLogger;
        }

        private Task WriteExceptionLog(string username, string message) {
            var logger = GetLogger();

            var description = new Description() {
                Name = "Character Creation Excpetion",
                WarningCode = WarningCode.Error,
                Message = $"An error ocurred by {username} ... {message}",
            };

            logger?.Write(description);

            OutputLog.Write($"Character Creation throw an exception ... ");

            return Task.CompletedTask;
        }

        private IPacketSender? GetSender() {
            return PacketSenderService?.PacketSender;
        }
    }
}
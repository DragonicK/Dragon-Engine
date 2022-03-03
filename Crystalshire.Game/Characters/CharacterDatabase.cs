using Crystalshire.Core.Database;
using Crystalshire.Core.Database.Handler;
using Crystalshire.Core.Model.Characters;

using Crystalshire.Game.Configurations;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Characters {
    public class CharacterDatabase : ICharacterDatabase {
        private readonly MembershipHandler handler;

        public CharacterDatabase(IConfiguration configuration, IDatabaseFactory factory) {
            handler = factory.GetMembershipHandler(configuration.DatabaseMembership);
        }

        public async Task<int> AddCharacter(Character character) {
            return await handler.AddAsync(character);
        }

        public async Task<int> AddInventories(IList<CharacterInventory> list) {
            return await handler.AddRangeAsync(list);
        }

        public async Task<int> AddEquipments(IList<CharacterEquipment> list) {
            return await handler.AddRangeAsync(list);
        }

        public async Task<int> AddSkills(IList<CharacterSkill> list) {
            return await handler.AddRangeAsync(list);
        }

        public async Task<int> AddPassives(IList<CharacterPassive> list) {
            return await handler.AddRangeAsync(list);
        }

        public void Dispose() {
            handler?.Dispose();
        }

        public async Task<bool> LoadCharacterAsync(long characterId, IPlayer player) {
            var account = await handler.GetFullAccountAsync(player.Username);
            var character = await handler.GetCharacterAsync(characterId);
            var achievements = await handler.GetAchievementsAsync(characterId);
            var appearance = await handler.GetAppearanceAsync(characterId);
            var craft = await handler.GetCraftAsync(characterId);
            var attributes = await handler.GetPrimaryAttributesAsync(characterId);
            var slots = await handler.GetQuickSlotAsync(characterId);
            var settings = await handler.GetSettingsAsync(characterId);
            var vitals = await handler.GetVitalsAsync(characterId);
            var progresses = await handler.GetAchievementsProgressAsync(characterId);
            var effects = await handler.GetEffectsAsync(characterId);
            var currencies = await handler.GetCurrenciesAsync(characterId);
            var equipments = await handler.GetEquipmentsAsync(characterId);
            var heraldries = await handler.GetHeraldriesAsync(characterId);
            var inventories = await handler.GetInventoriesAsync(characterId);
            var warehouse = await handler.GetWarehouseAsync(characterId);
            var mails = await handler.GetMailsAsync(characterId);
            var passives = await handler.GetPassivesAsync(characterId);
            var recipes = await handler.GetRecipesAsync(characterId);
            var skills = await handler.GetSkillsAsync(characterId);
            var titles = await handler.GetTitlesAsync(characterId);

            foreach (var mail in mails) {
                if (mail.AttachItemFlag) {
                    mail.MailAttachItem = await handler.GetMailAttachItemsAsync(mail.Id);
                }
            }

            player.Account = account;
            player.Character = character;
            player.Services = new PlayerService(player.AccountId, player.Account.AccountService);
            player.Achievements = new PlayerAchievement(characterId, achievements);
            player.Appearance = new PlayerAppearance(characterId, appearance);
            player.Craft = new PlayerCraft(characterId, craft);
            player.PrimaryAttributes = new PlayerPrimaryAttribute(characterId, attributes);
            player.Currencies = new PlayerCurrency(characterId, currencies);
            player.QuickSlots = new PlayerQuickSlot(characterId, slots);
            player.Settings = new PlayerSettings(characterId, settings);
            player.Vitals = new PlayerVital(characterId, vitals);
            player.Inventories = new PlayerInventory(characterId, inventories);
            player.Equipments = new PlayerEquipment(characterId, equipments);
            player.Effects = new PlayerEffect(characterId, effects);
            player.AchievementsProgress = new PlayerAchievementProgress(characterId, progresses);
            player.Heraldries = new PlayerHeraldry(characterId, heraldries);
            player.Warehouse = new PlayerWarehouse(characterId, warehouse);
            player.Mails = new PlayerMail(characterId, mails);
            player.Passives = new PlayerPassive(characterId, passives);
            player.Recipes = new PlayerRecipe(characterId, recipes);
            player.Skills = new PlayerSkill(characterId, skills);
            player.Titles = new PlayerTitle(characterId, titles);
            player.Auras = new PlayerAura();

            return true;
        }

        public async Task<bool> ExistCharacterAsync(string name) {
            return await handler.ExistCharacterAsync(name);
        }

        public async Task<long> GetCharacterIdAsync(string name) {
            return await handler.GetCharacterIdAsync(name);
        }

        public async Task<int> SaveMailAsync(CharacterMail mail) {
            return await handler.SaveMailAsync(mail);
        }

        public async Task<List<CharacterPreview>> GetCharactersPreviewAsync(long accountId) {
            var characters = await handler.GetCharactersAsync(accountId);
            var requests = await handler.GetDeleteRequestAsync(accountId);

            var list = new List<CharacterPreview>(characters.Count);

            foreach (var character in characters) {
                var request = requests.FirstOrDefault(x => x.CharacterId == character.CharacterId);
                var preview = new CharacterPreview(character, request);

                list.Add(preview);
            }

            return list;
        }

        public async Task<int> DeleteCharacterAsync(long characterId) {
            return await handler.DeleteCharacterAsync(characterId);
        }

        public async Task<int> UpdateDeleteRequest(long id) {
            return await handler.UpdateDeleteRequestAsync(id);
        }

        public async Task<CharacterDeleteRequest> AddCharacterDeleteRequest(IDeleteRequest request, bool isActive, string ipAddress, string machineId) {
            var dbRequest = new CharacterDeleteRequest() {
                IsActive = isActive,
                Name = request.Name,
                AccountId = request.AccountId,
                CharacterId = request.CharacterId,
                RequestDate = request.RequestDate,
                ExclusionDate = request.ExclusionDate,
                IpAddress = ipAddress,
                MachineId = machineId
            };

            await handler.AddDeleteRequestAsync(dbRequest);

            return dbRequest;
        }
    }
}

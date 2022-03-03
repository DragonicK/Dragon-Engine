using Microsoft.EntityFrameworkCore;

using Crystalshire.Core.Model.Accounts;
using Crystalshire.Core.Model.Characters;
using Crystalshire.Core.Database.Context;

namespace Crystalshire.Core.Database.Handler {
    public class MembershipHandler : IDisposable {
        private readonly MembershipContext Context;
        private bool disposed = false;

        public MembershipHandler(MembershipContext accountContext) {
            Context = accountContext;
        }

        public async Task<bool> ExistCharacterAsync(string name) {
            return await Context.Characters.FirstOrDefaultAsync(x => x.Name == name && !x.IsDeleted) is not null;         
        }

        public async Task<int> AddAsync(Account account) {
            await Context.Accounts.AddAsync(account);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> AddAsync(Character character) {
            await Context.Characters.AddAsync(character);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IList<CharacterInventory> list) {
            await Context.CharacterInventory.AddRangeAsync(list);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IList<CharacterEquipment> list) {
            await Context.CharacterEquipment.AddRangeAsync(list);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IList<CharacterSkill> list) {
            await Context.CharacterSkill.AddRangeAsync(list);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IList<CharacterPassive> list) {
            await Context.CharacterPassive.AddRangeAsync(list);
            return await Context.SaveChangesAsync();
        }

        #region Get Character Content

        public async Task<long> GetCharacterIdAsync(string name) {
            long id = 0;

            var c = await Context.Characters.FirstOrDefaultAsync(x => x.Name == name && !x.IsDeleted);

            if (c is not null) {
                id = c.CharacterId;
            }

            return id;
        }

        public async Task<Character> GetCharacterAsync(long characterId) {
            return await Context.Characters.FirstOrDefaultAsync(x => x.CharacterId == characterId);
        }

        public async Task<CharacterAppearance> GetAppearanceAsync(long characterId) {
            return await Context.CharacterAppearance.FirstOrDefaultAsync(x => x.CharacterId == characterId);
        }

        public async Task<CharacterCraft> GetCraftAsync(long characterId) {
            return await Context.CharacterCraft.FirstOrDefaultAsync(x => x.CharacterId == characterId);
        }

        public async Task<CharacterPrimaryAttribute> GetPrimaryAttributesAsync(long characterId) {
            return await Context.CharacterPrimaryAttribute.FirstOrDefaultAsync(x => x.CharacterId == characterId);
        }

        public async Task<IList<CharacterQuickSlot>> GetQuickSlotAsync(long characterId) {
            return await Context.CharacterQuickSlot.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<CharacterSettings> GetSettingsAsync(long characterId) {
            return await Context.CharacterSettings.FirstOrDefaultAsync(x => x.CharacterId == characterId);
        }

        public async Task<CharacterVital> GetVitalsAsync(long characterId) {
            return await Context.CharacterVital.FirstOrDefaultAsync(x => x.CharacterId == characterId);
        }

        public async Task<IList<CharacterAchievement>> GetAchievementsAsync(long characterId) {
            return await Context.CharacterAchievement.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<IList<CharacterAchievementProgress>> GetAchievementsProgressAsync(long characterId) {
            return await Context.CharacterAchievementProgress.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<IList<CharacterAttributeEffect>> GetEffectsAsync(long characterId) {
            return await Context.CharacterAttributeEffect.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<IList<CharacterCurrency>> GetCurrenciesAsync(long characterId) {
            return await Context.CharacterCurrency.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<IList<CharacterEquipment>> GetEquipmentsAsync(long characterId) {
            return await Context.CharacterEquipment.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<IList<CharacterHeraldry>> GetHeraldriesAsync(long characterId) {
            return await Context.CharacterHeraldry.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<IList<CharacterInventory>> GetInventoriesAsync(long characterId) {
            return await Context.CharacterInventory.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<IList<CharacterWarehouse>> GetWarehouseAsync(long characterId) {
            return await Context.CharacterWarehouse.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<IList<CharacterMail>> GetMailsAsync(long characterId) {
            return await Context.CharacterMail
                .Where(x => x.ReceiverCharacterId == characterId && !x.DeleteFlag)
                .ToListAsync();
        }

        public async Task<CharacterMailAttachItem> GetMailAttachItemsAsync(long mailId) {
            return await Context.CharacterMailAttachItem.Where(x => x.MailId == mailId).FirstOrDefaultAsync();
        }

        public async Task<IList<CharacterPassive>> GetPassivesAsync(long characterId) {
            return await Context.CharacterPassive.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<IList<CharacterRecipe>> GetRecipesAsync(long characterId) {
            return await Context.CharacterRecipe.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<IList<CharacterSkill>> GetSkillsAsync(long characterId) {
            return await Context.CharacterSkill.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<IList<CharacterTitle>> GetTitlesAsync(long characterId) {
            return await Context.CharacterTitle.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        #endregion

        #region Save Character Content

        public async Task<int> SaveCharacterAsync(Character character) {
            Context.Entry(character).State = EntityState.Modified;
            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveCraftAsync(CharacterCraft craft) {
            if (craft.Id > 0) {
                Context.Entry(craft).State = EntityState.Modified;
            }
            else {
                Context.CharacterCraft!.Add(craft);
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveVitalAsync(CharacterVital vital) {
            if (vital.Id > 0) {
                Context.Entry(vital).State = EntityState.Modified;
            }
            else {
                Context.CharacterVital!.Add(vital);
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveCurrencyAsync(IList<CharacterCurrency> list) {
            for (var i = 0; i < list.Count; i++) {
                var item = list[i];

                if (item.Id == 0) {
                    Context.CharacterCurrency!.Add(item);
                }
                else if (item.Id > 0) {
                    Context.Entry(item).State = EntityState.Modified;
                }
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveInventoryAsync(IList<CharacterInventory> list) {
            for (var i = 0; i < list.Count; i++) {
                var item = list[i];

                if (item.Id > 0 && item.ItemId == 0) {
                    Context.CharacterInventory!.Remove(item);
                }
                else if (item.Id == 0 && item.ItemId > 0) {
                    Context.CharacterInventory!.Add(item);
                }
                else if (item.Id > 0 && item.ItemId > 0) {
                    Context.Entry(item).State = EntityState.Modified;
                }
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveEquipmentAsync(IList<CharacterEquipment> list) {
            for (var i = 0; i < list.Count; i++) {
                var item = list[i];

                if (item.Id > 0 && item.ItemId == 0) {
                    Context.CharacterEquipment!.Remove(item);
                }
                else if (item.Id == 0 && item.ItemId > 0) {
                    Context.CharacterEquipment!.Add(item);
                }
                else if (item.Id > 0 && item.ItemId > 0) {
                    Context.Entry(item).State = EntityState.Modified;
                }
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveHeraldryAsync(IList<CharacterHeraldry> list) {
            for (var i = 0; i < list.Count; i++) {
                var item = list[i];

                if (item.Id > 0 && item.ItemId == 0) {
                    Context.CharacterHeraldry!.Remove(item);
                }
                else if (item.Id == 0 && item.ItemId > 0) {
                    Context.CharacterHeraldry!.Add(item);
                }
                else if (item.Id > 0 && item.ItemId > 0) {
                    Context.Entry(item).State = EntityState.Modified;
                }
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveWarehouseAsync(IList<CharacterWarehouse> list) {
            for (var i = 0; i < list.Count; i++) {
                var item = list[i];

                if (item.Id > 0 && item.ItemId == 0) {
                    Context.CharacterWarehouse!.Remove(item);
                }
                else if (item.Id == 0 && item.ItemId > 0) {
                    Context.CharacterWarehouse!.Add(item);
                }
                else if (item.Id > 0 && item.ItemId > 0) {
                    Context.Entry(item).State = EntityState.Modified;
                }
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveRecipesAsync(IList<CharacterRecipe> list) {
            for (var i = 0; i < list.Count; i++) {
                var item = list[i];

                if (item.Id > 0 && item.RecipeId == 0) {
                    Context.CharacterRecipe!.Remove(item);
                }
                else if (item.Id == 0 && item.RecipeId > 0) {
                    Context.CharacterRecipe!.Add(item);
                }
                else if (item.Id > 0 && item.RecipeId > 0) {
                    Context.Entry(item).State = EntityState.Modified;
                }
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveQuickSlotAsync(IList<CharacterQuickSlot> list) {
            for (var i = 0; i < list.Count; i++) {
                var item = list[i];

                if (item.Id > 0 && item.ObjectValue == 0) {
                    Context.CharacterQuickSlot!.Remove(item);
                }
                else if (item.Id == 0 && item.ObjectValue > 0) {
                    Context.CharacterQuickSlot!.Add(item);
                }
                else if (item.Id > 0 && item.ObjectValue > 0) {
                    Context.Entry(item).State = EntityState.Modified;
                }
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveEffectsAsync(IList<CharacterAttributeEffect> list) {
            for (var i = 0; i < list.Count; i++) {
                var item = list[i];

                if (!item.IsAura) {
                    if (item.Id > 0 && item.EffectId == 0) {
                        Context.CharacterAttributeEffect!.Remove(item);
                    }
                    else if (item.Id == 0 && item.EffectId > 0) {
                        Context.CharacterAttributeEffect!.Add(item);
                    }
                    else if (item.Id > 0 && item.EffectId > 0) {
                        Context.Entry(item).State = EntityState.Modified;
                    }
                }
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveSkillsAsync(IList<CharacterSkill> list) {
            for (var i = 0; i < list.Count; i++) {
                var item = list[i];

                if (item.Id > 0 && item.SkillId == 0) {
                    Context.CharacterSkill!.Remove(item);
                }
                else if (item.Id == 0 && item.SkillId > 0) {
                    Context.CharacterSkill!.Add(item);
                }
                else if (item.Id > 0 && item.SkillId > 0) {
                    Context.Entry(item).State = EntityState.Modified;
                }
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<int> SavePassivesAsync(IList<CharacterPassive> list) {
            for (var i = 0; i < list.Count; i++) {
                var item = list[i];

                if (item.Id > 0 && item.PassiveId == 0) {
                    Context.CharacterPassive!.Remove(item);
                }
                else if (item.Id == 0 && item.PassiveId > 0) {
                    Context.CharacterPassive!.Add(item);
                }
                else if (item.Id > 0 && item.PassiveId > 0) {
                    Context.Entry(item).State = EntityState.Modified;
                }
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveMailsAsync(IList<CharacterMail> list) {
            for (var i = 0; i < list.Count; i++) {
                var item = list[i];

                if (item.Id == 0) {
                    Context.CharacterMail!.Add(item);

                    await Context.SaveChangesAsync();

                    if (item.MailAttachItem is not null) {
                        item.MailAttachItem.MailId = item.Id;

                        Context.CharacterMailAttachItem!.Add(item.MailAttachItem);
                    }    
                }
                else if (item.Id > 0) {
                    Context.Entry(item).State = EntityState.Modified;
                }
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveMailAsync(CharacterMail mail) {
            Context.CharacterMail!.Add(mail);

            await Context.SaveChangesAsync();

            if (mail.MailAttachItem is not null) {
                mail.MailAttachItem.MailId = mail.Id;

                Context.CharacterMailAttachItem!.Add(mail.MailAttachItem);
            }

            return await Context.SaveChangesAsync();
        }

        #endregion

        public async Task<Account> GetFullAccountAsync(string username) {
            var account = Context.Accounts
                .Include(x => x.AccountAuthentication)
                .FirstOrDefault(x => x.Username == username);

            if (account is not null) {
                var accountId = account.AccountId;

                account.AccountService = await Context.AccountService
                    .Where(x => x.AccountId == accountId && !x.Expired)
                    .ToListAsync();
            }

            return account;       
        }

        public async Task<Account> GetAccountWithLockAsync(string username) {
            return await Context.Accounts
                .Include(x => x.AccountLock)
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<Account> GetAccountWithAuthenticationAndServiceAsync(long accountId) {
            return await Context.Accounts!
                .Include(x => x.AccountAuthentication)
                .Include(x => x.AccountService)
                .FirstOrDefaultAsync(x => x.AccountId == accountId);
        }

        public async Task<int> PutAccountAsync(Account account) {
            Context.Entry(account).State = EntityState.Modified;
            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveFullAccountAsync(Account account) {
            Context.Entry(account).State = EntityState.Modified;

            if (account.AccountService is not null) {
                var services = account.AccountService;

                foreach (var service in services) {
                    if (service.Id == 0) {
                        Context.AccountService.Add(service);
                    }
                    else if (service.Id > 0 && service.ServiceId > 0) {
                        Context.Entry(service).State = EntityState.Modified;
                    }
                }
            }

            if (account.AccountAuthentication is not null) {
                Context.Entry(account.AccountAuthentication).State = EntityState.Modified;
            }

            return await Context.SaveChangesAsync();
        }

        public async Task<List<Character>> GetCharactersAsync(long accountId) {
            return await Context.Characters.Where(x => x.AccountId == accountId && !x.IsDeleted).ToListAsync();
        }

        public async Task<List<CharacterDeleteRequest>> GetDeleteRequestAsync(long accountId) {
            return await Context.CharacterDeleteRequest.Where(x => x.AccountId == accountId && x.IsActive).ToListAsync();
        }

        public async Task<int> UpdateDeleteRequestAsync(long id) {
            var request = Context.CharacterDeleteRequest.Where(x => x.Id == id).FirstOrDefault();

            request.IsActive = false;

            Context.Entry(request).State = EntityState.Modified;

            return await Context.SaveChangesAsync();
        }

        public async Task<int> AddDeleteRequestAsync(CharacterDeleteRequest request) {
            Context.CharacterDeleteRequest.Add(request);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> DeleteCharacterAsync(long characterId) {
            var character = Context.Characters.Where(x => x.CharacterId == characterId).FirstOrDefault();

            character.IsDeleted = true;

            Context.Entry(character).State = EntityState.Modified;

            return await Context.SaveChangesAsync();
        }

        public bool CanConnect() {
            return Context.Database.CanConnect();
        }

        public void Dispose() {
            Dispose(true);
        }

        private void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    Context.Dispose();
                }

                disposed = true;
            }
        }
    }
}
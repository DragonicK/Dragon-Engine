using Crystalshire.Core.Model.Characters;

using Crystalshire.Game.Players;

namespace Crystalshire.Game.Characters {
    public interface ICharacterDatabase {
        void Dispose();

        Task<int> AddCharacter(Character character);
        Task<int> AddInventories(IList<CharacterInventory> list);
        Task<int> AddEquipments(IList<CharacterEquipment> list);
        Task<int> AddSkills(IList<CharacterSkill> list);
        Task<int> AddPassives(IList<CharacterPassive> list);
        Task<bool> ExistCharacterAsync(string name);
        Task<bool> LoadCharacterAsync(long characterId, IPlayer player);
        Task<List<CharacterPreview>> GetCharactersPreviewAsync(long accountId);
        Task<int> DeleteCharacterAsync(long characterId);
        Task<int> UpdateDeleteRequest(long id);
        Task<int> SaveMailAsync(CharacterMail mail);
        Task<long> GetCharacterIdAsync(string name);
        Task<CharacterDeleteRequest> AddCharacterDeleteRequest(IDeleteRequest request, bool isActive, string ipAddress, string machineId);
    }
}
using Dragon.Core.Content;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public interface IPlayerWarehouse {
    IDatabase<Item>? Items { get; set; }
    int Count { get; }
    IList<CharacterWarehouse> ToList();
    CharacterWarehouse? FindByItemId(int id);
    CharacterWarehouse? FindByIndex(int index);
    CharacterWarehouse? FindFreeInventory(int maximum);
    int Deposit(CharacterInventory inventory, int amount, int maximumWarehouse);
    int Deposit(CharacterInventory inventory, int maximumWarehouse);
    void Swap(int source, int destination);
}
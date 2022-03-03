using Crystalshire.Core.Model;
using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Items;
using Crystalshire.Core.Model.Recipes;

using Crystalshire.Game.Players;
using Crystalshire.Game.Network;
using Crystalshire.Game.Configurations;

namespace Crystalshire.Game.Manager {
    public class RecipeManager {
        public IPlayer? Player { get; init; }
        public IDatabase<Item>? Items { get; init; }
        public IDatabase<Recipe>? Recipes { get; init; }
        public IPacketSender? PacketSender { get; init; }
        public IConfiguration? Configuration { get; init; }

        public void UseRecipe(int index, Item item) {
            if (Recipes is null) {
                return;
            }

            var recipeId = item.RecipeId;

            if (Recipes.Contains(recipeId)) {
                var recipe = Recipes[recipeId]!;
                var profession = Player!.Craft.Profession;

                if (profession == recipe.CraftType) {

                    if (Player.Recipes.Add(recipeId)) {
                        PacketSender!.SendAddRecipe(Player, recipeId);

                        var inventory = Player.Inventories.FindByIndex(index);

                        if (inventory is not null) {
                            if (item.MaximumStack > 0) {
                                inventory.Value--;

                                if (inventory.Value <= 0) {
                                    inventory.Clear();
                                }
                            }
                            else {
                                inventory.Clear();
                            }

                            PacketSender!.SendInventoryUpdate(Player, index);
                        }
                    }
                    else {
                        if (Player.Recipes.Count >= Configuration!.Player.MaximumRecipes) {
                            PacketSender!.SendMessage(SystemMessage.RecipeListIsFull, QbColor.BrigthRed, Player!);
                        }
                    }

                }
                else {
                    PacketSender!.SendMessage(SystemMessage.InvalidProfession, QbColor.BrigthRed, Player!);
                }
            }
        }
    }
}

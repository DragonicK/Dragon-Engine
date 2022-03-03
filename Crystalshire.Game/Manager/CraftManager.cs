using Crystalshire.Core.Model;
using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Items;
using Crystalshire.Core.Model.Crafts;
using Crystalshire.Core.Model.Recipes;
using Crystalshire.Core.Model.Characters;

using Crystalshire.Game.Network;
using Crystalshire.Game.Configurations;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Manager {
    public class CraftManager {
        public IPlayer? Player { get; init; }
        public IPacketSender? PacketSender { get; init; }
        public IConfiguration? Configuration { get; init; }
        public IDatabase<Item>? Items { get; init; }
        public IDatabase<Recipe>? Recipes { get; init; }
        public Experience? Experience { get; init; }

        private const int Step = 10;

        public void Start(int index) {
            if (Recipes is null) {
                return;
            }

            var recipeId = Player!.Recipes.GetId(index);

            if (Recipes.Contains(recipeId)) {
                var recipe = Recipes[recipeId];

                if (HasRequirements(recipe!)) {
                    ContinueStartProcess(recipe!);
                }
                else {
                    PacketSender!.SendMessage(SystemMessage.YouDoNotHaveEnoughMaterial, QbColor.Red, Player!);
                }
            }
        }

        public void Conclude() {
            if (Recipes is null) {
                return;
            }

            if (Player!.Craft.State == CraftState.Started) {
                var recipeId = Player.Craft.ProcessingRecipeId;

                if (Recipes.Contains(recipeId)) {
                    var recipe = Recipes[recipeId];

                    if (HasRequirements(recipe!)) {
                        TakeRequiredItems(recipe!);
                        ContinueConcludeProcess(recipe!);
                    }
                }
            }
        }

        private void ContinueStartProcess(Recipe recipe) {
            var inventory = GetInventory(recipe, out var _);

            if (inventory is not null) {
                Player!.Craft.ProcessingRecipeId = recipe.Id;
                Player!.Craft.State = CraftState.Started;

                PacketSender!.SendStartCraft(Player!, Step);
            }
            else {
                PacketSender!.SendMessage(SystemMessage.InventoryFull, QbColor.BrigthRed, Player!);
            }
        }

        private void ContinueConcludeProcess(Recipe recipe) {
            var inventory = GetInventory(recipe!, out var shouldStack);

            Player!.Craft.State = CraftState.Stopped;
            Player!.Craft.ProcessingRecipeId = 0;

            if (inventory is not null) {
                if (shouldStack) {
                    inventory.Value += recipe.Reward.Value;
                }
                else {
                    inventory.ItemId = recipe.Reward.Id;
                    inventory.Value = recipe.Reward.Value;
                    inventory.Level = recipe.Reward.Level;
                }

                PacketSender!.SendMessage(SystemMessage.ItemCreated, QbColor.BrigthGreen, Player!);
                PacketSender!.SendInventoryUpdate(Player, inventory.InventoryIndex);

                ApplyExperience(recipe);
            }
        }

        private bool HasRequirements(Recipe recipe) {
            var items = recipe.Required;

            for (var i = 0; i < items.Count; ++i) {
                if (!HasItem(items[i])) {
                    return false;
                }
            }

            return true;
        }

        private bool HasItem(RecipeItem required) {
            if (required.Id == 0) {
                return true;
            }

            var count = 0;
            var inventories = Player!.Inventories.ToList();

            for (var i = 0; i < inventories.Count; ++i) {
                if (inventories[i].ItemId == required.Id && inventories[i].Level == required.Level) {
                    count += inventories[i].Value;

                    if (count >= required.Value) {
                        return true;
                    }
                }
            }       

            return false;
        }

        private CharacterInventory? GetInventory(Recipe recipe, out bool isStacked) {
            isStacked = false;

            if (Items is null) {
                return null;
            }

            CharacterInventory? inventory = default;

            if (Items.Contains(recipe.Reward.Id)) {
                var item = Items[recipe.Reward.Id]!;

                if (item.MaximumStack > 0) {
                    isStacked = true;

                    inventory = Player!.Inventories.FindByItemId(item.Id);
                }

                if (inventory is null) {
                    isStacked = false; 

                    var maximum = Player!.Character.MaximumInventories;
                    inventory = Player!.Inventories.FindFreeInventory(maximum);
                }
            }

            return inventory;
        }

        private void TakeRequiredItems(Recipe recipe) {
            var items = recipe.Required;

            for (var i = 0; i < items.Count; ++i) {
                TakeRequiredItem(items[i]);
            }
        }

        private void TakeRequiredItem(RecipeItem required) {
            var inventories = Player!.Inventories.ToList();
            var value = required.Value;
            int rest;

            CharacterInventory? inventory;

            for (var i = 0; i < inventories.Count; ++i) {
                inventory = inventories[i];

                if (inventory.ItemId == required.Id && inventory.Level == required.Level) {
                    rest = inventory.Value - value;

                    if (rest < 0) {
                        rest = value - inventory.Value;
                        inventory.Clear();
                    }
                    else {
                        inventory.Value -= value;
                    }

                    if (inventory.Value <= 0) {
                        inventory.Clear();
                    }

                    PacketSender!.SendInventoryUpdate(Player, inventory.InventoryIndex);

 
                    if (rest == 0) {
                        return;
                    }
                    else {
                        value = rest;
                    }

                }
            }
        }

        private void ApplyExperience(Recipe recipe) {
            var maximum = Configuration!.Craft.MaximumLevel;
            var level = Player!.Craft.Level;

            if (level < maximum) {
                var rates = Configuration!.Rates.Craft; // TODO + Service Rates

                var experience = Convert.ToInt32(recipe.Experience * rates);

                var count = GetLevelUpCount(level, maximum, experience);

                if (count > 0) {
                    PacketSender!.SendCraftData(Player!);
                }
                else {
                    PacketSender!.SendCraftExperience(Player!);
                }
            }
        }

        private int GetLevelUpCount(int level, int maximum, int experience) {
            var count = 0;

            if (Experience is not null) {
                experience += Player!.Craft.Experience;

                if (level >= maximum) {
                    if (experience > Experience.Get(maximum)) {
                        experience = Experience.Get(maximum);

                        Player!.Craft.Experience = experience;
                        Player!.Craft.NextLevelExperience = experience;
                    }

                    return 0;
                }

                while (experience >= Experience.Get(level)) {
                    var rest = experience - Experience.Get(level);

                    experience = rest;

                    count++;
                    level++;

                    if (level >= maximum) {
                        break;
                    }
                }

                Player!.Craft.Level = level;
                Player!.Craft.Experience = experience;
                Player!.Craft.NextLevelExperience = Experience.Get(level);

                if (level >= maximum) {
                    if (experience > Experience.Get(maximum)) {
                        experience = Experience.Get(maximum);

                        Player!.Craft.Experience = experience;
                        Player!.Craft.NextLevelExperience = experience;
                    }
                }
            }

            return count;
        }
    }
}

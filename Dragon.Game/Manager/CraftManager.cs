using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Crafts;
using Dragon.Core.Model.Recipes;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class CraftManager {
    public ContentService? ContentService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int Step = 10;

    public CraftManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void Start(IPlayer player, int index) {
        var sender = GetPacketSender();
        var recipes = GetDatabaseRecipe();

        var recipeId = player.Recipes.GetId(index);

        recipes.TryGet(recipeId, out var recipe);

        if (recipe is not null) {
            if (HasRequirements(player, recipe)) {
                ContinueStartProcess(sender, player, recipe);
            }
            else {
                sender.SendMessage(SystemMessage.YouDoNotHaveEnoughMaterial, QbColor.Red, player);
            }
        }
    }

    public void Conclude(IPlayer player) {
        var sender = GetPacketSender();
        var recipes = GetDatabaseRecipe();

        if (player!.Craft.State == CraftState.Started) {
            var recipeId = player.Craft.ProcessingRecipeId;

            recipes.TryGet(recipeId, out var recipe);

            if (recipe is not null) {
                if (HasRequirements(player, recipe)) {
                    TakeRequiredItems(sender, player, recipe);
                    ContinueConcludeProcess(sender, player, recipe);
                }
            }
        }
    }

    private void ContinueStartProcess(IPacketSender sender, IPlayer player, Recipe recipe) {
        var inventory = GetInventory(player, recipe, out var _);

        if (inventory is not null) {
            player.Craft.ProcessingRecipeId = recipe.Id;
            player.Craft.State = CraftState.Started;

            sender.SendStartCraft(player, Step);
        }
        else {
            sender.SendMessage(SystemMessage.InventoryFull, QbColor.BrigthRed, player);
        }
    }

    private void ContinueConcludeProcess(IPacketSender sender, IPlayer player, Recipe recipe) {
        var inventory = GetInventory(player, recipe, out var shouldStack);

        player.Craft.State = CraftState.Stopped;
        player.Craft.ProcessingRecipeId = 0;

        if (inventory is not null) {
            if (shouldStack) {
                inventory.Value += recipe.Reward.Value;
            }
            else {
                inventory.ItemId = recipe.Reward.Id;
                inventory.Value = recipe.Reward.Value;
                inventory.Level = recipe.Reward.Level;
            }

            sender.SendMessage(SystemMessage.ItemCreated, QbColor.BrigthGreen, player);
            sender.SendInventoryUpdate(player, inventory.InventoryIndex);

            ApplyExperience(sender, player, recipe);
        }
    }

    private bool HasRequirements(IPlayer player, Recipe recipe) {
        var items = recipe.Required;

        for (var i = 0; i < items.Count; ++i) {
            if (!HasItem(player, items[i])) {
                return false;
            }
        }

        return true;
    }

    private bool HasItem(IPlayer player, RecipeItem required) {
        if (required.Id == 0) {
            return true;
        }

        var count = 0;
        var inventories = player.Inventories.ToList();

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

    private CharacterInventory? GetInventory(IPlayer player, Recipe recipe, out bool isStacked) {
        isStacked = false;

        var items = GetDatabaseItem();

        CharacterInventory? inventory = default;

        items.TryGet(recipe.Reward.Id, out var item);

        if (item is not null) {
            if (item.MaximumStack > 0) {
                isStacked = true;

                inventory = player.Inventories.FindByItemId(item.Id);
            }

            if (inventory is null) {
                isStacked = false;

                var maximum = player.Character.MaximumInventories;
                inventory = player.Inventories.FindFreeInventory(maximum);
            }
        }

        return inventory;
    }

    private void TakeRequiredItems(IPacketSender sender, IPlayer player, Recipe recipe) {
        var items = recipe.Required;

        for (var i = 0; i < items.Count; ++i) {
            TakeRequiredItem(sender, player, items[i]);
        }
    }

    private void TakeRequiredItem(IPacketSender sender, IPlayer player, RecipeItem required) {
        var inventories = player.Inventories.ToList();
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

                sender.SendInventoryUpdate(player, inventory.InventoryIndex);

                if (rest == 0) {
                    return;
                }
                else {
                    value = rest;
                }
            }
        }
    }

    private void ApplyExperience(IPacketSender sender, IPlayer player, Recipe recipe) {
        var maximum = Configuration!.Craft.MaximumLevel;

        var level = player.Craft.Level;

        if (level < maximum) {
            var rates = Configuration!.Rates.Craft; // TODO + Service Rates

            var experience = Convert.ToInt32(recipe.Experience * rates);

            var count = GetLevelUpCount(player, level, maximum, experience);

            if (count > 0) {
                sender.SendCraftData(player);
            }
            else {
                sender.SendCraftExperience(player);
            }
        }
    }

    private int GetLevelUpCount(IPlayer player, int level, int maximum, int experience) {
        var count = 0;

        var database = GetExperience();

        experience += player.Craft.Experience;

        if (level >= maximum) {
            if (experience > database.Get(maximum)) {
                experience = database.Get(maximum);

                player.Craft.Experience = experience;
                player.Craft.NextLevelExperience = experience;
            }

            return 0;
        }

        while (experience >= database.Get(level)) {
            var rest = experience - database.Get(level);

            experience = rest;

            count++;
            level++;

            if (level >= maximum) {
                break;
            }
        }

        player.Craft.Level = level;
        player.Craft.Experience = experience;
        player.Craft.NextLevelExperience = database.Get(level);

        if (level >= maximum) {
            if (experience > database.Get(maximum)) {
                experience = database.Get(maximum);

                player.Craft.Experience = experience;
                player.Craft.NextLevelExperience = experience;
            }
        }

        return count;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IDatabase<Item> GetDatabaseItem() {
        return ContentService!.Items;
    }

    private IDatabase<Recipe> GetDatabaseRecipe() {
        return ContentService!.Recipes;
    }

    private Experience GetExperience() {
        return ContentService!.CraftExperience;
    }
}
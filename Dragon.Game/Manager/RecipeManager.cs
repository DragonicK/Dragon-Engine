using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Recipes;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class RecipeManager {
    public ContentService? ContentService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    public RecipeManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void UseRecipe(IPlayer player, int index, Item item) {
        var recipeId = item.RecipeId;

        var sender = GetPacketSender();
        var recipes = GetDatabaseRecipe();

        recipes.TryGet(recipeId, out var recipe);

        if (recipe is not null) {
            var profession = player.Craft.Profession;

            if (profession == recipe.CraftType) {
                if (player.Recipes.Add(recipeId)) {              
                    sender.SendAddRecipe(player, recipeId);

                    var inventory = player.Inventories.FindByIndex(index);

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

                        sender.SendInventoryUpdate(player, index);
                    }
                }
                else {
                    if (player.Recipes.Count >= Configuration!.Player.MaximumRecipes) {
                        sender.SendMessage(SystemMessage.RecipeListIsFull, QbColor.BrigthRed, player);
                    }
                }

            }
            else {
                sender.SendMessage(SystemMessage.InvalidProfession, QbColor.BrigthRed, player);
            }
        }
    }

    private IDatabase<Recipe> GetDatabaseRecipe() {
        return ContentService!.Recipes;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}
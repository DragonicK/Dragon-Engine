using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Shops;
using Dragon.Core.Model.Conversations;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class ConversationManager {
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int FirstChatIndex = 0;

    private readonly WarperManager WarperManager;
    private readonly GiveEffectManager GiveEffectManager;

    public ConversationManager(IServiceInjector injector) {
        injector.Inject(this);

        WarperManager = new WarperManager(injector);
        GiveEffectManager = new GiveEffectManager(injector);
    }

    public void ProcessOptions(IPlayer player, int id, int chatIndex, int option) {
        var conversations = GetDatabaseConversation();

        conversations.TryGet(id, out var conversation);

        if (conversation is not null) {
            var sender = GetPacketSender();
            var count = conversation.ChatCount;

            if (count > 0 && chatIndex <= count) {
                chatIndex--;

                var chat = conversation.Chats[chatIndex];

                if (option != 0) {
                    ContinueNextChatOptions(sender, player, conversation, chat, id, option);
                }
                else {
                    ProcessFirstChat(sender, player, chat, id);
                }
            }
        }
    }

    private void ProcessFirstChat(IPacketSender sender, IPlayer player, Chat chat, int id) {
        if (chat.Event != ConversationEvent.Close) {
            ExecuteEventAndCanMoveToNextChat(sender, player, chat);

            sender.SendConversationOption(player, id, FirstChatIndex);
        }
        else {
            sender.SendConversationClose(player);
        }
    }

    private void ContinueNextChatOptions(IPacketSender sender, IPlayer player, Conversation conversation, Chat chat, int id, int option) {
        if (IsInRange(chat, option)) {
            option--;

            var chatIndex = GetNextChatIndex(chat, option);

            chat = conversation.Chats[chatIndex];

            if (chat.Event != ConversationEvent.Close) {
                if (ExecuteEventAndCanMoveToNextChat(sender, player, chat)) {
                    sender.SendConversationOption(player, id, chatIndex);
                }
            }
            else {
                sender.SendConversationClose(player);
            }
        }
    }

    private bool ExecuteEventAndCanMoveToNextChat(IPacketSender sender, IPlayer player, Chat chat) {
        var id = chat.Data1;
        var param1 = chat.Data2;
        var param2 = chat.Data3;

        switch (chat.Event) {
            case ConversationEvent.OpenShop:
                sender.SendConversationClose(player);

                var shops = GetDatabaseShop();

                shops.TryGet(id, out var shop);

                if (shop is not null) {
                    player.ShopId = id;
                    sender.SendShopOpen(player, shop);
                }

                break;

            case ConversationEvent.OpenBank:
                player.IsWarehouseOpen = true;
                sender.SendConversationClose(player);
                sender.SendWarehouseOpen(player);

                return false;

            case ConversationEvent.OpenCraft:
                sender.SendConversationClose(player);
                sender.SendCraftOpen(player);

                return false;

            case ConversationEvent.OpenUpgrade:
                sender.SendConversationClose(player);
                sender.SendUpgradeOpen(player);

                return false;

            case ConversationEvent.GiveItem:

                return false;

            case ConversationEvent.GiveEffect:
                sender.SendConversationClose(player);

                ExecuteEffect(player, id, param1, param2);

                return false;

            case ConversationEvent.LearnCraft:
                sender.SendConversationClose(player);

                return false;

            case ConversationEvent.StartQuest:
                sender.SendConversationClose(player);

                return false;

            case ConversationEvent.ShowQuestReward:

                return true;

            case ConversationEvent.Teleport:
                sender.SendConversationClose(player);

                ExecuteWarp(player,id, param1, param2);

                return false;
        }

        return true;
    }

    private void ExecuteEffect(IPlayer player, int id, int level, int duration) {
        GiveEffectManager.GiveEffect(player, id, level, duration);
    }

    private void ExecuteWarp(IPlayer player, int id, int x, int y) {
        var instance = GetInstance(id);

        if (instance is not null) {
            WarperManager.Warp(player, instance, x, y);
        }
    }

    private bool IsInRange(Chat chat, int option) {
        return option >= 1 && option <= chat.Reply.Count;
    }

    private int GetNextChatIndex(Chat chat, int option) {
        return chat.Reply[option].Target - 1;
    }

    private IInstance? GetInstance(int instanceId) {
        var instances = InstanceService!.Instances;

        instances.TryGetValue(instanceId, out var instance);

        return instance;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IDatabase<Shop> GetDatabaseShop() {
        return ContentService!.Shops;
    }

    private IDatabase<Conversation> GetDatabaseConversation() {
        return ContentService!.Conversations;
    }
}
using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Shops;
using Crystalshire.Core.Model.Effects;
using Crystalshire.Core.Model.Conversations;

using Crystalshire.Game.Players;
using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Instances;

namespace Crystalshire.Game.Manager {
    public class ConversationManager {
        public IPlayer? Player { get; init; }
        public IDatabase<Shop>? Shops { get; init; }
        public IDatabase<Effect>? Effects { get; init; }
        public IDatabase<Conversation>? Conversations { get; init; }
        public IPacketSender? PacketSender { get; init; }
        public InstanceService? InstanceService { get; init; }

        private const int FirstChatIndex = 0;

        public void ProcessOptions(int id, int chatIndex, int option) {
            var conversation = GetConversation(id);

            if (conversation is not null) {
                var count = conversation.ChatCount;

                if (count > 0 && chatIndex <= count) {
                    chatIndex--;

                    var chat = conversation.Chats[chatIndex];

                    if (option != 0) {
                        ContinueNextChatOptions(conversation, chat, id, option);
                    } 
                    else {
                        ProcessFirstChat(chat, id);
                    }                           
                }
            }
        }

        private void ProcessFirstChat(Chat chat, int id) {
            if (chat.Event != ConversationEvent.Close) {
                ExecuteEventAndCanMoveToNextChat(chat);

                PacketSender!.SendConversationOption(Player!, id, FirstChatIndex);
            }
            else {
                PacketSender!.SendConversationClose(Player!);
            }
        }

        private void ContinueNextChatOptions(Conversation conversation, Chat chat, int id, int option) {
            if (IsInRange(chat, option)) {
                option--;
                
                var chatIndex = GetNextChatIndex(chat, option);

                chat = conversation.Chats[chatIndex];

                if (chat.Event != ConversationEvent.Close) {
                    if (ExecuteEventAndCanMoveToNextChat(chat)) {
                        PacketSender!.SendConversationOption(Player!, id, chatIndex);
                    }
                }
                else {
                    PacketSender!.SendConversationClose(Player!);
                }
            }
        }

        private bool ExecuteEventAndCanMoveToNextChat(Chat chat) {
            var id = chat.Data1;
            var param1 = chat.Data2;
            var param2 = chat.Data3;

            switch (chat.Event) {
                case ConversationEvent.OpenShop:
                    PacketSender!.SendConversationClose(Player!);

                    var shop = GetShop(id);

                    if (shop is not null) {
                        Player!.ShopId = id;
                        PacketSender!.SendShopOpen(Player!, shop);
                    }

                    break;

                case ConversationEvent.OpenBank:
                    Player!.IsWarehouseOpen = true;
                    PacketSender!.SendConversationClose(Player!);
                    PacketSender!.SendWarehouseOpen(Player!);

                    return false;

                case ConversationEvent.OpenCraft:
                    PacketSender!.SendConversationClose(Player!);
                    PacketSender!.SendCraftOpen(Player!);

                    return false;

                case ConversationEvent.OpenUpgrade:
                    PacketSender!.SendConversationClose(Player!);
                    PacketSender!.SendUpgradeOpen(Player!);

                    return false;

                case ConversationEvent.GiveItem:

                    return false;

                case ConversationEvent.GiveEffect:
                    PacketSender!.SendConversationClose(Player!);

                    ExecuteEffect(id, param1, param2);

                    return false;

                case ConversationEvent.LearnCraft:
                    PacketSender!.SendConversationClose(Player!);

                    return false;

                case ConversationEvent.StartQuest:
                    PacketSender!.SendConversationClose(Player!);

                    return false;

                case ConversationEvent.ShowQuestReward:

                    return true;

                case ConversationEvent.Teleport:
                    PacketSender!.SendConversationClose(Player!);

                    ExecuteWarp(id, param1, param2);

                    return false;        
            }

            return true;
        }

        private void ExecuteEffect(int id, int level, int duration) {
            var manager = new GiveEffectManager() {
                Effects = Effects,
                PacketSender = PacketSender,
                InstanceService = InstanceService
            };

            manager.GiveEffect(Player!, id, level, duration);
        }

        private void ExecuteWarp(int id, int x, int y) {
            var instance = GetInstance(id);

            if (instance is not null) {
                var warper = new WarperManager() {
                    InstanceService = InstanceService,
                    PacketSender = PacketSender,
                    Player = Player
                };

                warper.Warp(instance, x, y);
            }
        }

        private bool IsInRange(Chat chat, int option) {
            return option >= 1 && option <= chat.Reply.Count;
        }

        private int GetNextChatIndex(Chat chat, int option) {
            return chat.Reply[option].Target - 1;
        }

        private Conversation? GetConversation(int id) {
            if (Conversations is not null) {
                return Conversations[id];
            }

            return null;
        }

        private Shop? GetShop(int id) {
            if (Shops is not null) {
                return Shops[id];
            }

            return null;
        }

        private IInstance? GetInstance(int instanceId) {
            var instances = InstanceService!.Instances;

            if (instances.ContainsKey(instanceId)) {
                return instances[instanceId];
            }

            return null;
        }
    }
}
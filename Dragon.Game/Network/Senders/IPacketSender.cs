﻿using Dragon.Network;

using Dragon.Core.Model;
using Dragon.Core.Model.Shops;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Mailing;
using Dragon.Core.Model.Premiums;
using Dragon.Core.Model.Upgrades;
using Dragon.Core.Model.Characters;
using Dragon.Core.Model.DisplayIcon;
using Dragon.Core.Model.BlackMarket;

using Dragon.Game.Parties;
using Dragon.Game.Players;
using Dragon.Game.Messages;
using Dragon.Game.Instances;
using Dragon.Game.Instances.Chests;

namespace Dragon.Game.Network.Senders;

public interface IPacketSender {
    void SendPing(IConnection connection);
    void SendGettingMap(IPlayer player, bool isGettingMap);
    void SendCharacters(IPlayer player);
    void SendAlertMessage(IPlayer player, AlertMessageType alertMessage, MenuResetType resetType, bool kick = false, bool forced = false);
    void SendAlertMessage(IConnection connection, AlertMessageType alertMessage, MenuResetType resetType, bool kick = false, bool forced = false);
    void SendPlayerIndex(IPlayer player);
    void SendServerConfiguration(IPlayer player);       
    void SendConnectChatServer(IPlayer player, string token);
    void SendLoadMap(IPlayer player);
    void SendInGame(IPlayer player);
    void SendPlayerData(IPlayer player);
    void SendPlayerDataTo(IPlayer player, IInstance instance);
    void SendHighIndex(IInstance instance);
    void SendClearPlayers(IPlayer player);
    void SendPlayerXY(IPlayer player);
    void SendPlayerXY(IPlayer player, IInstance instance);
    void SendPlayersOnMapTo(IPlayer player, IInstance instance);
    void SendPlayerVital(IPlayer player);
    void SendPlayerVital(IPlayer player, IInstance instance);
    void SendAttributes(IPlayer player);
    void SendDirection(IPlayer player, IInstance instance);
    void SendPlayerMovement(IPlayer player, MovementType movement, IInstance instance);
    void SendPlayerLeft(IPlayer player, IInstance instance, int index);
    void SendTitles(IPlayer player);
    void SendTitle(IPlayer player, IInstance instance);
    void SendExperience(IPlayer player, int experience, int maximum);
    void SendAttributePoint(IPlayer player);
    void SendInventory(IPlayer player);
    void SendInventoryUpdate(IPlayer player, int inventoryIndex);
    void SendMessage(SystemMessage message, QbColor color, string[]? parameters = null);
    void SendMessage(SystemMessage message, QbColor color, IPlayer player, string[]? parameters = null);
    void SendMessage(SystemMessage message, QbColor color, IInstance player, string[]? parameters = null);
    void SendPlayerModel(IPlayer player, IInstance instance);
    void SendEquipment(IPlayer player);
    void SendEquipmentUpdate(IPlayer player, PlayerEquipmentType index);
    void SendHeraldry(IPlayer player);
    void SendHeraldryUpdate(IPlayer player, int index);
    void SendWarehouse(IPlayer player);
    void SendWarehouseUpdate(IPlayer player, int index);
    void SendCraftData(IPlayer player);
    void SendCraftExperience(IPlayer player);
    void SendRecipes(IPlayer player);
    void SendAddRecipe(IPlayer player, int recipeId);
    void SendStartCraft(IPlayer player, int progress);
    void SendMessage(ref Damage message, IInstance instance);
    void SendQuickSlot(IPlayer player);
    void SendQuickSlotUpdate(IPlayer player, int index);
    void SendTradeInvite(IPlayer starter, IPlayer invited);
    void SendCloseTrade(IPlayer player);
    void SendOpenTrade(IPlayer player, string text, string otherText, string whoStartedName);
    void SendTradeMyInventory(IPlayer player, int index, int inventory, int amount);
    void SendTradeOtherInventory(IPlayer player, CharacterInventory inventory);
    void SendTradeState(IPlayer player, TradeState state, TradeState myState, TradeState otherState);
    void SendTradeCurrency(IPlayer player, int starterAmount, int invitedAmount);
    void SendCurrency(IPlayer player);
    void SendCurrencyUpdate(IPlayer player, CurrencyType id);
    void SendInstanceEntities(IPlayer player, IInstance instance);
    void SendInstanceEntity(IInstance instance, int index);
    void SendInstanceEntityVital(IInstance instance, int index);
    void SendInstanceEntityMove(IInstance instance, MovementType movementType, int index);
    void SendDisplayIcon(ref DisplayIcon display, DisplayIconTarget target, IEntity entity, IInstance instance);
    void SendDisplayIcons(IList<DisplayIcon> display, DisplayIconTarget target, IEntity entity, IInstance instance);
    void SendDisplayIcons(IPlayer player, IInstance instance);
    void SendSkills(IPlayer player);
    void SendSkillUpdate(IPlayer player, CharacterSkill skill);
    void SendPassives(IPlayer player);
    void SendPassiveUpdate(IPlayer player, CharacterPassive passive);
    void SendPartyInvite(IPlayer player, IPlayer invited);
    void SendClosePartyInvitation(IPlayer player);
    void SendParty(Party party);
    void SendPartyData(Party party);
    void SendPartyVital(IPlayer player);
    void SendPartyDisplayIcons(IPlayer player);
    void SendPartyLeave(IPlayer player);
    void SendServerRates(IPlayer player);
    void SendPremiumService(IPlayer player, Premium premium, string enddate);
    void SendUpgradeData(IPlayer player, int inventoryIndex);
    void SendUpgradeData(IPlayer player, int inventoryIndex, UpgradeLevel data);
    void SendMailOperationResult(IPlayer player, MailingOperationCode code);
    void SendMails(IPlayer player);
    void SendDeleteMail(IPlayer player, int[] id);
    void SendMailUpdate(IPlayer player, int id, bool currencyReceiveFlag, bool ItemReceiveFlag);
    void SendMail(IPlayer player, CharacterMail mail);
    void SendBlackMarketItems(IPlayer player, BlackMarketItem[]? items, int maximumCategoryPages);
    void SendCash(IPlayer player);
    void SendTarget(IPlayer player, TargetType targetType, int index);
    void SendConversation(IPlayer player, int npcId);
    void SendConversationOption(IPlayer player, int id, int chatIndex);
    void SendConversationClose(IPlayer player);
    void SendWarehouseOpen(IPlayer player);
    void SendUpgradeOpen(IPlayer player);
    void SendCraftOpen(IPlayer player);
    void SendShopOpen(IPlayer player, Shop shop);
    void SendViewEquipment(IPlayer player, IPlayer source);
    void SendSettings(IPlayer player);
    void SendAnimation(IInstance instance, int animation, int x, int y, TargetType lockType, int lockIndex, bool isCasting);
    void SendCancelAnimation(IInstance instance, int lockIndex);
    void SendClearCast(IPlayer player);
    void SendSkillCooldown(IPlayer player, int index);
    void SendCloseChest(IPlayer player, IInstanceChest chest);
    void SendChest(IInstance instance, IInstanceChest chest);
    void SendChests(IPlayer player, IInstance instance);
    void SendChestItems(IPlayer player, IInstanceChest chest);
    void SendSortChestItemList(IPlayer player, int removedIndex);
    void SendUpdateChestItem(IPlayer player, IInstanceChestItem item);
    void SendUpdateChestState(IInstance instance, IInstanceChest chest);
    void SendEnableTakeItemFromChest(IPlayer player);
}
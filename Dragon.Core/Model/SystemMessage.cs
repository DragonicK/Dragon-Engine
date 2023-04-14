﻿namespace Dragon.Core.Model;

public enum SystemMessage {
    YouDoNotHaveRequiredLevel,
    SuccessToRevealItemAttribute,
    FailedToRevealItemAttribute,
    CannotRevealItemAttribute,
    InventoryFull,
    PlayerIsNotOnline,
    NeedRevealItemAttribute,
    WarehouseFull,
    InvalidProfession,
    RecipeIsRegistered,
    RecipeListIsFull,
    YouDoNotHaveEnoughMaterial,
    ItemCreated,
    TheTargetIsInAnotherTrade,
    YouAreWaitingForConfirmation,
    YouAreInTrade,
    PlayerIsNowDisconnected,
    TradeAcceptTimeOut,
    ItemCannotBeTraded,
    DeclinedTradeRequest,
    YouAcceptedTrade,
    YouConfirmedTrade,
    DeclinedTrade,
    YouCantChangeItemWhenConfirmed,
    ThePlayerInventoryIsFull,
    TheCurrencyCannotBeSubtracted,
    TheCurrencyCannotBeAdded,
    TradeConcluded,
    ThisBoxIsEmpty,
    YouAlreadyLearnedSkill,
    YouLearnedSkill,
    YouObtainedItem,
    OnlyClassCodeCanUseItem,
    InvalidTarget,
    InvalidRange,
    InsuficientMana,
    YouAreNotLeader,
    PlayerIsAlreadyInParty,
    PlayerIsAlreadyInvited,
    PlayerFailedToAcceptParty,
    YouFailedToAcceptParty,
    PartyIsFull,
    YouDeclinedPartyRequest,
    PlayerDeclinedPartyRequest,
    PartyDisbanded,
    YouJoinedParty,
    PartyCreated,
    YouLeftParty,
    PlayerLeftParty,
    PlayerPartyReconnected,
    PlayerKickedFromParty,
    YouKickedFromParty,
    ItemCannotBeUpgraded,
    UpgradeFailed,
    UpgradeFailedButReduced,
    UpgradeSuccess,
    UpgradeBreak,
    UpgradeMaximumLevel,
    YouJustReceivedMail,
    InsuficientCurrency,
    ItemCannotBeSold,
    ItemHasBeenSold,
    ViewEquipmentIsDisabled,
    ChestDoesNotBelongYou,
    ChestIsOpenedByAnotherPlayer,
    ReceivedCurrency,
    ReceivedItem
}
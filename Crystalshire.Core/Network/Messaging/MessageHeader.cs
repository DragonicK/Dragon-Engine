﻿namespace Crystalshire.Core.Network.Messaging {
    public enum MessageHeader {
        Ping = 1,
        AlertMessage,
        Authentication,
        AuthenticationResult,
        GameServerLogin,
        Characters,
        CharacterDelete,
        CharacterCreate,
        CharacterBegin,
        GettingMap,
        LoadMap,
        InGame,
        SetPlayerIndex,
        PlayerConfiguration,
        PlayerData,
        ClearPlayers,
        HighIndex,
        PlayerXY,
        PlayerHp,
        PlayerMp,
        PlayerAttributes,
        PlayerDirection,
        PlayerMovement,
        PlayerLeft,
        BroadcastMessage,
        MessageBubble,
        PlayerTitles,
        SelectedTitle,
        Experience,
        SuperiorCommand,
        UseAttributePoint,
        AttributePoint,
        Inventory,
        InventoryUpdate,
        SwapInventory,
        UseItem,
        DestroyItem,
        SystemMessage,
        PlayerModel,
        Equipment,
        EquipmentUpdate,
        UnequipItem,
        Heraldry,
        HeraldryUpdate,
        UnequipHeraldry,
        EquipHeraldry,
        Warehouse,
        WarehouseUpdate,
        SwapWarehouse,
        DepositItem,
        WithdrawItem,
        CraftData,
        CraftExperience,
        CraftClear,
        Recipes,
        AddRecipe,
        StartCraft,
        StopCraft,
        DeleteCraft,
        CompletedCraft,
        StartCraftProgress,
        ActionMessage,
        QuickSlot,
        QuickSlotUpdate,
        SwapQuickSlot,
        QuickSlotChange,
        QuickSlotUse,
        TradeRequest,
        TradeInvite,
        AcceptTradeRequest,
        CloseTrade,
        OpenTrade,
        TradeItem,
        UntradeItem,
        TradeMyInventory,
        TradeOtherInventory,
        DeclineTradeRequest,
        TradeState,
        TradeCurrency,
        AcceptTrade,
        ConfirmTrade,
        DeclineTrade,
        Currency,
        CurrencyUpdate,
        InstanceEntities,
        InstanceEntity,
        InstanceEntityDirection,
        InstanceEntityVital,
        InstanceEntityMove,
        DisplayIcons,
        DisplayIcon,
        Skill,
        SkillUpdate,
        Passive,
        PassiveUpdate,
        Cast,
        PartyRequest,
        PartyInvite,
        AcceptParty,
        DeclineParty,
        Party,
        PartyData,
        PartyVital,
        PartyLeave,
        PartyKick,
        ClosePartyInvitation,
        ServerRates,
        PremiumService,
        SelectedItemToUpgrade,
        UpgradeData,
        StartUpgrade,
        SendMail,
        MailOperationResult,
        Mailing,
        UpdateMailReadFlag,
        DeleteMail,
        ReceiveMailCurrency,
        ReceiveMailItem,
        UpdateMail,
        AddMail,
        RequestBlackMarketItems,
        BlackMarketItems,
        PurchaseBlackMarketItem,
        Cash,
        Target,
        Conversation,
        ConversationOption,
        ConversationClose,
        WarehouseOpen,
        WarehouseClose,
        UpgradeOpen,
        CraftOpen,
        ShopOpen,
        ShopClose,
        ShopBuyItem,
        ShopSellItem,
        RequestViewEquipment,
        ViewEquipment
    }
}
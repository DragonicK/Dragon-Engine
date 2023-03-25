Attribute VB_Name = "Network_Packet"
Option Explicit

Public Enum EnginePacket
    PCheckPing = 1
    PAlertMessage
    PAuthentication
    PAuthenticationResult
    PGameServerLogin
    PModels
    PCharacterDelete
    PCharacterCreate
    PCharacterBegin
    PGettingMap
    PLoadMap
    PInGame
    PSetPlayerIndex
    PPlayerConfiguration
    PPlayerData
    PClearPlayers
    PHighIndex
    PPlayerXY
    PPlayerHp
    PPlayerMp
    PPlayerStats
    PPlayerDirection
    PPlayerMovement
    PPlayerLeft
    PBroadcastMessage
    PMessageBubble
    PPlayerTitles
    PSelectedTitle
    PExperience
    PSuperiorCommand
    PUseAttributePoint
    PAttributePoint
    PInventory
    PInventoryUpdate
    PSwapInventory
    PUseItem
    PDestroyItem
    PSystemMessage
    PPlayerModel
    PEquipment
    PEquipmentUpdate
    PUnequipItem
    PHeraldry
    PHeraldryUpdate
    PUnequipHeraldry
    PEquipHeraldry
    PWarehouse
    PWarehouseUpdate
    PSwapWarehouse
    PDepositItem
    PWithdrawItem
    PCraftData
    PCraftExperience
    PCraftClear
    PRecipes
    PAddRecipe
    PStartCraft
    PStopCraft
    PDeleteCraft
    PCompletedCraft
    PStartCraftProgress
    PActionMessage
    PQuickSlot
    PQuickSlotUpdate
    PSwapQuickSlot
    PQuickSlotChange
    PQuickSlotUse
    PTradeRequest
    PTradeInvite
    PAcceptTradeRequest
    PCloseTrade
    POpenTrade
    PTradeItem
    PUntradeItem
    PTradeMyInventory
    PTradeOtherItems
    PDeclineTradeRequest
    PTradeState
    PTradeCurrency
    PAcceptTrade
    PConfirmTrade
    PDeclineTrade
    PCurrency
    PCurrencyUpdate
    PInstanceEntities
    PInstanceEntity
    PInstanceEntityDirection
    PInstanceEntityVital
    PInstanceEntityMove
    PDisplayIcons
    PDisplayIcon
    PSkill
    PSkillUpdate
    PPassive
    PPassiveUpdate
    PCast
    PClearCast
    PPartyRequest
    PPartyInvite
    PAcceptParty
    PDeclineParty
    PParty
    PPartyData
    PPartyVital
    PPartyLeave
    PPartyKick
    PClosePartyInvitation
    PServerRates
    PPremiumService
    PSelectedItemToUpgrade
    PUpgradeData
    PStartUpgrade
    PSendMail
    PMailOperationResult
    PMailing
    PUpdateMailReadFlag
    PDeleteMail
    PReiceveMailCurrency
    PReceiveMailItem
    PUpdateMail
    PAddMail
    PRequestBlackMarketItems
    PBlackMarketItems
    PPurchaseBlackMarketItem
    PCash
    PTarget
    PConversation
    PConversationOption
    PConversationClose
    PWarehouseOpen
    PWarehouseClose
    PUpgradeOpen
    PCraftOpen
    PShopOpen
    PShopClose
    PShopBuyItem
    PShopSellItem
    PRequestViewEquipment
    PViewEquipment
    PViewEquipmentVisibility
    PSettings
    PAnimation
    PCancelAnimation
    
    PPacketCount
End Enum


Attribute VB_Name = "Network_Packet"
Option Explicit

' Packets sent by client to server
Public Enum ClientPackets
    CRequestNewMap = 5000
           
    CRessurrectSelf
    CRessurrectByPlayer
    
    CAttack
    
    ' Make sure CMsgCOUNT is below everything else
    CMsgCOUNT
End Enum


' The order of the packets must match with the server's packet enumeration
' Packets sent by server to client
Public Enum ServerPackets
    SNpcAttack
    SNpcDead
    SSound
    
    SPlayerAchievement
    SUpdateAchievement
    
    SDeadPanelOperation
    SPlayerDead
    SRessurrection
    
    SAttack
    SRollDiceItem
    
    SStunned
    ' Make sure SMsgCOUNT is below everything else
    SMsgCOUNT
End Enum



Public Enum EnginePacket
    PCheckPing = 1
    PUpdateCipherKey
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
    PSkillCooldown
    PChests
    PCloseChest
    PUpdateChestState
    PChestItemList
    PTakeItemFromChest
    PSortChestItemList
    PUpdateChestItemList
    PEnableChestTakeItem
    PRollDiceResult

    PPacketCount
End Enum


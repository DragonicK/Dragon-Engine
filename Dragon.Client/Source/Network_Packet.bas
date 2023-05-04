Attribute VB_Name = "Network_Packet"
Option Explicit

Public Enum EnginePacket
    PCheckPing = 1
    PUpdateCipherKey
    PAlertMessage
    PAuthentication
    PAuthenticationResult
    PGameServerLogin
    PChatServerLogin
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


Public HandleDataSub(EnginePacket.PPacketCount) As Long

Public Function GetAddress(FunAddr As Long) As Long
    GetAddress = FunAddr
End Function

Public Sub InitMessages()
    HandleDataSub(EnginePacket.PCheckPing) = GetAddress(AddressOf HandleCheckPing)
    HandleDataSub(EnginePacket.PUpdateCipherKey) = GetAddress(AddressOf HandleUpdateCipherKey)
    HandleDataSub(EnginePacket.PAlertMessage) = GetAddress(AddressOf HandleAlertMsg)
    HandleDataSub(EnginePacket.PAuthenticationResult) = GetAddress(AddressOf HandleAuthenticationResult)
    HandleDataSub(EnginePacket.PModels) = GetAddress(AddressOf HandlePlayerModels)
    HandleDataSub(EnginePacket.PGettingMap) = GetAddress(AddressOf HandleGettingMap)
    HandleDataSub(EnginePacket.PLoadMap) = GetAddress(AddressOf HandleLoadMap)
    HandleDataSub(EnginePacket.PInGame) = GetAddress(AddressOf HandleInGame)
    HandleDataSub(EnginePacket.PSetPlayerIndex) = GetAddress(AddressOf HandleSetPlayerIndex)
    HandleDataSub(EnginePacket.PPlayerConfiguration) = GetAddress(AddressOf HandlePlayerConfiguration)
    HandleDataSub(EnginePacket.PPlayerData) = GetAddress(AddressOf HandlePlayerData)
    HandleDataSub(EnginePacket.PClearPlayers) = GetAddress(AddressOf HandleClearPlayers)
    HandleDataSub(EnginePacket.PHighIndex) = GetAddress(AddressOf HandleHighIndex)
    HandleDataSub(EnginePacket.PPlayerXY) = GetAddress(AddressOf HandlePlayerXY)
    HandleDataSub(EnginePacket.PPlayerHp) = GetAddress(AddressOf HandlePlayerHp)
    HandleDataSub(EnginePacket.PPlayerMp) = GetAddress(AddressOf HandlePlayerMp)
    HandleDataSub(EnginePacket.PPlayerStats) = GetAddress(AddressOf HandlePlayerStats)
    HandleDataSub(EnginePacket.PPlayerDirection) = GetAddress(AddressOf HandlePlayerDirection)
    HandleDataSub(EnginePacket.PPlayerMovement) = GetAddress(AddressOf HandlePlayerMovement)
    HandleDataSub(EnginePacket.PPlayerLeft) = GetAddress(AddressOf HandlePlayerLeft)
    HandleDataSub(EnginePacket.PBroadcastMessage) = GetAddress(AddressOf HandleBroadcastMessage)
    HandleDataSub(EnginePacket.PMessageBubble) = GetAddress(AddressOf HandleMessageBubble)
    HandleDataSub(EnginePacket.PPlayerTitles) = GetAddress(AddressOf HandlePlayerTitles)
    HandleDataSub(EnginePacket.PSelectedTitle) = GetAddress(AddressOf HandleTitle)
    HandleDataSub(EnginePacket.PExperience) = GetAddress(AddressOf HandleExperience)
    HandleDataSub(EnginePacket.PAttributePoint) = GetAddress(AddressOf HandleAttributePoint)
    HandleDataSub(EnginePacket.PInventory) = GetAddress(AddressOf HandleInventory)
    HandleDataSub(EnginePacket.PInventoryUpdate) = GetAddress(AddressOf HandleInventoryUpdate)
    HandleDataSub(EnginePacket.PSystemMessage) = GetAddress(AddressOf HandleSystemMessage)
    HandleDataSub(EnginePacket.PPlayerModel) = GetAddress(AddressOf HandlePlayerModel)
    HandleDataSub(EnginePacket.PEquipment) = GetAddress(AddressOf HandleEquipment)
    HandleDataSub(EnginePacket.PEquipmentUpdate) = GetAddress(AddressOf HandleEquipmentUpdate)
    HandleDataSub(EnginePacket.PHeraldry) = GetAddress(AddressOf HandleHeraldry)
    HandleDataSub(EnginePacket.PHeraldryUpdate) = GetAddress(AddressOf HandleHeraldryUpdate)
    HandleDataSub(EnginePacket.PWarehouse) = GetAddress(AddressOf HandleWarehouse)
    HandleDataSub(EnginePacket.PWarehouseUpdate) = GetAddress(AddressOf HandleWarehouseUpdate)
    HandleDataSub(EnginePacket.PCraftData) = GetAddress(AddressOf HandleCraftData)
    HandleDataSub(EnginePacket.PCraftExperience) = GetAddress(AddressOf HandleCraftExperience)
    HandleDataSub(EnginePacket.PCraftClear) = GetAddress(AddressOf HandleCraftClear)
    HandleDataSub(EnginePacket.PRecipes) = GetAddress(AddressOf HandleRecipes)
    HandleDataSub(EnginePacket.PAddRecipe) = GetAddress(AddressOf HandleAddRecipe)
    HandleDataSub(EnginePacket.PStartCraftProgress) = GetAddress(AddressOf HandleCraftStartProgressBar)
    HandleDataSub(EnginePacket.PActionMessage) = GetAddress(AddressOf HandleActionMessage)
    HandleDataSub(EnginePacket.PQuickSlot) = GetAddress(AddressOf HandleQuickSlot)
    HandleDataSub(EnginePacket.PQuickSlotUpdate) = GetAddress(AddressOf HandleQuickSlotUpdate)
    HandleDataSub(EnginePacket.PTradeInvite) = GetAddress(AddressOf HandleTradeInvite)
    HandleDataSub(EnginePacket.PCloseTrade) = GetAddress(AddressOf HandleCloseTrade)
    HandleDataSub(EnginePacket.POpenTrade) = GetAddress(AddressOf HandleOpenTrade)
    HandleDataSub(EnginePacket.PTradeMyInventory) = GetAddress(AddressOf HandleTradeMyInventory)
    HandleDataSub(EnginePacket.PTradeOtherItems) = GetAddress(AddressOf HandleTradeOtherItems)
    HandleDataSub(EnginePacket.PTradeState) = GetAddress(AddressOf HandleTradeState)
    HandleDataSub(EnginePacket.PTradeCurrency) = GetAddress(AddressOf HandleTradeCurrency)
    HandleDataSub(EnginePacket.PCurrency) = GetAddress(AddressOf HandleCurrency)
    HandleDataSub(EnginePacket.PCurrencyUpdate) = GetAddress(AddressOf HandleCurrencyUpdate)
    HandleDataSub(EnginePacket.PInstanceEntities) = GetAddress(AddressOf HandleInstanceEntities)
    HandleDataSub(EnginePacket.PInstanceEntity) = GetAddress(AddressOf HandleInstanceEntity)
    HandleDataSub(EnginePacket.PInstanceEntityDirection) = GetAddress(AddressOf HandleInstanceEntityDirection)
    HandleDataSub(EnginePacket.PInstanceEntityVital) = GetAddress(AddressOf HandleInstanceEntityVital)
    HandleDataSub(EnginePacket.PInstanceEntityMove) = GetAddress(AddressOf HandleInstanceEntityMove)
    HandleDataSub(EnginePacket.PDisplayIcons) = GetAddress(AddressOf HandleDisplayIcons)
    HandleDataSub(EnginePacket.PDisplayIcon) = GetAddress(AddressOf HandleDisplayIcon)
    HandleDataSub(EnginePacket.PSkill) = GetAddress(AddressOf HandleSkill)
    HandleDataSub(EnginePacket.PSkillUpdate) = GetAddress(AddressOf HandleSkillUpdate)
    HandleDataSub(EnginePacket.PPassive) = GetAddress(AddressOf HandlePassive)
    HandleDataSub(EnginePacket.PPassiveUpdate) = GetAddress(AddressOf HandlePassiveUpdate)
    HandleDataSub(EnginePacket.PPartyInvite) = GetAddress(AddressOf HandlePartyInvite)
    HandleDataSub(EnginePacket.PParty) = GetAddress(AddressOf HandleParty)
    HandleDataSub(EnginePacket.PPartyData) = GetAddress(AddressOf HandlePartyData)
    HandleDataSub(EnginePacket.PPartyVital) = GetAddress(AddressOf HandlePartyVital)
    HandleDataSub(EnginePacket.PPartyLeave) = GetAddress(AddressOf HandlePartyLeave)
    HandleDataSub(EnginePacket.PClosePartyInvitation) = GetAddress(AddressOf HandleClosePartyInviation)
    HandleDataSub(EnginePacket.PServerRates) = GetAddress(AddressOf HandleServerRates)
    HandleDataSub(EnginePacket.PPremiumService) = GetAddress(AddressOf HandlePremiumService)
    HandleDataSub(EnginePacket.PUpgradeData) = GetAddress(AddressOf HandleUpgradeData)
    HandleDataSub(EnginePacket.PMailOperationResult) = GetAddress(AddressOf HandleMailOperationResult)
    HandleDataSub(EnginePacket.PMailing) = GetAddress(AddressOf HandleMailing)
    HandleDataSub(EnginePacket.PDeleteMail) = GetAddress(AddressOf HandleDeletedMail)
    HandleDataSub(EnginePacket.PUpdateMail) = GetAddress(AddressOf HandleUpdateMail)
    HandleDataSub(EnginePacket.PAddMail) = GetAddress(AddressOf HandleAddMail)
    HandleDataSub(EnginePacket.PBlackMarketItems) = GetAddress(AddressOf HandleBlackMarketItems)
    HandleDataSub(EnginePacket.PCash) = GetAddress(AddressOf HandleCash)
    HandleDataSub(EnginePacket.PConversation) = GetAddress(AddressOf HandleConversation)
    HandleDataSub(EnginePacket.PConversationOption) = GetAddress(AddressOf HandleConversationOption)
    HandleDataSub(EnginePacket.PConversationClose) = GetAddress(AddressOf HandleConversationClose)
    HandleDataSub(EnginePacket.PWarehouseOpen) = GetAddress(AddressOf HandleWarehouseOpen)
    HandleDataSub(EnginePacket.PUpgradeOpen) = GetAddress(AddressOf HandleUpgradeOpen)
    HandleDataSub(EnginePacket.PCraftOpen) = GetAddress(AddressOf HandleCraftOpen)
    HandleDataSub(EnginePacket.PShopOpen) = GetAddress(AddressOf HandleShopOpen)
    HandleDataSub(EnginePacket.PViewEquipment) = GetAddress(AddressOf HandleViewEquipment)
    HandleDataSub(EnginePacket.PSettings) = GetAddress(AddressOf HandleSettings)
    HandleDataSub(EnginePacket.PAnimation) = GetAddress(AddressOf HandleAnimation)
    HandleDataSub(EnginePacket.PCancelAnimation) = GetAddress(AddressOf HandleCancelAnimation)
    HandleDataSub(EnginePacket.PClearCast) = GetAddress(AddressOf HandleClearCast)
    HandleDataSub(EnginePacket.PSkillCooldown) = GetAddress(AddressOf HandleSkillCooldown)
    HandleDataSub(EnginePacket.PTarget) = GetAddress(AddressOf HandleTarget)
    HandleDataSub(EnginePacket.PChests) = GetAddress(AddressOf HandleChests)
    HandleDataSub(EnginePacket.PCloseChest) = GetAddress(AddressOf HandleCloseChest)
    HandleDataSub(EnginePacket.PChestItemList) = GetAddress(AddressOf HandleChestItemList)
    HandleDataSub(EnginePacket.PSortChestItemList) = GetAddress(AddressOf HandleSortChestItemList)
    HandleDataSub(EnginePacket.PUpdateChestItemList) = GetAddress(AddressOf HandleUpdateChestItemList)
    HandleDataSub(EnginePacket.PUpdateChestState) = GetAddress(AddressOf HandleUpdateChestState)
    HandleDataSub(EnginePacket.PEnableChestTakeItem) = GetAddress(AddressOf HandleEnableChestTakeItem)

    '
    ' HandleDataSub(SRollDiceItem) = GetAddress(AddressOf HandleRollDiceItem)
    ' HandleDataSub(SNpcAttack) = GetAddress(AddressOf HandleNpcAttack)
    ' HandleDataSub(SSound) = GetAddress(AddressOf HandleSound)
    ' HandleDataSub(SPlayerAchievement) = GetAddress(AddressOf HandlePlayerAchievement)
    ' HandleDataSub(SUpdateAchievement) = GetAddress(AddressOf HandleUpdatePlayerAchievement)
    ' HandleDataSub(SDeadPanelOperation) = GetAddress(AddressOf HandleDeadPanelOperation)
    ' HandleDataSub(SPlayerDead) = GetAddress(AddressOf HandlePlayerDead)
    ' HandleDataSub(SRessurrection) = GetAddress(AddressOf HandleRessurection)
    ' HandleDataSub(SAttack) = GetAddress(AddressOf HandleAttack)
    ' HandleDataSub(SNpcDead) = GetAddress(AddressOf HandleNpcDead)

    ' HandleDataSub(SStunned) = GetAddress(AddressOf HandleStunned)
End Sub





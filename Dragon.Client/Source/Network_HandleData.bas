Attribute VB_Name = "Network_HandleData"
Option Explicit

Public HandleDataSub(EnginePacket.PPacketCount) As Long

Public Function GetAddress(FunAddr As Long) As Long
    GetAddress = FunAddr
End Function

Public Sub InitMessages()
    HandleDataSub(EnginePacket.PCheckPing) = GetAddress(AddressOf HandleCheckPing)
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
   
  
    
    ' HandleDataSub(SNpcAttack) = GetAddress(AddressOf HandleNpcAttack)
    ' HandleDataSub(STarget) = GetAddress(AddressOf HandleTarget)
    ' HandleDataSub(SSound) = GetAddress(AddressOf HandleSound)
    ' HandleDataSub(SPlayerAchievement) = GetAddress(AddressOf HandlePlayerAchievement)
    ' HandleDataSub(SUpdateAchievement) = GetAddress(AddressOf HandleUpdatePlayerAchievement)
    ' HandleDataSub(SDeadPanelOperation) = GetAddress(AddressOf HandleDeadPanelOperation)
    ' HandleDataSub(SPlayerDead) = GetAddress(AddressOf HandlePlayerDead)
    ' HandleDataSub(SRessurrection) = GetAddress(AddressOf HandleRessurection)
    ' HandleDataSub(SAttack) = GetAddress(AddressOf HandleAttack)
    ' HandleDataSub(SNpcDead) = GetAddress(AddressOf HandleNpcDead)
    ' HandleDataSub(SMapLoot) = GetAddress(AddressOf HandleMapLoot)
    ' HandleDataSub(SUpdateLootState) = GetAddress(AddressOf HandleUpdateLootState)
    ' HandleDataSub(SOpenLoot) = GetAddress(AddressOf HandleOpenLoot)
    ' HandleDataSub(SCloseLoot) = GetAddress(AddressOf HandleCloseLoot)
    ' HandleDataSub(SSortLootList) = GetAddress(AddressOf HandleSortLootList)
    ' HandleDataSub(SEnableDropTakeItem) = GetAddress(AddressOf HandleEnableDropTakeItem)
    ' HandleDataSub(SRollDiceItem) = GetAddress(AddressOf HandleRollDiceItem)

 
    ' HandleDataSub(SCooldown) = GetAddress(AddressOf HandleCooldown)
    ' HandleDataSub(SClearSpellBuffer) = GetAddress(AddressOf HandleClearSpellBuffer)
    ' HandleDataSub(SStunned) = GetAddress(AddressOf HandleStunned)
End Sub

Sub HandleData(ByRef Data() As Byte)
    Dim Buffer As clsBuffer
    Dim MsgType As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

   ' Dim Length As Long
  '  Dim Key As Byte
  '  Dim KeyIndex As Byte
  '  Dim Iv As Byte
  '  Dim IvIndex As Byte
  '  Dim DecryptedBytes() As Byte
  '  Dim Success As Boolean
  '  Dim ErrorDescription As String

   ' Length = Buffer.ReadLong
  '  Key = Buffer.ReadByte
  '  KeyIndex = Buffer.ReadByte
  ' ' Iv = Buffer.ReadByte
  '  IvIndex = Buffer.ReadByte

  '  DecryptedBytes = ConnectionAES.Decrypt(Buffer.ReadBytes(Length), CreateKey(KeyType_Key, KeyIndex, Key), CreateKey(KeyType_Iv, IvIndex, Iv), Success)

  '  If Not Success Then
  '      GoTo Error
  '  End If

   ' Buffer.Flush

    MsgType = Buffer.ReadLong

    If MsgType < 0 Then
        DestroyGame
        Exit Sub
    End If

    If MsgType >= EnginePacket.PPacketCount Then
        DestroyGame
        Exit Sub
    End If

    CallWindowProc HandleDataSub(MsgType), 1, Buffer.ReadBytes(Buffer.Length), 0, 0
    Exit Sub

Error:
    MsgBox "Não foi possível ler os dados."
    DestroyGame
End Sub

Public Sub HandleHighIndex(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    Player_HighIndex = Buffer.ReadLong

    Set Buffer = Nothing
End Sub

Public Sub HandleSetPlayerIndex(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    MyIndex = Buffer.ReadLong

    Set Buffer = Nothing

End Sub

Public Sub HandleInGame(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    HideWindows

    InGame = True
    inMenu = False

    CanMoveNow = True
    GettingMap = False

    ' show gui
    ShowWindow GetWindowIndex("winBars"), , False
    ' ShowWindow GetWindowIndex("winMenu"), , False
    ShowWindow GetWindowIndex("winHotbar"), , False
    ShowWindow GetWindowIndex("winChatSmall"), , False

    If Party.Leader > 0 Then
        ShowWindow GetWindowIndex("winParty"), , False
    End If

    ' enter loop
    GameLoop
End Sub


Private Sub HandleAttack(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    i = Buffer.ReadLong

    ' Set player to attacking
    Player(i).Attacking = 1
    Player(i).AttackTimer = GetTickCount
    Player(i).AttackFrameIndex = 1
End Sub

Private Sub HandleNpcAttack(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    i = Buffer.ReadLong

    ' Set player to attacking
    MapNpc(i).Attacking = 1
    MapNpc(i).AttackTimer = GetTickCount
    MapNpc(i).AttackFrameIndex = 1
    MapNpc(i).FrameTick = GetTickCount
End Sub

Private Sub HandlePlayerLeft(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, pIndex As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    pIndex = Buffer.ReadLong

    Set Buffer = Nothing

    Call ClearPlayer(pIndex)
End Sub

Private Sub HandleExitGame(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, pIndex As Long
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    pIndex = Buffer.ReadLong

    Call ClearPlayer(pIndex)

    Set Buffer = Nothing
End Sub

Private Sub HandleSendPing(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    PingEnd = GetTickCount
    Ping = PingEnd - PingStart
End Sub


Private Sub HandleCooldown(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim Slot As Long
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    Slot = Buffer.ReadLong
    SpellCd(Slot) = GetTickCount
    Set Buffer = Nothing
End Sub

Private Sub HandleClearSpellBuffer(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    SpellBuffer = 0
    SpellBufferTimer = 0
End Sub


Private Sub HandleStunned(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    StunDuration = Buffer.ReadLong
    Set Buffer = Nothing
End Sub

Private Sub HandleTarget(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    MyTargetIndex = Buffer.ReadLong
    MyTargetType = Buffer.ReadByte
    Set Buffer = Nothing

    If MyTargetIndex <= 0 Then
        Call CloseTargetWindow
    Else
        Call OpenTargetWindow
    End If
End Sub




Private Sub HandleSound(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim X As Long, Y As Long, entityType As Long, entityNum As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    X = Buffer.ReadInteger
    Y = Buffer.ReadInteger
    entityType = Buffer.ReadByte
    entityNum = Buffer.ReadLong

    Set Buffer = Nothing

    PlayMapSound X, Y, entityType, entityNum
End Sub


Private Sub HandleChatUpdate(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, NpcNum As Long
    Dim ConversationNum As Long, CurrentChat As Long
    'mT As String, o(1 To 4) As String, i As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    NpcNum = Buffer.ReadLong
    ConversationNum = Buffer.ReadLong
    CurrentChat = Buffer.ReadLong

    Set Buffer = Nothing

    ' if npcNum is 0, exit the chat system
    If NpcNum = 0 Then
        inChat = False
        HideWindow GetWindowIndex("winNpcChat")
        Exit Sub
    End If

    If ConversationNum > 0 And CurrentChat > 0 Then
       ' OpenNpcChat NpcNum, Conv(ConversationNum).Conv(CurrentChat).Conv, Conv(ConversationNum).Conv(CurrentChat).rText
    End If
End Sub


Private Sub HandleCheckPing(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim ClientRequest As Byte
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data
    ClientRequest = Buffer.ReadByte

    If ClientRequest = 1 Then
        PingEnd = GetTickCount
        Ping = PingEnd - PingStart
    End If
End Sub







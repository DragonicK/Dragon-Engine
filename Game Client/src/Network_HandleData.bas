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
    
        

    ' HandleDataSub(SNpcAttack) = GetAddress(AddressOf HandleNpcAttack)
    ' HandleDataSub(STarget) = GetAddress(AddressOf HandleTarget)
    ' HandleDataSub(SSound) = GetAddress(AddressOf HandleSound)
    ' HandleDataSub(SAnimation) = GetAddress(AddressOf HandleAnimation)
    ' HandleDataSub(SCancelAnimation) = GetAddress(AddressOf HandleCancelAnimation)
    '
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


    ' HandleDataSub(SMapKey) = GetAddress(AddressOf HandleMapKey)
    ' HandleDataSub(SDoorAnimation) = GetAddress(AddressOf HandleDoorAnimation)
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

Sub HandleAlertMsg(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, dialogue_index As Long, MenuReset As Long, Kick As Long, Forced As Boolean

    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data()
    
    dialogue_index = Buffer.ReadLong
    MenuReset = Buffer.ReadLong
    Kick = Buffer.ReadByte
    Forced = CBool(Buffer.ReadByte)

    If Forced Then
        HideWindows
        SetStatus vbNullString
    End If

    Set Buffer = Nothing

    If MenuReset > 0 Then
        HideWindows
        Select Case MenuReset
        Case MenuCount.menuLogin
            ShowWindow GetWindowIndex("winLogin")
        Case MenuCount.menuChars
            ShowWindow GetWindowIndex("winModels")
        Case MenuCount.menuClass
            ShowWindow GetWindowIndex("winClasses")
        Case MenuCount.menuNewChar
            ShowWindow GetWindowIndex("winNewChar")
        Case MenuCount.menuMain
            ShowWindow GetWindowIndex("winLogin")
        End Select
    Else
        If Kick > 0 Or inMenu = True Then
            ShowWindow GetWindowIndex("winLogin")
            DialogueAlert dialogue_index
            LogoutGame
            Exit Sub
        End If
    End If

    DialogueAlert dialogue_index
End Sub

Sub HandleSetPlayerIndex(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    MyIndex = Buffer.ReadLong

    Set Buffer = Nothing

End Sub

Sub HandleInGame(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
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

Private Sub HandlePlayerHp(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, pIndex As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    pIndex = Buffer.ReadLong

    Call SetPlayerMaxVital(pIndex, Vitals.HP, Buffer.ReadLong)
    Call SetPlayerVital(pIndex, Vitals.HP, Buffer.ReadLong)

    If pIndex = MyIndex Then
        ' set max width
        If GetPlayerVital(MyIndex, Vitals.HP) > 0 Then
            BarWidth_GuiHP_Max = ((GetPlayerVital(MyIndex, Vitals.HP) / 209) / (GetPlayerMaxVital(MyIndex, Vitals.HP) / 209)) * 209
        Else
            BarWidth_GuiHP_Max = 0
        End If

        ' Update GUI
        UpdateStats_UI
    End If

End Sub

Private Sub HandlePlayerMp(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, pIndex As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    pIndex = Buffer.ReadLong

    Call SetPlayerMaxVital(pIndex, Vitals.MP, Buffer.ReadLong)
    Call SetPlayerVital(pIndex, Vitals.MP, Buffer.ReadLong)

    If pIndex = MyIndex Then
        ' set max width
        If GetPlayerVital(MyIndex, Vitals.MP) > 0 Then
            BarWidth_GuiSP_Max = ((GetPlayerVital(MyIndex, Vitals.MP) / 209) / (GetPlayerMaxVital(MyIndex, Vitals.MP) / 209)) * 209
        Else
            BarWidth_GuiSP_Max = 0
        End If
        ' Update GUI
        UpdateStats_UI
    End If

End Sub


Private Sub HandlePlayerData(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long, X As Long, Dead As Byte
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    i = Buffer.ReadLong

    Call SetPlayerName(i, Buffer.ReadString)
    Call SetPlayerAccess(i, Buffer.ReadLong)
    Call SetPlayerClass(i, Buffer.ReadLong)
    Call SetPlayerSprite(i, Buffer.ReadLong)
    Call SetPlayerLevel(i, Buffer.ReadLong)
    Call SetPlayerMap(i, Buffer.ReadLong)
    Call SetPlayerX(i, Buffer.ReadInteger)
    Call SetPlayerY(i, Buffer.ReadInteger)
    Call SetPlayerDirection(i, Buffer.ReadLong)
    Call SetPlayerTitle(i, Buffer.ReadLong)
    Call SetPlayerDead(i, Buffer.ReadByte)

    ' Check if the player is the client player
    If i = MyIndex Then
        ' Reset directions
        WDown = False
        ADown = False
        SDown = False
        DDown = False

        DirUpLeft = False
        DirUpRight = False
        DirDownLeft = False
        DirDownRight = False

        With Windows(GetWindowIndex("winCharacter"))
            .Controls(GetControlIndex("winCharacter", "lblName")).Text = UCase$(GetPlayerName(MyIndex)) & " LV. " & GetPlayerLevel(MyIndex)
            .Controls(GetControlIndex("winCharacter", "lblClass")).Text = UCase$(Class(GetPlayerClass(MyIndex)).Name)
        End With

        Call UpdateActiveTitle(GetPlayerTitle(MyIndex))
    End If

    ' Define a sprite do jogador.
    Call SetPlayerModelIndex(i)

    ' Make sure they aren't walking
    Player(i).Moving = 0
    Player(i).xOffset = 0
    Player(i).yOffset = 0
End Sub

Private Sub HandlePlayerMovement(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim X As Long
    Dim Y As Long
    Dim Dir As Long
    Dim State As Long

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Index = Buffer.ReadLong
    Dir = Buffer.ReadLong
    State = Buffer.ReadLong
    X = Buffer.ReadInteger
    Y = Buffer.ReadInteger
    
    Call SetPlayerX(Index, X)
    Call SetPlayerY(Index, Y)
    Call SetPlayerDirection(Index, Dir)

    Player(Index).xOffset = 0
    Player(Index).yOffset = 0
    Player(Index).Moving = State

    Select Case GetPlayerDir(Index)

    Case DIR_UP
        Player(Index).yOffset = PIC_Y

    Case DIR_DOWN
        Player(Index).yOffset = PIC_Y * -1

    Case DIR_LEFT
        Player(Index).xOffset = PIC_X

    Case DIR_RIGHT
        Player(Index).xOffset = PIC_X * -1
        
    Case DIR_UP_LEFT
        Player(Index).yOffset = PIC_Y
        Player(Index).xOffset = PIC_X

    Case DIR_UP_RIGHT
        Player(Index).yOffset = PIC_Y
        Player(Index).xOffset = PIC_X * -1

    Case DIR_DOWN_LEFT
        Player(Index).yOffset = PIC_Y * -1
        Player(Index).xOffset = PIC_X

    Case DIR_DOWN_RIGHT
        Player(Index).yOffset = PIC_Y * -1
        Player(Index).xOffset = PIC_X * -1

    End Select
End Sub

Private Sub HandlePlayerDirection(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long
    Dim Dir As Byte
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    i = Buffer.ReadLong
    Dir = Buffer.ReadLong

    Set Buffer = Nothing

    Call SetPlayerDirection(i, Dir)

    With Player(i)
        .xOffset = 0
        .yOffset = 0
        .Moving = 0
    End With

End Sub

Private Sub HandlePlayerXY(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim X As Long
    Dim Y As Long
    Dim Dir As Long
    Dim Buffer As clsBuffer
    Dim pIndex As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    pIndex = Buffer.ReadLong

    X = Buffer.ReadLong
    Y = Buffer.ReadLong
    Dir = Buffer.ReadLong

    Set Buffer = Nothing

    Call SetPlayerX(pIndex, X)
    Call SetPlayerY(pIndex, Y)
    Call SetPlayerDirection(pIndex, Dir)

    ' Make sure they aren't walking
    Player(pIndex).Moving = 0
    Player(pIndex).xOffset = 0
    Player(pIndex).yOffset = 0
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

Private Sub HandleGettingMap(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    GettingMap = Buffer.ReadByte
    
    Set Buffer = Nothing
End Sub

Private Sub HandleMapDone()
    Dim i As Long
    Dim MusicFile As String
    
    InWarehouse = False
    InTrade = False
    
    CloseShop

    ' clear the action msgs
    For i = 1 To MAX_BYTE
        ClearActionMsg (i)
    Next i

    Action_HighIndex = 1

    ' player music
    ' If InGame Then
    MusicFile = Trim$(CurrentMap.MapData.Music)

    If Not MusicFile = "None." Then
        Play_Music MusicFile
    Else
        Stop_Music
    End If

    ' now cache the positions
    GettingMap = False
    CanMoveNow = True
End Sub

Private Sub HandleMapKey(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim n As Long
    Dim X As Long
    Dim Y As Long
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    X = Buffer.ReadLong
    Y = Buffer.ReadLong
    n = Buffer.ReadByte
    TempTile(X, Y).DoorOpen = n

    ' re-cache rendering
    ' If Not GettingMap Then cacheRenderState X, Y, MapLayer.Mask
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

Private Sub HandleDoorAnimation(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim X As Long, Y As Long
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    X = Buffer.ReadLong
    Y = Buffer.ReadLong

    With TempTile(X, Y)
        .DoorFrame = 1
        .DoorAnimate = 1    ' 0 = nothing| 1 = opening | 2 = closing
        .DoorTimer = GetTickCount
    End With

    Set Buffer = Nothing
End Sub


Private Sub HandleAnimation(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, X As Long, Y As Long
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    AnimationIndex = AnimationIndex + 1

    If AnimationIndex >= MAX_BYTE Then AnimationIndex = 1

    With AnimInstance(AnimationIndex)
        .Animation = Buffer.ReadLong
        .X = Buffer.ReadInteger
        .Y = Buffer.ReadInteger
        .LockType = Buffer.ReadByte
        .lockindex = Buffer.ReadLong
        .isCasting = Buffer.ReadByte
        .Used(0) = True
        .Used(1) = True
    End With

    Set Buffer = Nothing

    ' play the sound if we've got one
    With AnimInstance(AnimationIndex)

        If .LockType = 0 Then
            X = AnimInstance(AnimationIndex).X
            Y = AnimInstance(AnimationIndex).Y
        ElseIf .LockType = TargetTypePlayer Then
            X = GetPlayerX(.lockindex)
            Y = GetPlayerY(.lockindex)
        ElseIf .LockType = TargetTypeNpc Then
            X = MapNpc(.lockindex).X
            Y = MapNpc(.lockindex).Y
        End If

    End With

    PlayMapSound X, Y, SoundEntity.SeAnimation, AnimInstance(AnimationIndex).Animation
End Sub

Private Sub HandleMapNpcVitals(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim i As Long
    Dim MapNpcNum As Long

    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data()
    MapNpcNum = Buffer.ReadLong

    With MapNpc(MapNpcNum)
        For i = 1 To Vitals.Vital_Count - 1
            .Vital(i) = Buffer.ReadLong
            .MaxVital(i) = Buffer.ReadLong

            If .Vital(i) <= 0 Then
                .Vital(i) = 0
            End If
        Next
    End With

    Set Buffer = Nothing
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
    MyTarget = Buffer.ReadLong
    MyTargetType = Buffer.ReadByte
    Set Buffer = Nothing

    If MyTarget <= 0 Then
        Call CloseTargetWindow
    Else
        Call OpenTargetWindow
    End If
End Sub


Private Sub HandleHighIndex(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    Player_HighIndex = Buffer.ReadLong

    Set Buffer = Nothing
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

Private Sub HandleTradeRequest(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, TheName As String, Top As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    TheName = Buffer.ReadString

    Set Buffer = Nothing

    ' cache name and show invitation
    diaDataString = TheName
    ShowWindow GetWindowIndex("winInvite_Trade")
    Windows(GetWindowIndex("winInvite_Trade")).Controls(GetControlIndex("winInvite_Trade", "btnInvite")).Text = ColourChar & White & TheName & ColourChar & "-1" & " convidou voce para uma negociacao."
    AddText Trim$(TheName) & " has invited you to trade.", White
    ' loop through
    Top = ScreenHeight - 80

    If Windows(GetWindowIndex("winInvite_Party")).Window.Visible Then
        Top = Top - 37
    End If

    Windows(GetWindowIndex("winInvite_Trade")).Window.Top = Top
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


Private Sub HandleCancelAnimation(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim theIndex As Long, Buffer As clsBuffer, i As Long
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    theIndex = Buffer.ReadLong
    Set Buffer = Nothing
    ' find the casting animation
    For i = 1 To MAX_BYTE
        If AnimInstance(i).LockType = TargetTypePlayer Then
            If AnimInstance(i).lockindex = theIndex Then
                If AnimInstance(i).isCasting = 1 Then
                    ' clear it
                    ClearAnimInstance i
                End If
            End If
        End If
    Next
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

Private Sub HandleLoadMap(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    GettingMap = True

    Dim Buffer As clsBuffer, MapNum As Long
    Dim Key() As Byte, IV() As Byte
    Dim Length As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    MapNum = Buffer.ReadLong

    Length = Buffer.ReadLong
    Key = Buffer.ReadBytes(Length)

    Length = Buffer.ReadLong
    IV = Buffer.ReadBytes(Length)

    Set Buffer = Nothing

    Call ClearMapNpcs
    Call ClearMap
    Call ClearCorpses

    Call CopyMapProperty(MapNum)
    Call LoadMapParallax(MapNum, Key, IV)

    Call HandleMapDone

    ClearTempTile
    UpdateMapView
End Sub

Private Sub HandleClearPlayers(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long

    For i = 1 To MaxPlayers
        Call ClearPlayer(i)
    Next
End Sub




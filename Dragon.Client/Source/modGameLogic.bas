Attribute VB_Name = "modGameLogic"
Option Explicit

Function IsTryingToMove() As Boolean

'If DirUp Or DirDown Or DirLeft Or DirRight Then
    If WDown Or SDown Or ADown Or DDown Then
        IsTryingToMove = True
    End If

End Function

Function CanMove() As Boolean
    Dim D As Long
    CanMove = True

    If Player(MyIndex).Dead Then
        CanMove = False
        Exit Function
    End If

    ' Make sure they aren't trying to move when they are already moving
    If Player(MyIndex).Moving <> 0 Then
        CanMove = False
        Exit Function
    End If

    ' Make sure they haven't just casted a spell
    'If SpellBuffer > 0 Then
    '    CanMove = False
    '    Exit Function
    'End If

    ' Verifica se o jogador pode-se mover durante um movimento.
    If Not CanPlayerMoveInMovement(MyIndex) Then
        CanMove = False
        Exit Function
    End If

    ' make sure they're not stunned
    If StunDuration > 0 Then
        CanMove = False
        Exit Function
    End If

    ' make sure they're not in a shop
    If InShop Then
        CanMove = False
        Exit Function
    End If

    If InTrade Then
        CanMove = False
        Exit Function
    End If

    If InWarehouse Then
        CanMove = False
        Exit Function
    End If

    If inTutorial Then
        CanMove = False
        Exit Function
    End If

    D = GetPlayerDir(MyIndex)


    If WDown Then
        Call SetPlayerDirection(MyIndex, DIR_UP)

        ' Check to see if they are trying to go out of bounds
        If GetPlayerY(MyIndex) > 0 Then
            If CheckDirection(DIR_UP) Then
                CanMove = False

                ' Set the new direction if they weren't facing that direction
                If D <> DIR_UP Then
                    Call SendPlayerDirection
                End If

                Exit Function
            End If
        Else
            ' Check if they can warp to a new map
            If CurrentMap.MapData.Up > 0 Then
                Call SendPlayerRequestNewMap
                ' GettingMap = True
                CanMoveNow = False
            End If

            CanMove = False
            Exit Function
        End If
    End If

    If SDown Then
        Call SetPlayerDirection(MyIndex, DIR_DOWN)

        ' Check to see if they are trying to go out of bounds
        If GetPlayerY(MyIndex) < CurrentMap.MapData.MaxY Then
            If CheckDirection(DIR_DOWN) Then
                CanMove = False

                ' Set the new direction if they weren't facing that direction
                If D <> DIR_DOWN Then
                    Call SendPlayerDirection
                End If

                Exit Function
            End If

        Else

            ' Check if they can warp to a new map
            If CurrentMap.MapData.Down > 0 Then
                Call SendPlayerRequestNewMap
                ' GettingMap = True
                CanMoveNow = False
            End If

            CanMove = False
            Exit Function
        End If
    End If

    If ADown Then
        Call SetPlayerDirection(MyIndex, DIR_LEFT)

        ' Check to see if they are trying to go out of bounds
        If GetPlayerX(MyIndex) > 0 Then
            If CheckDirection(DIR_LEFT) Then
                CanMove = False

                ' Set the new direction if they weren't facing that direction
                If D <> DIR_LEFT Then
                    Call SendPlayerDirection
                End If

                Exit Function
            End If

        Else

            ' Check if they can warp to a new map
            If CurrentMap.MapData.Left > 0 Then
                Call SendPlayerRequestNewMap
                ' GettingMap = True
                CanMoveNow = False
            End If

            CanMove = False
            Exit Function
        End If
    End If

    If DDown Then
        Call SetPlayerDirection(MyIndex, DIR_RIGHT)

        ' Check to see if they are trying to go out of bounds
        If GetPlayerX(MyIndex) < CurrentMap.MapData.MaxX Then
            If CheckDirection(DIR_RIGHT) Then
                CanMove = False

                ' Set the new direction if they weren't facing that direction
                If D <> DIR_RIGHT Then
                    Call SendPlayerDirection
                End If

                Exit Function
            End If
        Else
            ' Check if they can warp to a new map
            If CurrentMap.MapData.Right > 0 Then
                Call SendPlayerRequestNewMap
                ' GettingMap = True
                CanMoveNow = False
            End If

            CanMove = False
            Exit Function
        End If
    End If

End Function

Function CheckDirection(ByVal Direction As Byte) As Boolean
    Dim X As Long, Y As Long, i As Long, EventCount As Long, Page As Long

    CheckDirection = False

    If GettingMap Then Exit Function

    X = GetPlayerX(MyIndex)
    Y = GetPlayerY(MyIndex)

    Select Case Direction
    Case DIR_UP
        X = GetPlayerX(MyIndex)
        Y = GetPlayerY(MyIndex) - 1

    Case DIR_DOWN
        X = GetPlayerX(MyIndex)
        Y = GetPlayerY(MyIndex) + 1

    Case DIR_LEFT
        X = GetPlayerX(MyIndex) - 1
        Y = GetPlayerY(MyIndex)

    Case DIR_RIGHT
        X = GetPlayerX(MyIndex) + 1
        Y = GetPlayerY(MyIndex)

    End Select

    ' check directional blocking
    If IsDirBlocked(CurrentMap.TileData.Tile(GetPlayerX(MyIndex), GetPlayerY(MyIndex)).DirBlock, Direction) Then
        CheckDirection = True
        Exit Function
    End If

    ' Check to see if the map tile is blocked or not
    If CurrentMap.TileData.Tile(X, Y).Type = TILE_TYPE_BLOCKED Then
        CheckDirection = True
        Exit Function
    End If

    ' Check to see if the map tile is tree or not
    If CurrentMap.TileData.Tile(X, Y).Type = TILE_TYPE_RESOURCE Then
        CheckDirection = True
        Exit Function
    End If

    ' Check to see if a player is already on that tile
    If CurrentMap.MapData.Moral = 0 Then
        For i = 1 To Player_HighIndex
            If IsPlaying(i) And GetPlayerMap(i) = GetPlayerMap(MyIndex) Then
                If GetPlayerX(i) = X Then
                    If GetPlayerY(i) = Y Then
                        CheckDirection = True
                        Exit Function
                    End If
                End If
            End If
        Next i
    End If

    ' Check to see if a npc is already on that tile
    For i = 1 To Npc_HighIndex
        If MapNpc(i).Num > 0 Then
            If Not GetNpcDead(i) Then
                If MapNpc(i).X = X Then
                    If MapNpc(i).Y = Y Then
                        CheckDirection = True
                        Exit Function
                    End If
                End If
            End If
        End If
    Next

    ' check if it's a drop warp - avoid if walking
    If ShiftDown Then
        If CurrentMap.TileData.Tile(X, Y).Type = TILE_TYPE_WARP Then
            If CurrentMap.TileData.Tile(X, Y).Data4 Then
                CheckDirection = True
            End If
        End If
    End If

End Function

Sub CheckMovement()
    If Not GettingMap Then
        If IsTryingToMove Then
            If CanMove Then

                Call CheckForCloseLoot

                ' Check if player has the shift key down for running
                If ShiftDown Then
                    Player(MyIndex).Moving = MOVING_RUNNING
                Else
                    Player(MyIndex).Moving = MOVING_WALKING
                End If

                Select Case GetPlayerDir(MyIndex)

                Case DIR_UP
                    Call SendPlayerMovement
                    Player(MyIndex).yOffset = PIC_Y
                    Call SetPlayerY(MyIndex, GetPlayerY(MyIndex) - 1)

                Case DIR_DOWN
                    Call SendPlayerMovement
                    Player(MyIndex).yOffset = PIC_Y * -1
                    Call SetPlayerY(MyIndex, GetPlayerY(MyIndex) + 1)

                Case DIR_LEFT
                    Call SendPlayerMovement
                    Player(MyIndex).xOffset = PIC_X
                    Call SetPlayerX(MyIndex, GetPlayerX(MyIndex) - 1)

                Case DIR_RIGHT
                    Call SendPlayerMovement
                    Player(MyIndex).xOffset = PIC_X * -1
                    Call SetPlayerX(MyIndex, GetPlayerX(MyIndex) + 1)


                End Select

                If CurrentMap.TileData.Tile(GetPlayerX(MyIndex), GetPlayerY(MyIndex)).Type = TILE_TYPE_WARP Then
                    GettingMap = True
                End If
            End If
        End If
    End If

End Sub

Public Function IsInBounds()

    If (CurX >= 0) Then
        If (CurX <= CurrentMap.MapData.MaxX) Then
            If (CurY >= 0) Then
                If (CurY <= CurrentMap.MapData.MaxY) Then
                    IsInBounds = True
                End If
            End If
        End If
    End If

End Function

Public Function IsValidMapPoint(ByVal X As Long, ByVal Y As Long) As Boolean
    IsValidMapPoint = False

    If X < 0 Then Exit Function
    If Y < 0 Then Exit Function
    If X > CurrentMap.MapData.MaxX Then Exit Function
    If Y > CurrentMap.MapData.MaxY Then Exit Function
    IsValidMapPoint = True
End Function


Public Function IsHotbar(StartX As Long, StartY As Long) As Long
    Dim TempRec As RECT
    Dim i As Long

    For i = 1 To MaximumQuickSlot
        If QuickSlot(i).Slot Then
            With TempRec
                .Top = StartY + HotbarTop
                .Bottom = .Top + PIC_Y
                .Left = StartX + HotbarLeft + ((i - 1) * HotbarOffsetX)
                .Right = .Left + PIC_X
            End With

            If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
                If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
                    IsHotbar = i
                    Exit Function
                End If
            End If
        End If
    Next
End Function

Public Sub UseItem()
' Check for subscript out of range
    If InventoryItemSelected < 1 Or InventoryItemSelected > MaxInventoryPerTab Then
        Exit Sub
    End If

    Call SendUseItem(InventoryItemSelected)
End Sub


Public Sub CastSpell(ByVal SpellSlot As Long)
' Check for subscript out of range
    If SpellSlot < 1 Or SpellSlot > MaxPlayerSkill Then
        Exit Sub
    End If

    If SpellCd(SpellSlot) > 0 Then
        AddText "A habilidade ainda esta em resfriamento!", BrightRed
        Exit Sub
    End If

    ' make sure we're not casting same spell
    If SpellBuffer > 0 Then
        If SpellBuffer = SpellSlot Then
            ' stop them
            Exit Sub
        End If
    End If

    If PlayerSkill(SpellSlot).Id = 0 Then Exit Sub

    Dim CostText As String
    Dim VitalType As Long
    Dim SkillId As Long

    SkillId = PlayerSkill(SpellSlot).Id

    If Skill(SkillId).CostType = SkillCostType.SkillCostType_HP Then
        VitalType = Vitals.HP

    ElseIf Skill(SkillId).CostType = SkillCostType.SkillCostType_MP Then
        VitalType = Vitals.MP
    End If

    If VitalType > 0 Then
        ' Check if player has enough MP
        If GetPlayerVital(MyIndex, VitalType) < PlayerSkill(SpellSlot).Cost Then
            Call AddText("Não tem MP suficiente para conjurar " & Skill(PlayerSkill(SpellSlot).Id).Name & ".", BrightRed)
            Exit Sub
        End If
    End If

    If SkillId > 0 Then
        If GetTickCount > Player(MyIndex).AttackTimer + 1000 Then
            If Player(MyIndex).Moving = 0 Then
                Dim Buffer As clsBuffer
                Set Buffer = New clsBuffer

                Buffer.WriteLong EnginePacket.PCast
                Buffer.WriteLong SpellSlot

                SendData Buffer.ToArray()
                Set Buffer = Nothing

                SpellBuffer = SpellSlot
                SpellBufferTimer = GetTickCount
            Else
                Call AddText("Não pode conjurar enquanto anda!", BrightRed)
            End If
        End If
    End If

End Sub

Public Sub DevMsg(ByVal Text As String, ByVal Color As Byte)

    If InGame Then
        If GetPlayerAccess(MyIndex) > ACCESS_ADMINISTRATOR Then
            Call AddText(Text, Color)
        End If
    End If

    Debug.Print Text
End Sub

Public Function TwipsToPixels(ByVal twip_val As Long, ByVal XorY As Byte) As Long

    If XorY = 0 Then
        TwipsToPixels = twip_val / Screen.TwipsPerPixelX
    ElseIf XorY = 1 Then
        TwipsToPixels = twip_val / Screen.TwipsPerPixelY
    End If

End Function

Public Function PixelsToTwips(ByVal pixel_val As Long, ByVal XorY As Byte) As Long

    If XorY = 0 Then
        PixelsToTwips = pixel_val * Screen.TwipsPerPixelX
    ElseIf XorY = 1 Then
        PixelsToTwips = pixel_val * Screen.TwipsPerPixelY
    End If

End Function

Public Function ConvertCurrency(ByVal Amount As Long) As String

    If Int(Amount) < 10000 Then
        ConvertCurrency = Amount
    ElseIf Int(Amount) < 999999 Then
        ConvertCurrency = Int(Amount / 1000) & "k"
    ElseIf Int(Amount) < 999999999 Then
        ConvertCurrency = Int(Amount / 1000000) & "m"
    Else
        ConvertCurrency = Int(Amount / 1000000000) & "b"
    End If

End Function


Public Sub CreateActionMsg(ByVal Message As String, ByVal Color As Integer, ByVal MsgType As Byte, ByVal FontType As Byte, ByVal X As Long, ByVal Y As Long)
    Dim i As Long
    ActionMsgIndex = ActionMsgIndex + 1

    If ActionMsgIndex >= MAX_BYTE Then ActionMsgIndex = 1

    With ActionMsg(ActionMsgIndex)
        .Message = Message
        .Color = Color
        .Type = MsgType
        .Created = GetTickCount
        .Scroll = 1
        .X = X
        .Y = Y
        .FontType = FontType
        .Alpha = 255
    End With

    If ActionMsg(ActionMsgIndex).Type = ACTIONMsgSCROLL Then
        ActionMsg(ActionMsgIndex).Y = ActionMsg(ActionMsgIndex).Y + Rand(-2, 6)
        ActionMsg(ActionMsgIndex).X = ActionMsg(ActionMsgIndex).X + Rand(-8, 8)
    End If

    ' find the new high index
    For i = MAX_BYTE To 1 Step -1

        If ActionMsg(i).Created > 0 Then
            Action_HighIndex = i + 1
            Exit For
        End If

    Next

    ' make sure we don't overflow
    If Action_HighIndex > MAX_BYTE Then Action_HighIndex = MAX_BYTE
End Sub

Public Sub ClearActionMsg(ByVal Index As Byte)
    Dim i As Long
    ActionMsg(Index).Message = vbNullString
    ActionMsg(Index).Created = 0
    ActionMsg(Index).Type = 0
    ActionMsg(Index).Color = 0
    ActionMsg(Index).Scroll = 0
    ActionMsg(Index).X = 0
    ActionMsg(Index).Y = 0
    ActionMsg(Index).FontType = 0

    ' find the new high index
    For i = MAX_BYTE To 1 Step -1

        If ActionMsg(i).Created > 0 Then
            Action_HighIndex = i + 1
            Exit For
        End If

    Next

    ' make sure we don't overflow
    If Action_HighIndex > MAX_BYTE Then Action_HighIndex = MAX_BYTE
End Sub

Public Sub CheckAnimInstance(ByVal Index As Long)
    Dim Looptime As Long
    Dim Layer As Long
    Dim FrameCount As Long

    ' if doesn't exist then exit sub
    If AnimInstance(Index).Animation <= 0 Then Exit Sub
    If AnimInstance(Index).Animation >= MAX_ANIMATIONS Then Exit Sub

    For Layer = 0 To 1

        If AnimInstance(Index).Used(Layer) Then
            Looptime = Animation(AnimInstance(Index).Animation).Looptime(Layer)
            FrameCount = Animation(AnimInstance(Index).Animation).FrameCount(Layer)

            ' if zero'd then set so we don't have extra loop and/or frame
            If AnimInstance(Index).FrameIndex(Layer) = 0 Then AnimInstance(Index).FrameIndex(Layer) = 1
            If AnimInstance(Index).LoopIndex(Layer) = 0 Then AnimInstance(Index).LoopIndex(Layer) = 1

            ' check if frame timer is set, and needs to have a frame change
            If AnimInstance(Index).Timer(Layer) + Looptime <= GetTickCount Then

                ' check if out of range
                If AnimInstance(Index).FrameIndex(Layer) >= FrameCount Then
                    AnimInstance(Index).LoopIndex(Layer) = AnimInstance(Index).LoopIndex(Layer) + 1

                    If AnimInstance(Index).LoopIndex(Layer) > Animation(AnimInstance(Index).Animation).LoopCount(Layer) Then
                        AnimInstance(Index).Used(Layer) = False
                    Else
                        AnimInstance(Index).FrameIndex(Layer) = 1
                    End If

                Else
                    AnimInstance(Index).FrameIndex(Layer) = AnimInstance(Index).FrameIndex(Layer) + 1
                End If

                AnimInstance(Index).Timer(Layer) = GetTickCount
            End If
        End If
    Next

    ' if neither layer is used, clear
    If AnimInstance(Index).Used(0) = False And AnimInstance(Index).Used(1) = False Then ClearAnimInstance (Index)
End Sub

' BitWise Operators for directional blocking
Public Sub SetDirBlock(ByRef blockvar As Byte, ByRef Dir As Byte, ByVal Block As Boolean)

    If Block Then
        blockvar = blockvar Or (2 ^ Dir)
    Else
        blockvar = blockvar And Not (2 ^ Dir)
    End If

End Sub

Public Function IsDirBlocked(ByRef blockvar As Byte, ByRef Dir As Byte) As Boolean

    If Not blockvar And (2 ^ Dir) Then
        IsDirBlocked = False
    Else
        IsDirBlocked = True
    End If

End Function

Public Sub PlayMapSound(ByVal X As Long, ByVal Y As Long, ByVal entityType As Long, ByVal entityNum As Long)
    Dim soundName As String

    If entityNum <= 0 Then Exit Sub

    ' find the sound
    Select Case entityType

        ' animations
    Case SoundEntity.SeAnimation

        If entityNum > MAX_ANIMATIONS Then Exit Sub
        soundName = Animation(entityNum).Sound

        ' items
    Case SoundEntity.SeItem

        If entityNum > MaximumItems Then Exit Sub
        soundName = Item(entityNum).Sound

        ' npcs
    Case SoundEntity.SeNpc

        If entityNum > MaximumNpcs Then Exit Sub
        soundName = Npc(entityNum).Sound

        ' resources
    'Case SoundEntity.SeResource

 '       If entityNum > MAX_RESOURCES Then Exit Sub
 '       soundName = Trim$(Resource(entityNum).Sound)
'
        ' spells
    Case SoundEntity.SeSpell

        If entityNum > MaximumSkills Then Exit Sub
        soundName = Skill(entityNum).Sound

        ' other
    Case Else
        Exit Sub
    End Select

    ' exit out if it's not set
    If Trim$(soundName) = "None." Then Exit Sub

    ' play the sound
    If X > 0 And Y > 0 Then Play_Sound soundName, X, Y
End Sub


Public Function ConvertMapX(ByVal X As Long) As Long
    ConvertMapX = X - (TileView.Left * PIC_X) - Camera.Left
End Function

Public Function ConvertMapY(ByVal Y As Long) As Long
    ConvertMapY = Y - (TileView.Top * PIC_Y) - Camera.Top
End Function

Public Sub UpdateCamera()
    Dim OffsetX As Long, OffSetY As Long, StartX As Long, StartY As Long, EndX As Long, EndY As Long

    OffsetX = Player(MyIndex).xOffset + PIC_X
    OffSetY = Player(MyIndex).yOffset + PIC_Y
    StartX = GetPlayerX(MyIndex) - ((TileWidth + 1) \ 2) - 1
    StartY = GetPlayerY(MyIndex) - ((TileHeight + 1) \ 2) - 1

    If TileWidth + 1 <= CurrentMap.MapData.MaxX Then
        If StartX < 0 Then
            OffsetX = 0

            If StartX = -1 Then
                If Player(MyIndex).xOffset > 0 Then
                    OffsetX = Player(MyIndex).xOffset
                End If
            End If

            StartX = 0
        End If

        EndX = StartX + (TileWidth + 1) + 1

        If EndX > CurrentMap.MapData.MaxX Then
            OffsetX = 32

            If EndX = CurrentMap.MapData.MaxX + 1 Then
                If Player(MyIndex).xOffset < 0 Then
                    OffsetX = Player(MyIndex).xOffset + PIC_X
                End If
            End If

            EndX = CurrentMap.MapData.MaxX
            StartX = EndX - TileWidth - 1
        End If
    Else
        EndX = StartX + (TileWidth + 1) + 1
    End If

    If TileHeight + 1 <= CurrentMap.MapData.MaxY Then
        If StartY < 0 Then
            OffSetY = 0

            If StartY = -1 Then
                If Player(MyIndex).yOffset > 0 Then
                    OffSetY = Player(MyIndex).yOffset
                End If
            End If

            StartY = 0
        End If

        EndY = StartY + (TileHeight + 1) + 1

        If EndY > CurrentMap.MapData.MaxY Then
            OffSetY = 32

            If EndY = CurrentMap.MapData.MaxY + 1 Then
                If Player(MyIndex).yOffset < 0 Then
                    OffSetY = Player(MyIndex).yOffset + PIC_Y
                End If
            End If

            EndY = CurrentMap.MapData.MaxY
            StartY = EndY - TileHeight - 1
        End If
    Else
        EndY = StartY + (TileHeight + 1) + 1
    End If

    If TileWidth + 1 = CurrentMap.MapData.MaxX Then
        OffsetX = 0
    End If

    If TileHeight + 1 = CurrentMap.MapData.MaxY Then
        OffSetY = 0
    End If

    With TileView
        .Top = StartY
        .Bottom = EndY
        .Left = StartX
        .Right = EndX
    End With

    With Camera
        .Top = OffSetY
        .Bottom = .Top + ScreenY
        .Left = OffsetX
        .Right = .Left + ScreenX
    End With

    CurX = TileView.Left + CInt((GlobalX + Camera.Left) \ PIC_X)
    CurY = TileView.Top + CInt((GlobalY + Camera.Top) \ PIC_Y)
    GlobalX_Map = GlobalX + (TileView.Left * PIC_X) + Camera.Left
    GlobalY_Map = GlobalY + (TileView.Top * PIC_Y) + Camera.Top
End Sub

Public Function CensorWord(ByVal sString As String) As String
    CensorWord = String$(Len(sString), "*")
End Function

Public Sub ScrollChatBox(ByVal Direction As Byte)
    If Direction = 0 Then    ' up
        If ChatScroll < ChatLines Then
            ChatScroll = ChatScroll + 1
        End If
    Else
        If ChatScroll > 0 Then
            ChatScroll = ChatScroll - 1
        End If
    End If
End Sub

Public Sub AddChatBubble(ByVal Target As Long, ByVal TargetType As Byte, ByVal Msg As String, ByVal Colour As Long)
    Dim i As Long, Index As Long
    ' set the global index
    ChatBubbleIndex = ChatBubbleIndex + 1

    ' reset to yourself for eventing
    If TargetType = 0 Then
        TargetType = TargetTypePlayer
        If Target = 0 Then Target = MyIndex
    End If

    If ChatBubbleIndex < 1 Or ChatBubbleIndex > MAX_BYTE Then ChatBubbleIndex = 1
    ' default to new bubble
    Index = ChatBubbleIndex

    ' loop through and see if that player/npc already has a chat bubble
    For i = 1 To MAX_BYTE
        If ChatBubble(i).TargetType = TargetType Then
            If ChatBubble(i).Target = Target Then
                ' reset master index
                If ChatBubbleIndex > 1 Then ChatBubbleIndex = ChatBubbleIndex - 1
                ' we use this one now, yes?
                Index = i
                Exit For
            End If
        End If
    Next

    ' set the bubble up
    With ChatBubble(Index)
        .Target = Target
        .TargetType = TargetType
        .Msg = Msg
        .Colour = Colour
        .Timer = GetTickCount
        .Active = True
    End With
End Sub

Public Sub FindNearestTarget()
    Dim i As Long, X As Long, Y As Long, x2 As Long, y2 As Long, xDif As Long, yDif As Long
    Dim bestX As Long, bestY As Long, bestIndex As Long
    x2 = GetPlayerX(MyIndex)
    y2 = GetPlayerY(MyIndex)
    bestX = 255
    bestY = 255

    For i = 1 To Npc_HighIndex

        If MapNpc(i).Num > 0 Then
            If Not GetNpcDead(i) Then
                X = MapNpc(i).X
                Y = MapNpc(i).Y

                ' find the difference - x
                If X < x2 Then
                    xDif = x2 - X
                ElseIf X > x2 Then
                    xDif = X - x2
                Else
                    xDif = 0
                End If

                ' find the difference - y
                If Y < y2 Then
                    yDif = y2 - Y
                ElseIf Y > y2 Then
                    yDif = Y - y2
                Else
                    yDif = 0
                End If

                ' best so far?
                If (xDif + yDif) < (bestX + bestY) Then
                    bestX = xDif
                    bestY = yDif
                    bestIndex = i
                End If
            End If
        End If

    Next

    ' target the best
    If bestIndex > 0 And bestIndex <> MyTargetIndex Then
        SendPlayerTarget bestIndex, TargetTypeNpc
        Call OpenTargetWindow
    End If
End Sub

Public Sub FindTarget()
    Dim i As Long, X As Long, Y As Long

    ' check players
    For i = 1 To Player_HighIndex

        If IsPlaying(i) And GetPlayerMap(MyIndex) = GetPlayerMap(i) Then
            X = (GetPlayerX(i) * 32) + Player(i).xOffset + 32
            Y = (GetPlayerY(i) * 32) + Player(i).yOffset + 32

            If X >= GlobalX_Map And X <= GlobalX_Map + 32 Then
                If Y >= GlobalY_Map And Y <= GlobalY_Map + 32 Then

                    If MyTargetType = TargetTypeLoot And MyTargetIndex > 0 Then
                        Call SendCloseLoot
                    End If

                    ' found our target!
                    SendPlayerTarget i, TargetTypePlayer
                    Call OpenTargetWindow

                    Exit Sub
                End If
            End If
        End If

    Next

    ' check npcs
    For i = 1 To Npc_HighIndex

        If MapNpc(i).Num > 0 Then
            If Not GetNpcDead(i) Then
                X = (MapNpc(i).X * 32) + MapNpc(i).xOffset + 32
                Y = (MapNpc(i).Y * 32) + MapNpc(i).yOffset + 32

                If X >= GlobalX_Map And X <= GlobalX_Map + 32 Then
                    If Y >= GlobalY_Map And Y <= GlobalY_Map + 32 Then

                        If MyTargetType = TargetTypeLoot And MyTargetIndex > 0 Then
                            Call SendCloseLoot
                        End If

                        ' found our target!
                        SendPlayerTarget i, TargetTypeNpc
                        Call OpenTargetWindow
                        Exit Sub
                    End If
                End If
            End If
        End If
    Next

    For i = 1 To Corpse_HighIndex
        If Corpse(i).LootId > 0 Then
            X = Corpse(i).X + 32
            Y = Corpse(i).Y + 32

            If X >= GlobalX_Map And X <= GlobalX_Map + 32 Then
                If Y >= GlobalY_Map And Y <= GlobalY_Map + 32 Then

                    If MyTargetType = TargetTypeLoot And MyTargetIndex = Corpse(i).LootId Then
                        If Windows(GetWindowIndex("winLoot")).Window.Visible Then
                            Exit Sub
                        End If
                    ElseIf MyTargetType = TargetTypeLoot And MyTargetIndex <> Corpse(i).LootId Then
                        Call SendCloseLoot
                    End If

                    ' found our target!
                    SendPlayerTarget Corpse(i).LootId, TargetTypeLoot
                    Exit Sub
                End If
            End If

        End If
    Next

    ' case else, close if its open
    Call CloseTargetWindow

End Sub

Public Sub SetBarWidth(ByRef MaxWidth As Long, ByRef Width As Long)
    Dim barDifference As Long

    If MaxWidth < Width Then
        ' find out the amount to increase per loop
        barDifference = ((Width - MaxWidth) / 100) * 10

        ' if it's less than 1 then default to 1
        If barDifference < 1 Then barDifference = 1
        ' set the width
        Width = Width - barDifference
    ElseIf MaxWidth > Width Then
        ' find out the amount to increase per loop
        barDifference = ((MaxWidth - Width) / 100) * 10

        ' if it's less than 1 then default to 1
        If barDifference < 1 Then barDifference = 1
        ' set the width
        Width = Width + barDifference
    End If

End Sub

Public Sub AttemptLogin()
    TcpInit GameServerIp, GameServerPort

    ' send login packet
    If ConnectToServer Then
        SendLogin Windows(GetWindowIndex("winLogin")).Controls(GetControlIndex("winLogin", "txtUser")).Text
        Exit Sub
    End If

    If Not IsConnected Then
        ShowWindow GetWindowIndex("winLogin")
        ShowDialogue "Problema de Conexao", "Não pode conectar-se ao game server.", "Tente novamente depois.", DialogueTypeAlert
    End If
End Sub

Public Sub DialogueAlert(ByVal Index As Long)
    Dim Header As String, Body As String, Body2 As String

    ' find the body/header
    Select Case Index

    Case AlertMessage.Alert_Failed
        Header = "Falha"
        Body = "Falha na operação."
        Body2 = "Entre em contato com o administrador."

    Case AlertMessage.Alert_connection
        Header = "Problema de Conexão"
        Body = "A conexão foi o servidor foi perdida."
        Body2 = "Tente novamente mais tarde."

    Case AlertMessage.Alert_AccountIsBanned
        Header = "Banido"
        Body = "Voce foi banido do servidor."
        Body2 = "Entre em contato com o administrador."

    Case AlertMessage.Alert_Kicked
        Header = "Chutado"
        Body = "Voce foi chutado do servidor."
        Body2 = "Comporte-se."

    Case AlertMessage.Alert_VersionOutdated
        Header = "Versao Errada"
        Body = "A versao do jogo esta errada."
        Body2 = "Uma atualizacao é necessária."

    Case AlertMessage.Alert_StringLength
        Header = "Tamanho Inválido"
        Body = "Usuário ou senha está pequeno ou grande demais."
        Body2 = "Insira um usuário e senha válidos."

    Case AlertMessage.Alert_IllegalName
        Header = "Caracteres Ilegais"
        Body = "Usuário ou senha contém caracteres ilegais."
        Body2 = "Insira um usuário e senha válidos"

    Case AlertMessage.Alert_Maintenance
        Header = "Manutenção"
        Body = "O servidor está em manutenção"
        Body2 = "Tente novamente mais tarde"

    Case AlertMessage.Alert_NameTaken
        Header = "Nome Inválido"
        Body = "O nome está sendo usado."
        Body2 = "Tente um nome diferente."

    Case AlertMessage.Alert_NameLength
        Header = "Nome Inválido"
        Body = "O nome é muito grande ou pequeno demais."
        Body2 = "Tente um nome diferente."

    Case AlertMessage.Alert_NameIllegal
        Header = "Nome Inválido"
        Body = "O nome contém caracteres ilegais."
        Body2 = "Use letras e números somente."

    Case AlertMessage.Alert_Database
        Header = "Problema de Conexão"
        Body = "Não pode se conectar ao banco de dados."
        Body2 = "Tente novamente mais tarde."

    Case AlertMessage.Alert_WrongAccountData
        Header = "Login Inválido"
        Body = "Usuario ou senha inválidos."
        Body2 = "Tente novamente."

    Case AlertMessage.Alert_AccountIsNotActivated
        Header = "Usuario Desativado"
        Body = "O usuário não está ativado."
        Body2 = "Ative sua conta e tente novamente."

    Case AlertMessage.Alert_CharacterIsDeleted
        Header = "Personagem Deletado"
        Body = "O personagem foi deletado."
        Body2 = "Faça o login para continuar jogando."

    Case AlertMessage.Alert_CharacterCreation
        Header = "Criação de Personagens"
        Body = "A criação de personagens esta desativada."
        Body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_CharacterDelete
        Header = "Exclusão de Personagens"
        Body = "A exclusão de personagens está desativada."
        Body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_InvalidLevelDelete
        Header = "Exclusão de Personagens"
        Body = "Este personagem não pode ser mais excluido."
        Body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_DuplicatedLogin
        Header = "Login Duplicado"
        Body = "Este usuário já está conectado."
        Body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_TryingToLogin
        Header = "Login Duplicado"
        Body = "Alguém está tentando acessar está conta."
        Body2 = "Mude as suas opções de segurança."

    Case AlertMessage.Alert_InvalidPacket
        Header = "Pacote Inválido"
        Body = "Houve um bloqueio nos dados transmitidos."
        Body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_InvalidRecipientName
        Header = "Nome Inválido"
        Body = "O nome do usuário é inválido."
        Body2 = "Verifique a ortografia."

    Case AlertMessage.Alert_InvalidItem
        Header = "Item Inválido"
        Body = "O item selecionado é inválido."
        Body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_NotEnoughCash
        Header = "Saldo Insuficiente"
        Body = "O saldo é insuficiente para realizar a compra."
        Body2 = "Faça uma recarga."

    Case AlertMessage.Alert_SuccessPurchase
        Header = "Sucesso na Operação"
        Body = "O processo de compra foi realizado com sucesso."
        Body2 = "Será enviado pelo correio para o destinatário."

    Case AlertMessage.Alert_NotEnoughCurrency
        Header = "Ouro insuficiente"
        Body = "Não há ouro suficiente para a operação."
        Body2 = "Entre em contato com o Administrador."

    End Select

    ' set the dialogue up!
    ShowDialogue Header, Body, Body2, DialogueTypeAlert
End Sub
Public Function Clamp(ByVal Value As Long, ByVal Min As Long, ByVal Max As Long) As Long
    Clamp = Value

    If Value < Min Then Clamp = Min
    If Value > Max Then Clamp = Max
End Function


Public Sub ShowInvDesc(X As Long, Y As Long, InvNum As Long)

    If InvNum <= 0 Or InvNum > MaxInventory Then Exit Sub
    Dim ItemNum As Long

    ItemNum = GetInventoryItemNum(InvNum)

    If ItemNum > 0 Then
         Dim Inventory As InventoryRec
    
         Inventory.Num = GetInventoryItemNum(InvNum)
         Inventory.Value = GetInventoryItemValue(InvNum)
         Inventory.Level = GetInventoryItemLevel(InvNum)
         Inventory.Bound = GetInventoryItemBound(InvNum)
         Inventory.AttributeId = GetInventoryItemAttributeId(InvNum)
         Inventory.UpgradeId = GetInventoryItemUpgradeId(InvNum)
    
        If Item(ItemNum).Type = ItemType.ItemType_Heraldry Then
            Call ShowHeraldryDescription(X, Y, Inventory, Item(ItemNum).Price)
        Else
            ShowItemDesc X, Y, Inventory
        End If
    End If
End Sub

Public Sub ShowEqDesc(X As Long, Y As Long, eqNum As Long)
    If eqNum <= 0 Or eqNum > PlayerEquipments.PlayerEquipment_Count - 1 Then
        Exit Sub
    End If

    If GetPlayerEquipmentId(eqNum) Then
        Dim Inventory As InventoryRec
        
        Inventory.Num = GetPlayerEquipmentId(eqNum)
        Inventory.Value = 1
        Inventory.Level = GetPlayerEquipmentLevel(eqNum)
        Inventory.Bound = GetPlayerEquipmentBound(eqNum)
        Inventory.AttributeId = GetPlayerEquipmentAttributeId(eqNum)
        Inventory.UpgradeId = GetPlayerEquipmentUpgradeId(eqNum)

        ShowItemDesc X, Y, Inventory
    End If
End Sub

Public Sub AddDescInfo(Text As String, Optional Colour As Long = White)
    Dim Count As Long
    Count = UBound(DescText)
    ReDim Preserve DescText(1 To Count + 1) As TextColourRec
    DescText(Count + 1).Text = Text
    DescText(Count + 1).Colour = Colour
End Sub

Public Sub SwitchHotbar(OldSlot As Long, NewSlot As Long)
    If OldSlot < 1 Or OldSlot > MaximumQuickSlot Then
        Exit Sub
    End If

    If NewSlot < 1 Or NewSlot > MaximumQuickSlot Then
        Exit Sub
    End If

    Call SendSwapQuickSlot(OldSlot, NewSlot)
End Sub

Public Sub ShowChat()
    ShowWindow GetWindowIndex("winChat"), , False
    HideWindow GetWindowIndex("winChatSmall")
    ' Set the active control
    activeWindow = GetWindowIndex("winChat")
    SetActiveControl GetWindowIndex("winChat"), GetControlIndex("winChat", "txtChat")
    inSmallChat = False
    ChatScroll = 0
End Sub

Public Sub HideChat()
    ShowWindow GetWindowIndex("winChatSmall"), , False
    HideWindow GetWindowIndex("winChat")
    inSmallChat = True
    ChatScroll = 0
End Sub

Public Sub SetChatHeight(Height As Long)
    actChatHeight = Height
End Sub

Public Sub SetChatWidth(Width As Long)
    actChatWidth = Width
End Sub

Public Sub UpdateChat()
    SaveOptions
End Sub

Sub ShowPlayerMenu(Index As Long, X As Long, Y As Long)
    PlayerMenuIndex = Index
    If PlayerMenuIndex = 0 Then Exit Sub
    Windows(GetWindowIndex("winPlayerMenu")).Window.Left = X - 5
    Windows(GetWindowIndex("winPlayerMenu")).Window.Top = Y - 5
    Windows(GetWindowIndex("winPlayerMenu")).Controls(GetControlIndex("winPlayerMenu", "btnName")).Text = Trim$(GetPlayerName(PlayerMenuIndex))
    ShowWindow GetWindowIndex("winRightClickBG")
    ShowWindow GetWindowIndex("winPlayerMenu"), , False
End Sub

Public Function AryCount(ByRef Ary() As Byte) As Long
    On Error Resume Next

    AryCount = UBound(Ary) + 1
End Function

Public Function ByteToInt(ByVal B1 As Long, ByVal B2 As Long) As Long
    ByteToInt = B1 * 256 + B2
End Function

Sub UpdateStats_UI()
' set the bar labels
    With Windows(GetWindowIndex("winBars"))
        .Controls(GetControlIndex("winBars", "lblHP")).Text = GetPlayerVital(MyIndex, HP) & "/" & GetPlayerMaxVital(MyIndex, HP)
        .Controls(GetControlIndex("winBars", "lblMP")).Text = GetPlayerVital(MyIndex, MP) & "/" & GetPlayerMaxVital(MyIndex, MP)
        .Controls(GetControlIndex("winBars", "lblEXP")).Text = GetPlayerExp() & "/" & TNL
    End With

    ' update character screen
End Sub


Sub ShowComboMenu(curWindow As Long, curControl As Long)
    Dim Top As Long
    With Windows(curWindow).Controls(curControl)
        ' linked to
        Windows(GetWindowIndex("winComboMenu")).Window.linkedToWin = curWindow
        Windows(GetWindowIndex("winComboMenu")).Window.linkedToCon = curControl
        ' set the size
        Windows(GetWindowIndex("winComboMenu")).Window.Height = 2 + (UBound(.list) * 16)
        Windows(GetWindowIndex("winComboMenu")).Window.Left = Windows(curWindow).Window.Left + .Left + 2
        Top = Windows(curWindow).Window.Top + .Top + .Height
        If Top + Windows(GetWindowIndex("winComboMenu")).Window.Height > ScreenHeight Then Top = ScreenHeight - Windows(GetWindowIndex("winComboMenu")).Window.Height
        Windows(GetWindowIndex("winComboMenu")).Window.Top = Top
        Windows(GetWindowIndex("winComboMenu")).Window.Width = .Width - 4
        ' set the values
        Windows(GetWindowIndex("winComboMenu")).Window.list() = .list()
        Windows(GetWindowIndex("winComboMenu")).Window.Value = .Value
        Windows(GetWindowIndex("winComboMenu")).Window.group = 0
        ' load the menu
        ShowWindow GetWindowIndex("winComboMenuBG"), True, False
        ShowWindow GetWindowIndex("winComboMenu"), True, False
    End With
End Sub

Sub ComboMenu_MouseMove(curWindow As Long)
    Dim Y As Long, i As Long
    With Windows(curWindow).Window
        Y = currMouseY - .Top
        ' find the option we're hovering over
        If UBound(.list) > 0 Then
            For i = 1 To UBound(.list)
                If Y >= (16 * (i - 1)) And Y <= (16 * (i)) Then
                    .group = i
                End If
            Next
        End If
    End With
End Sub

Sub ComboMenu_MouseDown(curWindow As Long)
    Dim Y As Long, i As Long
    With Windows(curWindow).Window
        Y = currMouseY - .Top
        ' find the option we're hovering over
        If UBound(.list) > 0 Then
            For i = 1 To UBound(.list)
                If Y >= (16 * (i - 1)) And Y <= (16 * (i)) Then
                    Windows(.linkedToWin).Controls(.linkedToCon).Value = i
                    CloseComboMenu
                End If
            Next
        End If
    End With
End Sub





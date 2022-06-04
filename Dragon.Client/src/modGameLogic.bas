Attribute VB_Name = "modGameLogic"
Option Explicit

Public Sub GameLoop()
    Dim FrameTime As Long, Tick As Long, TickFPS As Long, FPS As Long, i As Long, WalkTimer As Long, X As Long, Y As Long
    Dim tmr25 As Long, tmr1000, tmr10000 As Long, tmr100 As Long, mapTimer As Long, chatTmr As Long, targetTmr As Long, fogTmr As Long, barTmr As Long
    Dim barDifference As Long, fadeTmr As Long

    ' *** Start GameLoop ***
    Do While InGame
        Tick = GetTickCount                            ' Set the inital tick
        ElapsedTime = Tick - FrameTime                 ' Set the time difference for time-based movement
        FrameTime = Tick                               ' Set the time second loop time to the first.

        ' handle input
        If GetForegroundWindow() = frmMain.hWnd Then
            HandleMouseInput
        End If
        
        ' * Check surface timers *
        ' Sprites
        If tmr10000 < Tick Then
            ' check ping
            Call GetPing
            tmr10000 = Tick + 10000
        End If

        If tmr25 < Tick Then
            InGame = IsConnected
            Call CheckKeys    ' Check to make sure they aren't trying to auto do anything

            If GetForegroundWindow() = frmMain.hWnd Then
                Call CheckInputKeys    ' Check which keys were pressed
            End If

            ' check if we need to end the CD icon
            If Count_Spellicon > 0 Then
                For i = 1 To MaxPlayerSkill
                    If PlayerSkill(i).Id > 0 Then
                        If SpellCd(i) > 0 Then
                            If SpellCd(i) + (Skill(PlayerSkill(i).Id).Cooldown * 1000) < Tick Then
                                SpellCd(i) = 0
                            End If
                        End If
                    End If
                Next
            End If

            ' check if we need to unlock the player's spell casting restriction
            If SpellBuffer > 0 Then
                If SpellBufferTimer + (Skill(PlayerSkill(SpellBuffer).Id).CastTime * 1000) < Tick Then
                    SpellBuffer = 0
                    SpellBufferTimer = 0
                End If
            End If

            If CanMoveNow Then
                Call CheckMovement    ' Check if player is trying to move
                Call CheckAttack   ' Check to see if player is trying to attack
            End If

            For i = 1 To MAX_BYTE
                CheckAnimInstance i
            Next

            tmr25 = Tick + 25
        End If

        ' targetting
        If targetTmr < Tick Then
            If TabDown Then
                FindNearestTarget
            End If

            targetTmr = Tick + 50
        End If

        ' chat timer
        If chatTmr < Tick Then
            ' scrolling
            If ChatButtonUp Then
                ScrollChatBox 0
            End If

            If ChatButtonDown Then
                ScrollChatBox 1
            End If

            ' remove messages
            If ChatLastRemove + ChatDifferenceTimer < GetTickCount Then
                ' remove timed out messages from chat
                For i = Chat_HighIndex To 1 Step -1
                    If Len(Chat(i).Text) > 0 Then
                        If Chat(i).Visible Then
                            If Chat(i).Timer + CHAT_TIMER < Tick Then
                                Chat(i).Visible = False
                                ChatLastRemove = GetTickCount
                                Exit For
                            End If
                        End If
                    End If
                Next
            End If

            chatTmr = Tick + 50
        End If

        ' fog scrolling
        If fogTmr < Tick Then
            ' move
            FogOffsetX = FogOffsetX - 1
            FogOffsetY = FogOffsetY - 1

            ' reset
            If FogOffsetX < -256 Then FogOffsetX = 0
            If FogOffsetY < -256 Then FogOffsetY = 0

            ' reset timer
            fogTmr = Tick + 20
        End If

        ' elastic bars
        If barTmr < Tick Then
            SetBarWidth BarWidth_GuiHP_Max, BarWidth_GuiHP
            SetBarWidth BarWidth_GuiSP_Max, BarWidth_GuiSP
            SetBarWidth BarWidth_GuiEXP_Max, BarWidth_GuiEXP

            SetBarWidth BarWidth_GuiGuildExp_Max, BarWidth_GuiGuildExp

            SetBarWidth BarWidth_TargetHP_Max, BarWidth_TargetHP
            SetBarWidth BarWidth_TargetMP_Max, BarWidth_TargetMP

            For i = 1 To Npc_HighIndex
                If MapNpc(i).Num > 0 Then
                    SetBarWidth BarWidth_NpcHP_Max(i), BarWidth_NpcHP(i)
                End If
            Next

            For i = 1 To Player_HighIndex
                If IsPlaying(i) And GetPlayerMap(i) = GetPlayerMap(MyIndex) Then
                    SetBarWidth BarWidth_PlayerHP_Max(i), BarWidth_PlayerHP(i)
                End If
            Next

            ' reset timer
            barTmr = Tick + 15
        End If

        ' Animations!
        If mapTimer < Tick Then
            ' animate textbox
            If chatShowLine = "|" Then
                chatShowLine = vbNullString
            Else
                chatShowLine = "|"
            End If

            ' re-set timer
            mapTimer = Tick + 500
        End If

        ' Process input before rendering, otherwise input will be behind by 1 frame
        If WalkTimer < Tick Then

            For i = 1 To Player_HighIndex
                If IsPlaying(i) Then
                    Call ProcessMovement(i)
                End If
            Next i

            ' Process npc movements (actually move them)
            For i = 1 To Npc_HighIndex
                If Not MapNpc(i).Dead Then
                    If MapNpc(i).Num > 0 Then
                        If Not MapNpc(i).Dying Then
                            Call ProcessNpcMovement(i)
                        End If
                    End If
                End If
            Next i

            WalkTimer = Tick + 30    ' edit this value to change WalkTimer
        End If

        If tmr1000 < Tick Then
            ' Check for player
            Call CalculatePlayerIconRemainTime
            Call CalculateNpcIconRemainTime
            Call CalculatePartyIconRemainTime

            ' Progress bar effect
            Call ProcessCraftItem
            Call ProcessUpgradeItem
            Call ProcessRollDice

            tmr1000 = Tick + 1000
        End If

        ' fading
        If fadeTmr < Tick Then
            ' Disappear corpse
            Call ProcessCorpseFadeAlpha

            fadeTmr = Tick + 45
        End If

        ' Change target frames (animation)
        Call UpdateTargetCalculation

        ' *********************
        ' ** Render Graphics **
        ' *********************
        Call Render_Graphics

        DoEvents

        ' Lock fps
        If Not FPS_Lock Then

          '  Do While GetTickCount < Tick + 15
                '  DoEvents
                Sleep 1
           ' Loop

        End If

        ' Calculate fps
        If TickFPS < Tick Then
            GameFPS = FPS
            TickFPS = Tick + 1000
            FPS = 0
        Else
            FPS = FPS + 1
        End If
    Loop

    frmMain.Visible = False

    If isLogging Then
        isLogging = False
        MenuLoop
        GettingMap = True
        Stop_Music
        Play_Music MenuMusic
    Else
        ' Shutdown the game
        Call SetStatus("Destruindo os dados do jogo.")
        Call DestroyGame
    End If

End Sub

Public Sub MenuLoop()
    Dim FrameTime As Long, Tick As Long, TickFPS As Long, FPS As Long, tmr500 As Long, fadeTmr As Long
    Dim tmr1000 As Long

    ' *** Start GameLoop ***
    Do While inMenu
        Tick = GetTickCount                            ' Set the inital tick
        ElapsedTime = Tick - FrameTime                 ' Set the time difference for time-based movement
        FrameTime = Tick                               ' Set the time second loop time to the first.

        ' handle input
        If GetForegroundWindow() = frmMain.hWnd Then
            HandleMouseInput
        End If

        ' Animations!
        If tmr500 < Tick Then
            ' animate textbox
            If chatShowLine = "|" Then
                chatShowLine = vbNullString
            Else
                chatShowLine = "|"
            End If

            ' re-set timer
            tmr500 = Tick + 500
        End If

        ' Process time in character screen.
        If tmr1000 < Tick Then
            Call ProcessCharacterDeleteDate

            tmr1000 = Tick + 999
        End If

        ' fading
        If fadeTmr < Tick Then
            If FadeAlpha > 5 Then
                ' lower fade
                FadeAlpha = FadeAlpha - 5
            Else
                FadeAlpha = 0
            End If

            fadeTmr = Tick + 1
        End If

        ' *********************
        ' ** Render Graphics **
        ' *********************
        Call Render_Menu

        ' do events
        DoEvents

        ' Lock fps
        '  If Not FPS_Lock Then

        ' Do While GetTickCount < tick + 20
        '       DoEvents
        Sleep 1
        'Loop

        'End If

        ' Calculate fps
        If TickFPS < Tick Then
            GameFPS = FPS
            TickFPS = Tick + 1000
            FPS = 0
        Else
            FPS = FPS + 1
        End If

    Loop

End Sub

Function IsTryingToMove() As Boolean

'If DirUp Or DirDown Or DirLeft Or DirRight Then
    If WDown Or SDown Or ADown Or DDown Or DirUpLeft Or DirUpRight Or DirDownLeft Or DirDownRight Then
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

    If DirUpLeft Then
        Call SetPlayerDirection(MyIndex, DIR_UP_LEFT)

        ' Check to see if they are trying to go out of bounds
        If GetPlayerY(MyIndex) > 0 And GetPlayerX(MyIndex) > 0 Then
            If CheckDirection(DIR_UP_LEFT) Then
                CanMove = False
                ' Set the new direction if they weren't facing that direction
                If D <> DIR_UP_LEFT Then
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


    If DirUpRight Then
        Call SetPlayerDirection(MyIndex, DIR_UP_RIGHT)

        ' Check to see if they are trying to go out of bounds
        If GetPlayerY(MyIndex) > 0 And GetPlayerX(MyIndex) < CurrentMap.MapData.MaxX Then
            If CheckDirection(DIR_UP_RIGHT) Then
                CanMove = False

                ' Set the new direction if they weren't facing that direction
                If D <> DIR_UP_RIGHT Then
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


    If DirDownLeft Then
        Call SetPlayerDirection(MyIndex, DIR_DOWN_LEFT)
        ' Check to see if they are trying to go out of bounds

        If GetPlayerY(MyIndex) < CurrentMap.MapData.MaxY And GetPlayerX(MyIndex) > 0 Then
            If CheckDirection(DIR_DOWN_LEFT) Then
                CanMove = False
                ' Set the new direction if they weren't facing that direction

                If D <> DIR_DOWN_LEFT Then
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


    If DirDownRight Then
        Call SetPlayerDirection(MyIndex, DIR_DOWN_RIGHT)
        ' Check to see if they are trying to go out of bounds

        If GetPlayerY(MyIndex) < CurrentMap.MapData.MaxY And GetPlayerX(MyIndex) < CurrentMap.MapData.MaxX Then
            If CheckDirection(DIR_DOWN_RIGHT) Then
                CanMove = False
                ' Set the new direction if they weren't facing that direction
                If D <> DIR_DOWN_RIGHT Then
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

    Case DIR_UP_LEFT
        X = GetPlayerX(MyIndex) - 1
        Y = GetPlayerY(MyIndex) - 1

    Case DIR_UP_RIGHT
        X = GetPlayerX(MyIndex) + 1
        Y = GetPlayerY(MyIndex) - 1

    Case DIR_DOWN_LEFT
        X = GetPlayerX(MyIndex) - 1
        Y = GetPlayerY(MyIndex) + 1

    Case DIR_DOWN_RIGHT
        X = GetPlayerX(MyIndex) + 1
        Y = GetPlayerY(MyIndex) + 1

    End Select

    If Direction >= DIR_UP_LEFT Then
        ' check directional blocking
        If Direction = DIR_UP_LEFT Then
            If isDirBlocked(CurrentMap.TileData.Tile(X, Y).DirBlock, DIR_UP) Then    'And isDirBlocked(CurrentMap.TileData.Tile(X, Y).DirBlock, DIR_LEFT) Then
                CheckDirection = True
                Exit Function
            End If
        ElseIf Direction = DIR_UP_RIGHT Then
            If isDirBlocked(CurrentMap.TileData.Tile(X, Y).DirBlock, DIR_UP) Then    'And isDirBlocked(CurrentMap.TileData.Tile(X, Y).DirBlock, DIR_RIGHT) Then
                CheckDirection = True
                Exit Function
            End If
        ElseIf Direction = DIR_DOWN_LEFT Then
            If isDirBlocked(CurrentMap.TileData.Tile(X, Y).DirBlock, DIR_DOWN) Then    'And isDirBlocked(CurrentMap.TileData.Tile(X, Y).DirBlock, DIR_LEFT) Then
                CheckDirection = True
                Exit Function
            End If
        ElseIf Direction = DIR_DOWN_RIGHT Then
            If isDirBlocked(CurrentMap.TileData.Tile(X, Y).DirBlock, DIR_DOWN) Then    'And isDirBlocked(CurrentMap.TileData.Tile(X, Y).DirBlock, DIR_RIGHT) Then
                CheckDirection = True
                Exit Function
            End If
        End If
    Else
        ' check directional blocking
        If isDirBlocked(CurrentMap.TileData.Tile(GetPlayerX(MyIndex), GetPlayerY(MyIndex)).DirBlock, Direction) Then
            CheckDirection = True
            Exit Function
        End If
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

    ' Check to see if the key door is open or not
    If CurrentMap.TileData.Tile(X, Y).Type = TILE_TYPE_KEY Then
        ' This actually checks if its open or not
        If TempTile(X, Y).DoorOpen = 0 Then
            CheckDirection = True
            Exit Function
        End If
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

                Case DIR_UP_LEFT
                    Call SendPlayerMovement
                    Player(MyIndex).yOffset = PIC_Y
                    Call SetPlayerY(MyIndex, GetPlayerY(MyIndex) - 1)
                    Player(MyIndex).xOffset = PIC_X
                    Call SetPlayerX(MyIndex, GetPlayerX(MyIndex) - 1)

                Case DIR_UP_RIGHT
                    Call SendPlayerMovement
                    Player(MyIndex).yOffset = PIC_Y
                    Call SetPlayerY(MyIndex, GetPlayerY(MyIndex) - 1)
                    Player(MyIndex).xOffset = PIC_X * -1
                    Call SetPlayerX(MyIndex, GetPlayerX(MyIndex) + 1)

                Case DIR_DOWN_LEFT
                    Call SendPlayerMovement
                    Player(MyIndex).yOffset = PIC_Y * -1
                    Call SetPlayerY(MyIndex, GetPlayerY(MyIndex) + 1)
                    Player(MyIndex).xOffset = PIC_X
                    Call SetPlayerX(MyIndex, GetPlayerX(MyIndex) - 1)

                Case DIR_DOWN_RIGHT
                    Call SendPlayerMovement
                    Player(MyIndex).yOffset = PIC_Y * -1
                    Call SetPlayerY(MyIndex, GetPlayerY(MyIndex) + 1)
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

Sub ClearTempTile()
    Dim X As Long
    Dim Y As Long

    ReDim TempTile(0 To CurrentMap.MapData.MaxX, 0 To CurrentMap.MapData.MaxY)

    For X = 0 To CurrentMap.MapData.MaxX
        For Y = 0 To CurrentMap.MapData.MaxY
            TempTile(X, Y).DoorOpen = 0

            ' If Not GettingMap Then cacheRenderState X, Y, MapLayer.Mask
        Next
    Next

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
Public Sub setDirBlock(ByRef blockvar As Byte, ByRef Dir As Byte, ByVal Block As Boolean)

    If Block Then
        blockvar = blockvar Or (2 ^ Dir)
    Else
        blockvar = blockvar And Not (2 ^ Dir)
    End If

End Sub

Public Function isDirBlocked(ByRef blockvar As Byte, ByRef Dir As Byte) As Boolean

    If Not blockvar And (2 ^ Dir) Then
        isDirBlocked = False
    Else
        isDirBlocked = True
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

Public Sub CloseDialogue()
    diaIndex = 0
    HideWindow GetWindowIndex("winBlank")
    HideWindow GetWindowIndex("winDialogue")
End Sub

Public Sub Dialogue(ByVal Header As String, ByVal body As String, ByVal body2 As String, ByVal Index As Long, Optional ByVal style As Byte = 1, Optional ByVal Data1 As Long = 0)
' exit out if we've already got a dialogue open
    If diaIndex > 0 Then Exit Sub

    ' set buttons
    With Windows(GetWindowIndex("winDialogue"))
        If style = StyleYESNO Then
            .Controls(GetControlIndex("winDialogue", "btnYes")).Visible = True
            .Controls(GetControlIndex("winDialogue", "btnNo")).Visible = True
            .Controls(GetControlIndex("winDialogue", "btnOkay")).Visible = False
            .Controls(GetControlIndex("winDialogue", "txtInput")).Visible = False
            .Controls(GetControlIndex("winDialogue", "lblBody_2")).Visible = True
        ElseIf style = StyleOKAY Then
            .Controls(GetControlIndex("winDialogue", "btnYes")).Visible = False
            .Controls(GetControlIndex("winDialogue", "btnNo")).Visible = False
            .Controls(GetControlIndex("winDialogue", "btnOkay")).Visible = True
            .Controls(GetControlIndex("winDialogue", "txtInput")).Visible = False
            .Controls(GetControlIndex("winDialogue", "lblBody_2")).Visible = True
        ElseIf style = StyleINPUT Then
            .Controls(GetControlIndex("winDialogue", "btnYes")).Visible = False
            .Controls(GetControlIndex("winDialogue", "btnNo")).Visible = False
            .Controls(GetControlIndex("winDialogue", "btnOkay")).Visible = True
            .Controls(GetControlIndex("winDialogue", "txtInput")).Visible = True
            .Controls(GetControlIndex("winDialogue", "lblBody_2")).Visible = False
        End If

        ' set labels
        .Controls(GetControlIndex("winDialogue", "lblHeader")).Text = Header
        .Controls(GetControlIndex("winDialogue", "lblBody_1")).Text = body
        .Controls(GetControlIndex("winDialogue", "lblBody_2")).Text = body2
        .Controls(GetControlIndex("winDialogue", "txtInput")).Text = vbNullString
    End With

    Windows(GetWindowIndex("winDialogue")).activeControl = GetControlIndex("winDialogue", "txtInput")

    ' set it all up
    diaIndex = Index
    diaData1 = Data1
    diaStyle = style

    ' make the windows visible
    ShowWindow GetWindowIndex("winBlank"), True
    ShowWindow GetWindowIndex("winDialogue"), True
End Sub

Public Sub DialogueHandler(ByVal Index As Long)
    Dim Value As Long, DiaInput As String

    Dim Buffer As New clsBuffer
    Set Buffer = New clsBuffer

    DiaInput = Trim$(Windows(GetWindowIndex("winDialogue")).Controls(GetControlIndex("winDialogue", "txtInput")).Text)

    ' find out which button
    If Index = 1 Then    ' okay button

        ' dialogue index
        Select Case diaIndex
        Case TypeTRADEAMOUNT
            Value = Val(DiaInput)
            If Value > 0 Then SendTradeItem diaData1, Value
        Case TypeDEPOSITITEM
            Value = Val(DiaInput)
            If Value > 0 Then SendDepositItem diaData1, Value
        Case TypeWITHDRAWITEM
            Value = Val(DiaInput)
            If Value > 0 Then SendWithdrawItem diaData1, Value
        Case TypeTRADEGOLDAMOUNT
            Value = Val(DiaInput)
            If Value > 0 Then SendSelectTradeCurrency Value
        Case TypeSELLAMOUNT
            Value = Val(DiaInput)
            If Value > 0 Then SendSellItem diaData1, Value
        Case TypeADDMAILCURRENCY
            AddSendMailCurrencyValue Val(DiaInput)
        Case TypeADDMAILAMOUNT
            AddSendMailItemValue Val(DiaInput)
        End Select

    ElseIf Index = 2 Then    ' yes button
        ' dialogue index
        Select Case diaIndex

        Case TypeDESTROYITEM
            SendDestroyItem diaData1

        Case TypeTRADE
            SendAcceptTradeRequest

        Case TypeFORGET

        Case TypePARTY
            SendAcceptParty

        Case TypeLOOTITEM
            ' send the packet
            'Player(MyIndex).MapGetTimer = GetTickCount
            'Buffer.WriteLong CMapGetItem
            'SendData Buffer.ToArray()

        Case TypeDELCHAR
            ' send the deletion
            SendDeleteChar diaData1

        Case TypeDELETEMAIL
            SendDeleteMail

        End Select

    ElseIf Index = 3 Then    ' no button

        ' dialogue index
        Select Case diaIndex

        Case TypeTRADE
            SendDeclineTradeRequest

        Case TypePARTY
            SendDeclineParty
        End Select
    End If

    CloseDialogue
    diaIndex = 0
    DiaInput = vbNullString
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
    If bestIndex > 0 And bestIndex <> MyTarget Then
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

                    If MyTargetType = TargetTypeLoot And MyTarget > 0 Then
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

                        If MyTargetType = TargetTypeLoot And MyTarget > 0 Then
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

                    If MyTargetType = TargetTypeLoot And MyTarget = Corpse(i).LootId Then
                        If Windows(GetWindowIndex("winLoot")).Window.Visible Then
                            Exit Sub
                        End If
                    ElseIf MyTargetType = TargetTypeLoot And MyTarget <> Corpse(i).LootId Then
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
        Dialogue "Problema de Conexao", "Não pode conectar-se ao game server.", "Tente novamente depois.", TypeALERT
    End If
End Sub

Public Sub DialogueAlert(ByVal Index As Long)
    Dim Header As String, body As String, body2 As String

    ' find the body/header
    Select Case Index

    Case AlertMessage.Alert_Failed
        Header = "Falha"
        body = "Falha na operação."
        body2 = "Entre em contato com o administrador."

    Case AlertMessage.Alert_connection
        Header = "Problema de Conexão"
        body = "A conexão foi o servidor foi perdida."
        body2 = "Tente novamente mais tarde."

    Case AlertMessage.Alert_AccountIsBanned
        Header = "Banido"
        body = "Voce foi banido do servidor."
        body2 = "Entre em contato com o administrador."

    Case AlertMessage.Alert_Kicked
        Header = "Chutado"
        body = "Voce foi chutado do servidor."
        body2 = "Comporte-se."

    Case AlertMessage.Alert_VersionOutdated
        Header = "Versao Errada"
        body = "A versao do jogo esta errada."
        body2 = "Uma atualizacao é necessária."

    Case AlertMessage.Alert_StringLength
        Header = "Tamanho Inválido"
        body = "Usuário ou senha está pequeno ou grande demais."
        body2 = "Insira um usuário e senha válidos."

    Case AlertMessage.Alert_IllegalName
        Header = "Caracteres Ilegais"
        body = "Usuário ou senha contém caracteres ilegais."
        body2 = "Insira um usuário e senha válidos"

    Case AlertMessage.Alert_Maintenance
        Header = "Manutenção"
        body = "O servidor está em manutenção"
        body2 = "Tente novamente mais tarde"

    Case AlertMessage.Alert_NameTaken
        Header = "Nome Inválido"
        body = "O nome está sendo usado."
        body2 = "Tente um nome diferente."

    Case AlertMessage.Alert_NameLength
        Header = "Nome Inválido"
        body = "O nome é muito grande ou pequeno demais."
        body2 = "Tente um nome diferente."

    Case AlertMessage.Alert_NameIllegal
        Header = "Nome Inválido"
        body = "O nome contém caracteres ilegais."
        body2 = "Use letras e números somente."

    Case AlertMessage.Alert_Database
        Header = "Problema de Conexão"
        body = "Não pode se conectar ao banco de dados."
        body2 = "Tente novamente mais tarde."

    Case AlertMessage.Alert_WrongAccountData
        Header = "Login Inválido"
        body = "Usuario ou senha inválidos."
        body2 = "Tente novamente."

    Case AlertMessage.Alert_AccountIsNotActivated
        Header = "Usuario Desativado"
        body = "O usuário não está ativado."
        body2 = "Ative sua conta e tente novamente."

    Case AlertMessage.Alert_CharacterIsDeleted
        Header = "Personagem Deletado"
        body = "O personagem foi deletado."
        body2 = "Faça o login para continuar jogando."

    Case AlertMessage.Alert_CharacterCreation
        Header = "Criação de Personagens"
        body = "A criação de personagens esta desativada."
        body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_CharacterDelete
        Header = "Exclusão de Personagens"
        body = "A exclusão de personagens está desativada."
        body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_InvalidLevelDelete
        Header = "Exclusão de Personagens"
        body = "Este personagem não pode ser mais excluido."
        body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_DuplicatedLogin
        Header = "Login Duplicado"
        body = "Este usuário já está conectado."
        body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_TryingToLogin
        Header = "Login Duplicado"
        body = "Alguém está tentando acessar está conta."
        body2 = "Mude as suas opções de segurança."

    Case AlertMessage.Alert_InvalidPacket
        Header = "Pacote Inválido"
        body = "Houve um bloqueio nos dados transmitidos."
        body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_InvalidRecipientName
        Header = "Nome Inválido"
        body = "O nome do usuário é inválido."
        body2 = "Verifique a ortografia."

    Case AlertMessage.Alert_InvalidItem
        Header = "Item Inválido"
        body = "O item selecionado é inválido."
        body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_NotEnoughCash
        Header = "Saldo Insuficiente"
        body = "O saldo é insuficiente para realizar a compra."
        body2 = "Faça uma recarga."

    Case AlertMessage.Alert_SuccessPurchase
        Header = "Sucesso na Operação"
        body = "O processo de compra foi realizado com sucesso."
        body2 = "Será enviado pelo correio para o destinatário."

    Case AlertMessage.Alert_NotEnoughCurrency
        Header = "Ouro insuficiente"
        body = "Não há ouro suficiente para a operação."
        body2 = "Entre em contato com o Administrador."

    End Select

    ' set the dialogue up!
    Dialogue Header, body, body2, TypeALERT
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

Sub CheckResolution()
    Dim Resolution As Byte, Width As Long, Height As Long
    ' find the selected resolution
    Resolution = Options.Resolution
    ' reset
    If Resolution = 0 Then
        Resolution = 12
        ' loop through till we find one which fits
        Do Until ScreenFit(Resolution) Or Resolution > RES_COUNT
            ScreenFit Resolution
            Resolution = Resolution + 1
            DoEvents
        Loop

        ' right resolution
        If Resolution > RES_COUNT Then Resolution = RES_COUNT
        Options.Resolution = Resolution
    End If

    ' size the window
    GetResolutionSize Options.Resolution, Width, Height
    Resize Width, Height

    ' save it
    CurResolution = Options.Resolution

    SaveOptions
End Sub

Function ScreenFit(Resolution As Byte) As Boolean
    Dim sWidth As Long, sHeight As Long, Width As Long, Height As Long

    ' exit out early
    If Resolution = 0 Then
        ScreenFit = False
        Exit Function
    End If

    ' get screen size
    sWidth = Screen.Width / Screen.TwipsPerPixelX
    sHeight = Screen.Height / Screen.TwipsPerPixelY

    GetResolutionSize Resolution, Width, Height

    ' check if match
    If Width > sWidth Or Height > sHeight Then
        ScreenFit = False
    Else
        ScreenFit = True
    End If
End Function

Function GetResolutionSize(Resolution As Byte, ByRef Width As Long, ByRef Height As Long)
    Select Case Resolution
    Case 1
        Width = 1920
        Height = 1080
    Case 2
        Width = 1680
        Height = 1050
    Case 3
        Width = 1600
        Height = 900
    Case 4
        Width = 1440
        Height = 900
    Case 5
        Width = 1440
        Height = 1050
    Case 6
        Width = 1366
        Height = 768
    Case 7
        Width = 1360
        Height = 1024
    Case 8
        Width = 1360
        Height = 768
    Case 9
        Width = 1280
        Height = 1024
    Case 10
        Width = 1280
        Height = 800
    Case 11
        Width = 1280
        Height = 768
    Case 12
        Width = 1280
        Height = 720
    Case 13
        Width = 1024
        Height = 768
    Case 14
        Width = 1024
        Height = 576
    Case 15
        Width = 800
        Height = 600
    Case 16
        Width = 800
        Height = 450
    End Select
End Function

Sub Resize(ByVal Width As Long, ByVal Height As Long)
    frmMain.Width = (frmMain.Width \ 15 - frmMain.ScaleWidth + Width) * 15
    frmMain.Height = (frmMain.Height \ 15 - frmMain.ScaleHeight + Height) * 15
    frmMain.Left = (Screen.Width - frmMain.Width) \ 2
    frmMain.Top = (Screen.Height - frmMain.Height) \ 2
    DoEvents
End Sub

Sub ResizeGUI()
    Dim Top As Long

    ' move hotbar
    Windows(GetWindowIndex("winHotbar")).Window.Left = ScreenWidth - 430    ' (ScreenWidth / 2) - (Windows(GetWindowIndex("winHotbar")).Window.Width / 2) ' ScreenWidth - 430
    Windows(GetWindowIndex("winHotbar")).Window.Top = 15    ' ScreenHeight - 50

    ' move chat
    Windows(GetWindowIndex("winChat")).Window.Top = ScreenHeight - 178
    Windows(GetWindowIndex("winChatSmall")).Window.Top = ScreenHeight - 162
    ' move menu
    ' Windows(GetWindowIndex("winMenu")).Window.Left = ScreenWidth - 236
    ' Windows(GetWindowIndex("winMenu")).Window.Top = ScreenHeight - 37
    ' move invitations
    Windows(GetWindowIndex("winInvite_Party")).Window.Left = ScreenWidth - 234
    Windows(GetWindowIndex("winInvite_Party")).Window.Top = ScreenHeight - 100

    ' loop through
    Top = ScreenHeight - 100
    If Windows(GetWindowIndex("winInvite_Party")).Window.Visible Then
        Top = Top - 37
    End If

    Windows(GetWindowIndex("winInvite_Trade")).Window.Left = ScreenWidth - 234
    Windows(GetWindowIndex("winInvite_Trade")).Window.Top = Top
    ' re-size right-click background
    Windows(GetWindowIndex("winRightClickBG")).Window.Width = ScreenWidth
    Windows(GetWindowIndex("winRightClickBG")).Window.Height = ScreenHeight
    ' re-size black background
    Windows(GetWindowIndex("winBlank")).Window.Width = ScreenWidth
    Windows(GetWindowIndex("winBlank")).Window.Height = ScreenHeight
    ' re-size combo background
    Windows(GetWindowIndex("winComboMenuBG")).Window.Width = ScreenWidth
    Windows(GetWindowIndex("winComboMenuBG")).Window.Height = ScreenHeight
    ' centralise windows
    CentraliseWindow GetWindowIndex("winLogin")
    CentraliseWindow GetWindowIndex("winModels")
    CentraliseWindow GetWindowIndex("winLoading")
    CentraliseWindow GetWindowIndex("winDialogue")
    CentraliseWindow GetWindowIndex("winClasses")
    CentraliseWindow GetWindowIndex("winNewChar")
    CentraliseWindow GetWindowIndex("winEscMenu")
    CentraliseWindow GetWindowIndex("winInventory")
    CentraliseWindow GetWindowIndex("winCharacter")
    CentraliseWindow GetWindowIndex("winSkills")
    CentraliseWindow GetWindowIndex("winOptions")
    CentraliseWindow GetWindowIndex("winShop")
    CentraliseWindow GetWindowIndex("winTrade")
    CentraliseWindow GetWindowIndex("winItemUpgrade")
    CentraliseWindow GetWindowIndex("winCraft")
    CentraliseWindow GetWindowIndex("winAchievement")
    CentraliseWindow GetWindowIndex("winLoot")
End Sub

Sub SetResolution()
    Dim Width As Long, Height As Long
    CurResolution = Options.Resolution
    GetResolutionSize CurResolution, Width, Height
    Resize Width, Height
    ScreenWidth = Width
    ScreenHeight = Height
    TileWidth = (Width / 32) - 1
    TileHeight = (Height / 32) - 1
    ScreenX = (TileWidth) * PIC_X
    ScreenY = (TileHeight) * PIC_Y
    ResetGFX
    ResizeGUI
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

Sub SetOptionsScreen()
' clear the combolists
    Erase Windows(GetWindowIndex("winOptions")).Controls(GetControlIndex("winOptions", "cmbRes")).list
    ReDim Windows(GetWindowIndex("winOptions")).Controls(GetControlIndex("winOptions", "cmbRes")).list(0)
    Erase Windows(GetWindowIndex("winOptions")).Controls(GetControlIndex("winOptions", "cmbRender")).list
    ReDim Windows(GetWindowIndex("winOptions")).Controls(GetControlIndex("winOptions", "cmbRender")).list(0)

    ' Resolutions
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1920x1080"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1680x1050"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1600x900"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1440x900"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1440x1050"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1366x768"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1360x1024"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1360x768"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1280x1024"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1280x800"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1280x768"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1280x720"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1024x768"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "1024x576"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "800x600"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRes"), "800x450"

    ' Render Options
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRender"), "Automatic"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRender"), "Hardware"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRender"), "Mixed"
    Combobox_AddItem GetWindowIndex("winOptions"), GetControlIndex("winOptions", "cmbRender"), "Software"

    ' fill the options screen
    With Windows(GetWindowIndex("winOptions"))
        .Controls(GetControlIndex("winOptions", "chkMusic")).Value = Options.Music
        .Controls(GetControlIndex("winOptions", "chkSound")).Value = Options.Sound
        If Options.NoAuto = 1 Then
            .Controls(GetControlIndex("winOptions", "chkAutotiles")).Value = 0
        Else
            .Controls(GetControlIndex("winOptions", "chkAutotiles")).Value = 1
        End If
        .Controls(GetControlIndex("winOptions", "chkFullscreen")).Value = Options.Fullscreen
        .Controls(GetControlIndex("winOptions", "cmbRes")).Value = Options.Resolution
        .Controls(GetControlIndex("winOptions", "cmbRender")).Value = Options.Render + 1
    End With
End Sub

Function HasItem(ByVal ItemNum As Long) As Long
    Dim i As Long

    For i = 1 To MaxInventories
        ' Check to see if the player has the item
        If GetInventoryItemNum(i) = ItemNum Then
            If Item(ItemNum).Stackable > 0 Then
                HasItem = GetInventoryItemValue(i)
            Else
                HasItem = 1
            End If
            Exit Function
        End If
    Next
End Function



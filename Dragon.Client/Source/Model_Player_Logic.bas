Attribute VB_Name = "Model_Player_Logic"
Option Explicit

Function IsTryingToMove() As Boolean
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

Public Sub CheckMovement()
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

    If Not CanCastOnSelf(SkillId) Then
        Exit Sub
    End If

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

Private Function CanCastOnSelf(ByVal SkillId As Long) As Boolean
    Dim FirstEffect As Long
    
    FirstEffect = 1

    With Skill(SkillId)
        If MyTargetIndex = MyIndex And MyTargetType = TargetTypePlayer Then
            If .EffectCount > 0 Then
                If .Effect(FirstEffect).EffectType = SkillEffectType_Damage Then
                    Exit Function
                End If

                If .Effect(FirstEffect).EffectType = SkillEffectType_Dot Then
                    Exit Function
                End If

                If .Effect(FirstEffect).EffectType = SkillEffectType_Immobilize Then
                    Exit Function
                End If

                If .Effect(FirstEffect).EffectType = SkillEffectType_Dispel Then
                    Exit Function
                End If

                If .Effect(FirstEffect).EffectType = SkillEffectType_Dispel Then
                    Exit Function
                End If

                If .Effect(FirstEffect).EffectType = SkillEffectType_Silence Then
                    Exit Function
                End If
            End If
        End If
    End With

    CanCastOnSelf = True

End Function


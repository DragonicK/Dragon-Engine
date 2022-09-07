Attribute VB_Name = "Model_Player_Implementation"
Option Explicit

Public Function CanPlayerMoveInMovement(ByVal Index As Long) As Boolean

    If Player(Index).Attacking = 1 Then
        Dim ModelIndex As Long
        Dim FrameIndex As Long

        ModelIndex = Player(Index).ModelIndex
        FrameIndex = Player(Index).AttackFrameIndex

        CanPlayerMoveInMovement = Models(ModelIndex).Attack.Up.Frames(FrameIndex).CanMove
        Exit Function
    End If

    CanPlayerMoveInMovement = True
End Function

Public Sub ProcessMovement(ByVal Index As Long)
    Dim MovementSpeed As Long

    ' Check if player is walking, and if so process moving them over
    Select Case Player(Index).Moving

    Case MOVING_WALKING:
        MovementSpeed = RUN_SPEED

    Case MOVING_RUNNING:
        MovementSpeed = WALK_SPEED

    Case Else:
        Exit Sub

    End Select

    Select Case GetPlayerDir(Index)

    Case DIR_UP
        Player(Index).yOffset = Player(Index).yOffset - MovementSpeed

        If Player(Index).yOffset < 0 Then Player(Index).yOffset = 0

    Case DIR_DOWN
        Player(Index).yOffset = Player(Index).yOffset + MovementSpeed

        If Player(Index).yOffset > 0 Then Player(Index).yOffset = 0

    Case DIR_LEFT
        Player(Index).xOffset = Player(Index).xOffset - MovementSpeed

        If Player(Index).xOffset < 0 Then Player(Index).xOffset = 0

    Case DIR_RIGHT
        Player(Index).xOffset = Player(Index).xOffset + MovementSpeed

        If Player(Index).xOffset > 0 Then Player(Index).xOffset = 0


    End Select

    ' Check if completed walking over to the next tile
    If Player(Index).Moving > 0 Then
        If GetPlayerDir(Index) = DIR_RIGHT Or GetPlayerDir(Index) = DIR_DOWN Then
            If (Player(Index).xOffset >= 0) And (Player(Index).yOffset >= 0) Then
                Player(Index).Moving = 0

                Call StartIdleMovement(Index)

            End If
        Else
            If (Player(Index).xOffset <= 0) And (Player(Index).yOffset <= 0) Then
                Player(Index).Moving = 0

                Call StartIdleMovement(Index)

            End If
        End If
    End If

End Sub

Public Sub ProcessPlayerMovements(ByVal Index As Long)
    ' Processa o ataque enquanto está em movimento.
    If Player(Index).Attacking = 1 And Player(Index).Moving > 0 Then
        Call ProcessPlayerAttackMovement(Index)
    Else
        If Not Player(Index).Dead Then
            ' Do contrário, processa de outras formas.
            If Player(Index).Attacking = 1 Then
                Call ProcessPlayerAttackMovement(Index)
            ElseIf Player(Index).Moving > 0 Then
                Call ProcessPlayerMoving(Index)
            ElseIf Player(Index).Moving = 0 Then
                Call ProcessPlayerIdleMovement(Index)
            End If
        End If

        If Player(Index).Dead Then
            Call ProcessPlayerDeathMovement(Index)
        End If
    End If
End Sub

Public Sub DrawPlayerLower(ByVal Index As Long)
    Dim X As Long
    Dim Y As Long
    Dim ModelIndex As Long

    ModelIndex = Player(Index).ModelIndex
    
    If ModelIndex = 0 Then
        Exit Sub
    End If

    ' Desenha o ataque enquanto está em movimento.
    If Player(Index).Attacking = 1 And Player(Index).Moving > 0 Then
        Call RenderMovementLower(GetPlayerDir(Index), Player(Index).AttackFrameIndex, Player(Index).SavedX, Player(Index).SavedY, Models(ModelIndex).Attack)
    Else
        ' Desenha os movimentos quando não está morto.
        If Not Player(Index).Dead Then
            ' Do contrário, desenha de outras formas.
            If Player(Index).Attacking = 1 Then
                Call RenderMovementLower(GetPlayerDir(Index), Player(Index).AttackFrameIndex, Player(Index).SavedX, Player(Index).SavedY, Models(ModelIndex).Attack)

            ElseIf Player(Index).Moving > 0 Then
                Call RenderMovementLower(GetPlayerDir(Index), Player(Index).FrameIndex, Player(Index).SavedX, Player(Index).SavedY, Models(ModelIndex).Walking)

            ElseIf Player(Index).Moving = 0 Then
                Call RenderMovementLower(GetPlayerDir(Index), Player(Index).FrameIndex, Player(Index).SavedX, Player(Index).SavedY, Models(ModelIndex).Idle)
            End If
        End If

        If Player(Index).Dead Then
            Call RenderMovementLower(GetPlayerDir(Index), Player(Index).FrameIndex, Player(Index).SavedX, Player(Index).SavedY, Models(ModelIndex).Death)
        End If
    End If
End Sub

Public Sub DrawPlayerUpper(ByVal Index As Long)
    Dim X As Long
    Dim Y As Long
    Dim ModelIndex As Long

    ModelIndex = Player(Index).ModelIndex

    If ModelIndex = 0 Then
        Exit Sub
    End If

    ' Desenha o ataque enquanto está em movimento.
    If Player(Index).Attacking = 1 And Player(Index).Moving > 0 Then
        Call RenderMovementUpper(GetPlayerDir(Index), Player(Index).AttackFrameIndex, Player(Index).SavedX, Player(Index).SavedY, Models(ModelIndex).Attack)
    Else
        ' Desenha os movimentos quando não está morto.
        If Not Player(Index).Dead Then
            ' Do contrário, desenha de outras formas.
            If Player(Index).Attacking = 1 Then
                Call RenderMovementUpper(GetPlayerDir(Index), Player(Index).AttackFrameIndex, Player(Index).SavedX, Player(Index).SavedY, Models(ModelIndex).Attack)

            ElseIf Player(Index).Moving > 0 Then
                Call RenderMovementUpper(GetPlayerDir(Index), Player(Index).FrameIndex, Player(Index).SavedX, Player(Index).SavedY, Models(ModelIndex).Walking)

            ElseIf Player(Index).Moving = 0 Then
                Call RenderMovementUpper(GetPlayerDir(Index), Player(Index).FrameIndex, Player(Index).SavedX, Player(Index).SavedY, Models(ModelIndex).Idle)
            End If
        End If

        If Player(Index).Dead Then
            Call RenderMovementUpper(GetPlayerDir(Index), Player(Index).FrameIndex, Player(Index).SavedX, Player(Index).SavedY, Models(ModelIndex).Death)
        End If

    End If
End Sub

' Verfica se a animação ultrapassou a quantidade de animações.
Public Sub CheckForPlayerFrameOutOfRange(ByVal Index As Long)
    Dim ModelIndex As Long
    Dim Dir As Long

    ModelIndex = Player(Index).ModelIndex

    If ModelIndex = 0 Then
        Exit Sub
    End If

    Dir = GetPlayerDir(Index)

    ' Desenha o ataque enquanto está em movimento.
    If Player(Index).Attacking = 1 And Player(Index).Moving > 0 Then
        If CheckForResetAnim(Dir, Player(Index).AttackFrameIndex, Models(ModelIndex).Attack) Then
            Player(Index).Attacking = 0
            Player(Index).AttackTimer = 0
            Player(Index).FrameTick = 0
        End If
    Else
        ' Do contrário, desenha de outras formas.
        If Player(Index).Attacking = 1 Then
            If CheckForResetAnim(Dir, Player(Index).AttackFrameIndex, Models(ModelIndex).Attack) Then
                Player(Index).Attacking = 0
                Player(Index).AttackTimer = 0
                Player(Index).FrameTick = 0
            End If

        ElseIf Player(Index).Moving > 0 Then
            If CheckForResetAnim(Dir, Player(Index).FrameIndex, Models(ModelIndex).Walking) Then
                Player(Index).FrameTick = 0
            End If

        ElseIf Player(Index).Moving = 0 Then
            If CheckForResetAnim(Dir, Player(Index).FrameIndex, Models(ModelIndex).Idle) Then
                Player(Index).FrameTick = 0
            End If
        End If
    End If
End Sub

Private Sub ProcessPlayerIdleMovement(ByVal Index As Long)
    Dim Anim As Long
    Dim ModelIndex As Long
    Dim Tick As Long
    Dim X As Long, Y As Long

    ' Calculate the X
    Player(Index).SavedX = GetPlayerX(Index) * PIC_X + Player(Index).xOffset - 64
    Player(Index).SavedY = GetPlayerY(Index) * PIC_Y + Player(Index).yOffset - 68

    ModelIndex = Player(Index).ModelIndex
    Tick = Player(Index).FrameTick + Models(ModelIndex).Idle.Up.Time
    Anim = Player(Index).FrameIndex

    If GetTickCount >= Tick Then
        Anim = Anim + 1

        Player(Index).FrameTick = GetTickCount
        Player(Index).FrameIndex = Anim
    End If

    If Anim > Models(ModelIndex).Idle.Up.Count Then
        Player(Index).FrameIndex = 1
    End If

End Sub

Private Sub ProcessPlayerDeathMovement(ByVal Index As Long)
    Dim Anim As Long
    Dim ModelIndex As Long
    Dim Tick As Long
    Dim X As Long, Y As Long

    ' Calculate the X
    Player(Index).SavedX = GetPlayerX(Index) * PIC_X + Player(Index).xOffset - 64
    Player(Index).SavedY = GetPlayerY(Index) * PIC_Y + Player(Index).yOffset - 68

    ModelIndex = Player(Index).ModelIndex
    Tick = Player(Index).FrameTick + Models(ModelIndex).Death.Up.Time
    Anim = Player(Index).FrameIndex

    If GetTickCount >= Tick Then
        Anim = Anim + 1

        Player(Index).FrameTick = GetTickCount
        Player(Index).FrameIndex = Anim
    End If

    If Anim > Models(ModelIndex).Death.Up.Count Then
        Player(Index).FrameIndex = Models(ModelIndex).Death.Up.Count
    End If

End Sub

Private Sub ProcessPlayerMoving(ByVal Index As Long)
    Dim X As Long
    Dim Y As Long
    Dim Anim As Long

    Anim = 1

    Select Case GetPlayerDir(Index)
    Case DIR_UP
        If (Player(Index).yOffset > 16) Then
            Anim = 1
        Else
            Anim = 2
        End If

    Case DIR_DOWN
        If (Player(Index).yOffset < -16) Then
            Anim = 1
        Else
            Anim = 2
        End If

    Case DIR_LEFT
        If (Player(Index).xOffset > 16) Then
            Anim = 1
        Else
            Anim = 2
        End If

    Case DIR_RIGHT
        If (Player(Index).xOffset < -16) Then
            Anim = 1
        Else
            Anim = 2
        End If

    End Select

    ' Calculate the X
    X = GetPlayerX(Index) * PIC_X + Player(Index).xOffset - 64
    Y = GetPlayerY(Index) * PIC_Y + Player(Index).yOffset - 68

    Player(Index).FrameIndex = Anim
    Player(Index).SavedX = X
    Player(Index).SavedY = Y
End Sub

Private Sub ProcessPlayerAttackMovement(ByVal Index As Long)
    Dim Anim As Long
    Dim ModelIndex As Long
    Dim Tick As Long
    Dim X As Long, Y As Long

    ' Calculate the X
    Player(Index).SavedX = GetPlayerX(Index) * PIC_X + Player(Index).xOffset - 64
    Player(Index).SavedY = GetPlayerY(Index) * PIC_Y + Player(Index).yOffset - 68

    ModelIndex = Player(Index).ModelIndex
    Tick = Player(Index).FrameTick + Models(ModelIndex).Attack.Up.Time
    Anim = Player(Index).AttackFrameIndex

    If GetTickCount >= Tick Then
        Anim = Anim + 1

        Player(Index).FrameTick = GetTickCount
        Player(Index).AttackFrameIndex = Anim
    End If

    If Anim > Models(ModelIndex).Attack.Up.Count Then
        Player(Index).AttackFrameIndex = 1

        Player(Index).Attacking = 0
        Player(Index).AttackTimer = 0
        Player(Index).FrameIndex = 1
        Player(Index).FrameTick = 0
    End If

End Sub

' Verfica se a animação ultrapassou a quantidade de animações.
Private Function CheckForResetAnim(ByVal Dir As Byte, ByRef FrameIndex As Long, ByRef Movement As DirectionRec) As Boolean
    CheckForResetAnim = False

    Select Case Dir
    Case DIR_UP
        If FrameIndex > Movement.Up.Count Then
            FrameIndex = 0
            CheckForResetAnim = True
        End If

    Case DIR_RIGHT
        If FrameIndex > Movement.Right.Count Then
            FrameIndex = 0
            CheckForResetAnim = True
        End If

    Case DIR_DOWN
        If FrameIndex > Movement.Down.Count Then
            FrameIndex = 0
            CheckForResetAnim = True
        End If

    Case DIR_LEFT
        If FrameIndex > Movement.Left.Count Then
            FrameIndex = 0
            CheckForResetAnim = True
        End If

    End Select
End Function

Private Sub StartIdleMovement(ByVal Index As Long)
    Player(Index).FrameTick = GetTickCount
    Player(Index).FrameIndex = 1
End Sub

Public Sub StartDeathMovement(ByVal Index As Long)
    Player(Index).FrameTick = GetTickCount
    Player(Index).FrameIndex = 1
End Sub

Public Sub StartAttackMovement(ByVal Index As Long)
    Player(Index).AttackFrameIndex = 1
    Player(Index).FrameTick = GetTickCount
End Sub



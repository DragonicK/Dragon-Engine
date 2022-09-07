Attribute VB_Name = "Model_Npc_Implementation"
Public Sub ProcessNpcMovements(ByVal MapNpcNum As Long)
' Processa o ataque enquanto está em movimento.
    If MapNpc(MapNpcNum).Attacking = 1 And MapNpc(MapNpcNum).Moving > 0 Then
        Call ProcessNpcAttackMovement(MapNpcNum)
    Else
        If Not MapNpc(MapNpcNum).Dying Then
            ' Do contrário, processa de outras formas.
            If MapNpc(MapNpcNum).Attacking = 1 Then
                Call ProcessNpcAttackMovement(MapNpcNum)
            ElseIf MapNpc(MapNpcNum).Moving > 0 Then
                Call ProcessNpcMoving(MapNpcNum)
            ElseIf MapNpc(MapNpcNum).Moving = 0 Then
                Call ProcessNpcIdleMovement(MapNpcNum)
            End If
        ElseIf MapNpc(MapNpcNum).Dying Then
            Call ProcessNpcDeathMovement(MapNpcNum)
        End If
    End If
End Sub

Public Sub DrawNpcLower(ByVal MapNpcNum As Long)
    Dim X As Long
    Dim Y As Long
    Dim ModelIndex As Long

    ' pre-load texture for calculations
    ModelIndex = GetModelIndex(Npc(MapNpc(MapNpcNum).Num).ModelId)

    If ModelIndex = 0 Then
        Exit Sub
    End If

    ' Desenha o ataque enquanto está em movimento.
    If MapNpc(MapNpcNum).Attacking = 1 And MapNpc(MapNpcNum).Moving > 0 Then
        Call RenderMovementLower(MapNpc(MapNpcNum).Dir, MapNpc(MapNpcNum).AttackFrameIndex, MapNpc(MapNpcNum).SavedX, MapNpc(MapNpcNum).SavedY, Models(ModelIndex).Attack)
    Else
        If Not MapNpc(MapNpcNum).Dying Then
            ' Do contrário, desenha de outras formas.
            If MapNpc(MapNpcNum).Attacking = 1 Then
                Call RenderMovementLower(MapNpc(MapNpcNum).Dir, MapNpc(MapNpcNum).AttackFrameIndex, MapNpc(MapNpcNum).SavedX, MapNpc(MapNpcNum).SavedY, Models(ModelIndex).Attack)

            ElseIf MapNpc(MapNpcNum).Moving > 0 Then
                Call RenderMovementLower(MapNpc(MapNpcNum).Dir, MapNpc(MapNpcNum).FrameIndex, MapNpc(MapNpcNum).SavedX, MapNpc(MapNpcNum).SavedY, Models(ModelIndex).Walking)

            ElseIf MapNpc(MapNpcNum).Moving = 0 Then
                Call RenderMovementLower(MapNpc(MapNpcNum).Dir, MapNpc(MapNpcNum).FrameIndex, MapNpc(MapNpcNum).SavedX, MapNpc(MapNpcNum).SavedY, Models(ModelIndex).Idle)
            End If
        ElseIf MapNpc(MapNpcNum).Dying Then
            Call RenderMovementLower(MapNpc(MapNpcNum).Dir, MapNpc(MapNpcNum).FrameIndex, MapNpc(MapNpcNum).SavedX, MapNpc(MapNpcNum).SavedY, Models(ModelIndex).Death)
        End If
    End If
End Sub

Public Sub DrawNpcUpper(ByVal MapNpcNum As Long)
    Dim X As Long
    Dim Y As Long
    Dim ModelIndex As Long

    ModelIndex = GetModelIndex(Npc(MapNpc(MapNpcNum).Num).ModelId)

    If ModelIndex = 0 Then
        Exit Sub
    End If

    ' Desenha o ataque enquanto está em movimento.
    If MapNpc(MapNpcNum).Attacking = 1 And MapNpc(MapNpcNum).Moving > 0 Then
        Call RenderMovementUpper(MapNpc(MapNpcNum).Dir, MapNpc(MapNpcNum).AttackFrameIndex, MapNpc(MapNpcNum).SavedX, MapNpc(MapNpcNum).SavedY, Models(ModelIndex).Attack)
    Else
        If Not MapNpc(MapNpcNum).Dying Then
            ' Do contrário, desenha de outras formas.
            If MapNpc(MapNpcNum).Attacking = 1 Then
                Call RenderMovementUpper(MapNpc(MapNpcNum).Dir, MapNpc(MapNpcNum).AttackFrameIndex, MapNpc(MapNpcNum).SavedX, MapNpc(MapNpcNum).SavedY, Models(ModelIndex).Attack)

            ElseIf MapNpc(MapNpcNum).Moving > 0 Then
                Call RenderMovementUpper(MapNpc(MapNpcNum).Dir, MapNpc(MapNpcNum).FrameIndex, MapNpc(MapNpcNum).SavedX, MapNpc(MapNpcNum).SavedY, Models(ModelIndex).Walking)

            ElseIf MapNpc(MapNpcNum).Moving = 0 Then
                Call RenderMovementUpper(MapNpc(MapNpcNum).Dir, MapNpc(MapNpcNum).FrameIndex, MapNpc(MapNpcNum).SavedX, MapNpc(MapNpcNum).SavedY, Models(ModelIndex).Idle)
            End If
        ElseIf MapNpc(MapNpcNum).Dying Then
            Call RenderMovementUpper(MapNpc(MapNpcNum).Dir, MapNpc(MapNpcNum).FrameIndex, MapNpc(MapNpcNum).SavedX, MapNpc(MapNpcNum).SavedY, Models(ModelIndex).Death)
        End If

    End If
End Sub

Public Sub StartNpcDeathMovement(ByVal MapNpcNum As Long)
    MapNpc(MapNpcNum).FrameTick = GetTickCount
    MapNpc(MapNpcNum).FrameIndex = 1
End Sub

Private Sub ProcessNpcDeathMovement(ByVal MapNpcNum As Long)
    Dim Anim As Long
    Dim ModelIndex As Long
    Dim Tick As Long
    Dim X As Long, Y As Long

    ' Calculate the X
    MapNpc(MapNpcNum).SavedX = MapNpc(MapNpcNum).X * PIC_X + MapNpc(MapNpcNum).xOffset - 64
    MapNpc(MapNpcNum).SavedY = MapNpc(MapNpcNum).Y * PIC_Y + MapNpc(MapNpcNum).yOffset - 68

    ModelIndex = GetModelIndex(Npc(MapNpc(MapNpcNum).Num).ModelId)
    Tick = MapNpc(MapNpcNum).FrameTick + Models(ModelIndex).Death.Up.Time
    Anim = MapNpc(MapNpcNum).FrameIndex

    If GetTickCount >= Tick Then
        Anim = Anim + 1

        MapNpc(MapNpcNum).FrameTick = GetTickCount
        MapNpc(MapNpcNum).FrameIndex = Anim
    End If

    If Anim = Models(ModelIndex).Death.Up.Count - 1 Then
        MapNpc(MapNpcNum).FrameIndex = Models(ModelIndex).Death.Up.Count

        Call AddNpcCorpse(MapNpcNum)

        MapNpc(MapNpcNum).X = 0
        MapNpc(MapNpcNum).Y = 0
        MapNpc(MapNpcNum).Dir = 0
        MapNpc(MapNpcNum).CorpseId = 0
        MapNpc(MapNpcNum).FrameIndex = 1
        MapNpc(MapNpcNum).AttackFrameIndex = 1

        MapNpc(MapNpcNum).Dead = True
        MapNpc(MapNpcNum).Dying = False
    End If

End Sub

Private Sub StartNpcIdleMovement(ByVal MapNpcNum As Long)
    MapNpc(MapNpcNum).FrameTick = GetTickCount
    MapNpc(MapNpcNum).FrameIndex = 1
End Sub

Private Sub ProcessNpcIdleMovement(ByVal MapNpcNum As Long)
    If GetNpcDead(MapNpcNum) Then
        Exit Sub
    End If

    Dim Anim As Long
    Dim ModelIndex As Long
    Dim Tick As Long
    Dim X As Long, Y As Long

    ' Calculate the X
    MapNpc(MapNpcNum).SavedX = MapNpc(MapNpcNum).X * PIC_X + MapNpc(MapNpcNum).xOffset - 64
    MapNpc(MapNpcNum).SavedY = MapNpc(MapNpcNum).Y * PIC_Y + MapNpc(MapNpcNum).yOffset - 68

    ModelIndex = GetModelIndex(Npc(MapNpc(MapNpcNum).Num).ModelId)
    Tick = MapNpc(MapNpcNum).FrameTick + Models(ModelIndex).Idle.Up.Time
    Anim = MapNpc(MapNpcNum).FrameIndex

    If GetTickCount >= Tick Then
        Anim = Anim + 1

        MapNpc(MapNpcNum).FrameTick = GetTickCount
        MapNpc(MapNpcNum).FrameIndex = Anim
    End If

    If Anim > Models(ModelIndex).Idle.Up.Count Then
        Anim = 1
        MapNpc(MapNpcNum).FrameIndex = Anim
    End If

End Sub

Private Sub ProcessNpcAttackMovement(ByVal MapNpcNum As Long)
    Dim Anim As Long
    Dim ModelIndex As Long
    Dim Tick As Long
    Dim X As Long, Y As Long

    ' Calculate the X
    MapNpc(MapNpcNum).SavedX = MapNpc(MapNpcNum).X * PIC_X + MapNpc(MapNpcNum).xOffset - 64
    MapNpc(MapNpcNum).SavedY = MapNpc(MapNpcNum).Y * PIC_Y + MapNpc(MapNpcNum).yOffset - 68

    ModelIndex = GetModelIndex(Npc(MapNpc(MapNpcNum).Num).ModelId)
    Tick = MapNpc(MapNpcNum).FrameTick + Models(ModelIndex).Attack.Up.Time
    Anim = MapNpc(MapNpcNum).AttackFrameIndex

    If GetTickCount >= Tick Then
        Anim = Anim + 1

        MapNpc(MapNpcNum).FrameTick = GetTickCount
        MapNpc(MapNpcNum).AttackFrameIndex = Anim
    End If

    If Anim > Models(ModelIndex).Attack.Up.Count Then
        MapNpc(MapNpcNum).AttackFrameIndex = 1

        MapNpc(MapNpcNum).Attacking = 0
        MapNpc(MapNpcNum).AttackTimer = 0
        MapNpc(MapNpcNum).FrameIndex = 1
        MapNpc(MapNpcNum).FrameTick = 0
    End If

End Sub

Private Sub ProcessNpcMoving(ByVal MapNpcNum As Long)
    If MapNpc(MapNpcNum).Vital(HP) <= 0 Then
        Exit Sub
    End If

    If MapNpc(MapNpcNum).Num = 0 Then
        Exit Sub
    End If

    Dim Anim As Byte
    Dim X As Long
    Dim Y As Long
    Dim ModelIndex As Long

    Anim = 1

    ' pre-load texture for calculations
    ModelIndex = GetModelIndex(Npc(MapNpc(MapNpcNum).Num).ModelId)

    ' If not attacking, walk normally
    Select Case MapNpc(MapNpcNum).Dir
    Case DIR_UP
        If (MapNpc(MapNpcNum).yOffset > 16) Then
            Anim = 1
        Else
            Anim = 2
        End If
    Case DIR_DOWN
        If (MapNpc(MapNpcNum).yOffset < -16) Then
            Anim = 1
        Else
            Anim = 2
        End If

    Case DIR_LEFT
        If (MapNpc(MapNpcNum).xOffset > 16) Then
            Anim = 1
        Else
            Anim = 2
        End If

    Case DIR_RIGHT
        If (MapNpc(MapNpcNum).xOffset < -16) Then
            Anim = 1
        Else
            Anim = 2
        End If

    End Select

    ' Calculate the X
    X = MapNpc(MapNpcNum).X * PIC_X + MapNpc(MapNpcNum).xOffset - 64
    Y = MapNpc(MapNpcNum).Y * PIC_Y + MapNpc(MapNpcNum).yOffset - 68

    MapNpc(MapNpcNum).FrameIndex = Anim
    MapNpc(MapNpcNum).SavedX = X
    MapNpc(MapNpcNum).SavedY = Y

End Sub

Public Sub ProcessNpcMovement(ByVal MapNpcNum As Long)
    Dim MovementSpeed As Long

    ' Check if NPC is walking, and if so process moving them over
    Select Case MapNpc(MapNpcNum).Moving
    Case MOVING_WALKING: MovementSpeed = WALK_SPEED

    Case MOVING_RUNNING: MovementSpeed = RUN_SPEED

    Case Else: Exit Sub

    End Select

    Select Case MapNpc(MapNpcNum).Dir

    Case DIR_UP
        MapNpc(MapNpcNum).yOffset = MapNpc(MapNpcNum).yOffset - MovementSpeed

        If MapNpc(MapNpcNum).yOffset < 0 Then MapNpc(MapNpcNum).yOffset = 0

    Case DIR_DOWN
        MapNpc(MapNpcNum).yOffset = MapNpc(MapNpcNum).yOffset + MovementSpeed

        If MapNpc(MapNpcNum).yOffset > 0 Then MapNpc(MapNpcNum).yOffset = 0

    Case DIR_LEFT
        MapNpc(MapNpcNum).xOffset = MapNpc(MapNpcNum).xOffset - MovementSpeed

        If MapNpc(MapNpcNum).xOffset < 0 Then MapNpc(MapNpcNum).xOffset = 0

    Case DIR_RIGHT
        MapNpc(MapNpcNum).xOffset = MapNpc(MapNpcNum).xOffset + MovementSpeed

        If MapNpc(MapNpcNum).xOffset > 0 Then MapNpc(MapNpcNum).xOffset = 0


    End Select

    ' Check if completed walking over to the next tile
    If MapNpc(MapNpcNum).Moving > 0 Then
        If MapNpc(MapNpcNum).Dir = DIR_RIGHT Or MapNpc(MapNpcNum).Dir = DIR_DOWN Then
            If (MapNpc(MapNpcNum).xOffset >= 0) And (MapNpc(MapNpcNum).yOffset >= 0) Then
                MapNpc(MapNpcNum).Moving = 0

                Call StartNpcIdleMovement(MapNpcNum)

            End If
        Else
            If (MapNpc(MapNpcNum).xOffset <= 0) And (MapNpc(MapNpcNum).yOffset <= 0) Then
                MapNpc(MapNpcNum).Moving = 0

                Call StartNpcIdleMovement(MapNpcNum)

            End If
        End If
    End If

End Sub


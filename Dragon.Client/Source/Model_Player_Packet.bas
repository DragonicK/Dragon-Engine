Attribute VB_Name = "Model_Player_Packet"
Option Explicit

Public Sub SendPlayerMovement()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PPlayerMovement
    Buffer.WriteLong MyIndex
    Buffer.WriteLong GetPlayerDir(MyIndex)
    Buffer.WriteLong Player(MyIndex).Moving
    Buffer.WriteInteger Player(MyIndex).X
    Buffer.WriteInteger Player(MyIndex).Y
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendPlayerDirection()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PPlayerDirection
    Buffer.WriteLong MyIndex
    Buffer.WriteLong GetPlayerDir(MyIndex)
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub HandlePlayerDirection(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
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

Public Sub HandlePlayerMovement(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
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
        
    End Select
End Sub

Public Sub HandlePlayerXY(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
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

Public Sub CheckAttack()
    Dim Buffer As clsBuffer
    Dim AttackSpeed As Long

    If ControlDown Then
        If SpellBuffer > 0 Then Exit Sub    ' currently casting a spell, can't attack
        If StunDuration > 0 Then Exit Sub    ' stunned, can't attack

        ' speed from weapon
        AttackSpeed = 1000

        If Player(MyIndex).AttackTimer + AttackSpeed < GetTickCount Then
            If Player(MyIndex).Attacking = 0 Then

                Call StartAttackMovement(MyIndex)

                With Player(MyIndex)
                    .Attacking = 1
                    .AttackTimer = GetTickCount
                End With

                Set Buffer = New clsBuffer
                Buffer.WriteLong CAttack
                SendData Buffer.ToArray()
                Set Buffer = Nothing
            End If
        End If
    End If

End Sub




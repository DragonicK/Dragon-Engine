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




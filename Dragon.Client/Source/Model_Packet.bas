Attribute VB_Name = "Model_Packet"
Option Explicit


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

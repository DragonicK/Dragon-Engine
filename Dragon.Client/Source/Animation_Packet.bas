Attribute VB_Name = "Animation_Packet"
Option Explicit

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
        .lockIndex = Buffer.ReadLong
        .IsCasting = Buffer.ReadByte
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
            X = GetPlayerX(.lockIndex)
            Y = GetPlayerY(.lockIndex)
        ElseIf .LockType = TargetTypeNpc Then
            X = MapNpc(.lockIndex).X
            Y = MapNpc(.lockIndex).Y
        End If

    End With

    PlayMapSound X, Y, SoundEntity.SeAnimation, AnimInstance(AnimationIndex).Animation
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
            If AnimInstance(i).lockIndex = theIndex Then
                If AnimInstance(i).IsCasting = 1 Then
                    ' clear it
                    ClearAnimInstance i
                End If
            End If
        End If
    Next
End Sub

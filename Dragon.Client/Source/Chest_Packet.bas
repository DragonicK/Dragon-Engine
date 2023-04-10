Attribute VB_Name = "Chest_Packet"
Option Explicit

Public Sub HandleChests(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, Count As Long, i As Long
    Dim Id As Long
    Dim X As Long
    Dim Y As Long
    Dim Sprite As Long
    Dim State As ChestState

    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    Count = Buffer.ReadLong

    For i = 1 To Count
        Id = Buffer.ReadLong
        X = Buffer.ReadLong
        Y = Buffer.ReadLong
        Sprite = Buffer.ReadLong
        State = Buffer.ReadLong

        Call AddChest(Id, X, Y, Sprite, State)
    Next

    Set Buffer = Nothing
End Sub

Public Sub HandleUpdateChestState(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim Id As Long
    Dim State As ChestState

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    Id = Buffer.ReadLong
    State = Buffer.ReadLong

    Set Buffer = Nothing

    Dim i As Long

    For i = 1 To MaximumChests
        With Chests(i)
            If .Id = Id Then
                .State = State

                If .State = ChestState_Empty Then
                    .AlreadyLooted = True
                End If

                Exit For
            End If
        End With
    Next

End Sub

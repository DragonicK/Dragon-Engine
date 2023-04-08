Attribute VB_Name = "Chest_Packet"
Option Explicit

Public Sub HandleChests(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, Count As Long, i As Long
    Dim Id As Long
    Dim X As Long
    Dim Y As Long
    Dim Sprite As Long
    Dim IsLooted As Boolean

    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    Count = Buffer.ReadLong

    For i = 1 To Count
        Id = Buffer.ReadLong
        X = Buffer.ReadLong
        Y = Buffer.ReadLong
        Sprite = Buffer.ReadLong
        IsLooted = Buffer.ReadByte

        Call AddChest(Id, X, Y, Sprite, IsLooted)
    Next

    Set Buffer = Nothing
End Sub

Attribute VB_Name = "Heraldry_Packet"
Option Explicit

Public Sub HandleHeraldry(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long, Count As Long
    Dim Id As Long, Level As Long, AttributeId As Long, UpgradeId As Long
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    Count = Buffer.ReadLong

    For i = 1 To Count
        Index = Buffer.ReadLong
        Id = Buffer.ReadLong
        Level = Buffer.ReadLong
        AttributeId = Buffer.ReadLong
        UpgradeId = Buffer.ReadLong

        If Index > 0 Then
            Call SetHeraldryItemId(Index, Id)
            Call SetHeraldryItemLevel(Index, Level)
            Call SetHeraldryItemAttributeId(Index, AttributeId)
            Call SetHeraldryItemUpgradeId(Index, UpgradeId)
        End If
    Next

    Set Buffer = Nothing

End Sub

Public Sub HandleHeraldryUpdate(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteBytes Data

    Index = Buffer.ReadLong

    If Index > 0 Then
        Call SetHeraldryItemId(Index, Buffer.ReadLong)
        Call SetHeraldryItemLevel(Index, Buffer.ReadLong)
        Call SetHeraldryItemAttributeId(Index, Buffer.ReadLong)
        Call SetHeraldryItemUpgradeId(Index, Buffer.ReadLong)
    End If

    Set Buffer = Nothing
End Sub

Public Sub SendUnequipHeraldry(ByVal Index As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PUnequipHeraldry
    Buffer.WriteLong Index

    SendData Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Public Sub SendEquipHeraldryIndex(ByVal Index As Long, ByVal InvSlot As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PEquipHeraldry
    Buffer.WriteLong Index
    Buffer.WriteLong InvSlot

    SendData Buffer.ToArray()
    Set Buffer = Nothing
End Sub

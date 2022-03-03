Attribute VB_Name = "Inventory_Packet"
Option Explicit

Public Sub SendSwapInventory(ByVal OldSlot As Long, ByVal NewSlot As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PSwapInventory
    Buffer.WriteLong OldSlot
    Buffer.WriteLong NewSlot

    SendData Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Public Sub SendUseItem(ByVal InvNum As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PUseItem
    Buffer.WriteLong InvNum
    
    SendData Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Public Sub SendDestroyItem(ByVal InvNum As Long)
    If InWarehouse Then
        Exit Sub
    End If

    If InShop > 0 Then
        Exit Sub
    End If

    ' do basic checks
    If InvNum < 1 Or InvNum > MaxInventory Then
        Exit Sub
    End If

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PDestroyItem
    Buffer.WriteLong InvNum
    
    SendData Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Public Sub HandleInventory(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long, Count As Long
    Dim Id As Long, Value As Long, Level As Long, Bound As Long, AttributeId As Long, UpgradeId As Long
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    MaxInventories = Buffer.ReadLong
    Count = Buffer.ReadLong

    For i = 1 To Count
        Index = Buffer.ReadLong
        Id = Buffer.ReadLong
        Value = Buffer.ReadLong
        Level = Buffer.ReadLong
        Bound = Buffer.ReadByte
        AttributeId = Buffer.ReadLong
        UpgradeId = Buffer.ReadLong
        
        If Index > 0 Then
            Call SetInventoryItemNum(Index, Id)
            Call SetInventoryItemValue(Index, Value)
            Call SetInventoryItemLevel(Index, Level)
            Call SetInventoryItemBound(Index, Bound)
            Call SetInventoryItemAttributeId(Index, AttributeId)
            Call SetInventoryItemUpgradeId(Index, UpgradeId)
        End If
    Next

    Buffer.Flush
    
    Set Buffer = Nothing

    SelectCraftRecipe
    CountRequiredItemToUpgrade

End Sub

Public Sub HandleInventoryUpdate(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Index = Buffer.ReadLong

    If Index > 0 Then
        Call SetInventoryItemNum(Index, Buffer.ReadLong)
        Call SetInventoryItemValue(Index, Buffer.ReadLong)
        Call SetInventoryItemLevel(Index, Buffer.ReadLong)
        Call SetInventoryItemBound(Index, Buffer.ReadByte)
        Call SetInventoryItemAttributeId(Index, Buffer.ReadLong)
        Call SetInventoryItemUpgradeId(Index, Buffer.ReadLong)
    End If

    Buffer.Flush
    
    Set Buffer = Nothing

    SelectCraftRecipe
    CountRequiredItemToUpgrade

End Sub



Attribute VB_Name = "Warehouse_Packet"
Option Explicit

Public Sub SendDepositItem(ByVal InvSlot As Long, ByVal Amount As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PDepositItem
    Buffer.WriteLong InvSlot
    Buffer.WriteLong Amount
    
    SendGameMessage Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendWithdrawItem(ByVal WarehouseSlot As Long, ByVal Amount As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PWithdrawItem
    Buffer.WriteLong WarehouseSlot
    Buffer.WriteLong Amount
    
    SendGameMessage Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendCloseWarehouse()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PWarehouseClose
    
    SendGameMessage Buffer.ToArray()
    
    Set Buffer = Nothing
    
    InWarehouse = False
End Sub

Public Sub SendSwapWarehouseSlots(ByVal OldSlot As Long, ByVal NewSlot As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PSwapWarehouse
    Buffer.WriteLong OldSlot
    Buffer.WriteLong NewSlot
    
    SendGameMessage Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub HandleWarehouseUpdate(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Index = Buffer.ReadLong

    If Index > 0 Then
        Call SetWarehouseItemNum(Index, Buffer.ReadLong)
        Call SetWarehouseItemValue(Index, Buffer.ReadLong)
        Call SetWarehouseItemLevel(Index, Buffer.ReadLong)
        Call SetWarehouseItemBound(Index, Buffer.ReadByte)
        Call SetWarehouseItemAttributeId(Index, Buffer.ReadLong)
        Call SetWarehouseItemUpgradeId(Index, Buffer.ReadLong)
    End If
    
    Set Buffer = Nothing
End Sub
Public Sub HandleWarehouse(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long, Count As Long
    Dim Id As Long, Value As Long, Level As Long, Bound As Long, AttributeId As Long, UpgradeId As Long
    
    Dim Buffer As clsBuffer
    
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    
    ClearWarehouse
    
    ' Read Maximum Player Warehouse
    Buffer.ReadLong
    Count = Buffer.ReadLong

    For i = 1 To Count
        Index = Buffer.ReadLong
        Id = Buffer.ReadLong
        Value = Buffer.ReadLong
        Level = Buffer.ReadLong
        Bound = Buffer.ReadByte
        AttributeId = Buffer.ReadLong
        UpgradeId = Buffer.ReadLong

        ' If there's some item in this slot.
        If Index > 0 Then
            Call SetWarehouseItemNum(Index, Id)
            Call SetWarehouseItemValue(Index, Value)
            Call SetWarehouseItemLevel(Index, Level)
            Call SetWarehouseItemBound(Index, Bound)
            Call SetWarehouseItemAttributeId(Index, AttributeId)
            Call SetWarehouseItemUpgradeId(Index, UpgradeId)
        End If
    Next

    Set Buffer = Nothing

End Sub

Public Sub HandleWarehouseOpen(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    InWarehouse = True

    If Not IsWarehouseVisible() Then
        Call ShowWarehouse
    End If
End Sub

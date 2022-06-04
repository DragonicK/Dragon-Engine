Attribute VB_Name = "Inventory_Implementation"
Option Explicit

Private Inventory(1 To MaxInventory) As InventoryRec

Public Type InventoryRec
    Num As Long
    Value As Long
    Level As Long
    Bound As InventoryBoundType
    AttributeId As Long
    UpgradeId As Long
End Type

Public Sub ClearInventories()
    Dim i As Long

    For i = 1 To MaxInventory
        Call ClearInventory(i)
    Next

End Sub

Public Sub ClearInventory(ByVal Index As Long)
    Inventory(Index).Num = 0
    Inventory(Index).Value = 0
    Inventory(Index).Level = 0
    Inventory(Index).Bound = InventoryBoundType_None
    Inventory(Index).AttributeId = 0
    Inventory(Index).UpgradeId = 0
End Sub

Public Function GetInventoryItemNum(ByVal InvSlot As Long) As Long
    GetInventoryItemNum = Inventory(InvSlot).Num
End Function
Public Sub SetInventoryItemNum(ByVal InvSlot As Long, ByVal ItemNum As Long)
    Inventory(InvSlot).Num = ItemNum
End Sub

Function GetInventoryItemValue(ByVal InvSlot As Long) As Long
    GetInventoryItemValue = Inventory(InvSlot).Value
End Function
Public Sub SetInventoryItemValue(ByVal InvSlot As Long, ByVal ItemValue As Long)
    Inventory(InvSlot).Value = ItemValue
End Sub

Public Function GetInventoryItemLevel(ByVal InvSlot As Long) As Long
    GetInventoryItemLevel = Inventory(InvSlot).Level
End Function
Public Sub SetInventoryItemLevel(ByVal InvSlot As Long, ByVal ItemLevel As Long)
    Inventory(InvSlot).Level = ItemLevel
End Sub

Public Function GetInventoryItemBound(ByVal InvSlot As Long) As InventoryBoundType
    GetInventoryItemBound = Inventory(InvSlot).Bound
End Function
Public Sub SetInventoryItemBound(ByVal InvSlot As Long, ByVal Bound As InventoryBoundType)
    Inventory(InvSlot).Bound = Bound
End Sub

Public Function GetInventoryItemAttributeId(ByVal InvSlot As Long) As Long
    GetInventoryItemAttributeId = Inventory(InvSlot).AttributeId
End Function
Public Sub SetInventoryItemAttributeId(ByVal InvSlot As Long, ByVal AttributeId As Long)
    Inventory(InvSlot).AttributeId = AttributeId
End Sub

Public Function GetInventoryItemUpgradeId(ByVal InvSlot As Long) As Long
    GetInventoryItemUpgradeId = Inventory(InvSlot).UpgradeId
End Function
Public Sub SetInventoryItemUpgradeId(ByVal InvSlot As Long, ByVal UpgradeId As Long)
    Inventory(InvSlot).UpgradeId = UpgradeId
End Sub


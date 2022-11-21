Attribute VB_Name = "Warehouse_Implementation"
Option Explicit

Public InWarehouse As Boolean
Public Const MaxWarehouse As Long = 50
Private Warehouse As WarehouseRec

Private Type WarehouseRec
    Item(1 To MaxWarehouse) As InventoryRec
End Type

Public Sub ClearWarehouse()
    Call ZeroMemory(ByVal VarPtr(Warehouse), LenB(Warehouse))
End Sub
Public Function GetWarehouseItemNum(ByVal Slot As Long) As Long
    GetWarehouseItemNum = Warehouse.Item(Slot).Num
End Function
Public Sub SetWarehouseItemNum(ByVal Slot As Long, ByVal ItemNum As Long)
     Warehouse.Item(Slot).Num = ItemNum
End Sub
Public Function GetWarehouseItemValue(ByVal Slot As Long) As Long
    GetWarehouseItemValue = Warehouse.Item(Slot).Value
End Function
Public Sub SetWarehouseItemValue(ByVal Slot As Long, ByVal ItemValue As Long)
     Warehouse.Item(Slot).Value = ItemValue
End Sub
Public Function GetWarehouseItemLevel(ByVal Slot As Long) As Long
    GetWarehouseItemLevel = Warehouse.Item(Slot).Level
End Function
Public Sub SetWarehouseItemLevel(ByVal Slot As Long, ByVal ItemLevel As Long)
    Warehouse.Item(Slot).Level = ItemLevel
End Sub
Public Function GetWarehouseItemBound(ByVal Slot As Long) As InventoryBoundType
    GetWarehouseItemBound = Warehouse.Item(Slot).Bound
End Function
Public Sub SetWarehouseItemBound(ByVal Slot As Long, ByVal ItemBound As InventoryBoundType)
    Warehouse.Item(Slot).Bound = ItemBound
End Sub
Public Function GetWarehouseItemAttributeId(ByVal Slot As Long) As Long
    GetWarehouseItemAttributeId = Warehouse.Item(Slot).AttributeId
End Function
Public Sub SetWarehouseItemAttributeId(ByVal Slot As Long, ByVal AttributeId As Long)
    Warehouse.Item(Slot).AttributeId = AttributeId
End Sub
Public Function GetWarehouseItemUpgradeId(ByVal Slot As Long) As Long
    GetWarehouseItemUpgradeId = Warehouse.Item(Slot).UpgradeId
End Function
Public Sub SetWarehouseItemUpgradeId(ByVal Slot As Long, ByVal UpgradeId As Long)
    Warehouse.Item(Slot).UpgradeId = UpgradeId
End Sub

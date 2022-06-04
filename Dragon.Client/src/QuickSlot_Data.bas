Attribute VB_Name = "QuickSlot_Data"
Public Const MaximumQuickSlot As Byte = 10

' QuickSlot
Public QuickSlot(1 To MaximumQuickSlot) As QuickSlotRec

Public Type QuickSlotRec
    Slot As Long
    SType As Byte
    Value As Long
End Type

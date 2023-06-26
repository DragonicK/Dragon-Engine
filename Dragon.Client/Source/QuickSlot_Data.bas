Attribute VB_Name = "QuickSlot_Data"
Option Explicit

Public Const MaximumQuickSlot As Byte = 10

' QuickSlot
Public QuickSlot(1 To MaximumQuickSlot) As QuickSlotRec

Public Type QuickSlotRec
    Slot As Long
    SType As Byte
    Value As Long
End Type

Public Sub ClearPlayerQuickSlots()
    Dim i As Long

    For i = 1 To MaximumQuickSlot
        QuickSlot(i).Slot = 0
        QuickSlot(i).SType = 0
        QuickSlot(i).Value = 0
    Next

End Sub

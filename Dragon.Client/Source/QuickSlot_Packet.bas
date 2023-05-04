Attribute VB_Name = "QuickSlot_Packet"
Option Explicit

Public Sub SendQuickSlotChange(ByVal SType As Long, ByVal InventoryIndex As Long, ByVal Index As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PQuickSlotChange
    Buffer.WriteLong SType
    Buffer.WriteLong InventoryIndex
    Buffer.WriteLong Index

    SendGameMessage Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub SendSwapQuickSlot(ByVal OldSlot As Long, ByVal NewSlot As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PSwapQuickSlot
    Buffer.WriteLong OldSlot
    Buffer.WriteLong NewSlot
    
    SendGameMessage Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendQuickSlotUse(ByVal Slot As Long)
    Dim Buffer As clsBuffer, X As Long

    ' check if spell
    If QuickSlot(Slot).SType = 2 Then    ' spell
        For X = 1 To MaxPlayerSkill
            ' is the spell matching the hotbar?
            If PlayerSkill(X).Id = QuickSlot(Slot).Slot Then
                ' found it, cast it
                CastSpell X
                Exit Sub
            End If
        Next

        ' can't find the spell, exit out
        Exit Sub
    End If

    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PQuickSlotUse
    Buffer.WriteLong Slot
    
    SendGameMessage Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub HandleQuickSlot(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim i As Long, Count As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Count = Buffer.ReadLong

    For i = 1 To Count
        Index = Buffer.ReadLong

        If Index > 0 Then
            QuickSlot(Index).SType = Buffer.ReadLong
            QuickSlot(Index).Slot = Buffer.ReadLong
        End If
    Next

    Set Buffer = Nothing
End Sub

Public Sub HandleQuickSlotUpdate(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim i As Long, Count As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Index = Buffer.ReadLong

    If Index > 0 Then
        QuickSlot(Index).SType = Buffer.ReadLong
        QuickSlot(Index).Slot = Buffer.ReadLong
    End If

    Set Buffer = Nothing
End Sub

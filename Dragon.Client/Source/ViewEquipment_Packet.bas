Attribute VB_Name = "ViewEquipment_Packet"
Option Explicit

Public Sub SendRequestViewEquipment(ByVal Name As String)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PRequestViewEquipment
    Buffer.WriteString Name
    
    SendGameMessage Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub HandleViewEquipment(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, i As Long, Count As Long
    Dim Equip As PlayerEquipmentRec

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Call ClearViewEquipment

    ViewPlayerName = Buffer.ReadString
    ViewPlayerLevel = Buffer.ReadLong
    ViewPlayerClassCode = Buffer.ReadLong

    Count = Buffer.ReadLong

    For i = 1 To Count
        Index = Buffer.ReadLong

        Equip.Num = Buffer.ReadLong
        Equip.Level = Buffer.ReadLong
        Equip.Bound = Buffer.ReadByte
        Equip.AttributeId = Buffer.ReadLong
        Equip.UpgradeId = Buffer.ReadLong

        Call SetViewEquipment(Equip, Index)
    Next

    Set Buffer = Nothing

    Call UpdateViewTwoHandedWeaponInformation

    Call UpdateViewEquipmentWindow
    Call ShowViewEquipment

End Sub


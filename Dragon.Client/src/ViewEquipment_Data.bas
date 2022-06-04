Attribute VB_Name = "ViewEquipment_Data"
Option Explicit

Public ViewPlayerName As String
Public ViewPlayerLevel As String
Public ViewPlayerClassCode As Long

Public ViewEquipment(1 To PlayerEquipments.PlayerEquipment_Count - 1) As PlayerEquipmentRec

Public Function GetViewEquipment(ByVal Id As Long) As PlayerEquipmentRec
    GetViewEquipment = ViewEquipment(Id)
End Function

Public Sub SetViewEquipment(ByRef Equip As PlayerEquipmentRec, ByVal EquipmentSlot As PlayerEquipments)
    ViewEquipment(EquipmentSlot).Num = Equip.Num
    ViewEquipment(EquipmentSlot).Level = Equip.Level
    ViewEquipment(EquipmentSlot).Bound = Equip.Bound
    ViewEquipment(EquipmentSlot).AttributeId = Equip.AttributeId
    ViewEquipment(EquipmentSlot).UpgradeId = Equip.UpgradeId
End Sub

Public Sub ClearViewEquipment()
    Dim i As Long

    ViewPlayerName = vbNullString
    ViewPlayerLevel = vbNullString
    ViewPlayerClassCode = 0

    For i = 1 To PlayerEquipments.PlayerEquipment_Count - 1
        ViewEquipment(i).Num = 0
        ViewEquipment(i).Level = 0
        ViewEquipment(i).Bound = 0
        ViewEquipment(i).AttributeId = 0
        ViewEquipment(i).UpgradeId = 0
    Next
End Sub

Public Sub UpdateViewTwoHandedWeaponInformation()
    Dim EquipmentId As Long
    Dim ItemId As Long
    Dim Index As PlayerEquipments

    Dim Data As EquipmentRec

    ItemId = ViewEquipment(PlayerEquipments.EquipWeapon).Num

    If ItemId > 0 And ItemId <= MaximumItems Then
        EquipmentId = Item(ItemId).EquipmentId

        If EquipmentId > 0 And EquipmentId <= MaxEquipments Then
            Data = GetEquipmentData(EquipmentId)

            If Data.Type = Weapon Then
                If Data.HandStyle = HandStyle_TwoHanded Then
                
                    Call SetViewEquipment(ViewEquipment(EquipWeapon), EquipShield)
                    
                End If
            End If
        End If
    End If
End Sub

Attribute VB_Name = "Upgrade_Packet"
Option Explicit

Public Sub SendSelectedItemToUpgrade(ByVal InventoryIndex As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PSelectedItemToUpgrade
    Buffer.WriteLong InventoryIndex

    SendGameMessage Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendStartUpgrade(ByVal InventoryIndex As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PStartUpgrade
    Buffer.WriteLong InventoryIndex

    SendGameMessage Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub HandleUpgradeData(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim i As Long
    Dim Count As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    UpgradeInventoryIndex = Buffer.ReadLong

    Upgrade.Break = Buffer.ReadLong
    Upgrade.Success = Buffer.ReadLong
    Upgrade.Reduce = Buffer.ReadLong
    Upgrade.ReduceValue = Buffer.ReadLong
    Upgrade.CostValue = Buffer.ReadLong

    For i = 1 To MaximumUpgradeRequirements
        Upgrade.Requirement(i).Id = 0
        Upgrade.Requirement(i).Amount = 0
        Upgrade.Requirement(i).InventoryAmount = 0
        Upgrade.Requirement(i).InventoryIndex = 0
    Next

    Count = Buffer.ReadLong

    For i = 1 To Count
        If i <= MaximumUpgradeRequirements Then
            Upgrade.Requirement(i).Id = Buffer.ReadLong
            Upgrade.Requirement(i).Amount = Buffer.ReadLong
        End If
    Next

    Call UpdateUpgradeWindow
    Call CountRequiredItemToUpgrade

    Set Buffer = Nothing
End Sub

Public Sub HandleUpgradeOpen(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)

    If Not IsUpgradeVisible() Then
        Call ShowUpgrade
    End If
End Sub


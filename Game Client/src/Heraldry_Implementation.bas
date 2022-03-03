Attribute VB_Name = "Heraldry_Implementation"
Option Explicit

Public Const MaxPlayerHeraldry As Long = 17
Private PlayerHeraldry(1 To MaxPlayerHeraldry) As PlayerHeraldryRec

Public Type PlayerHeraldryRec
    ItemId As Long
    ItemLevel As Integer
    ItemAttributeId As Long
    ItemUpgradeId As Long
    X As Long
    Y As Long
End Type

Public Function GetHeraldry(ByVal Index As Long) As InventoryRec
    GetHeraldry.Num = PlayerHeraldry(Index).ItemId
    GetHeraldry.Value = 1
    GetHeraldry.Level = PlayerHeraldry(Index).ItemLevel
    GetHeraldry.Bound = InventoryBoundType_None
    GetHeraldry.AttributeId = PlayerHeraldry(Index).ItemAttributeId
    GetHeraldry.UpgradeId = PlayerHeraldry(Index).ItemUpgradeId
End Function

Public Function GetHeraldryItemId(ByVal Index As Long) As Long
    GetHeraldryItemId = PlayerHeraldry(Index).ItemId
End Function
Public Sub SetHeraldryItemId(ByVal Index As Long, ByVal ItemId As Long)
    PlayerHeraldry(Index).ItemId = ItemId
End Sub

Public Function GetHeraldryItemLevel(ByVal Index As Long) As Long
    GetHeraldryItemLevel = PlayerHeraldry(Index).ItemLevel
End Function
Public Sub SetHeraldryItemLevel(ByVal Index As Long, ByVal ItemLevel As Long)
    PlayerHeraldry(Index).ItemLevel = ItemLevel
End Sub

Public Function GetHeraldryItemAttributeId(ByVal Index As Long) As Long
    GetHeraldryItemAttributeId = PlayerHeraldry(Index).ItemAttributeId
End Function
Public Sub SetHeraldryItemAttributeId(ByVal Index As Long, ByVal AttributeId As Long)
    PlayerHeraldry(Index).ItemAttributeId = AttributeId
End Sub

Public Function GetHeraldryItemUpgradeId(ByVal Index As Long) As Long
    GetHeraldryItemUpgradeId = PlayerHeraldry(Index).ItemUpgradeId
End Function
Public Sub SetHeraldryItemUpgradeId(ByVal Index As Long, ByVal AttributeId As Long)
    PlayerHeraldry(Index).ItemUpgradeId = AttributeId
End Sub

Public Function GetHeraldryPositionX(ByVal Index As Long) As Long
    GetHeraldryPositionX = PlayerHeraldry(Index).X
End Function
Public Sub SetHeraldryPositionX(ByVal Index As Long, ByVal X As Long)
    PlayerHeraldry(Index).X = X
End Sub

Public Function GetHeraldryPositionY(ByVal Index As Long) As Long
    GetHeraldryPositionY = PlayerHeraldry(Index).Y
End Function
Public Sub SetHeraldryPositionY(ByVal Index As Long, ByVal Y As Long)
    PlayerHeraldry(Index).Y = Y
End Sub


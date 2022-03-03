Attribute VB_Name = "Shop_Implementation"
Option Explicit

Public InShop As Boolean

Public Const MaxShopItems As Long = 35

Public Shop As ShopRec

Private Type ShopItemRec
    ItemNum As Long
    ItemValue As Long
    ItemLevel As Long
    ItemBound As InventoryBoundType
    AttributeId As Long
    UpgradeId As Long
    CostValue As Long
    CurrencyId As Long
End Type
 
Private Type ShopRec
    Name As String
    Item(1 To MaxShopItems) As ShopItemRec
End Type

Public Sub ClearShop()
    Call ZeroMemory(ByVal VarPtr(Shop), LenB(Shop))
    Shop.Name = vbNullString
End Sub

Public Function GetShopItemNum(ByVal Index As Long) As Long
    GetShopItemNum = Shop.Item(Index).ItemNum
End Function
Public Sub SetShopItemNum(ByVal Index As Long, ByVal ItemNum As Long)
    Shop.Item(Index).ItemNum = ItemNum
End Sub

Public Function GetShopItemValue(ByVal Index As Long) As Long
    GetShopItemValue = Shop.Item(Index).ItemValue
End Function
Public Sub SetShopItemValue(ByVal Index As Long, ByVal ItemValue As Long)
    Shop.Item(Index).ItemValue = ItemValue
End Sub

Public Function GetShopItemLevel(ByVal Index As Long) As Long
    GetShopItemLevel = Shop.Item(Index).ItemLevel
End Function
Public Sub SetShopItemLevel(ByVal Index As Long, ByVal ItemLevel As Long)
    Shop.Item(Index).ItemLevel = ItemLevel
End Sub

Public Function GetShopItemBound(ByVal Index As Long) As InventoryBoundType
    GetShopItemBound = Shop.Item(Index).ItemBound
End Function
Public Sub SetShopItemBound(ByVal Index As Long, ByVal ItemBound As InventoryBoundType)
    Shop.Item(Index).ItemBound = ItemBound
End Sub

Public Function GetShopItemAttributeId(ByVal Index As Long) As Long
    GetShopItemAttributeId = Shop.Item(Index).AttributeId
End Function
Public Sub SetShopItemAttributeId(ByVal Index As Long, ByVal AttributeId As Long)
    Shop.Item(Index).AttributeId = AttributeId
End Sub

Public Function GetShopItemUpgradeId(ByVal Index As Long) As Long
    GetShopItemUpgradeId = Shop.Item(Index).UpgradeId
End Function
Public Sub SetShopItemUpgradeId(ByVal Index As Long, ByVal UpgradeId As Long)
    Shop.Item(Index).UpgradeId = UpgradeId
End Sub

Public Function GetShopItemCost(ByVal Index As Long) As Long
    GetShopItemCost = Shop.Item(Index).CostValue
End Function
Public Sub SetShopItemCost(ByVal Index As Long, ByVal CostValue As Long)
    Shop.Item(Index).CostValue = CostValue
End Sub
Public Function GetShopItemCurrencyId(ByVal Index As Long) As Long
    GetShopItemCurrencyId = Shop.Item(Index).CurrencyId
End Function
Public Sub SetShopItemCurrencyId(ByVal Index As Long, ByVal CurrencyId As Long)
    Shop.Item(Index).CurrencyId = CurrencyId
End Sub


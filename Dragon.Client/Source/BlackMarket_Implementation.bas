Attribute VB_Name = "BlackMarket_Implementation"
Option Explicit

Public Const BlackMarketListCount As Long = 6

Public BlackMarketItems(1 To BlackMarketListCount) As BlackMarketItemRec
' Moeda corrente do usuário.
Public PlayerCash As Long

Public Enum BlackMarketCategory
    BlackMarketCategory_Promo
    BlackMarketCategory_Boost
    BlackMarketCategory_Supply
    BlackMarketCategory_Consumable
    BlackMarketCategory_Service
    BlackMarketCategory_Package
    BlackMarketCategory_Pet
End Enum

Private Type BlackMarketItemRec
    Id As Long
    Price As Long
    Category As BlackMarketCategory
    ItemNum As Long
    ItemLevel As Long
    ItemValue As Long
    ItemBound As Byte
    AttributeId As Long
    UpgradeId As Long
    PurchaseLimit As Long
    GiftEnabled As Boolean
    MaximumStack As Long
End Type

Public Sub ClearBlackMarketItems()
    Dim i As Long

    For i = 1 To BlackMarketListCount
        Call ZeroMemory(ByVal VarPtr(BlackMarketItems(i)), LenB(BlackMarketItems(i)))
    Next
    
End Sub

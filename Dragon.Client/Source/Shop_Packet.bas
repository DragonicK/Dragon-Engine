Attribute VB_Name = "Shop_Packet"
Option Explicit

Public Sub SendBuyItem(ByVal ShopSlot As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PShopBuyItem
    Buffer.WriteLong ShopSlot
    Buffer.WriteLong 1
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendSellItem(ByVal InvSlot As Long, ByVal Amount As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PShopSellItem
    Buffer.WriteLong InvSlot
    Buffer.WriteLong Amount
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendCloseShop()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PShopClose
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub HandleShopOpen(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim MaxShopItems As Long, i As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    ClearShop

    Shop.Name = Buffer.ReadString
    MaxShopItems = Buffer.ReadLong

    If MaxShopItems > 0 Then
        For i = 1 To MaxShopItems
            Call SetShopItemNum(i, Buffer.ReadLong)
            Call SetShopItemValue(i, Buffer.ReadLong)
            Call SetShopItemLevel(i, Buffer.ReadLong)
            Call SetShopItemBound(i, Buffer.ReadByte)
            Call SetShopItemAttributeId(i, Buffer.ReadLong)
            Call SetShopItemUpgradeId(i, Buffer.ReadLong)
            Call SetShopItemCurrencyId(i, Buffer.ReadLong)
            Call SetShopItemCost(i, Buffer.ReadLong)
        Next
    End If

    Call OpenShop

    Set Buffer = Nothing
End Sub


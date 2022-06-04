Attribute VB_Name = "BlackMarket_Packet"
Option Explicit

Public Sub SendRequestBlackMarketItems(ByVal Category As BlackMarketCategory, ByVal Page As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PRequestBlackMarketItems
    Buffer.WriteLong Category
    Buffer.WriteLong Page

    SendData Buffer.ToArray
    Set Buffer = Nothing
End Sub

Public Sub SendPurchaseCashShopItem(ByVal CashItemId As Long, ByVal Quantity As Long, ByVal Target As String)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PPurchaseBlackMarketItem
    Buffer.WriteLong CashItemId
    Buffer.WriteLong Quantity
    Buffer.WriteString Target
    
    SendData Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Public Sub HandleCash(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data
    PlayerCash = Buffer.ReadLong

    Set Buffer = Nothing

    Call UpdateCashControlValue
End Sub

Public Sub HandleBlackMarketItems(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, Count As Long, i As Long
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    MaximumItemCategoryPage = Buffer.ReadLong
    
    Count = Buffer.ReadLong

    Call ClearBlackMarketItems

    If MaximumItemCategoryPage > 0 Then

        For i = 1 To Count
            With BlackMarketItems(i)
                .Id = Buffer.ReadLong
                .Price = Buffer.ReadLong
                .Category = Buffer.ReadLong
                .ItemNum = Buffer.ReadLong
                .ItemLevel = Buffer.ReadLong
                .ItemBound = Buffer.ReadByte
                .AttributeId = Buffer.ReadLong
                .UpgradeId = Buffer.ReadLong
                .MaximumStack = Buffer.ReadLong
                .PurchaseLimit = Buffer.ReadLong
                .GiftEnabled = CBool(Buffer.ReadByte)
            End With
        Next
        
    Else
        MaximumItemCategoryPage = 0
        CurrentItemCategoryPage = 0
    End If

    Call UpdateBlackMarketItemList
    
    CanMoveToNextPage = True
End Sub

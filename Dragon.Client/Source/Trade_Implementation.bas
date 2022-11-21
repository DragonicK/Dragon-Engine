Attribute VB_Name = "Trade_Implementation"
Option Explicit

Public InTrade As Boolean

Public Const MaxTradeItems As Long = 15

Public Enum TradeStatusType
    TradeStatusType_Failed
    TradeStatusType_Waiting
    TradeStatusType_Confirmed
    TradeStatusType_Accepted
    TradeStatusType_ServerCheck
    TradeStatusType_Concluded
End Enum

' Nome do personagem que iniciou a negociação.
Public IsMeWhoStartedTrade As Boolean

Private MyTradeInventory(1 To MaxTradeItems) As MyTradeInventoryRec
Private OtherTradeItems(1 To MaxTradeItems) As TradeInvRec

Private TradeStatus As TradeStatusType

Private MyCurrencyValue As Long
Private OtherCurrencyValue As Long

Private MyTradeStatus As TradeStatusType
Private OtherTradeStatus As TradeStatusType

Private Type MyTradeInventoryRec
    InventoryNum As Long
    ItemValue As Long
End Type

Private Type TradeInvRec
    Num As Long
    Value As Long
    Level As Long
    Bound As InventoryBoundType
    AttributeId As Long
    UpgradeId As Long
End Type

Public Function GetMyTradeStatus() As Long
    GetMyTradeStatus = MyTradeStatus
End Function
Public Sub SetMyTradeStatus(ByVal Status As TradeStatusType)
    MyTradeStatus = Status
End Sub

Public Function GetOtherTradeStatus() As Long
    GetOtherTradeStatus = OtherTradeStatus
End Function
Public Sub SetOtherTradeStatus(ByVal Status As TradeStatusType)
    OtherTradeStatus = Status
End Sub

Public Function GetTradeStatus() As Long
    GetTradeStatus = TradeStatus
End Function
Public Sub SetTradeStatus(ByVal Status As TradeStatusType)
    TradeStatus = Status
End Sub

Public Function GetMyTradeCurrency() As Long
    GetMyTradeCurrency = MyCurrencyValue
End Function
Public Sub SetMyTradeCurrency(ByVal Value As Long)
    MyCurrencyValue = Value
End Sub

Public Function GetOtherTradeCurrency() As Long
    GetOtherTradeCurrency = OtherCurrencyValue
End Function
Public Sub SetOtherTradeCurrency(ByVal Value As Long)
    OtherCurrencyValue = Value
End Sub

' My Inventory
Public Function GetMyTradeInventoryIndex(ByVal Slot As Long) As Long
    GetMyTradeInventoryIndex = MyTradeInventory(Slot).InventoryNum
End Function
Public Sub SetMyTradeInventoryIndex(ByVal Slot As Long, ByVal InventoryNum As Long)
    MyTradeInventory(Slot).InventoryNum = InventoryNum
End Sub
Public Function GetMyTradeInventoryItemValue(ByVal Slot As Long) As Long
    GetMyTradeInventoryItemValue = MyTradeInventory(Slot).ItemValue
End Function
Public Sub SetMyTradeInventoryItemValue(ByVal Slot As Long, ByVal ItemValue As Long)
     MyTradeInventory(Slot).ItemValue = ItemValue
End Sub

' Other Items
Public Function GetOtherTradeItemNum(ByVal Slot As Long) As Long
    GetOtherTradeItemNum = OtherTradeItems(Slot).Num
End Function
Public Sub SetOtherTradeItemNum(ByVal Slot As Long, ByVal ItemNum As Long)
    OtherTradeItems(Slot).Num = ItemNum
End Sub

Public Function GetOtherTradeItemValue(ByVal Slot As Long) As Long
    GetOtherTradeItemValue = OtherTradeItems(Slot).Value
End Function
Public Sub SetOtherTradeItemValue(ByVal Slot As Long, ByVal ItemValue As Long)
    OtherTradeItems(Slot).Value = ItemValue
End Sub

Public Function GetOtherTradeItemLevel(ByVal Slot As Long) As Long
    GetOtherTradeItemLevel = OtherTradeItems(Slot).Level
End Function
Public Sub SetOtherTradeItemLevel(ByVal Slot As Long, ByVal ItemLevel As Long)
    OtherTradeItems(Slot).Level = ItemLevel
End Sub

Public Function GetOtherTradeItemBound(ByVal Slot As Long) As InventoryBoundType
    GetOtherTradeItemBound = OtherTradeItems(Slot).Bound
End Function
Public Sub SetOtherTradeItemBound(ByVal Slot As Long, ByVal ItemBound As InventoryBoundType)
    OtherTradeItems(Slot).Bound = ItemBound
End Sub

Public Function GetOtherTradeItemAttributeId(ByVal Slot As Long) As Long
    GetOtherTradeItemAttributeId = OtherTradeItems(Slot).AttributeId
End Function
Public Sub SetOtherTradeItemAttributeId(ByVal Slot As Long, ByVal ItemAttributeId As Long)
    OtherTradeItems(Slot).AttributeId = ItemAttributeId
End Sub

Public Function GetOtherTradeItemUpgradeId(ByVal Slot As Long) As Long
    GetOtherTradeItemUpgradeId = OtherTradeItems(Slot).UpgradeId
End Function
Public Sub SetOtherTradeItemUpgradeId(ByVal Slot As Long, ByVal ItemUpgradeId As Long)
    OtherTradeItems(Slot).UpgradeId = ItemUpgradeId
End Sub

' Clear
Public Sub ClearMyTradeInventory(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(MyTradeInventory(Index)), LenB(MyTradeInventory(Index)))
End Sub
Public Sub ClearOtherTradeItems(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(OtherTradeItems(Index)), LenB(OtherTradeItems(Index)))
End Sub
  
Public Function GetTradeStatusColor(ByVal Status As TradeStatusType) As Long
    Select Case Status
    Case TradeStatusType.TradeStatusType_Waiting
        GetTradeStatusColor = BrightRed
    Case TradeStatusType.TradeStatusType_Confirmed
        GetTradeStatusColor = Yellow
    Case TradeStatusType.TradeStatusType_Accepted
        GetTradeStatusColor = BrightGreen
    Case TradeStatusType.TradeStatusType_ServerCheck
        GetTradeStatusColor = Coral
    End Select
End Function

Public Function GetTradeStatusText(ByVal Status As TradeStatusType) As String
    Select Case Status
    Case TradeStatusType.TradeStatusType_Waiting
        GetTradeStatusText = "Aguardando confirmação"
    Case TradeStatusType.TradeStatusType_Confirmed
        GetTradeStatusText = "Confirmado"
    Case TradeStatusType.TradeStatusType_Accepted
        GetTradeStatusText = "Aceitado"
    Case TradeStatusType.TradeStatusType_ServerCheck
        GetTradeStatusText = "Verificação do Servidor"
    End Select
End Function

Attribute VB_Name = "Trade_Packet"
Option Explicit

Public Sub SendTradeRequest(ByVal CharacterId As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PTradeRequest
    Buffer.WriteLong CharacterId
    
    SendData Buffer.ToArray
    Set Buffer = Nothing
End Sub

Public Sub SendAcceptTradeRequest()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PAcceptTradeRequest
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendDeclineTradeRequest()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PDeclineTradeRequest
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendSelectTradeCurrency(ByVal Value As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PTradeCurrency
    Buffer.WriteLong Value
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendAcceptTrade()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PAcceptTrade
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendConfirmTrade()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PConfirmTrade
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendDeclineTrade()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PDeclineTrade
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendTradeItem(ByVal InvSlot As Long, ByVal Amount As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PTradeItem
    Buffer.WriteLong InvSlot
    Buffer.WriteLong Amount
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendUntradeItem(ByVal InvSlot As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PUntradeItem
    Buffer.WriteLong InvSlot
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub HandleTradeMyInventory(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, TradeSlot As Long
    
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    
    TradeSlot = Buffer.ReadLong

    Call SetMyTradeInventoryIndex(TradeSlot, Buffer.ReadLong)

    Call SetMyTradeInventoryItemValue(TradeSlot, Buffer.ReadLong)
 
    Set Buffer = Nothing

End Sub

Public Sub HandleTradeOtherItems(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, TradeSlot As Long
    
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    TradeSlot = Buffer.ReadLong

    Call SetOtherTradeItemNum(TradeSlot, Buffer.ReadLong)
    Call SetOtherTradeItemValue(TradeSlot, Buffer.ReadLong)
    Call SetOtherTradeItemLevel(TradeSlot, Buffer.ReadLong)
    Call SetOtherTradeItemBound(TradeSlot, Buffer.ReadByte)
    Call SetOtherTradeItemAttributeId(TradeSlot, Buffer.ReadLong)
    Call SetOtherTradeItemUpgradeId(TradeSlot, Buffer.ReadLong)

    Set Buffer = Nothing

End Sub

Public Sub HandleTradeState(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    
    Call SetTradeStatus(Buffer.ReadLong)
    Call SetMyTradeStatus(Buffer.ReadLong)
    Call SetOtherTradeStatus(Buffer.ReadLong)
 
    Set Buffer = Nothing
    
    Call UpdateTradeStatus
End Sub

Public Sub HandleTradeCurrency(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    Call SetMyTradeCurrency(Buffer.ReadLong)
    Call SetOtherTradeCurrency(Buffer.ReadLong)

    Set Buffer = Nothing
    
    Call UpdateTradeCurrency
End Sub

Public Sub HandleCloseTrade(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Call HideTrade
    Call CloseDialogue

    HideWindow GetWindowIndex("winInvite_Trade")

    InTrade = False
    CanMoveNow = True
    CanSwapInvItems = True
End Sub

Public Sub HandleOpenTrade(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, MyText As String, OtherText As String, CharacterName As String

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    MyText = Buffer.ReadString
    OtherText = Buffer.ReadString
    CharacterName = Buffer.ReadString

    Set Buffer = Nothing

    If CharacterName = GetPlayerName(MyIndex) Then
        IsMeWhoStartedTrade = True
    Else
        IsMeWhoStartedTrade = False
    End If

    ClearTrade

    InTrade = True
    CanSwapInvItems = False

    Call SetTradeStatus(TradeStatusType_Waiting)
    Call SetOtherTradeStatus(TradeStatusType_Waiting)
    Call SetMyTradeStatus(TradeStatusType_Waiting)
    
    Call UpdateTradeText(MyText, OtherText)
    Call ShowTrade
End Sub

Public Sub HandleTradeInvite(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, Character As String

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    Character = Buffer.ReadString
    Set Buffer = Nothing

    HideWindow GetWindowIndex("winInvite_Trade")
    
    Windows(GetWindowIndex("winInvite_party")).Window.Top = ScreenHeight - 120
    Dialogue "Convite para negociação", Character & " convidou você para uma negociação.", "Voce gostaria de aceitar?", TypeTRADE, StyleYESNO
End Sub







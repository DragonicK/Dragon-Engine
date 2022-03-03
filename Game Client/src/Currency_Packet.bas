Attribute VB_Name = "Currency_Packet"
Option Explicit

Public Sub HandleCurrency(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, i As Long
    Dim Count As Long
    Dim Id As Long
    Dim Value As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    ClearPlayerCurrency

    Count = Buffer.ReadLong

    For i = 1 To Count
        Id = Buffer.ReadLong
        Value = Buffer.ReadLong

        Call SetPlayerCurrency(Id, Value)
    Next

    Set Buffer = Nothing

    Call UpdateInventoryCurrency
    Call UpdateGoldInUpgradeWindow

End Sub

Public Sub HandleCurrencyUpdate(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim Id As Long
    Dim Value As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    Id = Buffer.ReadLong
    Value = Buffer.ReadLong
    
    Set Buffer = Nothing

    Call SetPlayerCurrency(Id, Value)

    Call UpdateInventoryCurrency
    Call UpdateGoldInUpgradeWindow
End Sub

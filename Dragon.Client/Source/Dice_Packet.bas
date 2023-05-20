Attribute VB_Name = "Dice_Packet"
Option Explicit

Public Sub SendRollDiceResult(ByVal Rolled As Boolean)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong PRollDiceResult
    
    If Rolled Then Buffer.WriteByte 2
    If Not Rolled Then Buffer.WriteByte 1

    SendGameMessage Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub HandleCloseRollDice(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Call SetRollDiceVisibility(False)
    Call HideWindow(GetWindowIndex("winDice"))
End Sub

Public Sub HandleRollDiceItem(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim RemainingTime As Long, ReceivedItem As RolledItemRec
    Dim WindowIndex As Long
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    RemainingTime = Buffer.ReadLong

    ReceivedItem.Id = Buffer.ReadLong
    ReceivedItem.Value = Buffer.ReadLong
    ReceivedItem.Level = Buffer.ReadLong
    ReceivedItem.Bound = Buffer.ReadByte
    ReceivedItem.UpgradeId = Buffer.ReadLong
    ReceivedItem.AttributeId = Buffer.ReadLong

    Set Buffer = Nothing

    WindowIndex = GetWindowIndex("winDice")

    Call UpdateRolledItem(RemainingTime, ReceivedItem)
    Call SetRollDiceVisibility(True)

    ShowWindow WindowIndex

End Sub


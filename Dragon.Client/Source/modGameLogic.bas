Attribute VB_Name = "modGameLogic"
Option Explicit

Public Function ConvertCurrency(ByVal Amount As Long) As String

    If Int(Amount) < 10000 Then
        ConvertCurrency = Amount
    ElseIf Int(Amount) < 999999 Then
        ConvertCurrency = Int(Amount / 1000) & "k"
    ElseIf Int(Amount) < 999999999 Then
        ConvertCurrency = Int(Amount / 1000000) & "m"
    Else
        ConvertCurrency = Int(Amount / 1000000000) & "b"
    End If

End Function


Public Sub SetBarWidth(ByRef MaxWidth As Long, ByRef Width As Long)
    Dim barDifference As Long

    If MaxWidth < Width Then
        ' find out the amount to increase per loop
        barDifference = ((Width - MaxWidth) / 100) * 10

        ' if it's less than 1 then default to 1
        If barDifference < 1 Then barDifference = 1
        ' set the width
        Width = Width - barDifference
    ElseIf MaxWidth > Width Then
        ' find out the amount to increase per loop
        barDifference = ((MaxWidth - Width) / 100) * 10

        ' if it's less than 1 then default to 1
        If barDifference < 1 Then barDifference = 1
        ' set the width
        Width = Width + barDifference
    End If

End Sub

Public Function Clamp(ByVal Value As Long, ByVal Min As Long, ByVal Max As Long) As Long
    Clamp = Value

    If Value < Min Then Clamp = Min
    If Value > Max Then Clamp = Max
End Function

Public Sub ShowEqDesc(X As Long, Y As Long, eqNum As Long)
    If eqNum <= 0 Or eqNum > PlayerEquipments.PlayerEquipment_Count - 1 Then
        Exit Sub
    End If

    If GetPlayerEquipmentId(eqNum) Then
        Dim Inventory As InventoryRec
        
        Inventory.Num = GetPlayerEquipmentId(eqNum)
        Inventory.Value = 1
        Inventory.Level = GetPlayerEquipmentLevel(eqNum)
        Inventory.Bound = GetPlayerEquipmentBound(eqNum)
        Inventory.AttributeId = GetPlayerEquipmentAttributeId(eqNum)
        Inventory.UpgradeId = GetPlayerEquipmentUpgradeId(eqNum)

        ShowItemDesc X, Y, Inventory
    End If
End Sub

Public Sub AddDescInfo(Text As String, Optional Colour As Long = White)
    Dim Count As Long
    Count = UBound(DescText)
    ReDim Preserve DescText(1 To Count + 1) As TextColourRec
    DescText(Count + 1).Text = Text
    DescText(Count + 1).Colour = Colour
End Sub

Sub ShowPlayerMenu(Index As Long, X As Long, Y As Long)
    PlayerMenuIndex = Index
    If PlayerMenuIndex = 0 Then Exit Sub
    Windows(GetWindowIndex("winPlayerMenu")).Window.Left = X - 5
    Windows(GetWindowIndex("winPlayerMenu")).Window.Top = Y - 5
    Windows(GetWindowIndex("winPlayerMenu")).Controls(GetControlIndex("winPlayerMenu", "btnName")).Text = Trim$(GetPlayerName(PlayerMenuIndex))
    ShowWindow GetWindowIndex("winRightClickBG")
    ShowWindow GetWindowIndex("winPlayerMenu"), , False
End Sub

Public Function AryCount(ByRef Ary() As Byte) As Long
    On Error Resume Next

    AryCount = UBound(Ary) + 1
End Function

Public Function ByteToInt(ByVal B1 As Long, ByVal B2 As Long) As Long
    ByteToInt = B1 * 256 + B2
End Function

Sub UpdateStats_UI()
' set the bar labels
    With Windows(GetWindowIndex("winBars"))
        .Controls(GetControlIndex("winBars", "lblHP")).Text = GetPlayerVital(MyIndex, HP) & "/" & GetPlayerMaxVital(MyIndex, HP)
        .Controls(GetControlIndex("winBars", "lblMP")).Text = GetPlayerVital(MyIndex, MP) & "/" & GetPlayerMaxVital(MyIndex, MP)
        .Controls(GetControlIndex("winBars", "lblEXP")).Text = GetPlayerExp() & "/" & TNL
    End With

    ' update character screen
End Sub





Attribute VB_Name = "Dialogue_Implementation"
Option Explicit

Public Sub CloseDialogue()
    Call ClearDialogue

    HideWindow GetWindowIndex("winBlank")
    HideWindow GetWindowIndex("winDialogue")
End Sub

Public Sub ClearDialogue()
    With Dialogue
        .Index = 0
        .DataLong = 0
        .DataString = vbNullString
        .Style = 0
    End With
End Sub

Public Sub ShowDialogue(ByVal Header As String, ByVal Body As String, ByVal Body2 As String, ByVal Index As Long, Optional ByVal Style As DialogueStyle = 1, Optional ByVal Data1 As Long = 0)
    Dim WindowIndex As Long

    ' exit out if we've already got a dialogue open
    If Dialogue.Index > 0 Then Exit Sub

    WindowIndex = GetWindowIndex("winDialogue")

    ' set buttons
    With Windows(WindowIndex)
        If Style = DialogueStyleYesNo Then
            .Controls(GetControlIndex("winDialogue", "btnYes")).Visible = True
            .Controls(GetControlIndex("winDialogue", "btnNo")).Visible = True
            .Controls(GetControlIndex("winDialogue", "btnOkay")).Visible = False
            .Controls(GetControlIndex("winDialogue", "txtInput")).Visible = False
            .Controls(GetControlIndex("winDialogue", "lblBody_2")).Visible = True
        ElseIf Style = DialogueStyleOkay Then
            .Controls(GetControlIndex("winDialogue", "btnYes")).Visible = False
            .Controls(GetControlIndex("winDialogue", "btnNo")).Visible = False
            .Controls(GetControlIndex("winDialogue", "btnOkay")).Visible = True
            .Controls(GetControlIndex("winDialogue", "txtInput")).Visible = False
            .Controls(GetControlIndex("winDialogue", "lblBody_2")).Visible = True
        ElseIf Style = DialogueStyleInput Then
            .Controls(GetControlIndex("winDialogue", "btnYes")).Visible = False
            .Controls(GetControlIndex("winDialogue", "btnNo")).Visible = False
            .Controls(GetControlIndex("winDialogue", "btnOkay")).Visible = True
            .Controls(GetControlIndex("winDialogue", "txtInput")).Visible = True
            .Controls(GetControlIndex("winDialogue", "lblBody_2")).Visible = False
        End If

        ' set labels
        .Controls(GetControlIndex("winDialogue", "lblHeader")).Text = Header
        .Controls(GetControlIndex("winDialogue", "lblBody_1")).Text = Body
        .Controls(GetControlIndex("winDialogue", "lblBody_2")).Text = Body2
        .Controls(GetControlIndex("winDialogue", "txtInput")).Text = vbNullString
    End With

    Windows(WindowIndex).activeControl = GetControlIndex("winDialogue", "txtInput")

    ' set it all up
    Dialogue.Index = Index
    Dialogue.DataLong = Data1
    Dialogue.Style = Style

    ' make the windows visible
    ShowWindow GetWindowIndex("winBlank"), True
    ShowWindow GetWindowIndex("winDialogue"), True
End Sub

Public Sub DialogueHandler(ByVal Index As Long)
    Dim Value As Long, DiaInput As String

    Dim Buffer As New clsBuffer
    Set Buffer = New clsBuffer

    DiaInput = Trim$(Windows(GetWindowIndex("winDialogue")).Controls(GetControlIndex("winDialogue", "txtInput")).Text)

    ' find out which button
    If Index = DialogueButtonOkay Then

        Select Case Dialogue.Index

        Case DialogueTypeTradeAmount
            Value = Val(DiaInput)

            If Value > 0 Then
                Call SendTradeItem(Dialogue.DataLong, Value)
            End If

        Case DialogueTypeDepositItem
            Value = Val(DiaInput)

            If Value > 0 Then
                Call SendDepositItem(Dialogue.DataLong, Value)
            End If

        Case DialogueTypeWithdrawItem
            Value = Val(DiaInput)

            If Value > 0 Then
                Call SendWithdrawItem(Dialogue.DataLong, Value)
            End If

        Case DialogueTypeTradeGoldAmount
            Value = Val(DiaInput)

            If Value > 0 Then
                Call SendSelectTradeCurrency(Value)
            End If

        Case DialogueTypeSellAmount
            Value = Val(DiaInput)

            If Value > 0 Then
                Call SendSellItem(Dialogue.DataLong, Value)
            End If

        Case DialogueTypeAddMailCurrency
            Call AddSendMailCurrencyValue(Val(DiaInput))

        Case DialogueTypeAddMailAmount
            Call AddSendMailItemValue(Val(DiaInput))

        End Select

    ElseIf Index = DialogueButtonYes Then
        Select Case Dialogue.Index

        Case DialogueTypeDestroyItem
            Call SendDestroyItem(Dialogue.DataLong)

        Case DialogueTypeTrade
            Call SendAcceptTradeRequest

        Case DialogueTypeParty
            Call SendAcceptParty

        Case DialogueTypeLootItem
            ' send the packet
            'Player(MyIndex).MapGetTimer = GetTickCount
            'Buffer.WriteLong CMapGetItem
            'SendData Buffer.ToArray()

        Case DialogueTypeDeleteChar
            ' send the deletion
            Call SendDeleteChar(Dialogue.DataLong)

        Case DialogueTypeDeleteMail
            Call SendDeleteMail

        End Select

    ElseIf Index = DialogueButtonNo Then
        Select Case Dialogue.Index

        Case DialogueTypeTrade
            Call SendDeclineTradeRequest

        Case DialogueTypeParty
            Call SendDeclineParty

        End Select
    End If

    CloseDialogue
End Sub


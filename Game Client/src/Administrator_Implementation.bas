Attribute VB_Name = "Administrator_Implementation"
Option Explicit

Private Const CommandName As Byte = 0
Private Const CommandTarget As Byte = 1

Public Enum SuperiorCommands
    None
    SetLevel
    SetAttribute
    SetAttributePoint
    SetModel
    SetClass
    SetService
    SetActiveTitle
    SetCurrency
    AddCurrency
    AddItem
    AddAttributePoint
    AddTitle
    AddBuff
    WarpTo
    WarpToMe
    WarpMeTo
    WarpToLocation
    Kick
    SuperiorCommands_Count
End Enum

Public Sub ParseAdministratorCommands(ByRef chatText As String)
    Dim Commands() As String

    On Error GoTo ErrorLabel

    Commands = Split(chatText, Space$(1))

    If Len(Commands(CommandTarget)) = 0 Then
        chatText = vbNullString
        Exit Sub
    End If

    Select Case Commands(CommandName)
    Case ".SetLevel"
        Call SendCommandLevel(Commands, SetLevel)
    Case ".SetAttribute"
        Call SendCommandStat(Commands, SetAttribute)
    Case ".SetAttributePoint"
        Call SendCommandStatPoints(Commands, SetAttributePoint)
    Case ".SetModel"
        Call SendSetModel(Commands)
    Case ".SetClass"
        Call SendSetClass(Commands)
    Case ".SetService"
        Call SendSetService(Commands)
    Case ".SetActiveTitle"
        Call SendSetActiveTitle(Commands)
    Case ".SetCurrency"
        Call SendCommandCurrency(Commands, SetCurrency)
    Case ".AddCurrency"
        Call SendCommandCurrency(Commands, AddCurrency)
    Case ".AddItem"
        Call SendAddItem(Commands, AddItem)
    Case ".AddAttributePoint"
        Call SendCommandStatPoints(Commands, AddAttributePoint)
    Case ".AddTitle"
        Call SendCommandTitle(Commands, AddTitle)
    Case ".AddEffect"
        Call SendAddEffect(Commands)
    End Select

    chatText = vbNullString
    Exit Sub

ErrorLabel:
    chatText = vbNullString
End Sub

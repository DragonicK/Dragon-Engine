Attribute VB_Name = "Dialogue_Data"
Option Explicit

Public Const DialogueButtonOkay As Long = 1
Public Const DialogueButtonYes As Long = 2
Public Const DialogueButtonNo As Long = 3

Public Dialogue As DialogueInfo

Public Type DialogueInfo
    Index As Long
    DataLong As Long
    DataString As String
    Style As DialogueStyle
End Type

Public Enum DialogueStyle
    DialogueStyleOkay = 1
    DialogueStyleYesNo
    DialogueStyleInput
End Enum

Public Enum DialogueType
    DialogueTypeName = 0
    DialogueTypeTrade
    DialogueTypeParty
    DialogueTypeLootItem
    DialogueTypeAlert
    DialogueTypeDeleteChar
    DialogueTypeDestroyItem
    DialogueTypeTradeAmount
    DialogueTypeDepositItem
    DialogueTypeWithdrawItem
    DialogueTypeTradeGoldAmount
    DialogueTypeSellAmount
    DialogueTypeDeleteMail
    DialogueTypeAddMailCurrency
    DialogueTypeAddMailAmount
End Enum



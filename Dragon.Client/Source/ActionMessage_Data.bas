Attribute VB_Name = "ActionMessage_Data"
Option Explicit

Public ActionMsg(1 To MAX_BYTE) As ActionMsgRec

' Scrolling action message constants
Public Const ACTIONMsgSTATIC As Long = 0
Public Const ACTIONMsgSCROLL As Long = 1
Public Const ACTIONMsgSCREEN As Long = 2

Public Const ACTION_MSG_FONT_DAMAGE As Long = 0
Public Const ACTION_MSG_FONT_ALPHABET As Long = 1

Private Type ActionMsgRec
    Message As String
    Created As Long
    Type As Long
    Color As Long
    Scroll As Long
    X As Long
    Y As Long
    Timer As Long
    Alpha As Long
    FontType As Byte
    TickCount As Long
End Type

Public Type ChatBubbleRec
    Msg As String
    Colour As Long
    Target As Long
    TargetType As Byte
    Timer As Long
    Active As Boolean
End Type

Public Type TextColourRec
    Text As String
    Colour As Long
End Type


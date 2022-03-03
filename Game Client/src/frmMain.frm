VERSION 5.00
Object = "{248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0"; "mswinsck.ocx"
Begin VB.Form frmMain 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Twilight Online"
   ClientHeight    =   10800
   ClientLeft      =   45
   ClientTop       =   375
   ClientWidth     =   19200
   BeginProperty Font 
      Name            =   "Verdana"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "frmMain.frx":0000
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   720
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   1280
   StartUpPosition =   2  'CenterScreen
   Visible         =   0   'False
   Begin MSWinsockLib.Winsock Socket 
      Left            =   120
      Top             =   240
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Form_Unload(Cancel As Integer)
    DestroyGame
End Sub

Private Sub Form_KeyPress(KeyAscii As Integer)
    Call HandleKeyPresses(KeyAscii)
End Sub

Private Sub Form_KeyUp(KeyCode As Integer, Shift As Integer)
' handles screenshot mode
    If KeyCode = vbKeyF11 Then
        If GetPlayerAccess(MyIndex) > 0 Then
            ScreenshotMode = Not ScreenshotMode
        End If
    End If

    If KeyCode = vbKeyF1 Then
        'If MyIndex <= 0 Then Exit Sub

        '  If GetPlayerAccess(MyIndex) >= ACCESS_ADMINISTRATOR Then
        '     frmAdminPanel.Show
        frmFont_Editor.Show
        '  End If
    End If

    ' Handles form
    If KeyCode = vbKeyInsert Then
        If frmMain.BorderStyle = 0 Then
            frmMain.BorderStyle = 1
        Else
            frmMain.BorderStyle = 0
        End If

        frmMain.caption = frmMain.caption
    End If

End Sub

Private Sub Form_DblClick()
    HandleGuiMouse entStates.DblClick
End Sub

' Winsock event
Private Sub Socket_DataArrival(ByVal bytesTotal As Long)
    If IsConnected Then
        Call IncomingData(bytesTotal)
    End If
End Sub

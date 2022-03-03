VERSION 5.00
Begin VB.Form frmFont_Editor 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Form1"
   ClientHeight    =   4875
   ClientLeft      =   45
   ClientTop       =   390
   ClientWidth     =   8820
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4875
   ScaleWidth      =   8820
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton CommandSave 
      Caption         =   "Save"
      Height          =   375
      Left            =   3120
      TabIndex        =   5
      Top             =   4320
      Width           =   2175
   End
   Begin VB.Frame Frame1 
      Caption         =   "Frame1"
      Height          =   2055
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   3135
      Begin VB.CommandButton Command1 
         Caption         =   "Command1"
         Height          =   375
         Left            =   240
         TabIndex        =   7
         Top             =   360
         Width           =   615
      End
      Begin VB.TextBox TextAscii 
         Height          =   285
         Left            =   1440
         TabIndex        =   3
         Text            =   "0"
         Top             =   450
         Width           =   735
      End
      Begin VB.HScrollBar ScrollWidth 
         Height          =   255
         Left            =   240
         Max             =   25
         Min             =   -25
         TabIndex        =   1
         Top             =   1440
         Width           =   2535
      End
      Begin VB.Label LabelCharacter 
         Alignment       =   2  'Center
         Caption         =   "Character: 0"
         Height          =   255
         Left            =   120
         TabIndex        =   6
         Top             =   840
         Width           =   2850
      End
      Begin VB.Label Label2 
         Caption         =   "ASCII:"
         Height          =   255
         Left            =   960
         TabIndex        =   4
         Top             =   480
         Width           =   495
      End
      Begin VB.Label LabelWidth 
         Caption         =   "Width: 0"
         Height          =   255
         Left            =   240
         TabIndex        =   2
         Top             =   1200
         Width           =   2175
      End
   End
End
Attribute VB_Name = "frmFont_Editor"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private FontNum As Long
Private KeyAscii As Byte

Private Sub Command1_Click()
    KeyAscii = AscW(TextAscii.Text)
    TextAscii.Text = KeyAscii
End Sub

'Private Sub Command1_Click()
' Dim i As Long, Value As Long

'  For i = 0 To 255
' Value = modText.font(Fonts.OpenSans_Regular).HeaderInfo.CharWidth(i)

' modText.font(Fonts.OpenSans_Italic).HeaderInfo.CharWidth(i) = Value
'  modText.font(Fonts.OpenSans_Bold).HeaderInfo.CharWidth(i) = Value
'  modText.font(Fonts.OpenSans_Effect).HeaderInfo.CharWidth(i) = Value
' Next
'End Sub

Private Sub CommandSave_Click()
    Call SaveFontHeader(OpenSans_Damage, "OpenSansDamage.dat")
    Call SaveFontHeader(OpenSans_Bold, "OpenSansBold_11.dat")
    Call SaveFontHeader(OpenSans_Italic, "OpenSansItalic_11.dat")
    Call SaveFontHeader(OpenSans_Effect, "OpenSansEffect_11.dat")
End Sub

Private Sub Form_Load()
    FontNum = 1
End Sub

Private Sub ScrollWidth_Change()
    If KeyAscii >= 0 And KeyAscii <= 255 Then
    
        Font_Implementation.Font(Fonts.OpenSans_Damage).HeaderInfo.CharWidth(KeyAscii) = ScrollWidth.Value
        Font_Implementation.Font(Fonts.OpenSans_Regular).HeaderInfo.CharWidth(KeyAscii) = ScrollWidth.Value
        Font_Implementation.Font(Fonts.OpenSans_Italic).HeaderInfo.CharWidth(KeyAscii) = ScrollWidth.Value
        Font_Implementation.Font(Fonts.OpenSans_Bold).HeaderInfo.CharWidth(KeyAscii) = ScrollWidth.Value
        Font_Implementation.Font(Fonts.OpenSans_Effect).HeaderInfo.CharWidth(KeyAscii) = ScrollWidth.Value

        LabelWidth.caption = "Width: " & Font_Implementation.Font(Fonts.OpenSans_Damage).HeaderInfo.CharWidth(KeyAscii)
    End If
End Sub

Private Sub TextAscii_Change()
    Dim Value As Long

    Value = Val(TextAscii.Text)

    If Value >= 0 And Value <= 255 Then
        KeyAscii = Value

        LabelCharacter.caption = "Character: " & ChrW(KeyAscii)

        LabelWidth.caption = "Width: " & Font_Implementation.Font(Fonts.OpenSans_Regular).HeaderInfo.CharWidth(KeyAscii)
        ScrollWidth.Value = Font_Implementation.Font(Fonts.OpenSans_Regular).HeaderInfo.CharWidth(KeyAscii)
    End If

End Sub

Private Sub TextFont_Validate(Cancel As Boolean)
  FontNum = Val(TextFont.Text)
   TextFont.Text = FontNum
End Sub



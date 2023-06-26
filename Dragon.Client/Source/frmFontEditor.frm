VERSION 5.00
Begin VB.Form frmFontEditor 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Font Editor"
   ClientHeight    =   10230
   ClientLeft      =   45
   ClientTop       =   390
   ClientWidth     =   6555
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   10230
   ScaleWidth      =   6555
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame GroupFonts 
      Caption         =   "Game Fonts"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1815
      Left            =   120
      TabIndex        =   1
      Top             =   120
      Width           =   6255
      Begin VB.CommandButton ButtonReload 
         Caption         =   "Reload"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   330
         Left            =   480
         TabIndex        =   54
         Top             =   1200
         Width           =   5175
      End
      Begin VB.CommandButton ButtonSave 
         Caption         =   "Save"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   330
         Left            =   480
         TabIndex        =   53
         Top             =   780
         Width           =   5175
      End
      Begin VB.ComboBox ComboFonts 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Left            =   480
         Style           =   2  'Dropdown List
         TabIndex        =   2
         Top             =   360
         Width           =   5175
      End
   End
   Begin VB.Frame GroupLetters 
      Caption         =   "Font Editor"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   8175
      Left            =   120
      TabIndex        =   0
      Top             =   2040
      Width           =   6255
      Begin VB.HScrollBar ScrollPage 
         Height          =   255
         Left            =   720
         Max             =   16
         Min             =   1
         TabIndex        =   51
         Top             =   7680
         Value           =   1
         Width           =   4815
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   16
         Left            =   3120
         TabIndex        =   48
         Top             =   6240
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   16
            Left            =   1560
            Max             =   32
            TabIndex        =   49
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   16
            Left            =   240
            TabIndex        =   50
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   15
         Left            =   3120
         TabIndex        =   45
         Top             =   5400
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   15
            Left            =   1560
            Max             =   32
            TabIndex        =   46
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   15
            Left            =   240
            TabIndex        =   47
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   14
         Left            =   3120
         TabIndex        =   42
         Top             =   4560
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   14
            Left            =   1560
            Max             =   32
            TabIndex        =   43
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   14
            Left            =   240
            TabIndex        =   44
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   13
         Left            =   3120
         TabIndex        =   39
         Top             =   3720
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   13
            Left            =   1560
            Max             =   32
            TabIndex        =   40
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   13
            Left            =   240
            TabIndex        =   41
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   12
         Left            =   3120
         TabIndex        =   36
         Top             =   2880
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   12
            Left            =   1560
            Max             =   32
            TabIndex        =   37
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   12
            Left            =   240
            TabIndex        =   38
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   11
         Left            =   3120
         TabIndex        =   33
         Top             =   2040
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   11
            Left            =   1560
            Max             =   32
            TabIndex        =   34
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   11
            Left            =   240
            TabIndex        =   35
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   10
         Left            =   3120
         TabIndex        =   30
         Top             =   1200
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   10
            Left            =   1560
            Max             =   32
            TabIndex        =   31
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   10
            Left            =   240
            TabIndex        =   32
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   9
         Left            =   3120
         TabIndex        =   27
         Top             =   360
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   9
            Left            =   1560
            Max             =   32
            TabIndex        =   28
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   9
            Left            =   240
            TabIndex        =   29
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   8
         Left            =   240
         TabIndex        =   24
         Top             =   6240
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   8
            Left            =   1560
            Max             =   32
            TabIndex        =   25
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   8
            Left            =   240
            TabIndex        =   26
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   7
         Left            =   240
         TabIndex        =   21
         Top             =   5400
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   7
            Left            =   1560
            Max             =   32
            TabIndex        =   22
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   7
            Left            =   240
            TabIndex        =   23
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   6
         Left            =   240
         TabIndex        =   18
         Top             =   4560
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   6
            Left            =   1560
            Max             =   32
            TabIndex        =   19
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   6
            Left            =   240
            TabIndex        =   20
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   5
         Left            =   240
         TabIndex        =   15
         Top             =   3720
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   5
            Left            =   1560
            Max             =   32
            TabIndex        =   16
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   5
            Left            =   240
            TabIndex        =   17
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   4
         Left            =   240
         TabIndex        =   12
         Top             =   2880
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   4
            Left            =   1560
            Max             =   32
            TabIndex        =   13
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   4
            Left            =   240
            TabIndex        =   14
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   3
         Left            =   240
         TabIndex        =   9
         Top             =   2040
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   3
            Left            =   1560
            Max             =   32
            TabIndex        =   10
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   3
            Left            =   240
            TabIndex        =   11
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   2
         Left            =   240
         TabIndex        =   6
         Top             =   1200
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   2
            Left            =   1560
            Max             =   32
            TabIndex        =   7
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   2
            Left            =   240
            TabIndex        =   8
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Frame GroupIndex 
         Caption         =   "Index: 0 Character: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Index           =   1
         Left            =   240
         TabIndex        =   3
         Top             =   360
         Width           =   2775
         Begin VB.HScrollBar ScrollWidth 
            Height          =   255
            Index           =   1
            Left            =   1560
            Max             =   32
            TabIndex        =   4
            Top             =   360
            Width           =   975
         End
         Begin VB.Label LabelWidth 
            Caption         =   "Width: 0"
            Height          =   255
            Index           =   1
            Left            =   240
            TabIndex        =   5
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Label LabelPageIndex 
         Alignment       =   2  'Center
         Caption         =   "Page Index: 1/16"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   840
         TabIndex        =   52
         Top             =   7320
         Width           =   4575
      End
   End
End
Attribute VB_Name = "frmFontEditor"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Const MaximumCharacters As Long = 16

Private PageIndex As Long
Private FontIndex As Long
Private FontNames() As String

Private Sub Form_Load()
    FontIndex = 1
    PageIndex = 1

    ReDim FontNames(1 To Fonts.Fonts_Count - 1)
    
    FontNames(1) = "regular"
    FontNames(2) = "damage"

    ComboFonts.Clear

    ComboFonts.AddItem "FontRegular"
    ComboFonts.AddItem "OpenSans Damage"

    ComboFonts.ListIndex = 0
End Sub

Private Sub ComboFonts_Click()
    FontIndex = ComboFonts.ListIndex + 1
    PageIndex = ScrollPage.Value - 1

    Call LoadFont
End Sub

Private Sub ScrollPage_Change()
    FontIndex = ComboFonts.ListIndex + 1
    PageIndex = ScrollPage.Value - 1
    
    LabelPageIndex.caption = "Page Index: " & ScrollPage.Value & "/" & MaximumCharacters
    
    Call LoadFont
End Sub

Private Sub LoadFont()
    Dim i As Long
    Dim Index As Long
    
    Index = PageIndex * MaximumCharacters
    
    If Index < 1 Then Index = 1
  
    For i = 1 To MaximumCharacters
        GroupIndex(i).caption = "Index: " & Index & " Character: " & Chr$(Index)
        LabelWidth(i).caption = "Width: " & Font_Implementation.Font(FontIndex).HeaderInfo.CharWidth(Index)
        ScrollWidth(i).Value = Font_Implementation.Font(FontIndex).HeaderInfo.CharWidth(Index)
        
        Index = Index + 1
    Next
    
End Sub

Private Sub ScrollWidth_Change(Index As Integer)
    Dim CurrentCharacterIndex As Long
    
    CurrentCharacterIndex = PageIndex * MaximumCharacters

    If CurrentCharacterIndex < 1 Then
        CurrentCharacterIndex = 1
    Else
        CurrentCharacterIndex = CurrentCharacterIndex + (Index - 1)
    End If
    
    Font_Implementation.Font(FontIndex).HeaderInfo.CharWidth(CurrentCharacterIndex) = ScrollWidth(Index).Value
    LabelWidth(Index).caption = "Width: " & Font_Implementation.Font(FontIndex).HeaderInfo.CharWidth(CurrentCharacterIndex)
End Sub

Private Sub ButtonSave_Click()
    Dim Answer As Long
    Dim Message As String
    Dim Title As String

    Message = "Are you sure to save " & FontNames(FontIndex) & "?"
    Title = "Font Editor"

    Answer = MsgBox(Message, vbQuestion + vbYesNo + vbDefaultButton2, Title)
    
    If Answer = vbYes Then
        Call SaveFontHeader(FontIndex, FontNames(FontIndex) & ".dat")
    End If
End Sub

Private Sub ButtonReload_Click()
    Dim Answer As Long
    Dim Message As String
    Dim Title As String

    Message = "Are you sure to reload " & FontNames(FontIndex) & "?"
    Title = "Font Editor"

    Answer = MsgBox(Message, vbQuestion + vbYesNo + vbDefaultButton2, Title)
          
    If Answer = vbYes Then
        Call LoadFontHeader(Font_Implementation.Font(FontIndex), FontNames(FontIndex) & ".dat")
        Call LoadFont
    End If

End Sub

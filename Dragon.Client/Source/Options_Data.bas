Attribute VB_Name = "Options_Data"
Option Explicit

Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationname As String, ByVal lpKeyname As Any, ByVal lpString As String, ByVal lpfilename As String) As Long
Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationname As String, ByVal lpKeyname As Any, ByVal lpdefault As String, ByVal lpreturnedstring As String, ByVal nsize As Long, ByVal lpfilename As String) As Long

Public Options As OptionsRec

Private Type OptionsRec
    Music As Byte
    Sound As Byte
    Render As Byte
    Username As String
    SaveUser As Long
    ChannelState(0 To Channel_Count - 1) As Byte
    PlayIntro As Byte
    Resolution As Byte
    Fullscreen As Byte
End Type

' Gets a string from a text file
Public Function GetVar(File As String, Header As String, Var As String) As String
    Dim sSpaces As String   ' Max string length
    Dim szReturn As String  ' Return default value if not found
    szReturn = vbNullString
    sSpaces = Space$(5000)
    Call GetPrivateProfileString$(Header, Var, szReturn, sSpaces, Len(sSpaces), File)
    GetVar = RTrim$(sSpaces)
    GetVar = Left$(GetVar, Len(GetVar) - 1)
End Function

' Writes a variable to a text file
Public Sub PutVar(File As String, Header As String, Var As String, Value As String)
    Call WritePrivateProfileString$(Header, Var, Value, File)
End Sub

Public Sub SaveOptions()
    Dim FileName As String, i As Long

    FileName = App.Path & "\Data Files\Configuration.ini"

    Call PutVar(FileName, "Options", "Username", Options.Username)
    Call PutVar(FileName, "Options", "Music", Str$(Options.Music))
    Call PutVar(FileName, "Options", "Sound", Str$(Options.Sound))
    Call PutVar(FileName, "Options", "Render", Str$(Options.Render))
    Call PutVar(FileName, "Options", "SaveUser", Str$(Options.SaveUser))
    Call PutVar(FileName, "Options", "Resolution", Str$(Options.Resolution))
    Call PutVar(FileName, "Options", "Fullscreen", Str$(Options.Fullscreen))
    For i = 0 To ChatChannel.Channel_Count - 1
        Call PutVar(FileName, "Options", "Channel" & i, Str$(Options.ChannelState(i)))
    Next
End Sub

Public Sub LoadOptions()
    Dim FileName As String, i As Long

    On Error GoTo ErrorHandler

    FileName = App.Path & "\Data Files\Configuration.ini"

    If Not FileExist(FileName) Then
        GoTo ErrorHandler
    Else
        Options.Username = GetVar(FileName, "Options", "Username")
        Options.Music = GetVar(FileName, "Options", "Music")
        Options.Sound = Val(GetVar(FileName, "Options", "Sound"))
        Options.Render = Val(GetVar(FileName, "Options", "Render"))
        Options.SaveUser = Val(GetVar(FileName, "Options", "SaveUser"))
        Options.Resolution = Val(GetVar(FileName, "Options", "Resolution"))
        Options.Fullscreen = Val(GetVar(FileName, "Options", "Fullscreen"))
        For i = 0 To ChatChannel.Channel_Count - 1
            Options.ChannelState(i) = Val(GetVar(FileName, "Options", "Channel" & i))
        Next
    End If

    Exit Sub
    
ErrorHandler:
    Options.Music = 1
    Options.Sound = 1
    Options.Username = vbNullString
    Options.Fullscreen = 0
    Options.Render = 0
    Options.SaveUser = 0
    
    For i = 0 To ChatChannel.Channel_Count - 1
        Options.ChannelState(i) = 1
    Next
    
    SaveOptions
End Sub

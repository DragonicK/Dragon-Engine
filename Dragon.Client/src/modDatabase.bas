Attribute VB_Name = "modDatabase"
Option Explicit
' Text API
Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationname As String, ByVal lpKeyname As Any, ByVal lpString As String, ByVal lpfilename As String) As Long
Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationname As String, ByVal lpKeyname As Any, ByVal lpdefault As String, ByVal lpreturnedstring As String, ByVal nsize As Long, ByVal lpfilename As String) As Long

Public Sub ChkDir(ByVal tDir As String, ByVal tName As String)

    If LCase$(Dir$(tDir & tName, vbDirectory)) <> tName Then Call MkDir(tDir & tName)
End Sub

Public Function FileExist(ByVal FileName As String) As Boolean

    If LenB(Dir$(FileName)) > 0 Then
        FileExist = True
    End If

End Function

' gets a string from a text file
Public Function GetVar(File As String, Header As String, Var As String) As String
    Dim sSpaces As String   ' Max string length
    Dim szReturn As String  ' Return default value if not found
    szReturn = vbNullString
    sSpaces = Space$(5000)
    Call GetPrivateProfileString$(Header, Var, szReturn, sSpaces, Len(sSpaces), File)
    GetVar = RTrim$(sSpaces)
    GetVar = Left$(GetVar, Len(GetVar) - 1)
End Function

' writes a variable to a text file
Public Sub PutVar(File As String, Header As String, Var As String, Value As String)
    Call WritePrivateProfileString$(Header, Var, Value, File)
End Sub

Public Sub SaveOptions()
    Dim FileName As String, i As Long

    FileName = App.Path & "\Data Files\config_v2.ini"

    Call PutVar(FileName, "Options", "Username", Options.Username)
    Call PutVar(FileName, "Options", "Music", Str$(Options.Music))
    Call PutVar(FileName, "Options", "Sound", Str$(Options.Sound))
    Call PutVar(FileName, "Options", "NoAuto", Str$(Options.NoAuto))
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

    On Error GoTo errorhandler

    FileName = App.Path & "\Data Files\config_v2.ini"

    If Not FileExist(FileName) Then
        GoTo errorhandler
    Else
        Options.Username = GetVar(FileName, "Options", "Username")
        Options.Music = GetVar(FileName, "Options", "Music")
        Options.Sound = Val(GetVar(FileName, "Options", "Sound"))
        Options.NoAuto = Val(GetVar(FileName, "Options", "NoAuto"))
        Options.Render = Val(GetVar(FileName, "Options", "Render"))
        Options.SaveUser = Val(GetVar(FileName, "Options", "SaveUser"))
        Options.Resolution = Val(GetVar(FileName, "Options", "Resolution"))
        Options.Fullscreen = Val(GetVar(FileName, "Options", "Fullscreen"))
        For i = 0 To ChatChannel.Channel_Count - 1
            Options.ChannelState(i) = Val(GetVar(FileName, "Options", "Channel" & i))
        Next
    End If

    Exit Sub
errorhandler:
    Options.Music = 1
    Options.Sound = 1
    Options.NoAuto = 0
    Options.Username = vbNullString
    Options.Fullscreen = 0
    Options.Render = 0
    Options.SaveUser = 0
    
    For i = 0 To ChatChannel.Channel_Count - 1
        Options.ChannelState(i) = 1
    Next
    
    SaveOptions
    
    Exit Sub
End Sub

Sub ClearPlayer(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(Player(Index)), LenB(Player(Index)))
    Player(Index).Name = vbNullString
End Sub

Sub ClearAnimInstance(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(AnimInstance(Index)), LenB(AnimInstance(Index)))
End Sub


Sub ClearMapNpc(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(MapNpc(Index)), LenB(MapNpc(Index)))
    MapNpc(Index).Dead = True
End Sub

Sub ClearMapNpcs()
    Dim i As Long

    For i = 1 To MaxMapNpcs
        Call ClearMapNpc(i)
    Next

End Sub

Attribute VB_Name = "Dialogue_Packet"
Option Explicit

Public Sub HandleAlertMsg(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, dialogue_index As Long, MenuReset As Long, Kick As Long, Forced As Boolean

    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data()
    
    dialogue_index = Buffer.ReadLong
    MenuReset = Buffer.ReadLong
    Kick = Buffer.ReadByte
    Forced = CBool(Buffer.ReadByte)

    If Forced Then
        HideWindows
        SetStatus vbNullString
    End If

    Set Buffer = Nothing

    If MenuReset > 0 Then
        HideWindows
        Select Case MenuReset
        Case MenuCount.MenuLogin
            ShowWindow GetWindowIndex("winLogin")
            ShowWindow GetWindowIndex("winLoginFooter")
        Case MenuCount.MenuModels
            ShowWindow GetWindowIndex("winModels")
            ShowWindow GetWindowIndex("winLoginFooter")
        Case MenuCount.MenuClass
            ShowWindow GetWindowIndex("winClasses")
        Case MenuCount.MenuNewChar
            ShowWindow GetWindowIndex("winNewModel")
        Case MenuCount.MenuMain
            ShowWindow GetWindowIndex("winLogin")
            ShowWindow GetWindowIndex("winLoginFooter")
        End Select
    Else
        If Kick > 0 Or inMenu = True Then
            ShowWindow GetWindowIndex("winLogin")
            ShowWindow GetWindowIndex("winLoginFooter")
            
            DialogueAlert dialogue_index
            LogoutGame
            Exit Sub
        End If
    End If

    DialogueAlert dialogue_index
End Sub


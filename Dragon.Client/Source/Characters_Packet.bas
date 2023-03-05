Attribute VB_Name = "Characters_Packet"
Option Explicit

Public Sub HandlePlayerModels(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, i As Long, winNum As Long, conNum As Long, isSlotEmpty(1 To MAX_CHARS) As Boolean, X As Long
    Dim TimeString As String
    Dim Count As Long

    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data()
    
    Count = Buffer.ReadLong

    For i = 1 To Count
        CharName(i) = Trim$(Buffer.ReadString)
        CharSprite(i) = Buffer.ReadLong
        CharClass(i) = Buffer.ReadLong
        CharPendingExclusion(i) = Buffer.ReadByte

        CharHour(i) = Buffer.ReadLong
        CharMinutes(i) = Buffer.ReadLong
        CharSeconds(i) = Buffer.ReadLong
        
        CharEnabled(i) = True

        If CharPendingExclusion(i) Then
            CharDate(i) = CharHour(i) & ":" & CharMinutes(i) & ":" & CharSeconds(i)
        End If

        ' set as empty or not
        If Not Len(Trim$(CharName(i))) > 0 Then isSlotEmpty(i) = True
    Next

    Set Buffer = Nothing

    If InGame Then
        Exit Sub
    End If

    HideWindows
    ShowWindow GetWindowIndex("winModels")
    ShowWindow GetWindowIndex("winModelFooter")

    ' set GUI window up
    winNum = GetWindowIndex("winModels")
    For i = 1 To MAX_CHARS
        conNum = GetControlIndex("winModels", "lblCharName_" & i)

        With Windows(winNum).Controls(conNum)
            If Not isSlotEmpty(i) Then
                .Text = CharName(i)
            Else
                .Text = "Vazio"
            End If
        End With

        If Not CharPendingExclusion(i) Then
            conNum = GetControlIndex("winModels", "lblCharDate_" & i)
            Windows(winNum).Controls(conNum).Text = ""
        End If

        ' hide/show buttons
        If isSlotEmpty(i) Then
            ' create button
            conNum = GetControlIndex("winModels", "ButtonCreateChar_" & i)
            Windows(winNum).Controls(conNum).Visible = True
            ' select button
            conNum = GetControlIndex("winModels", "ButtonSelectChar_" & i)
            Windows(winNum).Controls(conNum).Visible = False
            ' delete button
            conNum = GetControlIndex("winModels", "ButtonDelChar_" & i)
            Windows(winNum).Controls(conNum).Visible = False

        Else
            ' create button
            conNum = GetControlIndex("winModels", "ButtonCreateChar_" & i)
            Windows(winNum).Controls(conNum).Visible = False
            ' select button
            conNum = GetControlIndex("winModels", "ButtonSelectChar_" & i)
            Windows(winNum).Controls(conNum).Visible = True
            ' delete button
            conNum = GetControlIndex("winModels", "ButtonDelChar_" & i)
            Windows(winNum).Controls(conNum).Visible = True
        End If
    Next
    
    ResizeGUI
End Sub

Public Sub SendUseChar(ByVal CharSlot As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PCharacterBegin
    Buffer.WriteLong CharSlot
    
    SendData Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Public Sub SendDeleteChar(ByVal CharSlot As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteLong EnginePacket.PCharacterDelete
    Buffer.WriteLong CharSlot
    
    SendData Buffer.ToArray()
    Set Buffer = Nothing
End Sub

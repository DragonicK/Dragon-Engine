Attribute VB_Name = "Party_Packet"
Option Explicit

Public Sub SendPartyRequest(ByVal Name As String)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PPartyRequest
    Buffer.WriteString Name
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendAcceptParty()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PAcceptParty
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendDeclineParty()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PDeclineParty
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendPartyLeave()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PPartyLeave
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendPartyKick(ByVal CharacterIndex As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PPartyKick
    Buffer.WriteLong CharacterIndex
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub HandlePartyInvite(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, TheName As String, Top As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    TheName = Buffer.ReadString

    Set Buffer = Nothing

    ' cache name and show invitation popup
    Dialogue.DataString = TheName
    
    ShowWindow GetWindowIndex("winInvite_Party")
    Windows(GetWindowIndex("winInvite_Party")).Controls(GetControlIndex("winInvite_Party", "btnInvite")).Text = ColourChar & White & TheName & ColourChar & "-1" & " convidou você para um grupo."
    
    AddText ColourChar & GetColStr(ColorType.BrightRed) & "[Sistema] : " & ColourChar & GetColStr(White) & Trim$(TheName) & " convidou você para um grupo.", White, , ChatChannel.ChannelGame

    ' loop through
    Top = ScreenHeight - 80

    If Windows(GetWindowIndex("winInvite_Trade")).Window.Visible Then
        Top = Top - 37
    End If

    Windows(GetWindowIndex("winInvite_Party")).Window.Top = Top
End Sub

Public Sub HandleClosePartyInviation(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Parameters() As String
    Dim Message As String
    
    Call CloseDialogue

    HideWindow GetWindowIndex("winInvite_Party")
    
    Message = GetSystemMessage(SystemMessage.YouFailedToAcceptParty, 0, Parameters)

    AddText ColourChar & GetColStr(ColorType.Gold) & "[Sistema] : " & ColourChar & GetColStr(Grey) & Message, Grey, , ChatChannel.ChannelGame
End Sub

Public Sub HandleParty(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, i As Long, VitalCount As Long
    Dim Count As Long, n As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Call ClearParty

    Party.Leader = Buffer.ReadLong

    ' exit out if we're not in a party
    If Party.Leader = 0 Then
        UpdatePartyInterface
        'exit out early

        Buffer.Flush
        Set Buffer = Nothing

        Exit Sub
    End If

    Party.Level = Buffer.ReadLong
    Party.Experience = Buffer.ReadLong
    Party.MaxExperience = Buffer.ReadLong

    Count = Buffer.ReadLong

    For i = 1 To Count
        Party.Member(i).Index = Buffer.ReadLong
        Party.Member(i).Name = Buffer.ReadString
        Party.Member(i).Model = Buffer.ReadLong
        Party.Member(i).Disconnected = Buffer.ReadByte
        Party.Member(i).InstanceId = Buffer.ReadLong

        VitalCount = Buffer.ReadLong

        For n = 1 To VitalCount
            Party.Member(i).Vital(n) = Buffer.ReadLong
        Next

        VitalCount = Buffer.ReadLong

        For n = 1 To VitalCount
            Party.Member(i).MaxVital(n) = Buffer.ReadLong
        Next

        If Party.Member(i).Disconnected Then
            For n = 1 To VitalCount
                Party.Member(i).Vital(n) = 0
                Party.Member(i).MaxVital(n) = 0
            Next
        End If
    Next

    Party.MemberCount = Count

    Set Buffer = Nothing

    ' update the party interface
    UpdatePartyInterface
End Sub

Public Sub HandlePartyData(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, i As Long, VitalCount As Long
    Dim Count As Long, n As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Party.Leader = Buffer.ReadLong

    ' exit out if we're not in a party
    If Party.Leader = 0 Then
        UpdatePartyInterface
        'exit out early

        Buffer.Flush
        Set Buffer = Nothing

        Exit Sub
    End If

    Party.Level = Buffer.ReadLong
    Party.Experience = Buffer.ReadLong
    Party.MaxExperience = Buffer.ReadLong
    
        ' update the party interface
    UpdatePartyInterface
End Sub

Public Sub HandlePartyVital(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, i As Long, n As Long
    Dim VitalCount As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Index = Buffer.ReadLong

    For i = 1 To MaximumPartyMembers
        If Party.Member(i).Index = Index Then

            VitalCount = Buffer.ReadLong

            For n = 1 To VitalCount
                Party.Member(i).Vital(n) = Buffer.ReadLong
            Next

            VitalCount = Buffer.ReadLong

            For n = 1 To VitalCount
                Party.Member(i).MaxVital(n) = Buffer.ReadLong
            Next
            
            Exit For
        End If
    Next

    Set Buffer = Nothing

    ' update the party interface
    UpdatePartyBars
End Sub

Public Sub HandlePartyLeave(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Call ClearParty

    UpdatePartyInterface
    
    
End Sub

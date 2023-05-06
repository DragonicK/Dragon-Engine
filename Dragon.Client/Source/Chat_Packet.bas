Attribute VB_Name = "Chat_Packet"
Option Explicit

Public Sub SendMapMessage(ByVal Text As String)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PBroadcastMessage
    Buffer.WriteLong ChatChannel.ChannelMap
    Buffer.WriteString Text
    Buffer.WriteString GetPlayerName(MyIndex)
    Buffer.WriteLong 0 ' AccountLevel
    Buffer.WriteLong ColorType.White ' Color

    SendChatMessage Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub SendBroadcastMessage(ByVal Text As String)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PBroadcastMessage
    Buffer.WriteLong ChatChannel.ChannelGlobal
    Buffer.WriteString Text
    Buffer.WriteString GetPlayerName(MyIndex)
    Buffer.WriteLong 0   ' AccountLevel
    Buffer.WriteLong ColorType.White  ' Color

    SendChatMessage Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub SendPlayerMessage(ByVal Text As String, ByVal MsgTo As String)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PBroadcastMessage
    Buffer.WriteLong ChatChannel.ChannelPrivate
    Buffer.WriteString Text
    Buffer.WriteString MsgTo
    Buffer.WriteLong 0 ' AccountLevel
    Buffer.WriteLong ColorType.White  ' Color
    
    SendChatMessage Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub HandleMessageBubble(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, TargetType As Long, Target As String, Message As String, Colour As Long
    Dim i As Long, IsOnline As Boolean

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Target = Trim$(Buffer.ReadString)
    TargetType = Buffer.ReadLong
    Message = Trim$(Buffer.ReadString)
    Colour = Buffer.ReadLong

    For i = 1 To Player_HighIndex
        If LenB(Player(i).Name) > 0 Then
            If Player(i).Name = Target Then
                IsOnline = True
                Exit For
            End If
        End If
    Next

    If IsOnline Then
        AddChatBubble i, TargetType, Message, Colour
    End If

    Set Buffer = Nothing
End Sub

Public Sub HandleBroadcastMessage(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim Access As Long
    Dim Name As String
    Dim Message As String
    Dim Color As Long
    Dim Header As String
    Dim Channel As Long, colStr As String

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Channel = Buffer.ReadLong
    Message = Trim$(Buffer.ReadString)
    Name = Trim$(Buffer.ReadString)
    Access = Buffer.ReadLong
    Color = Buffer.ReadLong

    Set Buffer = Nothing

    Select Case Channel
    Case ChatChannel.ChannelMap
        Header = "[Mapa] "

    Case ChatChannel.ChannelGlobal
        Header = "[Global] "

    Case ChatChannel.ChannelGame
        Header = "[Jogo] "

    Case ChatChannel.ChannelParty
        Header = "[Party] "
        
    Case ChatChannel.ChannelGuild
        Header = "[Clã] "
                
    Case ChatChannel.ChannelPrivate
        Header = "[Privado] "
    End Select

    If Access > 0 Then Color = Pink

    ' add to the chat box
    AddText ColourChar & GetColStr(Color) & Header & Name & ": " & ColourChar & GetColStr(Grey) & Message, Grey, , Channel
End Sub

Public Sub HandleSystemMessage(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim Color As Long
    Dim ParamCount As Long
    Dim Header As Long
    Dim Parameters() As String
    Dim Message As String
    Dim i As Long

    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    Header = Buffer.ReadLong
    Color = Buffer.ReadLong
    ParamCount = Buffer.ReadLong

    If ParamCount > 0 Then
        ReDim Parameters(1 To ParamCount)

        For i = 1 To ParamCount
            Parameters(i) = Buffer.ReadString
        Next

    End If

    Set Buffer = Nothing

    Message = GetSystemMessage(Header, ParamCount, Parameters)

    ' add to the chat box
    AddText ColourChar & GetColStr(Color) & "[Sistema] : " & ColourChar & GetColStr(Grey) & Message, Grey, , ChatChannel.ChannelGame

End Sub


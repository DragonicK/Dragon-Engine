Attribute VB_Name = "Conversation_Packet"
Option Explicit

Public Sub HandleConversationClose(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Call HideConversation
End Sub

Public Sub HandleConversation(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim NpcId As Long
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    NpcId = Buffer.ReadLong

    If NpcId >= 1 And NpcId <= MaximumNpcs Then
        Index = GetWindowIndex("winConversation")

        Call EnumerateConversations(NpcId)
        
        ' Allocate text
        Call AllocateGreetingsLineCount(NpcId)
        
        Call ListConversations
        
        If Index > 0 Then
            Call CentraliseWindow(Index)
        End If
        
        Call ShowConversation
    End If

    Set Buffer = Nothing
End Sub

Public Sub HandleConversationOption(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Id As Long, ChatIndex As Long
    
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data
    
    Id = Buffer.ReadLong
    ChatIndex = Buffer.ReadLong
    
    ConversationView.IsInConversation = True
    ConversationView.ConversationId = Id
    ConversationView.ChatIndex = ChatIndex + 1
    
    Call AllocateMainTextLineCount
    Call ListConversationOptions

    Set Buffer = Nothing
End Sub

Public Sub SendConversationOption(ByVal ConversationId As Long, ByVal ChatIndex As Long, Optional ByVal OptionValue As Long = 0)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PConversationOption
    Buffer.WriteLong ConversationId
    Buffer.WriteLong ChatIndex
    Buffer.WriteLong OptionValue

    SendGameMessage Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

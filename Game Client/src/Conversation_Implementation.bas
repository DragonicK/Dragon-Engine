Attribute VB_Name = "Conversation_Implementation"
Option Explicit

Public Const MaximumViewConversations As Long = 50

Public ConversationView As ConversationViewRec

Public Type ConversationViewRec
    Count As Long
    Conversations(1 To MaximumViewConversations) As Long
    NpcId As Long
    ChatIndex As Long
    IsInConversation As Boolean
    ConversationId As Long
End Type

Public Sub EnumerateConversations(ByVal NpcId As Long)
    Dim i As Long
    Dim Id As Long
    Dim Count As Long
    Dim Index As Long

    If NpcId <= 0 Or NpcId > MaximumNpcs Then
        Exit Sub
    End If

    Count = Npc(NpcId).ConversationCount

    For i = 1 To Count
        Id = Npc(NpcId).Conversations(i)

        If Id > 0 Then
            Call AddNormalConversation(Index, Id)
        End If
    Next

    ConversationView.NpcId = NpcId
    ConversationView.Count = Index
    ConversationView.IsInConversation = False

End Sub

Private Sub AddNormalConversation(ByRef Index As Long, ByVal Id As Long)
    Index = Index + 1
    ConversationView.Conversations(Index) = Id
End Sub


















' Private Sub AddQuestConversation(ByVal ConversationId As Long, ByRef View As ConversationViewRec)
' Dim QuestId As Long

'QuestId = Conversations(ConversationId).QuestId
'
'If QuestId > 0 Then

' check requirements
'      If CanAddQuest(QuestId) Then
'         Call AddNormalConversation(ConversationId, View)
'       End If

'    End If
'End Sub

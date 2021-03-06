Attribute VB_Name = "Conversation_Data"
Public MaxConversations As Long
Public Const MaxConversationOptions As Long = 4

Public Conversations() As ConversationRec

Public Enum ConversationType
    ConversationType_None = 0
    ConversationType_Quest
End Enum

Public Enum ConversationEvent
    ConversationEvent_None = 0
    ConversationEvent_Close
    ConversationEvent_OpenShop
    ConversationEvent_OpenBank
    ConversationEvent_OpenCraft
    ConversationEvent_OpenUpgrade
    ConversationEvent_GiveItem
    ConversationEvent_GiveEffect
    ConversationEvent_LearnCraft
    ConversationEvent_StartQuest
    ConversationEvent_ShowQuestReward
    ConversationEvent_Teleport
End Enum

Private Type ChatReplyRec
    Target As Long
    Text As String
End Type

Private Type ChatRec
    Text As String
    OptionCount As Long
    Reply() As ChatReplyRec
    Data1 As Long
    Data2 As Long
    Data3 As Long
    Event As ConversationEvent
End Type

Private Type ConversationRec
    Id As Long
    Name As String
    QuestId As Long
    Type As ConversationType
    ChatCount As Long
    Chats() As ChatRec
    MeanwhileText As String
    CompletedText As String
End Type

Public Sub LoadConversations()
    Dim Index As Long
    Dim i As Long
    Dim Name As String
    Dim Text As String
    Dim ChatCount As Long
    Dim OptionCount As Long
    Dim n As Long
    Dim x As Long

    Index = GetFileHandler(App.Path & "\Data Files\Data\Conversations.dat")

    If Index > 0 Then
        MaxConversations = ReadInt32(Index)

        If MaxConversations > 0 Then
            ReDim Conversations(1 To MaxConversations)

            For i = 1 To MaxConversations
                Name = String(255, vbNullChar)

                Conversations(i).Id = ReadInt32(Index)

                Call ReadString(Index, Name)

                Conversations(i).Name = Replace$(Name, vbNullChar, vbNullString)
                Conversations(i).Type = ReadInt32(Index)
                Conversations(i).QuestId = ReadInt32(Index)

                ChatCount = ReadInt32(Index)
                Conversations(i).ChatCount = ChatCount

                If ChatCount > 0 Then
                    ReDim Conversations(i).Chats(1 To ChatCount)

                    For n = 1 To ChatCount
                        Text = String(255, vbNullChar)

                        OptionCount = ReadInt32(Index)

                        Call ReadString(Index, Text)

                        With Conversations(i).Chats(n)
                            .Text = Replace$(Text, vbNullChar, vbNullString)
                            .Event = ReadInt32(Index)
                            .Data1 = ReadInt32(Index)
                            .Data2 = ReadInt32(Index)
                            .Data3 = ReadInt32(Index)
                            .OptionCount = OptionCount
                            
                            ReDim .Reply(1 To OptionCount)
                        End With

                        For x = 1 To OptionCount
                            Conversations(i).Chats(n).Reply(x).Target = ReadInt32(Index)

                            Call ReadString(Index, Text)

                            Conversations(i).Chats(n).Reply(x).Text = Replace$(Text, vbNullChar, vbNullString)
                        Next
                    Next
                End If

                Call ReadString(Index, Text)

                Conversations(i).MeanwhileText = Replace$(Text, vbNullChar, vbNullString)

                Call ReadString(Index, Text)

                Conversations(i).CompletedText = Replace$(Text, vbNullChar, vbNullString)
            Next
        End If
    End If

    Call CloseFileHandler(Index)

End Sub

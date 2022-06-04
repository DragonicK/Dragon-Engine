Attribute VB_Name = "Npc_Data"
Option Explicit

Public MaximumNpcs As Long
Public Npc() As NpcRec

Public Enum NpcBehaviour
    NpcBehaviour_None
    NpcBehaviour_Friendly
    NpcBehaviour_Patrol
    NpcBehaviour_Shopkeeper
    NpcBehaviour_Monster
    NpcBehaviour_Boss
End Enum

Public Type NpcRec
    Id As Long
    Name As String
    Title As String
    Sound As String
    Behaviour As NpcBehaviour
    ModelId As Long
    Level As Long
    AttributeId As Long
    Experience As Long
    ConversationCount As Long
    Conversations() As Long
    Greetings As String
End Type

Public Sub LoadNpcs()
    Dim Index As Long
    Dim i As Long
    Dim n As Long

    Dim Name As String
    Dim Title As String
    Dim Sound As String
    Dim Greetings As String
    Dim Count As Long

    Index = GetFileHandler(App.Path & "\Data Files\Data\Npcs.dat")

    If Index > 0 Then
        MaximumNpcs = ReadInt32(Index)

        If MaximumNpcs > 0 Then
            ReDim Npc(1 To MaximumNpcs)

            For i = 1 To MaximumNpcs
                Name = String(512, vbNullChar)
                Title = String(512, vbNullChar)
                Sound = String(512, vbNullChar)
                Greetings = String(512, vbNullChar)

                Npc(i).Id = ReadInt32(Index)
                Call ReadString(Index, Name)
                Call ReadString(Index, Title)
                Call ReadString(Index, Sound)
                Npc(i).Behaviour = ReadInt32(Index)
                Npc(i).ModelId = ReadInt32(Index)
                Npc(i).Level = ReadInt32(Index)
                Npc(i).AttributeId = ReadInt32(Index)
                Npc(i).Experience = ReadInt32(Index)

                Npc(i).Name = Replace$(Name, vbNullChar, vbNullString)
                Npc(i).Title = Replace$(Title, vbNullChar, vbNullString)
                Npc(i).Sound = Replace$(Sound, vbNullChar, vbNullString)

                Call ReadString(Index, Greetings)
                
                Npc(i).Greetings = Replace$(Greetings, vbNullChar, vbNullString)
                
                Count = ReadInt32(Index)

                Npc(i).ConversationCount = Count

                If Count > 0 Then
                    ReDim Npc(i).Conversations(1 To Count)

                    For n = 1 To Count
                        Npc(i).Conversations(n) = ReadInt32(Index)
                    Next
                End If

            Next
        End If
    End If

    Call CloseFileHandler(Index)

End Sub



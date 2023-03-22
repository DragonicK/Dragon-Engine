Attribute VB_Name = "Quest_Data"
Option Explicit

Public MaximumQuests As Long

Public Quests() As QuestRec

Public Enum QuestType
    QuestType_Quest
    QuestType_Campaing
    QuestType_Important
End Enum

Public Enum QuestShareable
    QuestShareable_None
    QuestShareable_Share
End Enum

Public Enum QuestSelectableReward
    QuestSelectableReward_Fixed
    QuestSelectableReward_Optional
End Enum

Public Enum QuestRewardType
    QuestRewardType_Item
    QuestRewardType_Currency
    QuestRewardType_Experience
End Enum

Public Enum QuestRepeatable
    QuestRepeatable_None
    QuestRepeatable_Repeatable
    QuestRepeatable_Daily
    QuestRepeatable_Weekly
End Enum

Public Enum QuestActionType
    QuestActionType_UseItem
    QuestActionType_CollectItem
    QuestActionType_ObtainItem
    QuestActionType_TalkNpc
    QuestActionType_TalkObject
    QuestActionType_KillNpc
    QuestActionType_KillPlayer
    QuestActionType_GoToMap
    QuestActionType_GoToPosition
    QuestActionType_GetInside
End Enum

Public Type QuestRequirementRec
    EntityId As Long
    Value As Long
    X As Long
    Y As Long
End Type

Public Type QuestStepRec
    Title As String
    Summary As String
    ActionType As QuestActionType
    Requirement As QuestRequirementRec
End Type

Public Type QuestRewardRec
    Id As Long
    Value As Long
    Level As Long
    Bound As Boolean
    AttributeId As Long
    UpgradeId As Long
    Type As QuestRewardType
End Type

Public Type QuestRec
    Id As Long
    Title As String
    Summary As String
Type As QuestType
    Repeatable As QuestRepeatable
    Shareable As QuestShareable
    SelectableReward As QuestSelectableReward
    SelectableRewardCount As Long
    StepCount As Long
    Steps() As QuestStepRec
    RewardCount As Long
    Rewards() As QuestRewardRec
End Type

Public Sub LoadQuests()
    Dim Index As Long
    Dim i As Long, X As Long, Y As Long
    Dim Title As String
    Dim Summary As String

    If Not FileExist(App.Path & "\Data Files\Data\Quests.dat") Then
        MsgBox ("\Data Files\Data\Quests not found.")

        Exit Sub
    End If

    Index = GetFileHandler(App.Path & "\Data Files\Data\Quests.dat")

    If Index > 0 Then
        MaximumQuests = ReadInt32()

        If MaximumQuests > 0 Then
            ReDim Quests(1 To MaximumQuests)

            For i = 1 To MaximumQuests
                With Quests(i)
                    .Id = ReadInt32()
                    Title = String(255, vbNullChar)
                    Summary = String(255, vbNullChar)

                    Call ReadString(Title)
                    Call ReadString(Summary)

                    .Title = Replace$(Title, vbNullChar, vbNullString)
                    .Summary = Replace$(Summary, vbNullChar, vbNullString)
                    .Type = ReadInt32()
                    .Repeatable = ReadInt32()
                    .Shareable = ReadInt32()
                    .SelectableReward = ReadInt32()
                    .SelectableRewardCount = ReadInt32()

                    .StepCount = ReadInt32()

                    If .StepCount > 0 Then
                        ReDim .Steps(1 To .StepCount)

                        For X = 1 To .StepCount
                            Call ReadString(Title)
                            Call ReadString(Summary)

                            .Steps(X).Title = Replace$(Title, vbNullChar, vbNullString)
                            .Steps(X).Summary = Replace$(Summary, vbNullChar, vbNullString)
                            .Steps(X).ActionType = ReadInt32()
                            .Steps(X).Requirement.EntityId = ReadInt32()
                            .Steps(X).Requirement.Value = ReadInt32()
                            .Steps(X).Requirement.X = ReadInt32()
                            .Steps(X).Requirement.Y = ReadInt32()
                        Next
                    End If

                    .RewardCount = ReadInt32()

                    If .RewardCount > 0 Then
                        ReDim .Rewards(1 To .RewardCount)

                        For Y = 1 To .RewardCount
                            .Rewards(Y).Id = ReadInt32()
                            .Rewards(Y).Value = ReadInt32()
                            .Rewards(Y).Level = ReadInt32()
                            .Rewards(Y).Bound = ReadBoolean()
                            .Rewards(Y).AttributeId = ReadInt32()
                            .Rewards(Y).UpgradeId = ReadInt32()
                            .Rewards(Y).Type = ReadInt32()
                        Next
                    End If

                End With

            Next
        End If
    End If

    Call CloseFileHandler

End Sub

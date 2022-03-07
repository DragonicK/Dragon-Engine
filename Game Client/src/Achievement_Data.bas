Attribute VB_Name = "Achievement_Data"
Option Explicit

Public MaxAchievements As Long
Public MaxAchievementAttributes As Long

Public Achievement() As AchievementRec
Public AchievementAttributes() As AttributesRec

Public Enum AchievementCategory
    AchievementCategory_General
    AchievementCategory_Summary
    AchievementCategory_Character
    AchievementCategory_Quest
    AchievementCategory_Reputation
    AchievementCategory_Dungeon
    AchievementCategory_Profession
    AchievementCategory_Exploration
    AchievementCategory_Pvp
    AchievementCategory_Count
End Enum

Private Type AchievementRequirementRec
    Id As Long
    Value As Long
    Level As Long
    Count As Long
    Rarity As RarityType
    Equipment As Long
    PrimaryType As Long
    SecondaryType As Long
    Description As String
End Type

Private Type AchievementRewardRec
    Id As Long
    Value As Long
    Level As Long
    Bound As Byte
    AttributeId As Long
    UpgradeId As Long
End Type

Private Type AchievementRec
    Id As Long
    Name As String
    Description As String
    Rarity As Byte
    Points As Long
    AttributeId As Long
    Category As AchievementCategory
    RewardCount As Long
    Rewards() As AchievementRewardRec
    RequirementCount As Long
    Requirements() As AchievementRequirementRec
End Type

Public Sub LoadAchievements()
    Dim Index As Long
    Dim i As Long, n As Long
    Dim Name As String
    Dim Description As String

    Index = GetFileHandler(App.Path & "\Data Files\Data\Achievements.dat")

    If Index > 0 Then
        MaxAchievements = ReadInt32(Index)

        If MaxAchievements > 0 Then
            ReDim Achievement(1 To MaxAchievements)

            For i = 1 To MaxAchievements
                With Achievement(i)
                    .Id = ReadInt32(Index)

                    Name = String(255, vbNullChar)
                    Description = String(1024, vbNullChar)

                    Call ReadString(Index, Name)
                    Call ReadString(Index, Description)

                    .Name = Replace(Name, vbNullChar, vbNullString)
                    .Description = Replace(Description, vbNullChar, vbNullString)

                    .Rarity = ReadInt32(Index)
                    .Points = ReadInt32(Index)
                    .AttributeId = ReadInt32(Index)
                    .Category = ReadInt32(Index)

                    .RequirementCount = ReadInt32(Index)

                    If .RequirementCount > 0 Then
                        ReDim .Requirements(1 To .RequirementCount)

                        For n = 1 To .RequirementCount
                            .Requirements(n).Id = ReadInt32(Index)
                            .Requirements(n).Value = ReadInt32(Index)
                            .Requirements(n).Level = ReadInt32(Index)
                            .Requirements(n).Count = ReadInt32(Index)
                            .Requirements(n).Rarity = ReadInt32(Index)
                            .Requirements(n).Equipment = ReadInt32(Index)
                            .Requirements(n).PrimaryType = ReadInt32(Index)
                            .Requirements(n).SecondaryType = ReadInt32(Index)
                            
                             Call ReadString(Index, Description)
                            
                            .Requirements(n).Description = Replace(Description, vbNullChar, vbNullString)
                        Next
                    End If

                    .RewardCount = ReadInt32(Index)

                    If .RewardCount > 0 Then
                        ReDim .Rewards(1 To .RewardCount)

                        For n = 1 To .RewardCount
                            .Rewards(n).Id = ReadInt32(Index)
                            .Rewards(n).Value = ReadInt32(Index)
                            .Rewards(n).Level = ReadInt32(Index)
                            .Rewards(n).Bound = ReadBoolean(Index)
                            .Rewards(n).AttributeId = ReadInt32(Index)
                            .Rewards(n).UpgradeId = ReadInt32(Index)
                        Next
                    End If

                End With

            Next
        End If
    End If

    Call CloseFileHandler(Index)

End Sub

Public Sub LoadAchievementAttributes()
    Call LoadAttributes(AchievementAttributes, MaxAchievementAttributes, "AchievementAttributes.dat")
End Sub


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

Public Enum AchievementRewardType
    AchievementRewardType_None
    AchievementRewardType_Item
    AchievementRewardType_Title
    AchievementRewardType_Currency
End Enum

Public Enum AchievementPrimaryRequirement
    AchievementPrimaryRequirement_None
    AchievementPrimaryRequirement_Location
    AchievementPrimaryRequirement_Kill
    AchievementPrimaryRequirement_Quest
    AchievementPrimaryRequirement_LevelUp
    AchievementPrimaryRequirement_Instance
    AchievementPrimaryRequirement_Acquire
    AchievementPrimaryRequirement_UseItem
    AchievementPrimaryRequirement_EquipItem
    AchievementPrimaryRequirement_ItemUpgrade
    AchievementPrimaryRequirement_Casting
End Enum

Public Enum AchievementSecondaryRequirement
    AchievementSecondaryRequirement_None
    AchievementSecondaryRequirement_AcquireItem '= &H1
    AchievementSecondaryRequirement_AcquireCurrency '= &H2
    AchievementSecondaryRequirement_DestroyNpc '= &H4
    AchievementSecondaryRequirement_DestroyObject '= &H8
    AchievementSecondaryRequirement_DestroyPlayer '= &H10
    AchievementSecondaryRequirement_EquipItemById '= &H20
    AchievementSecondaryRequirement_EquipItemByLevel '= &H40
    AchievementSecondaryRequirement_EquipItemRarity '= &H80
    AchievementSecondaryRequirement_EquipItemByType '= &H100
    AchievementSecondaryRequirement_InstanceEnter '= &H200
    AchievementSecondaryRequirement_InstanceCompleted '= &H400
    AchievementSecondaryRequirement_ItemUpgradeByFailed '= &H800
    AchievementSecondaryRequirement_ItemUpgradeById '= &H1000
    AchievementSecondaryRequirement_ItemUpgradeByLevel '= &H2000
    AchievementSecondaryRequirement_ItemUpgradeByRarity '= &H4000
    AchievementSecondaryRequirement_ItemUpgradeByType '= &H8000
    AchievementSecondaryRequirement_LevelUpByCharacter '= &H10000
    AchievementSecondaryRequirement_LevelUpBySkill '= &H20000
    AchievementSecondaryRequirement_LevelUpByParty '= &H40000
    AchievementSecondaryRequirement_LevelUpByCraft '= &H80000
    AchievementSecondaryRequirement_QuestDoneById '= &H100000
    AchievementSecondaryRequirement_UseItemById '= &H200000
End Enum

Public Type AchievementRequirementRec
    Id As Long
    Value As Long
    Level As Long
    Count As Long
    Rarity As RarityType
    Equipment As Long
    PrimaryType As AchievementPrimaryRequirement
    SecondaryType As AchievementSecondaryRequirement
    Description As String
End Type

Public Type AchievementRewardRec
    Type As AchievementRewardType
    Id As Long
    Value As Long
    Level As Long
    Bound As Byte
    AttributeId As Long
    UpgradeId As Long
End Type

Public Type AchievementRec
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

    If Index = 0 Then
        MaxAchievements = ReadInt32()

        If MaxAchievements > 0 Then
            ReDim Achievement(1 To MaxAchievements)

            For i = 1 To MaxAchievements
                With Achievement(i)
                    .Id = ReadInt32()

                    Name = String(255, vbNullChar)
                    Description = String(1024, vbNullChar)

                    Call ReadString(Name)
                    Call ReadString(Description)

                    .Name = Replace(Name, vbNullChar, vbNullString)
                    .Description = Replace(Description, vbNullChar, vbNullString)

                    .Rarity = ReadInt32()
                    .Points = ReadInt32()
                    .AttributeId = ReadInt32()
                    .Category = ReadInt32()

                    .RequirementCount = ReadInt32()

                    If .RequirementCount > 0 Then
                        ReDim .Requirements(1 To .RequirementCount)

                        For n = 1 To .RequirementCount
                            .Requirements(n).Id = ReadInt32()
                            .Requirements(n).Value = ReadInt32()
                            .Requirements(n).Level = ReadInt32()
                            .Requirements(n).Count = ReadInt32()
                            .Requirements(n).Rarity = ReadInt32()
                            .Requirements(n).Equipment = ReadInt32()
                            .Requirements(n).PrimaryType = ReadInt32()
                            .Requirements(n).SecondaryType = ReadInt32()

                            Call ReadString(Description)

                            .Requirements(n).Description = Replace(Description, vbNullChar, vbNullString)
                        Next
                    End If

                    .RewardCount = ReadInt32()

                    If .RewardCount > 0 Then
                        ReDim .Rewards(1 To .RewardCount)

                        For n = 1 To .RewardCount
                            .Rewards(n).Type = ReadInt32()
                            .Rewards(n).Id = ReadInt32()
                            .Rewards(n).Value = ReadInt32()
                            .Rewards(n).Level = ReadInt32()
                            .Rewards(n).Bound = ReadBoolean()
                            .Rewards(n).AttributeId = ReadInt32()
                            .Rewards(n).UpgradeId = ReadInt32()
                        Next
                    End If

                End With

            Next
        End If
    End If

    Call CloseFileHandler

End Sub

Public Sub LoadAchievementAttributes()
    Call LoadAttributes(AchievementAttributes, MaxAchievementAttributes, "AchievementAttributes.dat")
End Sub


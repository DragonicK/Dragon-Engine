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

Private Type AchievementEntryRec
    Id As Long
    Value As Long
    Level As Long
    Count As Long
    Rarity As RarityType
    Equipment As Long
    PrimaryType As Long
    SecondaryType As Long
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
    Entry As AchievementEntryRec
    Reward As AchievementRewardRec
End Type

Public Sub LoadAchievements()
    Dim Index As Long
    Dim i As Long
    Dim Name As String
    Dim Description As String

    Index = GetFileHandler(App.Path & "\Data Files\Data\Achievements.dat")
    
    If Index > 0 Then
        MaxAchievements = ReadInt32(Index)

        If MaxAchievements > 0 Then
            ReDim Achievement(1 To MaxAchievements)

            For i = 1 To MaxAchievements
                Achievement(i).Id = ReadInt32(Index)
                
                Name = String(255, vbNullChar)
                Description = String(1024, vbNullChar)
                
                Call ReadString(Index, Name)
                Call ReadString(Index, Description)
                
                Achievement(i).Name = Replace(Name, vbNullChar, vbNullString)
                Achievement(i).Description = Replace(Description, vbNullChar, vbNullString)
                
                Achievement(i).Rarity = ReadInt32(Index)
                Achievement(i).Points = ReadInt32(Index)
                Achievement(i).AttributeId = ReadInt32(Index)
                Achievement(i).Category = ReadInt32(Index)

                Achievement(i).Entry.Id = ReadInt32(Index)
                Achievement(i).Entry.Value = ReadInt32(Index)
                Achievement(i).Entry.Level = ReadInt32(Index)
                Achievement(i).Entry.Count = ReadInt32(Index)
                Achievement(i).Entry.Rarity = ReadInt32(Index)
                Achievement(i).Entry.Equipment = ReadInt32(Index)
                Achievement(i).Entry.PrimaryType = ReadInt32(Index)
                Achievement(i).Entry.SecondaryType = ReadInt32(Index)

                Achievement(i).Reward.Id = ReadInt32(Index)
                Achievement(i).Reward.Value = ReadInt32(Index)
                Achievement(i).Reward.Level = ReadInt32(Index)
                Achievement(i).Reward.Bound = ReadByte(Index)
                Achievement(i).Reward.AttributeId = ReadInt32(Index)
                Achievement(i).Reward.UpgradeId = ReadInt32(Index)
            Next
        End If
    End If

    Call CloseFileHandler(Index)

End Sub

Public Sub LoadAchievementAttributes()
    Call LoadAttributes(AchievementAttributes, MaxAchievementAttributes, "AchievementAttributes.dat")
End Sub


Attribute VB_Name = "Achievement_Packet"
Option Explicit

Public Sub HandlePlayerAchievement(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, i As Long, MaxAchievements As Integer, AchievementId As Integer

    For i = 1 To MaxAchievements
        Call SetPlayerAchievement(i, 0)
    Next

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    MaxAchievements = Buffer.ReadInteger

    If MaxAchievements > 0 Then
        For i = 1 To MaxAchievements
            AchievementId = Buffer.ReadInteger

            If AchievementId > 0 And AchievementId <= MaxAchievements Then
                Call SetPlayerAchievement(AchievementId, 1)
                Call SetPlayerAchievementPoints(GetPlayerAchievementPoints() + Achievement(AchievementId).Points)
            End If
        Next

        CountPlayerAchievements
    End If

    Set Buffer = Nothing
End Sub

Public Sub HandleUpdatePlayerAchievement(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim AchievementId As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    AchievementId = Buffer.ReadInteger

    If AchievementId > 0 And AchievementId <= MaxAchievements Then
        Call SetPlayerAchievement(AchievementId, 1)
        Call SetPlayerAchievementPoints(GetPlayerAchievementPoints() + Achievement(AchievementId).Points)

        AddText "Você obteve uma nova conquista", Gold
        AddText Trim$(Achievement(AchievementId).Name), Gold

        CountPlayerAchievements
    End If

    Set Buffer = Nothing
End Sub

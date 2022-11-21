Attribute VB_Name = "Achievement_Implementation"
Option Explicit

Private PlayerAchievement() As Long
Private AchievementPoints As Long

Public Sub InitPlayerAchievementArray()
    If MaxAchievements > 0 Then
        ReDim PlayerAchievement(0 To MaxAchievements)
    End If
End Sub

Public Function GetPlayerAchievement(ByVal Index As Long) As Integer
    GetPlayerAchievement = PlayerAchievement(Index)
End Function
Public Sub SetPlayerAchievement(ByVal Index As Long, ByVal Value As Integer)
    PlayerAchievement(Index) = Value
End Sub

Public Function GetPlayerAchievementPoints() As Long
    GetPlayerAchievementPoints = AchievementPoints
End Function

Public Sub SetPlayerAchievementPoints(ByVal Points As Long)
    AchievementPoints = Points
End Sub

Public Sub ClearPlayerAchievement()
    Dim i  As Long
    
    For i = 1 To MaxAchievements
        Call SetPlayerAchievement(i, 0)
    Next
    
    Call SetPlayerAchievementPoints(0)
End Sub

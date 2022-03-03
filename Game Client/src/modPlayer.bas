Attribute VB_Name = "modPlayer"
Option Explicit

Public Sub ClearData()
    Dim i As Long, n As Long

    For n = 1 To MaxPlayers
        For i = 1 To MAX_PLAYER_ICON_EFFECT
            Call SetPlayerIconId(n, i, 0)
            Call SetPlayerIconDuration(n, i, 0)
            Call SetPlayerIconLevel(n, i, 0)
        Next
    Next

    ClearTitles
    Call ClearTitleWindow
End Sub

'#####################
'######## DEAD #######
'#####################
Function GetPlayerDead(ByVal Index As Long) As Boolean
    GetPlayerDead = Player(Index).Dead
End Function
Sub SetPlayerDead(ByVal Index As Long, ByVal Dead As Boolean)
    Player(Index).Dead = Dead
End Sub

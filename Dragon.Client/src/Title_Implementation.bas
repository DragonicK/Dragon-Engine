Attribute VB_Name = "Title_Implementation"
Option Explicit

Public MaxPlayerTitles As Long
' Titulos do personagem.
Private Titles() As Long

Public Function GetPlayerTitle(ByVal Index As Long) As Long
    GetPlayerTitle = Player(Index).ActiveTitleNum
End Function
Public Function SetPlayerTitle(ByVal Index As Long, ByVal TitleId As Long) As Long
    Player(Index).ActiveTitleNum = TitleId
End Function

Public Function GetTitle(ByVal Index As Long) As Long
    GetTitle = Titles(Index)
End Function
Public Sub SetTitle(ByVal Index As Long, ByVal TitleId As Long)
    Titles(Index) = TitleId
End Sub

Public Sub ClearTitles()
    Dim i As Long

    If MaxPlayerTitles > 0 Then
    
        For i = 1 To MaxPlayerTitles
            Call SetTitle(i, 0)
        Next
        
    End If
End Sub

Public Sub InitializePlayerTitles()
    ReDim Titles(1 To MaxPlayerTitles)
End Sub


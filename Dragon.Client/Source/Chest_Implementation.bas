Attribute VB_Name = "Chest_Implementation"
Option Explicit

Public Sub AddChest(ByVal Id As Long, ByVal X As Long, ByVal Y As Long, ByVal Sprite As Long, ByVal IsLooted As Boolean)
    Dim i As Long
    
    For i = 1 To MaximumChests
        If Chests(i).Id = 0 Then
            Chests(i).Id = Id
            Chests(i).X = X * PIC_X
            Chests(i).Y = Y * PIC_Y
            Chests(i).Sprite = Sprite
            Chests(i).AlreadyLooted = False
            Chests(i).Alpha = 255
            
            Exit For
        End If
    Next
    
    CalculateHighIndex
End Sub

Private Sub CalculateHighIndex()
    Dim i As Long, HighIndex As Long

    For i = MaximumChests To 1 Step -1
        If Chests(i).Id > 0 Then
            HighIndex = i
            Exit For
        End If
    Next

    Chests_HighIndex = HighIndex
End Sub

Public Sub ClearChests()
    Dim i As Long

    For i = 1 To MaximumChests
        Call ClearChest(i)
    Next
End Sub

Public Sub ClearChest(ByVal Index As Long)
    Chests(Index).Id = 0
    Chests(Index).X = 0
    Chests(Index).Y = 0
    Chests(Index).Alpha = 0
    Chests(Index).Sprite = 0
    Chests(Index).AlreadyLooted = False
End Sub

Public Sub ProcessChestsFadeAlpha()
    Dim i As Long

    For i = 1 To Chests_HighIndex
        If Chests(i).Id > 0 And Chests(i).AlreadyLooted Then
            If Chests(i).Alpha > 5 Then
                Chests(i).Alpha = Chests(i).Alpha - 5
            Else
                Call ClearChest(i)
            End If
        End If
    Next

End Sub

Public Sub DrawChests()
    Dim i As Long, X As Long, Y As Long

    For i = 1 To Chests_HighIndex
        With Chests(i)
            If .Id > 0 Then
                X = ConvertMapX(.X)
                Y = ConvertMapY(.Y)

                Call RenderTexture(Tex_Chest(.Sprite), X, Y, 0, 0, PIC_X, PIC_Y, PIC_X, PIC_Y)
            End If
        End With
    Next

End Sub

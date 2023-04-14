Attribute VB_Name = "Chest_Implementation"
Option Explicit

Public Sub AddChest(ByVal Id As Long, ByVal X As Long, ByVal Y As Long, ByVal Sprite As Long, ByVal State As ChestState)
    Dim i As Long

    For i = 1 To MaximumChests
        With Chests(i)
            If .Id = 0 Then
                .Id = Id
                .X = X * PIC_X
                .Y = Y * PIC_Y
                .Sprite = Sprite
                .Alpha = 255
                .State = State

                If .State = ChestState_Empty Then
                    .AlreadyLooted = True
                End If

                Exit For
            End If
        End With
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
    Chests(Index).State = ChestState.ChestState_Closed
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
    Dim SourceX As Long, SourceY As Long

    For i = 1 To Chests_HighIndex
        With Chests(i)
            If .Id > 0 Then
                X = ConvertMapX(.X)
                Y = ConvertMapY(.Y)

                If .State = ChestState_Closed Then
                    SourceX = 0
                    SourceY = 0

                ElseIf .State = ChestState_Open Or .State = ChestState_Empty Then
                    SourceX = 3 * PIC_X
                    SourceY = 3 * PIC_Y
                End If

                Call RenderTexture(Tex_Chest(.Sprite), X, Y, SourceX, SourceY, PIC_X, PIC_Y, PIC_X, PIC_Y, DX8Colour(White, .Alpha))

            End If
        End With
    Next

End Sub

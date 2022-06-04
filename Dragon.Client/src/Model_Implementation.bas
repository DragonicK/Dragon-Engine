Attribute VB_Name = "Model_Implementation"
Option Explicit

Private Const MODEL_WIDTH  As Long = 160
Private Const MODEL_HEIGHT As Long = 160
Private Const MODEL_HALF_WIDTH As Long = 80
Private Const MODEL_HALF_HEIGHT As Long = 80

Public Sub SetPlayerModelIndex(ByVal Index As Long)
    Player(Index).ModelIndex = GetModelIndex(GetPlayerSprite(Index))
End Sub

Public Function GetModelIndex(ByVal ModelId As Long) As Long
    Dim i As Long

    GetModelIndex = 0

    For i = 1 To Model_Count
        If Models(i).Id = ModelId Then
            GetModelIndex = i
            Exit Function
        End If
    Next

End Function

Public Sub RenderMovementLower(ByVal Dir As Byte, ByVal Anim As Long, ByVal X As Long, ByVal Y As Long, ByRef Directions As DirectionRec, Optional ByVal Color As Long = -1)

    Select Case Dir
    Case DIR_UP, DIR_UP_LEFT, DIR_UP_RIGHT
        Call RenderParallaxTexture(Directions.Up.Frames(Anim).Texture, _
                                   ConvertMapX(X), _
                                   ConvertMapY(Y) + MODEL_HALF_HEIGHT, _
                                   0, _
                                   MODEL_HALF_HEIGHT, _
                                   MODEL_WIDTH, _
                                   MODEL_HALF_HEIGHT, _
                                   MODEL_HEIGHT, _
                                   MODEL_HALF_HEIGHT, _
                                   Color)

    Case DIR_RIGHT
        Call RenderParallaxTexture(Directions.Right.Frames(Anim).Texture, _
                                   ConvertMapX(X), _
                                   ConvertMapY(Y) + MODEL_HALF_HEIGHT, _
                                   0, _
                                   MODEL_HALF_HEIGHT, _
                                   MODEL_WIDTH, _
                                   MODEL_HALF_HEIGHT, _
                                   MODEL_HEIGHT, _
                                   MODEL_HALF_HEIGHT, _
                                   Color)

    Case DIR_DOWN, DIR_DOWN_LEFT, DIR_DOWN_RIGHT
        Call RenderParallaxTexture(Directions.Down.Frames(Anim).Texture, _
                                   ConvertMapX(X), _
                                   ConvertMapY(Y) + MODEL_HALF_HEIGHT, _
                                   0, _
                                   MODEL_HALF_HEIGHT, _
                                   MODEL_WIDTH, _
                                   MODEL_HALF_HEIGHT, _
                                   MODEL_HEIGHT, _
                                   MODEL_HALF_HEIGHT, _
                                   Color)

    Case DIR_LEFT
        Call RenderParallaxTexture(Directions.Left.Frames(Anim).Texture, _
                                   ConvertMapX(X), _
                                   ConvertMapY(Y) + MODEL_HALF_HEIGHT, _
                                   0, _
                                   MODEL_HALF_HEIGHT, _
                                   MODEL_WIDTH, _
                                   MODEL_HALF_HEIGHT, _
                                   MODEL_HEIGHT, _
                                   MODEL_HALF_HEIGHT, _
                                   Color)
    End Select

End Sub

Public Sub RenderMovementUpper(ByVal Dir As Byte, ByVal Anim As Long, ByVal X As Long, ByVal Y As Long, ByRef Directions As DirectionRec, Optional ByVal Color As Long = -1)

    Select Case Dir
    Case DIR_UP, DIR_UP_LEFT, DIR_UP_RIGHT
        Call RenderParallaxTexture(Directions.Up.Frames(Anim).Texture, _
                                   ConvertMapX(X), _
                                   ConvertMapY(Y), _
                                   0, _
                                   0, _
                                   MODEL_WIDTH, _
                                   MODEL_HALF_HEIGHT, _
                                   MODEL_WIDTH, _
                                   MODEL_HALF_HEIGHT, _
                                   Color)

    Case DIR_RIGHT
        Call RenderParallaxTexture(Directions.Right.Frames(Anim).Texture, _
                                   ConvertMapX(X), _
                                   ConvertMapY(Y), _
                                   0, _
                                   0, _
                                   MODEL_WIDTH, _
                                   MODEL_HALF_HEIGHT, _
                                   MODEL_WIDTH, _
                                   MODEL_HALF_HEIGHT, _
                                   Color)

    Case DIR_DOWN, DIR_DOWN_LEFT, DIR_DOWN_RIGHT
        Call RenderParallaxTexture(Directions.Down.Frames(Anim).Texture, _
                                   ConvertMapX(X), _
                                   ConvertMapY(Y), _
                                   0, _
                                   0, _
                                   MODEL_WIDTH, _
                                   MODEL_HALF_HEIGHT, _
                                   MODEL_WIDTH, _
                                   MODEL_HALF_HEIGHT, _
                                   Color)

    Case DIR_LEFT
        Call RenderParallaxTexture(Directions.Left.Frames(Anim).Texture, _
                                   ConvertMapX(X), _
                                   ConvertMapY(Y), _
                                   0, _
                                   0, _
                                   MODEL_WIDTH, _
                                   MODEL_HALF_HEIGHT, _
                                   MODEL_WIDTH, _
                                   MODEL_HALF_HEIGHT, _
                                   Color)

    End Select

End Sub

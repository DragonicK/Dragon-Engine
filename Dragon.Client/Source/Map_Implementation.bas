Attribute VB_Name = "Map_Implementation"
Option Explicit

Public Sub RenderMapName()
    Dim zonetype As String, Colour As Long

    If CurrentMap.MapData.Moral = 0 Then
        zonetype = "Pvp Ativo"
        Colour = Red
    ElseIf CurrentMap.MapData.Moral = 1 Then
        zonetype = "Zona Segura"
        Colour = White
    ElseIf CurrentMap.MapData.Moral = 2 Then
        zonetype = "C�mara de Boss"
        Colour = Grey
    End If

    RenderText Font(Fonts.FontRegular), Trim$(CurrentMap.MapData.Name) & " - " & zonetype, ScreenWidth - 15 - TextWidth(Font(Fonts.FontRegular), Trim$(CurrentMap.MapData.Name) & " - " & zonetype), 45, Colour, 255
End Sub

Public Function UpdateMapView() As RECT
    Dim OffsetX As Long, OffSetY As Long, StartX As Long, StartY As Long, EndX As Long, EndY As Long

    OffsetX = 0 + PIC_X
    OffSetY = 0 + PIC_Y
    StartX = CurrentMap.MapData.MaxX - ((TileWidth + 1) \ 2) - 1
    StartY = CurrentMap.MapData.MaxY - ((TileHeight + 1) \ 2) - 1

    If TileWidth + 1 <= CurrentMap.MapData.MaxX Then
        If StartX < 0 Then
            OffsetX = 0

            If StartX = -1 Then
                If Player(MyIndex).xOffset > 0 Then
                    OffsetX = Player(MyIndex).xOffset
                End If
            End If

            StartX = 0
        End If

        EndX = StartX + (TileWidth + 1) + 1

        If EndX > CurrentMap.MapData.MaxX Then
            OffsetX = 32

            If EndX = CurrentMap.MapData.MaxX + 1 Then
                If Player(MyIndex).xOffset < 0 Then
                    OffsetX = Player(MyIndex).xOffset + PIC_X
                End If
            End If

            EndX = CurrentMap.MapData.MaxX
            StartX = EndX - TileWidth - 1
        End If
    Else
        EndX = StartX + (TileWidth + 1) + 1
    End If

    If TileHeight + 1 <= CurrentMap.MapData.MaxY Then
        If StartY < 0 Then
            OffSetY = 0

            If StartY = -1 Then
                If Player(MyIndex).yOffset > 0 Then
                    OffSetY = Player(MyIndex).yOffset
                End If
            End If

            StartY = 0
        End If

        EndY = StartY + (TileHeight + 1) + 1

        If EndY > CurrentMap.MapData.MaxY Then
            OffSetY = 32

            If EndY = CurrentMap.MapData.MaxY + 1 Then
                If Player(MyIndex).yOffset < 0 Then
                    OffSetY = Player(MyIndex).yOffset + PIC_Y
                End If
            End If

            EndY = CurrentMap.MapData.MaxY
            StartY = EndY - TileHeight - 1
        End If
    Else
        EndY = StartY + (TileHeight + 1) + 1
    End If

    If TileWidth + 1 = CurrentMap.MapData.MaxX Then
        OffsetX = 0
    End If

    If TileHeight + 1 = CurrentMap.MapData.MaxY Then
        OffSetY = 0
    End If

    MapView.Top = StartY
    MapView.Bottom = EndY
    MapView.Left = StartX
    MapView.Right = EndX
End Function


Public Sub DrawMapGround()
    Dim i As Long
    Dim PlayerX As Long, PlayerY As Long
    Dim TileX As Long, TileY As Long
    Dim X As Long, Y As Long

    If TileView.Left > 0 Or Camera.Left > 0 Then
        X = TileView.Left * PIC_X + Player(MyIndex).xOffset

        If TileView.Left = 0 Then
            X = X - Player(MyIndex).xOffset + Camera.Left
        Else
            X = X + PIC_X
        End If

        If TileView.Left = MapView.Left Then
            X = TileView.Left * PIC_X + Camera.Left
        End If
    End If

    If TileView.Top > 0 Or Camera.Top > 0 Then
        Y = TileView.Top * PIC_Y + Player(MyIndex).yOffset

        If TileView.Top = 0 Then
            Y = Y - Player(MyIndex).yOffset + Camera.Top
        Else
            Y = Y + PIC_Y
        End If

        If TileView.Bottom = CurrentMap.MapData.MaxY Then
            Y = TileView.Top * PIC_Y + Camera.Top
        End If
    End If
    
    PlayerX = Player(MyIndex).xOffset + (GetPlayerX(MyIndex) * 32)
    PlayerY = Player(MyIndex).yOffset + (GetPlayerY(MyIndex) * 32)

    For i = 1 To MaxGroundParallax
        TileX = GroundParallax(i).X
        TileY = GroundParallax(i).Y

        RenderParallaxTexture GroundParallax(i).Texture, TileX - X, TileY - Y, 0, 0, TileSize, TileSize, TileSize, TileSize
    Next

End Sub

Public Sub DrawMapFringe()
    Dim i As Long
    Dim PlayerX As Long, PlayerY As Long
    Dim TileX As Long, TileY As Long
    Dim X As Long, Y As Long

    If TileView.Left > 0 Or Camera.Left > 0 Then
        X = TileView.Left * PIC_X + Player(MyIndex).xOffset

        If TileView.Left = 0 Then
            X = X - Player(MyIndex).xOffset + Camera.Left
        Else
            X = X + PIC_X
        End If

        If TileView.Left = MapView.Left Then
            X = TileView.Left * PIC_X + Camera.Left
        End If
    End If

    If TileView.Top > 0 Or Camera.Top > 0 Then
        Y = TileView.Top * PIC_Y + Player(MyIndex).yOffset

        If TileView.Top = 0 Then
            Y = Y - Player(MyIndex).yOffset + Camera.Top
        Else
            Y = Y + PIC_Y
        End If

        If TileView.Bottom = CurrentMap.MapData.MaxY Then
            Y = TileView.Top * PIC_Y + Camera.Top
        End If
    End If

    PlayerX = Player(MyIndex).xOffset + (GetPlayerX(MyIndex) * 32)
    PlayerY = Player(MyIndex).yOffset + (GetPlayerY(MyIndex) * 32)

    For i = 1 To MaxGroundParallax
        TileX = FringeParallax(i).X
        TileY = FringeParallax(i).Y

        RenderParallaxTexture FringeParallax(i).Texture, TileX - X, TileY - Y, 0, 0, TileSize, TileSize, TileSize, TileSize
    Next

End Sub

Public Sub RenderParallaxTexture(ByRef Texture As TextureStruct, ByVal X As Long, ByVal Y As Long, ByVal sX As Single, ByVal sY As Single, ByVal W As Long, ByVal H As Long, ByVal sW As Single, ByVal sH As Single, Optional ByVal Colour As Long = -1, Optional ByVal offset As Boolean = False)
    Dim HasTexture As Boolean

    HasTexture = SetParallaxTexture(Texture)
    If HasTexture Then RenderParallaxGeom Texture, X, Y, sX, sY, W, H, sW, sH, Colour, offset
End Sub

Private Function SetParallaxTexture(ByRef Texture As TextureStruct) As Boolean
    If Texture.W > 0 Or Texture.H > 0 Then
        Call D3DDevice.SetTexture(0, Texture.Texture)
        SetParallaxTexture = True
    Else
        Call D3DDevice.SetTexture(0, Nothing)
        SetParallaxTexture = False
    End If
End Function

Private Sub RenderParallaxGeom(ByRef Texture As TextureStruct, ByVal X As Long, ByVal Y As Long, ByVal sX As Single, ByVal sY As Single, ByVal W As Long, ByVal H As Long, ByVal sW As Single, ByVal sH As Single, Optional ByVal Colour As Long = -1, Optional ByVal offset As Boolean = False)
    If W = 0 Then Exit Sub
    If H = 0 Then Exit Sub
    If sW = 0 Then Exit Sub
    If sH = 0 Then Exit Sub

    If mClip.Right <> 0 Then
        If mClip.Top <> 0 Then
            If mClip.Left > X Then
                sX = sX + (mClip.Left - X) / (W / sW)
                sW = sW - (mClip.Left - X) / (W / sW)
                W = W - (mClip.Left - X)
                X = mClip.Left
            End If

            If mClip.Top > Y Then
                sY = sY + (mClip.Top - Y) / (H / sH)
                sH = sH - (mClip.Top - Y) / (H / sH)
                H = H - (mClip.Top - Y)
                Y = mClip.Top
            End If

            If mClip.Right < X + W Then
                sW = sW - (X + W - mClip.Right) / (W / sW)
                W = -X + mClip.Right
            End If

            If mClip.Bottom < Y + H Then
                sH = sH - (Y + H - mClip.Bottom) / (H / sH)
                H = -Y + mClip.Bottom
            End If

            If W <= 0 Then Exit Sub
            If H <= 0 Then Exit Sub
            If sW <= 0 Then Exit Sub
            If sH <= 0 Then Exit Sub
        End If
    End If

    Call GeomCalcParallax(Box, Texture, X, Y, W, H, sX, sY, sW, sH, Colour)
    Call D3DDevice.DrawPrimitiveUP(D3DPT_TRIANGLESTRIP, 2, Box(0), Len(Box(0)))
End Sub

Private Sub GeomCalcParallax(ByRef Geom() As Vertex, ByRef Texture As TextureStruct, ByVal X As Single, ByVal Y As Single, ByVal W As Integer, ByVal H As Integer, ByVal sX As Single, ByVal sY As Single, ByVal sW As Single, ByVal sH As Single, ByVal Colour As Long)
    sW = (sW + sX) / Texture.W + 0.000003
    sH = (sH + sY) / Texture.H + 0.000003
    sX = sX / Texture.W + 0.000003
    sY = sY / Texture.H + 0.000003
    Geom(0) = MakeVertex(X, Y, 0, 1, Colour, 1, sX, sY)
    Geom(1) = MakeVertex(X + W, Y, 0, 1, Colour, 0, sW, sY)
    Geom(2) = MakeVertex(X, Y + H, 0, 1, Colour, 0, sX, sH)
    Geom(3) = MakeVertex(X + W, Y + H, 0, 1, Colour, 0, sW, sH)
End Sub

' BitWise Operators for directional blocking
Public Sub SetDirBlock(ByRef blockvar As Byte, ByRef Dir As Byte, ByVal Block As Boolean)

    If Block Then
        blockvar = blockvar Or (2 ^ Dir)
    Else
        blockvar = blockvar And Not (2 ^ Dir)
    End If

End Sub

Public Function IsDirBlocked(ByRef blockvar As Byte, ByRef Dir As Byte) As Boolean

    If Not blockvar And (2 ^ Dir) Then
        IsDirBlocked = False
    Else
        IsDirBlocked = True
    End If

End Function

Public Function ConvertMapX(ByVal X As Long) As Long
    ConvertMapX = X - (TileView.Left * PIC_X) - Camera.Left
End Function

Public Function ConvertMapY(ByVal Y As Long) As Long
    ConvertMapY = Y - (TileView.Top * PIC_Y) - Camera.Top
End Function

Public Sub UpdateCamera()
    Dim OffsetX As Long, OffSetY As Long, StartX As Long, StartY As Long, EndX As Long, EndY As Long

    OffsetX = Player(MyIndex).xOffset + PIC_X
    OffSetY = Player(MyIndex).yOffset + PIC_Y
    StartX = GetPlayerX(MyIndex) - ((TileWidth + 1) \ 2) - 1
    StartY = GetPlayerY(MyIndex) - ((TileHeight + 1) \ 2) - 1

    If TileWidth + 1 <= CurrentMap.MapData.MaxX Then
        If StartX < 0 Then
            OffsetX = 0

            If StartX = -1 Then
                If Player(MyIndex).xOffset > 0 Then
                    OffsetX = Player(MyIndex).xOffset
                End If
            End If

            StartX = 0
        End If

        EndX = StartX + (TileWidth + 1) + 1
    Else
        EndX = StartX + (TileWidth + 1) + 1
    End If

    If TileHeight + 1 <= CurrentMap.MapData.MaxY Then
        If StartY < 0 Then
            OffSetY = 0

            If StartY = -1 Then
                If Player(MyIndex).yOffset > 0 Then
                    OffSetY = Player(MyIndex).yOffset
                End If
            End If

            StartY = 0
        End If

        EndY = StartY + (TileHeight + 1) + 1
    Else
        EndY = StartY + (TileHeight + 1) + 1
    End If

    If TileWidth + 1 = CurrentMap.MapData.MaxX Then
        OffsetX = 0
    End If

    If TileHeight + 1 = CurrentMap.MapData.MaxY Then
        OffSetY = 0
    End If

    With TileView
        .Top = StartY
        .Bottom = EndY
        .Left = StartX
        .Right = EndX
    End With

    With Camera
        .Top = OffSetY
        .Bottom = .Top + ScreenY
        .Left = OffsetX
        .Right = .Left + ScreenX
    End With

    CurX = TileView.Left + CInt((GlobalX + Camera.Left) \ PIC_X)
    CurY = TileView.Top + CInt((GlobalY + Camera.Top) \ PIC_Y)
    GlobalX_Map = GlobalX + (TileView.Left * PIC_X) + Camera.Left
    GlobalY_Map = GlobalY + (TileView.Top * PIC_Y) + Camera.Top
End Sub

Public Function IsInBounds()
    If (CurX >= 0) Then
        If (CurX <= CurrentMap.MapData.MaxX) Then
            If (CurY >= 0) Then
                If (CurY <= CurrentMap.MapData.MaxY) Then
                    IsInBounds = True
                End If
            End If
        End If
    End If

End Function

Public Function IsValidMapPoint(ByVal X As Long, ByVal Y As Long) As Boolean
    IsValidMapPoint = False

    If X < 0 Then Exit Function
    If Y < 0 Then Exit Function
    If X > CurrentMap.MapData.MaxX Then Exit Function
    If Y > CurrentMap.MapData.MaxY Then Exit Function
    IsValidMapPoint = True
End Function

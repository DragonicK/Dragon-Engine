Attribute VB_Name = "Map_Implementation"
Option Explicit

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
    Dim Right As Long, Bottom As Long
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

    PlayerX = GetPlayerX(MyIndex) * 32
    PlayerY = GetPlayerY(MyIndex) * 32
    Right = Camera.Right * 2
    Bottom = Camera.Bottom * 2

    For i = 1 To MaxGroundParallax
        TileX = GroundParallax(i).X
        TileY = GroundParallax(i).Y

        ' Desenha somente o que pode ser visto.
        If PlayerX < TileX + Right And PlayerY < TileY + Bottom Then
            If TileX < PlayerX + Right And TileY < PlayerY + Bottom Then
                RenderParallaxTexture GroundParallax(i).Texture, TileX - X, TileY - Y, 0, 0, TileSize, TileSize, TileSize, TileSize
            End If
        End If
    Next

End Sub

Public Sub DrawMapFringe()
    Dim i As Long
    Dim PlayerX As Long, PlayerY As Long
    Dim Right As Long, Bottom As Long
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

    PlayerX = GetPlayerX(MyIndex) * 32
    PlayerY = GetPlayerY(MyIndex) * 32
    Right = Camera.Right * 2
    Bottom = Camera.Bottom * 2

    For i = 1 To MaxGroundParallax
        TileX = FringeParallax(i).X
        TileY = FringeParallax(i).Y

        ' Desenha somente o que pode ser visto.
        If PlayerX < TileX + Right And PlayerY < TileY + Bottom Then
            If TileX < PlayerX + Right And TileY < PlayerY + Bottom Then
                RenderParallaxTexture FringeParallax(i).Texture, TileX - X, TileY - Y, 0, 0, TileSize, TileSize, TileSize, TileSize
            End If
        End If
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


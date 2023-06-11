Attribute VB_Name = "DirectX8_Implementation"
Option Explicit

' Public CallSaveScreen As Boolean

' Variables
Public DX8 As DirectX8
Public D3D As Direct3D8
Public D3DX As D3DX8
Public D3DDevice As Direct3DDevice8
Public DXVB As Direct3DVertexBuffer8
Public D3DWindow As D3DPRESENT_PARAMETERS
Public mhWnd As Long

Public Const FVF As Long = D3DFVF_XYZRHW Or D3DFVF_TEX1 Or D3DFVF_DIFFUSE    'Or D3DFVF_SPECULAR

Public Type TextureStruct
    Texture As Direct3DTexture8
    Data() As Byte
    W As Long
    H As Long
End Type

Public Type TextureDataStruct
    Data() As Byte
End Type

Public Type GeomRec
    Top As Long
    Left As Long
    Height As Long
    Width As Long
End Type

Public Type Vertex
    X As Single
    Y As Single
    Z As Single
    RHW As Single
    Colour As Long
    tu As Single
    tv As Single
End Type

Public mClip As RECT
Public Box(0 To 3) As Vertex
Public mTexture() As TextureStruct
Public mTextures As Long
Public CurrentTexture As Long

Public ScreenWidth As Long, ScreenHeight As Long
Attribute ScreenHeight.VB_VarUserMemId = 1073741836
Public TileWidth As Long, TileHeight As Long
Attribute TileWidth.VB_VarUserMemId = 1073741838
Attribute TileHeight.VB_VarUserMemId = 1073741838
Public ScreenX As Long, ScreenY As Long
Attribute ScreenX.VB_VarUserMemId = 1073741840
Attribute ScreenY.VB_VarUserMemId = 1073741840
Public CurResolution As Byte, isFullscreen As Boolean
Attribute CurResolution.VB_VarUserMemId = 1073741842
Attribute isFullscreen.VB_VarUserMemId = 1073741842

Public Sub InitDX8(ByVal hWnd As Long)
    Dim DispMode As D3DDISPLAYMODE, Width As Long, Height As Long

    mhWnd = hWnd

    Set DX8 = New DirectX8
    Set D3D = DX8.Direct3DCreate
    Set D3DX = New D3DX8

    ' set size
    GetResolutionSize CurResolution, Width, Height
    ScreenWidth = Width
    ScreenHeight = Height
    TileWidth = (Width / 32) - 1
    TileHeight = (Height / 32) - 1
    ScreenX = (TileWidth) * PIC_X
    ScreenY = (TileHeight) * PIC_Y

    ' set up window
    Call D3D.GetAdapterDisplayMode(D3DADAPTER_DEFAULT, DispMode)
    DispMode.Format = D3DFMT_A8R8G8B8

    If Options.Fullscreen = 0 Then
        isFullscreen = False
        D3DWindow.SwapEffect = D3DSWAPEFFECT_COPY    ' D3DSWAPEFFECT_COPY
        D3DWindow.hDeviceWindow = hWnd
        D3DWindow.BackBufferFormat = DispMode.Format
        D3DWindow.Windowed = 1
    Else
        isFullscreen = True
        D3DWindow.SwapEffect = D3DSWAPEFFECT_COPY
        D3DWindow.BackBufferCount = 1
        D3DWindow.BackBufferFormat = DispMode.Format
        D3DWindow.BackBufferWidth = ScreenWidth
        D3DWindow.BackBufferHeight = ScreenHeight
        D3DWindow.FullScreen_PresentationInterval = D3DPRESENT_INTERVAL_IMMEDIATE
    End If

    Select Case Options.Render
    Case 1    ' hardware
        If LoadDirectX(D3DCREATE_HARDWARE_VERTEXPROCESSING, hWnd) <> 0 Then
            Options.Fullscreen = 0
            Options.Resolution = 0
            Options.Render = 0
            SaveOptions
            Call MsgBox("Could not initialize DirectX with hardware vertex processing.", vbCritical)
            Call DestroyGame
        End If
    Case 2    ' mixed
        If LoadDirectX(D3DCREATE_MIXED_VERTEXPROCESSING, hWnd) <> 0 Then
            Options.Fullscreen = 0
            Options.Resolution = 0
            Options.Render = 0
            SaveOptions
            Call MsgBox("Could not initialize DirectX with mixed vertex processing.", vbCritical)
            Call DestroyGame
        End If
    Case 3    ' software
        If LoadDirectX(D3DCREATE_SOFTWARE_VERTEXPROCESSING, hWnd) <> 0 Then
            Options.Fullscreen = 0
            Options.Resolution = 0
            Options.Render = 0
            SaveOptions
            Call MsgBox("Could not initialize DirectX with software vertex processing.", vbCritical)
            Call DestroyGame
        End If
    Case Else    ' auto
        If LoadDirectX(D3DCREATE_HARDWARE_VERTEXPROCESSING, hWnd) <> 0 Then
            If LoadDirectX(D3DCREATE_MIXED_VERTEXPROCESSING, hWnd) <> 0 Then
                If LoadDirectX(D3DCREATE_SOFTWARE_VERTEXPROCESSING, hWnd) <> 0 Then
                    Options.Fullscreen = 0
                    Options.Resolution = 0
                    Options.Render = 0
                    SaveOptions
                    Call MsgBox("Could not initialize DirectX.  DX8VB.dll may not be registered.", vbCritical)
                    Call DestroyGame
                End If
            End If
        End If
    End Select

    ' Render states
    Call D3DDevice.SetVertexShader(FVF)

    Call D3DDevice.SetRenderState(D3DRS_EDGEANTIALIAS, False)
    Call D3DDevice.SetRenderState(D3DRS_MULTISAMPLE_ANTIALIAS, False)
    Call D3DDevice.SetRenderState(D3DRS_CULLMODE, D3DCULL_NONE)
    Call D3DDevice.SetRenderState(D3DRS_LIGHTING, False)
    Call D3DDevice.SetRenderState(D3DRS_ALPHABLENDENABLE, True)
    Call D3DDevice.SetRenderState(D3DRS_SRCBLEND, D3DBLEND_SRCALPHA)
    Call D3DDevice.SetRenderState(D3DRS_DESTBLEND, D3DBLEND_INVSRCALPHA)

    Call D3DDevice.SetTextureStageState(0, D3DTSS_ALPHAOP, D3DTOP_MODULATE)
    Call D3DDevice.SetTextureStageState(0, D3DTSS_ALPHAARG2, D3DTA_CURRENT)
    Call D3DDevice.SetTextureStageState(0, D3DTSS_ALPHAARG1, 2)
    Call D3DDevice.SetTextureStageState(0, D3DTSS_MINFILTER, D3DTEXF_POINT)
    Call D3DDevice.SetTextureStageState(0, D3DTSS_MAGFILTER, D3DTEXF_POINT)
    Call D3DDevice.SetStreamSource(0, DXVB, Len(Box(0)))

End Sub

Public Function LoadDirectX(ByVal BehaviourFlags As CONST_D3DCREATEFLAGS, ByVal hWnd As Long) As Long
    On Error GoTo ErrorInit

    Set D3DDevice = D3D.CreateDevice(D3DADAPTER_DEFAULT, D3DDEVTYPE_HAL, hWnd, BehaviourFlags, D3DWindow)
    Exit Function

ErrorInit:
    LoadDirectX = 1
End Function

Sub DestroyDX8()
    Dim i As Long

    'For i = 1 To mTextures
    '    mTexture(i).Data
    'Next

    If Not DX8 Is Nothing Then Set DX8 = Nothing
    If Not D3D Is Nothing Then Set D3D = Nothing
    If Not D3DX Is Nothing Then Set D3DX = Nothing
    If Not D3DDevice Is Nothing Then Set D3DDevice = Nothing
End Sub

Public Sub CheckGFX()
    If D3DDevice.TestCooperativeLevel <> D3D_OK Then
        Do While D3DDevice.TestCooperativeLevel = D3DERR_DEVICELOST
            DoEvents
        Loop
        Call ResetGFX
    End If
End Sub

Public Sub ResetGFX()
    Dim Temp() As TextureDataStruct
    Dim GroundParallaxTemp() As TextureDataStruct
    Dim FringeParallaxTemp() As TextureDataStruct

    Dim i As Long, n As Long
    Dim GroundCount As Long, FringeCount As Long

    n = mTextures
    ReDim Temp(1 To n)
    For i = 1 To n
        Set mTexture(i).Texture = Nothing
        Temp(i).Data = mTexture(i).Data
    Next

    ' Ground
    GroundCount = MaxGroundParallax

    If GroundCount > 0 Then
        ReDim GroundParallaxTemp(1 To GroundCount)

        For i = 1 To GroundCount
            If IsGroundTexturesInitialized() Then
                Set GroundParallax(i).Texture.Texture = Nothing
                GroundParallaxTemp(i).Data = GroundParallax(i).Texture.Data
            End If
        Next
    End If

    ' Fringe
    FringeCount = MaxFringeParallax

    If FringeCount > 0 Then
        ReDim FringeParallaxTemp(1 To FringeCount)

        For i = 1 To FringeCount
            If IsFringeTexturesInitialized() Then
                Set FringeParallax(i).Texture.Texture = Nothing
                FringeParallaxTemp(i).Data = FringeParallax(i).Texture.Data
            End If
        Next
    End If

    Erase mTexture
    mTextures = 0

    Call D3DDevice.Reset(D3DWindow)
    Call D3DDevice.SetVertexShader(FVF)

    Call D3DDevice.SetRenderState(D3DRS_EDGEANTIALIAS, False)
    Call D3DDevice.SetRenderState(D3DRS_MULTISAMPLE_ANTIALIAS, False)
    Call D3DDevice.SetRenderState(D3DRS_CULLMODE, D3DCULL_NONE)
    Call D3DDevice.SetRenderState(D3DRS_LIGHTING, False)
    Call D3DDevice.SetRenderState(D3DRS_ALPHABLENDENABLE, True)
    Call D3DDevice.SetRenderState(D3DRS_SRCBLEND, D3DBLEND_SRCALPHA)
    Call D3DDevice.SetRenderState(D3DRS_DESTBLEND, D3DBLEND_INVSRCALPHA)

    Call D3DDevice.SetTextureStageState(0, D3DTSS_ALPHAOP, D3DTOP_MODULATE)
    Call D3DDevice.SetTextureStageState(0, D3DTSS_ALPHAARG2, D3DTA_CURRENT)
    Call D3DDevice.SetTextureStageState(0, D3DTSS_ALPHAARG1, 2)
    Call D3DDevice.SetTextureStageState(0, D3DTSS_MINFILTER, D3DTEXF_POINT)
    Call D3DDevice.SetTextureStageState(0, D3DTSS_MAGFILTER, D3DTEXF_POINT)
    Call D3DDevice.SetStreamSource(0, DXVB, Len(Box(0)))

    For i = 1 To n
        Call LoadTexture(Temp(i).Data)
    Next

    If IsGroundTexturesInitialized() Then
        For i = 1 To GroundCount
            Call LoadParallaxTexture(GroundParallaxTemp(i).Data, GroundParallax(i).Texture)
        Next
    End If

    If IsFringeTexturesInitialized() Then
        For i = 1 To FringeCount
            Call LoadParallaxTexture(FringeParallaxTemp(i).Data, FringeParallax(i).Texture)
        Next
    End If

End Sub

Public Sub SetTexture(ByVal TextureNum As Long)
    If TextureNum > 0 Then
        Call D3DDevice.SetTexture(0, mTexture(TextureNum).Texture)
        CurrentTexture = TextureNum
    Else
        Call D3DDevice.SetTexture(0, Nothing)
        CurrentTexture = 0
    End If
End Sub

'PositionX, PositionY, CuteX, CuteY, ScaleX, ScaleY, SizeX, SizeY
'(Texture As Long, ByVal PositionX, ByVal PositionY, ByVal PositionY,)
Public Sub RenderTexture(Texture As Long, ByVal PositionX As Long, ByVal PositionY As Long, ByVal SourceX As Single, ByVal SourceY As Single, ByVal Width As Long, ByVal Height As Long, ByVal SourceWidth As Single, ByVal SourceHeight As Single, Optional ByVal Colour As Long = -1, Optional ByVal offset As Boolean = False)
    SetTexture Texture
    RenderGeom PositionX, PositionY, SourceX, SourceY, Width, Height, SourceWidth, SourceHeight, Colour, offset
End Sub

Public Sub RenderGeom(ByVal X As Long, ByVal Y As Long, ByVal sX As Single, ByVal sY As Single, ByVal W As Long, ByVal H As Long, ByVal sW As Single, ByVal sH As Single, Optional ByVal Colour As Long = -1, Optional ByVal offset As Boolean = False)
    Dim i As Long

    If CurrentTexture = 0 Then Exit Sub
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

    Call GeomCalc(Box, CurrentTexture, X, Y, W, H, sX, sY, sW, sH, Colour)
    Call D3DDevice.DrawPrimitiveUP(D3DPT_TRIANGLESTRIP, 2, Box(0), Len(Box(0)))
End Sub

Public Sub GeomCalc(ByRef Geom() As Vertex, ByVal TextureNum As Long, ByVal X As Single, ByVal Y As Single, ByVal W As Integer, ByVal H As Integer, ByVal sX As Single, ByVal sY As Single, ByVal sW As Single, ByVal sH As Single, ByVal Colour As Long)
    sW = (sW + sX) / mTexture(TextureNum).W + 0.000003
    sH = (sH + sY) / mTexture(TextureNum).H + 0.000003
    sX = sX / mTexture(TextureNum).W + 0.000003
    sY = sY / mTexture(TextureNum).H + 0.000003
    Geom(0) = MakeVertex(X, Y, 0, 1, Colour, 1, sX, sY)
    Geom(1) = MakeVertex(X + W, Y, 0, 1, Colour, 0, sW, sY)
    Geom(2) = MakeVertex(X, Y + H, 0, 1, Colour, 0, sX, sH)
    Geom(3) = MakeVertex(X + W, Y + H, 0, 1, Colour, 0, sW, sH)
End Sub

Private Sub GeomSetBox(ByVal X As Single, ByVal Y As Single, ByVal W As Integer, ByVal H As Integer, ByVal Colour As Long)
    Box(0) = MakeVertex(X, Y, 0, 1, Colour, 0, 0, 0)
    Box(1) = MakeVertex(X + W, Y, 0, 1, Colour, 0, 0, 0)
    Box(2) = MakeVertex(X, Y + H, 0, 1, Colour, 0, 0, 0)
    Box(3) = MakeVertex(X + W, Y + H, 0, 1, Colour, 0, 0, 0)
End Sub

Public Function MakeVertex(X As Single, Y As Single, Z As Single, RHW As Single, Colour As Long, Specular As Long, tu As Single, tv As Single) As Vertex
    MakeVertex.X = X
    MakeVertex.Y = Y
    MakeVertex.Z = Z
    MakeVertex.RHW = RHW
    MakeVertex.Colour = Colour
    'MakeVertex.Specular = Specular
    MakeVertex.tu = tu
    MakeVertex.tv = tv
End Function


Public Sub DrawFade()
    RenderTexture Tex_Blank, 0, 0, 0, 0, ScreenWidth, ScreenHeight, 32, 32, DX8Colour(White, FadeAlpha)
End Sub

Public Sub DrawFog()
    Dim fogNum As Long, Colour As Long, X As Long, Y As Long, RenderState As Long
    fogNum = 0

    If fogNum <= 0 Or fogNum > Count_Fog Then Exit Sub
    Colour = D3DColorARGB(64, 255, 255, 255)
    RenderState = 1

    ' render state
    Select Case RenderState

    Case 1    ' Additive
        D3DDevice.SetTextureStageState 0, D3DTSS_COLOROP, D3DTOP_MODULATE
        D3DDevice.SetRenderState D3DRS_DESTBLEND, D3DBLEND_ONE

    Case 2    ' Subtractive
        D3DDevice.SetTextureStageState 0, D3DTSS_COLOROP, D3DTOP_SUBTRACT
        D3DDevice.SetRenderState D3DRS_SRCBLEND, D3DBLEND_ZERO
        D3DDevice.SetRenderState D3DRS_DESTBLEND, D3DBLEND_INVSRCCOLOR
    End Select

    For X = 0 To 4
        For Y = 0 To 3
            'RenderTexture Tex_Fog(fogNum), (x * 256) + fogOffsetX, (y * 256) + fogOffsetY, 0, 0, 256, 256, 256, 256, colour
            RenderTexture Tex_Fog(fogNum), (X * 256), (Y * 256), 0, 0, 256, 256, 256, 256, Colour
        Next
    Next

    ' reset render state
    If RenderState > 0 Then
        D3DDevice.SetRenderState D3DRS_SRCBLEND, D3DBLEND_SRCALPHA
        D3DDevice.SetRenderState D3DRS_DESTBLEND, D3DBLEND_INVSRCALPHA
        D3DDevice.SetTextureStageState 0, D3DTSS_COLOROP, D3DTOP_MODULATE
    End If

End Sub

Public Sub DrawBars()
    Dim Left As Long, Top As Long, Width As Long, Height As Long
    Dim tmpX As Long, tmpY As Long, barWidth As Long, i As Long, NpcNum As Long
    Dim partyIndex As Long

    ' dynamic bar calculations
    Width = mTexture(Tex_Bars).W
    Height = mTexture(Tex_Bars).H / 4

    ' render npc health bars
    For i = 1 To Npc_HighIndex
        NpcNum = MapNpc(i).Num

        ' exists?
        If NpcNum > 0 Then
            ' alive?
            If MapNpc(i).Vital(Vitals.HP) > 0 And MapNpc(i).Vital(Vitals.HP) < MapNpc(i).MaxVital(Vitals.HP) Then
                ' lock to npc
                tmpX = MapNpc(i).X * PIC_X + MapNpc(i).xOffset + 16 - (Width / 2)
                tmpY = MapNpc(i).Y * PIC_Y + MapNpc(i).yOffset + 35

                ' calculate the width to fill
                If Width > 0 Then BarWidth_NpcHP_Max(i) = ((MapNpc(i).Vital(Vitals.HP) / Width) / (MapNpc(i).MaxVital(Vitals.HP) / Width)) * Width

                ' draw bar background
                Top = Height * 1    ' HP bar background
                Left = 0
                RenderTexture Tex_Bars, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, Width, Height, Width, Height

                ' draw the bar proper
                Top = 0    ' HP bar
                Left = 0
                RenderTexture Tex_Bars, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, BarWidth_NpcHP(i), Height, BarWidth_NpcHP(i), Height
            End If
        End If
    Next

    ' check for casting time bar
    If SpellBuffer > 0 Then
        If Skill(PlayerSkill(SpellBuffer).Id).CastTime > 0 Then
            ' lock to player
            tmpX = GetPlayerX(MyIndex) * PIC_X + Player(MyIndex).xOffset + 16 - (Width / 2)
            tmpY = GetPlayerY(MyIndex) * PIC_Y + Player(MyIndex).yOffset + 35 + Height + 1

            ' calculate the width to fill
            If Width > 0 Then barWidth = (GetTickCount - SpellBufferTimer) / ((Skill(PlayerSkill(SpellBuffer).Id).CastTime * 1000)) * Width

            ' draw bar background
            Top = Height * 3    ' cooldown bar background
            Left = 0
            RenderTexture Tex_Bars, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, Width, Height, Width, Height

            ' draw the bar proper
            Top = Height * 2    ' cooldown bar
            Left = 0
            RenderTexture Tex_Bars, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, barWidth, Height, barWidth, Height
        End If
    End If

    ' draw own health bar
    If GetPlayerVital(MyIndex, Vitals.HP) > 0 And GetPlayerVital(MyIndex, Vitals.HP) < GetPlayerMaxVital(MyIndex, Vitals.HP) Then
        ' lock to Player
        tmpX = GetPlayerX(MyIndex) * PIC_X + Player(MyIndex).xOffset + 16 - (Width / 2)
        tmpY = GetPlayerY(MyIndex) * PIC_X + Player(MyIndex).yOffset + 35

        ' calculate the width to fill
        If Width > 0 Then BarWidth_PlayerHP_Max(MyIndex) = ((GetPlayerVital(MyIndex, Vitals.HP) / Width) / (GetPlayerMaxVital(MyIndex, Vitals.HP) / Width)) * Width

        ' draw bar background
        Top = Height * 1    ' HP bar background
        Left = 0
        RenderTexture Tex_Bars, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, Width, Height, Width, Height

        ' draw the bar proper
        Top = 0    ' HP bar
        Left = 0
        RenderTexture Tex_Bars, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, BarWidth_PlayerHP(MyIndex), Height, BarWidth_PlayerHP(MyIndex), Height
    End If
End Sub

Sub DrawMenuBG()
    RenderTexture Tex_Surface(1), 0, 0, 0, 0, ScreenWidth, ScreenHeight, 1920, 1080
End Sub

' Main Loop
Public Sub Render_Graphics()
    Dim X As Long, Y As Long, i As Long, bgColour As Long
    Dim PosX As Long, PosY As Long
    Dim Right As Long, Bottom As Long
    Dim PlayerX As Long, PlayerY As Long

    ' fuck off if we're not doing anything
    If GettingMap Then Exit Sub

    ' update the camera
    UpdateCamera

    ' check graphics
    CheckGFX

    ' Start rendering
    Call D3DDevice.Clear(0, ByVal 0, D3DCLEAR_TARGET, bgColour, 1#, 0)
    Call D3DDevice.BeginScene

    ' Render lower tiles
    Call DrawMapGround

    ' draw animations
    If Count_Anim > 0 Then
        For i = 1 To MAX_BYTE
            If AnimInstance(i).Used(0) Then
                DrawAnimation i, 0
            End If
        Next
    End If

    ' blt the hover icon
    Call DrawTarget
    Call DrawTargetHover

    Call DrawChests

    ' Y-based render. Renders Players, Npcs and Resources based on Y-axis.
    If Model_Count > 0 Then
        PlayerX = GetPlayerX(MyIndex) * 32
        PlayerY = GetPlayerY(MyIndex) * 32
        Right = Camera.Right
        Bottom = Camera.Bottom

        ' Players Lower
        For i = 1 To Player_HighIndex
            If IsPlaying(i) And GetPlayerMap(i) = GetPlayerMap(MyIndex) Then
                Call ProcessPlayerMovements(i)

                PosX = GetPlayerX(i) * 32
                PosY = GetPlayerY(i) * 32

                If PlayerX < PosX + Right And PlayerY < PosY + Bottom Then
                    If PosX < PlayerX + Right And PosY < PlayerY + Bottom Then
                        Call DrawPlayerLower(i)
                    End If
                End If
            End If
        Next

        ' Npcs Lower
        For i = 1 To Npc_HighIndex
            If Not GetNpcDead(i) Then
                Call ProcessNpcMovements(i)

                PosX = MapNpc(i).X * 32
                PosY = MapNpc(i).Y * 32

                If PlayerX < PosX + Right And PlayerY < PosY + Bottom Then
                    If PosX < PlayerX + Right And PosY < PlayerY + Bottom Then
                        Call DrawNpcLower(i)
                    End If
                End If
            End If
        Next

        ' Player Upper
        For i = 1 To Player_HighIndex
            If IsPlaying(i) Then
                PosX = GetPlayerX(i) * 32
                PosY = GetPlayerY(i) * 32

                If PlayerX < PosX + Right And PlayerY < PosY + Bottom Then
                    If PosX < PlayerX + Right And PosY < PlayerY + Bottom Then
                        '  Call CheckForPlayerFrameOutOfRange(i)
                        Call DrawPlayerUpper(i)
                    End If
                End If

            End If
        Next

        ' Npc Upper
        For i = 1 To Npc_HighIndex
            If Not GetNpcDead(i) Then
                PosX = MapNpc(i).X * 32
                PosY = MapNpc(i).Y * 32

                If PlayerX < PosX + Right And PlayerY < PosY + Bottom Then
                    If PosX < PlayerX + Right And PosY < PlayerY + Bottom Then
                        Call DrawNpcUpper(i)
                    End If
                End If
            End If
        Next
    End If


    ' Render upper tiles
    Call DrawMapFringe

    ' render fog
    DrawFog

    ' render animations
    If Count_Anim > 0 Then
        For i = 1 To MAX_BYTE
            If AnimInstance(i).Used(1) Then
                DrawAnimation i, 1
            End If
        Next
    End If

    ' render target
    Call UpdateTargetWindow

    Call DrawPartyActiveIcons

    ' draw the bars
    Call DrawBars

    ' draw player names
    If Not ScreenshotMode Then
        For i = 1 To Player_HighIndex
            If IsPlaying(i) Then
                PosX = GetPlayerX(i) * 32
                PosY = GetPlayerY(i) * 32

                If PlayerX < PosX + Right And PlayerY < PosY + Bottom Then
                    If PosX < PlayerX + Right And PosY < PlayerY + Bottom Then
                        Call DrawPlayerName(i)
                    End If
                End If
            End If
        Next
    End If

    ' draw npc names
    If Not ScreenshotMode Then
        For i = 1 To Npc_HighIndex
            If Not MapNpc(i).Dead Then
                If MapNpc(i).Num > 0 Then
                    PosX = MapNpc(i).X * 32
                    PosY = MapNpc(i).Y * 32

                    If PlayerX < PosX + Right And PlayerY < PosY + Bottom Then
                        If PosX < PlayerX + Right And PosY < PlayerY + Bottom Then
                            Call DrawNpcName(i)
                        End If
                    End If
                End If
            End If
        Next
    End If

    ' draw action msg
    For i = 1 To MAX_BYTE
        DrawActionMsg i
    Next

    ' draw the messages
    For i = 1 To MAX_BYTE
        If ChatBubble(i).Active Then
            DrawChatBubble i
        End If
    Next

    ' draw shadow
    If Not ScreenshotMode Then
        RenderTexture Tex_GUI(29), 0, 0, 0, 0, ScreenWidth, 64, 1, 64
        RenderTexture Tex_GUI(28), 0, ScreenHeight - 64, 0, 0, ScreenWidth, 64, 1, 64
    End If

    ' Draw Player icons
    Call DrawPlayerActiveIcons

    ' Draw dead panel above all windows
    Call UpdateDeadPanel

    ' Render entities
    If Not hideGUI And Not ScreenshotMode Then RenderEntities

    ' render FPS
    If Not ScreenshotMode Then RenderText Font(Fonts.FontRegular), "FPS: " & GameFPS & " Ping: " & Ping, 1, 1, White

    ' draw loc
    If BLoc Then
        RenderText Font(Fonts.FontRegular), Trim$("cur x: " & CurX & " y: " & CurY), 260, 6, Yellow
        RenderText Font(Fonts.FontRegular), Trim$("loc x: " & GetPlayerX(MyIndex) & " y: " & GetPlayerY(MyIndex)), 260, 22, Yellow
        RenderText Font(Fonts.FontRegular), Trim$(" (map #" & GetPlayerMap(MyIndex) & ")"), 260, 38, Yellow
    End If

    ' draw map name
    Call RenderMapName

    ' End the rendering
    Call D3DDevice.EndScene
    Call D3DDevice.Present(ByVal 0, ByVal 0, 0, ByVal 0)

End Sub

Public Sub Render_Menu()
' check graphics
    CheckGFX
    ' Start rendering
    Call D3DDevice.Clear(0, ByVal 0, D3DCLEAR_TARGET, &HFFFFFF, 1#, 0)
    Call D3DDevice.BeginScene

    ' Render menu background
    DrawMenuBG
    ' Render entities
    RenderEntities
    ' render white fade
    DrawFade
    ' End the rendering

    Call D3DDevice.EndScene

    Call D3DDevice.Present(ByVal 0, ByVal 0, 0, ByVal 0)

End Sub

Public Sub CheckResolution()
    Dim Resolution As Byte, Width As Long, Height As Long
    ' find the selected resolution
    Resolution = Options.Resolution
    ' reset
    If Resolution = 0 Then
        Resolution = 12
        ' loop through till we find one which fits
        Do Until ScreenFit(Resolution) Or Resolution > RES_COUNT
            ScreenFit Resolution
            Resolution = Resolution + 1
            DoEvents
        Loop

        ' right resolution
        If Resolution > RES_COUNT Then Resolution = RES_COUNT
        Options.Resolution = Resolution
    End If

    ' size the window
    GetResolutionSize Options.Resolution, Width, Height
    Resize Width, Height

    ' save it
    CurResolution = Options.Resolution

    Call SaveOptions
End Sub

Function ScreenFit(Resolution As Byte) As Boolean
    Dim sWidth As Long, sHeight As Long, Width As Long, Height As Long

    ' exit out early
    If Resolution = 0 Then
        ScreenFit = False
        Exit Function
    End If

    ' get screen size
    sWidth = Screen.Width / Screen.TwipsPerPixelX
    sHeight = Screen.Height / Screen.TwipsPerPixelY

    GetResolutionSize Resolution, Width, Height

    ' check if match
    If Width > sWidth Or Height > sHeight Then
        ScreenFit = False
    Else
        ScreenFit = True
    End If
End Function

Function GetResolutionSize(Resolution As Byte, ByRef Width As Long, ByRef Height As Long)
    Select Case Resolution
    Case 1
        Width = 2560
        Height = 1080
    Case 2
        Width = 1920
        Height = 1080
    Case 3
        Width = 1600
        Height = 900
    Case 4
        Width = 1366
        Height = 768
    Case 5
        Width = 1280
        Height = 720
    End Select
End Function

Public Sub Resize(ByVal Width As Long, ByVal Height As Long)
    frmMain.Width = (frmMain.Width \ 15 - frmMain.ScaleWidth + Width) * 15
    frmMain.Height = (frmMain.Height \ 15 - frmMain.ScaleHeight + Height) * 15
    frmMain.Left = (Screen.Width - frmMain.Width) \ 2
    frmMain.Top = (Screen.Height - frmMain.Height) \ 2
    DoEvents
End Sub

Public Sub SetResolution()
    Dim Width As Long, Height As Long

    CurResolution = Options.Resolution
    GetResolutionSize CurResolution, Width, Height
    Resize Width, Height
    ScreenWidth = Width
    ScreenHeight = Height
    TileWidth = (Width / 32) - 1
    TileHeight = (Height / 32) - 1
    ScreenX = (TileWidth) * PIC_X
    ScreenY = (TileHeight) * PIC_Y
    ResetGFX
    ResizeGUI
End Sub

Public Sub ResizeGUI()
    Dim Top As Long
    Dim WindowIndex As Long
    Dim ControlIndex As Long

    ' move hotbar
    Windows(GetWindowIndex("winHotbar")).Window.Left = ScreenWidth - 430    ' (ScreenWidth / 2) - (Windows(GetWindowIndex("winHotbar")).Window.Width / 2) ' ScreenWidth - 430
    Windows(GetWindowIndex("winHotbar")).Window.Top = 15    ' ScreenHeight - 50

    ' move menu
    ' Windows(GetWindowIndex("winMenu")).Window.Left = ScreenWidth - 236
    ' Windows(GetWindowIndex("winMenu")).Window.Top = ScreenHeight - 37

    ' move invitations
    Windows(GetWindowIndex("winInvite_Party")).Window.Left = ScreenWidth - 234
    Windows(GetWindowIndex("winInvite_Party")).Window.Top = ScreenHeight - 100

    ' loop through
    Top = ScreenHeight - 100
    If Windows(GetWindowIndex("winInvite_Party")).Window.Visible Then
        Top = Top - 37
    End If

    Windows(GetWindowIndex("winInvite_Trade")).Window.Left = ScreenWidth - 234
    Windows(GetWindowIndex("winInvite_Trade")).Window.Top = Top

    ' re-size right-click background
    Windows(GetWindowIndex("winRightClickBG")).Window.Width = ScreenWidth
    Windows(GetWindowIndex("winRightClickBG")).Window.Height = ScreenHeight

    ' re-size black background
    Windows(GetWindowIndex("winBlank")).Window.Width = ScreenWidth
    Windows(GetWindowIndex("winBlank")).Window.Height = ScreenHeight

    ' re-size combo background
    Windows(GetWindowIndex("winComboMenuBG")).Window.Width = ScreenWidth
    Windows(GetWindowIndex("winComboMenuBG")).Window.Height = ScreenHeight

    ' centralise windows
    CentraliseWindow GetWindowIndex("winLogin")

    CentraliseWindow GetWindowIndex("winModels")
    CentraliseWindow GetWindowIndex("winDialogue")
    CentraliseWindow GetWindowIndex("winClasses")
    CentraliseWindow GetWindowIndex("winNewModel")
    CentraliseWindow GetWindowIndex("winEscMenu")
    CentraliseWindow GetWindowIndex("winInventory")
    CentraliseWindow GetWindowIndex("winCharacter")
    CentraliseWindow GetWindowIndex("winSkills")
    CentraliseWindow GetWindowIndex("winOptions")
    CentraliseWindow GetWindowIndex("winShop")
    CentraliseWindow GetWindowIndex("winTrade")
    CentraliseWindow GetWindowIndex("winItemUpgrade")
    CentraliseWindow GetWindowIndex("winCraft")
    CentraliseWindow GetWindowIndex("winAchievement")
    CentraliseWindow GetWindowIndex("winChestItem")


    Resize_WinModelFooter
    Resize_WinLoginFooter
    Resize_WinLoading
    Resize_WinChat

End Sub

Public Sub SaveScreen()
    Dim Palette As PALETTEENTRY
    Dim RECT As RECT
    Dim BackBuffer As Direct3DSurface8

    RECT.Left = 0
    RECT.Top = 0
    RECT.Bottom = ScreenHeight
    RECT.Right = ScreenWidth

    Set BackBuffer = D3DDevice.GetBackBuffer(0, D3DBACKBUFFER_TYPE_MONO)
    Call D3DX.SaveSurfaceToFile(App.Path & "\File.Png", D3DXIFF_PNG, BackBuffer, Palette, RECT)
End Sub







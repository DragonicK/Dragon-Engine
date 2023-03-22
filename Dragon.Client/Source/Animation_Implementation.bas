Attribute VB_Name = "Animation_Implementation"
Option Explicit

Public Sub DrawAnimation(ByVal Index As Long, ByVal Layer As Long)
    Dim Sprite As Integer, sRECT As GeomRec, Width As Long, Height As Long, FrameCount As Long
    Dim X As Long, Y As Long, LockIndex As Long

    If AnimInstance(Index).Animation = 0 Then
        ClearAnimInstance Index
        Exit Sub
    End If

    Sprite = Animation(AnimInstance(Index).Animation).Sprite(Layer)

    If Sprite < 1 Or Sprite > Count_Anim Then Exit Sub
    ' pre-load texture for calculations
    'SetTexture Tex_Anim(Sprite)
    FrameCount = Animation(AnimInstance(Index).Animation).FrameCount(Layer)
    ' total width divided by frame count
    Width = Animation(AnimInstance(Index).Animation).DestWidth(Layer)      'mTexture(Tex_Anim(Sprite)).width / frameCount
    Height = Animation(AnimInstance(Index).Animation).DestHeight(Layer)      'mTexture(Tex_Anim(Sprite)).height

    With sRECT
        .Top = (Height * ((AnimInstance(Index).FrameIndex(Layer) - 1) \ AnimColumns))
        .Height = Height
        .Left = (Width * (((AnimInstance(Index).FrameIndex(Layer) - 1) Mod AnimColumns)))
        .Width = Width
    End With

    ' change x or y if locked
    If AnimInstance(Index).LockType > TargetTypeNone Then    ' if <> none
        ' is a player
        If AnimInstance(Index).LockType = TargetTypePlayer Then
            ' quick save the index
            LockIndex = AnimInstance(Index).LockIndex

            ' check if is ingame
            If IsPlaying(LockIndex) Then

                ' check if on same map
                If GetPlayerMap(LockIndex) = GetPlayerMap(MyIndex) Then
                    ' is on map, is playing, set x & y
                    X = (GetPlayerX(LockIndex) * PIC_X) + 16 - (Width / 2) + Player(LockIndex).xOffset
                    Y = (GetPlayerY(LockIndex) * PIC_Y) + 16 - (Height / 2) + Player(LockIndex).yOffset
                End If
            End If

        ElseIf AnimInstance(Index).LockType = TargetTypeNpc Then
            ' quick save the index
            LockIndex = AnimInstance(Index).LockIndex

            ' check if NPC exists
            If MapNpc(LockIndex).Num > 0 Then
                ' check if alive
                If MapNpc(LockIndex).Vital(Vitals.HP) > 0 Then
                    ' exists, is alive, set x & y
                    X = (MapNpc(LockIndex).X * PIC_X) + 16 - (Width / 2) + MapNpc(LockIndex).xOffset
                    Y = (MapNpc(LockIndex).Y * PIC_Y) + 16 - (Height / 2) + MapNpc(LockIndex).yOffset
                End If
            Else
                ' npc not alive anymore, kill the animation
                ClearAnimInstance Index
                Exit Sub
            End If
        End If

    Else
        ' no lock, default x + y
        X = (AnimInstance(Index).X * 32) + 16 - (Width / 2)
        Y = (AnimInstance(Index).Y * 32) + 16 - (Height / 2)
    End If

    X = ConvertMapX(X)
    Y = ConvertMapY(Y)
    'EngineRenderRectangle Tex_Anim(sprite), x, y, sRECT.left, sRECT.top, sRECT.width, sRECT.height, sRECT.width, sRECT.height, sRECT.width, sRECT.height
    RenderTexture Tex_Anim(Sprite), X, Y, sRECT.Left, sRECT.Top, sRECT.Width, sRECT.Height, sRECT.Width, sRECT.Height
End Sub

Public Sub CheckAnimInstance(ByVal Index As Long)
    Dim Looptime As Long
    Dim Layer As Long
    Dim FrameCount As Long

    ' if doesn't exist then exit sub
    If AnimInstance(Index).Animation <= 0 Then Exit Sub
    If AnimInstance(Index).Animation > MaximumAnimations Then Exit Sub

    For Layer = 0 To 1
        If AnimInstance(Index).Used(Layer) Then
            Looptime = Animation(AnimInstance(Index).Animation).Looptime(Layer)
            FrameCount = Animation(AnimInstance(Index).Animation).FrameCount(Layer)

            ' if zero'd then set so we don't have extra loop and/or frame
            If AnimInstance(Index).FrameIndex(Layer) = 0 Then AnimInstance(Index).FrameIndex(Layer) = 1
            If AnimInstance(Index).LoopIndex(Layer) = 0 Then AnimInstance(Index).LoopIndex(Layer) = 1

            ' check if frame timer is set, and needs to have a frame change
            If AnimInstance(Index).Timer(Layer) + Looptime <= GetTickCount Then

                ' check if out of range
                If AnimInstance(Index).FrameIndex(Layer) >= FrameCount Then
                    AnimInstance(Index).LoopIndex(Layer) = AnimInstance(Index).LoopIndex(Layer) + 1

                    If AnimInstance(Index).LoopIndex(Layer) > Animation(AnimInstance(Index).Animation).LoopCount(Layer) Then
                        AnimInstance(Index).Used(Layer) = False
                    Else
                        AnimInstance(Index).FrameIndex(Layer) = 1
                    End If

                Else
                    AnimInstance(Index).FrameIndex(Layer) = AnimInstance(Index).FrameIndex(Layer) + 1
                End If

                AnimInstance(Index).Timer(Layer) = GetTickCount
            End If
        End If
    Next

    ' if neither layer is used, clear
    If AnimInstance(Index).Used(0) = False And AnimInstance(Index).Used(1) = False Then ClearAnimInstance (Index)
End Sub


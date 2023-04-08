Attribute VB_Name = "Target_Implementation"
Option Explicit

Private Const TargetWidth = 64
Private Const TargetHeight = 32

Private TargetTick As Long
Private TargetFrameIndex As Long

Public Sub UpdateTargetCalculation()
    If TargetFrameIndex = 0 Then TargetFrameIndex = 1

    If GetTickCount >= TargetTick + 70 Then
        TargetFrameIndex = TargetFrameIndex + 1
        TargetTick = GetTickCount

        If TargetFrameIndex > Count_Target Then
            TargetFrameIndex = 1
        End If
    End If
End Sub

Public Sub DrawTarget()
    Dim X As Long, Y As Long

    If MyTargetIndex > 0 Then
        If MyTargetType = TargetTypePlayer Then
            X = (Player(MyTargetIndex).X * 32) + Player(MyTargetIndex).xOffset
            Y = (Player(MyTargetIndex).Y * 32) + Player(MyTargetIndex).yOffset
        ElseIf MyTargetType = TargetTypeNpc Then
            X = (MapNpc(MyTargetIndex).X * 32) + MapNpc(MyTargetIndex).xOffset
            Y = (MapNpc(MyTargetIndex).Y * 32) + MapNpc(MyTargetIndex).yOffset
        End If
    Else
        Exit Sub
    End If

    X = X - 16
    Y = Y + 9
    X = ConvertMapX(X)
    Y = ConvertMapY(Y)

    RenderParallaxTexture TargetTexture(TargetFrameIndex), X, Y, 0, 0, TargetWidth, TargetHeight, TargetWidth, TargetHeight
End Sub

Public Sub DrawTargetHover()
    Dim i As Long, X As Long, Y As Long

    If Dialogue.Index > 0 Then Exit Sub

    For i = 1 To Player_HighIndex
        If IsPlaying(i) And GetPlayerMap(MyIndex) = GetPlayerMap(i) Then
            X = (Player(i).X * 32) + Player(i).xOffset + 32
            Y = (Player(i).Y * 32) + Player(i).yOffset + 32

            If X >= GlobalX_Map And X <= GlobalX_Map + 32 Then
                If Y >= GlobalY_Map And Y <= GlobalY_Map + 32 Then
                    X = X - 48
                    Y = Y - 23
                    X = ConvertMapX(X)
                    Y = ConvertMapY(Y)

                    RenderParallaxTexture TargetTexture(TargetFrameIndex), X, Y, 0, 0, TargetWidth, TargetHeight, TargetWidth, TargetHeight, D3DColorARGB(140, 255, 255, 255)
                End If
            End If
        End If
    Next

    For i = 1 To Npc_HighIndex
        If MapNpc(i).Num > 0 Then
            If Not GetNpcDead(i) Then
                X = (MapNpc(i).X * 32) + MapNpc(i).xOffset + 32
                Y = (MapNpc(i).Y * 32) + MapNpc(i).yOffset + 32

                If X >= GlobalX_Map And X <= GlobalX_Map + 32 Then
                    If Y >= GlobalY_Map And Y <= GlobalY_Map + 32 Then
                        X = X - 48
                        Y = Y - 23
                        X = ConvertMapX(X)
                        Y = ConvertMapY(Y)

                        RenderParallaxTexture TargetTexture(TargetFrameIndex), X, Y, 0, 0, TargetWidth, TargetHeight, TargetWidth, TargetHeight, D3DColorARGB(140, 255, 255, 255)
                    End If
                End If
            End If
        End If
    Next

End Sub

Public Sub FindNearestTarget()
    Dim i As Long, X As Long, Y As Long, x2 As Long, y2 As Long, xDif As Long, yDif As Long
    Dim bestX As Long, bestY As Long, bestIndex As Long
    x2 = GetPlayerX(MyIndex)
    y2 = GetPlayerY(MyIndex)
    bestX = 255
    bestY = 255

    For i = 1 To Npc_HighIndex

        If MapNpc(i).Num > 0 Then
            If Not GetNpcDead(i) Then
                X = MapNpc(i).X
                Y = MapNpc(i).Y

                ' find the difference - x
                If X < x2 Then
                    xDif = x2 - X
                ElseIf X > x2 Then
                    xDif = X - x2
                Else
                    xDif = 0
                End If

                ' find the difference - y
                If Y < y2 Then
                    yDif = y2 - Y
                ElseIf Y > y2 Then
                    yDif = Y - y2
                Else
                    yDif = 0
                End If

                ' best so far?
                If (xDif + yDif) < (bestX + bestY) Then
                    bestX = xDif
                    bestY = yDif
                    bestIndex = i
                End If
            End If
        End If

    Next

    ' target the best
    If bestIndex > 0 And bestIndex <> MyTargetIndex Then
        SendPlayerTarget bestIndex, TargetTypeNpc
        Call OpenTargetWindow
    End If
End Sub

Public Sub FindTarget()
    Dim i As Long, X As Long, Y As Long

    ' check players
    For i = 1 To Player_HighIndex

        If IsPlaying(i) And GetPlayerMap(MyIndex) = GetPlayerMap(i) Then
            X = (GetPlayerX(i) * 32) + Player(i).xOffset + 32
            Y = (GetPlayerY(i) * 32) + Player(i).yOffset + 32

            If X >= GlobalX_Map And X <= GlobalX_Map + 32 Then
                If Y >= GlobalY_Map And Y <= GlobalY_Map + 32 Then

                    If MyTargetType = TargetTypeLoot And MyTargetIndex > 0 Then
                        Call SendCloseLoot
                    End If

                    ' found our target!
                    SendPlayerTarget i, TargetTypePlayer
                    Call OpenTargetWindow

                    Exit Sub
                End If
            End If
        End If

    Next

    ' check npcs
    For i = 1 To Npc_HighIndex

        If MapNpc(i).Num > 0 Then
            If Not GetNpcDead(i) Then
                X = (MapNpc(i).X * 32) + MapNpc(i).xOffset + 32
                Y = (MapNpc(i).Y * 32) + MapNpc(i).yOffset + 32

                If X >= GlobalX_Map And X <= GlobalX_Map + 32 Then
                    If Y >= GlobalY_Map And Y <= GlobalY_Map + 32 Then

                        If MyTargetType = TargetTypeLoot And MyTargetIndex > 0 Then
                            Call SendCloseLoot
                        End If

                        ' found our target!
                        SendPlayerTarget i, TargetTypeNpc
                        Call OpenTargetWindow
                        Exit Sub
                    End If
                End If
            End If
        End If
    Next

    ' case else, close if its open
    Call CloseTargetWindow

End Sub

Public Sub ShouldCloseTargetWindow(ByVal TargetType As Long, ByVal TargetIndex As Long)
    If MyTargetType = TargetType Then
        If MyTargetIndex = TargetIndex Then
            Call SendPlayerTarget(0, TargetTypeNone)
            Call CloseTargetWindow
        End If
    End If
End Sub


Attribute VB_Name = "ActiveIcon_Implemetation"
Option Explicit

Public Function IsActiveIconSelected(StartX As Long, StartY As Long, ByVal Width As Long, ByVal Heigth As Long, ByVal IconIndex As Long) As Boolean
    Dim TempRec As RECT

    With TempRec
        .Top = StartY + ActiveIconTop + ((ActiveIconOffsetY + Heigth) * ((IconIndex - 1) \ ActiveIconColumns))
        .Bottom = .Top + Heigth
        .Left = StartX + ActiveIconLeft + ((ActiveIconOffsetX + Width) * (((IconIndex - 1) Mod ActiveIconColumns)))
        .Right = .Left + Width
    End With

    If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
        If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
            IsActiveIconSelected = True
            Exit Function
        End If
    End If

End Function

Public Function GetActiveIconIndex(StartX As Long, StartY As Long, ByVal Width As Long, ByVal Heigth As Long, ByRef ActiveIcon As EntityActiveIconRec) As Long
    Dim TempRec As RECT
    Dim i As Long, Count As Long
    Dim IconIndex As Long

    Count = ActiveIcon.ActiveIconCount

    For i = 1 To Count
        IconIndex = ActiveIcon.ActiveIconIndex(i)

        With TempRec
            .Top = StartY + ActiveIconTop + ((ActiveIconOffsetY + Heigth) * ((IconIndex - 1) \ ActiveIconColumns))
            .Bottom = .Top + Heigth
            .Left = StartX + ActiveIconLeft + ((ActiveIconOffsetX + Width) * (((IconIndex - 1) Mod ActiveIconColumns)))
            .Right = .Left + Width
        End With

        If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
            If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
                GetActiveIconIndex = IconIndex
                Exit Function
            End If
        End If
    Next

End Function

Public Sub DrawTargetActiveIcons()
    Dim WindowIndex As Long

    WindowIndex = GetWindowIndex("winTarget")

    If Not Windows(WindowIndex).Window.Visible Then
        Exit Sub
    End If

    Dim i As Long
    Dim Count As Long
    Dim IconId As Long
    Dim IconIndex As Long
    Dim DurationText As String
    Dim Duration As Long
    Dim x As Long, Y As Long
    Dim Index As Long

    Index = MyTargetIndex

    If Index < 1 Or Index > MaxPlayers Then
        Exit Sub
    End If

    Dim Inventory As InventoryRec

    x = Windows(WindowIndex).Window.Left + 5
    Y = Windows(WindowIndex).Window.Top + 40

    Count = 1

    If MyTargetType = TargetTypePlayer Then
        For i = 1 To PlayerActiveIcon(Index).ActiveIconCount
            IconIndex = PlayerActiveIcon(Index).ActiveIconIndex(i)
            IconId = GetPlayerIconId(Index, IconIndex)

            If IconId > 0 Then
                ' Verifica se é necessário desenhar o tempo.
                If GetPlayerIconDurationType(Index, IconIndex) = IconDurationType.IconDurationType_Limited Then
                    Duration = GetPlayerIconDuration(Index, IconIndex)

                    If Duration <= 60 Then
                        DurationText = Duration
                    ElseIf Duration > 60 And Duration <= 3600 Then
                        DurationText = CInt(Duration / 60) & " M"
                    ElseIf Duration > 3600 Then
                        DurationText = CInt((Duration / 60) / 60) & " H"
                    End If
                Else
                    DurationText = vbNullString
                End If

                Call DrawActiveIcon(x, Y, GetPlayerIconType(Index, IconIndex), IconId, DurationText, Count, 24, 24)

                If IsActiveIconSelected(x, Y, 24, 24, Count) Then
                    Select Case GetPlayerIconType(Index, IconIndex)
                    Case IconType.IconType_Skill

                    Case IconType.IconType_Item
                        Inventory.Num = GetPlayerIconId(Index, IconIndex)
                        Inventory.Level = GetPlayerIconLevel(Index, IconIndex)

                        Call ShowItemDesc(x, Y + 25, Inventory)

                    Case IconType.IconType_Effect
                        Call ShowAttributeEffectDesc(x, Y + 25, MyTargetIndex, MyTargetType, 0, IconIndex)

                    Case IconType.IconType_Custom
                           Call ShowNotificationIconDescription(x - 95, Y + 100, GetNotificationIcon(GetPlayerIconId(Index, IconIndex)))

                    End Select
                End If

                Count = Count + 1

            End If
        Next

    End If


    If MyTargetType = TargetTypeNpc Then
        For i = 1 To MapNpcActiveIcon(Index).ActiveIconCount
            IconIndex = MapNpcActiveIcon(Index).ActiveIconIndex(i)
            IconId = GetNpcIconId(Index, IconIndex)

            If IconId > 0 Then
                ' Verifica se é necessário desenhar o tempo.
                If GetNpcIconDurationType(Index, IconIndex) = IconDurationType.IconDurationType_Limited Then
                    Duration = GetNpcIconDuration(Index, IconIndex)

                    If Duration <= 60 Then
                        DurationText = Duration
                    ElseIf Duration > 60 And Duration <= 3600 Then
                        DurationText = CInt(Duration / 60) & " M"
                    ElseIf Duration > 3600 Then
                        DurationText = CInt((Duration / 60) / 60) & " H"
                    End If
                Else
                    DurationText = vbNullString
                End If

                Call DrawActiveIcon(x, Y, GetNpcIconType(Index, IconIndex), IconId, DurationText, Count, 24, 24)

                If IsActiveIconSelected(x, Y, 24, 24, Count) Then
                    Select Case GetNpcIconType(Index, IconIndex)
                    Case IconType.IconType_Skill

                    Case IconType.IconType_Item
                        Inventory.Num = GetNpcIconId(Index, IconIndex)
                        Inventory.Level = GetNpcIconLevel(Index, IconIndex)

                        Call ShowItemDesc(x, Y + 25, Inventory)

                    Case IconType.IconType_Effect
                        Call ShowAttributeEffectDesc(x, Y + 25, MyTargetIndex, MyTargetType, 0, IconIndex)

                    Case IconType.IconType_Custom
                        Call ShowNotificationIconDescription(x - 95, Y + 100, GetNotificationIcon(GetNpcIconId(Index, IconIndex)))

                    End Select
                End If

                Count = Count + 1
            End If
        Next
    End If

End Sub

Public Sub DrawActiveIcon(ByVal x As Long, ByVal Y As Long, ByVal ActiveIconType As IconType, ByVal ObjectId As Long, DurationText As String, ByVal PositionCount As Long, ByVal Width As Long, ByVal Heigth As Long)
    Dim Left As Long, Top As Long, IconId As Long

    Top = Y + ActiveIconTop + ((ActiveIconOffsetY + Heigth) * ((PositionCount - 1) \ ActiveIconColumns))
    Left = x + ActiveIconLeft + ((ActiveIconOffsetX + Width) * (((PositionCount - 1) Mod ActiveIconColumns)))

    Select Case ActiveIconType
    Case IconType.IconType_Item
        IconId = Item(ObjectId).IconId

        If IconId > 0 Then
            RenderTexture Tex_Item(IconId), Left, Top, 0, 0, Width, Heigth, 32, 32
        End If

    Case IconType.IconType_Skill
        IconId = Skill(ObjectId).IconId

        If IconId > 0 Then
            RenderTexture Tex_Spellicon(IconId), Left, Top, 0, 0, Width, Heigth, 32, 32
        End If

    Case IconType.IconType_Effect
        IconId = AttributeEffect(ObjectId).IconId

        If IconId > 0 Then
            RenderTexture Tex_Spellicon(IconId), Left, Top, 0, 0, Width, Heigth, 32, 32
        End If

    Case IconType.IconType_Custom
        Dim Notification As NotificationIconRec

        Notification = GetNotificationIcon(ObjectId)

        If Notification.Id > 0 Then
            IconId = Notification.IconId

            If Notification.IconType = NotificationIconType_Item Then
                If IconId > 0 Then
                    RenderTexture Tex_Item(IconId), Left, Top, 0, 0, Width, Heigth, 32, 32
                End If

            ElseIf Notification.IconType = NotificationIconType_Skill Then
                If IconId > 0 Then
                    RenderTexture Tex_Spellicon(IconId), Left, Top, 0, 0, Width, Heigth, 32, 32
                End If

            ElseIf Notification.IconType = NotificationIconType_Custom Then

            End If
        End If

    End Select

    If IconId > 0 Then
        RenderText Font(Fonts.FontRegular), DurationText, Left - 3, Top - 15, ColorType.Gold
    End If

End Sub

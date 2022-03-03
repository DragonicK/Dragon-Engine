Attribute VB_Name = "ActiveIcon_Implementation_Player"
Option Explicit

Public Sub ClearPlayerActiveIcon(ByVal Index As Long, ByVal Slot As Long)
    PlayerActiveIcon(Index).Icons(Slot).Id = 0
    PlayerActiveIcon(Index).Icons(Slot).Level = 0
    PlayerActiveIcon(Index).Icons(Slot).Duration = 0
    PlayerActiveIcon(Index).Icons(Slot).SkillType = IconSkillType_None
    PlayerActiveIcon(Index).Icons(Slot).IconType = IconType_None
    PlayerActiveIcon(Index).Icons(Slot).DurationType = IconDurationType_Unlimited
End Sub

Public Function GetPlayerIconType(ByVal Index As Long, ByVal Slot As Long) As IconType
    GetPlayerIconType = PlayerActiveIcon(Index).Icons(Slot).IconType
End Function
Public Sub SetPlayerIconType(ByVal Index As Long, ByVal Slot As Long, ByVal Value As IconType)
    PlayerActiveIcon(Index).Icons(Slot).IconType = Value
End Sub
Public Function GetPlayerIconSkillType(ByVal Index As Long, ByVal Slot As Long, ByVal Value As Long) As IconSkillType
    GetPlayerIconSkillType = PlayerActiveIcon(Index).Icons(Slot).SkillType
End Function
Public Sub SetPlayerIconSkillType(ByVal Index As Long, ByVal Slot As Long, ByVal Value As IconSkillType)
    PlayerActiveIcon(Index).Icons(Slot).SkillType = Value
End Sub
Public Function GetPlayerIconDurationType(ByVal Index As Long, ByVal Slot As Long) As IconDurationType
    GetPlayerIconDurationType = PlayerActiveIcon(Index).Icons(Slot).DurationType
End Function
Public Sub SetPlayerIconDurationType(ByVal Index As Long, ByVal Slot As Long, ByVal Value As IconDurationType)
    PlayerActiveIcon(Index).Icons(Slot).DurationType = Value
End Sub
Public Function GetPlayerIconId(ByVal Index As Long, ByVal Slot As Long) As Long
    GetPlayerIconId = PlayerActiveIcon(Index).Icons(Slot).Id
End Function
Public Sub SetPlayerIconId(ByVal Index As Long, ByVal Slot As Long, ByVal Value As Long)
    PlayerActiveIcon(Index).Icons(Slot).Id = Value
End Sub
Public Function GetPlayerIconDuration(ByVal Index As Long, ByVal Slot As Long) As Long
    GetPlayerIconDuration = PlayerActiveIcon(Index).Icons(Slot).Duration
End Function
Public Sub SetPlayerIconDuration(ByVal Index As Long, ByVal Slot As Long, ByVal Value As Long)
    PlayerActiveIcon(Index).Icons(Slot).Duration = Value
End Sub
Public Function GetPlayerIconLevel(ByVal Index As Long, ByVal Slot As Long) As Long
    GetPlayerIconLevel = PlayerActiveIcon(Index).Icons(Slot).Level
End Function
Public Sub SetPlayerIconLevel(ByVal Index As Long, ByVal Slot As Long, ByVal Value As Long)
    PlayerActiveIcon(Index).Icons(Slot).Level = Value
End Sub
Public Function GetPlayerIconExhibitionType(ByVal Index As Long, ByVal Slot As Long) As IconExhibitionType
    GetPlayerIconExhibitionType = PlayerActiveIcon(Index).Icons(Slot).Exhibition
End Function
Public Sub SetPlayerIconExhibitionType(ByVal Index As Long, ByVal Slot As Long, ByVal Exhibition As IconExhibitionType)
    PlayerActiveIcon(Index).Icons(Slot).Exhibition = Exhibition
End Sub

Public Function FindPlayerFreeIconSlot(ByVal Index As Long) As Long
    Dim i As Long

    For i = 1 To MaxEntityActiveIcon
        If GetPlayerIconId(Index, i) = 0 Then
            FindPlayerFreeIconSlot = i
            Exit Function
        End If
    Next

End Function

Public Function FindPlayerIconSlot(ByVal Index As Long, ByVal IconId As Long, ByVal IconType As IconType) As Long
    Dim i As Long

    FindPlayerIconSlot = 0

    For i = 1 To MaxEntityActiveIcon
        If GetPlayerIconId(Index, i) = IconId And GetPlayerIconType(Index, i) = IconType Then
            FindPlayerIconSlot = i
            Exit Function
        End If
    Next

End Function

Public Sub CalculatePlayerIconRemainTime()
    Dim i As Long, Index As Long
    Dim Count As Long, IconIndex As Long
    Dim ListEnabled As Boolean

    For Index = 1 To Player_HighIndex
        ' Checa se é possível percorrer a lista.
        ListEnabled = PlayerActiveIcon(Index).ListEnabled

        If ListEnabled Then
            ' Percorre somente os indices alocados.
            Count = PlayerActiveIcon(Index).ActiveIconCount

            For i = 1 To Count
                IconIndex = PlayerActiveIcon(Index).ActiveIconIndex(i)

                If GetPlayerIconId(Index, IconIndex) > 0 Then
                    If GetPlayerIconDuration(Index, IconIndex) > 0 Then
                        Call SetPlayerIconDuration(Index, IconIndex, GetPlayerIconDuration(Index, IconIndex) - 1)
                    End If
                End If
            Next

        End If
    Next
End Sub

Public Sub AllocatePlayerActiveIconIndex(ByVal Index As Long)
    Dim i As Long, Count As Long

    PlayerActiveIcon(Index).ListEnabled = False

    For i = 1 To MaxEntityActiveIcon
        If GetPlayerIconId(Index, i) > 0 Then
            Count = Count + 1
            PlayerActiveIcon(Index).ActiveIconIndex(Count) = i
        End If
    Next

    PlayerActiveIcon(Index).ActiveIconCount = Count
    PlayerActiveIcon(Index).ListEnabled = True
End Sub

Public Sub DrawPlayerActiveIcons()
    Dim i As Long, IconId As Long, Duration As Long
    Dim X As Long, Y As Long, IconIndex As Long

    Dim Count As Long
    Dim DurationText As String
    Dim WindowIndex As Long
    Dim Inventory As InventoryRec

    WindowIndex = GetWindowIndex("winHotbar")
    Count = 1

    X = Windows(WindowIndex).Window.Left
    Y = Windows(WindowIndex).Window.Top

    For i = 1 To PlayerActiveIcon(MyIndex).ActiveIconCount
        IconIndex = PlayerActiveIcon(MyIndex).ActiveIconIndex(i)
        IconId = GetPlayerIconId(MyIndex, IconIndex)

        ' Se houver algum ícone.
        If IconId > 0 Then
            ' Verifica se é necessário desenhar o tempo.
            If GetPlayerIconDurationType(MyIndex, IconIndex) = IconDurationType.IconDurationType_Limited Then
                Duration = GetPlayerIconDuration(MyIndex, IconIndex)

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

            Call DrawActiveIcon(X, Y + PositiveActiveIconTop, GetPlayerIconType(MyIndex, IconIndex), IconId, DurationText, Count, 32, 32)

            ' Verifica se o ponteiro do mouse está em cima do ícone.
            If IsActiveIconSelected(X, Y + PositiveActiveIconTop, 32, 32, Count) Then
                Select Case GetPlayerIconType(MyIndex, IconIndex)
                Case IconType.IconType_Skill

                Case IconType.IconType_Item
                    Inventory.Num = GetPlayerIconId(MyIndex, IconIndex)
                    Inventory.Level = GetPlayerIconLevel(MyIndex, IconIndex)

                    Call ShowItemDesc(X - 98, Y + 100, Inventory)

                Case IconType.IconType_Effect
                    Call ShowAttributeEffectDesc(X - 95, Y + 100, MyIndex, TargetTypePlayer, 0, IconIndex)

                Case IconType.IconType_Custom
                    Call ShowNotificationIconDescription(X - 95, Y + 100, GetNotificationIcon(GetPlayerIconId(MyIndex, IconIndex)))

                End Select
            End If

            Count = Count + 1

        End If
    Next

End Sub

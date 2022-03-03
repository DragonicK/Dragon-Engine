Attribute VB_Name = "ActiveIcon_Implementation_Party"
Option Explicit

Public Sub ClearPartyMemberActiveIcon(ByVal Index As Long, ByVal Slot As Long)
    PartyActiveIcon(Index).Icons(Slot).Id = 0
    PartyActiveIcon(Index).Icons(Slot).Level = 0
    PartyActiveIcon(Index).Icons(Slot).Duration = 0
    PartyActiveIcon(Index).Icons(Slot).SkillType = IconSkillType_None
    PartyActiveIcon(Index).Icons(Slot).IconType = IconType_None
    PartyActiveIcon(Index).Icons(Slot).Exhibition = IconExhibitionType_Player
    PartyActiveIcon(Index).Icons(Slot).DurationType = IconDurationType_Unlimited
End Sub

Public Sub SetPartyIconType(ByVal MemberIndex As Long, ByVal Slot As Long, ByVal Value As IconType)
    PartyActiveIcon(MemberIndex).Icons(Slot).IconType = Value
End Sub
Public Function GetPartyIconType(ByVal MemberIndex As Long, ByVal Slot As Long) As IconType
    GetPartyIconType = PartyActiveIcon(MemberIndex).Icons(Slot).IconType
End Function
Public Sub SetPartyIconSkillType(ByVal MemberIndex As Long, ByVal Slot As Long, ByVal Value As IconSkillType)
    PartyActiveIcon(MemberIndex).Icons(Slot).SkillType = Value
End Sub
Public Function GetPartyIconSkillType(ByVal MemberIndex As Long, ByVal Slot As Long) As IconSkillType
    GetPartyIconSkillType = PartyActiveIcon(MemberIndex).Icons(Slot).SkillType
End Function
Public Sub SetPartyIconDurationType(ByVal MemberIndex As Long, ByVal Slot As Long, ByVal Value As IconDurationType)
    PartyActiveIcon(MemberIndex).Icons(Slot).DurationType = Value
End Sub
Public Function GetPartyIconDurationType(ByVal MemberIndex As Long, ByVal Slot As Long) As IconDurationType
    GetPartyIconDurationType = PartyActiveIcon(MemberIndex).Icons(Slot).DurationType
End Function
Public Function GetPartyIconExhibitionType(ByVal MemberIndex As Long, ByVal Slot As Long) As IconExhibitionType
    GetPartyIconExhibitionType = PartyActiveIcon(MemberIndex).Icons(Slot).Exhibition
End Function
Public Sub SetPartyIconExhibitionType(ByVal MemberIndex As Long, ByVal Slot As Long, ByVal Value As IconExhibitionType)
    PartyActiveIcon(MemberIndex).Icons(Slot).Exhibition = Value
End Sub
Public Sub SetPartyIconId(ByVal MemberIndex As Long, ByVal Slot As Long, ByVal Value As Long)
    PartyActiveIcon(MemberIndex).Icons(Slot).Id = Value
End Sub
Public Function GetPartyIconId(ByVal MemberIndex As Long, ByVal Slot As Long) As Long
    GetPartyIconId = PartyActiveIcon(MemberIndex).Icons(Slot).Id
End Function
Public Sub SetPartyIconLevel(ByVal MemberIndex As Long, ByVal Slot As Long, ByVal Value As Long)
    PartyActiveIcon(MemberIndex).Icons(Slot).Level = Value
End Sub
Public Function GetPartyIconLevel(ByVal MemberIndex As Long, ByVal Slot As Long) As Long
    GetPartyIconLevel = PartyActiveIcon(MemberIndex).Icons(Slot).Level
End Function
Public Sub SetPartyIconDuration(ByVal MemberIndex As Long, ByVal Slot As Long, ByVal Value As Long)
    PartyActiveIcon(MemberIndex).Icons(Slot).Duration = Value
End Sub
Public Function GetPartyIconDuration(ByVal MemberIndex As Long, ByVal Slot As Long) As Long
    GetPartyIconDuration = PartyActiveIcon(MemberIndex).Icons(Slot).Duration
End Function

Public Function FindPartyFreeIconSlot(ByVal MemberIndex As Long) As Long
    Dim i As Long

    For i = 1 To MaxEntityActiveIcon
        If PartyActiveIcon(MemberIndex).Icons(i).Id = 0 Then
            FindPartyFreeIconSlot = i
            Exit Function
        End If
    Next

End Function

Public Function FindPartyIconSlot(ByVal MemberIndex As Long, ByVal IconId As Long, ByVal IconType As IconType) As Long
    Dim i As Long

    FindPartyIconSlot = 0

    For i = 1 To MaxEntityActiveIcon
        If PartyActiveIcon(MemberIndex).Icons(i).Id = IconId And PartyActiveIcon(MemberIndex).Icons(i).IconType = IconType Then
            FindPartyIconSlot = i
            Exit Function
        End If
    Next

End Function

' Processa o tempo restante de cada buff no grupo.
Public Sub CalculatePartyIconRemainTime()
    Dim MemberIndex As Long, i As Long
    Dim ListEnabled As Boolean
    Dim Count As Long, IconIndex As Long

    For MemberIndex = 1 To MaximumPartyMembers
        If Party.Member(MemberIndex).Index > 0 Then

            ListEnabled = MapNpcActiveIcon(MemberIndex).ListEnabled

            If ListEnabled Then
                ' Percorre somente os indices alocados.
                Count = PartyActiveIcon(MemberIndex).ActiveIconCount

                For i = 1 To Count
                    IconIndex = PartyActiveIcon(MemberIndex).ActiveIconIndex(i)

                    If GetPartyIconId(MemberIndex, IconIndex) > 0 Then
                        If GetPartyIconDuration(MemberIndex, IconIndex) > 0 Then
                            Call SetPartyIconDuration(MemberIndex, IconIndex, GetPartyIconDuration(MemberIndex, IconIndex) - 1)
                        End If
                    End If
                Next
            End If
        End If
    Next

End Sub

Public Sub AllocatePartyActiveIconIndex(ByVal Index As Long)
    Dim i As Long, Count As Long

    PartyActiveIcon(Index).ListEnabled = False

    For i = 1 To MaxEntityActiveIcon
        If GetPartyIconId(Index, i) > 0 Then
            Count = Count + 1
            PartyActiveIcon(Index).ActiveIconIndex(Count) = i
        End If
    Next

    PartyActiveIcon(Index).ActiveIconCount = Count
    PartyActiveIcon(Index).ListEnabled = True

End Sub

Public Sub DrawPartyActiveIcons()
    If Party.Leader = 0 Then
        Exit Sub
    End If

    Dim WindowIndex As Long
    WindowIndex = GetWindowIndex("winParty")

    If Not Windows(WindowIndex).Window.Visible Then
        Exit Sub
    End If

    Dim Inventory As InventoryRec
    Dim n As Long, i As Long, Count As Long, PlayerCount As Long
    Dim X As Long, Y As Long, PlayerIndex As Long
    Dim Duration As Long, DurationText As String
    Dim IconIndex As Long

    X = Windows(WindowIndex).Window.Left
    Y = Windows(WindowIndex).Window.Top + 25

    For n = 1 To MaximumPartyMembers

        If Party.Member(n).Index > 0 Then
            If Not Party.Member(n).Disconnected Then
                If Party.Member(n).Name <> GetPlayerName(MyIndex) Then

                    PlayerCount = PlayerCount + 1
                    Count = 1

                    For i = 1 To PartyActiveIcon(n).ActiveIconCount
                        IconIndex = PartyActiveIcon(n).ActiveIconIndex(i)

                        If GetPartyIconId(n, IconIndex) > 0 Then

                            ' Verifica se é necessário desenhar o tempo.
                            If GetPartyIconDurationType(n, IconIndex) = IconDurationType.IconDurationType_Limited Then
                                Duration = GetPartyIconDuration(n, IconIndex)

                                If Duration <= 60 Then
                                    DurationText = Duration
                                ElseIf Duration > 60 And Duration <= 3600 Then
                                    DurationText = CInt(Duration / 60) & "M"
                                ElseIf Duration > 3600 Then
                                    DurationText = CInt((Duration / 60) / 60) & "H"
                                End If
                            Else
                                DurationText = vbNullString
                            End If

                            Call DrawActiveIcon(X + 25, Y + ((PlayerCount - 1) * 95), GetPartyIconType(n, IconIndex), GetPartyIconId(n, IconIndex), DurationText, Count, 24, 24)

                            ' Verifica se o ponteiro do mouse está em cima do ícone.
                            If IsActiveIconSelected(X + 25, Y + ((PlayerCount - 1) * 95), 24, 24, Count) Then
                                Select Case GetPartyIconType(n, IconIndex)
                                Case IconType.IconType_Skill

                                Case IconType.IconType_Item
                                    Inventory.Num = GetPartyIconId(n, IconIndex)
                                    Inventory.Level = GetPartyIconLevel(n, IconIndex)

                                    Call ShowItemDesc(X - 98, Y + 100, Inventory)

                                Case IconType.IconType_Effect
                                    Call ShowAttributeEffectDesc(X + 25, Y + ((PlayerCount - 1) * 95), 0, 0, n, IconIndex)

                                Case IconType.IconType_Custom
                                    Call ShowNotificationIconDescription(X - 95, Y + 100, GetNotificationIcon(GetPartyIconId(n, IconIndex)))

                                End Select
                            End If

                            ' Vai para o próximo buff.
                            Count = Count + 1

                        End If    ' Party Member's Name
                    Next

                End If    ' Disconnect
            End If
        End If
    Next


End Sub

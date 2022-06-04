Attribute VB_Name = "ActiveIcon_Implementation_MapNpc"
Option Explicit

Public Sub ClearNpcActiveIcon(ByVal Index As Long, ByVal Slot As Long)
    MapNpcActiveIcon(Index).Icons(Slot).Id = 0
    MapNpcActiveIcon(Index).Icons(Slot).Level = 0
    MapNpcActiveIcon(Index).Icons(Slot).Duration = 0
    MapNpcActiveIcon(Index).Icons(Slot).SkillType = IconSkillType_None
    MapNpcActiveIcon(Index).Icons(Slot).IconType = IconType_None
    MapNpcActiveIcon(Index).Icons(Slot).Exhibition = IconExhibitionType_Player
    MapNpcActiveIcon(Index).Icons(Slot).DurationType = IconDurationType_Unlimited
End Sub

Public Function GetNpcIconType(ByVal Index As Long, ByVal Slot As Long) As IconType
    GetNpcIconType = MapNpcActiveIcon(Index).Icons(Slot).IconType
End Function
Public Sub SetNpcIconType(ByVal Index As Long, ByVal Slot As Long, ByVal Value As IconType)
    MapNpcActiveIcon(Index).Icons(Slot).IconType = Value
End Sub
Public Function GetNpcIconSkillType(ByVal Index As Long, ByVal Slot As Long) As IconSkillType
    GetNpcIconSkillType = MapNpcActiveIcon(Index).Icons(Slot).SkillType
End Function
Public Sub SetNpcIconSkillType(ByVal Index As Long, ByVal Slot As Long, ByVal Value As IconSkillType)
    MapNpcActiveIcon(Index).Icons(Slot).SkillType = Value
End Sub
Public Function GetNpcIconDurationType(ByVal Index As Long, ByVal Slot As Long) As IconDurationType
    GetNpcIconDurationType = MapNpcActiveIcon(Index).Icons(Slot).DurationType
End Function
Public Sub SetNpcIconDurationType(ByVal Index As Long, ByVal Slot As Long, ByVal Value As IconDurationType)
    MapNpcActiveIcon(Index).Icons(Slot).DurationType = Value
End Sub
Public Function GetNpcIconExhibitionType(ByVal Index As Long, ByVal Slot As Long) As IconExhibitionType
    GetNpcIconExhibitionType = MapNpcActiveIcon(Index).Icons(Slot).Exhibition
End Function
Public Sub SetNpcIconExhibitionType(ByVal Index As Long, ByVal Slot As Long, ByVal Value As IconExhibitionType)
    MapNpcActiveIcon(Index).Icons(Slot).Exhibition = Value
End Sub
Public Function GetNpcIconId(ByVal Index As Long, ByVal Slot As Long) As Long
    GetNpcIconId = MapNpcActiveIcon(Index).Icons(Slot).Id
End Function
Public Sub SetNpcIconId(ByVal Index As Long, ByVal Slot As Long, ByVal Value As Long)
    MapNpcActiveIcon(Index).Icons(Slot).Id = Value
End Sub
Public Function GetNpcIconDuration(ByVal Index As Long, ByVal Slot As Long) As Long
    GetNpcIconDuration = MapNpcActiveIcon(Index).Icons(Slot).Duration
End Function
Public Sub SetNpcIconDuration(ByVal Index As Long, ByVal Slot As Long, ByVal Value As Long)
    MapNpcActiveIcon(Index).Icons(Slot).Duration = Value
End Sub
Public Function GetNpcIconLevel(ByVal Index As Long, ByVal Slot As Long) As Long
    GetNpcIconLevel = MapNpcActiveIcon(Index).Icons(Slot).Level
End Function
Public Sub SetNpcIconLevel(ByVal Index As Long, ByVal Slot As Long, ByVal Value As Long)
    MapNpcActiveIcon(Index).Icons(Slot).Level = Value
End Sub

Public Function FindNpcFreeIconSlot(ByVal Index As Long) As Long
    Dim i As Long

    For i = 1 To MaxEntityActiveIcon
        If MapNpcActiveIcon(Index).Icons(i).Id = 0 Then
            FindNpcFreeIconSlot = i
            Exit Function
        End If
    Next

End Function

Public Function FindNpcIconSlot(ByVal Index As Long, ByVal IconId As Long, ByVal IconType As IconType) As Long
    Dim i As Long

    For i = 1 To MaxEntityActiveIcon
        If MapNpcActiveIcon(Index).Icons(i).Id = IconId And MapNpcActiveIcon(Index).Icons(i).IconType = IconType Then
            FindNpcIconSlot = i
            Exit Function
        End If
    Next

End Function

Public Sub CalculateNpcIconRemainTime()
    Dim i As Long, Index As Long
    Dim Count As Long, IconIndex As Long
    Dim ListEnabled As Boolean

    For Index = 1 To Npc_HighIndex
        If Not MapNpc(Index).Dead Then
            ListEnabled = MapNpcActiveIcon(Index).ListEnabled

            If ListEnabled Then
                ' Percorre somente os indices alocados.
                Count = MapNpcActiveIcon(Index).ActiveIconCount

                For i = 1 To Count
                    IconIndex = MapNpcActiveIcon(Index).ActiveIconIndex(i)

                    If GetNpcIconId(Index, IconIndex) > 0 Then
                        If GetNpcIconDuration(Index, IconIndex) > 0 Then
                            Call SetNpcIconDuration(Index, IconIndex, GetNpcIconDuration(Index, IconIndex) - 1)
                        End If
                    End If
                Next
            End If
        End If
    Next
End Sub

Public Sub AllocateNpcActiveIconIndex(ByVal Index As Long)
    Dim i As Long, Count As Long

    MapNpcActiveIcon(Index).ListEnabled = False

    For i = 1 To MaxEntityActiveIcon
        If GetNpcIconId(Index, i) > 0 Then
            Count = Count + 1
            MapNpcActiveIcon(Index).ActiveIconIndex(Count) = i
        End If
    Next

    MapNpcActiveIcon(Index).ActiveIconCount = Count
    MapNpcActiveIcon(Index).ListEnabled = True
End Sub

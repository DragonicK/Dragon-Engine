Attribute VB_Name = "ActiveIcon_Packet"
Option Explicit

Private Type ReceivedActiveIconRec
    IconType As IconType
    SkillType As IconSkillType
    DurationType As IconDurationType
    Operation As IconOperationType
    Exhibition As IconExhibitionType
    Id As Long
    Duration As Long
    Level As Long
End Type

Public Sub HandleDisplayIcons(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim ActiveIcon As ReceivedActiveIconRec
    Dim TargetType As ActiveIconTargetType

    Dim Buffer As clsBuffer, Count As Long, i As Long
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    Index = Buffer.ReadLong
    TargetType = Buffer.ReadLong

    Count = Buffer.ReadLong

    For i = 1 To Count
        ActiveIcon.IconType = Buffer.ReadLong
        ActiveIcon.SkillType = Buffer.ReadLong
        ActiveIcon.DurationType = Buffer.ReadLong
        ActiveIcon.Operation = Buffer.ReadLong
        ActiveIcon.Exhibition = Buffer.ReadLong
        ActiveIcon.Id = Buffer.ReadLong
        ActiveIcon.Level = Buffer.ReadLong
        ActiveIcon.Duration = Buffer.ReadLong

        If TargetType = ActiveIconTargetType_Npc Then
            Call ProcessNpc(Index + 1, ActiveIcon)

        ElseIf TargetType = ActiveIconTargetType_Party Then
            Call ProcessParty(Index, ActiveIcon)

        ElseIf TargetType = ActiveIconTargetType_Player Then
            Call ProcessPlayer(Index, ActiveIcon)

        End If
    Next

    Set Buffer = Nothing
End Sub

Public Sub HandleDisplayIcon(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim ActiveIcon As ReceivedActiveIconRec
    Dim TargetType As ActiveIconTargetType
    
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    Index = Buffer.ReadLong
    TargetType = Buffer.ReadLong
    
    ActiveIcon.IconType = Buffer.ReadLong
    ActiveIcon.SkillType = Buffer.ReadLong
    ActiveIcon.DurationType = Buffer.ReadLong
    ActiveIcon.Operation = Buffer.ReadLong
    ActiveIcon.Exhibition = Buffer.ReadLong
    ActiveIcon.Id = Buffer.ReadLong
    ActiveIcon.Level = Buffer.ReadLong
    ActiveIcon.Duration = Buffer.ReadLong
        
    Set Buffer = Nothing

    If TargetType = ActiveIconTargetType_Npc Then
        Call ProcessNpc(Index + 1, ActiveIcon)

    ElseIf TargetType = ActiveIconTargetType_Party Then
        Call ProcessParty(Index, ActiveIcon)
        
    ElseIf TargetType = ActiveIconTargetType_Player Then
        Call ProcessPlayer(Index, ActiveIcon)

    End If

End Sub

Private Sub ProcessPlayer(ByVal Index As Long, ByRef Icon As ReceivedActiveIconRec)
    Dim Slot As Long

    ' Procura o slot para atualizar o efeito.
    If Icon.Operation = IconOperationType.IconOperationType_Update Then
        ' Procura por um slot vazio.
        Slot = FindPlayerIconSlot(Index, Icon.Id, Icon.IconType)

        ' Se não encontrado, procura por um slot livre.
        If Slot = 0 Then
            Slot = FindPlayerFreeIconSlot(Index)
        End If
    End If

    ' Procura um efeito para remove-lo.
    If Icon.Operation = IconOperationType.IconOperationType_Remove Then
        Slot = FindPlayerIconSlot(Index, Icon.Id, Icon.IconType)

        If Slot > 0 Then
            Call ClearPlayerActiveIcon(Index, Slot)
            Call AllocatePlayerActiveIconIndex(Index)
            Exit Sub
        End If
    End If

    If Slot > 0 Then
        Call SetPlayerIconType(Index, Slot, Icon.IconType)
        Call SetPlayerIconSkillType(Index, Slot, Icon.SkillType)
        Call SetPlayerIconDurationType(Index, Slot, Icon.DurationType)
        Call SetPlayerIconExhibitionType(Index, Slot, Icon.Exhibition)
        Call SetPlayerIconId(Index, Slot, Icon.Id)
        Call SetPlayerIconLevel(Index, Slot, Icon.Level)
        Call SetPlayerIconDuration(Index, Slot, Icon.Duration)

        Call AllocatePlayerActiveIconIndex(Index)
    End If
End Sub

Private Sub ProcessNpc(ByVal Index As Long, ByRef Icon As ReceivedActiveIconRec)
    Dim Slot As Long
    
    If Icon.Operation = IconOperationType.IconOperationType_Update Then
        Slot = FindNpcIconSlot(Index, Icon.Id, Icon.IconType)

        ' Se não encontrado, procura por um slot livre.
        If Slot = 0 Then
            Slot = FindNpcFreeIconSlot(Index)
        End If
    End If

    If Icon.Operation = IconOperationType.IconOperationType_Remove Then
        Slot = FindNpcIconSlot(Index, Icon.Id, Icon.IconType)

        If Slot > 0 Then
            Call ClearNpcActiveIcon(Index, Slot)
            Call AllocateNpcActiveIconIndex(Index)
            Exit Sub
        End If
    End If

    If Slot > 0 Then
        Call SetNpcIconType(Index, Slot, Icon.IconType)
        Call SetNpcIconSkillType(Index, Slot, Icon.SkillType)
        Call SetNpcIconDurationType(Index, Slot, Icon.DurationType)
        Call SetNpcIconExhibitionType(Index, Slot, Icon.Exhibition)
        Call SetNpcIconId(Index, Slot, Icon.Id)
        Call SetNpcIconLevel(Index, Slot, Icon.Level)
        Call SetNpcIconDuration(Index, Slot, Icon.Duration)

        Call AllocateNpcActiveIconIndex(Index)
    End If

End Sub

Private Sub ProcessParty(ByVal Index As Long, ByRef Icon As ReceivedActiveIconRec)
    Dim Slot As Long, i As Long

    For i = 1 To MaximumPartyMembers
        If Party.Member(i).Index = Index Then
        
            If Icon.Operation = IconOperationType.IconOperationType_Update Then
                Slot = FindPartyIconSlot(i, Icon.Id, Icon.IconType)

                ' Se não encontrado, procura por um slot livre.
                If Slot = 0 Then
                    Slot = FindPartyFreeIconSlot(i)
                End If
            ElseIf Icon.Operation = IconOperationType.IconOperationType_Remove Then
                Slot = FindPartyIconSlot(i, Icon.Id, Icon.IconType)

                If Slot > 0 Then
                    Call ClearPartyMemberActiveIcon(Index, Slot)
                    Call AllocatePartyActiveIconIndex(Index)
                    Exit Sub
                End If
            End If

            If Slot > 0 Then
                Call SetPartyIconType(i, Slot, Icon.IconType)
                Call SetPartyIconSkillType(i, Slot, Icon.SkillType)
                Call SetPartyIconDurationType(i, Slot, Icon.DurationType)
                Call SetPartyIconExhibitionType(i, Slot, Icon.Exhibition)
                Call SetPartyIconId(i, Slot, Icon.Id)
                Call SetPartyIconLevel(i, Slot, Icon.Level)
                Call SetPartyIconDuration(i, Slot, Icon.Duration)

                Call AllocatePartyActiveIconIndex(Index)
            End If

            Exit For
        End If
    Next
End Sub













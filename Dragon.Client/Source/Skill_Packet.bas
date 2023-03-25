Attribute VB_Name = "Skill_Packet"
Option Explicit

Public Sub HandleSkill(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long, Count As Long
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Count = Buffer.ReadLong

    ' Clear all skills data.
    Call ClearPlayerSkills

    ' Read the new data.
    For i = 1 To Count
        PlayerSkill(i).Id = Buffer.ReadLong
        PlayerSkill(i).Level = Buffer.ReadLong
    Next

    Dim Id As Long
    Dim Level As Long

    ' Update passive effects to skills if exists.
    For i = 1 To MaxPlayerSkill
        Id = PlayerPassive(i).Id
        Level = PlayerPassive(i).Level

        If Id >= 1 And Id <= MaximumSkills Then
            Call ApplyPassiveEffect(Id, Level)
        End If
    Next

    Buffer.Flush
    Set Buffer = Nothing
End Sub

Public Sub HandleSkillUpdate(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long, Id As Long, Level As Long
    Dim Buffer As clsBuffer, Found As Boolean

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Found = False

    Id = Buffer.ReadLong
    Level = Buffer.ReadLong

    ' Find the skill to be updted.
    For i = 1 To MaxPlayerSkill
        If PlayerSkill(i).Id = Id Then
            PlayerSkill(i).Level = Level
            Found = True
            Exit For
        End If
    Next

    ' If not found, add new.
    If Not Found Then
        For i = 1 To MaxPlayerSkill
            If PlayerSkill(i).Id = 0 Then
                PlayerSkill(i).Id = Id
                PlayerSkill(i).Level = Level
                Exit For
            End If
        Next
    End If

    ' Clear all skills data and reallocate data.
    For i = 1 To MaxPlayerSkill
        Call ClearPlayerSkillData(i)
    Next

    ' Update passive effects to skills if exists.
    For i = 1 To MaxPlayerSkill
        Id = PlayerPassive(i).Id
        Level = PlayerPassive(i).Level

        If Id >= 1 And Id <= MaximumSkills Then
            Call ApplyPassiveEffect(Id, Level)
        End If
    Next

    Buffer.Flush
    Set Buffer = Nothing
End Sub

Public Sub HandlePassive(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long, Count As Long
    Dim Id As Long, Level As Long
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Count = Buffer.ReadLong

    ' Clear all passive data.
    Call ClearPlayerPassives

    ' Clear all skills data and reallocate data.
    For i = 1 To MaxPlayerSkill
        Call ClearPlayerSkillData(i)
    Next

    ' Read the new data.
    For i = 1 To Count
        Id = Buffer.ReadLong
        Level = Buffer.ReadLong

        PlayerPassive(i).Id = Id
        PlayerPassive(i).Level = Level
        
        ' If exists, update it.
        If Id >= 1 And Id <= MaximumSkills Then
            Call ApplyPassiveEffect(Id, Level)
        End If
    Next

    Buffer.Flush
    Set Buffer = Nothing
End Sub

Public Sub HandlePassiveUpdate(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Id As Long, Level As Long, Found As Boolean
    Dim Buffer As clsBuffer
    Dim i As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Found = False

    Id = Buffer.ReadLong
    Level = Buffer.ReadLong

    ' Find the passive to be updted.
    For i = 1 To MaxPlayerSkill
        If PlayerPassive(i).Id = Id Then
            PlayerPassive(i).Level = Level
            Found = True
            Exit For
        End If
    Next

    ' If not found, add new.
    If Not Found Then
        For i = 1 To MaxPlayerSkill
            If PlayerPassive(i).Id = 0 Then
                PlayerPassive(i).Id = Id
                PlayerPassive(i).Level = Level
                Exit For
            End If
        Next
    End If

    ' Clear all skills data and reallocate data.
    For i = 1 To MaxPlayerSkill
        Call ClearPlayerSkillData(i)
    Next

    ' Update passive effects to skills if exists.
    For i = 1 To MaxPlayerSkill
        Id = PlayerPassive(i).Id
        Level = PlayerPassive(i).Level

        If Id >= 1 And Id <= MaximumSkills Then
            Call ApplyPassiveEffect(Id, Level)
        End If
    Next

    Buffer.Flush
    Set Buffer = Nothing
End Sub

Public Sub HandleSkillCooldown(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim Slot As Long
    
    Set Buffer = New clsBuffer
    
    Buffer.WriteBytes Data()
    
    Slot = Buffer.ReadLong
    
    Slot = Slot + 1
    
    SpellCd(Slot) = GetTickCount
    
    Set Buffer = Nothing
End Sub


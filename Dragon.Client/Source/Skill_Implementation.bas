Attribute VB_Name = "Skill_Implementation"
Option Explicit

Public Const MaxPlayerSkill As Long = 85

Public PlayerPassive(1 To MaxPlayerSkill) As PlayerPassiveRec
Public PlayerSkill(1 To MaxPlayerSkill) As PlayerSkillRec

Public Type PlayerSkillRec
    Id As Long
    Level As Long
    LastPassiveId As Long
    TargetType As SkillTargetType
    Element As Elements
    Amplification As Single
    Range As Integer
    CastTime As Integer
    Cooldown As Integer
    Stun As Integer
    Cost As Long
    ' Diminui em 2 o tamanho, o campo SkillEffectType.None e SkillEffectType_Count não são contabilizados.
    Effect(1 To SkillEffectType.SkillEffectType_Count - 1) As SkillEffectRec
End Type

Public Type PlayerPassiveRec
    Id As Long
    Level As Long
End Type

Public Sub UpdatePassiveEffect(ByVal SkillSlot As Long)
    Dim i As Long, TargetId As Long
    Dim PassiveId As Long
    Dim Id As Long
    Dim Level As Long

    Id = PlayerSkill(SkillSlot).Id
    Level = PlayerSkill(SkillSlot).Level

    If Id < 1 Or Id > MaximumSkills Then
        Exit Sub
    End If

    For i = 1 To MaxPlayerSkill
        PassiveId = PlayerPassive(i).Id

        If PassiveId > 0 Then
            If Passive(PassiveId).SkillTargetId = Id Then
                Call ApplyPassiveEffect(PassiveId, Level)
            End If
        End If
    Next

End Sub

' Aplica o efeito da habilidade passiva na habilidade ativa (se houver)
Public Sub ApplyPassiveEffect(ByVal SkillId As Long, ByVal Level As Long)
    Dim i As Long, TargetId As Long
    Dim PassiveId As Long

    PassiveId = Skill(SkillId).PassiveSkillId

    If PassiveId = 0 Then
        Exit Sub
    End If

    If Passive(PassiveId).Type = PassiveType_Improvement Then
        TargetId = Passive(PassiveId).SkillTargetId

        For i = 1 To MaxPlayerSkill
            If PlayerSkill(i).Id = TargetId Then

                PlayerSkill(i).LastPassiveId = PassiveId
                PlayerSkill(i).TargetType = Passive(PassiveId).TargetType
                PlayerSkill(i).Element = Passive(PassiveId).Element
                PlayerSkill(i).Amplification = PlayerSkill(i).Amplification + CSng(Level * Passive(PassiveId).Amplification)
                PlayerSkill(i).Range = PlayerSkill(i).Range + (Level * Passive(PassiveId).Range)
                PlayerSkill(i).CastTime = PlayerSkill(i).CastTime + (Level * Passive(PassiveId).CastTime)
                PlayerSkill(i).Cooldown = PlayerSkill(i).Cooldown + (Level * Passive(PassiveId).Cooldown)
                PlayerSkill(i).Stun = PlayerSkill(i).Stun + (Level * Passive(PassiveId).Stun)
                PlayerSkill(i).Cost = PlayerSkill(i).Cost + (Level * Passive(PassiveId).Cost)
                
                ' Adiciona o efeito na habilidade se houver.
                If Passive(PassiveId).Effect.EffectType > SkillEffectType_None Then
                    Call UpdateSkillEffect(i, PassiveId, Level)
                End If

            End If
        Next
    End If

End Sub

' Adiciona as melhorias da habilidade passiva para a habilidade ativa.
Private Sub UpdateSkillEffect(ByVal SkillSlot As Long, ByVal PassiveId As Long, ByVal Level As Long)
    Dim Index As SkillEffectType

    Index = Passive(PassiveId).Effect.EffectType
    
    ' Altera os dados do efeito.
    PlayerSkill(SkillSlot).Effect(Index).VitalType = Passive(PassiveId).Effect.VitalType
    PlayerSkill(SkillSlot).Effect(Index).TargetType = Passive(PassiveId).Effect.TargetType
    
    ' Adiciona as melhorias na habilidade.
    PlayerSkill(SkillSlot).Effect(Index).Damage = PlayerSkill(SkillSlot).Effect(Index).Damage + (Level * Passive(PassiveId).Effect.Damage)
    PlayerSkill(SkillSlot).Effect(Index).Duration = PlayerSkill(SkillSlot).Effect(Index).Duration + (Level * Passive(PassiveId).Effect.Duration)
    PlayerSkill(SkillSlot).Effect(Index).Interval = PlayerSkill(SkillSlot).Effect(Index).Interval + (Level * Passive(PassiveId).Effect.Interval)
    PlayerSkill(SkillSlot).Effect(Index).StunDuration = PlayerSkill(SkillSlot).Effect(Index).StunDuration + (Level * Passive(PassiveId).Effect.StunDuration)
    
    ' Altera o efeito de buff.
    If Passive(PassiveId).Effect.EffectId > 0 Then
        PlayerSkill(SkillSlot).Effect(Index).EffectId = Passive(PassiveId).Effect.EffectId
    End If
End Sub

Public Sub ClearPlayerPassives()
    Dim i As Long
    
    For i = 1 To MaxPlayerSkill
        PlayerPassive(i).Id = 0
        PlayerPassive(i).Level = 0
    Next
End Sub

Public Sub ClearPlayerSkills()
    Dim i As Long

    For i = 1 To MaxPlayerSkill
       Call ZeroMemory(ByVal VarPtr(PlayerSkill(i)), LenB(PlayerSkill(i)))
    Next

End Sub

Public Sub ClearPlayerSkillData(ByVal Index As Long)
    Dim Id As Long
    Dim Level As Long
    
    Id = PlayerSkill(Index).Id
    Level = PlayerSkill(Index).Level
    
    Call ZeroMemory(ByVal VarPtr(PlayerSkill(Index)), LenB(PlayerSkill(Index)))
    
    PlayerSkill(Index).Id = Id
    PlayerSkill(Index).Level = Level
End Sub

Public Sub AllocateSkillData(ByVal Index As Long)
    Dim Id As Long, i As Long
    Dim Level As Long
    Dim CurIndex As Long

    Id = PlayerSkill(Index).Id
    Level = PlayerSkill(Index).Level - 1

    If Id >= 1 And Id <= MaximumSkills Then
        With Skill(Id)
            PlayerSkill(Index).TargetType = .TargetType
            PlayerSkill(Index).Element = .ElementType
            PlayerSkill(Index).Amplification = .Amplification + (Level * .AmplificationPerLevel)
            PlayerSkill(Index).Range = .Range
            PlayerSkill(Index).CastTime = .CastTime
            PlayerSkill(Index).Cooldown = .Cooldown
            PlayerSkill(Index).Stun = .StunDuration
            PlayerSkill(Index).Cost = .Cost

            For i = 1 To .EffectCount
                CurIndex = .Effect(i).EffectType

                PlayerSkill(Index).Effect(CurIndex).VitalType = .Effect(i).VitalType
                PlayerSkill(Index).Effect(CurIndex).TargetType = .Effect(i).TargetType
                PlayerSkill(Index).Effect(CurIndex).Damage = .Effect(i).Damage + (Level * .Effect(i).DamagePerLevel)
                PlayerSkill(Index).Effect(CurIndex).Duration = Level * .Effect(i).Duration
                PlayerSkill(Index).Effect(CurIndex).Interval = Level * .Effect(i).Interval
                PlayerSkill(Index).Effect(CurIndex).StunDuration = Level * .Effect(i).StunDuration
                PlayerSkill(Index).Effect(CurIndex).EffectId = .Effect(i).EffectId
            Next

        End With

    End If

End Sub

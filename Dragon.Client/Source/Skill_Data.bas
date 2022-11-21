Attribute VB_Name = "Skill_Data"
Option Explicit

Public MaximumSkills As Long
Public Skill() As SkillRec

Public Enum SkillType
    SkillType_Active
    SkillType_Passive
End Enum

Public Enum SkillAttributeType
    SkillAttributeType_None
    SkillAttributeType_Physic
    SkillAttributeType_Magic
End Enum

Public Enum SkillCostType
    SkillCostType_None
    SkillCostType_HP
    SkillCostType_MP
    SkillCostType_Special
End Enum

Public Enum SkillVitalType
    SkillVitalType_None
    SkillVitalType_HP
    SkillVitalType_MP
    SkillVitalType_Special
End Enum

Public Enum SkillTargetType
    SkillTargetType_None
    SkillTargetType_Caster
    SkillTargetType_Single
    SkillTargetType_AoE
    SkillTargetType_Group
End Enum

Public Enum SkillEffectType
    SkillEffectType_None
    SkillEffectType_Aura
    SkillEffectType_Buff
    SkillEffectType_Damage
    SkillEffectType_Heal
    SkillEffectType_DoT
    SkillEffectType_HoT
    SkillEffectType_Steal
    SkillEffectType_Teleport
    SkillEffectType_Silence
    SkillEffectType_Blind
    SkillEffectType_Dash
    SkillEffectType_Dispel
    SkillEffectType_Cleanse
    SkillEffectType_Immobilize
    SkillEffectType_Count
End Enum

Public Type SkillEffectRec
    EffectType As SkillEffectType
    VitalType As SkillVitalType
    TargetType As SkillTargetType
    Direction As Byte
    Damage As Long
    DamagePerLevel As Long
    Duration As Long
    Interval As Long
    StunDuration As Long
    MapId As Long
    X As Long
    Y As Long
    EffectId As Long
    Trigger As Long
End Type

Public Type SkillRec
    Id As Long
    Name As String
    Description As String
    Sound As String
    IconId As Long
    Type As SkillType
    AttributeType As SkillAttributeType
    TargetType As SkillTargetType
    ElementType As Elements
    CostType As SkillCostType
    EffectType As SkillEffectType
    MaximumLevel As Long
    Amplification As Single
    AmplificationPerLevel As Single
    Range As Byte
    Cost As Long
    CostPerLevel As Long
    CastTime As Long
    Cooldown As Long
    StunDuration As Long
    CastAnimationId As Long
    AttackAnimationId As Long
    PassiveSkillId As Long
    EffectCount As Long
    Effect() As SkillEffectRec
End Type

Public Sub LoadSkills()
    Dim Index As Long
    Dim Name As String
    Dim Description As String
    Dim Sound As String

    Index = GetFileHandler(App.Path & "\Data Files\Data\Skills.dat")

    If Index > 0 Then
        Dim i As Long
        Dim n As Long
        Dim Length As Long

        MaximumSkills = ReadInt32(Index)

        If MaximumSkills > 0 Then
            ReDim Skill(1 To MaximumSkills)

            For i = 1 To MaximumSkills
                Skill(i).Id = ReadInt32(Index)
                
                Name = String(255, vbNullChar)
                Description = String(1024, vbNullChar)
                Sound = String(255, vbNullChar)
                
                Call ReadString(Index, Name)
                Call ReadString(Index, Description)
                Call ReadString(Index, Sound)
                
                Skill(i).Name = Replace$(Name, vbNullChar, vbNullString)
                Skill(i).Description = Replace$(Description, vbNullChar, vbNullString)
                Skill(i).Sound = Replace$(Sound, vbNullChar, vbNullString)
                Skill(i).IconId = ReadInt32(Index)
                Skill(i).Type = ReadInt32(Index)
                Skill(i).AttributeType = ReadInt32(Index)
                Skill(i).TargetType = ReadInt32(Index)
                Skill(i).ElementType = ReadInt32(Index)
                Skill(i).CostType = ReadInt32(Index)
                Skill(i).EffectType = ReadInt32(Index)
                Skill(i).MaximumLevel = ReadInt32(Index)
                Skill(i).Amplification = ReadSingle(Index)
                Skill(i).AmplificationPerLevel = ReadSingle(Index)
                Skill(i).Range = ReadInt32(Index)
                Skill(i).Cost = ReadInt32(Index)
                Skill(i).CostPerLevel = ReadInt32(Index)
                Skill(i).CastTime = ReadInt32(Index)
                Skill(i).Cooldown = ReadInt32(Index)
                Skill(i).StunDuration = ReadInt32(Index)
                Skill(i).CastAnimationId = ReadInt32(Index)
                Skill(i).AttackAnimationId = ReadInt32(Index)
                Skill(i).PassiveSkillId = ReadInt32(Index)

                Length = ReadInt32(Index)
                Skill(i).EffectCount = Length

                If Length > 0 Then
                    ReDim Skill(i).Effect(1 To Length)

                    For n = 1 To Length
                        Skill(i).Effect(n).EffectType = ReadInt32(Index)
                        Skill(i).Effect(n).VitalType = ReadInt32(Index)
                        Skill(i).Effect(n).TargetType = ReadInt32(Index)
                        Skill(i).Effect(n).Direction = ReadInt32(Index)
                        Skill(i).Effect(n).Damage = ReadInt32(Index)
                        Skill(i).Effect(n).DamagePerLevel = ReadInt32(Index)
                        Skill(i).Effect(n).Duration = ReadInt32(Index)
                        Skill(i).Effect(n).Interval = ReadInt32(Index)
                        Skill(i).Effect(n).StunDuration = ReadInt32(Index)
                        Skill(i).Effect(n).MapId = ReadInt32(Index)
                        Skill(i).Effect(n).X = ReadInt32(Index)
                        Skill(i).Effect(n).Y = ReadInt32(Index)
                        Skill(i).Effect(n).EffectId = ReadInt32(Index)
                        Skill(i).Effect(n).Trigger = ReadInt32(Index)
                    Next
                End If
            Next
        End If
    End If

    Call CloseFileHandler(Index)

End Sub


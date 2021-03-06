Attribute VB_Name = "Passive_Data"
Option Explicit

Public MaxPassives As Long
Public MaxPassiveAttributes As Long
Public MaxPassiveEnhancements As Long

Public Passive() As PassiveRec
Public PassiveAttributes() As AttributesRec
Public PassiveEnhancements() As AttributesRec

Enum PassiveType
    PassiveType_Attributes
    PassiveType_Improvement
    PassiveType_Activation
End Enum

Enum PassiveChangeEffectType
    PassiveChangeEffectType_Increase
    PassiveChangeEffectType_Remove
End Enum

Enum PassiveActivationType
    PassiveActivationType_WhenTarget
    PassiveActivationType_WhenPlayer
End Enum

Enum PassiveActivationResult
    PassiveActivationResult_None
    PassiveActivationResult_Skill
    PassiveActivationResult_SpellBuff
    PassiveActivationResult_Recovery
    PassiveActivationResult_Dispel
    PassiveActivationResult_Cleanse
    PassiveActivationResult_Reflect
    PassiveActivationResult_Absorb
End Enum

Enum PassiveActivationConditional
    PassiveActivationConditional_Attack
    PassiveActivationConditional_MagicAttack
    PassiveActivationConditional_Heal
    PassiveActivationConditional_Block
    PassiveActivationConditional_Parry
    PassiveActivationConditional_Evasion
    PassiveActivationConditional_BreakCast
    PassiveActivationConditional_Critical
    PassiveActivationConditional_Stun
    PassiveActivationConditional_Silence
    PassiveActivationConditional_Immobilize
End Enum

Private Type PassiveRec
    Id As Long
    Type As PassiveType
    TargetType As SkillTargetType
    Element As Elements
    AttributeId As Long
    UpgradeId As Long
    SkillTargetId As Long
    Amplification As Single
    Range As Integer
    CastTime As Integer
    Cooldown As Integer
    Stun As Integer
    Cost As Long
    Effect As SkillEffectRec
    EffectChangeType As PassiveChangeEffectType
    Activation As PassiveActivationType
    Conditional As PassiveActivationConditional
    ActivationResult As PassiveActivationResult
    ActivationChance As Long
End Type

Public Sub LoadPassives()
    Dim Index As Long
    Dim i As Long
    Dim Name As String
    Dim Description As String

    Index = GetFileHandler(App.Path & "\Data Files\Data\Passives.dat")

    If Index > 0 Then
        MaxPassives = ReadInt32(Index)

        If MaxPassives > 0 Then
            ReDim Passive(1 To MaxPassives)

            For i = 1 To MaxPassives
                Passive(i).Id = ReadInt32(Index)

                Name = String(255, vbNullChar)
                Description = String(1024, vbNullChar)

                Call ReadString(Index, Name)
                Call ReadString(Index, Description)

                Passive(i).Type = ReadInt32(Index)
                Passive(i).TargetType = ReadInt32(Index)
                Passive(i).Element = ReadInt32(Index)
                Passive(i).AttributeId = ReadInt32(Index)
                Passive(i).UpgradeId = ReadInt32(Index)
                Passive(i).SkillTargetId = ReadInt32(Index)
                Passive(i).Amplification = ReadSingle(Index)
                Passive(i).Range = ReadInt32(Index)
                Passive(i).CastTime = ReadInt32(Index)
                Passive(i).Cooldown = ReadInt32(Index)
                Passive(i).Stun = ReadInt32(Index)
                Passive(i).Cost = ReadInt32(Index)

                Passive(i).EffectChangeType = ReadInt32(Index)
                Passive(i).Activation = ReadInt32(Index)
                Passive(i).Conditional = ReadInt32(Index)
                Passive(i).ActivationResult = ReadInt32(Index)
                Passive(i).ActivationChance = ReadInt32(Index)

                Passive(i).Effect.EffectType = ReadInt32(Index)
                Passive(i).Effect.VitalType = ReadInt32(Index)
                Passive(i).Effect.TargetType = ReadInt32(Index)
                Passive(i).Effect.Direction = ReadInt32(Index)
                Passive(i).Effect.Damage = ReadInt32(Index)
                Passive(i).Effect.DamagePerLevel = ReadInt32(Index)
                Passive(i).Effect.Duration = ReadInt32(Index)
                Passive(i).Effect.Interval = ReadInt32(Index)
                Passive(i).Effect.StunDuration = ReadInt32(Index)
                Passive(i).Effect.MapId = ReadInt32(Index)
                Passive(i).Effect.X = ReadInt32(Index)
                Passive(i).Effect.Y = ReadInt32(Index)
                Passive(i).Effect.EffectId = ReadInt32(Index)
            Next
        End If
    End If
    
    Call CloseFileHandler(Index)

End Sub

Public Sub LoadPassiveAttributes()
    Call LoadAttributes(PassiveAttributes, MaxPassiveAttributes, "PassiveAttributes.dat")
End Sub

Public Sub LoadPassiveEnhancements()
     Call LoadAttributes(PassiveEnhancements, MaxPassiveEnhancements, "PassiveUpgrades.dat")
End Sub




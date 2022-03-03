Attribute VB_Name = "Skill_Description"
Option Explicit

Private Multiplier As String

Private Const Increase As String = "Aumenta"
Private Const Decrease As String = "Diminui"
' Slot da skill usado para descrição.
Private SkillSlot As Long

Public Sub ShowPlayerSkillDesc(X As Long, Y As Long, ByVal SlotNum As Long, ByVal Passive As Boolean)
    If SlotNum <= 0 Or SlotNum > MaxPlayerSkill Then
        Exit Sub
    End If

    If Passive Then
        If PlayerPassive(SlotNum).Id > 0 And PlayerPassive(SlotNum).Id <= MaximumSkills Then
            ShowSkillDesc X, Y, SlotNum, PlayerPassive(SlotNum).Id, PlayerPassive(SlotNum).Level
        End If
    Else
        If PlayerSkill(SlotNum).Id > 0 And PlayerSkill(SlotNum).Id <= MaximumSkills Then
            ShowSkillDesc X, Y, SlotNum, PlayerSkill(SlotNum).Id, PlayerSkill(SlotNum).Level
        End If
    End If

End Sub

Public Sub ShowSkillDesc(X As Long, Y As Long, ByVal Slot As Long, SkillNum As Long, Optional SkillLevel As Long = 0, Optional SkillTreeIndex As Long = 0, Optional TalentIndex As Long = 0)
    Dim Colour As Long
    Dim WindowIndex As Long, ControlIndex As Long

    ' set globals
    DescType = 2    ' spell
    DescItem = SkillNum

    WindowIndex = GetWindowIndex("winDescription")

    ' set position
    Windows(WindowIndex).Window.Left = X
    Windows(WindowIndex).Window.Top = Y
    Windows(WindowIndex).Window.Width = X
    Windows(WindowIndex).Window.Width = 260

    ' show the window
    ShowWindow WindowIndex, , False

    ' exit out early if last is same
    If (DescLastType = DescType) And (DescLastItem = DescItem) And DescLastLevel = SkillLevel Then Exit Sub

    DescLastType = DescType
    DescLastItem = DescItem
    DescLastLevel = SkillLevel
    SkillSlot = Slot

    Windows(WindowIndex).Controls(GetControlIndex("winDescription", "lblPrice")).Visible = False

    ' clear
    ReDim DescText(1 To 1) As TextColourRec

    ' set variables
    With Windows(WindowIndex)
        ' set name
        ControlIndex = GetControlIndex("winDescription", "lblName")
        .Controls(ControlIndex).textColour = BrightGreen
        .Controls(ControlIndex).Text = Skill(SkillNum).Name
        .Controls(ControlIndex).Width = 260

        ControlIndex = GetControlIndex("winDescription", "lblType")
        .Controls(ControlIndex).Width = 260

        If SkillLevel > 0 Then
            .Controls(ControlIndex).Text = "Level " & SkillLevel
            .Controls(ControlIndex).textColour = BrightGreen
        Else
            .Controls(ControlIndex).Text = "Não aprendido"
            .Controls(ControlIndex).textColour = BrightRed
        End If

        .Controls(ControlIndex).align = Alignment.alignCentre
        .Controls(ControlIndex).Font = Fonts.OpenSans_Regular
    End With

    AddDescInfo ""
    AddDescInfo "Tipo: " & GetSkillTypeName(Skill(SkillNum).Type)

    If Skill(SkillNum).Type = SkillType_Active Then
        Call AddSkillActivePrimaryTypeInfo(Slot, SkillNum, SkillLevel)
    Else
        Call AddSkillPassiveInfo(SkillNum, SkillLevel)
    End If

    With Windows(WindowIndex)
        .Window.Height = 150 + (UBound(DescText) * 12)

        ControlIndex = GetControlIndex("winDescription", "lblDesc")
        .Controls(ControlIndex).Top = 50 + (UBound(DescText) * 12)
        .Controls(ControlIndex).Text = Skill(SkillNum).Description
        .Controls(ControlIndex).Visible = True
        .Controls(ControlIndex).Width = 240
    End With

End Sub

Private Sub AddSkillPassiveInfo(ByVal SkillId As Long, ByVal Level As Long)
    Dim PassiveId As Long
    Dim AttributeId As Long
    Dim UpgradeId As Long
    Dim SkillTargetId As Long

    PassiveId = Skill(SkillId).PassiveSkillId

    If PassiveId > 0 Then
    
        Select Case Passive(PassiveId).Type
        
        Case PassiveType.PassiveType_Attributes
            AttributeId = Passive(PassiveId).AttributeId
            UpgradeId = Passive(PassiveId).UpgradeId

            AddDescInfo ""
            AddDescInfo "Atibutos", Coral
            Call AddPassiveDescInfo(PassiveId, Level)

        Case PassiveType.PassiveType_Improvement
            SkillTargetId = Passive(PassiveId).SkillTargetId

            If SkillTargetId > 0 Then
                AddDescInfo "Afeta a habilidade " & ColourChar & BrightGreen & Skill(SkillTargetId).Name & ColourChar & "-1"
                AddDescInfo ""
                AddDescInfo "Efeitos", Coral

                Call AddPassiveImprovements(PassiveId, Level, SkillTargetId)

            End If

        Case PassiveType.PassiveType_Activation

        End Select
    End If
End Sub

Private Sub AddPassiveImprovements(ByVal PassiveId As Long, ByVal Level As Long, ByVal SkillTargetId As Long)

    If Passive(PassiveId).Amplification > 0 Then
        AddDescInfo Increase & " a amplificação em " & Level * CLng(Passive(PassiveId).Amplification * 100) & "%"
    ElseIf Passive(PassiveId).Amplification < 0 Then
        AddDescInfo Decrease & " a amplificação em " & Level * CLng(Passive(PassiveId).Amplification * 100) & "%"
    End If

    If Passive(PassiveId).Range > 0 Then
        AddDescInfo Increase & " o alcance em " & Level * Passive(PassiveId).Range
    ElseIf Passive(PassiveId).Range < 0 Then
        AddDescInfo Decrease & " o alcance em " & Level * Passive(PassiveId).Range
    End If

    If Passive(PassiveId).CastTime > 0 Then
        AddDescInfo Increase & " a conjuração em " & Level * Passive(PassiveId).CastTime & " (s)"
    ElseIf Passive(PassiveId).CastTime < 0 Then
        AddDescInfo Decrease & " a conjuração em " & Level * Passive(PassiveId).CastTime & " (s)"
    End If

    If Passive(PassiveId).Cooldown > 0 Then
        AddDescInfo Increase & " o resfriamento em " & Level * Passive(PassiveId).Cooldown & " (s)"
    ElseIf Passive(PassiveId).Cooldown < 0 Then
        AddDescInfo Decrease & " o resfriamento em " & Level * Passive(PassiveId).Cooldown & " (s)"
    End If

    If Passive(PassiveId).Stun > 0 Then
        AddDescInfo Increase & " o atordoamento em " & Level * Passive(PassiveId).Stun & " (s)"
    ElseIf Passive(PassiveId).Stun < 0 Then
        AddDescInfo Decrease & " o atordoamento em " & Level * Passive(PassiveId).Stun & " (s)"
    End If

    If Passive(PassiveId).Cost > 0 Then
        AddDescInfo Increase & " o custo de " & GetSkillVitalTypeName(Skill(SkillTargetId).CostType) & " em " & Level * Passive(PassiveId).Cost
    ElseIf Passive(PassiveId).Cost < 0 Then
        AddDescInfo Decrease & " o custo de " & GetSkillVitalTypeName(Skill(SkillTargetId).CostType) & " em " & Level * Passive(PassiveId).Cost
    End If

    If Passive(PassiveId).Element > 0 Then
        AddDescInfo "Altera o elemento para " & GetSkillElementTypeName(Passive(PassiveId).Element)
    End If

    If Passive(PassiveId).TargetType > SkillTargetType_None Then
        AddDescInfo "Altera o tipo de alvo para " & GetSkillTargetTypeName(Passive(PassiveId).TargetType, True)
    End If

    If Passive(PassiveId).Effect.EffectType > 0 Then
        AddDescInfo ""
        AddDescInfo GetSkillEffectTypeName(Passive(PassiveId).Effect.EffectType), Coral

        If Passive(PassiveId).Effect.TargetType > SkillTargetType_None Then
            AddDescInfo "Altera o tipo de alvo para " & GetSkillTargetTypeName(Passive(PassiveId).Effect.TargetType, True)
        End If

        Dim Text As String
        Select Case Passive(PassiveId).Effect.EffectType
        Case SkillEffectType.SkillEffectType_Damage
            Text = " o dano em "
        Case SkillEffectType.SkillEffectType_Heal
            Text = " a cura em "
        Case SkillEffectType.SkillEffectType_DoT
            Text = " o dano em "
        Case SkillEffectType.SkillEffectType_HoT
            Text = " a cura em "
        Case SkillEffectType.SkillEffectType_Steal
            Text = " o dano em "
        End Select

        If Passive(PassiveId).Effect.Damage > 0 Then
            AddDescInfo Increase & Text & Level * Passive(PassiveId).Effect.Damage
        ElseIf Passive(PassiveId).Effect.Damage < 0 Then
            AddDescInfo Decrease & Text & Level * Passive(PassiveId).Effect.Damage
        End If

        If Passive(PassiveId).Effect.Duration > 0 Then
            AddDescInfo Increase & " a duração em " & Level * Passive(PassiveId).Effect.Duration & " (s)"
        ElseIf Passive(PassiveId).Effect.Duration < 0 Then
            AddDescInfo Decrease & " a duração em " & Level * Passive(PassiveId).Effect.Duration & " (s)"
        End If

        If Passive(PassiveId).Effect.Interval > 0 Then
            AddDescInfo Increase & " o intervalo em " & Level * Passive(PassiveId).Effect.Interval & " (s)"
        ElseIf Passive(PassiveId).Effect.Interval < 0 Then
            AddDescInfo Decrease & " o intervalo em " & Level * Passive(PassiveId).Effect.Interval & " (s)"
        End If

    End If

End Sub

' Adiciona as informações primárias.
Private Sub AddSkillActivePrimaryTypeInfo(ByVal SkillSlot As Long, ByVal SkillNum As Long, ByVal Level As Long)
    Dim Text As String

    Dim SkillEffects As PlayerSkillRec
    SkillEffects = GetPlayerSkillEffect(SkillSlot, 0)

    Select Case Skill(SkillNum).EffectType
    Case SkillEffectType.SkillEffectType_Aura
        AddDescInfo "Alvo: " & GetSkillTargetTypeName(SkillEffects.TargetType, True)

        Text = GetSkillVitalTypeName(Skill(SkillNum).CostType)

        If Text <> vbNullString Then
            AddDescInfo "Custo de " & Text & "  " & SkillEffects.Cost
        Else
            AddDescInfo "Sem custo de uso"
        End If

        Call AddSkillEffectInfo(SkillNum, Level)

    Case SkillEffectType.SkillEffectType_Buff
        AddDescInfo "Alvo: " & GetSkillTargetTypeName(SkillEffects.TargetType, True)

        Text = GetSkillVitalTypeName(Skill(SkillNum).CostType)

        If Text <> vbNullString Then
            AddDescInfo "Custo de " & Text & "  " & SkillEffects.Cost
        Else
            AddDescInfo "Sem custo de uso"
        End If


        Call AddSkillEffectInfo(SkillNum, Level)

    Case SkillEffectType.SkillEffectType_Damage
        AddDescInfo "Atributo: " & GetSkillAttributeTypeName(Skill(SkillNum).AttributeType)
        AddDescInfo "Alvo: " & GetSkillTargetTypeName(SkillEffects.TargetType, True)
        If Skill(SkillNum).ElementType > 0 Then AddDescInfo "Elemento: " & GetSkillElementTypeName(SkillEffects.Element)

        Text = GetSkillVitalTypeName(Skill(SkillNum).CostType)

        If Text <> vbNullString Then
            AddDescInfo "Custo de " & Text & "  " & SkillEffects.Cost
        Else
            AddDescInfo "Sem custo de uso"
        End If

        AddDescInfo ""
        AddDescInfo "Amplificação: " & CLng(SkillEffects.Amplification * 100) & "%"

        Call AddSkillEffectInfo(SkillNum, Level)

    Case SkillEffectType.SkillEffectType_Heal
        AddDescInfo "Tipo: " & GetSkillAttributeTypeName(Skill(SkillNum).AttributeType)
        AddDescInfo "Alvo: " & GetSkillTargetTypeName(SkillEffects.TargetType, True)
        If Skill(SkillNum).ElementType > 0 Then AddDescInfo "Elemento: " & GetSkillElementTypeName(SkillEffects.Element)
        
        Text = GetSkillVitalTypeName(Skill(SkillNum).CostType)

        If Text <> vbNullString Then
            AddDescInfo "Custo de " & Text & "  " & SkillEffects.Cost
        Else
            AddDescInfo "Sem custo de uso"
        End If
        
        AddDescInfo ""
        AddDescInfo "Amplificação: " & CLng(SkillEffects.Amplification * 100) & "%"

        Call AddSkillEffectInfo(SkillNum, Level)

    Case SkillEffectType.SkillEffectType_Teleport
        AddDescInfo "Alvo: " & GetSkillTargetTypeName(SkillEffects.TargetType, True)
        
        Text = GetSkillVitalTypeName(Skill(SkillNum).CostType)

        If Text <> vbNullString Then
            AddDescInfo "Custo de " & Text & "  " & SkillEffects.Cost
        Else
            AddDescInfo "Sem custo de uso"
        End If

    End Select
End Sub

Private Sub AddSkillEffectInfo(ByVal SkillNum As Long, ByVal Level As Long)
    If Skill(SkillNum).EffectCount = 0 Then
        Exit Sub
    End If

    Dim i As Long
    Dim UpgradeDataId As Long
    Dim AttributeId As Long
    Dim EffectId As Long
    Dim BuffType As Byte
    Dim EffectIndex As Long
    Dim SkillEffects As PlayerSkillRec

    Dim Damage As Long
    Dim Interval As Long
    Dim Duration As Long
    Dim Range As Long
    Dim Amplification As Single
    Dim Cost As Long

    With Skill(SkillNum)
        For i = 1 To .EffectCount
            AddDescInfo ""
            AddDescInfo GetSkillEffectTypeName(.Effect(i).EffectType), Coral

            EffectIndex = .Effect(i).EffectType
            SkillEffects = GetPlayerSkillEffect(SkillSlot, EffectIndex)

            Duration = SkillEffects.Effect(EffectIndex).Duration
            Interval = SkillEffects.Effect(EffectIndex).Interval
            Damage = SkillEffects.Effect(EffectIndex).Damage
            Amplification = SkillEffects.Amplification
            Range = SkillEffects.Range
            Cost = SkillEffects.Cost

            Select Case .Effect(i).EffectType
            Case SkillEffectType.SkillEffectType_Buff
                '######################################################
                EffectId = SkillEffects.Effect(EffectIndex).EffectId

                If EffectId > 0 Then
                    AddDescInfo "Aplica o efeito " & ColourChar & BrightGreen & AttributeEffect(EffectId).Name
                    AddDescInfo "O efeito é aplicado para o (a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False)
                    AddDescInfo ""

                    Call AddAttributeEffectDescValues(EffectId, Level)
                End If
                '######################################################
            Case SkillEffectType.SkillEffectType_Damage
                Damage = SkillEffects.Effect(EffectIndex).Damage

                If SkillEffects.Element > 0 Then
                    AddDescInfo "Infligi o (a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & " com " & Damage & " de dano"
                    AddDescInfo "adicional de " & GetSkillElementTypeName(SkillEffects.Element) & "."
                Else
                    AddDescInfo "Infligi o (a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & " com " & Damage
                    AddDescInfo " de dano adicional."
                End If

            Case SkillEffectType.SkillEffectType_Heal
                If SkillEffects.Element > 0 Then
                    AddDescInfo "Recupera o (a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & " com " & Damage & " de " & GetSkillVitalTypeName(SkillEffects.Effect(EffectIndex).VitalType)
                    AddDescInfo " adicional de " & GetSkillElementTypeName(SkillEffects.Element) & "."
                Else
                    AddDescInfo "Recupera o (a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & " com " & Damage & " de " & GetSkillVitalTypeName(SkillEffects.Effect(EffectIndex).VitalType) & "."
                End If

            Case SkillEffectType.SkillEffectType_DoT
                If SkillEffects.Element > 0 Then
                    AddDescInfo "Infligi o (a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & " com " & Damage & " de dano"
                    AddDescInfo "adicional de " & GetSkillElementTypeName(SkillEffects.Element) & " a cada " & Interval & " segundo(s)"
                    AddDescInfo " ao longo de " & Duration & " segundo(s)."
                    AddDescInfo "Alcance de " & Range & "m dentro da área do alvo."
                Else
                    AddDescInfo "Infligi o (a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & " com " & Damage & " de dano"
                    AddDescInfo "adicional a cada " & Interval & " segundo(s)"
                    AddDescInfo " ao longo de " & Duration & " segundo(s)."
                    AddDescInfo "Alcance de " & Range & "m dentro da área do alvo."
                End If

            Case SkillEffectType.SkillEffectType_HoT
                If SkillEffects.Element > 0 Then
                    AddDescInfo "Regenera o (a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & " com " & Damage & " de " & GetSkillVitalTypeName(SkillEffects.Effect(EffectIndex).VitalType)
                    AddDescInfo " adicional de " & GetSkillElementTypeName(SkillEffects.Element) & " a cada " & Interval & " segundo(s)"
                    AddDescInfo " ao longo de " & Duration & " segundo(s)."
                Else
                    AddDescInfo "Regenera o (a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & " com " & Damage & " de " & GetSkillVitalTypeName(SkillEffects.Effect(EffectIndex).VitalType)
                    AddDescInfo " adicional a cada " & Interval & " segundo(s)"
                    AddDescInfo " ao longo de " & Duration & " segundo(s)."
                End If

            Case SkillEffectType.SkillEffectType_Steal
                If SkillEffects.Element > 0 Then
                    AddDescInfo "Infligi o (a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & " com " & Damage & " de dano"
                    AddDescInfo "adicional de " & GetSkillElementTypeName(SkillEffects.Element) & " e recupera " & GetSkillVitalTypeName(SkillEffects.Effect(EffectIndex).VitalType)
                    AddDescInfo " com o valor do dano infligido."
                Else
                    AddDescInfo "Infligi o (a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & " com " & Damage
                    AddDescInfo "adicional e recupera " & GetSkillVitalTypeName(SkillEffects.Effect(EffectIndex).VitalType)
                    AddDescInfo " com o valor do dano infligido."
                End If

            Case SkillEffectType.SkillEffectType_Silence
                AddDescInfo "Silencia o (a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & " impedindo-o de "
                AddDescInfo "qualquer conjuração ao longo de "
                AddDescInfo Duration & " segundo(s)."

            Case SkillEffectType.SkillEffectType_Blind
                AddDescInfo "Cega o (a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & " impedindo-o de "
                AddDescInfo "usar ataques físicos ao longo de "
                AddDescInfo Duration & " segundo(s)."

            Case SkillEffectType.SkillEffectType_Aura
                AddDescInfo "Aumenta os atributos dos membros"
                AddDescInfo " do grupo que estiverem dentro de "
                AddDescInfo " uma área de " & SkillEffects.Range & "m do portador."
                AddDescInfo ""

                EffectId = SkillEffects.Effect(EffectIndex).EffectId

                If EffectId > 0 Then
                    Call AddAttributeEffectDescValues(EffectId, Level)
                End If

            Case SkillEffectType.SkillEffectType_Dispel
                AddDescInfo "Remove " & Damage & " efeito(s) positivos do(a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & "."

            Case SkillEffectType.SkillEffectType_Cleanse
                AddDescInfo "Remove " & Damage & " efeito(s) negativos do(a) " & GetSkillTargetTypeName(SkillEffects.Effect(EffectIndex).TargetType, False) & "."

            End Select
        Next
    End With

End Sub

Private Sub AddPassiveDescInfo(ByVal PassiveId As Long, ByVal Level As Long)
    Dim AttributeId As Long
    Dim UpgradeId As Long
    
    AttributeId = Passive(PassiveId).AttributeId
    UpgradeId = Passive(PassiveId).UpgradeId

    If AttributeId < 1 Or AttributeId > MaxPassiveAttributes Then
        Exit Sub
    End If

    If Level > 0 Then
        Level = Level - 1
    End If

    Call AddText(PassiveAttributes(AttributeId).Stat(Stats.Strength), Level, PassiveEnhancements(UpgradeId).Stat(Stats.Strength), "Força")
    Call AddText(PassiveAttributes(AttributeId).Stat(Stats.Agility), Level, PassiveEnhancements(UpgradeId).Stat(Stats.Agility), "Agilidade")
    Call AddText(PassiveAttributes(AttributeId).Stat(Stats.Constitution), Level, PassiveEnhancements(UpgradeId).Stat(Stats.Constitution), "Constituição")
    Call AddText(PassiveAttributes(AttributeId).Stat(Stats.Intelligence), Level, PassiveEnhancements(UpgradeId).Stat(Stats.Intelligence), "Inteligência")
    Call AddText(PassiveAttributes(AttributeId).Stat(Stats.Spirit), Level, PassiveEnhancements(UpgradeId).Stat(Stats.Spirit), "Espírito")
        Call AddText(PassiveAttributes(AttributeId).Stat(Stats.Will), Level, PassiveEnhancements(UpgradeId).Stat(Stats.Will), "Vontade")
    Call AddText(PassiveAttributes(AttributeId).Vital(HP), Level, PassiveEnhancements(UpgradeId).Vital(HP), "HP")
    Call AddText(PassiveAttributes(AttributeId).Vital(HP), Level, PassiveEnhancements(UpgradeId).Vital(MP), "MP")
    Call AddText(PassiveAttributes(AttributeId).Attack, Level, PassiveEnhancements(UpgradeId).Attack, "Ataque")
    Call AddText(PassiveAttributes(AttributeId).Defense, Level, PassiveEnhancements(UpgradeId).Defense, "Defesa")
    Call AddText(PassiveAttributes(AttributeId).Accuracy, Level, PassiveEnhancements(UpgradeId).Accuracy, "Precisão")
    Call AddText(PassiveAttributes(AttributeId).Evasion, Level, PassiveEnhancements(UpgradeId).Evasion, "Evasão")
    Call AddText(PassiveAttributes(AttributeId).Parry, Level, PassiveEnhancements(UpgradeId).Parry, "Aparo")
    Call AddText(PassiveAttributes(AttributeId).Block, Level, PassiveEnhancements(UpgradeId).Block, "Bloqueio")
    Call AddText(PassiveAttributes(AttributeId).MagicAttack, Level, PassiveEnhancements(UpgradeId).MagicAttack, "Ataque Mágico")
    Call AddText(PassiveAttributes(AttributeId).MagicDefense, Level, PassiveEnhancements(UpgradeId).MagicDefense, "Defesa Mágica")
    Call AddText(PassiveAttributes(AttributeId).MagicAccuracy, Level, PassiveEnhancements(UpgradeId).MagicAccuracy, "Precisão Mágica")
    Call AddText(PassiveAttributes(AttributeId).MagicResist, Level, PassiveEnhancements(UpgradeId).MagicResist, "Resistência Mágica")
    Call AddText(PassiveAttributes(AttributeId).Concentration, Level, PassiveEnhancements(UpgradeId).Concentration, "Concentração")
    Call AddTextPercentage(PassiveAttributes(AttributeId).CritRate + (Level * PassiveEnhancements(UpgradeId).CritRate), "Taxa Crítica")
    Call AddTextPercentage(PassiveAttributes(AttributeId).CritDamage + (Level * PassiveEnhancements(UpgradeId).CritDamage), "Dano Crítico")
    Call AddTextPercentage(PassiveAttributes(AttributeId).ResistCritRate + (Level * PassiveEnhancements(UpgradeId).ResistCritRate), "Res. Taxa Crítica")
    Call AddTextPercentage(PassiveAttributes(AttributeId).ResistCritDamage + (Level * PassiveEnhancements(UpgradeId).ResistCritDamage), "Res. Dano Crítico")
    Call AddTextPercentage(PassiveAttributes(AttributeId).HealingPower + (Level * PassiveEnhancements(UpgradeId).HealingPower), "Poder de Cura")
    Call AddTextPercentage(PassiveAttributes(AttributeId).Amplification + (Level * PassiveEnhancements(UpgradeId).Amplification), "Amplificação")
    Call AddTextPercentage(PassiveAttributes(AttributeId).FinalDamage + (Level * PassiveEnhancements(UpgradeId).FinalDamage), "Dano Final")
    Call AddTextPercentage(PassiveAttributes(AttributeId).Enmity + (Level * PassiveEnhancements(UpgradeId).Enmity), "Inimizade")
    Call AddTextPercentage(PassiveAttributes(AttributeId).AttackSpeed + (Level * PassiveEnhancements(UpgradeId).AttackSpeed), "Vel. Ataque")
    Call AddTextPercentage(PassiveAttributes(AttributeId).CastSpeed + (Level * PassiveEnhancements(UpgradeId).CastSpeed), "Vel. Conjuração")
    Call AddText(PassiveAttributes(AttributeId).SilenceResistance, Level, PassiveEnhancements(UpgradeId).SilenceResistance, "Res. Silêncio")
    Call AddText(PassiveAttributes(AttributeId).BlindResistance, Level, PassiveEnhancements(UpgradeId).BlindResistance, "Res. Cegar")
    Call AddText(PassiveAttributes(AttributeId).StunResistance, Level, PassiveEnhancements(UpgradeId).StunResistance, "Res. Atordoar")
    Call AddText(PassiveAttributes(AttributeId).StumbleResistance, Level, PassiveEnhancements(UpgradeId).Concentration, "Res. Queda")
    Call AddText(PassiveAttributes(AttributeId).ElementAttack(Elements.Element_Fire), Level, PassiveEnhancements(UpgradeId).ElementAttack(Elements.Element_Fire), "Elemento Fogo")
    Call AddText(PassiveAttributes(AttributeId).ElementAttack(Elements.Element_Water), Level, PassiveEnhancements(UpgradeId).ElementAttack(Elements.Element_Water), "Elemento Água")
    Call AddText(PassiveAttributes(AttributeId).ElementAttack(Elements.Element_Earth), Level, PassiveEnhancements(UpgradeId).ElementAttack(Elements.Element_Earth), "Elemento Terra")
    Call AddText(PassiveAttributes(AttributeId).ElementAttack(Elements.Element_Wind), Level, PassiveEnhancements(UpgradeId).ElementAttack(Elements.Element_Wind), "Elemento Vento")
    Call AddText(PassiveAttributes(AttributeId).ElementAttack(Elements.Element_Light), Level, PassiveEnhancements(UpgradeId).ElementAttack(Elements.Element_Light), "Elemento Luz")
    Call AddText(PassiveAttributes(AttributeId).ElementAttack(Elements.Element_Dark), Level, PassiveEnhancements(UpgradeId).ElementAttack(Elements.Element_Dark), "Elemento Trevas")

End Sub

Private Sub AddAttributeEffectDescValues(ByVal EffectId As Long, ByVal Level As Long)
    If EffectId = 0 Then
        Exit Sub
    End If

    Dim AttributeId As Long
    Dim UpgradeId As Long

    If Level > 0 Then
        Level = Level - 1
    End If

    AttributeId = AttributeEffect(EffectId).AttributeId
    UpgradeId = AttributeEffect(EffectId).UpgradeId

    If AttributeId < 1 Or AttributeId > MaxAttributeEffectAttributes Then
        Exit Sub
    End If

    If AttributeEffect(EffectId).EffectType = AttributeEffectType_Increase Then
        Multiplier = vbNullString
    Else
        Multiplier = "-"
    End If

    AddDescInfo ""
    Call AddText(AttributeEffectAttributes(AttributeId).Stat(Stats.Strength), Level, AttributeEffectEnhancements(UpgradeId).Stat(Stats.Strength), "Força")
    Call AddText(AttributeEffectAttributes(AttributeId).Stat(Stats.Agility), Level, AttributeEffectEnhancements(UpgradeId).Stat(Stats.Agility), "Agilidade")
    Call AddText(AttributeEffectAttributes(AttributeId).Stat(Stats.Constitution), Level, AttributeEffectEnhancements(UpgradeId).Stat(Stats.Constitution), "Constituição")
    Call AddText(AttributeEffectAttributes(AttributeId).Stat(Stats.Intelligence), Level, AttributeEffectEnhancements(UpgradeId).Stat(Stats.Intelligence), "Inteligência")
    Call AddText(AttributeEffectAttributes(AttributeId).Stat(Stats.Spirit), Level, AttributeEffectEnhancements(UpgradeId).Stat(Stats.Spirit), "Espírito")
        Call AddText(AttributeEffectAttributes(AttributeId).Stat(Stats.Will), Level, AttributeEffectEnhancements(UpgradeId).Stat(Stats.Will), "Vontade")
    Call AddText(AttributeEffectAttributes(AttributeId).Vital(HP), Level, AttributeEffectEnhancements(UpgradeId).Vital(HP), "HP")
    Call AddText(AttributeEffectAttributes(AttributeId).Vital(HP), Level, AttributeEffectEnhancements(UpgradeId).Vital(MP), "MP")
    Call AddText(AttributeEffectAttributes(AttributeId).Attack, Level, AttributeEffectEnhancements(UpgradeId).Attack, "Ataque")
    Call AddText(AttributeEffectAttributes(AttributeId).Defense, Level, AttributeEffectEnhancements(UpgradeId).Defense, "Defesa")
    Call AddText(AttributeEffectAttributes(AttributeId).Accuracy, Level, AttributeEffectEnhancements(UpgradeId).Accuracy, "Precisão")
    Call AddText(AttributeEffectAttributes(AttributeId).Evasion, Level, AttributeEffectEnhancements(UpgradeId).Evasion, "Evasão")
    Call AddText(AttributeEffectAttributes(AttributeId).Parry, Level, AttributeEffectEnhancements(UpgradeId).Parry, "Aparo")
    Call AddText(AttributeEffectAttributes(AttributeId).Block, Level, AttributeEffectEnhancements(UpgradeId).Block, "Bloqueio")
    Call AddText(AttributeEffectAttributes(AttributeId).MagicAttack, Level, AttributeEffectEnhancements(UpgradeId).MagicAttack, "Ataque Mágico")
    Call AddText(AttributeEffectAttributes(AttributeId).MagicDefense, Level, AttributeEffectEnhancements(UpgradeId).MagicDefense, "Defesa Mágica")
    Call AddText(AttributeEffectAttributes(AttributeId).MagicAccuracy, Level, AttributeEffectEnhancements(UpgradeId).MagicAccuracy, "Precisão Mágica")
    Call AddText(AttributeEffectAttributes(AttributeId).MagicResist, Level, AttributeEffectEnhancements(UpgradeId).MagicResist, "Resistência Mágica")
    Call AddText(AttributeEffectAttributes(AttributeId).Concentration, Level, AttributeEffectEnhancements(UpgradeId).Concentration, "Concentração")
    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).CritRate + (Level * AttributeEffectEnhancements(UpgradeId).CritRate), "Taxa Crítica")
    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).CritDamage + (Level * AttributeEffectEnhancements(UpgradeId).CritDamage), "Dano Crítico")
    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).ResistCritRate + (Level * AttributeEffectEnhancements(UpgradeId).ResistCritRate), "Res. Taxa Crítica")
    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).ResistCritDamage + (Level * AttributeEffectEnhancements(UpgradeId).ResistCritDamage), "Res. Dano Crítico")
    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).HealingPower + (Level * AttributeEffectEnhancements(UpgradeId).HealingPower), "Poder de Cura")
    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).Amplification + (Level * AttributeEffectEnhancements(UpgradeId).Amplification), "Amplificação")
    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).FinalDamage + (Level * AttributeEffectEnhancements(UpgradeId).FinalDamage), "Dano Final")
    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).Enmity + (Level * AttributeEffectEnhancements(UpgradeId).Enmity), "Inimizade")
    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).AttackSpeed + (Level * AttributeEffectEnhancements(UpgradeId).AttackSpeed), "Vel. Ataque")
    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).CastSpeed + (Level * AttributeEffectEnhancements(UpgradeId).CastSpeed), "Vel. Conjuração")
    Call AddText(AttributeEffectAttributes(AttributeId).SilenceResistance, Level, AttributeEffectEnhancements(UpgradeId).SilenceResistance, "Res. Silêncio")
    Call AddText(AttributeEffectAttributes(AttributeId).BlindResistance, Level, AttributeEffectEnhancements(UpgradeId).BlindResistance, "Res. Cegar")
    Call AddText(AttributeEffectAttributes(AttributeId).StunResistance, Level, AttributeEffectEnhancements(UpgradeId).StunResistance, "Res. Atordoar")
    Call AddText(AttributeEffectAttributes(AttributeId).StumbleResistance, Level, AttributeEffectEnhancements(UpgradeId).Concentration, "Res. Queda")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementAttack(Elements.Element_Fire), Level, AttributeEffectEnhancements(UpgradeId).ElementAttack(Elements.Element_Fire), "Elemento Fogo")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementAttack(Elements.Element_Water), Level, AttributeEffectEnhancements(UpgradeId).ElementAttack(Elements.Element_Water), "Elemento Água")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementAttack(Elements.Element_Earth), Level, AttributeEffectEnhancements(UpgradeId).ElementAttack(Elements.Element_Earth), "Elemento Terra")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementAttack(Elements.Element_Wind), Level, AttributeEffectEnhancements(UpgradeId).ElementAttack(Elements.Element_Wind), "Elemento Vento")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementAttack(Elements.Element_Light), Level, AttributeEffectEnhancements(UpgradeId).ElementAttack(Elements.Element_Light), "Elemento Luz")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementAttack(Elements.Element_Dark), Level, AttributeEffectEnhancements(UpgradeId).ElementAttack(Elements.Element_Dark), "Elemento Trevas")
End Sub

Private Sub AddText(ByRef Stat As AttributeRec, ByVal Level As Long, ByRef Upgrade As AttributeRec, ByVal Text As String)
    If Stat.Value > 0 Or Upgrade.Value > 0 Then
        Dim Value As Long

        If Stat.Percentage Then
            Value = CLng((Stat.Value + CSng(Level * Upgrade.Value)) * 100)
            If Value > 0 Then Call AddDescInfo(Text & ": " & Multiplier & Value & "%")
        Else
            Value = CLng(Stat.Value) + CLng(Level * Upgrade.Value)
            If Value > 0 Then Call AddDescInfo(Text & ": " & Multiplier & Value)
        End If
    End If
End Sub
Private Sub AddTextPercentage(ByVal StatValue As Single, ByVal Text As String, Optional HasRawValue As Boolean = False)
    If StatValue > 0 Then
        Dim Value As Long
        Value = CLng(StatValue * 100)

        If HasRawValue Then
            Call AddDescInfo(Text & ": " & Multiplier & Value)
        Else
            Call AddDescInfo(Text & ": " & Multiplier & Value & "%")
        End If
    End If
End Sub

Public Function GetSkillTypeName(ByVal SType As SkillType) As String
    Select Case SType
    Case SkillType.SkillType_Active
        GetSkillTypeName = "Ativa"
    Case SkillType.SkillType_Passive
        GetSkillTypeName = "Passiva"
    End Select
End Function

Public Function GetSkillAttributeTypeName(ByVal AttributeType As SkillAttributeType) As String
    Select Case AttributeType
    Case SkillAttributeType.SkillAttributeType_Physic
        GetSkillAttributeTypeName = "Físico"
    Case SkillAttributeType.SkillAttributeType_Magic
        GetSkillAttributeTypeName = "Mágico"
    End Select
End Function

Public Function GetSkillElementTypeName(ByVal ElementType As Elements) As String
    Select Case ElementType
    Case Elements.Element_Fire
        GetSkillElementTypeName = "Fogo"
    Case Elements.Element_Water
        GetSkillElementTypeName = "Água"
    Case Elements.Element_Wind
        GetSkillElementTypeName = "Vento"
    Case Elements.Element_Earth
        GetSkillElementTypeName = "Terra"
    Case Elements.Element_Light
        GetSkillElementTypeName = "Luz"
    Case Elements.Element_Dark
        GetSkillElementTypeName = "Trevas"
    End Select
End Function

Public Function GetSkillCostTypeName(ByVal CostType As SkillCostType) As String
    GetSkillCostTypeName = GetSkillVitalTypeName(CostType)
End Function

Public Function GetSkillVitalTypeName(ByVal VitalType As SkillVitalType) As String
    Select Case VitalType
    Case SkillVitalType.SkillVitalType_HP
        GetSkillVitalTypeName = "HP"
    Case SkillVitalType.SkillVitalType_MP
        GetSkillVitalTypeName = "MP"
    Case SkillVitalType.SkillVitalType_Special
        GetSkillVitalTypeName = "Special"
    End Select
End Function

Public Function GetSkillTargetTypeName(ByVal TargetType As SkillTargetType, ByVal UseUniqueAsText As Boolean) As String
    Select Case TargetType
    Case SkillTargetType.SkillTargetType_AoE
        GetSkillTargetTypeName = "Área"
    Case SkillTargetType.SkillTargetType_Group
        GetSkillTargetTypeName = "Grupo"
    Case SkillTargetType.SkillTargetType_Caster
        GetSkillTargetTypeName = "Portador"
    Case SkillTargetType.SkillTargetType_Single
        If UseUniqueAsText Then GetSkillTargetTypeName = "Único"
        If Not UseUniqueAsText Then GetSkillTargetTypeName = "Alvo"
    End Select
End Function

Public Function GetSkillEffectTypeName(ByVal EffectType As SkillEffectType) As String
    Select Case EffectType
    Case SkillEffectType.SkillEffectType_Aura
        GetSkillEffectTypeName = "Aura"
    Case SkillEffectType.SkillEffectType_Buff
        GetSkillEffectTypeName = "Buff"
    Case SkillEffectType.SkillEffectType_Damage
        GetSkillEffectTypeName = "Dano"
    Case SkillEffectType.SkillEffectType_DoT
        GetSkillEffectTypeName = "Dano ao longo do tempo"
    Case SkillEffectType.SkillEffectType_Heal
        GetSkillEffectTypeName = "Cura"
    Case SkillEffectType.SkillEffectType_HoT
        GetSkillEffectTypeName = "Cura ao longo do tempo"
    Case SkillEffectType.SkillEffectType_Teleport
        GetSkillEffectTypeName = "Teletransporte"
    Case SkillEffectType.SkillEffectType_Steal
        GetSkillEffectTypeName = "Roubo"
    Case SkillEffectType.SkillEffectType_Silence
        GetSkillEffectTypeName = "Silêncio"
    Case SkillEffectType.SkillEffectType_Blind
        GetSkillEffectTypeName = "Cegar"
    Case SkillEffectType.SkillEffectType_Dash
        GetSkillEffectTypeName = "Arrancada"
    Case SkillEffectType.SkillEffectType_Dispel
        GetSkillEffectTypeName = "Dissipar"
    Case SkillEffectType.SkillEffectType_Cleanse
        GetSkillEffectTypeName = "Purificar"
    Case SkillEffectType.SkillEffectType_Immobilize
        GetSkillEffectTypeName = "Imobilizar"
    End Select
End Function

Public Function GetPlayerSkillEffect(ByVal SkillSlot As Long, ByVal EffectIndex As Long) As PlayerSkillRec
    Dim SkillId As Long, i As Long
    Dim SkillEffectIndex As Long
    
    SkillId = PlayerSkill(SkillSlot).Id
    
    If SkillId = 0 Then
        Exit Function
    End If

    If PlayerSkill(SkillSlot).TargetType > SkillTargetType_None Then
        GetPlayerSkillEffect.TargetType = PlayerSkill(SkillSlot).TargetType
    Else
        GetPlayerSkillEffect.TargetType = Skill(SkillId).TargetType
    End If

    If PlayerSkill(SkillSlot).Element > 0 Then
        GetPlayerSkillEffect.Element = PlayerSkill(SkillSlot).Element
    Else
        GetPlayerSkillEffect.Element = Skill(SkillId).ElementType
    End If

    GetPlayerSkillEffect.Amplification = PlayerSkill(SkillSlot).Amplification + Skill(SkillId).Amplification
    GetPlayerSkillEffect.Range = PlayerSkill(SkillSlot).Range + Skill(SkillId).Range
    GetPlayerSkillEffect.CastTime = PlayerSkill(SkillSlot).CastTime + Skill(SkillId).CastTime
    GetPlayerSkillEffect.Cooldown = PlayerSkill(SkillSlot).Cooldown + Skill(SkillId).Cooldown
    GetPlayerSkillEffect.Stun = PlayerSkill(SkillSlot).Stun + Skill(SkillId).StunDuration
    GetPlayerSkillEffect.Cost = PlayerSkill(SkillSlot).Cost + Skill(SkillId).Cost

    ' Verifica se o efeito existe na habilidade.
    For i = 1 To Skill(SkillId).EffectCount
        If Skill(SkillId).Effect(i).EffectType = EffectIndex Then
            SkillEffectIndex = i
            Exit For
        End If
    Next

    ' Se o efeito não existir, sai do método.
    If SkillEffectIndex = 0 Then
        Exit Function
    End If

    If EffectIndex > 0 Then
        GetPlayerSkillEffect.Effect(EffectIndex).Damage = PlayerSkill(SkillSlot).Effect(EffectIndex).Damage + Skill(SkillId).Effect(SkillEffectIndex).Damage
        GetPlayerSkillEffect.Effect(EffectIndex).DamagePerLevel = PlayerSkill(SkillSlot).Effect(EffectIndex).DamagePerLevel + Skill(SkillId).Effect(SkillEffectIndex).DamagePerLevel
        GetPlayerSkillEffect.Effect(EffectIndex).Interval = PlayerSkill(SkillSlot).Effect(EffectIndex).Interval + Skill(SkillId).Effect(SkillEffectIndex).Interval
        GetPlayerSkillEffect.Effect(EffectIndex).Duration = PlayerSkill(SkillSlot).Effect(EffectIndex).Duration + Skill(SkillId).Effect(SkillEffectIndex).Duration

        If PlayerSkill(SkillSlot).Effect(EffectIndex).VitalType > 0 Then
            GetPlayerSkillEffect.Effect(EffectIndex).VitalType = PlayerSkill(SkillSlot).Effect(EffectIndex).VitalType
        Else
            GetPlayerSkillEffect.Effect(EffectIndex).VitalType = Skill(SkillId).Effect(SkillEffectIndex).VitalType
        End If

        If PlayerSkill(SkillSlot).Effect(EffectIndex).TargetType > 0 Then
            GetPlayerSkillEffect.Effect(EffectIndex).TargetType = PlayerSkill(SkillSlot).Effect(EffectIndex).TargetType
        Else
            GetPlayerSkillEffect.Effect(EffectIndex).TargetType = Skill(SkillId).Effect(SkillEffectIndex).TargetType
        End If

        If PlayerSkill(SkillSlot).Effect(EffectIndex).EffectId > 0 Then
            GetPlayerSkillEffect.Effect(EffectIndex).EffectId = PlayerSkill(SkillSlot).Effect(EffectIndex).EffectId
        Else
            GetPlayerSkillEffect.Effect(EffectIndex).EffectId = Skill(SkillId).Effect(SkillEffectIndex).EffectId
        End If
    End If

End Function


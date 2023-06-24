Attribute VB_Name = "AttributeEffect_Description"
Option Explicit

Private Multiplier As String

Public Sub ShowAttributeEffectDesc(x As Long, Y As Long, ByVal Target As Long, ByVal TargetType, ByVal PartyMemberIndex As Long, Slot As Long)
    If Slot = 0 Then
        Exit Sub
    End If

    Dim Colour As Long
    Dim WindowIndex As Long, ControlIndex As Long
    Dim ControlType As Long

    Dim EffectId As Long
    Dim EffectLevel As Long
    Dim EffectDuration As Long

    If PartyMemberIndex > 0 Then
        EffectId = GetPartyIconId(PartyMemberIndex, Slot)
        EffectLevel = GetPartyIconLevel(PartyMemberIndex, Slot)
        EffectDuration = GetPartyIconDuration(PartyMemberIndex, Slot)
    Else
        If TargetType = TargetTypePlayer Then
            If Target > 0 Then
                EffectId = GetPlayerIconId(Target, Slot)
                EffectLevel = GetPlayerIconLevel(Target, Slot)
                EffectDuration = GetPlayerIconDuration(Target, Slot)
            End If
        ElseIf TargetType = TargetTypeNpc Then
            If Target > 0 Then
                EffectId = GetNpcIconId(Target, Slot)
                EffectLevel = GetNpcIconLevel(Target, Slot)
                EffectDuration = GetNpcIconDuration(Target, Slot)
            End If
        End If
    End If

    If EffectId = 0 Then Exit Sub

    DescType = 3    ' spell
    DescItem = EffectId

    WindowIndex = GetWindowIndex("winDescription")

    ' set position
    Windows(WindowIndex).Window.Left = x
    Windows(WindowIndex).Window.Top = Y
    Windows(WindowIndex).Window.Width = 225

    ' show the window
    ShowWindow WindowIndex, , False

    ' exit out early if last is same
    If (DescLastType = DescType) And (DescLastItem = DescItem) And DescLastLevel = EffectLevel Then Exit Sub

    DescLastType = DescType
    DescLastItem = DescItem
    DescLastLevel = EffectLevel

    Windows(WindowIndex).Controls(GetControlIndex("winDescription", "lblPrice")).Visible = False

    ' set variables
    With Windows(WindowIndex)
        ' set name
        ControlIndex = GetControlIndex("winDescription", "lblName")
        .Controls(ControlIndex).TextColour = BrightGreen
        .Controls(ControlIndex).Text = AttributeEffect(EffectId).Name
        .Controls(ControlIndex).Width = 225

        ControlIndex = GetControlIndex("winDescription", "lblType")
        .Controls(ControlIndex).TextColour = BrightGreen
        .Controls(ControlIndex).Width = 225
        .Controls(ControlIndex).Text = "Level " & EffectLevel
    End With

    ' clear
    ReDim DescText(1 To 1) As TextColourRec

    AddDescInfo ""

    If AttributeEffect(EffectId).EffectType = AttributeEffectType_Increase Then
        AddDescInfo "Buff", Gold
        Multiplier = vbNullString
    ElseIf AttributeEffect(EffectId).EffectType = AttributeEffectType_Decrease Then
        Multiplier = "-"
        AddDescInfo "Anormalidade", BrightRed
    End If

    Call AddAttributeEffectDescValues(EffectId, EffectLevel)

    ControlIndex = GetControlIndex("winDescription", "lblDesc")
    Windows(WindowIndex).Controls(ControlIndex).Text = AttributeEffect(EffectId).Description
    Windows(WindowIndex).Controls(ControlIndex).Visible = True
    Windows(WindowIndex).Controls(ControlIndex).Top = 50 + (UBound(DescText) * 12)
    Windows(WindowIndex).Controls(ControlIndex).Width = 205

    Windows(WindowIndex).Window.Height = 100 + (UBound(DescText) * 12)
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

    If AttributeEffect(EffectId).Dispelled = 0 Then
        AddDescInfo "Não pode ser removido", Coral
    Else
        AddDescInfo "Pode ser removido", Coral
    End If

    AddDescInfo ""
    Call AddText(AttributeEffectAttributes(AttributeId).Stat(Stats.Strength), Level, AttributeEffectEnhancements(UpgradeId).Stat(Stats.Strength), "Força")
    Call AddText(AttributeEffectAttributes(AttributeId).Stat(Stats.Agility), Level, AttributeEffectEnhancements(UpgradeId).Stat(Stats.Agility), "Agilidade")
    Call AddText(AttributeEffectAttributes(AttributeId).Stat(Stats.Constitution), Level, AttributeEffectEnhancements(UpgradeId).Stat(Stats.Constitution), "Constituição")
    Call AddText(AttributeEffectAttributes(AttributeId).Stat(Stats.Intelligence), Level, AttributeEffectEnhancements(UpgradeId).Stat(Stats.Intelligence), "Inteligência")
    Call AddText(AttributeEffectAttributes(AttributeId).Stat(Stats.Spirit), Level, AttributeEffectEnhancements(UpgradeId).Stat(Stats.Spirit), "Espírito")
    Call AddText(AttributeEffectAttributes(AttributeId).Stat(Stats.Will), Level, AttributeEffectEnhancements(UpgradeId).Stat(Stats.Will), "Vontade")
    Call AddText(AttributeEffectAttributes(AttributeId).Vital(HP), Level, AttributeEffectEnhancements(UpgradeId).Vital(HP), "HP")
    Call AddText(AttributeEffectAttributes(AttributeId).Vital(MP), Level, AttributeEffectEnhancements(UpgradeId).Vital(MP), "MP")
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
    Call AddText(AttributeEffectAttributes(AttributeId).StumbleResistance, Level, AttributeEffectEnhancements(UpgradeId).StumbleResistance, "Res. Queda")

    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).PveAttack + (Level * AttributeEffectEnhancements(UpgradeId).PveAttack), "Ataque PvE")
    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).PveDefense + (Level * AttributeEffectEnhancements(UpgradeId).PveDefense), "Defesa PvE")
    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).PvpAttack + (Level * AttributeEffectEnhancements(UpgradeId).PvpAttack), "Ataque PvP")
    Call AddTextPercentage(AttributeEffectAttributes(AttributeId).PvpDefense + (Level * AttributeEffectEnhancements(UpgradeId).PvpDefense), "Defesa PvP")

    Call AddText(AttributeEffectAttributes(AttributeId).ElementAttack(Elements.Element_Fire), Level, AttributeEffectEnhancements(UpgradeId).ElementAttack(Elements.Element_Fire), "Ataque Elemento Fogo")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementAttack(Elements.Element_Water), Level, AttributeEffectEnhancements(UpgradeId).ElementAttack(Elements.Element_Water), "Ataque Elemento Água")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementAttack(Elements.Element_Earth), Level, AttributeEffectEnhancements(UpgradeId).ElementAttack(Elements.Element_Earth), "Ataque Elemento Terra")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementAttack(Elements.Element_Wind), Level, AttributeEffectEnhancements(UpgradeId).ElementAttack(Elements.Element_Wind), "Ataque Elemento Vento")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementAttack(Elements.Element_Light), Level, AttributeEffectEnhancements(UpgradeId).ElementAttack(Elements.Element_Light), "Ataque Elemento Luz")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementAttack(Elements.Element_Dark), Level, AttributeEffectEnhancements(UpgradeId).ElementAttack(Elements.Element_Dark), "Ataque Elemento Trevas")

    Call AddText(AttributeEffectAttributes(AttributeId).ElementDefense(Elements.Element_Fire), Level, AttributeEffectEnhancements(UpgradeId).ElementDefense(Elements.Element_Fire), "Defesa Elemento Fogo")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementDefense(Elements.Element_Water), Level, AttributeEffectEnhancements(UpgradeId).ElementDefense(Elements.Element_Water), "Defesa Elemento Água")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementDefense(Elements.Element_Earth), Level, AttributeEffectEnhancements(UpgradeId).ElementDefense(Elements.Element_Earth), "Defesa Elemento Terra")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementDefense(Elements.Element_Wind), Level, AttributeEffectEnhancements(UpgradeId).ElementDefense(Elements.Element_Wind), "Defesa Elemento Vento")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementDefense(Elements.Element_Light), Level, AttributeEffectEnhancements(UpgradeId).ElementDefense(Elements.Element_Light), "Defesa Elemento Luz")
    Call AddText(AttributeEffectAttributes(AttributeId).ElementDefense(Elements.Element_Dark), Level, AttributeEffectEnhancements(UpgradeId).ElementDefense(Elements.Element_Dark), "Defesa Elemento Trevas")

End Sub


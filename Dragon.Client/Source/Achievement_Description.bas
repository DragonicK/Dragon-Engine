Attribute VB_Name = "Achievement_Description"
Option Explicit

Private HasAttributes As Boolean

Public Sub ShowAchievementDesc(X As Long, Y As Long, ByVal AchievementId As Long, ByVal Acquired As Byte)
    Dim Colour As Long
    Dim WindowIndex As Long, ControlIndex As Long
    Dim ControlType As Long

    DescType = 4    ' achievement
    DescItem = AchievementId

    WindowIndex = GetWindowIndex("winDescription")

    ' set position
    Windows(WindowIndex).Window.Left = X
    Windows(WindowIndex).Window.Top = Y
    Windows(WindowIndex).Window.Width = 225

    ' show the window
    ShowWindow WindowIndex, , False

    ' exit out early if last is same
    If (DescLastType = DescType) And (DescLastItem = DescItem) Then Exit Sub

    DescLastType = DescType
    DescLastItem = DescItem

    Windows(WindowIndex).Controls(GetControlIndex("winDescription", "lblPrice")).Visible = False

    ' set variables
    With Windows(WindowIndex)
        ' set name
        ControlIndex = GetControlIndex("winDescription", "lblName")
        .Controls(ControlIndex).TextColour = White    ' GetRarityColor(Achievement(AchievementId).Rarity)
        .Controls(ControlIndex).Text = Achievement(AchievementId).Name
        .Controls(ControlIndex).Width = 225

        ControlIndex = GetControlIndex("winDescription", "lblType")
        .Controls(ControlIndex).TextColour = BrightGreen
        .Controls(ControlIndex).Width = 225
        .Controls(ControlIndex).Text = "Pontos: " & Achievement(AchievementId).Points
    End With

    ' clear
    ReDim DescText(1 To 1) As TextColourRec

    AddDescInfo ""

    If Acquired > 0 Then
        AddDescInfo "Conquista adquirida", Gold
    Else
        AddDescInfo "Conquista não adquirida", BrightRed
    End If

    AddDescInfo ""
    AddDescInfo "Atributos", Coral

    HasAttributes = False

    Call AddAchievementDescValues(Achievement(AchievementId).AttributeId)

    If Not HasAttributes Then
        AddDescInfo "Sem atributos"
    End If

    ControlIndex = GetControlIndex("winDescription", "lblDesc")
    Windows(WindowIndex).Controls(ControlIndex).Text = Achievement(AchievementId).Description
    Windows(WindowIndex).Controls(ControlIndex).Visible = True
    Windows(WindowIndex).Controls(ControlIndex).Top = 50 + (UBound(DescText) * 12)
    Windows(WindowIndex).Controls(ControlIndex).Width = 205

    Windows(WindowIndex).Window.Height = 130 + (UBound(DescText) * 12)
End Sub

Private Sub AddText(ByRef Stat As AttributeRec, ByVal Level As Long, ByRef Upgrade As AttributeRec, ByVal Text As String)
    If Stat.Value > 0 Or Upgrade.Value > 0 Then
        Dim Value As Long

        If Stat.Percentage Then
            Value = CLng((Stat.Value + CSng(Level * Upgrade.Value)) * 100)
            If Value > 0 Then Call AddDescInfo(Text & ": " & Value & "%")
        Else
            Value = CLng(Stat.Value) + CLng(Level * Upgrade.Value)
            If Value > 0 Then Call AddDescInfo(Text & ": " & Value)
        End If

        HasAttributes = True
    End If
End Sub

Private Sub AddTextPercentage(ByVal StatValue As Single, ByVal Text As String, Optional HasRawValue As Boolean = False)
    If StatValue > 0 Then
        Dim Value As Long
        Value = CLng(StatValue * 100)

        If HasRawValue Then
            Call AddDescInfo(Text & ": " & Value)
        Else
            Call AddDescInfo(Text & ": " & Value & "%")
        End If
        
        HasAttributes = True
    End If
End Sub

Private Sub AddAchievementDescValues(ByVal AttributeId As Long)
    Dim UpgradeId As Long
    Dim Level As Long

    If AttributeId < 1 Or AttributeId > MaxAchievementAttributes Then
        Exit Sub
    End If

    Call AddText(AchievementAttributes(AttributeId).Stat(Stats.Strength), Level, AchievementAttributes(UpgradeId).Stat(Stats.Strength), "Força")
    Call AddText(AchievementAttributes(AttributeId).Stat(Stats.Agility), Level, AchievementAttributes(UpgradeId).Stat(Stats.Agility), "Agilidade")
    Call AddText(AchievementAttributes(AttributeId).Stat(Stats.Constitution), Level, AchievementAttributes(UpgradeId).Stat(Stats.Constitution), "Constituição")
    Call AddText(AchievementAttributes(AttributeId).Stat(Stats.Intelligence), Level, AchievementAttributes(UpgradeId).Stat(Stats.Intelligence), "Inteligência")
    Call AddText(AchievementAttributes(AttributeId).Stat(Stats.Spirit), Level, AchievementAttributes(UpgradeId).Stat(Stats.Spirit), "Espírito")
    Call AddText(AchievementAttributes(AttributeId).Stat(Stats.Will), Level, AchievementAttributes(UpgradeId).Stat(Stats.Will), "Vontade")
    Call AddText(AchievementAttributes(AttributeId).Vital(HP), Level, AchievementAttributes(UpgradeId).Vital(HP), "HP")
    Call AddText(AchievementAttributes(AttributeId).Vital(MP), Level, AchievementAttributes(UpgradeId).Vital(MP), "MP")
    Call AddText(AchievementAttributes(AttributeId).Attack, Level, AchievementAttributes(UpgradeId).Attack, "Ataque")
    Call AddText(AchievementAttributes(AttributeId).Defense, Level, AchievementAttributes(UpgradeId).Defense, "Defesa")
    Call AddText(AchievementAttributes(AttributeId).Accuracy, Level, AchievementAttributes(UpgradeId).Accuracy, "Precisão")
    Call AddText(AchievementAttributes(AttributeId).Evasion, Level, AchievementAttributes(UpgradeId).Evasion, "Evasão")
    Call AddText(AchievementAttributes(AttributeId).Parry, Level, AchievementAttributes(UpgradeId).Parry, "Aparo")
    Call AddText(AchievementAttributes(AttributeId).Block, Level, AchievementAttributes(UpgradeId).Block, "Bloqueio")
    Call AddText(AchievementAttributes(AttributeId).MagicAttack, Level, AchievementAttributes(UpgradeId).MagicAttack, "Ataque Mágico")
    Call AddText(AchievementAttributes(AttributeId).MagicDefense, Level, AchievementAttributes(UpgradeId).MagicDefense, "Defesa Mágica")
    Call AddText(AchievementAttributes(AttributeId).MagicAccuracy, Level, AchievementAttributes(UpgradeId).MagicAccuracy, "Precisão Mágica")
    Call AddText(AchievementAttributes(AttributeId).MagicResist, Level, AchievementAttributes(UpgradeId).MagicResist, "Resistência Mágica")
    Call AddText(AchievementAttributes(AttributeId).Concentration, Level, AchievementAttributes(UpgradeId).Concentration, "Concentração")
    Call AddTextPercentage(AchievementAttributes(AttributeId).CritRate + (Level * AchievementAttributes(UpgradeId).CritRate), "Taxa Crítica")
    Call AddTextPercentage(AchievementAttributes(AttributeId).CritDamage + (Level * AchievementAttributes(UpgradeId).CritDamage), "Dano Crítico")
    Call AddTextPercentage(AchievementAttributes(AttributeId).ResistCritRate + (Level * AchievementAttributes(UpgradeId).ResistCritRate), "Res. Taxa Crítica")
    Call AddTextPercentage(AchievementAttributes(AttributeId).ResistCritDamage + (Level * AchievementAttributes(UpgradeId).ResistCritDamage), "Res. Dano Crítico")
    Call AddTextPercentage(AchievementAttributes(AttributeId).HealingPower + (Level * AchievementAttributes(UpgradeId).HealingPower), "Poder de Cura")
    Call AddTextPercentage(AchievementAttributes(AttributeId).Amplification + (Level * AchievementAttributes(UpgradeId).Amplification), "Amplificação")
    Call AddTextPercentage(AchievementAttributes(AttributeId).FinalDamage + (Level * AchievementAttributes(UpgradeId).FinalDamage), "Dano Final")
    Call AddTextPercentage(AchievementAttributes(AttributeId).Enmity + (Level * AchievementAttributes(UpgradeId).Enmity), "Inimizade")
    Call AddTextPercentage(AchievementAttributes(AttributeId).AttackSpeed + (Level * AchievementAttributes(UpgradeId).AttackSpeed), "Vel. Ataque")
    Call AddTextPercentage(AchievementAttributes(AttributeId).CastSpeed + (Level * AchievementAttributes(UpgradeId).CastSpeed), "Vel. Conjuração")
    Call AddText(AchievementAttributes(AttributeId).SilenceResistance, Level, AchievementAttributes(UpgradeId).SilenceResistance, "Res. Silêncio")
    Call AddText(AchievementAttributes(AttributeId).BlindResistance, Level, AchievementAttributes(UpgradeId).BlindResistance, "Res. Cegar")
    Call AddText(AchievementAttributes(AttributeId).StunResistance, Level, AchievementAttributes(UpgradeId).StunResistance, "Res. Atordoar")
    Call AddText(AchievementAttributes(AttributeId).StumbleResistance, Level, AchievementAttributes(UpgradeId).Concentration, "Res. Queda")
    Call AddTextPercentage(AchievementAttributes(AttributeId).PveAttack + (Level * AchievementAttributes(UpgradeId).PveAttack), "Ataque PvE")
    Call AddTextPercentage(AchievementAttributes(AttributeId).PveDefense + (Level * AchievementAttributes(UpgradeId).PveDefense), "Defesa PvE")
    Call AddTextPercentage(AchievementAttributes(AttributeId).PvpAttack + (Level * AchievementAttributes(UpgradeId).PvpAttack), "Ataque PvP")
    Call AddTextPercentage(AchievementAttributes(AttributeId).PvpDefense + (Level * AchievementAttributes(UpgradeId).PvpDefense), "Defesa PvP")
    ' Elemental Attack
    Call AddText(AchievementAttributes(AttributeId).ElementAttack(Elements.Element_Fire), Level, AchievementAttributes(UpgradeId).ElementAttack(Elements.Element_Fire), "Ataque Elemento Fogo")
    Call AddText(AchievementAttributes(AttributeId).ElementAttack(Elements.Element_Water), Level, AchievementAttributes(UpgradeId).ElementAttack(Elements.Element_Water), "Ataque Elemento Água")
    Call AddText(AchievementAttributes(AttributeId).ElementAttack(Elements.Element_Earth), Level, AchievementAttributes(UpgradeId).ElementAttack(Elements.Element_Earth), "Ataque Elemento Terra")
    Call AddText(AchievementAttributes(AttributeId).ElementAttack(Elements.Element_Wind), Level, AchievementAttributes(UpgradeId).ElementAttack(Elements.Element_Wind), "Ataque Elemento Vento")
    Call AddText(AchievementAttributes(AttributeId).ElementAttack(Elements.Element_Light), Level, AchievementAttributes(UpgradeId).ElementAttack(Elements.Element_Light), "Ataque Elemento Luz")
    Call AddText(AchievementAttributes(AttributeId).ElementAttack(Elements.Element_Dark), Level, AchievementAttributes(UpgradeId).ElementAttack(Elements.Element_Dark), "Ataque Elemento Trevas")
    ' Elemental Defense
    Call AddText(AchievementAttributes(AttributeId).ElementDefense(Elements.Element_Fire), Level, AchievementAttributes(UpgradeId).ElementDefense(Elements.Element_Fire), "Defesa Elemento Fogo")
    Call AddText(AchievementAttributes(AttributeId).ElementDefense(Elements.Element_Water), Level, AchievementAttributes(UpgradeId).ElementDefense(Elements.Element_Water), "Defesa Elemento Água")
    Call AddText(AchievementAttributes(AttributeId).ElementDefense(Elements.Element_Earth), Level, AchievementAttributes(UpgradeId).ElementDefense(Elements.Element_Earth), "Defesa Elemento Terra")
    Call AddText(AchievementAttributes(AttributeId).ElementDefense(Elements.Element_Wind), Level, AchievementAttributes(UpgradeId).ElementDefense(Elements.Element_Wind), "Defesa Elemento Vento")
    Call AddText(AchievementAttributes(AttributeId).ElementDefense(Elements.Element_Light), Level, AchievementAttributes(UpgradeId).ElementDefense(Elements.Element_Light), "Defesa Elemento Luz")
    Call AddText(AchievementAttributes(AttributeId).ElementDefense(Elements.Element_Dark), Level, AchievementAttributes(UpgradeId).ElementDefense(Elements.Element_Dark), " Defesa Elemento Trevas")
End Sub



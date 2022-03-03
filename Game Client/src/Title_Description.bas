Attribute VB_Name = "Title_Description"
Option Explicit

Private HasAttributes As Boolean

Public Sub ShowTitleDesc(X As Long, Y As Long, ByVal TitleId As Long)
    Dim Colour As Long
    Dim WindowIndex As Long, ControlIndex As Long
    Dim ControlType As Long

    DescType = 9    ' achievement
    DescItem = TitleId

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
        .Controls(ControlIndex).textColour = GetRarityColor(Title(TitleId).Rarity)
        .Controls(ControlIndex).Text = Title(TitleId).Name
        .Controls(ControlIndex).Width = 225

        ControlIndex = GetControlIndex("winDescription", "lblType")
        '.Controls(ControlIndex).textColour = BrightGreen
        '.Controls(ControlIndex).Width = 225
        .Controls(ControlIndex).Text = vbNullString
    End With

    ' clear
    ReDim DescText(1 To 1) As TextColourRec

    AddDescInfo ""

    AddDescInfo "Atributos", Coral

    AddDescInfo ""
    
    HasAttributes = False

    Call AddTitleDescValues(Title(TitleId).AttributeId)

    If Not HasAttributes Then
        AddDescInfo "Sem atributos"
    End If

    ControlIndex = GetControlIndex("winDescription", "lblDesc")
    Windows(WindowIndex).Controls(ControlIndex).Text = Title(TitleId).Description
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

' Cria a lista de descrição do título.
Private Sub AddTitleDescValues(ByVal TitleNum As Long)
    Dim AttributeId As Long

    AttributeId = Title(TitleNum).AttributeId

    If AttributeId < 1 Or AttributeId > MaxTitlesAttributes Then
        Exit Sub
    End If

    Dim Level As Long
    Dim UpgradeId As Long

    Call AddText(TitleAttributes(AttributeId).Stat(Stats.Strength), Level, TitleAttributes(UpgradeId).Stat(Stats.Strength), "Força")
    Call AddText(TitleAttributes(AttributeId).Stat(Stats.Agility), Level, TitleAttributes(UpgradeId).Stat(Stats.Agility), "Agilidade")
    Call AddText(TitleAttributes(AttributeId).Stat(Stats.Will), Level, TitleAttributes(UpgradeId).Stat(Stats.Will), "Vontade")
    Call AddText(TitleAttributes(AttributeId).Stat(Stats.Constitution), Level, TitleAttributes(UpgradeId).Stat(Stats.Constitution), "Constituição")
    Call AddText(TitleAttributes(AttributeId).Stat(Stats.Intelligence), Level, TitleAttributes(UpgradeId).Stat(Stats.Intelligence), "Inteligência")
    Call AddText(TitleAttributes(AttributeId).Stat(Stats.Spirit), Level, TitleAttributes(UpgradeId).Stat(Stats.Spirit), "Espírito")
    Call AddText(TitleAttributes(AttributeId).Vital(HP), Level, TitleAttributes(UpgradeId).Vital(HP), "HP")
    Call AddText(TitleAttributes(AttributeId).Vital(MP), Level, TitleAttributes(UpgradeId).Vital(MP), "MP")
    Call AddText(TitleAttributes(AttributeId).Attack, Level, TitleAttributes(UpgradeId).Attack, "Ataque")
    Call AddText(TitleAttributes(AttributeId).Defense, Level, TitleAttributes(UpgradeId).Defense, "Defesa")
    Call AddText(TitleAttributes(AttributeId).Accuracy, Level, TitleAttributes(UpgradeId).Accuracy, "Precisão")
    Call AddText(TitleAttributes(AttributeId).Evasion, Level, TitleAttributes(UpgradeId).Evasion, "Evasão")
    Call AddText(TitleAttributes(AttributeId).Parry, Level, TitleAttributes(UpgradeId).Parry, "Aparo")
    Call AddText(TitleAttributes(AttributeId).Block, Level, TitleAttributes(UpgradeId).Block, "Bloqueio")
    Call AddText(TitleAttributes(AttributeId).MagicAttack, Level, TitleAttributes(UpgradeId).MagicAttack, "Ataque Mágico")
    Call AddText(TitleAttributes(AttributeId).MagicDefense, Level, TitleAttributes(UpgradeId).MagicDefense, "Defesa Mágica")
    Call AddText(TitleAttributes(AttributeId).MagicAccuracy, Level, TitleAttributes(UpgradeId).MagicAccuracy, "Precisão Mágica")
    Call AddText(TitleAttributes(AttributeId).MagicResist, Level, TitleAttributes(UpgradeId).MagicResist, "Resistência Mágica")
    Call AddText(TitleAttributes(AttributeId).Concentration, Level, TitleAttributes(UpgradeId).Concentration, "Concentração")
    Call AddTextPercentage(TitleAttributes(AttributeId).CritRate + (Level * TitleAttributes(UpgradeId).CritRate), "Taxa Crítica")
    Call AddTextPercentage(TitleAttributes(AttributeId).CritDamage + (Level * TitleAttributes(UpgradeId).CritDamage), "Dano Crítico")
    Call AddTextPercentage(TitleAttributes(AttributeId).ResistCritRate + (Level * TitleAttributes(UpgradeId).ResistCritRate), "Res. Taxa Crítica")
    Call AddTextPercentage(TitleAttributes(AttributeId).ResistCritDamage + (Level * TitleAttributes(UpgradeId).ResistCritDamage), "Res. Dano Crítico")
    Call AddTextPercentage(TitleAttributes(AttributeId).HealingPower + (Level * TitleAttributes(UpgradeId).HealingPower), "Poder de Cura")
    Call AddTextPercentage(TitleAttributes(AttributeId).Amplification + (Level * TitleAttributes(UpgradeId).Amplification), "Amplificação")
    Call AddTextPercentage(TitleAttributes(AttributeId).FinalDamage + (Level * TitleAttributes(UpgradeId).FinalDamage), "Dano Final")
    Call AddTextPercentage(TitleAttributes(AttributeId).Enmity + (Level * TitleAttributes(UpgradeId).Enmity), "Inimizade")
    Call AddTextPercentage(TitleAttributes(AttributeId).AttackSpeed + (Level * TitleAttributes(UpgradeId).AttackSpeed), "Vel. Ataque")
    Call AddTextPercentage(TitleAttributes(AttributeId).CastSpeed + (Level * TitleAttributes(UpgradeId).CastSpeed), "Vel. Conjuração")
    Call AddText(TitleAttributes(AttributeId).SilenceResistance, Level, TitleAttributes(UpgradeId).SilenceResistance, "Res. Silêncio")
    Call AddText(TitleAttributes(AttributeId).BlindResistance, Level, TitleAttributes(UpgradeId).BlindResistance, "Res. Cegar")
    Call AddText(TitleAttributes(AttributeId).StunResistance, Level, TitleAttributes(UpgradeId).StunResistance, "Res. Atordoar")
    Call AddText(TitleAttributes(AttributeId).StumbleResistance, Level, TitleAttributes(UpgradeId).Concentration, "Res. Queda")
    Call AddTextPercentage(TitleAttributes(AttributeId).PveAttack + (Level * TitleAttributes(UpgradeId).PveAttack), "Ataque PvE")
    Call AddTextPercentage(TitleAttributes(AttributeId).PveDefense + (Level * TitleAttributes(UpgradeId).PveDefense), "Defesa PvE")
    Call AddTextPercentage(TitleAttributes(AttributeId).PvpAttack + (Level * TitleAttributes(UpgradeId).PvpAttack), "Ataque PvP")
    Call AddTextPercentage(TitleAttributes(AttributeId).PvpDefense + (Level * TitleAttributes(UpgradeId).PvpDefense), "Defesa PvP")
    ' Elemental Attack
    Call AddText(TitleAttributes(AttributeId).ElementAttack(Elements.Element_Fire), Level, TitleAttributes(UpgradeId).ElementAttack(Elements.Element_Fire), "Ataque Elemento Fogo")
    Call AddText(TitleAttributes(AttributeId).ElementAttack(Elements.Element_Water), Level, TitleAttributes(UpgradeId).ElementAttack(Elements.Element_Water), "Ataque Elemento Água")
    Call AddText(TitleAttributes(AttributeId).ElementAttack(Elements.Element_Earth), Level, TitleAttributes(UpgradeId).ElementAttack(Elements.Element_Earth), "Ataque Elemento Terra")
    Call AddText(TitleAttributes(AttributeId).ElementAttack(Elements.Element_Wind), Level, TitleAttributes(UpgradeId).ElementAttack(Elements.Element_Wind), "Ataque Elemento Vento")
    Call AddText(TitleAttributes(AttributeId).ElementAttack(Elements.Element_Light), Level, TitleAttributes(UpgradeId).ElementAttack(Elements.Element_Light), "Ataque Elemento Luz")
    Call AddText(TitleAttributes(AttributeId).ElementAttack(Elements.Element_Dark), Level, TitleAttributes(UpgradeId).ElementAttack(Elements.Element_Dark), "Ataque Elemento Trevas")
    ' Elemental Defense
    Call AddText(TitleAttributes(AttributeId).ElementDefense(Elements.Element_Fire), Level, TitleAttributes(UpgradeId).ElementDefense(Elements.Element_Fire), "Defesa Elemento Fogo")
    Call AddText(TitleAttributes(AttributeId).ElementDefense(Elements.Element_Water), Level, TitleAttributes(UpgradeId).ElementDefense(Elements.Element_Water), "Defesa Elemento Água")
    Call AddText(TitleAttributes(AttributeId).ElementDefense(Elements.Element_Earth), Level, TitleAttributes(UpgradeId).ElementDefense(Elements.Element_Earth), "Defesa Elemento Terra")
    Call AddText(TitleAttributes(AttributeId).ElementDefense(Elements.Element_Wind), Level, TitleAttributes(UpgradeId).ElementDefense(Elements.Element_Wind), "Defesa Elemento Vento")
    Call AddText(TitleAttributes(AttributeId).ElementDefense(Elements.Element_Light), Level, TitleAttributes(UpgradeId).ElementDefense(Elements.Element_Light), "Defesa Elemento Luz")
    Call AddText(TitleAttributes(AttributeId).ElementDefense(Elements.Element_Dark), Level, TitleAttributes(UpgradeId).ElementDefense(Elements.Element_Dark), "Defesa Elemento Trevas")
End Sub



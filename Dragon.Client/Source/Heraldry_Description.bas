Attribute VB_Name = "Heraldry_Description"
Option Explicit
Private Percentage As String

Public Sub ShowHeraldryDescription(X As Long, Y As Long, ByRef Inventory As InventoryRec, ByVal Price As Long)
    Dim Colour As Long, i As Long
    Dim WindowIndex As Long
    Dim CurrentHeight As Long
    Dim ControlType As Long

    ' set globals
    DescType = 4   ' heraldry
    DescItem = Inventory.Num
    DescLevel = Inventory.Level
    DescBound = Inventory.Bound
    DescAttribute = Inventory.AttributeId
    DescUpgrade = Inventory.UpgradeId

    WindowIndex = GetWindowIndex("winDescription")
    CurrentHeight = Windows(WindowIndex).Window.Height

    ' set position
    Windows(WindowIndex).Window.Left = X
    Windows(WindowIndex).Window.Top = Y
    Windows(WindowIndex).Window.Width = 225

    ' show the window
    ShowWindow WindowIndex, , False

    ' exit out early if last is same
    If (DescLastType = DescType) And (DescLastItem = DescItem) And (DescLastLevel = DescLevel) And (DescLastBound = DescBound) And (DescLastAttribute = DescAttribute) And (DescLastUpgrade = DescUpgrade) Then
        Exit Sub
    End If

    ' set last to this
    DescLastType = DescType
    DescLastItem = DescItem
    DescLastLevel = DescLevel
    DescLastBound = DescBound
    DescLastAttribute = DescAttribute
    DescLastUpgrade = DescUpgrade

    ControlType = GetControlIndex("winDescription", "lblType")
    Windows(WindowIndex).Controls(ControlType).Visible = True

    ' set variables
    Dim ItemName As String
    Dim ControlIndex As Long
    Dim LevelText As String
    Dim EquipNum As Long
    Dim HeraldryId As Long

    With Windows(WindowIndex)
        ControlIndex = GetControlIndex("winDescription", "lblName")

        ItemName = Item(Inventory.Num).Name

        If Inventory.Level > 0 Then
            .Controls(ControlIndex).Text = ItemName & " +" & Inventory.Level
        Else
            .Controls(ControlIndex).Text = ItemName
        End If

        .Controls(ControlIndex).TextColour = GetRarityColor(Item(Inventory.Num).Rarity)
        .Controls(ControlIndex).Width = 225
        .Controls(ControlType).Text = "Heráldica"
        .Controls(ControlType).TextColour = White
        .Controls(ControlType).Align = Alignment.AlignCenter
        .Controls(ControlType).Width = 225
    End With

    ' clear
    ReDim DescText(1 To 1) As TextColourRec

    If Item(Inventory.Num).Level > 0 Then
        LevelText = "Level Requerido " & Item(Inventory.Num).Level

        If GetPlayerLevel(MyIndex) >= Item(Inventory.Num).Level Then
            Colour = Green
        Else
            Colour = BrightRed
        End If
    Else
        LevelText = "Sem Requiremento de level"
        Colour = Green
    End If

    AddDescInfo LevelText, Colour

    AddDescInfo ""

    If Item(Inventory.Num).ClassId > 0 Then
        If Item(Inventory.Num).ClassId = Player(MyIndex).Class Then
            Colour = Green
        Else
            Colour = BrightRed
        End If

        AddDescInfo "Equipado com " & GetClassName(Item(Inventory.Num).ClassId) & " (s)", Colour
        AddDescInfo ""
    End If

    If Inventory.Bound > 0 Then
        AddDescInfo "Ligado ao personagem", Coral
        AddDescInfo ""
    End If

    Call AddHeraldryDescValues(Inventory.AttributeId, Inventory.UpgradeId, Inventory.Level)

    ControlIndex = GetControlIndex("winDescription", "lblDesc")
    Windows(WindowIndex).Controls(ControlIndex).Width = 225
    Windows(WindowIndex).Window.Height = 150 + (UBound(DescText) * 12)

    Windows(WindowIndex).Controls(ControlIndex).Visible = True
    Windows(WindowIndex).Controls(ControlIndex).Top = 50 + (UBound(DescText) * 12)

    If Inventory.AttributeId > 0 Then
        Windows(WindowIndex).Controls(ControlIndex).Text = Item(Inventory.Num).Description
    Else
        Windows(WindowIndex).Controls(ControlIndex).Text = "Os detalhes deste item precisam ser revelados."
    End If

    ControlIndex = GetControlIndex("winDescription", "lblPrice")
    Windows(WindowIndex).Controls(ControlIndex).Visible = True
    Windows(WindowIndex).Controls(ControlIndex).Top = Windows(WindowIndex).Window.Height - 28

    If Price > 0 Then
        Windows(WindowIndex).Controls(ControlIndex).Text = "Valor: " & Price & " ouro"
    ElseIf Price = 0 Then
        Windows(WindowIndex).Controls(ControlIndex).Text = "Não pode ser vendido"
    ElseIf Price < 0 Then
        Windows(WindowIndex).Controls(ControlIndex).Text = ""
    End If

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
    End If
End Sub

Private Sub AddHeraldryDescValues(ByVal AttributeId As Long, ByVal UpgradeId As Long, ByVal Level As Long)
    If AttributeId < 1 Or AttributeId > MaxHeraldryAttributes Then
        Exit Sub
    End If

    Call AddText(HeraldryAttributes(AttributeId).Stat(Stats.Strength), Level, HeraldryEnhancements(UpgradeId).Stat(Stats.Strength), "Força")
    Call AddText(HeraldryAttributes(AttributeId).Stat(Stats.Agility), Level, HeraldryEnhancements(UpgradeId).Stat(Stats.Agility), "Agilidade")
    Call AddText(HeraldryAttributes(AttributeId).Stat(Stats.Constitution), Level, HeraldryEnhancements(UpgradeId).Stat(Stats.Constitution), "Constituição")
    Call AddText(HeraldryAttributes(AttributeId).Stat(Stats.Intelligence), Level, HeraldryEnhancements(UpgradeId).Stat(Stats.Intelligence), "Inteligência")
    Call AddText(HeraldryAttributes(AttributeId).Stat(Stats.Spirit), Level, HeraldryEnhancements(UpgradeId).Stat(Stats.Spirit), "Espírito")
        Call AddText(HeraldryAttributes(AttributeId).Stat(Stats.Will), Level, HeraldryEnhancements(UpgradeId).Stat(Stats.Will), "Vontade")
    Call AddText(HeraldryAttributes(AttributeId).Vital(HP), Level, HeraldryEnhancements(UpgradeId).Vital(HP), "HP")
    Call AddText(HeraldryAttributes(AttributeId).Vital(HP), Level, HeraldryEnhancements(UpgradeId).Vital(MP), "MP")
    Call AddText(HeraldryAttributes(AttributeId).Attack, Level, HeraldryEnhancements(UpgradeId).Attack, "Ataque")
    Call AddText(HeraldryAttributes(AttributeId).Defense, Level, HeraldryEnhancements(UpgradeId).Defense, "Defesa")
    Call AddText(HeraldryAttributes(AttributeId).Accuracy, Level, HeraldryEnhancements(UpgradeId).Accuracy, "Precisão")
    Call AddText(HeraldryAttributes(AttributeId).Evasion, Level, HeraldryEnhancements(UpgradeId).Evasion, "Evasão")
    Call AddText(HeraldryAttributes(AttributeId).Parry, Level, HeraldryEnhancements(UpgradeId).Parry, "Aparo")
    Call AddText(HeraldryAttributes(AttributeId).Block, Level, HeraldryEnhancements(UpgradeId).Block, "Bloqueio")
    Call AddText(HeraldryAttributes(AttributeId).MagicAttack, Level, HeraldryEnhancements(UpgradeId).MagicAttack, "Ataque Mágico")
    Call AddText(HeraldryAttributes(AttributeId).MagicDefense, Level, HeraldryEnhancements(UpgradeId).MagicDefense, "Defesa Mágica")
    Call AddText(HeraldryAttributes(AttributeId).MagicAccuracy, Level, HeraldryEnhancements(UpgradeId).MagicAccuracy, "Precisão Mágica")
    Call AddText(HeraldryAttributes(AttributeId).MagicResist, Level, HeraldryEnhancements(UpgradeId).MagicResist, "Resistência Mágica")
    Call AddText(HeraldryAttributes(AttributeId).Concentration, Level, HeraldryEnhancements(UpgradeId).Concentration, "Concentração")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).CritRate + (Level * HeraldryEnhancements(UpgradeId).CritRate), "Taxa Crítica")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).CritDamage + (Level * HeraldryEnhancements(UpgradeId).CritDamage), "Dano Crítico")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).ResistCritRate + (Level * HeraldryEnhancements(UpgradeId).ResistCritRate), "Res. Taxa Crítica")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).ResistCritDamage + (Level * HeraldryEnhancements(UpgradeId).ResistCritDamage), "Res. Dano Crítico")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).HealingPower + (Level * HeraldryEnhancements(UpgradeId).HealingPower), "Poder de Cura")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).Amplification + (Level * HeraldryEnhancements(UpgradeId).Amplification), "Amplificação")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).FinalDamage + (Level * HeraldryEnhancements(UpgradeId).FinalDamage), "Dano Final")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).Enmity + (Level * HeraldryEnhancements(UpgradeId).Enmity), "Inimizade")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).AttackSpeed + (Level * HeraldryEnhancements(UpgradeId).AttackSpeed), "Vel. Ataque")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).CastSpeed + (Level * HeraldryEnhancements(UpgradeId).CastSpeed), "Vel. Conjuração")
    Call AddText(HeraldryAttributes(AttributeId).SilenceResistance, Level, HeraldryEnhancements(UpgradeId).SilenceResistance, "Res. Silêncio")
    Call AddText(HeraldryAttributes(AttributeId).BlindResistance, Level, HeraldryEnhancements(UpgradeId).BlindResistance, "Res. Cegar")
    Call AddText(HeraldryAttributes(AttributeId).StunResistance, Level, HeraldryEnhancements(UpgradeId).StunResistance, "Res. Atordoar")
    Call AddText(HeraldryAttributes(AttributeId).StumbleResistance, Level, HeraldryEnhancements(UpgradeId).Concentration, "Res. Queda")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).PveAttack + (Level * HeraldryEnhancements(UpgradeId).PveAttack), "Ataque PvE")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).PveDefense + (Level * HeraldryEnhancements(UpgradeId).PveDefense), "Defesa PvE")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).PvpAttack + (Level * HeraldryEnhancements(UpgradeId).PvpAttack), "Ataque PvP")
    Call AddTextPercentage(HeraldryAttributes(AttributeId).PvpDefense + (Level * HeraldryEnhancements(UpgradeId).PvpDefense), "Defesa PvP")
    Call AddText(HeraldryAttributes(AttributeId).ElementAttack(Elements.Element_Fire), Level, HeraldryEnhancements(UpgradeId).ElementAttack(Elements.Element_Fire), "Elemento Fogo")
    Call AddText(HeraldryAttributes(AttributeId).ElementAttack(Elements.Element_Water), Level, HeraldryEnhancements(UpgradeId).ElementAttack(Elements.Element_Water), "Elemento Água")
    Call AddText(HeraldryAttributes(AttributeId).ElementAttack(Elements.Element_Earth), Level, HeraldryEnhancements(UpgradeId).ElementAttack(Elements.Element_Earth), "Elemento Terra")
    Call AddText(HeraldryAttributes(AttributeId).ElementAttack(Elements.Element_Wind), Level, HeraldryEnhancements(UpgradeId).ElementAttack(Elements.Element_Wind), "Elemento Vento")
    Call AddText(HeraldryAttributes(AttributeId).ElementAttack(Elements.Element_Light), Level, HeraldryEnhancements(UpgradeId).ElementAttack(Elements.Element_Light), "Elemento Luz")
    Call AddText(HeraldryAttributes(AttributeId).ElementAttack(Elements.Element_Dark), Level, HeraldryEnhancements(UpgradeId).ElementAttack(Elements.Element_Dark), "Elemento Trevas")
End Sub


Attribute VB_Name = "Title_AllocatedAttributes"
Option Explicit

Public Const MaximumAllocatedDescription As Long = 100

Public TitleAllocatedDescription(1 To MaximumAllocatedDescription) As String

Private LineIndex As Long

Private RawAttributes As AttributesRec
Private PercentageAttributes As AttributesRec

Public Sub AllocateTitleAttributeDescription()
    Dim i As Long, TitleId As Long

    LineIndex = 1

    Call ZeroMemory(ByVal RawAttributes, LenB(RawAttributes))
    Call ZeroMemory(ByVal PercentageAttributes, LenB(PercentageAttributes))

    For i = 1 To MaximumAllocatedDescription
        TitleAllocatedDescription(i) = vbNullString
    Next

    For i = 1 To MaxPlayerTitles
        TitleId = GetTitle(i)

        If TitleId > 0 Then
            Call ContinueAllocateTitleAttribute(TitleId)
        End If
    Next

    Call CreateAllocatedDescription

End Sub

Private Sub ContinueAllocateTitleAttribute(ByVal TitleId As Long)
    Dim AttributeId As Long
    Dim x As AttributesRec

    AttributeId = Title(TitleId).AttributeId

    If AttributeId > 0 Then
        Call AddAttributes(RawAttributes, TitleAttributes(AttributeId), False)
        Call AddAttributes(PercentageAttributes, TitleAttributes(AttributeId), True)
    End If
End Sub

Private Sub AddAttributes(ByRef Container As AttributesRec, ByRef Attributes As AttributesRec, ByVal IsPercentage As Long)
    Dim i As Long
    
    For i = 1 To Vitals.Vital_Count - 1
        If Attributes.Vital(i).Percentage = IsPercentage Then
            Container.Vital(i).Value = Container.Vital(i).Value + Attributes.Vital(i).Value
        End If

        Container.Vital(i).Percentage = IsPercentage
    Next

    For i = 1 To Stats.Stat_Count - 1
        If Attributes.Stat(i).Percentage = IsPercentage Then
            Container.Stat(i).Value = Container.Stat(i).Value + Attributes.Stat(i).Value
        End If

        Container.Stat(i).Percentage = IsPercentage
    Next

    If Attributes.Attack.Percentage = IsPercentage Then
        Container.Attack.Value = Container.Attack.Value + Attributes.Attack.Value
    End If

    Container.Attack.Percentage = IsPercentage

    If Attributes.Defense.Percentage = IsPercentage Then
        Container.Defense.Value = Container.Defense.Value + Attributes.Defense.Value
    End If

    Container.Defense.Percentage = IsPercentage

    If Attributes.Accuracy.Percentage = IsPercentage Then
        Container.Accuracy.Value = Container.Accuracy.Value + Attributes.Accuracy.Value
    End If

    Container.Accuracy.Percentage = IsPercentage


    If Attributes.Evasion.Percentage = IsPercentage Then
        Container.Evasion.Value = Container.Evasion.Value + Attributes.Evasion.Value
    End If

    Container.Evasion.Percentage = IsPercentage

    If Attributes.Parry.Percentage = IsPercentage Then
        Container.Parry.Value = Container.Parry.Value + Attributes.Parry.Value
    End If

    Container.Parry.Percentage = IsPercentage

    If Attributes.Block.Percentage = IsPercentage Then
        Container.Block.Value = Container.Block.Value + Attributes.Block.Value
    End If

    Container.Block.Percentage = IsPercentage

    If Attributes.MagicAttack.Percentage = IsPercentage Then
        Container.MagicAttack.Value = Container.MagicAttack.Value + Attributes.MagicAttack.Value
    End If

    Container.MagicAttack.Percentage = IsPercentage

    If Attributes.MagicDefense.Percentage = IsPercentage Then
        Container.MagicDefense.Value = Container.MagicDefense.Value + Attributes.MagicDefense.Value
    End If

    Container.MagicDefense.Percentage = IsPercentage

    If Attributes.MagicAccuracy.Percentage = IsPercentage Then
        Container.MagicAccuracy.Value = Container.MagicAccuracy.Value + Attributes.MagicAccuracy.Value
    End If

    Container.MagicAccuracy.Percentage = IsPercentage

    If Attributes.MagicResist.Percentage = IsPercentage Then
        Container.MagicResist.Value = Container.MagicResist.Value + Attributes.MagicResist.Value
    End If

    Container.MagicResist.Percentage = IsPercentage

    If Attributes.Concentration.Percentage = IsPercentage Then
        Container.Concentration.Value = Container.Concentration.Value + Attributes.Concentration.Value
    End If

    Container.Concentration.Percentage = IsPercentage

    If Attributes.SilenceResistance.Percentage = IsPercentage Then
        Container.SilenceResistance.Value = Container.SilenceResistance.Value + Attributes.SilenceResistance.Value
    End If

    Container.SilenceResistance.Percentage = IsPercentage

    If Attributes.BlindResistance.Percentage = IsPercentage Then
        Container.BlindResistance.Value = Container.BlindResistance.Value + Attributes.BlindResistance.Value
    End If

    Container.BlindResistance.Percentage = IsPercentage

    If Attributes.StunResistance.Percentage = IsPercentage Then
        Container.StunResistance.Value = Container.StunResistance.Value + Attributes.StunResistance.Value
    End If

    Container.StunResistance.Percentage = IsPercentage

    If Attributes.StumbleResistance.Percentage = IsPercentage Then
        Container.StumbleResistance.Value = Container.StumbleResistance.Value + Attributes.StumbleResistance.Value
    End If

    Container.StumbleResistance.Percentage = IsPercentage

    Container.CritRate = Container.CritRate + Attributes.CritRate
    Container.CritDamage = Container.CritDamage + Attributes.CritDamage
    Container.ResistCritRate = Container.ResistCritRate + Attributes.ResistCritRate
    Container.ResistCritDamage = Container.ResistCritDamage + Attributes.ResistCritDamage

    Container.HealingPower = Container.HealingPower + Attributes.HealingPower
    Container.FinalDamage = Container.FinalDamage + Attributes.FinalDamage
    Container.Amplification = Container.Amplification + Attributes.Amplification
    Container.Enmity = Container.Enmity + Attributes.Enmity
    Container.DamageSuppression = Container.DamageSuppression + Attributes.DamageSuppression
    Container.AttackSpeed = Container.AttackSpeed + Attributes.AttackSpeed
    Container.CastSpeed = Container.CastSpeed + Attributes.CastSpeed

    Container.PveAttack = Container.PveAttack + Attributes.PveAttack
    Container.PveDefense = Container.PveDefense + Attributes.PveDefense
    Container.PvpAttack = Container.PvpAttack + Attributes.PvpAttack
    Container.PvpDefense = Container.PvpDefense + Attributes.PvpDefense

    For i = 0 To Elements.Element_Count - 1
        If Attributes.ElementAttack(i).Percentage = IsPercentage Then
            Container.ElementAttack(i).Value = Container.ElementAttack(i).Value + Attributes.ElementAttack(i).Value
        End If

        If Attributes.ElementDefense(i).Percentage = IsPercentage Then
            Container.ElementDefense(i).Value = Container.ElementDefense(i).Value + Attributes.ElementDefense(i).Value
        End If

        Container.ElementAttack(i).Percentage = IsPercentage
        Container.ElementDefense(i).Percentage = IsPercentage
    Next

End Sub

Private Sub AddText(ByRef Stat As AttributeRec, ByVal Text As String)
    If Stat.Value > 0 Then
        Dim Value As Long

        If Stat.Percentage Then
            Value = CLng(Stat.Value * 100)

            If Value > 0 Then TitleAllocatedDescription(LineIndex) = Text & ": " & Value & "%"
        Else
            Value = Stat.Value

            If Value > 0 Then TitleAllocatedDescription(LineIndex) = Text & ": " & Value
        End If

        LineIndex = LineIndex + 1
    End If
End Sub

Private Sub AddTextPercentage(ByVal StatValue As Single, ByVal Text As String)
    If StatValue > 0 Then
        Dim Value As Long
        
        Value = CLng(StatValue * 100)

        TitleAllocatedDescription(LineIndex) = Text & ": " & Value & "%"

        LineIndex = LineIndex + 1
    End If
End Sub

Private Sub CreateAllocatedDescription()
    Call AddText(RawAttributes.Stat(Stats.Strength), "Força")
    Call AddText(PercentageAttributes.Stat(Stats.Strength), "Força")

    Call AddText(RawAttributes.Stat(Stats.Agility), "Agilidade")
    Call AddText(PercentageAttributes.Stat(Stats.Agility), "Agilidade")

    Call AddText(RawAttributes.Stat(Stats.Will), "Vontade")
    Call AddText(PercentageAttributes.Stat(Stats.Will), "Vontade")

    Call AddText(RawAttributes.Stat(Stats.Constitution), "Constituição")
    Call AddText(PercentageAttributes.Stat(Stats.Constitution), "Constituição")

    Call AddText(RawAttributes.Stat(Stats.Intelligence), "Inteligência")
    Call AddText(PercentageAttributes.Stat(Stats.Intelligence), "Inteligência")

    Call AddText(RawAttributes.Stat(Stats.Spirit), "Espírito")
    Call AddText(PercentageAttributes.Stat(Stats.Spirit), "Espírito")

    Call AddText(RawAttributes.Vital(HP), "HP")
    Call AddText(PercentageAttributes.Vital(HP), "HP")

    Call AddText(RawAttributes.Vital(MP), "MP")
    Call AddText(PercentageAttributes.Vital(MP), "MP")

    Call AddText(RawAttributes.Attack, "Ataque")
    Call AddText(PercentageAttributes.Attack, "Ataque")

    Call AddText(RawAttributes.Defense, "Defesa")
    Call AddText(PercentageAttributes.Defense, "Defesa")

    Call AddText(RawAttributes.Accuracy, "Precisão")
    Call AddText(PercentageAttributes.Accuracy, "Precisão")

    Call AddText(RawAttributes.Evasion, "Evasão")
    Call AddText(PercentageAttributes.Evasion, "Evasão")

    Call AddText(RawAttributes.Parry, "Aparo")
    Call AddText(PercentageAttributes.Parry, "Aparo")

    Call AddText(RawAttributes.Block, "Bloqueio")
    Call AddText(PercentageAttributes.Block, "Bloqueio")

    Call AddText(RawAttributes.MagicAttack, "Ataque Mágico")
    Call AddText(PercentageAttributes.MagicAttack, "Ataque Mágico")

    Call AddText(RawAttributes.MagicDefense, "Defesa Mágica")
    Call AddText(PercentageAttributes.MagicDefense, "Defesa Mágica")

    Call AddText(RawAttributes.MagicAccuracy, "Precisão Mágica")
    Call AddText(PercentageAttributes.MagicAccuracy, "Precisão Mágica")

    Call AddText(RawAttributes.MagicResist, "Resistência Mágica")
    Call AddText(PercentageAttributes.MagicResist, "Resistência Mágica")

    Call AddText(RawAttributes.Concentration, "Concentração")
    Call AddText(PercentageAttributes.Concentration, "Concentração")

    Call AddTextPercentage(RawAttributes.CritRate, "Taxa Crítica")
    Call AddTextPercentage(RawAttributes.CritDamage, "Dano Crítico")
    Call AddTextPercentage(RawAttributes.ResistCritRate, "Res. Taxa Crítica")
    Call AddTextPercentage(RawAttributes.ResistCritDamage, "Res. Dano Crítico")
    Call AddTextPercentage(RawAttributes.HealingPower, "Poder de Cura")
    Call AddTextPercentage(RawAttributes.Amplification, "Amplificação")
    Call AddTextPercentage(RawAttributes.FinalDamage, "Dano Final")
    Call AddTextPercentage(RawAttributes.Enmity, "Inimizade")
    Call AddTextPercentage(RawAttributes.AttackSpeed, "Vel. Ataque")
    Call AddTextPercentage(RawAttributes.CastSpeed, "Vel. Conjuração")

    Call AddText(RawAttributes.SilenceResistance, "Res. Silêncio")
    Call AddText(PercentageAttributes.SilenceResistance, "Res. Silêncio")

    Call AddText(RawAttributes.BlindResistance, "Res. Cegar")
    Call AddText(PercentageAttributes.BlindResistance, "Res. Cegar")

    Call AddText(RawAttributes.StunResistance, "Res. Atordoar")
    Call AddText(PercentageAttributes.StunResistance, "Res. Atordoar")

    Call AddText(RawAttributes.StumbleResistance, "Res. Queda")
    Call AddText(PercentageAttributes.StumbleResistance, "Res. Queda")

    Call AddTextPercentage(RawAttributes.PveAttack, "Ataque PvE")
    Call AddTextPercentage(RawAttributes.PveDefense, "Defesa PvE")
    Call AddTextPercentage(RawAttributes.PvpAttack, "Ataque PvP")
    Call AddTextPercentage(RawAttributes.PvpDefense, "Defesa PvP")

    ' Elemental Attack
    Call AddText(RawAttributes.ElementAttack(Elements.Element_Fire), "Ataque Elemento Fogo")
    Call AddText(PercentageAttributes.ElementAttack(Elements.Element_Fire), "Ataque Elemento Fogo")

    Call AddText(RawAttributes.ElementAttack(Elements.Element_Water), "Ataque Elemento Água")
    Call AddText(PercentageAttributes.ElementAttack(Elements.Element_Water), "Ataque Elemento Água")

    Call AddText(RawAttributes.ElementAttack(Elements.Element_Earth), "Ataque Elemento Terra")
    Call AddText(PercentageAttributes.ElementAttack(Elements.Element_Earth), "Ataque Elemento Terra")

    Call AddText(RawAttributes.ElementAttack(Elements.Element_Wind), "Ataque Elemento Vento")
    Call AddText(PercentageAttributes.ElementAttack(Elements.Element_Wind), "Ataque Elemento Vento")

    Call AddText(RawAttributes.ElementAttack(Elements.Element_Light), "Ataque Elemento Luz")
    Call AddText(PercentageAttributes.ElementAttack(Elements.Element_Light), "Ataque Elemento Luz")

    Call AddText(RawAttributes.ElementAttack(Elements.Element_Dark), "Ataque Elemento Trevas")
    Call AddText(PercentageAttributes.ElementAttack(Elements.Element_Dark), "Ataque Elemento Trevas")

    ' Elemental Defense
    Call AddText(RawAttributes.ElementDefense(Elements.Element_Fire), "Defesa Elemento Fogo")
    Call AddText(PercentageAttributes.ElementDefense(Elements.Element_Fire), "Defesa Elemento Fogo")

    Call AddText(RawAttributes.ElementDefense(Elements.Element_Water), "Defesa Elemento Água")
    Call AddText(PercentageAttributes.ElementDefense(Elements.Element_Water), "Defesa Elemento Água")

    Call AddText(RawAttributes.ElementDefense(Elements.Element_Earth), "Defesa Elemento Terra")
    Call AddText(PercentageAttributes.ElementDefense(Elements.Element_Earth), "Defesa Elemento Terra")

    Call AddText(RawAttributes.ElementDefense(Elements.Element_Wind), "Defesa Elemento Vento")
    Call AddText(PercentageAttributes.ElementDefense(Elements.Element_Wind), "Defesa Elemento Vento")

    Call AddText(RawAttributes.ElementDefense(Elements.Element_Light), "Defesa Elemento Luz")
    Call AddText(PercentageAttributes.ElementDefense(Elements.Element_Light), "Defesa Elemento Luz")

    Call AddText(RawAttributes.ElementDefense(Elements.Element_Dark), "Defesa Elemento Trevas")
    Call AddText(PercentageAttributes.ElementDefense(Elements.Element_Dark), "Defesa Elemento Trevas")
End Sub

Attribute VB_Name = "Item_Description"
Option Explicit

Private Const IncreaseText As String = "Aumento"
Private Const DecreaseText As String = "Redução"
Private Color As Long
Private EnabledEffect As Boolean

Public Sub ShowItemDesc(x As Long, Y As Long, ByRef Inventory As InventoryRec)
    Dim Colour As Long, i As Long
    Dim WindowIndex As Long
    Dim CurrentHeight As Long
    Dim ControlType As Long

    ' set globals
    DescType = 1    ' inventory
    DescItem = Inventory.Num
    DescLevel = Inventory.Level
    DescBound = Inventory.Bound
    DescAttribute = Inventory.AttributeId

    WindowIndex = GetWindowIndex("winDescription")
    CurrentHeight = Windows(WindowIndex).Window.Height

    ' set position
    Windows(WindowIndex).Window.Left = x
    Windows(WindowIndex).Window.Top = Y
    Windows(WindowIndex).Window.Width = 225

    ' show the window
    ShowWindow WindowIndex, , False

    ' exit out early if last is same
    If (DescLastType = DescType) And (DescLastItem = DescItem) And (DescLastLevel = DescLevel) And (DescLastBound = DescBound) And (DescLastAttribute = DescAttribute) Then
        Exit Sub
    End If

    ' set last to this
    DescLastType = DescType
    DescLastItem = DescItem
    DescLastLevel = DescLevel
    DescLastBound = DescBound
    DescLastAttribute = DescAttribute

    ControlType = GetControlIndex("winDescription", "lblType")
    Windows(WindowIndex).Controls(ControlType).Visible = True

    ' set variables
    Dim ItemName As String
    Dim ControlIndex As Long
    Dim LevelText As String
    Dim EquipNum As Long
    Dim ClassText As String

    EquipNum = Item(Inventory.Num).EquipmentId

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

        If Item(Inventory.Num).Type = ItemType.ItemType_Equipment Then
            .Controls(ControlType).Text = GetEquipmentTypeText(Equipment(EquipNum).Type) & " " & GetProficiencyText(Equipment(EquipNum).Proficiency)
        Else
            .Controls(ControlType).Text = GetItemTypeText(Item(Inventory.Num).Type)
        End If

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

        If Item(Inventory.Num).Type = ItemType.ItemType_Equipment Then
            AddDescInfo "Equipado com " & GetClassName(Item(Inventory.Num).ClassId) & " (s)", Colour
        Else
            AddDescInfo "Somente " & GetClassName(Item(Inventory.Num).ClassId) & " (s)", Colour
        End If

        AddDescInfo ""
    End If

    If Inventory.Bound > 0 Then
        AddDescInfo "Ligado ao personagem", Coral
    Else
        If Item(Inventory.Num).Bind = ItemBoundType_Obtained Then
            AddDescInfo "Preso quando coletado", Coral
        ElseIf Item(Inventory.Num).Bind = ItemBoundType_Equiped Then
            AddDescInfo "Preso quando equipado", Coral
        End If
    End If

    AddDescInfo ""

    If Item(Inventory.Num).Type = ItemType.ItemType_Equipment Then
        Call AddEquipmentDescValues(Inventory.Num, Inventory.Level, Inventory.AttributeId)

    ElseIf Item(Inventory.Num).Type = ItemType.ItemType_Recipe Then
        Call AddRecipeDesc(Item(Inventory.Num).RecipeId)

    ElseIf Item(Inventory.Num).Type = ItemType.ItemType_Heraldry Then
        If Inventory.AttributeId = 0 Then
            AddDescInfo "Os atributos estão ocultos."
            AddDescInfo "Botão Direito: Inspecionar.", ColorType.Gold
        End If
    End If

    ControlIndex = GetControlIndex("winDescription", "lblDesc")
    Windows(WindowIndex).Controls(ControlIndex).Width = 205

    If Item(Inventory.Num).Type = ItemType.ItemType_Equipment Then
        If EquipNum > 0 Then
            If Equipment(EquipNum).Type >= Equipments.Weapon And Equipment(EquipNum).Type <= Equipments.Equipment_Count - 1 Then

                Windows(WindowIndex).Window.Height = 150 + (UBound(DescText) * 12)
                Windows(WindowIndex).Controls(ControlIndex).Visible = True
                Windows(WindowIndex).Controls(ControlIndex).Top = 50 + (UBound(DescText) * 12)
                Windows(WindowIndex).Controls(ControlIndex).Text = Item(Inventory.Num).Description

            End If
        End If
    Else
        Windows(WindowIndex).Window.Height = 190
        Windows(WindowIndex).Controls(ControlIndex).Visible = True
        Windows(WindowIndex).Controls(ControlIndex).Text = Item(Inventory.Num).Description
        Windows(WindowIndex).Controls(ControlIndex).Top = 110
    End If

    ControlIndex = GetControlIndex("winDescription", "lblPrice")
    Windows(WindowIndex).Controls(ControlIndex).Visible = True
    Windows(WindowIndex).Controls(ControlIndex).Top = Windows(WindowIndex).Window.Height - 28

    If Item(Inventory.Num).Price > 0 Then
        Windows(WindowIndex).Controls(ControlIndex).Text = "Valor: " & Item(Inventory.Num).Price & " ouro"
    Else
        Windows(WindowIndex).Controls(ControlIndex).Text = "Não pode ser vendido"
    End If

End Sub

Private Sub AddText(ByRef Stat As AttributeRec, ByVal Level As Long, ByRef Upgrade As AttributeRec, ByVal Text As String)
    If Stat.Value > 0 Or Upgrade.Value > 0 Then
        Dim Value As Long

        If Stat.Percentage Then
            Value = CLng((Stat.Value + CSng(Level * Upgrade.Value)) * 100)
            If Value > 0 Then Call AddDescInfo(Text & ": " & Value & "%", Color)
        Else
            Value = CLng(Stat.Value) + CLng(Level * Upgrade.Value)
            If Value > 0 Then Call AddDescInfo(Text & ": " & Value, Color)
        End If
    End If
End Sub

Private Sub AddTextPercentage(ByVal StatValue As Single, ByVal Text As String, Optional HasRawValue As Boolean = False)
    If StatValue > 0 Then
        Dim Value As Long
        Value = CLng(StatValue * 100)

        If HasRawValue Then
            Call AddDescInfo(Text & ": " & Value, Color)
        Else
            Call AddDescInfo(Text & ": " & Value & "%", Color)
        End If
    End If
End Sub

Private Sub AddEquipmentDescValues(ByVal ItemNum As Long, ByVal Level As Long, ByVal AttributeId As Long)
    If Item(ItemNum).EquipmentId < 1 Or Item(ItemNum).EquipmentId > MaximumEquipments Then
        Exit Sub
    End If

    Dim i As Long
    Dim EquipNum As Long
    Dim UpgradeId As Long
    Dim EquipmentSetId As Long

    EquipNum = Item(ItemNum).EquipmentId
    UpgradeId = Equipment(EquipNum).UpgradeId
    EquipmentSetId = Equipment(EquipNum).EquipmentSetId

    ' Ativa a cor para branco.
    EnabledEffect = True

    If AttributeId = 0 Then
        AddDescInfo "Os atributos estão ocultos."
        AddDescInfo "Botão Direito: Inspecionar.", ColorType.Gold
    Else
        ' Adiciona os atributos do equipamento.
        If AttributeId > 0 And AttributeId <= MaxEquipmentAttributes Then
            Call AddItemDescValues(EquipmentAttributes(AttributeId), EquipmentEnhancements(UpgradeId), Level)
        End If
    End If

    ' Adiciona o efeito de conjunto.
    If EquipmentSetId > 0 Then
        With EquipmentSet(EquipmentSetId)
            If .MaxEffects > 0 Then
                AddDescInfo ""
                AddDescInfo "Bonus de conjunto", BrightBlue

                For i = 1 To .MaxEffects
                    Call AddItemSetEffectDescValues(EquipmentSetId, i)
                Next
            End If
        End With
    End If

End Sub

Private Sub AddItemDescValues(ByRef mAttributes As AttributesRec, ByRef uAttributes As AttributesRec, ByVal Level As Long)
    ' Quando o efeito está ativo, desenha o texto em branco.
    If EnabledEffect Then
        Color = White
    Else
        Color = Grey
    End If
    
    Call AddText(mAttributes.Stat(Stats.Strength), Level, uAttributes.Stat(Stats.Strength), "Força")
    Call AddText(mAttributes.Stat(Stats.Agility), Level, uAttributes.Stat(Stats.Agility), "Agilidade")
    Call AddText(mAttributes.Stat(Stats.Constitution), Level, uAttributes.Stat(Stats.Constitution), "Constituição")
    Call AddText(mAttributes.Stat(Stats.Intelligence), Level, uAttributes.Stat(Stats.Intelligence), "Inteligência")
    Call AddText(mAttributes.Stat(Stats.Spirit), Level, uAttributes.Stat(Stats.Spirit), "Espírito")
    Call AddText(mAttributes.Stat(Stats.Will), Level, uAttributes.Stat(Stats.Will), "Vontade")
    Call AddText(mAttributes.Vital(HP), Level, uAttributes.Vital(HP), "HP")
    Call AddText(mAttributes.Vital(MP), Level, uAttributes.Vital(MP), "MP")
    Call AddText(mAttributes.Attack, Level, uAttributes.Attack, "Ataque")
    Call AddText(mAttributes.Defense, Level, uAttributes.Defense, "Defesa")
    Call AddText(mAttributes.Accuracy, Level, uAttributes.Accuracy, "Precisão")
    Call AddText(mAttributes.Evasion, Level, uAttributes.Evasion, "Evasão")
    Call AddText(mAttributes.Parry, Level, uAttributes.Parry, "Aparo")
    Call AddText(mAttributes.Block, Level, uAttributes.Block, "Bloqueio")
    Call AddText(mAttributes.MagicAttack, Level, uAttributes.MagicAttack, "Ataque Mágico")
    Call AddText(mAttributes.MagicDefense, Level, uAttributes.MagicDefense, "Defesa Mágica")
    Call AddText(mAttributes.MagicAccuracy, Level, uAttributes.MagicAccuracy, "Precisão Mágica")
    Call AddText(mAttributes.MagicResist, Level, uAttributes.MagicResist, "Resistência Mágica")
    Call AddText(mAttributes.Concentration, Level, uAttributes.Concentration, "Concentração")
    Call AddTextPercentage(mAttributes.CritRate + (Level * uAttributes.CritRate), "Taxa Crítica")
    Call AddTextPercentage(mAttributes.CritDamage + (Level * uAttributes.CritDamage), "Dano Crítico")
    Call AddTextPercentage(mAttributes.ResistCritRate + (Level * uAttributes.ResistCritRate), "Res. Taxa Crítica")
    Call AddTextPercentage(mAttributes.ResistCritDamage + (Level * uAttributes.ResistCritDamage), "Res. Dano Crítico")
    Call AddTextPercentage(mAttributes.HealingPower + (Level * uAttributes.HealingPower), "Poder de Cura")
    Call AddTextPercentage(mAttributes.Amplification + (Level * uAttributes.Amplification), "Amplificação")
    Call AddTextPercentage(mAttributes.FinalDamage + (Level * uAttributes.FinalDamage), "Dano Final")
    Call AddTextPercentage(mAttributes.Enmity + (Level * uAttributes.Enmity), "Inimizade")
    Call AddTextPercentage(mAttributes.AttackSpeed + (Level * uAttributes.AttackSpeed), "Vel. Ataque")
    Call AddTextPercentage(mAttributes.CastSpeed + (Level * uAttributes.CastSpeed), "Vel. Conjuração")
    Call AddText(mAttributes.SilenceResistance, Level, uAttributes.SilenceResistance, "Res. Silêncio")
    Call AddText(mAttributes.BlindResistance, Level, uAttributes.BlindResistance, "Res. Cegar")
    Call AddText(mAttributes.StunResistance, Level, uAttributes.StunResistance, "Res. Atordoar")
    Call AddText(mAttributes.StumbleResistance, Level, uAttributes.Concentration, "Res. Queda")
    Call AddTextPercentage(mAttributes.PveAttack + (Level * uAttributes.PveAttack), "Ataque PvE")
    Call AddTextPercentage(mAttributes.PveDefense + (Level * uAttributes.PveDefense), "Defesa PvE")
    Call AddTextPercentage(mAttributes.PvpAttack + (Level * uAttributes.PvpAttack), "Ataque PvP")
    Call AddTextPercentage(mAttributes.PvpDefense + (Level * uAttributes.PvpDefense), "Defesa PvP")
    ' Elemental Attack
    Call AddText(mAttributes.ElementAttack(Elements.Element_Fire), Level, uAttributes.ElementAttack(Elements.Element_Fire), "Ataque Elemento Fogo")
    Call AddText(mAttributes.ElementAttack(Elements.Element_Water), Level, uAttributes.ElementAttack(Elements.Element_Water), "Ataque Elemento Água")
    Call AddText(mAttributes.ElementAttack(Elements.Element_Earth), Level, uAttributes.ElementAttack(Elements.Element_Earth), "Ataque Elemento Terra")
    Call AddText(mAttributes.ElementAttack(Elements.Element_Wind), Level, uAttributes.ElementAttack(Elements.Element_Wind), "Ataque Elemento Vento")
    Call AddText(mAttributes.ElementAttack(Elements.Element_Light), Level, uAttributes.ElementAttack(Elements.Element_Light), "Ataque Elemento Luz")
    Call AddText(mAttributes.ElementAttack(Elements.Element_Dark), Level, uAttributes.ElementAttack(Elements.Element_Dark), "Ataque Elemento Trevas")
    ' Elemental Defense
    Call AddText(mAttributes.ElementDefense(Elements.Element_Fire), Level, uAttributes.ElementDefense(Elements.Element_Fire), "Defesa Elemento Fogo")
    Call AddText(mAttributes.ElementDefense(Elements.Element_Water), Level, uAttributes.ElementDefense(Elements.Element_Water), "Defesa Elemento Água")
    Call AddText(mAttributes.ElementDefense(Elements.Element_Earth), Level, uAttributes.ElementDefense(Elements.Element_Earth), "Defesa Elemento Terra")
    Call AddText(mAttributes.ElementDefense(Elements.Element_Wind), Level, uAttributes.ElementDefense(Elements.Element_Wind), "Defesa Elemento Vento")
    Call AddText(mAttributes.ElementDefense(Elements.Element_Light), Level, uAttributes.ElementDefense(Elements.Element_Light), "Defesa Elemento Luz")
    Call AddText(mAttributes.ElementDefense(Elements.Element_Dark), Level, uAttributes.ElementDefense(Elements.Element_Dark), "Defesa Elemento Trevas")
End Sub

Private Sub AddItemSetEffectDescValues(ByVal EquipmentSetId As Long, ByVal Index As Long)
    If EquipmentSetId < 1 Or EquipmentSetId > MaxEquipmentSets Then
        Exit Sub
    End If

    Dim AttributeId As Long
    Dim SkillId As Long

    AttributeId = EquipmentSet(EquipmentSetId).Effect(Index).AttributeId
    SkillId = EquipmentSet(EquipmentSetId).Effect(Index).SkillId

    If EquipmentSet(EquipmentSetId).Effect(Index).PieceCount > 0 Then
        Dim Value As Long
        
        Value = GetPlayerItemSetCount(EquipmentSetId)
        
        If GetPlayerItemSetCount(EquipmentSetId) >= EquipmentSet(EquipmentSetId).Effect(Index).PieceCount Then
            EnabledEffect = True
        Else
            EnabledEffect = False
        End If

        AddDescInfo GetItemSetCountText(EquipmentSet(EquipmentSetId).Effect(Index).PieceCount), BrightBlue
        
        If AttributeId > 0 And AttributeId <= MaxEquipmentSetAttributes Then
            Call AddItemDescValues(EquipmentSetAttributes(AttributeId), EquipmentSetAttributes(AttributeId), 0)
        End If
 
    End If
End Sub

Public Function GetEquipmentTypeText(ByVal EquipmentType As Byte) As String
    Select Case EquipmentType
    Case Equipments.Weapon
        GetEquipmentTypeText = "Arma"
    Case Equipments.Armor
        GetEquipmentTypeText = "Armadura"
    Case Equipments.Helmet
        GetEquipmentTypeText = "Elmo"
    Case Equipments.Shield
        GetEquipmentTypeText = "Escudo"
    Case Equipments.Shoulder
        GetEquipmentTypeText = "Ombreira"
    Case Equipments.Belt
        GetEquipmentTypeText = "Cinto"
    Case Equipments.Gloves
        GetEquipmentTypeText = "Luvas"
    Case Equipments.Pants
        GetEquipmentTypeText = "Calças"
    Case Equipments.Boot
        GetEquipmentTypeText = "Botas"
    Case Equipments.Ring
        GetEquipmentTypeText = "Anel"
    Case Equipments.Necklace
        GetEquipmentTypeText = "Colar"
    Case Equipments.Earring
        GetEquipmentTypeText = "Brinco"
    Case Equipments.Bracelet
        GetEquipmentTypeText = "Bracelete"
    Case Equipments.Costume
        GetEquipmentTypeText = "Costume"
    End Select
End Function

Public Function GetItemTypeText(ByVal IType As Byte) As String
    Select Case IType
    Case ItemType.ItemType_None
        GetItemTypeText = "Sem tipo definido"
    Case ItemType.ItemType_Equipment
        GetItemTypeText = "Equipamento"
    Case ItemType.ItemType_Key
        GetItemTypeText = "Chave"
    Case ItemType.ItemType_Skill
        GetItemTypeText = "Habilidade"
    Case ItemType.ItemType_Food
        GetItemTypeText = "Comida"
    Case ItemType.ItemType_Consume
        GetItemTypeText = "Consumo"
    Case ItemType.ItemType_Upgrade
        GetItemTypeText = "Aprimoramento"
    Case ItemType.ItemType_Supplement
        GetItemTypeText = "Suplemento"
    Case ItemType.ItemType_Recipe
        GetItemTypeText = "Receita"
    Case ItemType.ItemType_GashaBox
        GetItemTypeText = "Caixa"
    Case ItemType.ItemType_Quest
        GetItemTypeText = "Missão"
    Case ItemType.ItemType_Heraldry
        GetItemTypeText = "Brasão"
    Case ItemType.ItemType_Talisman
        GetItemTypeText = "Talismã"
    Case ItemType.ItemType_Material
        GetItemTypeText = "Material"
    End Select
End Function

Public Function GetProficiencyText(ByVal Proficiency As Byte) As String
    Select Case Proficiency
    Case Proficiencies.None
        GetProficiencyText = vbNullString
    Case Proficiencies.Cloth
        GetProficiencyText = "Tecido"
    Case Proficiencies.Leather
        GetProficiencyText = "Couro"
    Case Proficiencies.Chain
        GetProficiencyText = "Malha"
    Case Proficiencies.Plate
        GetProficiencyText = "Armadura"
    Case Proficiencies.Sword
        GetProficiencyText = "Espada"
    Case Proficiencies.Dagger
        GetProficiencyText = "Adaga"
    Case Proficiencies.Mace
        GetProficiencyText = "Maça"
    Case Proficiencies.Bow
        GetProficiencyText = "Arco"
    Case Proficiencies.Axe
        GetProficiencyText = "Machado"
    Case Proficiencies.Polearm
        GetProficiencyText = "Polearm"
    Case Proficiencies.Greatsword
        GetProficiencyText = "Grande Espada"
    Case Proficiencies.Staff
        GetProficiencyText = "Cajado"
    Case Proficiencies.Spellbook
        GetProficiencyText = "Livro de Feitiços"
    Case Proficiencies.Orb
        GetProficiencyText = "Esfera de Feitiços"
    Case Proficiencies.Shield
        GetProficiencyText = "Escudo"
    End Select
End Function

Public Function GetItemSetCountText(ByVal Count As EquipmentSetCount) As String
    Select Case Count
    Case EquipmentSetCount.EquipmentSetCount_None
        GetItemSetCountText = vbNullString
    Case EquipmentSetCount.EquipmentSetCount_One
        GetItemSetCountText = "1 Parte"
    Case EquipmentSetCount.EquipmentSetCount_Two
        GetItemSetCountText = "2 Partes"
    Case EquipmentSetCount.EquipmentSetCount_Three
        GetItemSetCountText = "3 Partes"
    Case EquipmentSetCount.EquipmentSetCount_Four
        GetItemSetCountText = "4 Partes"
    Case EquipmentSetCount.EquipmentSetCount_Five
        GetItemSetCountText = "5 Partes"
    Case EquipmentSetCount.EquipmentSetCount_Six
        GetItemSetCountText = "6 Partes"
    Case EquipmentSetCount.EquipmentSetCount_Seven
        GetItemSetCountText = "7 Partes"
    Case EquipmentSetCount.EquipmentSetCount_Eight
        GetItemSetCountText = "8 Partes"
    Case EquipmentSetCount.EquipmentSetCount_Nine
        GetItemSetCountText = "9 Partes"
    Case EquipmentSetCount.EquipmentSetCount_Ten
        GetItemSetCountText = "10 Partes"
    Case EquipmentSetCount.EquipmentSetCount_Eleven
        GetItemSetCountText = "11 Partes"
    Case EquipmentSetCount.EquipmentSetCount_Twelven
        GetItemSetCountText = "12 Partes"
    Case EquipmentSetCount.EquipmentSetCount_Thirteen
        GetItemSetCountText = "13 Partes"
    Case EquipmentSetCount.EquipmentSetCount_Fourteen
        GetItemSetCountText = "14 Partes"
    Case EquipmentSetCount.EquipmentSetCount_Fifteen
        GetItemSetCountText = "15 Partes"
    End Select
End Function

Private Function GetPlayerItemSetCount(ByVal EquipmentSetId As Long) As Integer
    Dim i As Long, ItemNum As Long, EquipNum As Long, Count As Integer

    For i = 1 To PlayerEquipments.PlayerEquipment_Count - 1
        ItemNum = GetPlayerEquipmentId(i)

        If ItemNum > 0 Then
            EquipNum = Item(ItemNum).EquipmentId

            If EquipNum > 0 Then
                If Equipment(EquipNum).EquipmentSetId = EquipmentSetId Then

                    If i = PlayerEquipments.EquipShield Then
                        ' Do not count if we are using some two handed weapon
                        If ItemNum <> GetPlayerEquipmentId(EquipWeapon) Then
                            GetPlayerItemSetCount = GetPlayerItemSetCount + 1
                        End If
                    Else
                        GetPlayerItemSetCount = GetPlayerItemSetCount + 1
                    End If

                End If
            End If

        End If
    Next

End Function

Private Sub AddRecipeDesc(ByVal RecipeId As Long)
    If RecipeId > 0 Then

    End If
End Sub


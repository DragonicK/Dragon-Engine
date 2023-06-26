Attribute VB_Name = "Character_Window"
Option Explicit

Private Const DefaultPage As Long = 1
Private Const SpecialPage As Long = 2
Private Const ElementalPage As Long = 3

Private EquipmentPosition(1 To PlayerEquipments.PlayerEquipment_Count - 1) As EquipmentPositionRec

Private WindowIndex As Long

Private IsShowingAttributes As Boolean

Private AttributePage As Long

Public Sub CreateWindow_Character()
    Dim i As Long
    ' Create window
    CreateWindow "winCharacter", "PERSONAGEM", zOrder_Win, 0, 0, 260, 435, 0, False, Fonts.FontRegular, , 2, 6, DesignTypes.DesignWindowWithTopBarAndDoubleNavBar, DesignTypes.DesignWindowWithTopBarAndDoubleNavBar, DesignTypes.DesignWindowWithTopBarAndDoubleNavBar, , , , , GetAddress(AddressOf Character_MouseMove), GetAddress(AddressOf Character_MouseDown), GetAddress(AddressOf Character_MouseMove), GetAddress(AddressOf Character_DoubleClick), , , GetAddress(AddressOf RenderCharacter)
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonMenu_Char)

    CreateButton WindowCount, "btnCharacter", 25, 42, 100, 26, "PERSONAGEM", FontRegular, Green, , , , , , , , , , , , GetAddress(AddressOf ShowCharacter_Click)
    CreateButton WindowCount, "btnAttributes", 135, 42, 100, 26, "ATRIBUTOS", FontRegular, , , , , , , , , , , , , GetAddress(AddressOf ShowAttributes_Click)

    CreateButton WindowCount, "btnDefaultAttribute", 0, 70, 80, 26, "BÁSICO", FontRegular, Green, , False, , , , , , , , , , GetAddress(AddressOf ButtonDefaultPage_Click)
    CreateButton WindowCount, "btnSpecialAttribute", 85, 70, 80, 26, "ESPECIAL", FontRegular, White, , False, , , , , , , , , , GetAddress(AddressOf ButtonSpecialPage_Click)
    CreateButton WindowCount, "btnElementalAttribute", 170, 70, 80, 26, "ELEMENTAL", FontRegular, White, , False, , , , , , , , , , GetAddress(AddressOf ButtonElementalPage_Click)

    ' Labels
    CreateLabel WindowCount, "lblName", 50, 115, 156, 16, "NOME LV. 50", FontRegular, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblClass", 50, 170, 156, 16, "PRIEST", FontRegular, White, Alignment.AlignCenter

    ' Attributes
    CreateLabel WindowCount, "lblLabel_1", 60, 252, 138, , "Força:", FontRegular, White, Alignment.AlignLeft
    CreateLabel WindowCount, "lblLabel_2", 60, 272, 138, , "Agilidade:", FontRegular, White, Alignment.AlignLeft
    CreateLabel WindowCount, "lblLabel_3", 60, 292, 138, , "Constituição:", FontRegular, White, Alignment.AlignLeft
    CreateLabel WindowCount, "lblLabel_4", 60, 312, 138, , "Inteligência:", FontRegular, White, Alignment.AlignLeft
    CreateLabel WindowCount, "lblLabel_5", 60, 332, 138, , "Espírito:", FontRegular, White, Alignment.AlignLeft
    CreateLabel WindowCount, "lblLabel_6", 60, 352, 138, , "Vontade:", FontRegular, White, Alignment.AlignLeft
    CreateLabel WindowCount, "lblPoints", 60, 382, 138, , "DISPONÍVEL: 100", FontRegular, Gold, Alignment.AlignLeft

    CreateLabel WindowCount, "lblStat_1", 148, 252, 138, , "0", FontRegular, White, Alignment.AlignLeft
    CreateLabel WindowCount, "lblStat_2", 148, 272, 138, , "0", FontRegular, White, Alignment.AlignLeft
    CreateLabel WindowCount, "lblStat_3", 148, 292, 138, , "0", FontRegular, White, Alignment.AlignLeft
    CreateLabel WindowCount, "lblStat_4", 148, 312, 138, , "0", FontRegular, White, Alignment.AlignLeft
    CreateLabel WindowCount, "lblStat_5", 148, 332, 138, , "0", FontRegular, White, Alignment.AlignLeft
    CreateLabel WindowCount, "lblStat_6", 148, 352, 138, , "0", FontRegular, White, Alignment.AlignLeft

    ' Buttons
    CreateButton WindowCount, "btnStat_1", 180, 252, 16, 16, , , , , , , Tex_GUI(33), Tex_GUI(34), Tex_GUI(35), , , , , , GetAddress(AddressOf Character_SpendPoint1)
    CreateButton WindowCount, "btnStat_2", 180, 272, 16, 16, , , , , , , Tex_GUI(33), Tex_GUI(34), Tex_GUI(35), , , , , , GetAddress(AddressOf Character_SpendPoint2)
    CreateButton WindowCount, "btnStat_3", 180, 292, 16, 16, , , , , , , Tex_GUI(33), Tex_GUI(34), Tex_GUI(35), , , , , , GetAddress(AddressOf Character_SpendPoint3)
    CreateButton WindowCount, "btnStat_4", 180, 312, 16, 16, , , , , , , Tex_GUI(33), Tex_GUI(34), Tex_GUI(35), , , , , , GetAddress(AddressOf Character_SpendPoint4)
    CreateButton WindowCount, "btnStat_5", 180, 332, 16, 16, , , , , , , Tex_GUI(33), Tex_GUI(34), Tex_GUI(35), , , , , , GetAddress(AddressOf Character_SpendPoint5)
    CreateButton WindowCount, "btnStat_6", 180, 352, 16, 16, , , , , , , Tex_GUI(33), Tex_GUI(34), Tex_GUI(35), , , , , , GetAddress(AddressOf Character_SpendPoint6)

    ' fake buttons
    CreatePictureBox WindowCount, "btnGreyStat_1", 180, 252, 15, 15, False, , , , Tex_GUI(30), Tex_GUI(31), Tex_GUI(32)
    CreatePictureBox WindowCount, "btnGreyStat_2", 180, 272, 15, 15, False, , , , Tex_GUI(30), Tex_GUI(31), Tex_GUI(32)
    CreatePictureBox WindowCount, "btnGreyStat_3", 180, 292, 15, 15, False, , , , Tex_GUI(30), Tex_GUI(31), Tex_GUI(32)
    CreatePictureBox WindowCount, "btnGreyStat_4", 180, 312, 15, 15, False, , , , Tex_GUI(30), Tex_GUI(31), Tex_GUI(32)
    CreatePictureBox WindowCount, "btnGreyStat_5", 180, 332, 15, 15, False, , , , Tex_GUI(30), Tex_GUI(31), Tex_GUI(32)
    CreatePictureBox WindowCount, "btnGreyStat_6", 180, 352, 15, 15, False, , , , Tex_GUI(30), Tex_GUI(31), Tex_GUI(32)

    CreateCheckbox WindowCount, "chkView", 30, 76, 32, 32, False, "VISUALIZAR EQUIPAMENTO", FontRegular, ColorType.Gold, , , , DesignTypes.DesignCheckBox, , , GetAddress(AddressOf Button_ViewVisibility)

    AttributePage = DefaultPage

    WindowIndex = WindowCount
    SetAllEquipmentPosition
End Sub

Private Sub Button_ViewVisibility()

    With Windows(WindowIndex).Controls(GetControlIndex("winCharacter", "chkView"))
        If .Value = 0 Then    ' set as false
            Call SendViewEquipmentVisibility(False)
        Else
            Call SendViewEquipmentVisibility(True)
        End If
    End With
    
End Sub

Private Sub ButtonDefaultPage_Click()
    Dim DefaultIndex As Long
    Dim SpecialIndex As Long
    Dim ElementalIndex As Long

    DefaultIndex = GetControlIndex("winCharacter", "btnDefaultAttribute")
    SpecialIndex = GetControlIndex("winCharacter", "btnSpecialAttribute")
    ElementalIndex = GetControlIndex("winCharacter", "btnElementalAttribute")

    Windows(WindowIndex).Controls(DefaultIndex).TextColour = Green
    Windows(WindowIndex).Controls(DefaultIndex).TextColourClick = Green
    Windows(WindowIndex).Controls(DefaultIndex).TextColourHover = Green

    Windows(WindowIndex).Controls(SpecialIndex).TextColour = White
    Windows(WindowIndex).Controls(SpecialIndex).TextColourClick = White
    Windows(WindowIndex).Controls(SpecialIndex).TextColourHover = White

    Windows(WindowIndex).Controls(ElementalIndex).TextColour = White
    Windows(WindowIndex).Controls(ElementalIndex).TextColourClick = White
    Windows(WindowIndex).Controls(ElementalIndex).TextColourHover = White

    AttributePage = DefaultPage
End Sub

Private Sub ButtonSpecialPage_Click()
    Dim DefaultIndex As Long
    Dim SpecialIndex As Long
    Dim ElementalIndex As Long

    DefaultIndex = GetControlIndex("winCharacter", "btnDefaultAttribute")
    SpecialIndex = GetControlIndex("winCharacter", "btnSpecialAttribute")
    ElementalIndex = GetControlIndex("winCharacter", "btnElementalAttribute")

    Windows(WindowIndex).Controls(DefaultIndex).TextColour = White
    Windows(WindowIndex).Controls(DefaultIndex).TextColourClick = White
    Windows(WindowIndex).Controls(DefaultIndex).TextColourHover = White

    Windows(WindowIndex).Controls(SpecialIndex).TextColour = Green
    Windows(WindowIndex).Controls(SpecialIndex).TextColourClick = Green
    Windows(WindowIndex).Controls(SpecialIndex).TextColourHover = Green

    Windows(WindowIndex).Controls(ElementalIndex).TextColour = White
    Windows(WindowIndex).Controls(ElementalIndex).TextColourClick = White
    Windows(WindowIndex).Controls(ElementalIndex).TextColourHover = White

    AttributePage = SpecialPage
End Sub

Private Sub ButtonElementalPage_Click()
    Dim DefaultIndex As Long
    Dim SpecialIndex As Long
    Dim ElementalIndex As Long

    DefaultIndex = GetControlIndex("winCharacter", "btnDefaultAttribute")
    SpecialIndex = GetControlIndex("winCharacter", "btnSpecialAttribute")
    ElementalIndex = GetControlIndex("winCharacter", "btnElementalAttribute")

    Windows(WindowIndex).Controls(DefaultIndex).TextColour = White
    Windows(WindowIndex).Controls(DefaultIndex).TextColourClick = White
    Windows(WindowIndex).Controls(DefaultIndex).TextColourHover = White

    Windows(WindowIndex).Controls(SpecialIndex).TextColour = White
    Windows(WindowIndex).Controls(SpecialIndex).TextColourClick = White
    Windows(WindowIndex).Controls(SpecialIndex).TextColourHover = White

    Windows(WindowIndex).Controls(ElementalIndex).TextColour = Green
    Windows(WindowIndex).Controls(ElementalIndex).TextColourClick = Green
    Windows(WindowIndex).Controls(ElementalIndex).TextColourHover = Green
    
    AttributePage = ElementalPage
End Sub

Private Sub ShowCharacter_Click()
    Dim CharacterIndex As Long
    Dim AttributesIndex As Long
    Dim CheckView As Long
    Dim i As Long

    CheckView = GetControlIndex("winCharacter", "chkView")
    CharacterIndex = GetControlIndex("winCharacter", "btnCharacter")
    AttributesIndex = GetControlIndex("winCharacter", "btnAttributes")

    With Windows(WindowIndex)
        .Controls(CheckView).Visible = True

        .Controls(CharacterIndex).TextColour = Green
        .Controls(CharacterIndex).TextColourClick = Green
        .Controls(CharacterIndex).TextColourHover = Green

        .Controls(AttributesIndex).TextColour = White
        .Controls(AttributesIndex).TextColourClick = White
        .Controls(AttributesIndex).TextColourHover = White
    End With

    Call SetChildWindowVisible(True)

    IsShowingAttributes = False

    With Windows(WindowIndex)
        For i = 1 To Stats.Stat_Count - 1
            ' grey out buttons
            If GetPlayerPoints() = 0 Then
                .Controls(GetControlIndex("winCharacter", "btnGreyStat_" & i)).Visible = True
            Else
                .Controls(GetControlIndex("winCharacter", "btnGreyStat_" & i)).Visible = False
            End If
        Next
    End With
End Sub

Private Sub ShowAttributes_Click()
    Dim CharacterIndex As Long
    Dim AttributesIndex As Long
    Dim CheckView As Long

    CheckView = GetControlIndex("winCharacter", "chkView")
    CharacterIndex = GetControlIndex("winCharacter", "btnCharacter")
    AttributesIndex = GetControlIndex("winCharacter", "btnAttributes")
    
    Windows(WindowIndex).Controls(CheckView).Visible = False

    Windows(WindowIndex).Controls(CharacterIndex).TextColour = White
    Windows(WindowIndex).Controls(CharacterIndex).TextColourClick = White
    Windows(WindowIndex).Controls(CharacterIndex).TextColourHover = White

    Windows(WindowIndex).Controls(AttributesIndex).TextColour = Green
    Windows(WindowIndex).Controls(AttributesIndex).TextColourClick = Green
    Windows(WindowIndex).Controls(AttributesIndex).TextColourHover = Green

    Call SetChildWindowVisible(False)
    
    IsShowingAttributes = True
End Sub

Private Sub SetAllEquipmentPosition()
    Dim BaseX As Long, BaseY As Long

    BaseX = 17
    BaseY = 105

    Call SetEquipmentPosition(EquipWeapon, BaseX + 75, BaseY + 100)
    Call SetEquipmentPosition(EquipShield, BaseX + 115, BaseY + 100)

    Call SetEquipmentPosition(EquipHelmet, BaseX, BaseY)
    Call SetEquipmentPosition(EquipNecklace, BaseX, BaseY + 40)
    Call SetEquipmentPosition(EquipShoulder, BaseX, BaseY + 80)
    Call SetEquipmentPosition(EquipArmor, BaseX, BaseY + 120)
    Call SetEquipmentPosition(EquipGloves, BaseX, BaseY + 160)
    Call SetEquipmentPosition(EquipBelt, BaseX, BaseY + 200)
    Call SetEquipmentPosition(EquipPants, BaseX, BaseY + 240)
    Call SetEquipmentPosition(EquipBoot, BaseX, BaseY + 280)

    Call SetEquipmentPosition(EquipBracelet_1, BaseX + 190, BaseY)
    Call SetEquipmentPosition(EquipBracelet_2, BaseX + 190, BaseY + 40)
    Call SetEquipmentPosition(EquipEarring_1, BaseX + 190, BaseY + 80)
    Call SetEquipmentPosition(EquipEarring_2, BaseX + 190, BaseY + 120)
    Call SetEquipmentPosition(EquipRing_1, BaseX + 190, BaseY + 160)
    Call SetEquipmentPosition(EquipRing_2, BaseX + 190, BaseY + 200)
    Call SetEquipmentPosition(EquipCostume, BaseX + 190, BaseY + 240)
    Call SetEquipmentPosition(EquipMount, BaseX + 190, BaseY + 280)

End Sub

Private Sub SetEquipmentPosition(ByVal Equip As PlayerEquipments, ByVal x As Long, ByVal Y As Long)
    EquipmentPosition(Equip).x = x
    EquipmentPosition(Equip).Y = Y
End Sub

Public Sub ButtonMenu_Char()
    If GetPlayerDead(MyIndex) Then Exit Sub

    If Windows(WindowIndex).Window.Visible Then
        HideWindow WindowIndex
        HideWindow GetWindowIndex("winStatus")
    Else
        ShowWindow WindowIndex, , False
    End If
End Sub

Private Sub Character_DoubleClick()
    If (mouseClick(VK_LBUTTON) And lastMouseClick(VK_LBUTTON) = 0) Then
        Dim EquipSlot As PlayerEquipments

        EquipSlot = GetEquipmentSlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

        If EquipSlot >= PlayerEquipments.EquipWeapon Then
            If EquipSlot = PlayerEquipments.EquipShield Then
                If IsTwoHandedStyleEquipped Then
                    Call SendUnequip(PlayerEquipments.EquipWeapon)
                Else
                    Call SendUnequip(EquipSlot)
                End If
            Else
                Call SendUnequip(EquipSlot)
            End If
        End If
    End If

    ' show desc. if needed
    Character_MouseMove
End Sub

Private Sub Character_MouseDown()
    If (mouseClick(VK_RBUTTON) And lastMouseClick(VK_RBUTTON) = 0) Then
        Dim EquipSlot As PlayerEquipments

        EquipSlot = GetEquipmentSlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

        If EquipSlot >= PlayerEquipments.EquipWeapon Then
            If EquipSlot = PlayerEquipments.EquipShield Then
                If IsTwoHandedStyleEquipped Then
                    Call SendUnequip(PlayerEquipments.EquipWeapon)
                Else
                    Call SendUnequip(EquipSlot)
                End If
            Else
                Call SendUnequip(EquipSlot)
            End If
        End If
    End If

    ' show desc. if needed
    Character_MouseMove
End Sub

Private Sub Character_MouseMove()
    Dim EquipSlot As PlayerEquipments, x As Long, Y As Long

    ' exit out early if dragging
    If DragBox.Type <> PartNone Then Exit Sub

    EquipSlot = GetEquipmentSlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

    If EquipSlot >= PlayerEquipments.EquipWeapon Then
        ' calc position
        x = Windows(WindowIndex).Window.Left - Windows(GetWindowIndex("winDescription")).Window.Width - 2
        Y = Windows(WindowIndex).Window.Top

        ' offscreen?
        If x < 0 Then
            ' switch to right
            x = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width + 2
        End If
        ' go go go
        ShowEqDesc x, Y, EquipSlot
    End If
End Sub

Private Sub Character_SpendPoint1()
    SendUseAttributePoint 0
End Sub

Private Sub Character_SpendPoint2()
    SendUseAttributePoint 1
End Sub

Private Sub Character_SpendPoint3()
    SendUseAttributePoint 2
End Sub

Private Sub Character_SpendPoint4()
    SendUseAttributePoint 3
End Sub

Private Sub Character_SpendPoint5()
    SendUseAttributePoint 4
End Sub

Private Sub Character_SpendPoint6()
    SendUseAttributePoint 5
End Sub

Private Sub RenderCharacter()
    Dim xO As Long, yO As Long, Width As Long
    Dim i As Long, ItemNum As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width

    If Not IsShowingAttributes Then
        For i = 1 To PlayerEquipments.PlayerEquipment_Count - 1
            RenderTexture Tex_GUI(54 + i), xO + EquipmentPosition(i).x, yO + EquipmentPosition(i).Y, 0, 0, 34, 34, 34, 34

            ItemNum = GetPlayerEquipmentId(i)

            If ItemNum > 0 And ItemNum <= MaximumItems Then
                RenderTexture Tex_Item(Item(ItemNum).IconId), xO + EquipmentPosition(i).x - 1, yO + EquipmentPosition(i).Y - 1, 0, 0, PIC_X, PIC_Y, PIC_X, PIC_Y
            End If
        Next
    Else
        xO = xO + 30
        yO = yO + 120
        
        If AttributePage = DefaultPage Then
            Call RenderDefaultAttributes(xO, yO)
            
        ElseIf AttributePage = SpecialPage Then
            Call RenderSpecialAttributes(xO, yO)
            
        ElseIf AttributePage = ElementalPage Then
            Call RenderElementalAttributes(xO, yO)
            
        End If
    End If
End Sub

Private Function GetEquipmentSlotFromPosition(StartX As Long, StartY As Long) As PlayerEquipments
    Dim TempRec As RECT
    Dim i As Long

    For i = 1 To PlayerEquipments.PlayerEquipment_Count - 1
        If GetPlayerEquipmentId(i) > 0 Then
            With TempRec
                .Top = StartY + EquipmentPosition(i).Y
                .Bottom = .Top + PIC_Y
                .Left = StartX + EquipmentPosition(i).x
                .Right = .Left + PIC_X
            End With

            If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
                If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
                    GetEquipmentSlotFromPosition = i
                    Exit Function
                End If
            End If
        End If
    Next
End Function

Private Sub SetChildWindowVisible(ByVal Visible As Boolean)
    Dim i As Long

    Windows(WindowIndex).Controls(GetControlIndex("winCharacter", "lblName")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winCharacter", "lblClass")).Visible = Visible

    Windows(WindowIndex).Controls(GetControlIndex("winCharacter", "btnDefaultAttribute")).Visible = Not Visible
    Windows(WindowIndex).Controls(GetControlIndex("winCharacter", "btnSpecialAttribute")).Visible = Not Visible
    Windows(WindowIndex).Controls(GetControlIndex("winCharacter", "btnElementalAttribute")).Visible = Not Visible
    
    Windows(WindowIndex).Controls(GetControlIndex("winCharacter", "lblPoints")).Visible = Visible

    For i = 1 To 6
        Windows(WindowIndex).Controls(GetControlIndex("winCharacter", "lblLabel_" & i)).Visible = Visible
        Windows(WindowIndex).Controls(GetControlIndex("winCharacter", "lblStat_" & i)).Visible = Visible
        Windows(WindowIndex).Controls(GetControlIndex("winCharacter", "btnStat_" & i)).Visible = Visible
        Windows(WindowIndex).Controls(GetControlIndex("winCharacter", "btnGreyStat_" & i)).Visible = Visible
    Next

End Sub

Private Sub RenderDefaultAttributes(ByVal x As Long, ByVal Y As Long)
    RenderText Font(Fonts.FontRegular), "Ataque: " & MyAttributes.Attack, x, Y, ColorType.White
    RenderText Font(Fonts.FontRegular), "Defesa: " & MyAttributes.Defense, x, Y + 15, ColorType.White
    RenderText Font(Fonts.FontRegular), "Precisão: " & MyAttributes.Accuracy, x, Y + 30, ColorType.White
    RenderText Font(Fonts.FontRegular), "Evasão: " & MyAttributes.Evasion, x, Y + 45, ColorType.White
    RenderText Font(Fonts.FontRegular), "Aparo: " & MyAttributes.Parry, x, Y + 60, ColorType.White
    RenderText Font(Fonts.FontRegular), "Bloqueio: " & MyAttributes.Block, x, Y + 75, ColorType.White

    RenderText Font(Fonts.FontRegular), "Ataque Mágico: " & MyAttributes.MagicAttack, x, Y + 105, ColorType.White
    RenderText Font(Fonts.FontRegular), "Defesa Mágica: " & MyAttributes.MagicDefense, x, Y + 120, ColorType.White
    RenderText Font(Fonts.FontRegular), "Precisão Mágica: " & MyAttributes.MagicAccuracy, x, Y + 135, ColorType.White
    RenderText Font(Fonts.FontRegular), "Resistência Mágica: " & MyAttributes.MagicResist, x, Y + 150, ColorType.White
    RenderText Font(Fonts.FontRegular), "Concentração: " & MyAttributes.Concentration, x, Y + 165, ColorType.White

    RenderText Font(Fonts.FontRegular), "Taxa Crítica: " & MyAttributes.CritRate & "%", x, Y + 195, ColorType.White
    RenderText Font(Fonts.FontRegular), "Dano Crítico: " & MyAttributes.CritDamage & "%", x, Y + 210, ColorType.White
    RenderText Font(Fonts.FontRegular), "Resistência Taxa Crítica: " & MyAttributes.ResistCritRate & "%", x, Y + 225, ColorType.White
    RenderText Font(Fonts.FontRegular), "Resistência Dano Crítico: " & MyAttributes.ResistCritDamage & "%", x, Y + 240, ColorType.White
End Sub

Private Sub RenderSpecialAttributes(ByVal x As Long, ByVal Y As Long)
    RenderText Font(Fonts.FontRegular), "Amplificação: " & MyAttributes.Amplification & "%", x, Y, ColorType.White
    RenderText Font(Fonts.FontRegular), "Potência de Cura: " & MyAttributes.HealingPower & "%", x, Y + 15, ColorType.White
    RenderText Font(Fonts.FontRegular), "Dano Final: " & MyAttributes.FinalDamage & "%", x, Y + 30, ColorType.White
    RenderText Font(Fonts.FontRegular), "Supressão de Dano: " & MyAttributes.DamageSuppression & "%", x, Y + 45, ColorType.White
    RenderText Font(Fonts.FontRegular), "Inimizade: " & MyAttributes.Enmity & "%", x, Y + 60, ColorType.White

    RenderText Font(Fonts.FontRegular), "Velocidade de Ataque: " & MyAttributes.AttackSpeed & "%", x, Y + 90, ColorType.White
    RenderText Font(Fonts.FontRegular), "Velocidade de Conjuração: " & MyAttributes.CastSpeed & "%", x, Y + 105, ColorType.White

    RenderText Font(Fonts.FontRegular), "Ataque Adicional PvE: " & MyAttributes.PveAttack & "%", x, Y + 135, ColorType.White
    RenderText Font(Fonts.FontRegular), "Defesa Adicional PvE: " & MyAttributes.PveDefense & "%", x, Y + 150, ColorType.White
    RenderText Font(Fonts.FontRegular), "Ataque Adicional PvP: " & MyAttributes.PvpAttack & "%", x, Y + 165, ColorType.White
    RenderText Font(Fonts.FontRegular), "Defesa Adicional PvP: " & MyAttributes.PvpDefense & "%", x, Y + 180, ColorType.White

    RenderText Font(Fonts.FontRegular), "Resistência ao silêncio: " & MyAttributes.SilenceResistance, x, Y + 210, ColorType.White
    RenderText Font(Fonts.FontRegular), "Resistência à cegueira: " & MyAttributes.BlindResistance, x, Y + 225, ColorType.White
    RenderText Font(Fonts.FontRegular), "Resistência ao atordoamento: " & MyAttributes.StunResistance, x, Y + 240, ColorType.White
    RenderText Font(Fonts.FontRegular), "Resistência à queda: " & MyAttributes.StumbleResistance, x, Y + 255, ColorType.White
    
End Sub

Private Sub RenderElementalAttributes(ByVal x As Long, ByVal Y As Long)
    RenderText Font(Fonts.FontRegular), "Dano Adicional Fogo: " & MyAttributes.ElementAttack(Elements.Element_Fire), x, Y, ColorType.White
    RenderText Font(Fonts.FontRegular), "Dano Adicional Água: " & MyAttributes.ElementAttack(Elements.Element_Water), x, Y + 15, ColorType.White
    RenderText Font(Fonts.FontRegular), "Dano Adicional Terra: " & MyAttributes.ElementAttack(Elements.Element_Earth), x, Y + 30, ColorType.White
    RenderText Font(Fonts.FontRegular), "Dano Adicional Vento: " & MyAttributes.ElementAttack(Elements.Element_Wind), x, Y + 45, ColorType.White
    RenderText Font(Fonts.FontRegular), "Dano Adicional Luz: " & MyAttributes.ElementAttack(Elements.Element_Light), x, Y + 60, ColorType.White
    RenderText Font(Fonts.FontRegular), "Dano Adicional Trevas: " & MyAttributes.ElementAttack(Elements.Element_Dark), x, Y + 75, ColorType.White
    
    RenderText Font(Fonts.FontRegular), "Redução de Dano Fogo: " & MyAttributes.ElementDefense(Elements.Element_Fire), x, Y + 105, ColorType.White
    RenderText Font(Fonts.FontRegular), "Redução de Dano Água: " & MyAttributes.ElementDefense(Elements.Element_Water), x, Y + 120, ColorType.White
    RenderText Font(Fonts.FontRegular), "Redução de Dano Terra: " & MyAttributes.ElementDefense(Elements.Element_Earth), x, Y + 135, ColorType.White
    RenderText Font(Fonts.FontRegular), "Redução de Dano Vento: " & MyAttributes.ElementDefense(Elements.Element_Wind), x, Y + 150, ColorType.White
    RenderText Font(Fonts.FontRegular), "Redução de Dano Luz: " & MyAttributes.ElementDefense(Elements.Element_Light), x, Y + 165, ColorType.White
    RenderText Font(Fonts.FontRegular), "Redução de Dano Trevas: " & MyAttributes.ElementDefense(Elements.Element_Dark), x, Y + 180, ColorType.White
End Sub

Private Function IsTwoHandedStyleEquipped() As Boolean
    Dim ItemNum As Long
    Dim EquipmentId As Long

    ItemNum = GetPlayerEquipmentId(PlayerEquipments.EquipWeapon)

    If ItemNum > 0 And ItemNum <= MaximumItems Then
        EquipmentId = Item(ItemNum).EquipmentId

        If EquipmentId > 0 And EquipmentId <= MaximumEquipments Then
            IsTwoHandedStyleEquipped = Equipment(EquipmentId).HandStyle = HandStyle_TwoHanded
        End If
    End If
End Function

Attribute VB_Name = "Upgrade_Window"
Option Explicit

Public UpgradeProgressPercentage As Long
Public IsUpgradeStarted As Boolean

Private Const UpgradeProgressStep As Long = 20

Private Const SlotSize As Long = 34
Private Const SlotPadding As Long = 40

Private Const RequirementX As Long = 60
Private Const RequirementY As Long = 150

Private Const ItemSlotX As Long = 180
Private Const ItemSlotY As Long = 80

Private Const LabelResultX As Long = 0
Private Const LabelResultY As Long = 240

' Índice da janela.
Private WindowIndex As Long

Public Function IsUpgradeVisible() As Boolean
    IsUpgradeVisible = Windows(WindowIndex).Window.Visible
End Function

Public Sub ShowUpgrade()
    CanSwapInvItems = False
    Windows(WindowIndex).Window.Visible = True
End Sub

Public Sub HideUpgrade()
    CanSwapInvItems = True
    Windows(WindowIndex).Window.Visible = False
End Sub

Public Sub CreateWindow_ItemUpgrade()
' Create window
    CreateWindow "winItemUpgrade", "APRIMORAMENTO", zOrder_Win, 0, 0, 400, 400, 0, False, Fonts.OpenSans_Effect, , 2, 7, DesignTypes.desWin_AincradNorm, DesignTypes.desWin_AincradNorm, DesignTypes.desWin_AincradNorm, , , , , , , , , , , GetAddress(AddressOf ItemUpgrade_OnDraw)
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf btnMenu_ItemUpgrade)

    CreateButton WindowCount, "btnUpgrade", 50, 355, 100, 26, "APRIMORAR", OpenSans_Regular, White, , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Button_StartUpgrade)
    CreateButton WindowCount, "btnCancel", 250, 355, 100, 26, "CANCELAR", OpenSans_Regular, White, , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Button_CancelUpgrade)

    CreateLabel WindowCount, "lblItem", 0, ItemSlotY - 20, 400, 16, "Vazio", OpenSans_Regular, , Alignment.alignCentre
    CreateLabel WindowCount, "lblRequirement", 150, RequirementY - 20, 200, 16, "Itens Necessários", OpenSans_Regular, , Alignment.alignLeft

    ' Botões invisíveis para retirada dos items.
    CreatePictureBox WindowCount, "picBlank", ItemSlotX, ItemSlotY, 34, 34, , , , , , , , , , , , GetAddress(AddressOf Item_MouseMove), GetAddress(AddressOf RemoveItem), GetAddress(AddressOf Item_MouseMove)

    CreateLabel WindowCount, "lblWarning", LabelResultX + 25, LabelResultY, 350, 100, "Em caso de falha, existe a possibilidade do item ser destruído ou ter o nível reduzido.", OpenSans_Effect, BrightRed, Alignment.alignCentre

    CreateLabel WindowCount, "lblSuccess", LabelResultX, LabelResultY + 35, 400, 25, "Possibilidade de Sucesso: 0%", OpenSans_Effect, , Alignment.alignCentre
    CreateLabel WindowCount, "lblBreak", LabelResultX, LabelResultY + 50, 400, 25, "Possibilidade de Quebra: 0%", OpenSans_Effect, , Alignment.alignCentre
    CreateLabel WindowCount, "lblReduction", LabelResultX, LabelResultY + 65, 400, 25, "Possibilidade de Redução de Nível: 0%", OpenSans_Effect, , Alignment.alignCentre
    CreateLabel WindowCount, "lblCost", LabelResultX, LabelResultY + 80, 400, 25, "Custo: 0 Ouro", OpenSans_Effect, , Alignment.alignCentre

    CreateLabel WindowCount, "lblProgress", 110, 190, 180, 22, "Processando: 0%", OpenSans_Regular, ColorType.Gold, Alignment.alignCentre

    CanSwapInvItems = True
    WindowIndex = WindowCount
End Sub

Private Sub Button_StartUpgrade()

    If Not IsUpgradeStarted Then
        If CouldStartUpgrade() Then
            IsUpgradeStarted = True
            UpgradeProgressPercentage = 0
            CanMoveNow = False

            Call UpdateProcessText

            AddText ColourChar & GetColStr(ColorType.BrightGreen) & "[Sistema] : " & ColourChar & GetColStr(Grey) & "O aprimoramento foi iniciado.", Grey, , ChatChannel.ChannelGame
        End If
    End If

End Sub

Private Sub Button_CancelUpgrade()
    IsUpgradeStarted = False
    UpgradeProgressPercentage = 0
    CanMoveNow = True

    Call UpdateProcessText
End Sub

Public Sub btnMenu_ItemUpgrade()
    If GetPlayerDead(MyIndex) Then Exit Sub

    If Windows(WindowIndex).Window.Visible Then
        HideWindow WindowIndex

        CanSwapInvItems = True

        ClearWindowValues
        UpdateUpgradeWindow

        IsUpgradeStarted = False
        UpgradeProgressPercentage = 0
        CanMoveNow = True

        Call UpdateProcessText
    Else
        ShowWindow WindowIndex, , False

        CanSwapInvItems = False
    End If
End Sub

Private Sub ItemUpgrade_OnDraw()
    Dim xO As Long
    Dim yO As Long
    Dim i As Long

    Dim ItemId As Long
    Dim Amount As Long
    Dim InventoryIndex As Long
    Dim InventoryAmount As Long

    Dim Color As Long
    Dim Percentage As Single

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top

    RenderTexture Tex_GUI(37), xO + ItemSlotX, yO + ItemSlotY, 0, 0, SlotSize, SlotSize, SlotSize, SlotSize

    If DragBox.Origin <> origin_Upgrade Then
        If UpgradeInventoryIndex > 0 Then
            ItemId = GetInventoryItemNum(UpgradeInventoryIndex)

            If ItemId > 0 And ItemId <= MaximumItems Then
                RenderTexture Tex_Item(Item(ItemId).IconId), xO + ItemSlotX + 1, yO + ItemSlotY + 1, 0, 0, PIC_X, PIC_Y, PIC_X, PIC_Y
            End If
        End If
    End If

    For i = 1 To MaximumUpgradeRequirements
        RenderTexture Tex_GUI(37), xO + RequirementX + (i * SlotPadding), yO + RequirementY, 0, 0, SlotSize, SlotSize, SlotSize, SlotSize

        ItemId = Upgrade.Requirement(i).Id
        Amount = Upgrade.Requirement(i).Amount
        InventoryIndex = Upgrade.Requirement(i).InventoryIndex
        InventoryAmount = Upgrade.Requirement(i).InventoryAmount

        If InventoryIndex > 0 And InventoryAmount >= Amount Then
            Color = -1
        Else
            Color = D3DColorARGB(64, 255, 255, 255)
        End If

        If ItemId > 0 And ItemId <= MaximumItems Then
            RenderTexture Tex_Item(Item(ItemId).IconId), xO + RequirementX + (i * SlotPadding) + 1, yO + RequirementY + 1, 0, 0, PIC_X, PIC_Y, PIC_X, PIC_Y, Color
        End If

        If Amount > 0 Then
            RenderText Font(Fonts.OpenSans_Effect), ConvertCurrency(InventoryAmount) & "/" & Amount, xO + RequirementX + (i * SlotPadding) + 5, yO + RequirementY + 20, ColorType.Yellow
        End If

        If ItemId > 0 And ItemId <= MaximumItems Then
            Call ShowRequirementDescription(xO + RequirementX + (i * SlotPadding), yO + RequirementY, ItemId)
        End If
    Next

    If UpgradeProgressPercentage > 0 Then
        Windows(WindowIndex).Window.canDrag = False
    Else
        Windows(WindowIndex).Window.canDrag = True
    End If

    Percentage = CSng(UpgradeProgressPercentage / 100)

    RenderTexture Tex_GUI(105), xO + 110, yO + 205, 0, 0, 180, 32, 180, 32
    RenderTexture Tex_GUI(106), xO + 110, yO + 205, 0, 0, 180 * Percentage, 32, 180 * Percentage, 32

End Sub

Public Sub DragBox_CheckInventoryToItemUpgrade()

    If DragBox.Origin = origin_Inventory Then
        If DragBox.Type = Part_Item Then
            If DragBox.Slot > 0 Then
                Call SendSelectedItemToUpgrade(DragBox.Slot)
            End If
        End If
    End If

End Sub

Public Sub DragBox_CheckItemUpgradeToInventory()

    If DragBox.Origin = origin_Upgrade Then
        If DragBox.Type = Part_Item Then

            If DragBox.Slot > 0 Then
                ClearWindowValues
            End If
        End If
    End If

End Sub

Public Sub UpdateGoldInUpgradeWindow()
    Dim ControlIndex As Long
    Dim Color As Long

    Color = ColorType.White

    If Upgrade.CostValue > 0 Then
        If GetPlayerCurrency(CurrencyType.Currency_Gold) < Upgrade.CostValue Then
            Color = ColorType.BrightRed
        Else
            Color = ColorType.Gold
        End If
    End If

    ControlIndex = GetControlIndex("winItemUpgrade", "lblCost")
    Windows(WindowIndex).Controls(ControlIndex).Text = "Custo: " & Upgrade.CostValue & " Ouro"
    Windows(WindowIndex).Controls(ControlIndex).textColour = Color
    Windows(WindowIndex).Controls(ControlIndex).textColour_Click = Color
    Windows(WindowIndex).Controls(ControlIndex).textColour_Hover = Color
End Sub

Public Sub UpdateUpgradeWindow()
    Dim ControlIndex As Long
    Dim ItemId As Long
    Dim Level As Long
    Dim Name As String
    Dim Color As Long

    ControlIndex = GetControlIndex("winItemUpgrade", "lblSuccess")
    Windows(WindowIndex).Controls(ControlIndex).Text = "Possibilidade de Sucesso: " & Upgrade.Success & "%"

    ControlIndex = GetControlIndex("winItemUpgrade", "lblBreak")
    Windows(WindowIndex).Controls(ControlIndex).Text = "Possibilidade de Quebra: " & Upgrade.Break & "%"

    ControlIndex = GetControlIndex("winItemUpgrade", "lblReduction")
    Windows(WindowIndex).Controls(ControlIndex).Text = "Possibilidade de Redução de Nível: " & Upgrade.Reduce & "%"

    Color = ColorType.White

    If Upgrade.CostValue > 0 Then
        If GetPlayerCurrency(CurrencyType.Currency_Gold) < Upgrade.CostValue Then
            Color = ColorType.BrightRed
        Else
            Color = ColorType.Gold
        End If
    End If

    ControlIndex = GetControlIndex("winItemUpgrade", "lblCost")
    Windows(WindowIndex).Controls(ControlIndex).Text = "Custo: " & Upgrade.CostValue & " Ouro"
    Windows(WindowIndex).Controls(ControlIndex).textColour = Color
    Windows(WindowIndex).Controls(ControlIndex).textColour_Click = Color
    Windows(WindowIndex).Controls(ControlIndex).textColour_Hover = Color

    ControlIndex = GetControlIndex("winItemUpgrade", "lblItem")

    If UpgradeInventoryIndex > 0 Then
        ItemId = GetInventoryItemNum(UpgradeInventoryIndex)
        Level = GetInventoryItemLevel(UpgradeInventoryIndex)

        If Level > 0 Then
            Name = Item(ItemId).Name & " +" & Level
        Else
            Name = Item(ItemId).Name
        End If

        Color = ColorType.Gold

        Windows(WindowIndex).Controls(ControlIndex).Text = Name
    Else
        Color = ColorType.White

        Windows(WindowIndex).Controls(ControlIndex).Text = "Vazio"
    End If


    Windows(WindowIndex).Controls(ControlIndex).textColour = Color
    Windows(WindowIndex).Controls(ControlIndex).textColour_Click = Color
    Windows(WindowIndex).Controls(ControlIndex).textColour_Hover = Color
End Sub

Private Sub ClearWindowValues()
    Dim i As Long

    UpgradeInventoryIndex = 0

    Upgrade.Break = 0
    Upgrade.Success = 0
    Upgrade.Reduce = 0
    Upgrade.ReduceValue = 0
    Upgrade.CostValue = 0

    For i = 1 To MaximumUpgradeRequirements
        Upgrade.Requirement(i).Id = 0
        Upgrade.Requirement(i).Amount = 0
        Upgrade.Requirement(i).InventoryIndex = 0
        Upgrade.Requirement(i).InventoryAmount = 0
    Next

    Call UpdateUpgradeWindow
End Sub

Private Sub Item_MouseMove()
    If DragBox.Origin = origin_None Then
        If UpgradeInventoryIndex > 0 Then
            Dim xO As Long
            Dim yO As Long

            xO = Windows(WindowIndex).Window.Left
            yO = Windows(WindowIndex).Window.Top

            ShowInventoryDescription xO + ItemSlotX, yO + ItemSlotY, UpgradeInventoryIndex, False
        End If
    End If
End Sub

Private Sub ShowInventoryDescription(ByVal X As Long, ByVal Y As Long, ByRef InvSlot As Long, ByVal Preview As Boolean)
    If InvSlot > 0 Then
        If IsInsidePicture(X, Y, 34, 34) Then

            Dim WinDescription As Long
            WinDescription = GetWindowIndex("winDescription")

            ' calc position
            X = Windows(WindowIndex).Window.Left - Windows(GetWindowIndex("winDescription")).Window.Width - 2
            Y = Windows(WindowIndex).Window.Top
            ' offscreen?
            If X < 0 Then
                ' switch to right
                X = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width + 2
            End If

            If Y + Windows(WinDescription).Window.Height >= ScreenHeight Then
                Y = ScreenHeight - Windows(WinDescription).Window.Height
            End If

            Dim Inventory As InventoryRec

            Inventory.Num = GetInventoryItemNum(InvSlot)
            Inventory.Value = 1
            Inventory.Level = GetInventoryItemLevel(InvSlot)
            Inventory.Bound = GetInventoryItemBound(InvSlot)
            Inventory.AttributeId = GetInventoryItemAttributeId(InvSlot)
            Inventory.UpgradeId = GetInventoryItemUpgradeId(InvSlot)

            If Inventory.Num > 0 And Inventory.Num <= MaximumItems Then
                If Item(Inventory.Num).Type = ItemType_Heraldry Then
                    Call ShowHeraldryDescription(X, Y, Inventory, Item(Inventory.Num).Price)
                Else
                    Call ShowItemDesc(X, Y, Inventory)
                End If
            End If
        End If
    End If
End Sub

Private Sub RemoveItem()
    If UpgradeInventoryIndex > 0 Then
        If (mouseClick(VK_RBUTTON) And lastMouseClick(VK_RBUTTON) = 0) Then
            Call ClearWindowValues
            Exit Sub
        End If

        Dim WinIndex As Long

        ' drag it
        With DragBox
            .Type = Part_Item
            .Value = GetInventoryItemNum(UpgradeInventoryIndex)
            .Origin = origin_Upgrade
            .Slot = UpgradeInventoryIndex
        End With

        WinIndex = GetWindowIndex("winDragBox")

        With Windows(WinIndex).Window
            .State = MouseDown
            .Left = lastMouseX - 16
            .Top = lastMouseY - 16
            .movedX = clickedX - .Left
            .movedY = clickedY - .Top
        End With

        ShowWindow WinIndex, , False
        Windows(GetWindowIndex("winItemUpgrade")).Window.State = Normal
    End If

    Item_MouseMove
End Sub

Private Function IsInsidePicture(ByRef X As Long, ByRef Y As Long, ByVal Width As Long, ByVal Height As Long) As Boolean
    Dim TempRec As RECT

    With TempRec
        .Top = Y
        .Bottom = .Top + Height
        .Left = X
        .Right = .Left + Width
    End With

    If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
        If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
            IsInsidePicture = True
        End If
    End If

End Function

Private Sub ShowRequirementDescription(ByVal X As Long, ByVal Y As Long, ByVal ItemNum As Long)
    If ItemNum > 0 Then
        If IsInsidePicture(X, Y, 34, 34) Then
            Dim WinDescription As Long

            WinDescription = GetWindowIndex("winDescription")

            ' calc position
            X = Windows(WindowIndex).Window.Left - Windows(GetWindowIndex("winDescription")).Window.Width - 2
            Y = Windows(WindowIndex).Window.Top

            ' offscreen?
            If X < 0 Then
                ' switch to right
                X = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width + 2
            End If

            If Y + Windows(WinDescription).Window.Height >= ScreenHeight Then
                Y = ScreenHeight - Windows(WinDescription).Window.Height
            End If

            Dim Inventory As InventoryRec
            Inventory.Num = ItemNum

            ShowItemDesc X, Y, Inventory
        End If
    End If
End Sub

Private Sub UpdateProcessText()
    Windows(WindowIndex).Controls(GetControlIndex("winItemUpgrade", "lblProgress")).Text = "Processando: " & UpgradeProgressPercentage & "%"
End Sub

Public Sub ProcessUpgradeItem()
    If IsUpgradeStarted Then
        If UpgradeProgressPercentage = 100 Then
            UpgradeProgressPercentage = 0
            IsUpgradeStarted = False
            CanMoveNow = True

            Call SendStartUpgrade(UpgradeInventoryIndex)
        End If

        If UpgradeProgressPercentage < 100 And IsUpgradeStarted Then
            UpgradeProgressPercentage = UpgradeProgressPercentage + UpgradeProgressStep

            If UpgradeProgressPercentage > 100 Then
                UpgradeProgressPercentage = 100
            End If
        End If

        UpdateProcessText
    End If
End Sub

Public Sub CountRequiredItemToUpgrade()
    Dim i As Long, n As Long
    Dim ItemId As Long
    Dim Count As Long

    For i = 1 To MaximumUpgradeRequirements
        ItemId = Upgrade.Requirement(i).Id

        Upgrade.Requirement(i).InventoryIndex = 0
        Count = 0

        If ItemId > 0 And ItemId <= MaximumItems Then
            For n = 1 To MaxInventories
                If GetInventoryItemNum(n) = ItemId Then
                    Upgrade.Requirement(i).InventoryIndex = n
                    Count = Count + GetInventoryItemValue(n)
                End If
            Next
        End If

        Upgrade.Requirement(i).InventoryAmount = Count
    Next

End Sub

Private Function CouldStartUpgrade() As Boolean
    Dim VerifiedRequirement(1 To MaximumUpgradeRequirements) As Boolean
    Dim i As Long
    
    CouldStartUpgrade = False

    If UpgradeInventoryIndex < 1 Or UpgradeInventoryIndex > MaxInventories Then
        Exit Function
    End If
    
    Dim ItemNum As Long
    Dim ItemLevel As Long
    
    ItemNum = GetInventoryItemNum(UpgradeInventoryIndex)
    ItemLevel = GetInventoryItemLevel(UpgradeInventoryIndex)
    
    If ItemNum < 1 Or ItemNum > MaximumItems Then
        Exit Function
    End If
    
    If ItemLevel >= Item(ItemNum).MaximumLevel Then
        AddText ColourChar & GetColStr(ColorType.BrightRed) & "[Sistema] : " & ColourChar & GetColStr(Grey) & "Este item não pode ser mais aprimorado.", Grey, , ChatChannel.ChannelGame
        Exit Function
    End If
    
    If GetPlayerCurrency(CurrencyType.Currency_Gold) < Upgrade.CostValue Then
        Exit Function
    End If

    For i = 1 To MaximumUpgradeRequirements
        If Upgrade.Requirement(i).Id > 0 Then
            If Upgrade.Requirement(i).InventoryAmount < Upgrade.Requirement(i).Amount Then
                Exit Function
            End If
        End If
    Next

    CouldStartUpgrade = True

End Function

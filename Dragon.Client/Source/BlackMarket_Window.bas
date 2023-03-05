Attribute VB_Name = "BlackMarket_Window"
Option Explicit

Private Const LeftListX As Long = 130
Private Const LeftListY As Long = 45

Private Const ListOffsetY = 40

' Categoria selecionada.
Public SelectedCashCategory As BlackMarketCategory
' Quantidade máxima de páginas nesta categoria.
Public MaximumItemCategoryPage As Long
' Página atual nesta categoria.
Public CurrentItemCategoryPage As Long
Public CanMoveToNextPage As Boolean
' Item selecionado na janela.
Private SelectedIndex As Long
Private WindowIndex As Long

Public Sub CreateWindow_CashShop()
    Dim i As Long

    ' Create window
    CreateWindow "winCashShop", "MERCADO NEGRO", zOrder_Win, 0, 0, 404, 410, 0, False, Fonts.FontRegular, , 2, 7, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar
    ' Centralise it
    CentraliseWindow WindowCount, 0
    ' Set the index for spawning controls
    zOrder_Con = 1
    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonMenu_CashShop)
    CreateLabel WindowCount, "lblBalance", 0, 60, 404, 22, "Meu Balanço: $ 195,454.011", FontRegular, Gold, Alignment.AlignCenter

    'CreatePictureBox WindowCount, "picBack", 20, LeftListY + 45, 364, 290, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox

    For i = 0 To BlackMarketListCount - 1
        CreateLabel WindowCount, "lblItemName" & (i + 1), (LeftListX + 55), (LeftListY + 62) + (ListOffsetY * i), 190, 20, "", FontRegular, Coral, Alignment.AlignLeft
        CreateLabel WindowCount, "lblItemPrice" & (i + 1), (LeftListX + 55), (LeftListY + 76) + (ListOffsetY * i), 190, 20, "", FontRegular, White, Alignment.AlignLeft
    Next

    CreateButton WindowCount, "btnCategory1", LeftListX - 94, LeftListY + 75, 95, 25, "PROMOÇÃO", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf SelectCategory_Promo)
    CreateButton WindowCount, "btnCategory2", LeftListX - 94, LeftListY + 105, 95, 25, "ESTÍMULOS", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf SelectCategory_Boost)
    CreateButton WindowCount, "btnCategory3", LeftListX - 94, LeftListY + 135, 95, 25, "SUPLEMENTOS", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf SelectCategory_Supply)
    CreateButton WindowCount, "btnCategory4", LeftListX - 94, LeftListY + 165, 95, 25, "CONSUMÍVEIS", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf SelectCategory_Consumable)
    CreateButton WindowCount, "btnCategory5", LeftListX - 94, LeftListY + 195, 95, 25, "SERVIÇOS", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf SelectCategory_Service)
    CreateButton WindowCount, "btnCategory6", LeftListX - 94, LeftListY + 225, 95, 25, "PACOTES", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf SelectCategory_Package)
    CreateButton WindowCount, "btnCategory7", LeftListX - 94, LeftListY + 255, 95, 25, "PETS", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf SelectCategory_Pet)

    CreatePictureBox WindowCount, "picIcon1", (LeftListX + 15), (LeftListY + 60) + (ListOffsetY * 0), 32, 32, , , , , , , , , , , , GetAddress(AddressOf ButtonSelectItem1_MouseMove), GetAddress(AddressOf ButtonSelectItem1_Click), GetAddress(AddressOf ButtonSelectItem1_MouseMove)
    CreatePictureBox WindowCount, "picIcon2", (LeftListX + 15), (LeftListY + 60) + (ListOffsetY * 1), 32, 32, , , , , , , , , , , , GetAddress(AddressOf ButtonSelectItem2_MouseMove), GetAddress(AddressOf ButtonSelectItem2_Click), GetAddress(AddressOf ButtonSelectItem2_MouseMove)
    CreatePictureBox WindowCount, "picIcon3", (LeftListX + 15), (LeftListY + 60) + (ListOffsetY * 2), 32, 32, , , , , , , , , , , , GetAddress(AddressOf ButtonSelectItem3_MouseMove), GetAddress(AddressOf ButtonSelectItem3_Click), GetAddress(AddressOf ButtonSelectItem3_MouseMove)
    CreatePictureBox WindowCount, "picIcon4", (LeftListX + 15), (LeftListY + 60) + (ListOffsetY * 3), 32, 32, , , , , , , , , , , , GetAddress(AddressOf ButtonSelectItem4_MouseMove), GetAddress(AddressOf ButtonSelectItem4_Click), GetAddress(AddressOf ButtonSelectItem4_MouseMove)
    CreatePictureBox WindowCount, "picIcon5", (LeftListX + 15), (LeftListY + 60) + (ListOffsetY * 4), 32, 32, , , , , , , , , , , , GetAddress(AddressOf ButtonSelectItem5_MouseMove), GetAddress(AddressOf ButtonSelectItem5_Click), GetAddress(AddressOf ButtonSelectItem5_MouseMove)
    CreatePictureBox WindowCount, "picIcon6", (LeftListX + 15), (LeftListY + 60) + (ListOffsetY * 5), 32, 32, , , , , , , , , , , , GetAddress(AddressOf ButtonSelectItem6_MouseMove), GetAddress(AddressOf ButtonSelectItem6_Click), GetAddress(AddressOf ButtonSelectItem6_MouseMove)

    CreatePictureBox WindowCount, "picName1", (LeftListX + 47), (LeftListY + 60) + (ListOffsetY * 0), 190, 32, , , , , , , , , , , , GetAddress(AddressOf ButtonSelectItem1_MouseMove), GetAddress(AddressOf ButtonSelectItem1_Click), GetAddress(AddressOf ButtonSelectItem1_MouseMove)
    CreatePictureBox WindowCount, "picName2", (LeftListX + 47), (LeftListY + 60) + (ListOffsetY * 1), 190, 32, , , , , , , , , , , , GetAddress(AddressOf ButtonSelectItem2_MouseMove), GetAddress(AddressOf ButtonSelectItem2_Click), GetAddress(AddressOf ButtonSelectItem2_MouseMove)
    CreatePictureBox WindowCount, "picName3", (LeftListX + 47), (LeftListY + 60) + (ListOffsetY * 2), 190, 32, , , , , , , , , , , , GetAddress(AddressOf ButtonSelectItem3_MouseMove), GetAddress(AddressOf ButtonSelectItem3_Click), GetAddress(AddressOf ButtonSelectItem3_MouseMove)
    CreatePictureBox WindowCount, "picName4", (LeftListX + 47), (LeftListY + 60) + (ListOffsetY * 3), 190, 32, , , , , , , , , , , , GetAddress(AddressOf ButtonSelectItem4_MouseMove), GetAddress(AddressOf ButtonSelectItem4_Click), GetAddress(AddressOf ButtonSelectItem4_MouseMove)
    CreatePictureBox WindowCount, "picName5", (LeftListX + 47), (LeftListY + 60) + (ListOffsetY * 4), 190, 32, , , , , , , , , , , , GetAddress(AddressOf ButtonSelectItem5_MouseMove), GetAddress(AddressOf ButtonSelectItem5_Click), GetAddress(AddressOf ButtonSelectItem5_MouseMove)
    CreatePictureBox WindowCount, "picName6", (LeftListX + 47), (LeftListY + 60) + (ListOffsetY * 5), 190, 32, , , , , , , , , , , , GetAddress(AddressOf ButtonSelectItem6_MouseMove), GetAddress(AddressOf ButtonSelectItem6_Click), GetAddress(AddressOf ButtonSelectItem6_MouseMove)

    'Botões setas
    CreateLabel WindowCount, "lblPage", LeftListX + 65, LeftListY + 305, 120, 50, "Página: 1/2", FontRegular, White, Alignment.AlignCenter
    CreateButton WindowCount, "btnUp", LeftListX + 60, LeftListY + 305, 15, 15, , , , , , , Tex_GUI(44), Tex_GUI(45), Tex_GUI(46), , , , , , GetAddress(AddressOf MoveListToUp)
    CreateButton WindowCount, "btnDown", LeftListX + 179, LeftListY + 305, 15, 15, , , , , , , Tex_GUI(47), Tex_GUI(48), Tex_GUI(49), , , , , , GetAddress(AddressOf MoveListToDown)

    ' ################ SELECTED ITEM ################
    CreateLabel WindowCount, "lblItemName", LeftListX + 25, LeftListY + 63, 200, 20, "", FontRegular, Coral, Alignment.AlignCenter
    CreateLabel WindowCount, "lblItemPrice", LeftListX + 25, LeftListY + 77, 200, 20, "", FontRegular, White, Alignment.AlignCenter
    CreatePictureBox WindowCount, "picNameBack", LeftListX + 25, LeftListY + 60, 200, 34, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ShowSelectedItem), , GetAddress(AddressOf ShowSelectedItem)
    ' Icone
    CreatePictureBox WindowCount, "picIcon", (LeftListX + 109), LeftListY + 105, 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ShowSelectedItem), , GetAddress(AddressOf ShowSelectedItem)
    ' Text Character
    CreateLabel WindowCount, "lblTarget", LeftListX + 25, LeftListY + 145, 200, 20, "Personagem", FontRegular, Gold, Alignment.AlignCenter
    CreateTextbox WindowCount, "txtName", LeftListX + 25, LeftListY + 160, 200, 24, , Fonts.FontRegular, White, Alignment.AlignCenter, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 8, 5

    CreateLabel WindowCount, "lblQuantity", LeftListX + 25, LeftListY + 185, 200, 20, "Quantidade", FontRegular, Gold, Alignment.AlignCenter
    CreateTextbox WindowCount, "txtQuantity", LeftListX + 25, LeftListY + 200, 200, 24, "1", Fonts.FontRegular, White, Alignment.AlignCenter, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 8, 5, , GetAddress(AddressOf UpdatePrice)

    CreateLabel WindowCount, "lblGift", LeftListX + 25, LeftListY + 230, 200, 20, "Este item pode ser enviado como presente.", FontRegular, BrightGreen, Alignment.AlignCenter

    CreateButton WindowCount, "btnPurchase", LeftListX + 25, LeftListY + 270, 95, 25, "Comprar", FontRegular, White, , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonPurchase_Click)
    CreateButton WindowCount, "btnCancel", LeftListX + 130, LeftListY + 270, 95, 25, "Cancelar", FontRegular, White, , , , , , , DesignTypes.DesignRed, DesignTypes.DesignRedHover, DesignTypes.DesignRedClick, , , GetAddress(AddressOf ButtonCancel_Click)

    WindowIndex = WindowCount
    SelectedCashCategory = BlackMarketCategory_Promo

    ' Define para que possa aceitar somente números.
    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtQuantity")).AcceptOnlyNumbers = True
    ' Define o limite de caracteres.
    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtQuantity")).TextLimit = 3
    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtName")).TextLimit = 25
    ' Define o callback para o valor do preço. O evento é chamado toda vez que uma tecla for pressionada.
    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtQuantity")).EntityCallBack(entStates.KeyPress) = GetAddress(AddressOf UpdatePrice)

    MaximumItemCategoryPage = 1
    CurrentItemCategoryPage = 1

    Call ChangeToPurchaseState(False)

End Sub

Private Sub SelectCategory_Promo()
    SelectedCashCategory = BlackMarketCategory_Promo
    Call RequestBlackMarketItems
End Sub

Private Sub SelectCategory_Boost()
    SelectedCashCategory = BlackMarketCategory_Boost
    Call RequestBlackMarketItems
End Sub

Private Sub SelectCategory_Supply()
    SelectedCashCategory = BlackMarketCategory_Supply
    Call RequestBlackMarketItems
End Sub

Private Sub SelectCategory_Consumable()
    SelectedCashCategory = BlackMarketCategory_Consumable
    Call RequestBlackMarketItems
End Sub

Private Sub SelectCategory_Service()
    SelectedCashCategory = BlackMarketCategory_Service
    Call RequestBlackMarketItems
End Sub

Private Sub SelectCategory_Package()
    SelectedCashCategory = BlackMarketCategory_Package
    Call RequestBlackMarketItems
End Sub

Private Sub SelectCategory_Pet()
    SelectedCashCategory = BlackMarketCategory_Pet
    Call RequestBlackMarketItems
End Sub

Private Sub RequestBlackMarketItems()
    MaximumItemCategoryPage = 1
    CurrentItemCategoryPage = 1
    CanMoveToNextPage = False

    Call ChangeToPurchaseState(False)
    Call UpdateCashControlValue
    Call SendRequestBlackMarketItems(SelectedCashCategory, CurrentItemCategoryPage)
End Sub

Private Sub MoveListToUp()
    If CanMoveToNextPage Then
        If CurrentItemCategoryPage > 1 Then
            CurrentItemCategoryPage = CurrentItemCategoryPage - 1

            CanMoveToNextPage = False
            Call SendRequestBlackMarketItems(SelectedCashCategory, CurrentItemCategoryPage)
        End If
    End If
End Sub

Private Sub MoveListToDown()
    If CanMoveToNextPage Then
        If CurrentItemCategoryPage < MaximumItemCategoryPage Then
            CurrentItemCategoryPage = CurrentItemCategoryPage + 1

            CanMoveToNextPage = False
            Call SendRequestBlackMarketItems(SelectedCashCategory, CurrentItemCategoryPage)
        End If
    End If
End Sub

Public Sub ButtonMenu_CashShop()
    If MyIndex > 0 Then
        If GetPlayerDead(MyIndex) Then Exit Sub
    End If

    If Windows(WindowIndex).Window.Visible Then
        HideWindow WindowIndex
    Else
        SelectedCashCategory = BlackMarketCategory_Promo
        MaximumItemCategoryPage = 1
        CurrentItemCategoryPage = 1
        CanMoveToNextPage = False

        Call ChangeToPurchaseState(False)
        Call UpdateCashControlValue
        Call SendRequestBlackMarketItems(SelectedCashCategory, CurrentItemCategoryPage)

        ShowWindow WindowIndex
    End If
End Sub

Private Sub ButtonSelectItem1_MouseMove()
    Dim Index As Long
    Index = 1

    If BlackMarketItems(Index).ItemNum > 0 Then
        Call ShowItemDescription(Index)
    End If
End Sub

Private Sub ButtonSelectItem2_MouseMove()
    Dim Index As Long
    Index = 2

    If BlackMarketItems(Index).ItemNum > 0 Then
        Call ShowItemDescription(Index)
    End If
End Sub

Private Sub ButtonSelectItem3_MouseMove()
    Dim Index As Long
    Index = 3

    If BlackMarketItems(Index).ItemNum > 0 Then
        Call ShowItemDescription(Index)
    End If
End Sub

Private Sub ButtonSelectItem4_MouseMove()
    Dim Index As Long
    Index = 4

    If BlackMarketItems(Index).ItemNum > 0 Then
        Call ShowItemDescription(Index)
    End If
End Sub

Private Sub ButtonSelectItem5_MouseMove()
    Dim Index As Long
    Index = 5

    If BlackMarketItems(Index).ItemNum > 0 Then
        Call ShowItemDescription(Index)
    End If
End Sub

Private Sub ButtonSelectItem6_MouseMove()
    Dim Index As Long
    Index = 6

    If BlackMarketItems(Index).ItemNum > 0 Then
        Call ShowItemDescription(Index)
    End If
End Sub

Private Sub ShowSelectedItem()
    If SelectedIndex > 0 Then
        Call ShowItemDescription(SelectedIndex)
    End If
End Sub

Private Sub ShowItemDescription(ByVal Index As Long)
    If BlackMarketItems(Index).ItemNum <= 0 Then
        Exit Sub
    End If

    Dim X As Long, Y As Long
    Dim WinDescription As Long

    WinDescription = GetWindowIndex("winDescription")

    ' calc position
    X = Windows(WindowIndex).Window.Left - Windows(WinDescription).Window.Width - 2
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

    Inventory.Num = BlackMarketItems(Index).ItemNum
    Inventory.Level = BlackMarketItems(Index).ItemLevel
    Inventory.Bound = BlackMarketItems(Index).ItemBound
    Inventory.AttributeId = BlackMarketItems(Index).AttributeId
    Inventory.UpgradeId = BlackMarketItems(Index).UpgradeId

    Call ShowItemDesc(X, Y, Inventory)
End Sub

Private Sub ButtonSelectItem1_Click()
    Dim Index As Long
    Index = 1

    If BlackMarketItems(Index).ItemNum > 0 Then
        If MouseButton_Clicked(MouseButtons_Left) Then
            SelectedIndex = Index
            Call ChangeToPurchaseState(True)
            Call UpdateBuyControlsText
            Call UpdatePrice
        End If
    End If
End Sub

Private Sub ButtonSelectItem2_Click()
    Dim Index As Long
    Index = 2

    If BlackMarketItems(Index).ItemNum > 0 Then
        If MouseButton_Clicked(MouseButtons_Left) Then
            SelectedIndex = Index
            Call ChangeToPurchaseState(True)
            Call UpdateBuyControlsText
            Call UpdatePrice
        End If
    End If
End Sub

Private Sub ButtonSelectItem3_Click()
    Dim Index As Long
    Index = 3

    If BlackMarketItems(Index).ItemNum > 0 Then
        If MouseButton_Clicked(MouseButtons_Left) Then
            SelectedIndex = Index
            Call ChangeToPurchaseState(True)
            Call UpdateBuyControlsText
            Call UpdatePrice
        End If
    End If
End Sub

Private Sub ButtonSelectItem4_Click()
    Dim Index As Long
    Index = 4

    If BlackMarketItems(Index).ItemNum > 0 Then
        If MouseButton_Clicked(MouseButtons_Left) Then
            SelectedIndex = Index
            Call ChangeToPurchaseState(True)
            Call UpdateBuyControlsText
            Call UpdatePrice
        End If
    End If
End Sub

Private Sub ButtonSelectItem5_Click()
    Dim Index As Long
    Index = 5

    If BlackMarketItems(Index).ItemNum > 0 Then
        If MouseButton_Clicked(MouseButtons_Left) Then
            SelectedIndex = Index
            Call ChangeToPurchaseState(True)
            Call UpdateBuyControlsText
            Call UpdatePrice
        End If
    End If
End Sub

Private Sub ButtonSelectItem6_Click()
    Dim Index As Long
    Index = 6

    If BlackMarketItems(Index).ItemNum > 0 Then
        If MouseButton_Clicked(MouseButtons_Left) Then
            SelectedIndex = Index
            Call ChangeToPurchaseState(True)
            Call UpdateBuyControlsText
            Call UpdatePrice
        End If
    End If
End Sub

Private Sub ButtonPurchase_Click()
    If MouseButton_Clicked(MouseButtons_Left) Then
        If SelectedIndex > 0 Then
            Dim Target As String
            Dim Quantity As Long

            Target = Trim$(Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtName")).Text)
            Quantity = Val(Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtQuantity")).Text)

            If Quantity < 1 Then Quantity = 1

            SendPurchaseCashShopItem BlackMarketItems(SelectedIndex).Id, Quantity, Target
        End If
    End If
End Sub

Private Sub ButtonCancel_Click()
    If MouseButton_Clicked(MouseButtons_Left) Then
        Call ChangeToPurchaseState(False)
    End If
End Sub

Private Sub ChangeToPurchaseState(ByVal Enabled As Boolean)
    Dim i As Long

    For i = 0 To BlackMarketListCount - 1
        Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "picIcon" & (i + 1))).Visible = Not Enabled
        Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "lblItemName" & (i + 1))).Visible = Not Enabled
        Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "lblItemPrice" & (i + 1))).Visible = Not Enabled
        Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "picName" & (i + 1))).Visible = Not Enabled
    Next

    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "lblPage")).Visible = Not Enabled
    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "btnUp")).Visible = Not Enabled
    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "btnDown")).Visible = Not Enabled

    ' ##########
    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "picNameBack")).Visible = Enabled
    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "lblItemName")).Visible = Enabled
    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "lblItemPrice")).Visible = Enabled

    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "picIcon")).Visible = Enabled

    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "lblTarget")).Visible = Enabled
    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtName")).Visible = Enabled

    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "lblQuantity")).Visible = Enabled
    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtQuantity")).Visible = Enabled

    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "lblGift")).Visible = Enabled

    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "btnPurchase")).Visible = Enabled
    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "btnCancel")).Visible = Enabled

End Sub

Private Sub UpdatePrice()
    If SelectedIndex > 0 Then
        Dim Quantity As Long
        Dim Limit As Long
        Dim IsEnabled As Boolean

        Limit = BlackMarketItems(SelectedIndex).PurchaseLimit

        If Limit = 0 Or Limit = 1 Then
            Windows(WindowIndex).activeControl = 0

            IsEnabled = False
        Else
            IsEnabled = True
        End If

        Quantity = Val(Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtQuantity")).Text)

        If Quantity > Limit Then
            Quantity = Limit
        End If

        Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtQuantity")).Text = Quantity
        Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "lblItemPrice")).Text = "Price $ " & Quantity * BlackMarketItems(SelectedIndex).Price

        Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtQuantity")).Enabled = IsEnabled
    End If
End Sub

Private Sub SetControlImage(ByVal Index As Long, ByVal TextureNum As Long)
    Dim i As Long, ControlIndex As Long

    ControlIndex = GetControlIndex("winCashShop", "picIcon" & Index)

    For i = 0 To entStates.state_Count - 1
        Windows(WindowIndex).Controls(ControlIndex).image(i) = TextureNum
    Next

End Sub

Private Sub UpdateBuyControlsText()
    If SelectedIndex > 0 Then
        Dim ControlNameIndex As Long, ControlIconIndex As Long, ControlGiftIndex As Long
        Dim ItemNum As Long, ItemLevel As Long, Colour As Long
        Dim i As Long, IconId As Long

        ItemNum = BlackMarketItems(SelectedIndex).ItemNum
        ItemLevel = BlackMarketItems(SelectedIndex).ItemLevel

        ControlNameIndex = GetControlIndex("winCashShop", "lblItemName")

        If ItemNum > 0 And ItemNum <= MaximumItems Then
            Colour = GetRarityColor(Item(ItemNum).Rarity)

            If ItemLevel > 0 Then
                Windows(WindowIndex).Controls(ControlNameIndex).Text = Trim$(Item(ItemNum).Name) & " +" & ItemLevel
            Else
                Windows(WindowIndex).Controls(ControlNameIndex).Text = Trim$(Item(ItemNum).Name)
            End If
        End If

        Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtQuantity")).Text = "1"
        Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtName")).Text = Trim$(Player(MyIndex).Name)

        ControlIconIndex = GetControlIndex("winCashShop", "picIcon")

        ' Se houver algum ícone, obtem o índice do ícone.
        ' Do contrário, mantém zero para limpar o controle.
        IconId = Item(ItemNum).IconId

        If IconId > 0 Then
            IconId = Tex_Item(IconId)
        End If

        For i = 0 To entStates.state_Count - 1
            Windows(WindowIndex).Controls(ControlIconIndex).image(i) = IconId
        Next

        ControlGiftIndex = GetControlIndex("winCashShop", "lblGift")
        ControlNameIndex = GetControlIndex("winCashShop", "txtName")

        Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtName")).Enabled = BlackMarketItems(SelectedIndex).GiftEnabled

        If BlackMarketItems(SelectedIndex).GiftEnabled Then
            Windows(WindowIndex).Controls(ControlGiftIndex).Text = "Este item pode ser enviado como presente."
            Windows(WindowIndex).Controls(ControlGiftIndex).TextColour = Gold
            Windows(WindowIndex).Controls(ControlGiftIndex).TextColourClick = Gold
            Windows(WindowIndex).Controls(ControlGiftIndex).TextColourHover = Gold

            Windows(WindowIndex).Controls(ControlNameIndex).TextColour = White
            Windows(WindowIndex).Controls(ControlNameIndex).TextColourClick = White
            Windows(WindowIndex).Controls(ControlNameIndex).TextColourHover = White

        Else
            Windows(WindowIndex).Controls(ControlGiftIndex).Text = "Este item não pode ser enviado como presente."
            Windows(WindowIndex).Controls(ControlGiftIndex).TextColour = BrightRed
            Windows(WindowIndex).Controls(ControlGiftIndex).TextColourClick = BrightRed
            Windows(WindowIndex).Controls(ControlGiftIndex).TextColourHover = BrightRed

            Windows(WindowIndex).Controls(ControlNameIndex).TextColour = Grey
            Windows(WindowIndex).Controls(ControlNameIndex).TextColourClick = Grey
            Windows(WindowIndex).Controls(ControlNameIndex).TextColourHover = Grey
        End If

        Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "txtQuantity")).Enabled = Item(ItemNum).Stackable

        Dim ControlQuantityIndex As Long
        ControlQuantityIndex = GetControlIndex("winCashShop", "txtQuantity")

        If Item(ItemNum).Stackable Then
            Windows(WindowIndex).Controls(ControlQuantityIndex).TextColour = White
            Windows(WindowIndex).Controls(ControlQuantityIndex).TextColourClick = White
            Windows(WindowIndex).Controls(ControlQuantityIndex).TextColourHover = White
        Else
            Windows(WindowIndex).Controls(ControlQuantityIndex).TextColour = Grey
            Windows(WindowIndex).Controls(ControlQuantityIndex).TextColourClick = Grey
            Windows(WindowIndex).Controls(ControlQuantityIndex).TextColourHover = Grey
        End If

    End If
End Sub

Public Sub UpdateCashControlValue()
    Dim Text As String

    If PlayerCash > 0 Then
        Text = Format$(PlayerCash, "#,###,###,###")
    Else
        Text = "0"
    End If

    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "lblBalance")).Text = "Meu Balanço: $ " & Text
End Sub

Public Sub UpdateBlackMarketItemList()
    Dim i As Long
    Dim ControlNameIndex As Long, ControlPriceIndex As Long
    Dim ItemNum As Long, Price As Long, Colour As Long, ItemLevel As Long

    Windows(WindowIndex).Controls(GetControlIndex("winCashShop", "lblPage")).Text = "Página: " & CurrentItemCategoryPage & "/" & MaximumItemCategoryPage

    For i = 0 To BlackMarketListCount - 1
        ControlNameIndex = GetControlIndex("winCashShop", "lblItemName" & (i + 1))
        ControlPriceIndex = GetControlIndex("winCashShop", "lblItemPrice" & (i + 1))

        ItemNum = BlackMarketItems(i + 1).ItemNum
        ItemLevel = BlackMarketItems(i + 1).ItemLevel
        Price = BlackMarketItems(i + 1).Price

        If ItemNum > 0 And ItemNum <= MaximumItems Then
            Colour = GetRarityColor(Item(ItemNum).Rarity)

            If ItemLevel > 0 Then
                Windows(WindowIndex).Controls(ControlNameIndex).Text = Trim$(Item(ItemNum).Name) & " +" & ItemLevel
            Else
                Windows(WindowIndex).Controls(ControlNameIndex).Text = Trim$(Item(ItemNum).Name)
            End If

            Windows(WindowIndex).Controls(ControlNameIndex).TextColour = Colour
            Windows(WindowIndex).Controls(ControlNameIndex).TextColourClick = Colour
            Windows(WindowIndex).Controls(ControlNameIndex).TextColourHover = Colour

            Colour = GetCurrencyColor(Price)

            Windows(WindowIndex).Controls(ControlPriceIndex).Text = "Cash: $ " & Format$(Price, "#,###,###,###")
            Windows(WindowIndex).Controls(ControlPriceIndex).TextColour = Colour
            Windows(WindowIndex).Controls(ControlPriceIndex).TextColourClick = Colour
            Windows(WindowIndex).Controls(ControlPriceIndex).TextColourHover = Colour

            If Item(ItemNum).IconId > 0 Then
                Call SetControlImage(i + 1, Tex_Item(Item(ItemNum).IconId))
            End If
        Else
            Windows(WindowIndex).Controls(ControlNameIndex).Text = vbNullString
            Windows(WindowIndex).Controls(ControlPriceIndex).Text = vbNullString

            Call SetControlImage(i + 1, 0)
        End If
    Next

End Sub

Attribute VB_Name = "Shop_Window"
Option Explicit

Private Const ShopTop As Long = 83
Private Const ShopLeft As Long = 9
Private Const ShopOffsetY As Long = 6
Private Const ShopOffsetX As Long = 6
Private Const ShopColumns As Long = 7

Private ShopSelectedSlot As Long
Private ShopSelectedItem As Long
Private ShopIsSelling As Boolean

Private WindowIndex As Long

Public Function IsShopVisible() As Boolean
    IsShopVisible = Windows(WindowIndex).Window.Visible
End Function

Public Sub ShowShop()
    Windows(WindowIndex).Window.Visible = True
End Sub

Public Sub HideShop()
    Windows(WindowIndex).Window.Visible = False
End Sub

Public Sub CreateWindow_Shop()
    ' Create window
    CreateWindow "winShop", "SHOP", zOrder_Win, 0, 0, 278, 345, 0, False, Fonts.FontRegular, , 2, 5, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , GetAddress(AddressOf Shop_MouseMove), GetAddress(AddressOf Shop_MouseDown), GetAddress(AddressOf Shop_MouseMove), GetAddress(AddressOf Shop_MouseMove), , , GetAddress(AddressOf DrawShop)
    ' additional mouse event
    Windows(WindowCount).Window.EntityCallBack(entStates.MouseUp) = GetAddress(AddressOf Shop_MouseMove)
    ' Centralise it
    CentraliseWindow WindowCount, 0

    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonShop_Close)

    CreateButton WindowCount, "btnBuyTab", 0, 43, 135, 26, "COMPRAR", FontRegular, Green, , , , , , , , , , , , GetAddress(AddressOf Button_ShopBuying)
    CreateButton WindowCount, "btnSellTab", 140, 43, 135, 26, "VENDER", FontRegular, , , , , , , , , , , , , GetAddress(AddressOf Button_ShopSelling)
    ' Picture Box

    ' Gold
    CreatePictureBox WindowCount, "picBlank", 9, 278, 260, 52, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox

    ' Buttons
    CreateButton WindowCount, "btnBuy", 190, 289, 70, 26, "COMPRAR", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonShop_Buy)
    CreateButton WindowCount, "btnSell", 190, 289, 70, 26, "VENDER", FontRegular, White, , False, , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonShop_Sell)

    ' Labels
    CreateLabel WindowCount, "lblName", 56, 285, 300, , "Test Item", FontRegular, White, Alignment.AlignLeft
    CreateLabel WindowCount, "lblCost", 56, 302, 300, , "1000", FontRegular, White, Alignment.AlignLeft

    CreatePictureBox WindowCount, "picItemBG", 15, 286, 36, 36, , , , , Tex_GUI(37), Tex_GUI(37), Tex_GUI(37)
    CreatePictureBox WindowCount, "picItem", 15, 288, 32, 32
    
    WindowIndex = WindowCount
End Sub

Private Sub Button_ShopBuying()
    Dim BtnSellIndex As Long
    Dim BtnBuyIndex As Long

    BtnSellIndex = GetControlIndex("winShop", "btnSellTab")
    BtnBuyIndex = GetControlIndex("winShop", "btnBuyTab")

    Windows(WindowIndex).Controls(GetControlIndex("winShop", "btnBuy")).Visible = True
    Windows(WindowIndex).Controls(GetControlIndex("winShop", "btnSell")).Visible = False
    Windows(WindowIndex).Controls(BtnSellIndex).TextColour = White
    Windows(WindowIndex).Controls(BtnSellIndex).TextColourHover = White
    Windows(WindowIndex).Controls(BtnSellIndex).TextColourClick = White
    Windows(WindowIndex).Controls(BtnBuyIndex).TextColour = Green
    Windows(WindowIndex).Controls(BtnBuyIndex).TextColourHover = Green
    Windows(WindowIndex).Controls(BtnBuyIndex).TextColourClick = Green

    ShopIsSelling = False
    ShopSelectedSlot = 1
    UpdateShop
End Sub

Private Sub Button_ShopSelling()
    Dim BtnSellIndex As Long
    Dim BtnBuyIndex As Long

    BtnSellIndex = GetControlIndex("winShop", "btnSellTab")
    BtnBuyIndex = GetControlIndex("winShop", "btnBuyTab")

    Windows(WindowIndex).Controls(GetControlIndex("winShop", "btnBuy")).Visible = False
    Windows(WindowIndex).Controls(GetControlIndex("winShop", "btnSell")).Visible = True
    Windows(WindowIndex).Controls(BtnSellIndex).TextColour = Green
    Windows(WindowIndex).Controls(BtnSellIndex).TextColourHover = Green
    Windows(WindowIndex).Controls(BtnSellIndex).TextColourClick = Green
    Windows(WindowIndex).Controls(BtnBuyIndex).TextColour = White
    Windows(WindowIndex).Controls(BtnBuyIndex).TextColourHover = White
    Windows(WindowIndex).Controls(BtnBuyIndex).TextColourClick = White

    ShopIsSelling = True
    ShopSelectedSlot = 1
    UpdateShop
End Sub

Public Sub OpenShop()
    InShop = True
    
    ShopSelectedSlot = 1
    ShopSelectedItem = GetShopItemNum(ShopSelectedSlot)
    
    Windows(WindowIndex).Controls(GetControlIndex("winShop", "btnSell")).Visible = False
    Windows(WindowIndex).Controls(GetControlIndex("winShop", "btnBuy")).Visible = True
    
    ShopIsSelling = False

    ' set the current item
    UpdateShop
    ' show the window
    ShowShop
End Sub

Public Sub CloseShop()
    SendCloseShop

    HideShop
    
    ShopSelectedSlot = 0
    ShopSelectedItem = 0
    ShopIsSelling = False
    InShop = False
End Sub

Private Sub UpdateShop()
    Dim i As Long, CostValue As Long
    Dim CurrencyIndex As Long

    If Not InShop Then Exit Sub

    ' make sure we have an item selected
    If ShopSelectedSlot = 0 Then ShopSelectedSlot = 1

    With Windows(WindowIndex)
        ' buying items
        If Not ShopIsSelling Then
            ShopSelectedItem = GetShopItemNum(ShopSelectedSlot)
            ' labels
            If ShopSelectedItem > 0 And ShopSelectedItem <= MaximumItems Then
                .Controls(GetControlIndex("winShop", "lblName")).Text = Trim$(Item(ShopSelectedItem).Name)
                ' check if it's gold
                CurrencyIndex = GetShopItemCurrencyId(ShopSelectedSlot)
                .Controls(GetControlIndex("winShop", "lblCost")).Text = Trim$(GetCurrencyData(CurrencyIndex).Name) & " " & GetShopItemCost(ShopSelectedSlot)

                ' draw the item
                For i = 0 To entStates.state_Count - 1
                    .Controls(GetControlIndex("winShop", "picItem")).image(i) = Tex_Item(Item(ShopSelectedItem).IconId)
                Next
            Else
                .Controls(GetControlIndex("winShop", "lblName")).Text = "Empty Slot"
                .Controls(GetControlIndex("winShop", "lblCost")).Text = vbNullString
                ' draw the item
                For i = 0 To entStates.state_Count - 1
                    .Controls(GetControlIndex("winShop", "picItem")).image(i) = 0
                Next
            End If
        Else
            ShopSelectedItem = GetInventoryItemNum(ShopSelectedSlot)

            ' labels
            If ShopSelectedItem > 0 And ShopSelectedItem <= MaximumItems Then
                .Controls(GetControlIndex("winShop", "lblName")).Text = Trim$(Item(ShopSelectedItem).Name)
                ' calc cost
                CostValue = Item(ShopSelectedItem).Price

                If CostValue > 0 Then
                    .Controls(GetControlIndex("winShop", "lblCost")).Text = Trim$(GetCurrencyData(CurrencyType.Currency_Gold).Name) & " " & CostValue
                Else
                    .Controls(GetControlIndex("winShop", "lblCost")).Text = "Não pode ser vendido"
                End If

                ' draw the item
                For i = 0 To entStates.state_Count - 1
                    .Controls(GetControlIndex("winShop", "picItem")).image(i) = Tex_Item(Item(ShopSelectedItem).IconId)
                Next
            Else
                .Controls(GetControlIndex("winShop", "lblName")).Text = "Empty Slot"
                .Controls(GetControlIndex("winShop", "lblCost")).Text = vbNullString
                ' draw the item

                For i = 0 To entStates.state_Count - 1
                    .Controls(GetControlIndex("winShop", "picItem")).image(i) = 0
                Next
            End If
        End If
    End With
End Sub

Private Sub DrawShop()
    Dim xO As Long, yO As Long, ItemPic As Long, ItemNum As Long, i As Long, Top As Long, Left As Long, Y As Long, X As Long, Colour As Long
    Dim Width As Long
    Dim Height As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width

    

    Width = 76
    Height = 76

    Y = yO + 78
    ' render grid - row
    For i = 1 To 3
        If i = 3 Then Height = 42
        RenderTexture Tex_GUI(26), xO + 4, Y, 0, 0, Width, Height, Width, Height
        RenderTexture Tex_GUI(26), xO + 80, Y, 0, 0, Width, Height, Width, Height
        RenderTexture Tex_GUI(26), xO + 156, Y, 0, 0, Width, Height, Width, Height
        RenderTexture Tex_GUI(26), xO + 232, Y, 0, 0, 42, Height, 42, Height
        Y = Y + 76
    Next

    If Not ShopIsSelling Then
        ' render the shop items
        For i = 1 To MaxShopItems
            ItemNum = GetShopItemNum(i)

            ' draw early
            Top = yO + ShopTop + ((ShopOffsetY + 32) * ((i - 1) \ ShopColumns))
            Left = xO + ShopLeft + ((ShopOffsetX + 32) * (((i - 1) Mod ShopColumns)))
            
            ' draw selected square
            If ShopSelectedSlot = i Then RenderTexture Tex_GUI(38), Left, Top, 0, 0, 32, 32, 32, 32

            If ItemNum > 0 And ItemNum <= MaximumItems Then
                ItemPic = Item(ItemNum).IconId

                If ItemPic > 0 And ItemPic <= Count_Item Then
                    ' draw item
                    RenderTexture Tex_Item(ItemPic), Left, Top, 0, 0, 32, 32, 32, 32

                    ' If item is a stack - draw the amount you have
                    If GetShopItemValue(i) > 1 Then
                        Y = Top + 21
                        X = Left + 1
                        Colour = GetCurrencyColor(GetShopItemValue(i))
                        RenderText Font(Fonts.FontRegular), ConvertCurrency(GetShopItemValue(i)), X, Y, Colour
                    End If
                End If
            End If
        Next
    Else
        ' render the shop items
        For i = 1 To MaxShopItems
            ItemNum = GetInventoryItemNum(i)

            ' draw early
            Top = yO + ShopTop + ((ShopOffsetY + 32) * ((i - 1) \ ShopColumns))
            Left = xO + ShopLeft + ((ShopOffsetX + 32) * (((i - 1) Mod ShopColumns)))
            ' draw selected square
            If ShopSelectedSlot = i Then RenderTexture Tex_GUI(38), Left, Top, 0, 0, 32, 32, 32, 32

            If ItemNum > 0 And ItemNum <= MaximumItems Then
                ItemPic = Item(ItemNum).IconId
                
                If ItemPic > 0 And ItemPic <= Count_Item Then
                    ' draw item
                    RenderTexture Tex_Item(ItemPic), Left, Top, 0, 0, 32, 32, 32, 32

                    ' If item is a stack - draw the amount you have
                    If GetInventoryItemValue(i) > 1 Then
                        Y = Top + 21
                        X = Left + 1

                        Colour = GetCurrencyColor(GetInventoryItemValue(i))
                        RenderText Font(Fonts.FontRegular), ConvertCurrency(GetInventoryItemValue(i)), X, Y, Colour
                    End If
                End If
            End If
        Next
    End If
End Sub

Private Function GetShopSlotFromPosition(StartX As Long, StartY As Long) As Long
    Dim TempRec As RECT
    Dim i As Long

    For i = 1 To MaxShopItems
        With TempRec
            .Top = StartY + ShopTop + ((ShopOffsetY + 32) * ((i - 1) \ ShopColumns))
            .Bottom = .Top + PIC_Y
            .Left = StartX + ShopLeft + ((ShopOffsetX + 32) * (((i - 1) Mod ShopColumns)))
            .Right = .Left + PIC_X
        End With

        If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
            If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
                GetShopSlotFromPosition = i
                Exit Function
            End If
        End If
    Next
End Function

Private Sub ButtonShop_Close()
    CloseShop
End Sub

Private Sub ButtonShop_Buy()
    Call SendBuyItem(ShopSelectedSlot)
End Sub

Private Sub ButtonShop_Sell()
    Dim Amount As Long
    Dim ItemId As Long

    If ShopSelectedSlot > 0 Then
        ItemId = GetInventoryItemNum(ShopSelectedSlot)

        If ItemId > 0 And ItemId <= MaximumItems Then
            If Item(ItemId).Price > 0 Then
                Amount = GetInventoryItemValue(ShopSelectedSlot)

                If Amount > 1 Then
                    ShowDialogue "Vender Item", "Insira a quantidade para vender", "", DialogueTypeSellAmount, DialogueStyleInput, ShopSelectedSlot
                Else
                    Call SendSellItem(ShopSelectedSlot, 1)
                End If
            End If

        End If
    End If

End Sub

Private Sub Shop_MouseDown()
    Dim ShopSlot As Long

    ' is there an item?
    ShopSlot = GetShopSlotFromPosition(Windows(GetWindowIndex("winShop")).Window.Left, Windows(GetWindowIndex("winShop")).Window.Top)

    If ShopSlot Then
        ' set the active slot
        ShopSelectedSlot = ShopSlot
        UpdateShop
    End If

    Shop_MouseMove
End Sub

Private Sub Shop_MouseMove()
    Dim ShopSlot As Long, ItemNum As Long, X As Long, Y As Long
    Dim Inventory As InventoryRec

    If Not InShop Then Exit Sub

    ShopSlot = GetShopSlotFromPosition(Windows(GetWindowIndex("winShop")).Window.Left, Windows(GetWindowIndex("winShop")).Window.Top)

    If ShopSlot Then
        Dim WinDescription As Long
        WinDescription = GetWindowIndex("winDescription")

        ' calc position
        X = Windows(WindowIndex).Window.Left - Windows(GetWindowIndex("winDescription")).Window.Width - 5
        Y = Windows(WindowIndex).Window.Top
        ' offscreen?
        If X < 0 Then
            ' switch to right
            X = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width
        End If

        If Y + Windows(WinDescription).Window.Height >= ScreenHeight Then
            Y = ScreenHeight - Windows(WinDescription).Window.Height
        End If

        ' selling/buying
        If Not ShopIsSelling Then
            ' get the itemnum
            ItemNum = GetShopItemNum(ShopSlot)

            If ItemNum < 1 Or ItemNum > MaximumItems Then
                Exit Sub
            End If

            Inventory.Num = GetShopItemNum(ShopSlot)
            Inventory.Value = GetShopItemValue(ShopSlot)
            Inventory.Level = GetShopItemLevel(ShopSlot)
            Inventory.Bound = GetShopItemBound(ShopSlot)
            Inventory.AttributeId = GetShopItemAttributeId(ShopSlot)
            Inventory.UpgradeId = GetShopItemUpgradeId(ShopSlot)
            ' show
            Call ShowItemDesc(X, Y, Inventory)
        Else
            ItemNum = GetInventoryItemNum(ShopSlot)

            If ItemNum < 1 Or ItemNum > MaximumItems Then
                Exit Sub
            End If

            Inventory.Num = GetInventoryItemNum(ShopSlot)
            Inventory.Value = GetInventoryItemValue(ShopSlot)
            Inventory.Level = GetInventoryItemLevel(ShopSlot)
            Inventory.Bound = GetInventoryItemBound(ShopSlot)
            Inventory.AttributeId = GetInventoryItemAttributeId(ShopSlot)
            Inventory.UpgradeId = GetInventoryItemUpgradeId(ShopSlot)

            If Item(ItemNum).Type = ItemType.ItemType_Heraldry Then
                Call ShowHeraldryDescription(X, Y, Inventory, Item(ItemNum).Price)
            Else
                Call ShowItemDesc(X, Y, Inventory)
            End If

        End If
    End If
End Sub


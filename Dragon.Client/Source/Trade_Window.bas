Attribute VB_Name = "Trade_Window"
Option Explicit

' Trade
Private Const TradeTop As Long = 0
Private Const TradeLeft As Long = 0
Private Const TradeOffsetY As Long = 6
Private Const TradeOffsetX As Long = 6
Private Const TradeColumns As Long = 5

' Trade Window Index
Private WindowIndex As Long

Public Function IsTradeVisible() As Boolean
    IsTradeVisible = Windows(WindowIndex).Window.Visible
End Function

Public Sub ShowTrade()
    Dim InvWindowIndex As Long
    
    InvWindowIndex = GetWindowIndex("winInventory")

    Windows(InvWindowIndex).Window.Left = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width + 5
    Windows(InvWindowIndex).Window.Top = Windows(WindowIndex).Window.Top
    Windows(InvWindowIndex).Window.Visible = True
    Windows(WindowIndex).Window.Visible = True
End Sub

Public Sub HideTrade()
    Dim InvWindowIndex As Long
    
    Windows(GetWindowIndex("winInventory")).Window.Visible = False
    Windows(WindowIndex).Window.Visible = False
End Sub

Public Sub CreateWindow_Trade()
    ' Create window
    CreateWindow "winTrade", "NEGOCIAÇÃO", zOrder_Win, 0, 0, 412, 320, 0, False, Fonts.FontRegular, , 2, 5, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , , , , , , , GetAddress(AddressOf DrawTrade)
    ' Centralise it
    CentraliseWindow WindowCount
    ' Close Button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonTrade_Close)
    ' Labels
    CreateLabel WindowCount, "lblYourStatus", 15, 65, 180, 9, "Aguardando Confirmação", FontRegular, BrightRed, Alignment.AlignCenter
    CreateLabel WindowCount, "lblTheirStatus", 15 + 200, 65, 180, 9, "Aguardando Confirmação", FontRegular, BrightRed, Alignment.AlignCenter
    CreateLabel WindowCount, "lblYourTrade", 15, 50, 180, 9, "DragonicK Lv. 1", FontRegular, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblTheirTrade", 15 + 200, 50, 180, 9, "DragonicK Lv. 1", FontRegular, White, Alignment.AlignCenter
    ' Buttons
    CreateButton WindowCount, "btnConfirm", 70, 245, 120, 24, "CONFIRMAR", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonTrade_Confirm)
    CreateButton WindowCount, "btnDecline", 228, 245, 120, 24, "RECUSAR", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonTrade_Close)
    CreateButton WindowCount, "btnAccept", 149, 280, 120, 24, "ACEITAR", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonTrade_Accept)

    ' Amounts
    'CreatePictureBox WindowCount, "picYour", 15, 206, 184, 24, , , , , Tex_GUI(1), Tex_GUI(1), Tex_GUI(1)
    CreateLabel WindowCount, "lblYourCurrency", 15, 210, 180, 20, "Ouro: 0", FontRegular, White, Alignment.AlignCenter

    'CreatePictureBox WindowCount, "picYour", 215, 206, 184, 24, , , , , Tex_GUI(1), Tex_GUI(1), Tex_GUI(1)
    CreateLabel WindowCount, "lblTheirCurrency", 215, 210, 180, 20, "Ouro: 0", FontRegular, White, Alignment.AlignCenter

    ' Buttons
    CreateButton WindowCount, "btnSetGold", 15, 206, 24, 24, " +", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonTrade_Currency)

    CreatePictureBox WindowCount, "picYourTrade", 13, 81, 185, 115, , , , , , , , , , , , GetAddress(AddressOf TradeMouseMove_Me), GetAddress(AddressOf TradeMouseDown_Me), GetAddress(AddressOf TradeMouseMove_Me)
    CreatePictureBox WindowCount, "picOtherTrade", 214, 81, 185, 115, , , , , , , , , , , , GetAddress(AddressOf TradeMouseMove_Their), , GetAddress(AddressOf TradeMouseMove_Their)
    
    WindowIndex = WindowCount
End Sub

Private Sub DrawTrade()
    Dim xO As Long, yO As Long, Width As Long, Height As Long, i As Long, Y As Long, X As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width
    Height = Windows(WindowIndex).Window.Height

    ' left
    Width = 76
    Height = 76
    Y = yO + 81
    
    For i = 1 To 2
        If i = 2 Then Height = 38
        RenderTexture Tex_GUI(26), xO + 4 + 5, Y, 0, 0, Width, Height, Width, Height
        RenderTexture Tex_GUI(26), xO + 80 + 5, Y, 0, 0, Width, Height, Width, Height
        RenderTexture Tex_GUI(26), xO + 156 + 5, Y, 0, 0, 42, Height, 42, Height
        Y = Y + 76
    Next

    ' right
    Width = 76
    Height = 76
    Y = yO + 81
    
    For i = 1 To 2
        If i = 2 Then Height = 38
        RenderTexture Tex_GUI(26), xO + 4 + 205, Y, 0, 0, Width, Height, Width, Height
        RenderTexture Tex_GUI(26), xO + 80 + 205, Y, 0, 0, Width, Height, Width, Height
        RenderTexture Tex_GUI(26), xO + 156 + 205, Y, 0, 0, 42, Height, 42, Height
        Y = Y + 76
    Next

    DrawMyTrade
    DrawTheirTrade
End Sub

Private Sub DrawMyTrade()
    Dim i As Long, ItemNum As Long, ItemPic As Long, Top As Long, Left As Long, Colour As Long, X As Long, Y As Long
    Dim xO As Long, yO As Long

    xO = Windows(WindowIndex).Window.Left + 13
    yO = Windows(WindowIndex).Window.Top + 87

    ' your items
    For i = 1 To MaxTradeItems
        ' If some inventory is selected.
        If GetMyTradeInventoryIndex(i) > 0 Then
            ItemNum = GetInventoryItemNum(GetMyTradeInventoryIndex(i))

            If ItemNum > 0 And ItemNum <= MaximumItems Then
                ItemPic = Item(ItemNum).IconId
                
                If ItemPic > 0 And ItemPic <= Count_Item Then
                    Top = yO + TradeTop + ((TradeOffsetY + 32) * ((i - 1) \ TradeColumns))
                    Left = xO + TradeLeft + ((TradeOffsetX + 32) * (((i - 1) Mod TradeColumns)))

                    ' Draw icon
                    RenderTexture Tex_Item(ItemPic), Left, Top, 0, 0, 32, 32, 32, 32

                    ' If item is a stack - draw the amount you have
                    If GetMyTradeInventoryItemValue(i) > 1 Then
                        Y = Top + 21
                        X = Left + 1
                        
                        ' Draw currency but with k, m, b etc. using a convertion function
                        Colour = GetCurrencyColor(GetMyTradeInventoryItemValue(i))
                        RenderText Font(Fonts.FontRegular), ConvertCurrency(GetMyTradeInventoryItemValue(i)), X, Y, Colour
                    End If
                End If
            End If
        End If
    Next
End Sub

Private Sub DrawTheirTrade()
    Dim i As Long, ItemNum As Long, ItemPic As Long, Top As Long, Left As Long, Colour As Long, X As Long, Y As Long
    Dim xO As Long, yO As Long

    xO = Windows(WindowIndex).Window.Left + 214
    yO = Windows(WindowIndex).Window.Top + 87

    ' Their items
    For i = 1 To MaxTradeItems
        ItemNum = GetOtherTradeItemNum(i)

        If ItemNum > 0 And ItemNum <= MaximumItems Then
            ItemPic = Item(ItemNum).IconId

            If ItemPic > 0 And ItemPic <= Count_Item Then
                Top = yO + TradeTop + ((TradeOffsetY + 32) * ((i - 1) \ TradeColumns))
                Left = xO + TradeLeft + ((TradeOffsetX + 32) * (((i - 1) Mod TradeColumns)))
                ' Draw icon
                RenderTexture Tex_Item(ItemPic), Left, Top, 0, 0, 32, 32, 32, 32

                ' If item is a stack - draw the amount you have
                If GetOtherTradeItemValue(i) > 1 Then
                    Y = Top + 21
                    X = Left + 1
                    
                    ' Draw currency but with k, m, b etc. using a convertion function
                    Colour = GetCurrencyColor(GetOtherTradeItemValue(i))
                    RenderText Font(Fonts.FontRegular), ConvertCurrency(GetOtherTradeItemValue(i)), X, Y, Colour
                End If
            End If
        End If
    Next
End Sub

Private Sub TradeMouseDown_Me()
    Dim xO As Long, yO As Long, TradeSlot As Long, InvSlot As Long
    
    xO = Windows(WindowIndex).Window.Left + 13
    yO = Windows(WindowIndex).Window.Top + 87

    TradeSlot = GetTradeSlotFromPosition(xO, yO)

    ' make sure it exists
    If TradeSlot > 0 Then
        InvSlot = GetMyTradeInventoryIndex(TradeSlot)
        
        If InvSlot = 0 Then Exit Sub
        
        If GetInventoryItemNum(InvSlot) < 1 Or GetInventoryItemNum(InvSlot) > MaximumItems Then
            Exit Sub
        End If

        ' unoffer the item
        SendUntradeItem TradeSlot
    End If
End Sub

Private Sub TradeMouseMove_Me()
    Dim xO As Long, yO As Long, TradeSlot As Long, X As Long, Y As Long, InvSlot As Long

    xO = Windows(WindowIndex).Window.Left + 13
    yO = Windows(WindowIndex).Window.Top + 87

    TradeSlot = GetTradeSlotFromPosition(xO, yO)

    ' make sure it exists
    If TradeSlot > 0 Then
        InvSlot = GetMyTradeInventoryIndex(TradeSlot)

        If InvSlot = 0 Then Exit Sub
        If GetInventoryItemNum(InvSlot) = 0 Then Exit Sub

        Dim ItemNum As Long
        Dim WinDescription As Long

        ItemNum = GetInventoryItemNum(InvSlot)

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

        If ItemNum > 0 Then
            Dim Inventory As InventoryRec

            Inventory.Num = GetInventoryItemNum(InvSlot)
            Inventory.Value = GetInventoryItemValue(InvSlot)
            Inventory.Level = GetInventoryItemLevel(InvSlot)
            Inventory.Bound = GetInventoryItemBound(InvSlot)
            Inventory.AttributeId = GetInventoryItemAttributeId(InvSlot)
            Inventory.UpgradeId = GetInventoryItemUpgradeId(InvSlot)

            If Item(ItemNum).Type = ItemType.ItemType_Heraldry Then
                Call ShowHeraldryDescription(X, Y, Inventory, Item(ItemNum).Price)
            Else
                Call ShowItemDesc(X, Y, Inventory)
            End If
        End If
    End If
End Sub

Private Sub TradeMouseMove_Their()
    Dim xO As Long, yO As Long, TradeSlot As Long, X As Long, Y As Long

    xO = Windows(WindowIndex).Window.Left + 214
    yO = Windows(WindowIndex).Window.Top + 87

    TradeSlot = GetTradeSlotFromPosition(xO, yO)

    ' make sure it exists
    If TradeSlot > 0 Then
        If GetOtherTradeItemNum(TradeSlot) = 0 Then Exit Sub

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

        If GetOtherTradeItemNum(TradeSlot) > 0 Then
            Dim Inventory As InventoryRec

            Inventory.Num = GetOtherTradeItemNum(TradeSlot)
            Inventory.Value = GetOtherTradeItemValue(TradeSlot)
            Inventory.Level = GetOtherTradeItemLevel(TradeSlot)
            Inventory.AttributeId = GetOtherTradeItemAttributeId(TradeSlot)
            Inventory.UpgradeId = GetOtherTradeItemUpgradeId(TradeSlot)

            If Item(Inventory.Num).Type = ItemType.ItemType_Heraldry Then
                Call ShowHeraldryDescription(X, Y, Inventory, Item(Inventory.Num).Price)
            Else
                Call ShowItemDesc(X, Y, Inventory)
            End If
        End If

    End If
End Sub

Private Function GetTradeSlotFromPosition(StartX As Long, StartY As Long) As Long
    Dim TempRec As RECT
    Dim i As Long

    For i = 1 To MaxTradeItems
        With TempRec
            .Top = StartY + TradeTop + ((TradeOffsetY + 32) * ((i - 1) \ TradeColumns))
            .Bottom = .Top + PIC_Y
            .Left = StartX + TradeLeft + ((TradeOffsetX + 32) * (((i - 1) Mod TradeColumns)))
            .Right = .Left + PIC_X
        End With

        If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
            If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
                GetTradeSlotFromPosition = i
                Exit Function
            End If
        End If
    Next
End Function

Private Sub ButtonTrade_Accept()
    If GetTradeStatus() = TradeStatusType_Confirmed Then
       SendAcceptTrade
    ElseIf GetTradeStatus() = TradeStatusType_Waiting Then
        Call AddText("Ambos precisam confirmar a negociação antes.", BrightRed)
    End If
End Sub

Private Sub ButtonTrade_Confirm()
    SendConfirmTrade
End Sub

Private Sub ButtonTrade_Close()
    Call HideTrade
    SendDeclineTrade
End Sub

Private Sub ButtonTrade_Currency()
    If GetTradeStatus() < TradeStatusType.TradeStatusType_Confirmed Then
        If GetMyTradeStatus() < TradeStatusType.TradeStatusType_Confirmed Then
            ShowDialogue "Negociar moeda", "Insira a quantia de gold.", "", DialogueTypeTradeGoldAmount, DialogueStyleInput, 0
        Else
            Call AddText("Você já confirmou a negociação.", BrightRed)
        End If
    Else
        Call AddText("A negociação já foi confirmada.", BrightRed)
    End If
End Sub

Public Sub ClearTrade()
    Dim i As Long
    Dim MyControlIndex As Long
    Dim OtherControlIndex As Long

    Call SetTradeStatus(TradeStatusType_Waiting)
    Call SetMyTradeStatus(TradeStatusType_Waiting)
    Call SetOtherTradeStatus(TradeStatusType_Waiting)

    MyControlIndex = GetControlIndex("winTrade", "lblYourStatus")
    OtherControlIndex = GetControlIndex("winTrade", "lblTheirStatus")

    Windows(WindowIndex).Controls(GetControlIndex("winTrade", "lblYourCurrency")).Text = "Ouro: 0"
    Windows(WindowIndex).Controls(GetControlIndex("winTrade", "lblTheirCurrency")).Text = "Ouro: 0"

    Call SetControlTextAndColor(MyControlIndex, GetTradeStatusText(GetMyTradeStatus()), GetTradeStatusColor(GetMyTradeStatus()))
    Call SetControlTextAndColor(OtherControlIndex, GetTradeStatusText(GetOtherTradeStatus()), GetTradeStatusColor(GetOtherTradeStatus()))

    Call SetMyTradeCurrency(0)
    Call SetOtherTradeCurrency(0)

    For i = 1 To MaxTradeItems
        Call ClearMyTradeInventory(i)
        Call ClearOtherTradeItems(i)
    Next
End Sub

Private Sub SetControlTextAndColor(ByVal ControlIndex As Long, ByVal Text As String, ByVal Color As Long)
    Windows(WindowIndex).Controls(ControlIndex).Text = Text
    Windows(WindowIndex).Controls(ControlIndex).TextColour = Color
    Windows(WindowIndex).Controls(ControlIndex).TextColourClick = Color
    Windows(WindowIndex).Controls(ControlIndex).TextColourHover = Color
End Sub

Private Sub SetCurrencyTextAndColor(ByVal ControlIndex As Long, ByVal Amount As Long)
    Dim Color As Long
    Color = GetCurrencyColor(Amount)

    If Amount = 0 Then
        Windows(WindowIndex).Controls(ControlIndex).Text = "Ouro: 0"
    Else
        Windows(WindowIndex).Controls(ControlIndex).Text = "Ouro: " & Format$(Amount, "#,###,###,###")
    End If

    Windows(WindowIndex).Controls(ControlIndex).TextColour = Color
    Windows(WindowIndex).Controls(ControlIndex).TextColourClick = Color
    Windows(WindowIndex).Controls(ControlIndex).TextColourHover = Color
End Sub

Public Sub UpdateTradeStatus()
    Dim MyControl As Long, OtherControl As Long
    Dim MyControlIndex As Long, OtherControlIndex As Long

    MyControlIndex = GetControlIndex("winTrade", "lblYourStatus")
    OtherControlIndex = GetControlIndex("winTrade", "lblTheirStatus")

    If IsMeWhoStartedTrade Then
        MyControl = MyControlIndex
        OtherControl = OtherControlIndex
    Else
        MyControl = OtherControlIndex
        OtherControl = MyControlIndex
    End If

    Call SetControlTextAndColor(MyControl, GetTradeStatusText(GetMyTradeStatus()), GetTradeStatusColor(GetMyTradeStatus()))
    Call SetControlTextAndColor(OtherControl, GetTradeStatusText(GetOtherTradeStatus()), GetTradeStatusColor(GetOtherTradeStatus()))
End Sub

Public Sub UpdateTradeCurrency()
    Dim MyControl As Long, OtherControl As Long
    Dim MyControlIndex As Long, OtherControlIndex As Long

    MyControlIndex = GetControlIndex("winTrade", "lblYourCurrency")
    OtherControlIndex = GetControlIndex("winTrade", "lblTheirCurrency")

    If IsMeWhoStartedTrade Then
        MyControl = MyControlIndex
        OtherControl = OtherControlIndex
    Else
        MyControl = OtherControlIndex
        OtherControl = MyControlIndex
    End If

    Call SetCurrencyTextAndColor(MyControl, GetMyTradeCurrency())
    Call SetCurrencyTextAndColor(OtherControl, GetOtherTradeCurrency())
End Sub

Public Sub UpdateTradeText(ByVal MyText As String, ByVal OtherText As String)
    Dim MyControl As Long, OtherControl As Long
    Dim MyControlIndex As Long, OtherControlIndex As Long
    
    MyControlIndex = GetControlIndex("winTrade", "lblYourTrade")
    OtherControlIndex = GetControlIndex("winTrade", "lblTheirTrade")

    If IsMeWhoStartedTrade Then
        MyControl = MyControlIndex
        OtherControl = OtherControlIndex
    Else
        MyControl = OtherControlIndex
        OtherControl = MyControlIndex
    End If

    Windows(WindowIndex).Controls(MyControl).Text = MyText
    Windows(WindowIndex).Controls(OtherControl).Text = OtherText
End Sub

Attribute VB_Name = "Inventory_Window"
Option Explicit

Public Const MaxInventoryTab As Long = 5
Public Const MaxInventoryPerTab As Long = 35
Public Const MaxInventory As Long = 175

' Inventory constants
Public Const InvTop As Long = 87
Public Const InvLeft As Long = 9
Public Const InvOffsetY As Long = 6
Public Const InvOffsetX As Long = 6
Public Const InvColumns As Long = 5

Private WindowIndex As Long

Public InventoryentoryTabIndex As Long
Public MaxInventories As Long

Public Sub CreateWindow_Inventory()
    Dim i As Long
    ' Create window
    CreateWindow "winInventory", "INVENTÁRIO", zOrder_Win, 0, 0, 202, 380, 0, False, Fonts.FontRegular, , 2, 7, DesignTypes.DesignWindowWithTopBarAndNavBar, DesignTypes.DesignWindowWithTopBarAndNavBar, DesignTypes.DesignWindowWithTopBarAndNavBar, , , , , GetAddress(AddressOf Inventory_MouseMove), GetAddress(AddressOf Inventory_MouseDown), GetAddress(AddressOf Inventory_MouseMove), GetAddress(AddressOf Inventory_DblClick), , , GetAddress(AddressOf DrawInventory)
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1
    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf btnMenu_Inv)

    CreateButton WindowCount, "btnInventoryTab1", 12 + (0 * 36), 43, 26, 26, "1", FontRegular, White, , , , , , , , , , , , GetAddress(AddressOf Button_InventoryTab1_Click)
    CreateButton WindowCount, "btnInventoryTab2", 12 + (1 * 36), 43, 26, 26, "2", FontRegular, White, , , , , , , , , , , , GetAddress(AddressOf Button_InventoryTab2_Click)
    CreateButton WindowCount, "btnInventoryTab3", 12 + (2 * 36), 43, 26, 26, "3", FontRegular, White, , , , , , , , , , , , GetAddress(AddressOf Button_InventoryTab3_Click)
    CreateButton WindowCount, "btnInventoryTab4", 12 + (3 * 36), 43, 26, 26, "4", FontRegular, White, , , , , , , , , , , , GetAddress(AddressOf Button_InventoryTab4_Click)
    CreateButton WindowCount, "btnInventoryTab5", 12 + (4 * 36), 43, 26, 26, "5", FontRegular, White, , , , , , , , , , , , GetAddress(AddressOf Button_InventoryTab5_Click)

    ' Gold amount
    CreateLabel WindowCount, "lblGold", 15, 355, 150, , "Ouro: 0", FontRegular, Gold
    
    WindowIndex = WindowCount
    InventoryentoryTabIndex = 0
    
    Call SetTabButtonColor(1, BrightGreen)
End Sub

Private Sub SetTabButtonColor(ByVal Index As Long, ByVal Color As Long)
    Dim i As Long
    Dim ControlIndex As Long

    For i = 1 To MaxInventoryTab
        ControlIndex = GetControlIndex("winInventory", "btnInventoryTab" & i)

        If i = Index Then
            Windows(WindowIndex).Controls(ControlIndex).TextColour = Color
            Windows(WindowIndex).Controls(ControlIndex).TextColourHover = Green
            Windows(WindowIndex).Controls(ControlIndex).TextColourClick = Green
        Else
            Windows(WindowIndex).Controls(ControlIndex).TextColour = White
            Windows(WindowIndex).Controls(ControlIndex).TextColourHover = White
            Windows(WindowIndex).Controls(ControlIndex).TextColourClick = White
        End If
    Next
End Sub

Private Sub Button_InventoryTab1_MouseMove()
    If DragBox.Origin = OriginInventory And DragBox.Type = PartItem Then
        InventoryentoryTabIndex = 0
        Call SetTabButtonColor(1, Green)
    End If
End Sub
Private Sub Button_InventoryTab2_MouseMove()
    If DragBox.Origin = OriginInventory And DragBox.Type = PartItem Then
        InventoryentoryTabIndex = 1
        Call SetTabButtonColor(2, Green)
    End If
End Sub
Private Sub Button_InventoryTab3_MouseMove()
    If DragBox.Origin = OriginInventory And DragBox.Type = PartItem Then
        InventoryentoryTabIndex = 2
        Call SetTabButtonColor(3, Green)
    End If
End Sub
Private Sub Button_InventoryTab4_MouseMove()
    If DragBox.Origin = OriginInventory And DragBox.Type = PartItem Then
        InventoryentoryTabIndex = 3
        Call SetTabButtonColor(4, Green)
    End If
End Sub
Private Sub Button_InventoryTab5_MouseMove()
    If DragBox.Origin = OriginInventory And DragBox.Type = PartItem Then
        InventoryentoryTabIndex = 4
        Call SetTabButtonColor(5, Green)
    End If
End Sub

Private Sub Button_InventoryTab1_Click()
    InventoryentoryTabIndex = 0
    Call SetTabButtonColor(1, Green)
End Sub
Private Sub Button_InventoryTab2_Click()
    InventoryentoryTabIndex = 1
    Call SetTabButtonColor(2, Green)
End Sub
Private Sub Button_InventoryTab3_Click()
    InventoryentoryTabIndex = 2
    Call SetTabButtonColor(3, Green)
End Sub
Private Sub Button_InventoryTab4_Click()
    InventoryentoryTabIndex = 3
    Call SetTabButtonColor(4, Green)
End Sub
Private Sub Button_InventoryTab5_Click()
    InventoryentoryTabIndex = 4
    Call SetTabButtonColor(5, Green)
End Sub

Private Sub Inventory_MouseDown()
    Dim InvNum As Long, WinIndex As Long, i As Long
    Dim ItemNum As Long

    ' is there an item?
    InvNum = GetInventorySlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

    If InTrade = 0 Then
        If (mouseClick(VK_RBUTTON) And lastMouseClick(VK_RBUTTON) = 0) Then
            If InvNum Then
                SendUseItem InvNum
            End If

            ' show desc. if needed
            Inventory_MouseMove
            Exit Sub
        End If
    End If

    If InvNum Then
        ' exit out if we're offering that item
        If InTrade Then

            For i = 1 To MaxTradeItems
                If GetMyTradeInventoryIndex(i) = InvNum Then
                    ItemNum = GetInventoryItemNum(InvNum)

                    If ItemNum = 0 Then
                        Exit Sub
                    End If
                    
                    If Item(ItemNum).MaxStack > 0 Or Item(ItemNum).Stackable Then
                        ' only exit out if we're offering all of it
                        If GetMyTradeInventoryItemValue(i) = GetInventoryItemValue(InvNum) Then
                            Exit Sub
                        End If
                    Else
                        Exit Sub
                    End If
                End If
            Next

            ItemNum = GetInventoryItemNum(InvNum)

            If ItemNum > 0 Then
                If Item(ItemNum).MaxStack > 0 Or Item(ItemNum).Stackable Then
                    ShowDialogue "Negociação", "Insira a quantidade", "", DialogueTypeTradeAmount, DialogueStyleInput, InvNum
                    Exit Sub
                End If
            End If

            ' trade the normal item
            Call SendTradeItem(InvNum, 1)
            Exit Sub
        End If
        'trade

        If MailingWindowState = WindowMailState_Writing Then


        End If

        ' drag it
        With DragBox
            .Type = PartItem
            .Value = GetInventoryItemNum(InvNum)
            .Origin = OriginInventory
            .Slot = InvNum
        End With

        WinIndex = GetWindowIndex("winDragBox")
        With Windows(WinIndex).Window
            .State = MouseDown
            .Left = lastMouseX - 16
            .Top = lastMouseY - 16
            .MovedX = clickedX - .Left
            .MovedY = clickedY - .Top
        End With

        ShowWindow WinIndex, , False
        ' stop dragging inventory
        Windows(WindowIndex).Window.State = Normal
    End If

    ' show desc. if needed
    Inventory_MouseMove
End Sub

Private Sub Inventory_DblClick()
    Dim InvNum As Long, i As Long

    If InTrade > 0 Then Exit Sub

    InvNum = GetInventorySlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

    If InvNum > 0 Then
        SendUseItem InvNum
    End If

    ' show desc. if needed
    Inventory_MouseMove
End Sub

Private Sub Inventory_MouseMove()
    Dim InvNum As Long, x As Long, Y As Long, i As Long

    ' exit out early if dragging
    If DragBox.Type <> PartNone Then Exit Sub

    InvNum = GetInventorySlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

    If InvNum > 0 Then
        ' make sure we're not dragging the item
        If DragBox.Type = PartItem And DragBox.Value = InvNum Then Exit Sub

        For i = 1 To MaxTradeItems
            If GetMyTradeInventoryIndex(i) = InvNum And GetMyTradeInventoryItemValue(i) >= GetInventoryItemValue(InvNum) Then
                Exit Sub
            End If
        Next

        ' calc position
        x = Windows(WindowIndex).Window.Left - Windows(GetWindowIndex("winDescription")).Window.Width - 2
        Y = Windows(WindowIndex).Window.Top

        ' offscreen?
        If x < 0 Then
            ' switch to right
            x = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width + 2
        End If

        ' go go go
        ShowInvDesc x, Y, InvNum
    End If
End Sub

Private Sub DrawInventory()
    Dim xO As Long, yO As Long, Width As Long, Height As Long, i As Long, Y As Long, ItemNum As Long, ItemPic As Long, x As Long, Top As Long, Left As Long, Amount As String
    Dim Colour As Long, skipItem As Boolean, amountModifier As Long, tmpItem As Long
    Dim CurrentInventoryIndex As Long, n As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width

    '
    CurrentInventoryIndex = InventoryentoryTabIndex * MaxInventoryPerTab

    Width = 76
    Height = 76

    Y = yO + 80
    ' render grid - row
    For i = 1 To 4
        If i = 4 Then Height = 38
        RenderTexture Tex_GUI(26), xO + 4, Y, 0, 0, Width, Height, Width, Height
        RenderTexture Tex_GUI(26), xO + 80, Y, 0, 0, Width, Height, Width, Height
        RenderTexture Tex_GUI(26), xO + 156, Y, 0, 0, 42, Height, 42, Height
        Y = Y + 76
    Next
    ' render bottom wood

    ' actually draw the icons
    For i = 1 To MaxInventoryPerTab
        
        ' Trade
        If InTrade > 0 Then
            For n = 1 To MaxTradeItems
                If GetMyTradeInventoryIndex(n) > 0 Then
                    If GetMyTradeInventoryIndex(n) = i And GetMyTradeInventoryItemValue(n) >= GetInventoryItemValue(GetMyTradeInventoryIndex(n)) Then
                        skipItem = True
                        Exit For
                    ElseIf GetMyTradeInventoryIndex(n) = i And GetMyTradeInventoryItemValue(n) < GetInventoryItemValue(GetMyTradeInventoryIndex(n)) Then
                        amountModifier = GetMyTradeInventoryItemValue(n)
                        Exit For
                    End If
                End If
            Next
        End If
    
        ' Mail
        If MailingWindowState = WindowMailState_Writing Then
            If SendMailItemInventoryIndex > 0 Then
                If SendMailItemInventoryIndex = i + CurrentInventoryIndex And SendMailItemValue >= GetInventoryItemValue(SendMailItemInventoryIndex) Then
                    skipItem = True
                ElseIf SendMailItemInventoryIndex = i + CurrentInventoryIndex And SendMailItemValue < GetInventoryItemValue(SendMailItemInventoryIndex) Then
                    amountModifier = SendMailItemValue
                End If
            End If
        End If

        If (i + CurrentInventoryIndex) > MaxInventories Then
            Top = yO + InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
            Left = xO + InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))

            Y = Top + 8
            x = Left + 13
            RenderText Font(Fonts.FontRegular), "X", x, Y, Gold
        End If

        ItemNum = GetInventoryItemNum(i + CurrentInventoryIndex)

        If ItemNum > 0 And ItemNum <= MaximumItems Then
            ' not dragging?
            If Not (DragBox.Origin = OriginInventory And DragBox.Slot = i + CurrentInventoryIndex) Then
                ItemPic = Item(ItemNum).IconId

                If Not skipItem Then
                    If ItemPic > 0 And ItemPic <= Count_Item Then
                        Top = yO + InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
                        Left = xO + InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))

                        ' draw icon
                        RenderTexture Tex_Item(ItemPic), Left, Top, 0, 0, 32, 32, 32, 32

                        ' If item is a stack - draw the amount you have
                        If GetInventoryItemValue(i + CurrentInventoryIndex) > 1 Then
                            Y = Top + 21
                            x = Left + 1
                            Amount = GetInventoryItemValue(i + CurrentInventoryIndex) - amountModifier

                            ' Draw currency but with k, m, b etc. using a convertion function
                            Colour = GetCurrencyColor(Amount)
                            RenderText Font(Fonts.FontRegular), ConvertCurrency(Amount), x, Y, Colour
                        End If
                    End If
                End If

                ' reset
                skipItem = False
            End If
        End If
    Next
End Sub

Private Function GetInventorySlotFromPosition(StartX As Long, StartY As Long) As Long
    Dim TempRec As RECT
    Dim i As Long
    
    For i = 1 To MaxInventoryPerTab
        If GetInventoryItemNum(i + (InventoryentoryTabIndex * MaxInventoryPerTab)) Then

            With TempRec
                .Top = StartY + InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
                .Bottom = .Top + PIC_Y
                .Left = StartX + InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))
                .Right = .Left + PIC_X
            End With

            If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
                If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
                    GetInventorySlotFromPosition = i + (InventoryentoryTabIndex * MaxInventoryPerTab)
                    Exit Function
                End If
            End If
        End If
    Next
End Function

Public Sub UpdateInventoryCurrency()
    Dim Value As Long
    
    Value = GetPlayerCurrency(CurrencyType.Currency_Gold)
    
    If Value = 0 Then
        Windows(GetWindowIndex("winInventory")).Controls(GetControlIndex("winInventory", "lblGold")).Text = "Ouro: 0"
    Else
        Windows(GetWindowIndex("winInventory")).Controls(GetControlIndex("winInventory", "lblGold")).Text = "Ouro: " & Format$(Value, "#,###,###,###")
    End If
End Sub

Public Sub ShowInvDesc(x As Long, Y As Long, InvNum As Long)

    If InvNum <= 0 Or InvNum > MaxInventory Then Exit Sub
    Dim ItemNum As Long

    ItemNum = GetInventoryItemNum(InvNum)

    If ItemNum > 0 Then
         Dim Inventory As InventoryRec
    
         Inventory.Num = GetInventoryItemNum(InvNum)
         Inventory.Value = GetInventoryItemValue(InvNum)
         Inventory.Level = GetInventoryItemLevel(InvNum)
         Inventory.Bound = GetInventoryItemBound(InvNum)
         Inventory.AttributeId = GetInventoryItemAttributeId(InvNum)
         Inventory.UpgradeId = GetInventoryItemUpgradeId(InvNum)
    
        If Item(ItemNum).Type = ItemType.ItemType_Heraldry Then
            Call ShowHeraldryDescription(x, Y, Inventory, Item(ItemNum).Price)
        Else
            ShowItemDesc x, Y, Inventory
        End If
    End If
End Sub

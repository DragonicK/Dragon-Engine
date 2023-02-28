Attribute VB_Name = "Warehouse_Window"
Option Explicit

' Warehouse constants
Private Const WarehouseTop As Long = 49
Private Const WarehouseLeft As Long = 10
Private Const WarehouseOffsetY As Long = 6
Private Const WarehouseOffsetX As Long = 6
Private Const WarehouseColumns As Long = 5

' Warehouse Window Index.
Private WindowIndex As Long

Public Function IsWarehouseVisible() As Boolean
    IsWarehouseVisible = Windows(WindowIndex).Window.Visible
End Function

Public Sub ShowWarehouse()
    Windows(WindowIndex).Window.Visible = True
End Sub

Public Sub HideWarehouse()
    Windows(WindowIndex).Window.Visible = False
End Sub

Public Sub CreateWindow_Warehouse()
    CreateWindow "winWarehouse", "ARMAZÉM", zOrder_Win, 0, 0, 204, 432, 0, True, Fonts.OpenSans_Regular, , 2, 5, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , GetAddress(AddressOf Warehouse_MouseMove), GetAddress(AddressOf Warehouse_MouseDown), GetAddress(AddressOf Warehouse_MouseMove), 0, , , GetAddress(AddressOf DrawWarehouse)
    
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonMenu_Warehouse)

    ' Set de WindowIndex variable to avoid search for index.
    WindowIndex = WindowCount
End Sub

Private Sub ButtonMenu_Warehouse()
    If GetPlayerDead(MyIndex) Then Exit Sub

    If Windows(WindowIndex).Window.Visible Then
        InWarehouse = False
        
        Call SendCloseWarehouse
    End If

    Windows(WindowIndex).Window.Visible = Not Windows(WindowIndex).Window.Visible
End Sub

Private Sub DrawWarehouse()
    Dim X As Long, Y As Long, xO As Long, yO As Long, Width As Long, Height As Long
    Dim i As Long, ItemNum As Long, ItemPic As Long

    Dim Left As Long, Top As Long
    Dim Colour As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width
    Height = Windows(WindowIndex).Window.Height

    Width = 76
    Height = 76

    Y = yO + 44
    
    ' Render grid - Row.
    For i = 1 To 5
        If i = 4 Then Height = 38
        
        RenderTexture Tex_GUI(26), xO + 5, Y, 0, 0, 80, 80, 80, 80
        RenderTexture Tex_GUI(26), xO + 81, Y, 0, 0, 80, 80, 80, 80
        RenderTexture Tex_GUI(26), xO + 157, Y, 0, 0, 42, 80, 42, 80

        Y = Y + 76
    Next

    ' actually draw the icons
    For i = 1 To MaxWarehouse
        ItemNum = GetWarehouseItemNum(i)

        If ItemNum > 0 And ItemNum <= MaximumItems Then
            ' not dragging?
            If Not (DragBox.Origin = OriginWarehouse And DragBox.Slot = i) Then
                ItemPic = Item(ItemNum).IconId

                If ItemPic > 0 And ItemPic <= Count_Item Then
                    Top = yO + WarehouseTop + ((WarehouseOffsetY + 32) * ((i - 1) \ WarehouseColumns))
                    Left = xO + WarehouseLeft + ((WarehouseOffsetX + 32) * (((i - 1) Mod WarehouseColumns)))

                    ' draw icon
                    RenderTexture Tex_Item(ItemPic), Left, Top, 0, 0, 32, 32, 32, 32

                    ' If item is a stack - draw the amount you have
                    If GetWarehouseItemValue(i) > 1 Then
                        Y = Top + 21
                        X = Left + 1

                        ' Draw currency but with k, m, b etc. using a convertion function
                        Colour = GetCurrencyColor(GetWarehouseItemValue(i))
                        RenderText Font(Fonts.OpenSans_Regular), ConvertCurrency(GetWarehouseItemValue(i)), X, Y, Colour
                    End If
                End If
            End If
        End If
    Next

End Sub

Private Function GetWarehouseSlotFromPosition(StartX As Long, StartY As Long) As Long
    Dim TempRec As RECT
    Dim i As Long

    For i = 1 To MaxWarehouse
        ' Check early if there's some item here.
        If GetWarehouseItemNum(i) > 0 Then

            With TempRec
                .Top = StartY + WarehouseTop + ((WarehouseOffsetY + 32) * ((i - 1) \ WarehouseColumns))
                .Bottom = .Top + PIC_Y
                .Left = StartX + WarehouseLeft + ((WarehouseOffsetX + 32) * (((i - 1) Mod WarehouseColumns)))
                .Right = .Left + PIC_X
            End With

            If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
                If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
                    GetWarehouseSlotFromPosition = i
                    Exit Function
                End If
            End If
        End If
    Next

End Function

Private Sub Warehouse_MouseMove()
    Dim Slot As Long, X As Long, Y As Long, i As Long
    Dim ItemNum As Long, WinDescription As Long

    ' exit out early if dragging
    If DragBox.Type <> PartNone Then Exit Sub

    Slot = GetWarehouseSlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

    If Slot > 0 Then
        ItemNum = GetWarehouseItemNum(Slot)

        If ItemNum <= 0 Or ItemNum > MaximumItems Then
            Exit Sub
        End If

        ' make sure we're not dragging the item
        If DragBox.Type = PartItem And DragBox.Value = ItemNum Then Exit Sub

        WinDescription = GetWindowIndex("winDescription")

        ' calc position
        X = Windows(WindowIndex).Window.Left - Windows(GetWindowIndex("winDescription")).Window.Width
        Y = Windows(WindowIndex).Window.Top - 4

        ' offscreen?
        If X < 0 Then
            ' switch to right
            X = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width
        End If

        If Y + Windows(WinDescription).Window.Height >= ScreenHeight Then
            Y = ScreenHeight - Windows(WinDescription).Window.Height
        End If

        Dim Inventory As InventoryRec

        Inventory.Num = GetWarehouseItemNum(Slot)
        Inventory.Value = GetWarehouseItemValue(Slot)
        Inventory.Level = GetWarehouseItemLevel(Slot)
        Inventory.Bound = GetWarehouseItemBound(Slot)
        Inventory.AttributeId = GetWarehouseItemAttributeId(Slot)
        Inventory.UpgradeId = GetWarehouseItemUpgradeId(Slot)

        If Item(Inventory.Num).Type = ItemType.ItemType_Heraldry Then
            Call ShowHeraldryDescription(X, Y, Inventory, Item(Inventory.Num).Price)
        Else
            ShowItemDesc X, Y, Inventory
        End If
    End If
End Sub

Private Sub Warehouse_MouseDown()
    Dim Slot As Long, WinIndex As Long, i As Long
    Dim ItemNum As Long

    ' is there an item?
    Slot = GetWarehouseSlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

    If Slot > 0 Then
        ItemNum = GetWarehouseItemNum(Slot)
        
        If ItemNum <= 0 Or ItemNum > MaximumItems Then
            Exit Sub
        End If
        
        ' Check for right click
        If (mouseClick(VK_RBUTTON) And lastMouseClick(VK_RBUTTON) = 0) Then
            If Item(ItemNum).Stackable = 0 Then
                SendWithdrawItem Slot, 1
            Else
                ShowDialogue "Retirar Item", "Insira a quantidade que deseja retirar", "", DialogueTypeWithdrawItem, DialogueStyleInput, Slot
            End If
        Else
            ' drag it
            With DragBox
                .Type = PartItem
                .Value = ItemNum
                .Origin = OriginWarehouse
                .Slot = Slot
            End With

            WinIndex = GetWindowIndex("winDragBox")
            With Windows(WinIndex).Window
                .State = MouseDown
                .Left = lastMouseX - 16
                .Top = lastMouseY - 16
                .MovedX = clickedX - .Left
                .movedY = clickedY - .Top
            End With

            ShowWindow WinIndex, , False
            ' stop dragging
            Windows(WindowIndex).Window.State = Normal
        End If
    End If

    ' show desc. if needed
    Warehouse_MouseMove
End Sub

Public Sub DragBox_WarehouseToWarehouse()
    Dim i As Long
    Dim tmpRec As RECT

    If DragBox.Origin = OriginWarehouse Then
        ' it's from the inventory!
        If DragBox.Type = PartItem Then
            ' find the slot to switch with
            For i = 1 To MaxWarehouse
                With tmpRec
                    .Top = Windows(WindowIndex).Window.Top + WarehouseTop + ((WarehouseOffsetY + 32) * ((i - 1) \ WarehouseColumns))
                    .Bottom = .Top + 32
                    .Left = Windows(WindowIndex).Window.Left + WarehouseLeft + ((WarehouseOffsetX + 32) * (((i - 1) Mod WarehouseColumns)))
                    .Right = .Left + 32
                End With

                If currMouseX >= tmpRec.Left And currMouseX <= tmpRec.Right Then
                    If currMouseY >= tmpRec.Top And currMouseY <= tmpRec.Bottom Then
                        ' switch the slots
                        If DragBox.Slot <> i Then SendSwapWarehouseSlots DragBox.Slot, i
                        Exit For
                    End If
                End If
            Next
        End If
    End If
End Sub

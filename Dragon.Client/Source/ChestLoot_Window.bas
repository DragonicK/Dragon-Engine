Attribute VB_Name = "ChestLoot_Window"
Option Explicit

Private Const LIST_OFFSET_Y = 40
Private Const LIST_COUNT = 6
                 
Private WindowIndex As Long

Public ChestItemCount As Long
Public ChestItem() As ChestItemRec

Public CanSendTakeItemFromChest As Boolean
Public ChestItemListIndex As Long

Public Enum ChestItemType
    ChestItemType_Item
    ChestItemType_Currency
End Enum

Private Type ChestItemRec
    ItemType As ChestItemType
    CurrencyType As CurrencyType
    Num As Long
    Value As Long
    Level As Long
    AttributeId As Long
    UpgradeId As Long
    Bound As Boolean
End Type

Public Sub CreateWindow_ChestItems()
    Dim i As Long

    ' Create window
    CreateWindow "winChestItem", "ITEMS", zOrder_Win, 0, 0, 280, 340, 0, False, Fonts.FontRegular, , 2, 7, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar
    ' Centralise it
    CentraliseWindow WindowCount
    ' Set the index for spawning controls
    zOrder_Con = 1
    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf btnMenu_Loot)

    CreatePictureBox WindowCount, "picIcon1", 15, 60 + (LIST_OFFSET_Y * 0), 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot1_MouseMove), GetAddress(AddressOf ItemSlot1_MouseDown), GetAddress(AddressOf ItemSlot1_MouseMove)
    CreatePictureBox WindowCount, "picIcon2", 15, 60 + (LIST_OFFSET_Y * 1), 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot2_MouseMove), GetAddress(AddressOf ItemSlot2_MouseDown), GetAddress(AddressOf ItemSlot2_MouseMove)
    CreatePictureBox WindowCount, "picIcon3", 15, 60 + (LIST_OFFSET_Y * 2), 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot3_MouseMove), GetAddress(AddressOf ItemSlot3_MouseDown), GetAddress(AddressOf ItemSlot3_MouseMove)
    CreatePictureBox WindowCount, "picIcon4", 15, 60 + (LIST_OFFSET_Y * 3), 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot4_MouseMove), GetAddress(AddressOf ItemSlot4_MouseDown), GetAddress(AddressOf ItemSlot4_MouseMove)
    CreatePictureBox WindowCount, "picIcon5", 15, 60 + (LIST_OFFSET_Y * 4), 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot5_MouseMove), GetAddress(AddressOf ItemSlot5_MouseDown), GetAddress(AddressOf ItemSlot5_MouseMove)
    CreatePictureBox WindowCount, "picIcon6", 15, 60 + (LIST_OFFSET_Y * 5), 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot6_MouseMove), GetAddress(AddressOf ItemSlot6_MouseDown), GetAddress(AddressOf ItemSlot6_MouseMove)

    CreatePictureBox WindowCount, "picName1", 47, 60 + (LIST_OFFSET_Y * 0), 190, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot1_MouseMove), GetAddress(AddressOf ItemSlot1_MouseDown), GetAddress(AddressOf ItemSlot1_MouseMove)
    CreatePictureBox WindowCount, "picName2", 47, 60 + (LIST_OFFSET_Y * 1), 190, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot2_MouseMove), GetAddress(AddressOf ItemSlot2_MouseDown), GetAddress(AddressOf ItemSlot2_MouseMove)
    CreatePictureBox WindowCount, "picName3", 47, 60 + (LIST_OFFSET_Y * 2), 190, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot3_MouseMove), GetAddress(AddressOf ItemSlot3_MouseDown), GetAddress(AddressOf ItemSlot3_MouseMove)
    CreatePictureBox WindowCount, "picName4", 47, 60 + (LIST_OFFSET_Y * 3), 190, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot4_MouseMove), GetAddress(AddressOf ItemSlot4_MouseDown), GetAddress(AddressOf ItemSlot4_MouseMove)
    CreatePictureBox WindowCount, "picName5", 47, 60 + (LIST_OFFSET_Y * 4), 190, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot5_MouseMove), GetAddress(AddressOf ItemSlot5_MouseDown), GetAddress(AddressOf ItemSlot5_MouseMove)
    CreatePictureBox WindowCount, "picName6", 47, 60 + (LIST_OFFSET_Y * 5), 190, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot6_MouseMove), GetAddress(AddressOf ItemSlot6_MouseDown), GetAddress(AddressOf ItemSlot6_MouseMove)

    CreateLabel WindowCount, "lblItemName1", 55, 62 + (LIST_OFFSET_Y * 0), 190, 20, "", FontRegular, Coral, Alignment.AlignLeft, , , , , GetAddress(AddressOf ItemSlot1_MouseMove), GetAddress(AddressOf ItemSlot1_MouseDown), GetAddress(AddressOf ItemSlot1_MouseMove)
    CreateLabel WindowCount, "lblItemName2", 55, 62 + (LIST_OFFSET_Y * 1), 190, 20, "", FontRegular, Coral, Alignment.AlignLeft, , , , , GetAddress(AddressOf ItemSlot2_MouseMove), GetAddress(AddressOf ItemSlot2_MouseDown), GetAddress(AddressOf ItemSlot2_MouseMove)
    CreateLabel WindowCount, "lblItemName3", 55, 62 + (LIST_OFFSET_Y * 2), 190, 20, "", FontRegular, Coral, Alignment.AlignLeft, , , , , GetAddress(AddressOf ItemSlot3_MouseMove), GetAddress(AddressOf ItemSlot3_MouseDown), GetAddress(AddressOf ItemSlot3_MouseMove)
    CreateLabel WindowCount, "lblItemName4", 55, 62 + (LIST_OFFSET_Y * 3), 190, 20, "", FontRegular, Coral, Alignment.AlignLeft, , , , , GetAddress(AddressOf ItemSlot4_MouseMove), GetAddress(AddressOf ItemSlot4_MouseDown), GetAddress(AddressOf ItemSlot4_MouseMove)
    CreateLabel WindowCount, "lblItemName5", 55, 62 + (LIST_OFFSET_Y * 4), 190, 20, "", FontRegular, Coral, Alignment.AlignLeft, , , , , GetAddress(AddressOf ItemSlot5_MouseMove), GetAddress(AddressOf ItemSlot5_MouseDown), GetAddress(AddressOf ItemSlot5_MouseMove)
    CreateLabel WindowCount, "lblItemName6", 55, 62 + (LIST_OFFSET_Y * 5), 190, 20, "", FontRegular, Coral, Alignment.AlignLeft, , , , , GetAddress(AddressOf ItemSlot6_MouseMove), GetAddress(AddressOf ItemSlot6_MouseDown), GetAddress(AddressOf ItemSlot6_MouseMove)

    CreateLabel WindowCount, "lblItemCount1", 55, 74 + (LIST_OFFSET_Y * 0), 190, 20, "", FontRegular, White, Alignment.AlignLeft, , , , , GetAddress(AddressOf ItemSlot1_MouseMove), GetAddress(AddressOf ItemSlot1_MouseDown), GetAddress(AddressOf ItemSlot1_MouseMove)
    CreateLabel WindowCount, "lblItemCount2", 55, 74 + (LIST_OFFSET_Y * 1), 190, 20, "", FontRegular, White, Alignment.AlignLeft, , , , , GetAddress(AddressOf ItemSlot2_MouseMove), GetAddress(AddressOf ItemSlot2_MouseDown), GetAddress(AddressOf ItemSlot2_MouseMove)
    CreateLabel WindowCount, "lblItemCount3", 55, 74 + (LIST_OFFSET_Y * 2), 190, 20, "", FontRegular, White, Alignment.AlignLeft, , , , , GetAddress(AddressOf ItemSlot3_MouseMove), GetAddress(AddressOf ItemSlot3_MouseDown), GetAddress(AddressOf ItemSlot3_MouseMove)
    CreateLabel WindowCount, "lblItemCount4", 55, 74 + (LIST_OFFSET_Y * 3), 190, 20, "", FontRegular, White, Alignment.AlignLeft, , , , , GetAddress(AddressOf ItemSlot4_MouseMove), GetAddress(AddressOf ItemSlot4_MouseDown), GetAddress(AddressOf ItemSlot4_MouseMove)
    CreateLabel WindowCount, "lblItemCount5", 55, 74 + (LIST_OFFSET_Y * 4), 190, 20, "", FontRegular, White, Alignment.AlignLeft, , , , , GetAddress(AddressOf ItemSlot5_MouseMove), GetAddress(AddressOf ItemSlot5_MouseDown), GetAddress(AddressOf ItemSlot5_MouseMove)
    CreateLabel WindowCount, "lblItemCount6", 55, 74 + (LIST_OFFSET_Y * 5), 190, 20, "", FontRegular, White, Alignment.AlignLeft, , , , , GetAddress(AddressOf ItemSlot6_MouseMove), GetAddress(AddressOf ItemSlot6_MouseDown), GetAddress(AddressOf ItemSlot6_MouseMove)
    
    'Botões setas
    CreateButton WindowCount, "btnUp", 250, 60, 15, 15, , , , , , , Tex_GUI(44), Tex_GUI(45), Tex_GUI(46), , , , , , GetAddress(AddressOf MoveListToUp)
    CreateButton WindowCount, "btnDown", 250, 276, 15, 15, , , , , , , Tex_GUI(47), Tex_GUI(48), Tex_GUI(49), , , , , , GetAddress(AddressOf MoveListToDown)

    CreatePictureBox WindowCount, "picName", 15, 305, 250, 22, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox
    CreateLabel WindowCount, "lblItemCount", 15, 308, 250, 22, "Quantidade: 1", FontRegular, White, Alignment.AlignCenter

    WindowIndex = WindowCount
End Sub

Private Sub TakeItem(ByVal Index As Long)
    If Index > ChestItemCount Then
        Exit Sub
    End If

    Dim ItemType As ChestItemType
    Dim CanTakeItem As Boolean

    ItemType = ChestItem(Index).ItemType

    If ItemType = ChestItemType_Item Then
        Dim ItemNum As Long

        ItemNum = ChestItem(Index).Num
        
        If ItemNum > 0 Then CanTakeItem = True

    ElseIf ItemType = ChestItemType_Currency Then
        Dim CurrencyValue As Long

        CurrencyValue = ChestItem(Index).Value
        
        If CurrencyValue > 0 Then CanTakeItem = True
    End If

    ' Verifica se o comando já foi enviado.
    If CanTakeItem Then
        ' Permite que os drops de moeda sejam coletados sem restrições.
        If ItemType = ChestItemType_Currency Then CanSendTakeItemFromChest = True

        If CanSendTakeItemFromChest Then
            Call SendTakeItemFromChest(Index)
            ' Somente permite que um item seja coletado por vez pela resposta do servidor.
            CanSendTakeItemFromChest = False
        End If
    End If

End Sub

Private Sub ItemSlot1_MouseDown()
    Call TakeItem(ChestItemListIndex + 1)

    Call ItemSlot1_MouseMove
End Sub
Private Sub ItemSlot2_MouseDown()
    Call TakeItem(ChestItemListIndex + 2)

    ItemSlot2_MouseMove
End Sub
Private Sub ItemSlot3_MouseDown()
    Call TakeItem(ChestItemListIndex + 3)

    ItemSlot3_MouseMove
End Sub
Private Sub ItemSlot4_MouseDown()
    Call TakeItem(ChestItemListIndex + 4)

    ItemSlot4_MouseMove
End Sub
Private Sub ItemSlot5_MouseDown()
    Call TakeItem(ChestItemListIndex + 5)

    ItemSlot5_MouseMove
End Sub
Private Sub ItemSlot6_MouseDown()
    Call TakeItem(ChestItemListIndex + 6)

    ItemSlot6_MouseMove
End Sub

Private Sub ShowLootDescription(ByVal Index As Long)
    If Index > ChestItemCount Then
        Exit Sub
    End If

    Dim ItemType As ChestItemType
    Dim X As Long, Y As Long
    Dim WinDescription As Long

    WinDescription = GetWindowIndex("winDescription")

    ItemType = ChestItem(Index).ItemType

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

    If ItemType = ChestItemType.ChestItemType_Item Then
        Dim Inventory As InventoryRec
    
        Inventory.Num = ChestItem(Index).Num
        Inventory.Level = ChestItem(Index).Level
        Inventory.UpgradeId = ChestItem(Index).UpgradeId
        Inventory.AttributeId = ChestItem(Index).AttributeId
        Inventory.Bound = ChestItem(Index).Bound

        Call ShowItemDesc(X, Y, Inventory)

    ElseIf ItemType = ChestItemType.ChestItemType_Currency Then
        Dim CurrencyValue As Long, CurType As CurrencyType

        CurType = ChestItem(Index).CurrencyType
        CurrencyValue = ChestItem(Index).Value

        ShowCurrencyDesc X, Y, CurType, CurrencyValue
    End If

End Sub

Private Sub ItemSlot1_MouseMove()
    Call ShowLootDescription(ChestItemListIndex + 1)
End Sub

Private Sub ItemSlot2_MouseMove()
    Call ShowLootDescription(ChestItemListIndex + 2)
End Sub

Private Sub ItemSlot3_MouseMove()
    Call ShowLootDescription(ChestItemListIndex + 3)
End Sub

Private Sub ItemSlot4_MouseMove()
    Call ShowLootDescription(ChestItemListIndex + 4)
End Sub

Private Sub ItemSlot5_MouseMove()
    Call ShowLootDescription(ChestItemListIndex + 5)
End Sub

Private Sub ItemSlot6_MouseMove()
    Call ShowLootDescription(ChestItemListIndex + 6)
End Sub
Private Sub MoveListToUp()
    If ChestItemListIndex > 0 Then
        ChestItemListIndex = ChestItemListIndex - 1
        UpdateChestItemList
    End If
End Sub

Private Sub MoveListToDown()
    If ChestItemListIndex < (ChestItemCount - LIST_COUNT) Then
        ChestItemListIndex = ChestItemListIndex + 1
        UpdateChestItemList
    End If
End Sub

Private Sub btnMenu_Loot()
    If GetPlayerDead(MyIndex) Then Exit Sub

    If Windows(WindowIndex).Window.Visible Then
        Call SendCloseChest

        HideWindow WindowIndex

        If MyTargetType = TargetTypeChest Then
            MyTargetType = TargetTypeNone
            MyTargetIndex = 0
        End If
    End If

End Sub

Public Sub UpdateChestItemList()
    Dim i As Long

    Dim ControlNameIndex As Long
    Dim ControlCountIndex As Long

    Dim ItemNum As Long, Rarity As Long, ItemValue As Long, ItemLevel As Long
    Dim CurrencyType As Byte

    For i = 1 To LIST_COUNT
        ControlNameIndex = GetControlIndex("winChestItem", "lblItemName" & i)
        ControlCountIndex = GetControlIndex("winChestItem", "lblItemCount" & i)

        If (i + ChestItemListIndex) <= ChestItemCount Then
            ItemNum = ChestItem(i + ChestItemListIndex).Num
            ItemValue = ChestItem(i + ChestItemListIndex).Value
            ItemLevel = ChestItem(i + ChestItemListIndex).Level

            CurrencyType = ChestItem(i + ChestItemListIndex).CurrencyType

            If ChestItem(i + ChestItemListIndex).ItemType = ChestItemType.ChestItemType_Item Then
                Rarity = GetRarityColor(Item(ItemNum).Rarity)

                If ItemLevel > 0 Then
                    Windows(WindowIndex).Controls(ControlNameIndex).Text = Item(ItemNum).Name & " +" & ItemLevel
                Else
                    Windows(WindowIndex).Controls(ControlNameIndex).Text = Item(ItemNum).Name
                End If

                Windows(WindowIndex).Controls(ControlNameIndex).TextColour = Rarity
                Windows(WindowIndex).Controls(ControlNameIndex).TextColourClick = Rarity
                Windows(WindowIndex).Controls(ControlNameIndex).TextColourHover = Rarity

                Windows(WindowIndex).Controls(ControlCountIndex).Text = "Quantidade: " & ItemValue

                Call SetControlImage(i, Tex_Item(Item(ItemNum).IconId))

            ElseIf ChestItem(i + ChestItemListIndex).ItemType = ChestItemType.ChestItemType_Currency Then
                Dim CurRec As CurrencyRec

                CurRec = GetCurrencyData(CurrencyType)

                Windows(WindowIndex).Controls(ControlNameIndex).Text = CurRec.Name
                Windows(WindowIndex).Controls(ControlNameIndex).TextColour = White
                Windows(WindowIndex).Controls(ControlNameIndex).TextColourClick = White
                Windows(WindowIndex).Controls(ControlNameIndex).TextColourHover = White

                Windows(WindowIndex).Controls(ControlCountIndex).Text = "Quantidade: " & ItemValue

                Call SetControlImage(i, Tex_Item(CurRec.IconId))
            End If
        Else
            Windows(WindowIndex).Controls(ControlNameIndex).Text = vbNullString
            Windows(WindowIndex).Controls(ControlCountIndex).Text = vbNullString

            Call SetControlImage(i, 0)
        End If
    Next

    Windows(WindowIndex).Controls(GetControlIndex("winChestItem", "lblItemCount")).Text = "Quantidade: " & ChestItemCount

End Sub

Private Sub SetControlImage(ByVal Index As Long, ByVal TextureNum As Long)
    Dim i As Long, ControlIndex As Long

    ControlIndex = GetControlIndex("winChestItem", "picIcon" & Index)

    For i = 0 To entStates.state_Count - 1
        Windows(WindowIndex).Controls(ControlIndex).image(i) = TextureNum
    Next

End Sub

Public Sub CheckForCloseLoot()
    If Windows(WindowIndex).Window.Visible Then
        HideWindow WindowIndex
    End If

    If MyTargetType = TargetTypeChest Then
        MyTargetType = TargetTypeNone
        MyTargetIndex = 0
    End If
End Sub


